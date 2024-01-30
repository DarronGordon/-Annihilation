using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastEnemy : MonoBehaviour
{
    [SerializeField]GameObject checkpoint;

    private void OnDestroy() {
        checkpoint.SetActive(true);
    }

    private void OnDisable() {
        checkpoint.SetActive(true);
    }
}
