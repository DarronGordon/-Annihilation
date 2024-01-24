using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    int amountToHeal = 25;
    float exp = 25f;
    [SerializeField]ParticleSystem healEffect;

    private void OnTriggerEnter2D(Collider2D other) {

        IJamJuice potion = other.GetComponent<IJamJuice>();

        if(potion != null)
        {
            potion.IJamJuice(amountToHeal, exp);

            Destroy(gameObject);
        }
    }
}
