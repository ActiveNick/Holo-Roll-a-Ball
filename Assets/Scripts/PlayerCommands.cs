using UnityEngine;
using System.Collections;

public class PlayerCommands : MonoBehaviour {

    Vector3 originalPosition;
    GameObject[] allPickups;

    // Use this for initialization
    void Start()
    {
        // Grab the original local position of the sphere when the app starts.
        originalPosition = this.transform.localPosition;
        allPickups = GameObject.FindGameObjectsWithTag("PickUp");
    }

    // Update is called once per frame
    void Update () {
	
	}

    // Called by SpeechManager when the user says the "Reset game" command
    void OnReset()
    {
        // Get the rigidbody of the player sphere and stop all movement
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);

        // Put the player sphere back into its original local position.
        this.transform.localPosition = originalPosition;

        // Reset the board by reactivating all the pickup objects
        foreach (GameObject pickup in allPickups)
        {
            pickup.SetActive(true);
        }
    }

}
