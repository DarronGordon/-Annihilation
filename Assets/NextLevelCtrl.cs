using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelCtrl : MonoBehaviour
{
    [SerializeField] SceneManagerControlerLevel1 smc;
    void Start()
    {
        
    }
private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            smc.LoadLevelScene("Runner");
        }
}
    // Update is called once per frame
    void Update()
    {
        
    }
}
