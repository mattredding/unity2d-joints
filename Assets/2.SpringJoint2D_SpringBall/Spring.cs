using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour {

	public Transform parentTransform;
	LineRenderer line;

	bool springCompressed;

	[SerializeField]
	SpringJoint2D spring;

	SliderJoint2D slider;

	void Start () {
		line = GetComponent<LineRenderer>();
		slider = GetComponent<SliderJoint2D>();
	}
	
	void FixedUpdate () {
		line.SetPosition(1, parentTransform.localPosition);
		if(Input.anyKey)
		{
			CompressSpring();
		}
		else
		{
			if(springCompressed)
			{
				ReleaseSpring();
			}
		}
	}

	void CompressSpring()
	{
		springCompressed = true;
		slider.useMotor = true;
	}

	void ReleaseSpring()
	{
		springCompressed = false;
		slider.useMotor = false;
	}
}
