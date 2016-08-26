using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class gameController : MonoBehaviour {

	public int pearlCount=0;
	public Text pearDisplay;
	public int maxPearlCount;
	public bool victory;

	public GameObject push;
	public GameObject pull;

	// Use this for initialization
	void Start () {
		pearDisplay = transform.GetChild(0).GetChild(0).GetComponent<Text>();
		maxPearlCount = GameObject.FindGameObjectsWithTag("Pearl").Length;

		push = GameObject.Find("Push");
		pull = GameObject.Find("Pull");
	}
	
	// Update is called once per frame
	void Update () {
		if(pearlCount==maxPearlCount && !victory){
			victory = true;
			StartCoroutine(Victory());
		}

		if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.JoystickButton6)){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	IEnumerator Victory(){
		if(SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCount- 1){
            SceneManager.LoadScene(0);
		} else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}

		yield return null;
	}
}