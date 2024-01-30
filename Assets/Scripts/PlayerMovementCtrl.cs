using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerMovementCtrl : MonoBehaviour,IPlayerDamageReceiver,IJamJuice
{
    //THIS IS ACTUALY PLAYER CTRL

    #region [[[ VARS ]]]
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    [Header("--MOVEMENT--")]
    [SerializeField] float movementSpeed;
    bool isGrounded;
    [SerializeField] Transform feet;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Vector2 feetSize;
    [SerializeField] float feetAngle;

    [Header("--JUMPING--")]
    [SerializeField]bool pressedJump = false;
    [SerializeField]bool releasedJump = false;
   [SerializeField] bool startJumpTimer = false;
    [SerializeField]float jumpTimer;
    [SerializeField]bool isJumping;
    [SerializeField] bool canDoubleJump;
    [SerializeField] float jumpingVelocity;
    [SerializeField]int airJumps =1;
    [SerializeField]int jumpsLeft;
    private float gravityScale = 3f;
    [SerializeField]float jumpTime;
    [SerializeField]float startY;
    [SerializeField]float maxJumpHeight = 5.5f;



    [Header("ROLLING & DASHING")]
    [SerializeField] bool isRolling;
    [SerializeField] float dashSpeed;
    [SerializeField] LayerMask invulnerableLayer;

    [Header("--SHOOTING--")]
    [SerializeField] Gun gun;
    [SerializeField] Animator gunAnim;

    [Header("--HEALTH--")]
    [SerializeField] int health = 100;
    [SerializeField] int maxHealth= 100;
    [SerializeField] HealthBar hbar;
    [SerializeField] bool canTakeDmg = true;

    [Header("Inner-Light")]
    [SerializeField]Light2D playerLight;

    float currentExp;
    float levelExpCap;
    int level;
    [SerializeField]PlayerSoundCtrl playerSoundCtrl;


    #endregion
    private void OnEnable()
    {
        EventHandlerManager.onPlayerJumpEvent += PlayerJumpInput;
        EventHandlerManager.onPlayerJumpReleaseEvent += PlayerJumpInputReleased;
        EventHandlerManager.onPlayerDashEvent += PlayerDashInput;
        EventHandlerManager.onPlayerShootEvent += PlayerShootInput;
    }

    private void OnDisable()
    {
        EventHandlerManager.onPlayerJumpEvent -= PlayerJumpInput;
        EventHandlerManager.onPlayerJumpReleaseEvent -= PlayerJumpInputReleased;
        EventHandlerManager.onPlayerDashEvent -= PlayerDashInput;
        EventHandlerManager.onPlayerShootEvent -= PlayerShootInput;
    }

    private void Start()
    {
        playerSoundCtrl = GetComponent<PlayerSoundCtrl>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        ResetPlayer();
        jumpsLeft = airJumps;
        jumpTime = jumpTimer;
    }

    void PlayerJumpInput(bool b)
    {
        pressedJump = true;
    }

    void PlayerJumpInputReleased(bool b)
    {
        if(!canDoubleJump)
        releasedJump = true;
    }

    private void Update() 
    {

        // if(startJumpTimer)
        // {
        //     jumpTime -= Time.deltaTime;
        //     if(jumpTime <= 0){
        //         pressedJump = false;
        //         releasedJump = true;
        //     }
        // }
    }

    private void FixedUpdate()
    {

        #region [[[ PLAYER MOVEMENT CTRL ]]]
        isGrounded = Physics2D.OverlapBox(feet.position, feetSize, feetAngle, whatIsGround);

        if(isGrounded)
        {
           isJumping = false;
            canDoubleJump = true;
        }

        if (PlayerInputControl.Instance.XDir != 0)
        {

            rb.velocity = new Vector2(PlayerInputControl.Instance.XDir * movementSpeed * Time.deltaTime, rb.velocity.y);

            CheckFacingDirection();
            if(!isRolling)
            {
                anim.SetBool("Idle", false);
                anim.SetBool("Run", true);
            }
        }
        else if (PlayerInputControl.Instance.XDir == 0)
        {
            if (!isRolling)
            {
                anim.SetBool("Idle", true);
                anim.SetBool("Run", false);
            }

            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        #endregion

         if(pressedJump)
         {
            PrepareJump();
         }
         if(releasedJump || (transform.position.y - startY) > maxJumpHeight)
         {
            StopJump();
         }
    }


    #region [[ FACING DIRECTION ]]
    private void CheckFacingDirection()
    {
                switch (PlayerInputControl.Instance.MoveDir)
        {
            case Vector2 vector when vector.Equals(Vector2.right):
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            case Vector2 vector when vector.Equals(Vector2.left):
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,  -180,0));
                break;
        }
    }
