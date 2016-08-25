using UnityEngine;
using System.Collections;

public class parallaxBackground : MonoBehaviour {

	public Transform cameraObj;
	public float initial;
	public float initialCam;
	public float relativeDistance;
	public float direction;

	// Use this for initialization
	void Start () {
		cameraObj = GameObject.Find("Main Camera").transform;
		initial = transform.position.x;
		initialCam = cameraObj.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		relativeDistance = (initialCam-cameraObj.position.x);
		transform.position = new Vector3(initial+relativeDistance/10*direction,transform.position.y,transform.position.z);
	}
}
