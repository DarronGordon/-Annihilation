using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : MonoBehaviour, IDamagable
{
  #region [[[ VARS ]]]

    [Header("--MOVEMENT--")]
    [SerializeField] float movementSpeed;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    [SerializeField]Transform wonderAreaA, wonderAreaB;
    Transform currentPoint;
    [SerializeField]bool isChasingPlayer;
    GameObject playerObject;
    [SerializeField] bool canStartMoving = false;

    [Header("--CHASE & ATTACK--")]
    [SerializeField] float chaseDistance;
    [SerializeField] GameObject attackPos;
    [SerializeField] float attackRate;
    [SerializeField] float attackDistance;
    [SerializeField] int dmg;
    [SerializeField] LayerMask whatIsPlayer;
    float attackRateTimer;
    Collider2D[] chaseColliders;

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
    bool isWithinAttackRange= false;
    bool isTouchingWall = false;
    int dir;
    #endregion

    float takeDmgTimer=1;
    float takeDmgCounter;
    [SerializeField]bool canTakeDmg;

    AudioSource audioSource;
    [SerializeField]AudioClip takeDmgSouund;
    [SerializeField]AudioClip DieSound;
    [SerializeField]AudioClip attackSound;
    [SerializeField]AudioClip groanSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        currentPoint = wonderAreaB.transform;
        attackRateTimer = attackRate;
        
    }

    private void OnEnable()
    {
        anim = GetComponentInChildren<Animator>();
        ResetHealth();
    }

    private void Update()
    {
        takeDmgCounter -= Time.deltaTime;
        if(takeDmgCounter <=0)
        {
            canTakeDmg = true;
        }

        #region [[[ PATROL ]]] 
        isLedge = Physics2D.Raycast(triggerObject.position, Vector2.down, edgeTriggerDistance, whatIsGround);
        Debug.DrawRay(triggerObject.position, Vector2.down * edgeTriggerDistance, Color.blue, edgeTriggerDistance);

        chaseColliders = Physics2D.OverlapBoxAll(triggerObject.position, edgeTriggerSize, 0f, whatIsPlayer);

        isChasingPlayer = IsChasingPlayersCheck();

        isWithinAttackRange = Physics2D.Raycast(attackPos.transform.position, Vector2.right * dir, attackDistance, whatIsPlayer);
        isTouchingWall = Physics2D.Raycast(transform.position, Vector2.right * dir, attackDistance, whatIsGround);
        Debug.DrawRay(attackPos.transform.position, Vector2.right * dir * attackDistance, Color.blue, .5f);

        if (!isLedge)
        {
             Debug.Log("I Am At Ledge");

            if (currentPoint == wonderAreaB)
            {
                currentPoint = wonderAreaA.transform;
                            isChasingPlayer = false;
            }
            else
            {
                currentPoint = wonderAreaB.transform;
                            isChasingPlayer = false;
            }
        }
        else
        {
            Debug.Log("I Am not At Ledge");
        }

        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == wonderAreaB.transform && !isChasingPlayer || currentPoint == wonderAreaB.transform && isTouchingWall)
        {
            anim.SetBool("Run", true);
            dir =1;
            rb.velocity = new Vector2(movementSpeed * Time.deltaTime, rb.velocity.y);

            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else if (currentPoint != wonderAreaB.transform && !isChasingPlayer || currentPoint != wonderAreaB.transform && isTouchingWall)
        {
            dir = -1;
            rb.velocity = new Vector2(movementSpeed * Time.deltaTime * -1, rb.velocity.y);

            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.8f && currentPoint == wonderAreaB.transform && !isChasingPlayer || currentPoint == wonderAreaB.transform && isTouchingWall)
        {
            dir =1;
            currentPoint = wonderAreaA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.8f && currentPoint == wonderAreaA.transform && !isChasingPlayer || currentPoint == wonderAreaA.transform && isTouchingWall)
        {
            dir =-1;
            currentPoint = wonderAreaB.transform;
        }

        #endregion

        if (isChasingPlayer)
        {
            ChasePlayer();
        }

    }

    private bool IsChasingPlayersCheck()
    {
        for (int i = 0; i < chaseColliders.Length; i++)
        {
            Debug.Log(chaseColliders[i].name);
            if (chaseColliders[i].gameObject.name == "Player")
            {
                playerObject = chaseColliders[i].gameObject;
                return true;
            }
        }
                return false;
    }

    #region [[[ ATTACK AND CHASE PLAYER ]]]
    void ChasePlayer()
    {

        if (Vector2.Distance(transform.position, playerObject.transform.position) > chaseDistance && isChasingPlayer )
        {
            if (transform.position.x > playerObject.transform.position.x )
            {
                dir = -1;
                rb.velocity = new Vector2(movementSpeed * Time.deltaTime * -1, rb.velocity.y);
               transform.rotation = Quaternion.Euler(0,180,0);
            }
            else
            {
                dir =1;
                rb.velocity = new Vector2(movementSpeed * Time.deltaTime, rb.velocity.y);
                 transform.rotation = Quaternion.Euler(0,0,0);
            }
        }
        else if (Vector2.Distance(transform.position, playerObject.transform.position) < attackDistance)
        {

            attackRateTimer -= Time.deltaTime;

            if(attackRateTimer <= 0f )
            {
                audioSource.PlayOneShot(attackSound);
                attackRateTimer = attackRate;
                anim.SetTrigger("Attack");
                rb.velocity = Vector2.zero; 
            }
            else if(!isWithinAttackRange){


            }
        }
    }

    #endregion

    public void DealDmg()
    {
        
                playerObject.GetComponent<PlayerMovementCtrl>().ReceiveDamage(dmg);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(wonderAreaA.transform.position, .3f);
        Gizmos.DrawWireSphere(wonderAreaB.transform.position, .3f);

        Gizmos.DrawWireCube(triggerObject.position, edgeTriggerSize);
    }

    public void TakeDamage(int damage)
    {
        takeDmgCounter = takeDmgTimer;

        if(!canTakeDmg)
        {return;}

        health -= damage;
        UpdateHealthDisplay();
        audioSource.PlayOneShot(takeDmgSouund);
        if(health <= 0)
        {
            audioSource.PlayOneShot(DieSound);
            health = 0;
            canStartMoving = false;
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

        gameObject.SetActive(false);
    }
}
