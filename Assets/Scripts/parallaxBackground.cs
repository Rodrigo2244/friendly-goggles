using UnityEngine;
using System.Collections;

public class parallaxBackground : MonoBehaviour {

	public Transform cameraObj;
	float initial;
	float initialCam;
	float relativeDistance;
	public float direction;
    public float speed;

	// Use this for initialization
	void Start () {
		cameraObj = GameObject.Find("Main Camera").transform;
		initial = transform.position.x;
		initialCam = cameraObj.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		relativeDistance = (initialCam-cameraObj.position.x);
		transform.position = new Vector3(initial+relativeDistance/10*direction * speed,transform.position.y,transform.position.z);
	}
}
