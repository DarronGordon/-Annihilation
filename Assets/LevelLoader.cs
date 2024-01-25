using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] SceneManagerCtrl smc;
    [SerializeField]string levelToLoad;
    void Start()
    {
        
    }
private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            smc.LoadLevelScene(levelToLoad);
        }
}
}