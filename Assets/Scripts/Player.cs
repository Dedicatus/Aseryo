using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Player()
    {
        state = PlayerStates.MOVING;

    }
    Rigidbody rigidBody;
    BoxCollider playerCollider, dashCollider,ultCollider;
    GameObject exploseColliderObject;

    public enum PlayerStates { MOVING, DASHING };
    public PlayerStates state;

    [Header("Status")]
    public float Health = 100f;
    public float Attack = 1f;

    [Header("Movement")]
    public float moveSpeed = 10f;
    public float dashForce = 500f;
    public float turnSpeed = 250f;
    public float dashTime = 0.5f;
    public float dashBaseCD = 0.2f;
    public float dashCD = 3f;

    [Header("Skill")]
    public float UltTime = 5f;
    public float UltCost = 100f;
    public float exploseTime = 0.3f;

    [Header("Debug")]
    public float Exp = 0f;
    public float UltCharge = 100f;

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
        ultCollider = transform.Find("Colliders").gameObject.transform.Find("UltCollider").gameObject.GetComponent<BoxCollider>();
        exploseColliderObject = transform.Find("Colliders").gameObject.transform.Find("ExploseCollider").gameObject;
        dashTimer = 0.0f;
        PlayerStates state = PlayerStates.MOVING;
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

    public bool checkUlt()
    {
        if (UltCharge >= UltCost)
            return true;
        return false;
    }

    public void resetUltCharge()
    {
        UltCharge = 0;
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
            {
                ultCollider.enabled = false;
                isUltra = false;
            }
        }
        if (isUltra)
        {
            if (dashBaseCDcount <= 0.0f)
            {
                if (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.Space))
                {
                    if ((state == PlayerStates.MOVING) && (isDashed == false))
                    {
                        dashTimer = dashTime;
                        state = PlayerStates.DASHING;                 
                    }
                    ultCollider.enabled = true;
                }
            }
        }
        else
        {
            if (dashBaseCDcount <= 0.0f&&dashCDcount<=0)
            {
                if (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.Space))
                {
                    if ((state == PlayerStates.MOVING) && (isDashed == false))
                    {
                        dashTimer = dashTime;
                        state = PlayerStates.DASHING;
                        isDashed = true;
                    }
                    dashCollider.enabled = true;
                }
            }
        }

        if (Input.GetKey(KeyCode.JoystickButton10)&& Input.GetKey(KeyCode.JoystickButton11) && UltCharge >= UltCost)
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
            case PlayerStates.MOVING:
                dashCollider.enabled = false;
                movePlayer();
                break;

            case PlayerStates.DASHING:
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
            rigidBody.MovePosition(transform.position+transform.forward * moveSpeed * Time.fixedDeltaTime);
        }


        if (Input.GetKey(KeyCode.W))
            rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.S))
            rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);

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
            state = PlayerStates.MOVING;
            dashBaseCDcount = dashBaseCD;
            dashCDcount = dashCD;
            //rigidBody.AddForce(transform.forward * -dashForce);
        }
        dashTimer -= Time.deltaTime;

    }

    public bool isCollision()
    {
        if (state == PlayerStates.DASHING)
            return false;
        return true;
    }

    public void addUltCharge(float number)
    {
        UltCharge += number;
        if (UltCharge >= UltCost)
            UltCharge = UltCost;
        //Debug.Log(UltCharge);
    }

    public void addExp(float number)
    {
        Exp += number;
        //Debug.Log(Exp);
    }

    public float getHealth()
    {
        return Health;
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
