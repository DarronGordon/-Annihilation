using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputControl : MonoBehaviour
{

    public static PlayerInputControl Instance;

    PlayerInput inputCtrl;
    Annihilation inputctrl;


    [SerializeField] private float xDir;
    private bool isJumping;
    private bool isJumpingRelease;   
    private bool isShooting;
    private bool isDashing;

    private Vector2 moveDir;

bool canMove;
    public float XDir { get => xDir; set => xDir = value; }
    public bool IsJumping { get => isJumping; set => isJumping = value; }
    public bool IsShooting { get => isShooting; set => isShooting = value; }
    public bool IsDashing { get => isDashing; set => isDashing = value; }
    public Vector2 MoveDir { get => moveDir; set => moveDir = value; }
    public bool IsJumpingRelease { get => isJumpingRelease; set => isJumpingRelease = value; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        inputctrl = new Annihilation();

        inputctrl.Enable();
        inputctrl.Player.Move.performed += HorizontalMovementCtrl;
        inputctrl.Player.Move.canceled += HorizontalMovementCtrl;
        inputctrl.Player.Jump.performed += JumpCtrl;
        inputctrl.Player.Jump.canceled += JumpCtrlRelease;
        inputctrl.Player.Dash.performed += DashCtrl;
        inputctrl.Player.Fire.performed += ShootCtrl;
        
        ResetMovement();

    }

    void JumpCtrl(InputAction.CallbackContext contex)
    {
       if(!canMove){return;}
        isJumping = true;
        EventHandlerManager.CallOnPlayerJump(isJumping);
    }
        void JumpCtrlRelease(InputAction.CallbackContext contex)
    {
        if(!canMove){return;}
        isJumping = false;
        isJumpingRelease = true;

        EventHandlerManager.CallOnPlayerJumpRelease(isJumping);
    }

    void DashCtrl(InputAction.CallbackContext contex)
    {
        if(!canMove){return;}
        isDashing = !isDashing;

        EventHandlerManager.CallOnPlayerDash(isDashing);
    }
    void ShootCtrl(InputAction.CallbackContext contex)
    {
        if(!canMove){return;}
        isShooting = !isShooting;

        EventHandlerManager.CallOnPlayerShoot(isShooting);
    }

    void HorizontalMovementCtrl(InputAction.CallbackContext contex)
    {
        if(!canMove){return;}
        moveDir = contex.ReadValue<Vector2>();
        Vector2 v = contex.ReadValue<Vector2>().normalized;
        xDir = v.x;
    }

    internal void StopMovement()
    {
        canMove= false;
    }
        internal void ResetMovement()
    {
        canMove= true;
    }
}
