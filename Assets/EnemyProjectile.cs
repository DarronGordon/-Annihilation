using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    #region [[[ VARS ]]]

    [SerializeField] float velocity;
    [SerializeField] int damage;
    float life;
    [SerializeField] float lifeTime = 3f;
    Rigidbody2D rb;
    [SerializeField] ParticleSystem explosionVFX;
    Vector3 dir;
    public Vector3 Dir { get => dir; set => dir = value; }

    #endregion

    private void OnEnable()
    {
        life = lifeTime;
        dir = Vector2.right;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        life = lifeTime;

    }
    void Update()
    {
        
        rb.velocity = dir * velocity*Time.deltaTime;

        life -= Time.deltaTime;

        if(life <= 0)
        {
            ProjectileDeath();
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       // anim.SetTrigger("Impact");
        IPlayerDamageReceiver player = collision.gameObject.GetComponent<IPlayerDamageReceiver>();

        if (player != null)
        {
            player.ReceiveDamage(damage);
            ProjectileDeath();
        }

    }


    public void ProjectileDeath()
    {
        rb.velocity = Vector2.zero;
        Destroy(gameObject);
    }

    public void SetDir(float _dir)
    {

        dir.x = _dir;
    }
}

