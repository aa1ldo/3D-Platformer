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
    Rigidbody myRigidbody;

    bool isOnGround;
    public GameObject groundChecker;
    public LayerMask groundLayer;
    float jumpForce = 0.0f;

    float charger = 0.0f;
    bool discharge = false;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = GameObject.Find("Main Camera");
        myRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.1f, groundLayer);


        //if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        //{
        //    timer += Time.deltaTime;
        //}

        //if (isOnGround == true && Input.GetKeyUp(KeyCode.Space))
        //{
        //    myRigidbody.AddForce(transform.up * jumpForce);
        //    Debug.Log(timer);
        //}

        if (Input.GetKey(KeyCode.Space))
        {
            maxSpeed = 0f;
            charger += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            discharge = true;
            maxSpeed = 7f;
        }

        if (isOnGround == true && discharge == true)
        {
            jumpForce = 2000 * charger;

            Debug.Log(jumpForce);

            if (jumpForce < 1500f)
            {
                jumpForce = 1500f;
            } else if (jumpForce > 4000f)
            {
                jumpForce = 4000f;
            }

            myRigidbody.AddForce(transform.up * jumpForce);

            discharge = false;
            charger = 0f;
        }

        Vector3 newVelocity = (transform.forward * Input.GetAxis("Vertical") * maxSpeed) + (transform.right * Input.GetAxis("Horizontal") * maxSpeed);
        myRigidbody.velocity = new Vector3(newVelocity.x, myRigidbody.velocity.y, newVelocity.z);

        rotation = rotation + Input.GetAxis("Mouse X") * rotationSpeed;
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));

        camRotation = camRotation + Input.GetAxis("Mouse Y") * camRotationSpeed;

        camRotation = Mathf.Clamp(camRotation, -40.0f, -40.0f);
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
}