using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEjector : MonoBehaviour
{
    [SerializeField]int damage = 100;

    [SerializeField]float trapTime;
    float trapCounter;
    Animator anim;
    private void Start() {
        anim = GetComponent<Animator>();

        trapCounter = trapTime;
    }
private void OnTriggerEnter2D(Collider2D other) {
    
       // anim.SetTrigger("Impact");
        IPlayerDamageReceiver player = other.gameObject.GetComponent<IPlayerDamageReceiver>();

        if (player != null)
        {
            player.ReceiveDamage(damage);
        }

    }


    private void FixedUpdate() {
        
        trapCounter -= Time.deltaTime;
        if(trapCounter <= 0)
        {
            trapCounter = trapTime;
            anim.SetTrigger("Trigger");
        }
    }
}