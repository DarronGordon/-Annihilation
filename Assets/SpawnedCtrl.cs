using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedCtrl : MonoBehaviour
{
    [SerializeField]Goul_Enemy gE;
    
    public void DestroyParent()
    {
        gE.gameObject.SetActive(false);
    }
    public void EndSpawn()
    {
        gE.HasSpawned();
    }
}
