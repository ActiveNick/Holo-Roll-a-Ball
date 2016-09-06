using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    private int count;
    public Text countText;
    public Text winText;

	// Initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        ResetUI();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Play a sound when we pick-up an object
            this.gameObject.GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    // Called by SpeechManager when the user says the "Reset game" command
    void OnReset()
    {
        ResetUI();
    }

    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You win!";
        }
    }

    private void ResetUI()
    {
        count = 0;
        SetCountText();
        winText.text = "";
    }
}
