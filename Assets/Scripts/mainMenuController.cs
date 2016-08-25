using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;


public class mainMenuController : MonoBehaviour {
	GameObject play;
	GameObject quit;

	public void Play(){
		SceneManager.LoadScene("1-1");
	}

	void Quit(){
		Application.Quit();
	}

	void OnLevelWasLoaded(int level){
		GameObject.Find("Logo").GetComponent<RectTransform>().anchoredPosition = new Vector3(0,226,0);
		GameObject.Find("Logo").GetComponent<RectTransform>().sizeDelta = new Vector3(1000,400,0);
		transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
		transform.GetChild(1).gameObject.SetActive(true);
		transform.GetChild(2).gameObject.SetActive(true);
		GameObject.Find("Play").GetComponent<Button>().Select();
	}

	IEnumerator selectPlay(){
		yield return new WaitForSeconds(2.1f);
		GameObject.Find("Play").GetComponent<Button>().Select();
	}
}