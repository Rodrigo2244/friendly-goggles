using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class gameManager : MonoBehaviour {

    //Singleton
    public static gameManager Instance;

    [Header("Global Data")]
    public bool singlePlayer;
    public enum ActivePlayer {
        push,
        pull,
        none
    }
    public ActivePlayer activePlayer;

    [Header("Level Data")]
    public int pearlCount = 0;
    public Text pearDisplay;
    public int maxPearlCount;
    public bool victory;
    public GameObject push;
    public GameObject pull;

    [Header("SFX")]
    public AudioClip[] SFX;

    void Awake(){
        if (Instance == null){
            Instance = this;
        }else{
            Destroy(this);
        }

        if (FindObjectsOfType(GetType()).Length > 1){
            Destroy(gameObject);
        }

        singlePlayer = true;
        activePlayer = ActivePlayer.push;

        DontDestroyOnLoad(transform.root.gameObject);
    }

    void Update(){
        if (pearlCount == maxPearlCount && !victory && SceneManager.GetActiveScene().buildIndex != 0){
            pearlCount = 0;
            StartCoroutine(Victory());
        }

        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.JoystickButton6)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.JoystickButton3)) {
            toggleActivePlayer();
        }
    }

    void OnLevelWasLoaded(int level){
        if (level != 0){
            pearlCount = 0;
            victory = false;
            push = GameObject.Find("Push");
            pull = GameObject.Find("Pull");
        }
    }

    void LoadLevel(int level){
        SceneManager.LoadScene(level);
    }

    void PlayAudio(int audioIndex){
        AudioSource.PlayClipAtPoint(SFX[audioIndex],transform.position);
    }

    IEnumerator Victory(){
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCount - 1){
            SceneManager.LoadScene(0);
        }else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            activePlayer = ActivePlayer.push;
        }

        yield return null;
    }

    public void toggleSingleplayer() {
        Instance.singlePlayer = !Instance.singlePlayer;
        if (SceneManager.GetActiveScene().buildIndex != 0) {
            if (Instance.singlePlayer) {
                Instance.activePlayer = ActivePlayer.push;

                GameObject.Find("Push").GetComponent<moveChar>().moveInput = "Single Player Move";
                GameObject.Find("Push").GetComponent<moveChar>().jumpInput = "Single Player Jump";
                GameObject.Find("Push").GetComponent<moveChar>().powerInput = "none";

                GameObject.Find("Pull").GetComponent<moveChar>().moveInput = "none";
                GameObject.Find("Pull").GetComponent<moveChar>().jumpInput = "none";
                GameObject.Find("Pull").GetComponent<moveChar>().powerInput = "Single Player Power";

                GameObject.Find("2 Player Mode").GetComponent<Text>().text = "2 Player Mode: Off";
            } else {
                Instance.activePlayer = ActivePlayer.none;

                GameObject.Find("Push").GetComponent<moveChar>().moveInput = "Move Push";
                GameObject.Find("Push").GetComponent<moveChar>().jumpInput = "Jump Push";
                GameObject.Find("Push").GetComponent<moveChar>().powerInput = "Power Push";

                GameObject.Find("Pull").GetComponent<moveChar>().moveInput = "Move Pull";
                GameObject.Find("Pull").GetComponent<moveChar>().jumpInput = "Jump Pull";
                GameObject.Find("Pull").GetComponent<moveChar>().powerInput = "Power Pull";

                GameObject.Find("2 Player Mode").GetComponent<Text>().text = "2 Player Mode: On";
            }
        }
    }

    public void toggleActivePlayer() {
        if (singlePlayer && SceneManager.GetActiveScene().buildIndex != 0) {
            if (activePlayer == ActivePlayer.push) {
                activePlayer = ActivePlayer.pull;

                GameObject.Find("Push").GetComponent<moveChar>().moveInput = "none";
                GameObject.Find("Push").GetComponent<moveChar>().jumpInput = "none";
                GameObject.Find("Push").GetComponent<moveChar>().powerInput = "Single Player Power";

                GameObject.Find("Pull").GetComponent<moveChar>().moveInput = "Single Player Move";
                GameObject.Find("Pull").GetComponent<moveChar>().jumpInput = "Single Player Jump";
                GameObject.Find("Pull").GetComponent<moveChar>().powerInput = "none";
            } else if (activePlayer == ActivePlayer.pull) {
                activePlayer = ActivePlayer.push;

                GameObject.Find("Push").GetComponent<moveChar>().moveInput = "Single Player Move";
                GameObject.Find("Push").GetComponent<moveChar>().jumpInput = "Single Player Jump";
                GameObject.Find("Push").GetComponent<moveChar>().powerInput = "none";

                GameObject.Find("Pull").GetComponent<moveChar>().moveInput = "none";
                GameObject.Find("Pull").GetComponent<moveChar>().jumpInput = "none";
                GameObject.Find("Pull").GetComponent<moveChar>().powerInput = "Single Player Power";
            }
        }
    }
}