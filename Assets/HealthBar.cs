using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    public void UpdateHealthBar(float _currentvalue, float _maxHealth)
    {
        healthBar.value = _currentvalue / _maxHealth;
    }
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;

        transform.position = target.position + offset;

    }
}