using UnityEngine;
using System.Collections;

public class Piston : MonoBehaviour {

	[SerializeField]
	Rigidbody2D pistonHead;

	Vector3 explosionForce;

	void Start () {
		explosionForce = Vector2.up *- 600f;
	}
	
	void OnTriggerEnter2D (Collider2D collider)
	{
		pistonHead.AddForce(explosionForce);
	}
}
