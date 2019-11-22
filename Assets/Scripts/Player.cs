using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rigidBody;

    [SerializeField] float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        moveForward();
    }

    private void moveForward()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.velocity = (new Vector3( 0, 0, -moveSpeed));
        }
    }
}
