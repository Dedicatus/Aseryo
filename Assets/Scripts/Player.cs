using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Player()
    {
        state = PlayerStates.IDLING;

    }
    Rigidbody rigidBody;
    BoxCollider playerCollider, dashCollider, ultCollider;
    CapsuleCollider exploseCollider;

    PlayerEffect playerEffect;

    public enum PlayerStates { IDLING, MOVING, DASHING };
    public PlayerStates state;

    [Header("Status")]
    public float maxHealth = 3f;
    private float curHealth;
    public float attack = 1f;

    [Header("Movement")]
    public float moveSpeed = 10f;
    public float dashForce = 500f;
    public float turnSpeed = 250f;
    public float dashTime = 0.5f;
    public float dashGap = 0.2f;
    public float dashCD = 3f;

    [Header("Skill")]
    public float ultTime = 5f;
    public float ultCost = 100f;
    public float exploseTime = 0.3f;
    public float chargePerHit = 10f;

    [Header("Debug")]
    public float exp = 0f;
    public float ultCharge = 100f;

    bool isDashed;
    bool isExplosed;
    bool explosionFinished;
    bool isUltra;
    public bool revivable;
    public int reviveTime;

    public float dashTimer;
    float dashGapCount;
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
        exploseCollider = transform.Find("Colliders").gameObject.transform.Find("ExploseCollider").gameObject.GetComponent<CapsuleCollider>();
        dashTimer = 0.0f;
        PlayerStates state = PlayerStates.IDLING;

        isDashed = false;
        isExplosed = false;
        revivable = false;
        reviveTime = 0;
        isUltra = false;
        explosionFinished = true;
        curHealth = maxHealth;
        playerEffect = transform.GetComponent<PlayerEffect>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        inputHandler();    
    }

    public bool checkUlt()
    {
        if (ultCharge >= ultCost)
            return true;
        return false;
    }

    public void resetUltCharge()
    {
        ultCharge = 0;
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
        if (dashGapCount >= 0.0f)
            dashGapCount -= Time.deltaTime;
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
            if (dashGapCount <= 0.0f)
            {
                if (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.Space))
                {
                    if (((state == PlayerStates.MOVING)|| (state == PlayerStates.IDLING)) && (isDashed == false))
                    {
                        dashTimer = dashTime;
                        state = PlayerStates.DASHING;
                        playerEffect.startDashEffect();
                    }
                    ultCollider.enabled = true;
                }
            }
        }
        else
        {
            if (dashGapCount <= 0.0f && dashCDcount <= 0)
            {
                if (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.Space))
                {
                    if (((state == PlayerStates.MOVING) || (state == PlayerStates.IDLING)) && (isDashed == false))
                    {
                        dashTimer = dashTime;
                        state = PlayerStates.DASHING;
                        isDashed = true;
                        playerEffect.startDashEffect();
                    }
                    dashCollider.enabled = true;
                }
            }
        }

        if (((Input.GetKey(KeyCode.JoystickButton10) && Input.GetKey(KeyCode.JoystickButton11)) || Input.GetKey(KeyCode.R)) && ultCharge >= ultCost)
        {
            isUltra = true;
            ultCharge -= ultCost;
            ultCount = ultTime;
            playerEffect.startUltEffect();
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
            case PlayerStates.IDLING:
                dashCollider.enabled = false;
                movePlayer();
                break;

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
            exploseCollider.enabled = false;
            explosionFinished = true;
        }
        exploseTimer -= Time.deltaTime;
    }

    private void startExplosion()
    {
        exploseCollider.enabled = true;
        isExplosed = true;
        explosionFinished = false;
        exploseTimer = exploseTime;
        playerEffect.startExplosionEffect();
    }

    private void movePlayer()
    {
        //Play Station Controller
        if (Mathf.Abs(Input.GetAxis("Horizontal_L")) > 0.19f || Mathf.Abs(Input.GetAxis("Vertical_L")) > 0.19f)
        {
            float x = Input.GetAxis("Horizontal_L"), y = Input.GetAxis("Vertical_L");

            float angle = get_angle(x, y), currentAngle = (transform.localEulerAngles.y % 360 + 360) % 360; ;
            transform.Rotate(Vector3.up, angle - currentAngle);
            //rigidBody.AddForce(transform.forward * moveSpeed);
            state = PlayerStates.MOVING;
            rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
        }
        else {
            state = PlayerStates.IDLING;

            //Keyboard
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                state = PlayerStates.MOVING;

                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
                    state = PlayerStates.MOVING;
                }

                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
                    state = PlayerStates.MOVING;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
                    state = PlayerStates.IDLING;
                }

                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
                    state = PlayerStates.IDLING;
                }
            }

        }

        
    }

    private void dashForward()
    {
        if (dashTimer >= dashTime - 0.1f)
            rigidBody.AddForce(transform.forward * dashForce);
        else rigidBody.AddForce(transform.forward * 50.0f);
        if (dashTimer <= 0.0f)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal_L")) > 0.19f || Mathf.Abs(Input.GetAxis("Vertical_L")) > 0.19f)
                state = PlayerStates.MOVING;
            else state = PlayerStates.IDLING;
            dashGapCount = dashGap;
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

    public void addUltCharge()
    {
        ultCharge += chargePerHit;
        if (ultCharge >= ultCost)
            ultCharge = ultCost;
        //Debug.Log(UltCharge);
    }

    public void addExp(float expNum)
    {
        exp += expNum;
        //Debug.Log(Exp);
    }

    public float getHealth()
    {
        return curHealth;
    }

    public float getUltCharge()
    {
        return ultCharge;
    }

    public float getExp()
    {
        return exp;
    }

    public void getAttacked(float number)
    {
        curHealth -= number;
    }
}
