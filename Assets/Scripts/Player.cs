using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rigidBody;

    public enum playerStates { MOVING, DASHING };
    public float moveSpeed = 10f;
    public float dashForce = 500f;

    public float dashSpeed = 100f;
    public float turnSpeed = 500000;
    public float dashTime = 0.5f;
    public float dashCD = 0.1f;
    public playerStates state;

    float dashTimer;
    float dashCDcount;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        dashTimer = 0.0f;
        playerStates state = playerStates.MOVING;
    }

    // Update is called once per frame
    void Update()
    {
        // Target Phase
        if (state == playerStates.MOVING)
            inputHandler();
        if (state == playerStates.DASHING)
            dashForward();
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
        if (dashCDcount >= 0.0f)
            dashCDcount -= Time.deltaTime;

        if (dashCDcount <= 0.0f)
            if (Input.GetKey(KeyCode.JoystickButton0))
            {
                dashTimer = dashTime;
                state = playerStates.DASHING;
            }
        if (Mathf.Abs(Input.GetAxis("Horizontal_L")) > 0.19f || Mathf.Abs(Input.GetAxis("Vertical_L")) > 0.19f)
        {
            float x = Input.GetAxis("Horizontal_L"), y = Input.GetAxis("Vertical_L");

            float angle = get_angle(x, y), currentAngle = (transform.localEulerAngles.y % 360 + 360) % 360; ;
            transform.Rotate(Vector3.up, angle - currentAngle);
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

    private void dashForward()
    {

        if (dashTimer >= dashTime - 0.1f)
            rigidBody.AddForce(transform.forward * dashForce);
        else rigidBody.AddForce(transform.forward * 50.0f);
        if (dashTimer <= 0.0f)
        {
            state = playerStates.MOVING;
            dashCDcount = dashCD;
            //rigidBody.AddForce(transform.forward * -dashForce);
        }
        dashTimer -= Time.deltaTime;

    }
}
