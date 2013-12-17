using UnityEngine;
using System.Collections;

public class ClackerLine: MonoBehaviour {
	
	public Transform parentTransform;
	Vector3 pivot;
	LineRenderer line;
	
	void Start () {
		line = GetComponent<LineRenderer>();
		pivot = new Vector3(
			transform.position.x,
			parentTransform.position.y -transform.position.y,
			transform.position.z
			);
	}
	
	void Update () {
		line.SetPosition(0, transform.position);
		line.SetPosition(1, pivot);
	}
}
