using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Player()
    {
        state = playerStates.MOVING;

    }
    Rigidbody rigidBody;
    BoxCollider playerCollider, dashCollider;
    GameObject exploseColliderObject;
    public float Health = 3f;
    public float Attack = 1f;
    public float UltCharge = 0f;
    public float Exp = 0f;

    public enum playerStates { MOVING, DASHING };
    public float moveSpeed = 10f;
    public float dashForce = 500f;
    
    public float turnSpeed = 500000f;
    public float dashTime = 0.5f;
    public float dashBaseCD = 0.2f;
    public float dashCD = 3f;
    public float UltTime = 5f;
    public float UltCost = 5f;
    public float exploseTime = 0.3f;
    public playerStates state;

    bool isDashed;
    bool isExplosed;
    bool explosionFinished;
    bool isUltra;

    float dashTimer;
    float dashBaseCDcount;
    float dashCDcount;
    float ultCount;
    float exploseTimer;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        playerCollider = GetComponent<BoxCollider>();
        dashCollider = transform.Find("Colliders").gameObject.transform.Find("DashCollider").gameObject.GetComponent<BoxCollider>();
        exploseColliderObject = transform.Find("Colliders").gameObject.transform.Find("ExploseCollider").gameObject;
        dashTimer = 0.0f;
        playerStates state = playerStates.MOVING;
        isDashed = false;
        isExplosed = false;
        isUltra = false;
        explosionFinished = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        if (dashBaseCDcount >= 0.0f)
            dashBaseCDcount -= Time.deltaTime;
        if (dashCDcount >= 0.0f)
            dashCDcount -= Time.deltaTime;
        if (ultCount >= 0.0f)
        {
            ultCount -= Time.deltaTime;
            if (ultCount <= 0)
                isUltra = false;
        }
        if (isUltra)
        {
            if (dashBaseCDcount <= 0.0f)
            {
                if (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.Space))
                {
                    if ((state == playerStates.MOVING) && (isDashed == false))
                    {
                        dashTimer = dashTime;
                        state = playerStates.DASHING;
                        isDashed = true;
                    }
                    dashCollider.enabled = true;
                }
            }
        }
        else
        {
            if (dashBaseCDcount <= 0.0f&&dashCDcount<=0)
            {
                if (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.Space))
                {
                    if ((state == playerStates.MOVING) && (isDashed == false))
                    {
                        dashTimer = dashTime;
                        state = playerStates.DASHING;
                        isDashed = true;
                    }
                    dashCollider.enabled = true;
                }
            }
        }

        if (Input.GetKey(KeyCode.JoystickButton10)&& Input.GetKey(KeyCode.JoystickButton11)&&UltCharge>=UltCost)
        {
            isUltra = true;
            UltCharge -= UltCost;
            ultCount = UltTime;
        }

        if (Input.GetKeyUp(KeyCode.JoystickButton1) || Input.GetKeyUp(KeyCode.Space))
        {
            isDashed = false;
        }

        if (Input.GetKey(KeyCode.JoystickButton7) && (isExplosed == false) && (explosionFinished == true))
        {
            startExplosion();
        }

        checkExplosionFinished();

        if (Input.GetKeyUp(KeyCode.JoystickButton7))
        {
            isExplosed = false;
        }

        switch (state)
        {
            case playerStates.MOVING:
                dashCollider.enabled = false;
                movePlayer();
                break;

            case playerStates.DASHING:
                //playerCollider.enabled = false;
                dashForward();
                break;

        }

    }

    private void checkExplosionFinished()
    {
        if (exploseTimer <= 0.0f)
        {
            exploseColliderObject.SetActive(false);
            explosionFinished = true;
        }
        exploseTimer -= Time.deltaTime;
    }

    private void startExplosion()
    {
        exploseColliderObject.SetActive(true);
        isExplosed = true;
        explosionFinished = false;
        exploseTimer = exploseTime;
    }

    private void movePlayer()
    {
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
            dashBaseCDcount = dashBaseCD;
            dashCDcount = dashCD;
            //rigidBody.AddForce(transform.forward * -dashForce);
        }
        dashTimer -= Time.deltaTime;

    }

    public bool isCollision()
    {
        if (state == playerStates.DASHING)
            return false;
        return true;
    }

    public void addUltCharge(float number)
    {
        UltCharge += number;
        Debug.Log(UltCharge);
    }

    public void addExp(float number)
    {
        Exp += number;
        Debug.Log(Exp);
    }

    public float getUltCharge()
    {
        return UltCharge;
    }

    public float getExp()
    {
        return Exp;
    }
}
