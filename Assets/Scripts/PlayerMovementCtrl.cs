using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovementCtrl : MonoBehaviour,IPlayerDamageReceiver
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

    [Header("JUMPING")]
    [SerializeField] bool canDoubleJump;
    [SerializeField] float jumpingVelocity;
    bool isJumping;

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
    #endregion

    private void OnEnable()
    {
        EventHandlerManager.onPlayerJumpEvent += PlayerJumpInput;
        EventHandlerManager.onPlayerDashEvent += PlayerDashInput;
        EventHandlerManager.onPlayerShootEvent += PlayerShootInput;
    }

    private void OnDisable()
    {
        EventHandlerManager.onPlayerJumpEvent -= PlayerJumpInput;
        EventHandlerManager.onPlayerDashEvent -= PlayerDashInput;
        EventHandlerManager.onPlayerShootEvent -= PlayerShootInput;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        health = maxHealth;
        UpdateHealthDisplay();
    }

    private void FixedUpdate()
    {

        #region [[[ PLAYER MOVEMENT CTRL ]]]
        isGrounded = Physics2D.OverlapBox(feet.position, feetSize, feetAngle, whatIsGround);

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
    }
    #region [[ FACING DIRECTION ]]
    private void CheckFacingDirection()
    {

    }
#endregion

    #region [[[ JUMP CTRL ]]]

    void PlayerJumpInput(bool b)
    {
        if (b)
        {
            if (isGrounded)
            {
                rb.AddForce(Vector2.up * jumpingVelocity);
                isJumping = true;
            }

            if (!isGrounded && canDoubleJump && isJumping)
            {
                canDoubleJump = false;
                rb.AddForce(Vector2.up * jumpingVelocity);

                Invoke("ResetDoubleJump", 1.3f);
            }
        }
    }

    void ResetDoubleJump()
    {
        canDoubleJump = true;
    }
    #endregion

    #region [[[ DASH/ROLL CTRL ]]]
    void PlayerDashInput(bool b)
    {
        if (b)
        {
            if (isGrounded)
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
            gunAnim.SetTrigger("Fire");
            gun.Shoot();
        }
    }

    #endregion

    #region [[[ HEALTH ]]]



    public void Heal(int _health)
    {
        health += _health;

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
        anim.SetTrigger("TakeDmg");
        health -= dmg;
        UpdateHealthDisplay();
        if (health <= 0)
        {
            health = 0;
            UpdateHealthDisplay();
            anim.SetTrigger("Die");
        }
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

}
