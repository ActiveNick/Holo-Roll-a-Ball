using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rb;

	// Initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // FixedUpdate is called just before performing any physics calculations
    void FixedUpdate ()
    {
        // This code lets us use either keyboard arrow or WASD keys, as well
        // as a controller (left thumbstick) which is ideal for VR/MR
        float moveHortizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHortizontal, 0, moveVertical);

        rb.AddForce(movement * speed);
    }
}
