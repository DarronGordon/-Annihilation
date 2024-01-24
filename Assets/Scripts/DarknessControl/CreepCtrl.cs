using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class CreepCtrl : MonoBehaviour
{
    [Header(" references")]
    [SerializeField] private Material mat;
    [SerializeField] private ScriptableRendererFeature darknessCreep;

    [SerializeField] private float darkneesCreepFadeOutDuration;

    int vignetteIntensity = Shader.PropertyToID("_VignetteIntensity");
    int vignettePower = Shader.PropertyToID("_VignettePower");

    [SerializeField][Range(0f,2f)] float intensityAmount = 0f;
    [SerializeField][Range(0f, 10f)] float powerAmount = 0f;

    [Header("CREEP")]
    [SerializeField] private Slider creepSliderDispaly;
    [SerializeField] private float creepSpeed;


    private void LateUpdate()
    {
        if (mat.GetFloat(vignettePower) != powerAmount)
        {
            CreepPower();
        }
        else
        {
            CreepPower();
        }

        if (mat.GetFloat(vignetteIntensity) != intensityAmount)
        {
            CreepIntensity();
        }
        else
        {
            CreepIntensity();
        }
    }

    void CreepPower()
    {
        float lerpedPower = Mathf.Lerp(mat.GetFloat(vignettePower), powerAmount, creepSpeed * Time.deltaTime);
        mat.SetFloat(vignettePower, lerpedPower);
    }
    void CreepIntensity()
    {
        float lerpedIntensity = Mathf.Lerp(mat.GetFloat(vignetteIntensity), intensityAmount, creepSpeed * Time.deltaTime);
        mat.SetFloat (vignetteIntensity, lerpedIntensity);
    }

    public void SetInnerLight(float innerLight)
    {
        powerAmount = (innerLight /10)- .1f;
        SetIntensity(innerLight);
    }

    private void SetIntensity(float innerLight)
    {

        if(innerLight > 14)
        {
            intensityAmount = .1f;
        }      
        else if(innerLight < 14 && innerLight > 13)
        {
            intensityAmount = .2f;
        }
        else if(innerLight < 13 && innerLight > 12)
        {
            intensityAmount = .3f;
        }
        else if(innerLight < 12 && innerLight > 11)
        {
            intensityAmount = .5f;
        }
        else if(innerLight <11 && innerLight > 10)
        {
            intensityAmount = 0.7f;
        }
        else if(innerLight < 10 && innerLight > 8)
        {
            intensityAmount = .9f;
        }
        else if(innerLight < 8 && innerLight > 6)
        {
            intensityAmount = 1.1f;
        }
        else if(innerLight < 6 && innerLight > 4)
        {
            intensityAmount = 1.3f;
        }
        else if(innerLight < 4 && innerLight > 2)
        {
            intensityAmount = 1.5f;
        }
        else{
            intensityAmount = 1.8f;
        }
    }
}
