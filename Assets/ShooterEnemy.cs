using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour, IDamagable
{
    #region [[[ VARS ]]]

    [Header("--MOVEMENT--")]
    [SerializeField] float movementSpeed;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    [SerializeField]Transform wonderAreaA, wonderAreaB;
    Transform currentPoint;
    bool isChasingPlayer;
    GameObject playerObject;

    [Header("--CHASE & ATTACK--")]
    [SerializeField] float chaseDistance;
    [SerializeField] float attackRate;
    [SerializeField] float attackDistance;
    [SerializeField] int dmg;
    float attackRateTimer;
    bool isAttacking;

    [Header("--HEALTH--")]
    [SerializeField] int health;
    [SerializeField] int maxHealth= 100;
    [SerializeField] HealthBar hbar;

    [Header("--EDGE TRIGGER--")]
    [SerializeField] Transform triggerObject;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Vector2 edgeTriggerSize;
    [SerializeField] float edgeTriggerDistance;
    bool isLedge;

    [Header("--Projectile--")]
    [SerializeField]GameObject projectilePrefab;
    [SerializeField]Transform barrelTransform;
        [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] float shootingRange;
    bool isInRange;
    public float shootDir;

    AudioSource audioSource;
    [SerializeField]AudioClip takeDmgSouund;
    [SerializeField]AudioClip DieSound;
    [SerializeField]AudioClip attackSound;
    [SerializeField]AudioClip groanSound;
    #endregion
bool canFireProjectile;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerObject = FindObjectOfType<PlayerMovementCtrl>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentPoint = wonderAreaB.transform;
        attackRateTimer = attackRate;
        

    }

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        ResetHealth();
    }

    private void Update()
    {

        #region [[[ PATROL ]]] 

        isLedge = Physics2D.Raycast(triggerObject.position, Vector2.down, edgeTriggerDistance,whatIsGround);

        if(Vector2.Distance(transform.position, playerObject.transform.position) < shootingRange)
        {
            isInRange = true;
        }
        else{
            isInRange = false;
        }

        if(!isLedge)
        {
             if(currentPoint == wonderAreaB)
            {
                isChasingPlayer = false;
                currentPoint = wonderAreaA.transform;
            }
            else
            {
                isChasingPlayer = false;
                currentPoint = wonderAreaB.transform;
            }
        }
        else{
             
        }

        Vector2 point = currentPoint.position - transform.position;

        if(!isInRange)
        {
            if(currentPoint == wonderAreaB.transform && !isChasingPlayer) 
            {
               anim.SetBool("Run", true);
               rb.velocity = new Vector2(movementSpeed * Time.deltaTime, rb.velocity.y);

               transform.rotation = Quaternion.Euler(0,0,0);
               

            }
            else if(currentPoint != wonderAreaB.transform && !isChasingPlayer)
            {
                rb.velocity = new Vector2(movementSpeed * Time.deltaTime * -1, rb.velocity.y);

                transform.rotation = Quaternion.Euler(0,180,0);
            }

             if (Vector2.Distance(transform.position, currentPoint.position) < 0.8f && currentPoint == wonderAreaB.transform && !isChasingPlayer) 
            {
                currentPoint = wonderAreaA.transform;
            }

             if (Vector2.Distance(transform.position,currentPoint.position) < 0.8f && currentPoint == wonderAreaA.transform && !isChasingPlayer) 
             {
                currentPoint = wonderAreaB.transform;
             }
            }
        else
        {
            rb.velocity = Vector2.zero;
            if(playerObject.transform.position.x > transform.position.x)
            {   
                transform.rotation = Quaternion.Euler(0,0,0);
                shootDir = 1;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0,180,0);
                shootDir = -1;
            }

             attackRateTimer -= Time.deltaTime;
            if(attackRateTimer <= 0f && isInRange)
            {
                anim.SetTrigger("Attack");
                FireProjectile(shootDir);
                attackRateTimer = attackRate;
            }
        }
       

        #endregion

           

    }


    public void FireProjectile(float shootDir)
    {
        audioSource.PlayOneShot(attackSound);

        GameObject bullet = Instantiate(projectilePrefab,barrelTransform.position,quaternion.identity);
        bullet.GetComponent<EnemyProjectile>().SetDir(shootDir);
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(wonderAreaA.transform.position, .3f);
        Gizmos.DrawWireSphere(wonderAreaB.transform.position, .3f);

    }

    public void TakeDamage(int damage)
    {
        audioSource.PlayOneShot(takeDmgSouund);

        health -= damage;
        UpdateHealthDisplay();
        if(health <= 0)
        {
            audioSource.PlayOneShot(DieSound);
            health = 0;
            anim.SetTrigger("Die");
            UpdateHealthDisplay();
        }
        anim.SetTrigger("TakeDmg");
    }
        private void UpdateHealthDisplay()
    {
        hbar.UpdateHealthBar(health,maxHealth);
    }
    public void ResetHealth()
    {
        health = maxHealth;
        UpdateHealthDisplay();
    }
    public void DestroyGameObjectAfterAnim()
    {

        Destroy(gameObject);
    }
}
