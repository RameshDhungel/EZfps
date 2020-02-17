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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    public void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Translate(x, 0, z);

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * senseX);

        truncatVerticalRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * senseX;
        truncatVerticalRotation = Mathf.Clamp(truncatVerticalRotation, -30, 30);
        mainCam.transform.localEulerAngles = Vector3.left * truncatVerticalRotation;


    }
}
