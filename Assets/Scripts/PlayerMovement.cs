using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    CharacterController c;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpHeight = 0.8f;
    float veriticalInput;
    float horizontalInput;
    [SerializeField] float gravity;

    //for movement
    Vector3 movement = Vector3.zero;

    // For displaying collision info
    [SerializeField] Text collisionText;

    // For checking jump function
    bool canJump=true;

    //for prevent air jump
    bool isJump ;

    //For changing controls
    bool reversecontrols = false;
    void Start()
    {
        isJump = false;
        c = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        veriticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space)&& c.isGrounded)
        {
            isJump = true;
        }
        //reversing controls
        if (reversecontrols)
        {
            horizontalInput = -horizontalInput;
            veriticalInput = -veriticalInput;
        }
    }
    void FixedUpdate()
    {
        movement.x = movementSpeed * horizontalInput * Time.fixedDeltaTime;
        movement.z=movementSpeed*veriticalInput* Time.fixedDeltaTime;
        if (isJump&&canJump)
        {
            movement.y =jumpHeight;
            isJump = false;
        }
        else if(!c.isGrounded) 
        {
            movement.y -= gravity;
        }
        c.Move(movement);
        //if falls reload character
        if (transform.position.y<(GameObject.Find("Level1").transform.position.y-9f))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //remove white object if it goes infinity
        if (GameObject.Find("white") != null)
        {
            if (GameObject.Find("white").transform.position.y < (GameObject.Find("Level1").transform.position.y - 14f))
            {
                GameObject.Find("white").SetActive(false);
            }
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "black")
        {
            //if collide destroy object and display gained specification
            hit.gameObject.SetActive(false);
            canJump = false;
            if (collisionText != null)
            {
                collisionText.text = "Now can not jump!";
                collisionText.gameObject.SetActive(true);
            }
            //for getting the color of collided object
            GetComponent<Renderer>().material.color = hit.gameObject.GetComponent<Renderer>().material.color;
            //remove text after some seconds
            Invoke(nameof(disappear), 1.6f);
        }
        
        if (hit.gameObject.name == "green")
        {
            hit.gameObject.SetActive(false);
            movementSpeed = 12f;
            if (collisionText != null)
            {
                collisionText.text = "Faster!";
                collisionText.gameObject.SetActive(true);
            }
            GetComponent<Renderer>().material.color = hit.gameObject.GetComponent<Renderer>().material.color;
            Invoke(nameof(disappear), 1.6f);
        }
        

        if (hit.gameObject.name == "red")
        {
            hit.gameObject.SetActive(false);
            movementSpeed = 2f;
            if (collisionText != null)
            {
                collisionText.text = "Turtle";
                collisionText.gameObject.SetActive(true);
            }
            GetComponent<Renderer>().material.color = hit.gameObject.GetComponent<Renderer>().material.color;
            Invoke(nameof(disappear), 1.6f);
        }
        
        if (hit.gameObject.name == "blue")
        {
            hit.gameObject.SetActive(false);
            jumpHeight = 1.5f;
            if (collisionText != null)
            {
                collisionText.text = "Jump higher!";
                collisionText.gameObject.SetActive(true);
            }
            GetComponent<Renderer>().material.color = hit.gameObject.GetComponent<Renderer>().material.color;
            Invoke(nameof(disappear), 1.6f);
        }
        
        if (hit.gameObject.name == "yellow")
        {
            hit.gameObject.SetActive(false);
            reversecontrols = true;
            if (collisionText != null)
            {
                collisionText.text = "Controls reversed!";
                collisionText.gameObject.SetActive(true);
            }
            GetComponent<Renderer>().material.color = hit.gameObject.GetComponent<Renderer>().material.color;
            Invoke(nameof(disappear), 1.6f);
        }
        
        if (hit.gameObject.name == "white")
        {
            //giving movement to white ball
            var a = (hit.gameObject.transform.position - transform.position).normalized;
            a.y = 0;
            hit.gameObject.GetComponent<Rigidbody>().AddForce(a.normalized * 7, ForceMode.Impulse);
            if (collisionText != null)
            {
                collisionText.text = "Enemy can move!";
                collisionText.gameObject.SetActive(true);
            }
            GetComponent<Renderer>().material.color = hit.gameObject.GetComponent<Renderer>().material.color;
            Invoke(nameof(disappear), 1.6f);
        }
    }
    //function to remove text
    void disappear()
    {
        collisionText.gameObject.SetActive(false);
    }
}