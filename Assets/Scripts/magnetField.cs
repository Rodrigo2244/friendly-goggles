using UnityEngine;
using System.Collections;

public class magnetField : MonoBehaviour {

	public float force;

	void OnTriggerEnter(Collider col){
		if(col.gameObject.layer == 9){
			moveChar characterMove = col.GetComponent<moveChar>();
			characterMove.useGrav = false;
			characterMove.movement.y = 0;
		}
	}

	void OnTriggerStay(Collider col){
		if(col.gameObject.layer == 9){
			CharacterController character = col.GetComponent<CharacterController>();
			moveChar characterMove = col.GetComponent<moveChar>();
			characterMove.movement.y = 0;
			characterMove.useGrav = false;
			character.Move ((transform.right).normalized*force*Time.deltaTime);
		}
	}

	void OnTriggerExit(Collider col){
		if(col.gameObject.layer == 9){
			moveChar characterMove = col.GetComponent<moveChar>();
			characterMove.useGrav = true;
		}
	}
}