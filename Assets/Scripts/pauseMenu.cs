using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour {

	public GUISkin skin;
	public Texture blackFilm;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Pause")){
			Pause ();
		}
	}

	void Pause(){
		if(Time.timeScale == 1){
			Cursor.visible = true;
			Time.timeScale = 0;
			transform.GetChild(0).gameObject.SetActive(true);
		} else {
			transform.GetChild(0).gameObject.SetActive(false);
			Cursor.visible = false;
			Time.timeScale = 1;
		}
	}

	void MainMenu(){
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}
}