using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rigidBody;

    public float moveSpeed = 10f;
    public float dashSpeed = 100f;
    public float turnSpeed = 500000;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Target Phase

        inputHandler();

    }

    private void inputHandler()
    {
        if (Input.GetKey(KeyCode.JoystickButton0))
        {
            moveForward();
        }
        if (Input.GetAxis("Horizontal_L") > 0.19f)
        {
            float angle = (transform.localEulerAngles.y % 360 + 360) % 360;
            if (Mathf.Abs(angle - 90) > 3)
            {
                if (angle >= 90 && angle < 270)
                    transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
                else transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
            }
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetAxis("Horizontal_L") < -0.19f)
        {
            float angle = (transform.localEulerAngles.y % 360 + 360) % 360;
            if (Mathf.Abs(angle - 270) > 3)
            {
                if (angle >= 90 && angle < 270)
                    transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
                else transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
            }
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetAxis("Vertical_L") > 0.19f)
        {
            float angle = transform.localEulerAngles.y%360;
            if (Mathf.Abs(angle) > 3)
            {
                if (angle >= 0 && angle < 180)
                    transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
                else transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
            }
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetAxis("Vertical_L") <-0.19f)
        {
            float angle = transform.localEulerAngles.y%360;
            if (Mathf.Abs(angle-180) > 3)
            {
                if (angle >= 0 && angle < 180)
                    transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
                else transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
            }
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
        transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);
    }
}
