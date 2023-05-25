using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody2D;

    public bool isDragging;

    public float jumpForce;
    public float gravityForce;
    public float jumpMultiplier;

    public Vector2 mousePosition;
    public Vector2 playerPosition;
    public Vector2 dragPosition;
    
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            if (myRigidbody2D.velocity.y <= 0f)
            {
                jumpForce = gravityForce * jumpMultiplier;
                myRigidbody2D.velocity = new Vector2(0, jumpForce);
            }
        }
    }

    private void Update()
    {
        AddGravity();
        HandleInput();
        MovePlayer();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            if (_mainCamera is not null)
                mousePosition =
                    _mainCamera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            playerPosition = transform.position;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void MovePlayer()
    {
        if (isDragging)
        {
            dragPosition = _mainCamera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            transform.position = new Vector2(playerPosition.x + (dragPosition.x - mousePosition.x),transform.position.y);
        }
    }

    private void AddGravity()
    {
        myRigidbody2D.velocity = new Vector2(0, myRigidbody2D.velocity.y - (gravityForce * gravityForce));
    }
}