﻿using System.Collections;
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
    XInputDotNetPure.PlayerIndex PlayerIndex = XInputDotNetPure.PlayerIndex.One;
    public enum PlayerStates { IDLING, MOVING, DASHING };
    public enum UltType {NONE, FIRE, ICE, WIND };
    public PlayerStates state;
    public UltType Utype;
    public GameObject windCollider;

    [Header("Status")]
    public bool MJMode = false;
    public float maxHealth = 3f;
    public float curHealth;
    public float attack = 1f;
    public int vibrationBaseNumber = 3;
    public float avoidChance = 1f;
    public float killCount = 0f;

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
    public float WindDurnTime = 0.5f;

    [Header("Debug")]
    public float exp = 0f;
    public float ultCharge = 100f;
    public float fireAmount = 0;
    public float iceAmount = 0;
    public float windAmount = 0;

    bool isExplosed;
    bool explosionFinished;
    public bool canExploseThisDash;
    public bool isUltra;
    public int reviveTimes;
    public bool chargeRecover;
    public float vibrationTime = 0.5f;
    public bool isAddMaxHealth;
    public bool isExploseOpen;
    public float dashTimer;
    
    float dashGapCount;
    public float dashCDcount;
    float ultCount;
    float exploseTimer;
    float vibrationCount;
    public int singleDashCount;
    bool isVibrated;
    bool isWindOn;
    bool isRefreshDash;
    public bool isDashHeal;
    public bool isDashCharge;
    bool isMoreChargeCollection;
    float moreChargeNumber;
    float DashHealNumber;
    float DashChargeNumber;

    

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
        state = PlayerStates.IDLING;
        Utype = UltType.NONE;
        isExplosed = false;
        isExploseOpen = false;
        reviveTimes = 0;
        singleDashCount = 0;
        isUltra = false;
        isVibrated = false;
        isAddMaxHealth = false;
        isWindOn = false;
        explosionFinished = true;
        isRefreshDash = false;
        canExploseThisDash = false;
        isDashHeal = false;
        isDashCharge = false;
        isMoreChargeCollection = false;
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
        ultCharge -= ultCost;
    }

    public void addSingleKill()
    {
        singleDashCount++;
    }

    public void addKillCount()
    {
        killCount++;
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

    private void ControllerVibration()
    {
        if (isVibrated == false && singleDashCount >= vibrationBaseNumber)
        {
            isRefreshDash = true;
            canExploseThisDash = true;
            vibrationCount = vibrationTime;
            isVibrated = true;
        }
        vibrationCount -= Time.deltaTime;
        if(vibrationCount>0)
            XInputDotNetPure.GamePad.SetVibration(PlayerIndex, 0f, 0.8f);
        else XInputDotNetPure.GamePad.SetVibration(PlayerIndex, 0f, 0f);
    }

    void checkUltType()
    {
        if (fireAmount >= iceAmount && fireAmount >= windAmount)
            Utype = UltType.FIRE;
        if (iceAmount >= fireAmount && iceAmount >= windAmount)
            Utype = UltType.ICE;
        if (windAmount >= iceAmount && windAmount >= fireAmount)
        {
            Utype = UltType.WIND;
            
        }
    }

    void changeUltType(UltType UT)
    {
        if (UT == UltType.FIRE)
            fireAmount = 0;
        if (UT == UltType.ICE)
            iceAmount = 0;
        if (UT == UltType.WIND)
            windAmount = 0;
    }

    public void changeMaxHealth()
    { 
        isAddMaxHealth = true;
        maxHealth = 200f;
        curHealth = 200f;
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
                changeUltType(Utype);
                ultCollider.enabled = false;
                isUltra = false;
            }
        }

        if (isUltra)
        {
            if (dashGapCount <= 0.0f)
            {
                if (Input.GetKey(KeyCode.JoystickButton0) || Input.GetKey(KeyCode.Space))
                {
                    
                    if ((state == PlayerStates.MOVING)|| (state == PlayerStates.IDLING))
                    {
                        dashTimer = dashTime;
                        isVibrated = false;
                        singleDashCount = 0;
                        state = PlayerStates.DASHING;
                        playerEffect.startDashEffect();
                        GameObject.Find("AudioManager").GetComponent<AudioManager>().playDashSound();
                    }
                    ultCollider.enabled = true;
                }
            }
        }
        else
        {
            if (dashGapCount <= 0.0f && dashCDcount <= 0)
            {
                if (Input.GetKey(KeyCode.JoystickButton0) || Input.GetKey(KeyCode.Space))
                {
                    
                    if ((state == PlayerStates.MOVING) || (state == PlayerStates.IDLING))
                    {
                        dashTimer = dashTime;
                        isVibrated = false;
                        singleDashCount = 0;
                        state = PlayerStates.DASHING;
                        playerEffect.startDashEffect();
                        GameObject.Find("AudioManager").GetComponent<AudioManager>().playDashSound();
                    }
                    dashCollider.enabled = true;
                }
            }
        }

        if (((Input.GetKey(KeyCode.JoystickButton8) && Input.GetKey(KeyCode.JoystickButton9)) || Input.GetKey(KeyCode.R)) && ultCharge >= ultCost&&!isUltra)
        {
            checkUltType();
            isUltra = true;
            ultCharge -= ultCost;
            ultCount = ultTime;
            playerEffect.startUltEffect();
            GameObject.Find("AudioManager").GetComponent<AudioManager>().playUltSound();
        }

        if(Input.GetKey(KeyCode.M))
        {
            MJMode = true;
        }

        if (Input.GetKey(KeyCode.J))
        {
            MJMode = false;
        }



        if (canExploseThisDash && isExploseOpen && Input.GetAxis("LRT") > 0.19f && (isExplosed == false) && (explosionFinished == true))
        {
            singleDashCount = 0;
            isVibrated = false;
            canExploseThisDash = false;
            startExplosion();
        }

        checkExplosionFinished();

        if (Input.GetAxis("LRT") <= 0.19f && Input.GetAxis("LRT") >=0f)
        {
            isExplosed = false;
        }
        
        ControllerVibration();
        switch (state)
        {
            case PlayerStates.IDLING:
                dashCollider.enabled = false;
                ultCollider.enabled = false;
                movePlayer();
                break;

            case PlayerStates.MOVING:
                dashCollider.enabled = false;
                ultCollider.enabled = false;
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
        GameObject.Find("AudioManager").GetComponent<AudioManager>().playExploseSound();
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
            if (!isRefreshDash)
                dashCDcount = dashCD;
            else dashCDcount = 0;
            isRefreshDash = false;
            singleDashCount = 0;
            isVibrated = false;
            if (Utype == UltType.WIND)
            {
                GameObject temp = Instantiate(windCollider, transform.position , transform.rotation * Quaternion.Euler(0, 0, 0), GameObject.Find("Environment").transform.Find("Effect"));
                temp.GetComponent<BoxCollider>().center = new Vector3(0f, 0f, 0.6f);
                Destroy(temp,WindDurnTime);
            }
        }
        dashTimer -= Time.deltaTime;

    }

    public bool isCollision()
    {
        if (state == PlayerStates.DASHING)
            return false;
        return true;
    }

    public void openMoreChargeCollect(float num)
    {
        isMoreChargeCollection = true;
        moreChargeNumber = num;
    }

    public void addUltCharge(float amount)
    {
        ultCharge += amount;
        if (ultCharge >= ultCost*3f)
            ultCharge = ultCost*3f;
        //Debug.Log(UltCharge);
    }
    public void addExtraCharge()
    { 
        if (isMoreChargeCollection)
            ultCharge += moreChargeNumber;
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

    public void addHealth(float number)
    {
        curHealth += number;
        if (curHealth >= maxHealth)
            curHealth = maxHealth;
    }

    public void openDashHeal(float number)
    {
        isDashHeal = true;
        DashHealNumber = number;
    }

    public void DashHeal()
    {
        curHealth += DashHealNumber;
        if (curHealth >= maxHealth)
            curHealth = maxHealth;
    }

    public void openDashCharge(float number)
    {
        isDashCharge = true;
        DashChargeNumber = number;
    }

    public void DashCharge()
    {
        ultCharge += DashChargeNumber;
        if (ultCharge >= ultCost * 3f)
            ultCharge = ultCost * 3f;
    }

    public void getAttacked(float number)
    {
        float tempNum = Random.Range(0f, 1f);
        if(tempNum<=avoidChance)
            curHealth -= number;
        if (MJMode && curHealth < 41)
            curHealth = 41f;
        if (curHealth <= 0)
        {
            if (reviveTimes > 0)
            {
                curHealth = maxHealth / 2;
                reviveTimes--;
                return;
            }
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().gameOver();
            //transform.GetComponent<FaintEffect>().startFaintEffect();
            transform.GetComponent<FaintEffect>().startGroundBlood();
        }
    }
}
