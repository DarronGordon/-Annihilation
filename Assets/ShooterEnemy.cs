using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
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

    #endregion
bool canFireProjectile;
    private void Start()
    {
        playerObject = FindObjectOfType<PlayerMovementCtrl>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentPoint = wonderAreaB.transform;
        attackRateTimer = attackRate;

    }

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        #region [[[ PATROL ]]] 

        isLedge = Physics2D.Raycast(triggerObject.position, Vector2.down, edgeTriggerDistance,whatIsGround);

        if(Vector2.Distance(transform.position, playerObject.transform.position) < shootingRange)
        {
            isInRange = true;

            Debug.Log("is in shooting range");
        }
        else{
            Debug.Log("is not in shooting range");
            isInRange = false;
        }

        if(!isLedge)
        {
            Debug.Log("Am At Ledge");
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
             

            Debug.Log("I Am not At Ledge");
        }

        Vector2 point = currentPoint.position - transform.position;

        if(!isInRange)
        {Debug.Log(currentPoint);
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

        health -= damage;

        if(health <= 0)
        {
            health = 0;
            anim.SetTrigger("Die");
            
        }
        anim.SetTrigger("TakeDmg");
    }

    public void DestroyGameObjectAfterAnim()
    {

        Destroy(gameObject);
    }
}
