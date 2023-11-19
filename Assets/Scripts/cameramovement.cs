using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramovement : MonoBehaviour
{
    public Transform target; // Reference to the player's Transform

    [SerializeField] Vector3 offset = new Vector3(0f, 3.92f, -6.1f); // Adjust this to set the camera's offset from the player

    void Update()
    {
        if (target != null)
        {
            // Update the camera's position based on the player's position
            transform.position = target.position + offset;

            // Optionally, you can make the camera look at the player
            transform.LookAt(target);
        }
    }
}
