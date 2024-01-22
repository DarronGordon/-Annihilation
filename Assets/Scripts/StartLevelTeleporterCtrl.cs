using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelTeleporterCtrl : MonoBehaviour
{
    Animator anim;  
    IEnumerator Start()
    {
        anim = GetComponent<Animator>();
        yield return new WaitForSeconds(1f);
        anim.Play("Warp");
    }

  
}
