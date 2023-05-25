using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody2D;

    public float jumpForce;
    public float gravityForce;
    public float jumpMultiplier;

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            if (myRigidbody2D.velocity.y <= 0f)
            {
                jumpForce = gravityForce * jumpMultiplier;
                myRigidbody2D.velocity = new Vector2(0,jumpForce);
            }
        }
    }

    private void Update()
    {
        AddGravity();
    }

    private void AddGravity()
    {
        myRigidbody2D.velocity = new Vector2(0,myRigidbody2D.velocity.y - (gravityForce * gravityForce));
    }
}