#endregion

    #region [[[ JUMP CTRL ]]]

    void PrepareJump()
    {
        if(isGrounded && !isJumping)
        {
            StartJump();
        }
        else if(jumpsLeft > 0 && rb.velocity.y > 0f)
        {
            StartJump();
        
            jumpsLeft--;

            Invoke("ResetDoubleJump", 1.3f);
        }
    }

     private void StartJump()
     {
        startY = transform.position.y;

            startJumpTimer = true;
            pressedJump = false;
            rb.gravityScale = 0;
            rb.AddForce(new Vector2(0,jumpingVelocity),ForceMode2D.Impulse);
            isJumping = true;   
     }

     void StopJump()
     {
        Debug.Log("stopping jumping");
        startJumpTimer = false;
        rb.gravityScale = gravityScale;
        pressedJump = false;
        jumpTime = jumpTimer;
        releasedJump = false;

     }



    void ResetDoubleJump()
    {
            jumpsLeft = airJumps;
              isJumping = false;
    }
    #endregion

    #region [[[ DASH/ROLL CTRL ]]]
    void PlayerDashInput(bool b)
    {
        if (b)
        {
            
            if (isGrounded )
            {
                gameObject.layer = 16;
                canTakeDmg = false;
                isRolling = true;
                anim.SetBool("Idle", false);
                anim.SetBool("Run", false);
                anim.SetBool("Roll", true);
                rb.AddForce(Vector2.right * dashSpeed * PlayerInputControl.Instance.XDir);
            }

        }
    }

    public void ResetRoll()
    {
        anim.SetBool("Roll", false);
        isRolling = false;
        gameObject.layer = 14;
        Invoke("CanTakeDmgReset",.2f);
    }
    void CanTakeDmgReset(){canTakeDmg = true;}
    #endregion

    #region [[[ SHOOT CTRL ]]]
    void PlayerShootInput(bool b)
    {
        if (b)
        {
            playerSoundCtrl.PlayAttackSound();
            gunAnim.SetTrigger("Fire");
            gun.Shoot();
        }
    }

    #endregion

    #region [[[ HEALTH ]]]



    public void Heal(int _health)
    {
        health += _health;

        if(health > maxHealth)
        {
            health = maxHealth;
            UpdateHealthDisplay();
        }

        UpdateHealthDisplay();
    }

    private void UpdateHealthDisplay()
    {
        hbar.UpdateHealthBar(health,maxHealth);
    }

    public void ReceiveDamage(int dmg)
    {
        if(!canTakeDmg)
        {return;}

        playerSoundCtrl.PlayTakeDmg();
        anim.SetTrigger("TakeDmg");
        health -= dmg;
        UpdateHealthDisplay();
        if (health <= 0)
        { // DEATH

            PlayerInputControl.Instance.StopMovement();
            playerSoundCtrl.Die();
            SceneManagerCtrl.Instance.LoadLastCheckPointAndFadeOut();
            health = 0;
            UpdateHealthDisplay();
            anim.SetTrigger("Die");
            
            
        }
    }
     public void ResetPlayer()
     {
        //Reset Health
        health = maxHealth;
        UpdateHealthDisplay();
        PlayerInputControl.Instance.ResetMovement();
        //ResetInnerLight
        EventHandlerManager.CallOnPlayerReSpawnEvent();

     }
    #endregion  

    #region  [[[ INNER-LIGHT ]]]

    public void SetInnerLight(float currentInnerLight)
    {
        playerLight.intensity = Mathf.FloorToInt(currentInnerLight/10);
    }
    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(feet.transform.position, feetSize);
    }

    public void IJamJuice(int healing, float exp)
    {
        Heal(healing);

    }

    public void AddToExp(float expToAdd)
    {
        currentExp += expToAdd;


    }
}
