using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody2D;
    [SerializeField] private GameObject jumpEffect;
    [SerializeField] private GameObject touchToStartText;

    private bool _isDragging;

    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityForce;
    [SerializeField] private float jumpMultiplier;

    [SerializeField] private float leftBound;
    [SerializeField] private float rightBound;

    private bool _isDead;
    private bool _isStart;

    private Vector2 _mousePosition;
    private Vector2 _playerPosition;
    private Vector2 _dragPosition;

    private Camera _mainCamera;
    private AudioSource _jumpSound;

    private void Start()
    {
        _isDead = false;
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
                myRigidbody2D.velocity = Vector2.zero;
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
        WaitForTouch();
        if (_isDead)
        {
            return;
        }

        if (!_isStart)
        {
            return;
        }
        
        AddGravity();
        HandleInput();
        MovePlayer();
        CheckForDeath();
    }

    private void WaitForTouch()
    {
        if (!_isStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isStart = true;
                touchToStartText.SetActive(false);
            }
        }
    }
    
    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            _mousePosition = _mainCamera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            _playerPosition = transform.position;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }
    }

    private void MovePlayer()
    {
        if (_isDragging)
        {
            _dragPosition = _mainCamera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            transform.position = new Vector2(_playerPosition.x + (_dragPosition.x - _mousePosition.x),transform.position.y);

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
            _isDead = true;
            GameManager.Instance.GameOver();
        }
    }
}