using UnityEngine;

public class pearlPickup : MonoBehaviour {
    
	public AudioClip pickup;

	void OnTriggerEnter(Collider col){
		if(col.gameObject.layer == 9){
            if (gameManager.Instance != null) {
                gameManager.Instance.pearlCount++;
            }
            col.GetComponent<AudioSource>().PlayOneShot(pickup, 1);
            Destroy(gameObject);
		}
	}
}