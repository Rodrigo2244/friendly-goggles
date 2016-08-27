using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class moveChar : MonoBehaviour {
	
	[Header("Inputs")]
	public string moveInput;
	public string powerInput;
	public string jumpInput;
	
	[Header("Movement Variables")]
	public bool hasControl;
	public CharacterController controller;
	public float jumpDis;
	public float powerForce;
	public float speed;
	float usablePower;
	public Vector3 movement = Vector3.zero;
	public bool beingControlled;
	public bool isNull;
	public float invert;
	public bool useGrav = true;

	[Header("Objects Affected By Power")]
	GameObject Bro;
	CharacterController broController;
    public List<boxBehaviour> boxes = new List<boxBehaviour>();

    [Header("Effects and aesthetics")]
    public AudioSource audioSource;
    public AudioClip[] sounds;
	ParticleSystem powerParticle;
	ParticleSystem deathParticle;
	ParticleSystem jumpParticle;
	Animator anim;
	GameObject powerBar;
	public float sfxVolume;
    
	void Start () {
		hasControl = true;

		if(gameObject.name == "Push"){
			Bro = GameObject.Find("Pull");
		} else if (gameObject.name == "Pull"){
			Bro = GameObject.Find("Push");
		}
		broController = Bro.GetComponent<CharacterController>();
		powerParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
		deathParticle = transform.GetChild(1).GetComponent<ParticleSystem>();
		jumpParticle = transform.GetChild(2).GetComponent<ParticleSystem>();
		controller = GetComponent<CharacterController>();
		Physics.IgnoreCollision(controller,broController);
		usablePower = 100;
		anim = GetComponent<Animator>();
		invert = 1;
		powerBar = transform.GetChild(4).gameObject;
        audioSource = GetComponent<AudioSource>();

        //Load Boxes
        if (FindObjectsOfType<boxBehaviour>().Length != 0) {
            boxBehaviour[] boxesToCheck = FindObjectsOfType<boxBehaviour>();
            foreach (boxBehaviour box in boxesToCheck) {
                if (gameObject.name == "Push" && box.red) {
                    boxes.Add(box);
                } else if (gameObject.name == "Pull" && box.blue) {
                    boxes.Add(box);
                }
            }
        }
	}

	void Update(){
        if (Time.timeScale != 0){
			ControlAnimations();

			//Movement
			if(!beingControlled){
				movement.x = Input.GetAxisRaw(moveInput)*speed;
			}

			if(controller.isGrounded){
				movement.y = -Time.deltaTime;
				if(Input.GetButtonDown(jumpInput)){
					audioSource.PlayOneShot(sounds[0], sfxVolume);

                    movement.y = jumpDis;
					jumpParticle.Emit(10);
				}
			} else {
				if(useGrav){
					movement.y  += Physics.gravity.y * Time.deltaTime;
				}
			}

			if(Input.GetAxisRaw(moveInput) != 0){
				if(Input.GetAxisRaw(moveInput) > 0){
					transform.localScale = new Vector3 (1,1,1);
					powerParticle.transform.localScale = new Vector3(1,1,1);
					deathParticle.transform.localScale = new Vector3(1,1,1);
				} else if (Input.GetAxisRaw(moveInput) < 0) {
					transform.localScale = new Vector3 (-1,1,1);
					powerParticle.transform.localScale = new Vector3(-1,-1,1);
					deathParticle.transform.localScale = new Vector3(-1,-1,1);
				}
			}

            //Power
            if (Input.GetButtonDown(powerInput) && usablePower == 100) {
                powerParticle.Emit(10);
                audioSource.PlayOneShot(sounds[1], sfxVolume);
                Vector3 move = ((Bro.transform.position - transform.position).normalized) * powerForce * invert * 5;
                Bro.GetComponent<moveChar>().movement = Vector3.zero;
                Bro.GetComponent<moveChar>().movement = move;
                Bro.GetComponent<moveChar>().beingControlled = true;
                usablePower = 0;
            } else if (usablePower < 100) {
                usablePower += Time.deltaTime * 25f;
            } else if (usablePower > 100) {
                usablePower = 100;
            }

			//Power Bar
			if(usablePower < 100){
				if(transform.localScale.x == 1){
					powerBar.transform.GetChild(1).gameObject.GetComponent<Image>().fillOrigin = 0;
				} else {
					powerBar.transform.GetChild(1).gameObject.GetComponent<Image>().fillOrigin = 1;
				}
				powerBar.SetActive(true);
				powerBar.transform.GetChild(1).gameObject.GetComponent<Image>().fillAmount = usablePower/100;
			} else {
				powerBar.SetActive(false);
			}

            if (!beingControlled) {
                controller.Move(movement * Time.deltaTime);
            } else if (beingControlled && Mathf.Abs(movement.x) > 0.5f){
                controller.Move(movement * Time.deltaTime);
                movement.x = Mathf.Lerp(movement.x,0,Time.deltaTime);
                if (Mathf.Abs(movement.x) <= 0.5f || controller.velocity.x == 0) {
                    beingControlled = false;
                }
            }
		}
	}

	void TogglePower(bool onOff){
		if(!Bro.GetComponent<moveChar>().isNull){
			Bro.GetComponent<moveChar>().beingControlled = onOff;
			if(onOff){
				Bro.GetComponent<moveChar>().movement = Vector3.zero;
			}
			Bro.transform.GetChild(5).gameObject.SetActive(onOff);
			Bro.GetComponent<ParticleRenderer>().enabled = onOff;
		}

        foreach (boxBehaviour box in boxes) {
            box.useGrav = true;
        }
    }

	void OnControllerColliderHit(ControllerColliderHit col){
		if(col.normal.y > 0 && col.normal.x == 0){
			movement.y = -Time.deltaTime;
		}
	}

	IEnumerator Death(){
		transform.GetChild(4).gameObject.SetActive(false);
		deathParticle.Emit(25);
        audioSource.PlayOneShot(sounds[2], sfxVolume);
        Bro.GetComponent<moveChar>().enabled = false;
		transform.GetComponent<moveChar>().enabled = false;
		GetComponent<Renderer>().enabled = false;
		transform.GetChild(3).gameObject.SetActive(false);
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (gameManager.Instance != null) {
            gameManager.Instance.activePlayer = gameManager.ActivePlayer.push;
        }
	}

	void ControlAnimations(){
		if(!beingControlled && useGrav){
			if(Input.GetAxisRaw(moveInput) != 0 && controller.isGrounded){
				anim.Play("run");
			}
			
			if(controller.isGrounded  && Input.GetAxisRaw(moveInput) == 0){
				anim.Play("idle");
			} else if(!controller.isGrounded) {
				if(controller.velocity.y > 0){
					anim.Play("jump");
				} else if(controller.velocity.y<0){
					anim.Play("falling");
				}
			}
		}else if(beingControlled  || !useGrav){
			anim.Play("hovering");
		}
	}

	IEnumerator SlowMo(float seconds){
		Time.timeScale = 0.25f;
		yield return new WaitForSeconds(seconds);
		Time.timeScale = 1;
	}
}