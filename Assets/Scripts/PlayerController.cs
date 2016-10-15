using UnityEngine;
using UnityEngine.UI;
using System.Collections;
// Required for the Hololens Controller Input plugin for Hololens
// I'm using this temprorarily until Unity the Unity input manager 
// supports Bluetooth controllers on HoloLens like the Xbox One S Controller
using HoloLensXboxController;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;

    // Temporarily used by the HoloLensXboxController plugin
    private ControllerInput controllerInput;

    // Initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        ResetUI();

        // HoloLensXboxController plugin initialization
        // First parameter is the number, starting at zero, of the controller you want to follow.
        // Second parameter is the default “dead” value; meaning all stick readings less than this value will be set to 0.0.
        controllerInput = new ControllerInput(0, 0.19f);
    }

    // Update is called once per frame
    void Update () {
	
	}

    // FixedUpdate is called just before performing any physics calculations
    void FixedUpdate ()
    {
        float moveHorizontal;
        float moveVertical;

#if (UNITY_EDITOR)
        {
            // This code lets us use either keyboard arrow or WASD keys, as well
            // as a controller (left thumbstick) which is ideal for VR/MR
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }
#else 
        {
            controllerInput.Update();

            moveHorizontal = controllerInput.GetAxisLeftThumbstickX();
            moveVertical = controllerInput.GetAxisLeftThumbstickY();
        }
#endif       

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

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
