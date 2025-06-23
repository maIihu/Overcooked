using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter SelectedCounter;
    }
    
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float playerRadius = 0.7f;
    
    private Vector2 _moveInput;
    private Vector3 _lastInteractDir;
    private Animator _anim;
    private ClearCounter _selectedCounter;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        if (Instance != null)
        {
            Debug.Log("Have Player");
        }

        Instance = this;
    }

    private void Update()
    {
        InputHandler();
        UpdateAnimation();
        Move();
        HandleInteractions();
    }

    private void InputHandler()
    {
        _moveInput = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) _moveInput.y += 1;
        if (Input.GetKey(KeyCode.A)) _moveInput.x -= 1;
        if (Input.GetKey(KeyCode.S)) _moveInput.y -= 1;
        if (Input.GetKey(KeyCode.D)) _moveInput.x += 1;
        _moveInput.Normalize();
    }

    private void UpdateAnimation()
    {
        _anim.SetBool(ContainString.IsWalking, _moveInput != Vector2.zero);
    }
    
    private void Move()
    {
        Vector3 moveDir = new Vector3(_moveInput.x, 0, _moveInput.y);
        float moveDistance = moveSpeed  * Time.deltaTime;
        
        Vector3 pos = transform.position;
        Vector3 upOffset = Vector3.up * playerHeight;
        
        // tach truc X
        Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
        bool canMoveX = !Physics.CapsuleCast(pos, pos + upOffset, 
            playerRadius, moveDirX, moveDistance);
        
        // tach truc Z
        Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
        bool canMoveZ =  !Physics.CapsuleCast(pos, pos + upOffset, 
            playerRadius, moveDirZ, moveDistance);
        
        if(canMoveX)
            transform.position += moveDirX * moveDistance;
        if (canMoveZ)
            transform.position += moveDirZ * moveDistance;
        
        Vector3 finalMove = new Vector3(canMoveX ? moveDirX.x : 0,
            0, canMoveZ ? moveDirZ.z : 0);
        
        if (finalMove != Vector3.zero)
            transform.forward = Vector3.Slerp(transform.forward, finalMove.normalized, 
                Time.deltaTime * rotateSpeed);
    }

    private void HandleInteractions()
    {
        Vector3 moveDir = new Vector3(_moveInput.x, 0, _moveInput.y);
        if (moveDir != Vector3.zero)
            _lastInteractDir = moveDir;

        float interactDistance = 2f;
        float sphereRadius = 0.5f;

        if (Physics.SphereCast(transform.position, sphereRadius, _lastInteractDir, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.TryGetComponent(out ClearCounter clearCounter))
            {
                if (_selectedCounter != clearCounter)
                {
                    SetSelectedCounter(clearCounter);
                    clearCounter.Interact();
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }


    private void SetSelectedCounter(ClearCounter clearCounter)
    {
        _selectedCounter = clearCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{
            SelectedCounter = _selectedCounter
        });
    }
}