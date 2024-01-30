using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SummonGrounds : MonoBehaviour
{
    [SerializeField] Transform groundLevel;
    [SerializeField] GameObject goulPrefab;
    [SerializeField] int maxGouls;
    [SerializeField][Range(-20,20)] float ranFactor;
    [SerializeField] Animator anim;
    [SerializeField] List<GameObject> goulPool = new List<GameObject>();

    private void Start()
    {
        SetUpGoulPool();
    }

    private void SetUpGoulPool()
    {
        for(int i = 0; i < maxGouls; i++)
        {
            GameObject currentGoul = Instantiate(goulPrefab, groundLevel.position, Quaternion.identity, this.transform);
            currentGoul.SetActive(false);

            goulPool.Add(currentGoul);
        }
    }

    public void TrySummonGoul()
    {
        foreach(GameObject goul in goulPool) 
        {
            if(!goul.activeSelf)
            {
                
                anim.SetBool("Summon", true);    
                ResetGoulPos(goul.transform);
                goul.SetActive(true); 
                break;
            }
        }
    }

    private void ResetGoulPos(Transform currentGoul)
    {
        currentGoul.position = new Vector2(Random.Range(groundLevel.position.x, groundLevel.position.x + ranFactor), groundLevel.position.y);
    }



}
