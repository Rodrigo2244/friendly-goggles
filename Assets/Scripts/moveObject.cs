using UnityEngine;
using System.Collections;

public class moveObject : MonoBehaviour {

	public Vector3 movement;
	public float interval;
	CharacterController controller;
	public bool ignorePlayers;

	void Start(){
		controller = GetComponent<CharacterController>();
		StartCoroutine(Switch());
		if(ignorePlayers){
			Physics.IgnoreCollision(controller,GameObject.Find("Pull").GetComponent<CharacterController>());
			Physics.IgnoreCollision(controller,GameObject.Find("Push").GetComponent<CharacterController>());
		}
	}

	void Update(){
		controller.Move(movement*Time.deltaTime);
	}

	IEnumerator Switch(){
		yield return new WaitForSeconds(interval);
		movement = -movement;
		StartCoroutine(Switch());
	}
}