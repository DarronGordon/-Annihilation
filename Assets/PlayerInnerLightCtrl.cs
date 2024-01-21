using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInnerLightCtrl : MonoBehaviour, ILightAbsorber
{
    [SerializeField] Slider innerLightDisplay;
    [SerializeField] float currentInnerLight;


    [Header("--DARKNESS CREEP--")]
    [SerializeField] CreepCtrl creep;
    [SerializeField] bool isInLight = false;
    float maxInnerLight = 15f;

    PlayerMovementCtrl pmc;

    float dmgTimer = 1;
    float dmgTimerCounter;

    private void Start()
    {
        currentInnerLight = maxInnerLight;

        pmc = GetComponent<PlayerMovementCtrl>();
    }
    public void ConsumeLight()
    {
        
        isInLight = true ;
    }

    public void StartLooseLight()
    {
        isInLight = false;
    }

    private void Update()
    {
        if(!isInLight) 
        {
            currentInnerLight -= Time.deltaTime * .5f;

            if(currentInnerLight <= 5 )
            {
                currentInnerLight = 0 ;

                pmc.ReceiveDamage(50);
                
            }
        }
        if(isInLight)
        {
            currentInnerLight += 1 * Time.deltaTime;

            if(currentInnerLight >= maxInnerLight ) 
            {
                currentInnerLight = maxInnerLight;
                
            }
        }
        innerLightDisplay.value = currentInnerLight;
        creep.SetInnerLight(currentInnerLight);
    }
}