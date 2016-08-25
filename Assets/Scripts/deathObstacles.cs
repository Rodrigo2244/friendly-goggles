using UnityEngine;
using System.Collections;

public class deathObstacles : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider player){
		if(player.gameObject.layer == 9){
			player.gameObject.GetComponent<moveChar>().SendMessage("Death");
		}
	}
}
