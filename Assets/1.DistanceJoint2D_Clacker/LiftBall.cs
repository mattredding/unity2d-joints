using UnityEngine;
using System.Collections;

public class LiftBall : MonoBehaviour {

	DistanceJoint2D lifter;

	void Start () {
		lifter = GetComponent<DistanceJoint2D>();
	}

	void Update () {
		if(Input.anyKeyDown)
		{
			lifter.enabled = !lifter.enabled;
		}
	}
}
