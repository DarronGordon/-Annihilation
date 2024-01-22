using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField]int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
       // anim.SetTrigger("Impact");
        IPlayerDamageReceiver player = collision.gameObject.GetComponent<IPlayerDamageReceiver>();

        if (player != null)
        {
            player.ReceiveDamage(damage);
        }

    }
}