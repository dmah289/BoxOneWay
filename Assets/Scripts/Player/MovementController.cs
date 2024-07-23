using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    // Component
    public Rigidbody2D playerRb;
    public Rigidbody2D platformRb;

    // Stats
    [SerializeField] int speed;
    [SerializeField] float speedMultiplier;
    [Range(1,10)]
    [SerializeField] float acceleration;
    [SerializeField] Vector2 relativeTransform;

    // Logic
    bool btnSpacePressed;
    bool isWallTouch;
    public bool isOnPlatform;
    public LayerMask wallLayer;
    public Transform wallCheckPoint;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        isOnPlatform = false;
    }

    private void Start()
    {
        UpdateRelativeTransform();
    }

    private void FixedUpdate()
    {
        UpdateSpeedMultiplier();
        float targetSpeed = speed * speedMultiplier * (relativeTransform.x / Mathf.Abs(relativeTransform.x));

        MoveOnPlatform(targetSpeed);

        isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.05f, 0.5f), 0, wallLayer);
        if (isWallTouch)
            Flip();
    }

    public void Move(InputAction.CallbackContext value)
    {
        if (value.started)
            btnSpacePressed = true;
        else if (value.canceled)
            btnSpacePressed = false;
    }

    private void UpdateSpeedMultiplier()
    {
        if(btnSpacePressed && speedMultiplier < 1f)
            speedMultiplier += Time.deltaTime * acceleration;

        else if(!btnSpacePressed && speedMultiplier > 0f)
        {
            speedMultiplier -= Time.deltaTime * acceleration;
            if (speedMultiplier < 0f)
                speedMultiplier = 0f;
        }
    }

    private void MoveOnPlatform(float targetSpeed)
    {
        if(isOnPlatform)
        {
            playerRb.velocity = playerRb.velocity.With(x: (targetSpeed + platformRb.velocity.x));
        }
        else
        {
            playerRb.velocity = playerRb.velocity.With(x: targetSpeed);
        }
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        UpdateRelativeTransform();
    }

    private void UpdateRelativeTransform()
    {
        relativeTransform = transform.InverseTransformVector(Vector2.one);
    }
}
