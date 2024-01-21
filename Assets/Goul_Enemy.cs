using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goul_Enemy : MonoBehaviour, IDamagable
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
    [SerializeField] bool canStartMoving = false;

    [Header("--CHASE & ATTACK--")]
    [SerializeField] float chaseDistance;
    [SerializeField] float attackRate;
    [SerializeField] float attackDistance;
    [SerializeField] int dmg;
    float attackRateTimer;
    bool isAttacking;

    [Header("--HEALTH--")]
    [SerializeField] int health;

    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentPoint = wonderAreaB.transform;
        attackRateTimer = attackRate;
    }

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("Spawn");
    }

    public void HasSpawned()
    {
        canStartMoving = true;
    }

    private void Update()
    {
        if (!canStartMoving)
        { return; }
        #region [[[ PATROL ]]] 

        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == wonderAreaB.transform && !isChasingPlayer) 
        {
            anim.SetBool("Run", true);
            rb.velocity = new Vector2(movementSpeed * Time.deltaTime, rb.velocity.y);
            sr.flipX = false;
        }
        else if(currentPoint != wonderAreaB.transform && !isChasingPlayer)
        {
            rb.velocity = new Vector2(movementSpeed * Time.deltaTime * -1, rb.velocity.y);
            sr.flipX = true;
        }
      
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.8f && currentPoint == wonderAreaB.transform && !isChasingPlayer) 
        {
            currentPoint = wonderAreaA.transform;
        }
        if (Vector2.Distance(transform.position,currentPoint.position) < 0.8f && currentPoint == wonderAreaA.transform && !isChasingPlayer) 
        {
            currentPoint = wonderAreaB.transform;
        }

        #endregion

        if (isChasingPlayer)
        {
            ChasePlayer();
        }
    }

    #region [[[ ATTACK AND CHASE PLAYER ]]]
    void ChasePlayer()
    {

        if (Vector2.Distance(transform.position, playerObject.transform.position) > chaseDistance && isChasingPlayer)
        {
            if (transform.position.x > playerObject.transform.position.x)
            {
                rb.velocity = new Vector2(movementSpeed * Time.deltaTime * -1, rb.velocity.y);
                sr.flipX = true;
            }
            else
            {
                rb.velocity = new Vector2(movementSpeed * Time.deltaTime, rb.velocity.y);
                sr.flipX = false;
            }
        }
        else if (Vector2.Distance(transform.position, playerObject.transform.position) < attackDistance)
        {
            attackRateTimer -= Time.deltaTime;

            if(attackRateTimer <= 0f)
            {
                anim.SetTrigger("Attack");
                attackRateTimer = attackRate;
                rb.velocity = Vector2.zero;
                isAttacking = true;
                
                playerObject.GetComponent<PlayerMovementCtrl>().ReceiveDamage(dmg);

            }
        }
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) 
        {
            playerObject = collision.gameObject;

            isChasingPlayer = true;

        }
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
            canStartMoving = false;
            anim.SetTrigger("Die");
            
        }
        anim.SetTrigger("TakeDmg");
    }

    public void DestroyGameObjectAfterAnim()
    {

        Destroy(gameObject);
    }
}
