using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rigidBody;

    public float moveSpeed = 100f;
    public float dashForce = 100f;
    public float turnSpeed = 500000;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Target Phase

        inputHandler();

    }

    float get_angle(float x, float y)
    {
        float theta = Mathf.Atan2(x, y) - Mathf.Atan2(0, 1.0f);
        if (theta > (float)Mathf.PI)
            theta -= (float)Mathf.PI;
        if (theta < -(float)Mathf.PI)
            theta += (float)Mathf.PI;

        theta = (float)(theta * 180.0f / (float)Mathf.PI);
        return theta;
    }

    private void inputHandler()
    {
        if (Input.GetKey(KeyCode.JoystickButton0))
        {
            moveForward();
        }
        if (Mathf.Abs(Input.GetAxis("Horizontal_L")) > 0.19f||Mathf.Abs(Input.GetAxis("Vertical_L"))>0.19f)
        {
            float x = Input.GetAxis("Horizontal_L"),y= Input.GetAxis("Vertical_L");

            float angle = get_angle(x, y), currentAngle = (transform.localEulerAngles.y % 360 + 360) % 360; ;
            transform.Rotate(Vector3.up, angle-currentAngle);
            //rigidBody.AddForce(transform.forward * moveSpeed);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        

        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

    }

    private void moveForward()
    {
        rigidBody.AddForce(transform.forward * dashForce);
    }
}
