using UnityEngine;

public class boxBehaviour : MonoBehaviour {

    [Header("Box Variables")]
    public bool red;
    public bool blue;

    [Header("Movement Variables")]
    public CharacterController controller;
    public Vector3 movement = Vector3.zero;
    public bool useGrav = true;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale != 0){
            if (controller.isGrounded){
                movement.y = -0.01f;
            } else {
                if (useGrav){
                    movement.y += Physics.gravity.y * Time.deltaTime;
                }
            }
        }

        controller.Move(movement*Time.deltaTime);
    }
}