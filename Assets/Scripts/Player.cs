using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rigidBody;

    public float moveSpeed = 10f;
    public float dashSpeed = 100f;
    public float turnSpeed = 75f;


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
        if (Input.GetKey(KeyCode.Space))
        {
            moveForward();
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
