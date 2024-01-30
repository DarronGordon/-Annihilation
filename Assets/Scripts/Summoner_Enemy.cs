using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner_Enemy : MonoBehaviour, IDamagable
{
    [Header("Summoning Area")]
    [SerializeField] SummonGrounds summonGrounds;
    [SerializeField]float summonTimerMin, summonTimerMax;

    [Header("Health")]
    [SerializeField] int health ;
        [SerializeField] int maxHealth= 150;
    AudioSource audioSource;
    [SerializeField]AudioClip takeDmgSouund;
    [SerializeField]AudioClip DieSound;
    [SerializeField]AudioClip attackSound;
    [SerializeField]AudioClip spawnSound;
        [SerializeField] HealthBar hbar;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        InvokeRepeating("StartSummoning", summonTimerMin, Random.Range(summonTimerMin, summonTimerMax));
        UpdateHealthDisplay();
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
        audioSource.PlayOneShot(takeDmgSouund);
        UpdateHealthDisplay();

        if (health <= 0)
        {
            UpdateHealthDisplay();
            audioSource.PlayOneShot(DieSound);
            health = 0;
            anim.SetTrigger("Die");
        }
    }

    public void DestroyGameObjectAfterAnim()
    {
        AudioTriggeredSound ats = GetComponent<AudioTriggeredSound>();
        ats.PlaySound();
        Destroy(gameObject,1f);
    }

        private void UpdateHealthDisplay()
    {
        
        hbar.UpdateHealthBar(health,maxHealth);
    }



}
