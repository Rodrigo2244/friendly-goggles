using UnityEngine;

public class pearlPickup : MonoBehaviour {
    
	public AudioClip pickup;
    bool collected = false;

	void OnTriggerEnter(Collider col){
		if(col.gameObject.layer == 9 && !collected) {
            collected = true;
            col.GetComponent<AudioSource>().PlayOneShot(pickup, 1);
            transform.SetParent(col.transform);
            transform.localPosition = new Vector3(0,0,0);
            GetComponent<Animator>().Play("Collected");
		}
	}

    void Die() {
        if (gameManager.Instance != null) {
            gameManager.Instance.pearlCount++;
        }
        Destroy(this.gameObject);
    }
}