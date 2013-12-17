using UnityEngine;
using System.Collections;

public class Chain : MonoBehaviour {
	
	public int linkCount;
	public GameObject ballPrefab;
	public GameObject linkPrefab1;
	public GameObject linkPrefab2;
	
	void Start () {
		CreateChain(linkCount);
	}
	
	void CreateChain(int linkCount)
	{
		GameObject chain = new GameObject("Chain");
		int addedLinkCount = 0;
		GameObject addedLink = gameObject;
		while(addedLinkCount < linkCount)
		{
			GameObject linkPrefab = (addedLinkCount % 2 == 0) ? linkPrefab1 : linkPrefab2;
			GameObject newLink = (GameObject)GameObject.Instantiate(linkPrefab);
			newLink.transform.parent = transform;
			newLink.name = "Link_" + addedLinkCount;
			
			Vector2 parentAnchorOffset = addedLink.GetComponent<Link>().Anchor2.localPosition;
			Vector2 childAnchorOffset =  newLink.GetComponent<Link>().Anchor1.localPosition;
			newLink.transform.position = addedLink.transform.position;
			newLink.transform.position += (Vector3)parentAnchorOffset;
			newLink.transform.position -= (Vector3)childAnchorOffset;
			
			HingeJoint2D hinge = addedLink.GetComponent<HingeJoint2D>();
			hinge.connectedBody = newLink.GetComponent<Rigidbody2D>();
			hinge.anchor = parentAnchorOffset;
			hinge.connectedAnchor = childAnchorOffset;
			hinge.enabled = true;
			
			if(addedLinkCount == linkCount-1)
			{
				GameObject ball = (GameObject)GameObject.Instantiate(ballPrefab);
				ball.name = "Ball";
				ball.transform.parent = transform;
				ball.transform.localScale = transform.localScale;
				parentAnchorOffset = newLink.GetComponent<Link>().Anchor2.localPosition;
				childAnchorOffset =  ball.GetComponent<Link>().Anchor1.localPosition;
				ball.transform.position = newLink.transform.position;
				
				hinge = newLink.GetComponent<HingeJoint2D>();
				hinge.connectedBody = ball.GetComponent<Rigidbody2D>();
				hinge.anchor = parentAnchorOffset;
				hinge.connectedAnchor = childAnchorOffset;
				hinge.enabled = true;
				
			}
			addedLink = newLink;
			addedLinkCount ++;
		}
		transform.localScale = new Vector3(0.5f, 0.5f, 1f);
	}
}
