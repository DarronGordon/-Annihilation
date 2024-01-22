using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("--SHOOTING--")]
    [SerializeField] GameObject gun;

    [Header("--HEALTH--")]
    [SerializeField] int health = 100;

    [SerializeField] LayerMask invulnerableLayer;
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
    }
    private void Update()
    {

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

    private void CheckFacingDirection()
    {
        if (PlayerInputControl.Instance.XDir > 0)
        {
            sr.flipX = false;
        }
        else if (PlayerInputControl.Instance.XDir < 0)
        {
            sr.flipX = true;
        }
    }
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
                isRolling = true;
                anim.SetBool("Idle", false);
                anim.SetBool("Run", false);
                anim.SetBool("Roll", true);
                gameObject.layer = 16;
                rb.AddForce(Vector2.right * dashSpeed * PlayerInputControl.Instance.XDir);
            }

        }
    }

    public void ResetRoll()
    {
        anim.SetBool("Roll", false);
        isRolling = false;
        gameObject.layer = 14;
    }

    #endregion

    #region [[[ SHOOT CTRL ]]]
    void PlayerShootInput(bool b)
    {
        if (b)
        {
            gun.GetComponent<Gun>().Shoot();
        }
    }

    #endregion


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(feet.transform.position, feetSize);
    }
    #region [[[ HEALTH ]]]

    public void Heal(int _health)
    {
        health += _health;
    }

    public void ReceiveDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            health = 0;

            anim.SetTrigger("Die");
        }
    }

    #endregion  
}
