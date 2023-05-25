using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody2D;
    [SerializeField] private GameObject jumpEffect;

    public bool isDragging;

    public float jumpForce;
    public float gravityForce;
    public float jumpMultiplier;

    public float leftBound;
    public float rightBound;

    public Vector2 mousePosition;
    public Vector2 playerPosition;
    public Vector2 dragPosition;

    private Camera _mainCamera;
    private AudioSource _jumpSound;

    private void Start()
    {
        _mainCamera = Camera.main;
        _jumpSound = GetComponent<AudioSource>();
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
                ScoreManager.Instance.AddScore();
                gravityForce += 0.005f;
                GameManager.Instance.ambientMusic.pitch += 0.005f;
                _mainCamera.backgroundColor = other.gameObject.GetComponent<SpriteRenderer>().color;
                JumpEffect();
                DestroyAndCreateNewPlatform(other);
            }
        }
    }

    private void JumpEffect()
    {
        Destroy(Instantiate(jumpEffect,transform.position,Quaternion.identity),0.5f);
        _jumpSound.Play();
    }

    private void DestroyAndCreateNewPlatform(Collider2D platform)
    {
        platform.gameObject.SetActive(false);
        PlatformSpawner.Instance.CreateNewPlatform();
    }

    private void Update()
    {
        AddGravity();
        HandleInput();
        MovePlayer();
        CheckForDeath();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            mousePosition = _mainCamera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
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

            if (transform.position.x < leftBound)
            {
                transform.position = new Vector2(leftBound,transform.position.y);
            }
            if(transform.position.x > rightBound)
            {
                transform.position = new Vector2(rightBound,transform.position.y);
            }
        }
    }

    private void AddGravity()
    {
        myRigidbody2D.velocity = new Vector2(0, myRigidbody2D.velocity.y - (gravityForce * gravityForce));
    }

    private void CheckForDeath()
    {
        if (transform.position.y < _mainCamera.transform.position.y - 15)
        {
            GameManager.Instance.GameOver();
        }
    }
}