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
    float startingInnerLightAmount = 100f;


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
        intensityAmount = innerLight /100;
        powerAmount = innerLight /10;
    }
}
