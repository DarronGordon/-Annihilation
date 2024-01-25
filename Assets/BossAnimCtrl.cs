using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimCtrl : MonoBehaviour
{
    [SerializeField] BossCtrl bc;

    public void DealDamage()
    {
        bc.DealDmg();
    }

    public void Die()
    {
        bc.gameObject.SetActive(false);
    }
}
