using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandlerManager : MonoBehaviour
{
    public static Action<bool> onPlayerJumpEvent;
    public static void CallOnPlayerJump(bool b)
    {
        onPlayerJumpEvent?.Invoke(b);
    }

    public static Action<bool> onPlayerDashEvent;
    public static void CallOnPlayerDash(bool b)
    {
        onPlayerDashEvent?.Invoke(b);
    }

    public static Action<bool> onPlayerShootEvent;
    public static void CallOnPlayerShoot(bool b)
    {
        onPlayerShootEvent?.Invoke(b);
    }

    public static Action<int, Vector2> onPlayerCheckPointActivateEvent;
    public static void CallOnPlayerCheckPointActivate(int id, Vector2 position)
    {
            onPlayerCheckPointActivateEvent?.Invoke(id, position);
    }
}
