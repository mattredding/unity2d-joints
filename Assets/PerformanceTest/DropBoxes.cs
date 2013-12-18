using UnityEngine;
using System.Collections;

public class DropBoxes : MonoBehaviour {

	[SerializeField]
	GameObject box;
	int droppedBoxCount = 0;
	int totalBoxCount = 300;

	void Start()
	{
		InvokeRepeating("DropBox", 0.5f, 0.05f);
	}

	void DropBox()
	{
		if(droppedBoxCount >= totalBoxCount)
		{
			CancelInvoke();
			return;
		}
		droppedBoxCount ++;
		Vector3 position = new Vector3(
			Random.Range(-5f, 5f),
			6f,
			0f
			);
		GameObject.Instantiate(box, position, Quaternion.identity);
	}

}
