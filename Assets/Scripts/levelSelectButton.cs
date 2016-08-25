using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelSelectButton : MonoBehaviour {

	public int myLevel;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.GetInt("level") >= myLevel){
			GetComponent<Button>().interactable = true;
		} else {
			GetComponent<Button>().interactable = false;
		}
	}

	void Confirm(){
		SceneManager.LoadScene(myLevel);
	}
}
