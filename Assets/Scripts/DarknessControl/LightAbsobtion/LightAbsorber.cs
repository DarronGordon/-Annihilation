using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightAbsorber : MonoBehaviour
{
    [SerializeField] float lightAbsorbtionRate;
    [SerializeField] Light2D light;
    [SerializeField]bool absorbLight;
    [SerializeField] bool eternalLight;

    bool canAbsorb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ILightAbsorber absorber = collision.GetComponent<ILightAbsorber>();

        if (absorber != null && canAbsorb)
        {
            absorber.ConsumeLight();

            absorbLight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ILightAbsorber absorber = collision.GetComponent<ILightAbsorber>();

        if (absorber != null)
        {
            absorber.StartLooseLight();

            absorbLight = false;
        }
    }

    void Start()
    {
      //  light = GetComponentInChildren<Light>();
    }

    void Update()
    {
        if(absorbLight) 
        {
            if (eternalLight)
            { return; }

            float lerpedIntensity = Mathf.Lerp(light.intensity, 0f, lightAbsorbtionRate * Time.deltaTime);

            light.intensity = lerpedIntensity;
        }
    }
}
