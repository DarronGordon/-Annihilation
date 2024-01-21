using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRoll : MonoBehaviour
{
    [SerializeField] PlayerMovementCtrl player;

    public void ResetRollAnim()
    {
        player.ResetRoll();
    }

}
