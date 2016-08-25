using UnityEngine;

public class cameraFollow : MonoBehaviour {

	GameObject Push;
	GameObject Pull;
	public float targetSize;
	public bool follow = true;
	LineRenderer lr;
	public float yClamp;
	public Vector2 camOffset = Vector2.zero;
	bool showLine;

	// Use this for initialization
	void Start () {
		Push = GameObject.Find("Push");
		Pull = GameObject.Find("Pull");
		lr = GetComponent<LineRenderer>();
		if(follow){
			transform.position = new Vector3((Push.transform.position.x+Pull.transform.position.x)/2 + camOffset.x,(Push.transform.position.y+Pull.transform.position.y)/2 + camOffset.y,-10);
		}
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(follow){
            if (gameManager.Instance != null && gameManager.Instance.singlePlayer) {
                if (gameManager.Instance.activePlayer == gameManager.ActivePlayer.push) {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(Push.transform.position.x + camOffset.x, Push.transform.position.y + camOffset.y, -10), Time.deltaTime);
                } else if (gameManager.Instance.activePlayer == gameManager.ActivePlayer.pull) {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(Pull.transform.position.x + camOffset.x, Pull.transform.position.y + camOffset.y, -10), Time.deltaTime);
                }
            } else {
                transform.position = Vector3.Lerp(transform.position, new Vector3((Push.transform.position.x + Pull.transform.position.x) / 2 + camOffset.x, (Push.transform.position.y + Pull.transform.position.y) / 2 + camOffset.y, -10), Time.deltaTime);
            }
		}
		if(showLine){
			lr.SetPosition(0,Push.transform.position);
			lr.SetPosition(1,Pull.transform.position+(Pull.transform.position-Push.transform.position).normalized);
		}

		if(Input.GetKeyDown(KeyCode.JoystickButton2)||Input.GetKeyDown(KeyCode.Space)){
			ToggleLine(true);
		}

		if(Input.GetKeyUp(KeyCode.JoystickButton2)||Input.GetKeyUp(KeyCode.Space)){
			ToggleLine(false);
		}

		transform.position = new Vector3(transform.position.x,Mathf.Clamp(transform.position.y,yClamp,100),-10);
	}

	void ToggleLine(bool On){
		GetComponent<LineRenderer>().enabled = On;
		showLine = On;
	}
}