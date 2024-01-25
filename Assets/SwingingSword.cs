using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingSword : MonoBehaviour
{
    [SerializeField]int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
       // anim.SetTrigger("Impact");


    }
    private void OnTriggerEnter2D(Collider2D other) {
        IPlayerDamageReceiver player = other.gameObject.GetComponent<IPlayerDamageReceiver>();

        if (player != null)
        {
            player.ReceiveDamage(damage);
        }
    }
}