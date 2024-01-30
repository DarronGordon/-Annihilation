using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInnerLightCtrl : MonoBehaviour, ILightAbsorber
{
    [SerializeField] Slider innerLightDisplay;
    [SerializeField] float currentInnerLight;

    AudioSource audioSource;
    [SerializeField]AudioClip heartBeat;


    [Header("--DARKNESS CREEP--")]
    [SerializeField] CreepCtrl creep;
    [SerializeField] bool isInLight = false;
    float maxInnerLight = 15f;

    PlayerMovementCtrl pmc;
    float dmgTimerCounter;

    private void OnEnable() {
        EventHandlerManager.onPlayerReSpawnEvent += ResetInnerLight;
    }

    private void OnDisable() {
        EventHandlerManager.onPlayerReSpawnEvent -= ResetInnerLight;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        pmc.SetInnerLight(currentInnerLight);

        if(currentInnerLight <= maxInnerLight/2)
        {
            audioSource.PlayOneShot(heartBeat);
        }
    }

    public void ResetInnerLight()
    {
        currentInnerLight = maxInnerLight;

        innerLightDisplay.value = currentInnerLight;
        creep.SetInnerLight(currentInnerLight);
        pmc.SetInnerLight(currentInnerLight);
    }
}