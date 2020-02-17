using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public GameObject player;
    public Camera mainCam;

    float speed = 5f;
    Rigidbody rb;
    float senseX = 200f;
    float senseY = 100f;
    float truncatVerticalRotation = 0f;
    bool run;
    float jumpForce = 10f;
    bool isJumping;

    Animator animator;
    public float speedSmoothTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            run = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            run = false;
        }

        PlayerMovement();
    }

    public void PlayerMovement()
    {
        speed = run? 10 : 5; //Makes speed var 10 or 5 depending on if the player is running
        //Handles player moving
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Translate(x, 0, z);

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * senseX); //Player rotation left and right

        truncatVerticalRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * senseX;
        truncatVerticalRotation = Mathf.Clamp(truncatVerticalRotation, -30, 30);
        mainCam.transform.localEulerAngles = Vector3.left * truncatVerticalRotation; // Player rotation up and down

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) & IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        //animator
        if (x != 0 || z != 0)
        {   
           // Debug.Log("walking");
            animator.SetFloat("speedPercent", .5f);
        }
        else if (run)
        {
        animator.SetFloat("speedPercent", .5f);
        }
        else
        {
            animator.SetFloat("speedPercent", 0);
        }

    }
    public bool IsGrounded()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);
        if(Physics.Raycast(ray, out hit, 1+.1f))
        {
            if(hit.collider != null)
            {
                return true;
            }
        }
        return false;
    }
}
