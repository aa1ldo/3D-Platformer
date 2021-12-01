using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float maxSpeed = 7f;
    float rotation = 0.0f;
    float camRotation = 0.0f;
    public float rotationSpeed = 2.0f;
    public float camRotationSpeed = 1.5f;
    GameObject cam;

    public Rigidbody myRigidbody;

    public bool isOnGround;
    public GameObject groundChecker;
    public LayerMask groundLayer;
    float jumpForce = 0.0f;
    public float jumpMultiplier = 1500f;

    float charger = 0.0f;
    public bool discharge = false;

    Animator myAnim;

    Rigidbody lillypad;

    private void Start()
    {
        myAnim = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        cam = GameObject.Find("Main Camera");
        myRigidbody = GetComponent<Rigidbody>();
        lillypad = GameObject.Find("lillypad").GetComponent<Rigidbody>();
    }
    void Update()
    {
        isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.3f, groundLayer);
        myAnim.SetBool("isOnGround", isOnGround);


        if (Input.GetKey(KeyCode.Space))
        {
            maxSpeed = 1f;
            charger += Time.deltaTime;
            myAnim.SetBool("isCharging", true);
            Debug.Log(charger);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            discharge = true;
            maxSpeed = 7f;
            myAnim.SetBool("isCharging", false);
            myAnim.SetBool("isRelease", true);
        }

        if (isOnGround == true && discharge == true)
        {
            jumpForce = jumpMultiplier * charger;

            if (jumpForce < 1500f)
            {
                jumpForce = 1500f;
            } else if (jumpForce > 6000f)
            {
                jumpForce = 6000f;
            }

            Debug.Log(jumpForce);

            myRigidbody.AddForce(transform.up * jumpForce);

            discharge = false;
            myAnim.SetBool("isRelease", false);
            charger = 0f;
        }

        Vector3 newVelocity = (transform.forward * Input.GetAxis("Vertical") * maxSpeed) + (transform.right * Input.GetAxis("Horizontal") * maxSpeed);

        myAnim.SetFloat("speed", newVelocity.magnitude);

        myRigidbody.velocity = new Vector3(newVelocity.x, myRigidbody.velocity.y, newVelocity.z);

        rotation = rotation + Input.GetAxis("Mouse X") * rotationSpeed;
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));

        camRotation = camRotation + Input.GetAxis("Mouse Y") * camRotationSpeed;

        camRotation = Mathf.Clamp(camRotation, -25.0f, -25.0f);
        cam.transform.localRotation = Quaternion.Euler(new Vector3(-camRotation, 0.0f, 0.0f));
    }

    //private void FixedUpdate()
    //{
    //    if (discharge)
    //    {
    //        jumpForce = 100 * charger;
    //        myRigidbody.AddForce(transform.up * jumpForce);

    //        discharge = false;
    //        charger = 0f;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "lillypad")
        {
            myRigidbody.velocity = Vector3.zero;
            myRigidbody.AddForce(transform.up * 20000f);
        }
    }
}