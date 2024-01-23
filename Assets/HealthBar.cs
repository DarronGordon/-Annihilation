using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;



    public void UpdateHealthBar(int _currentvalue, int _maxHealth)
    {
        healthBar.value = _currentvalue;
    }
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;

        transform.position = target.position + offset;

    }
}