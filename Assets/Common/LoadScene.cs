using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	public string levelName;

	void OnClick()
	{
		Application.LoadLevel(levelName);
	}
}
