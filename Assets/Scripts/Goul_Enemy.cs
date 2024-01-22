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

    [Header("--HEALTH--")]
    [SerializeField] int health;

    [Header("--EDGE TRIGGER--")]
    [SerializeField] Transform triggerObject;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Vector2 edgeTriggerSize;
    [SerializeField] float edgeTriggerDistance;
    bool isLedge;
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
        Debug.DrawRay(triggerObject.position, Vector2.down, Color.red,edgeTriggerDistance);

        isLedge = Physics2D.Raycast(triggerObject.position, Vector2.down, edgeTriggerDistance,whatIsGround);

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
            if (transform.position.x > playerObject.transform.position.x )
            {
                rb.velocity = new Vector2(movementSpeed * Time.deltaTime * -1, rb.velocity.y);
               transform.rotation = Quaternion.Euler(0,0,0);
            }
            else
            {
                rb.velocity = new Vector2(movementSpeed * Time.deltaTime, rb.velocity.y);
                 transform.rotation = Quaternion.Euler(0,180,0);
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

        Gizmos.DrawWireCube(triggerObject.position, edgeTriggerSize);
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
