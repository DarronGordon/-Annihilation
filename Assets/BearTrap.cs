using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    
    [SerializeField]int damage = 100;
    Animator anim;
    private void Start() {
        anim = GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        IPlayerDamageReceiver player = other.gameObject.GetComponent<IPlayerDamageReceiver>();

        if (player != null)
        {
            anim.SetTrigger("Triggered");
            player.ReceiveDamage(damage);
        }

    }
    
}
