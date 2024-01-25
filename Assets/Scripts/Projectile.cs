using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    #region [[[ VARS ]]]

    [SerializeField] float velocity;
    [SerializeField] int damage;
    float life;
    [SerializeField] float lifeTime;
    Rigidbody2D rb;
    [SerializeField] ParticleSystem explosionVFX;
    Vector3 dir;
    Animator anim;
    public Vector3 Dir { get => dir; set => dir = value; }

    #endregion

    private void OnEnable()
    {
        dir = PlayerInputControl.Instance.MoveDir;
        life = lifeTime;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }
    void Update()
    {
        life -= Time.deltaTime;

        if(life <= 0)
        {
            anim.SetTrigger("Fade");
        }

        
        rb.velocity = (Dir * velocity*Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetTrigger("Impact");
        IDamagable enemy = collision.gameObject.GetComponent<IDamagable>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
                anim.SetTrigger("Impact");
        IDamagable enemy = other.gameObject.GetComponent<IDamagable>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    public void ProjectileDeath()
    {
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}

