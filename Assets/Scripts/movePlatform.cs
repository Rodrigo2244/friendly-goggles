using UnityEngine;
using System.Collections;

public class movePlatform : MonoBehaviour {

	public Vector3 movement;
	public float interval;
	CharacterController controller;

	void Start(){
		controller = GetComponent<CharacterController>();
		StartCoroutine(Switch());
	}

	void Update(){
		controller.Move(movement*Time.deltaTime);
	}

	void OnTriggerStay(Collider col){
		if(col.gameObject.layer == 9 && col.GetComponent<CharacterController>().isGrounded){
			col.GetComponent<moveChar>().movement += movement+new Vector3(0,0.01f,0);
		}
	}

	void OnTriggerExit(Collider col){
		if(col.gameObject.layer == 9){
			col.GetComponent<moveChar>().movement += Vector3.zero;
		}
	}

	IEnumerator Switch(){
		yield return new WaitForSeconds(interval);
		movement = -movement;
		StartCoroutine(Switch());
	}
}