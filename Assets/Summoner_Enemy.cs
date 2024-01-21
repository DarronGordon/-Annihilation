using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner_Enemy : MonoBehaviour, IDamagable
{
    [Header("Summoning Area")]
    [SerializeField] SummonGrounds summonGrounds;
    [SerializeField]float summonTimerMin, summonTimerMax;

    [Header("Health")]
    [SerializeField] int health = 80;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("StartSummoning", summonTimerMin, Random.Range(summonTimerMin, summonTimerMax));
    }

    void StartSummoning()
    {
        summonGrounds.TrySummonGoul();

    }

    public void ResetAnimState()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Summon", false);
    }

    public void TakeDamage(int damage)
    {
        anim.SetTrigger("TakeDmg");
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            anim.SetTrigger("Die");
        }
    }

    public void DestroyGameObjectAfterAnim()
    {
        Destroy(gameObject);
    }
}
