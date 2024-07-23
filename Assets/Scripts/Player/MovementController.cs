using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    // Component
    [SerializeField] Rigidbody2D rb2d;

    // Stats
    [SerializeField] int speed;
    [SerializeField] float speedMultiplier;
    [Range(1,10)]
    [SerializeField] float acceleration;
    [SerializeField] Vector2 relativeTransform;

    // Logic
    bool btnSpacePressed;

    bool isWallTouch;
    public LayerMask wallLayer;
    public Transform wallCheckPoint;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateRelativeTransform();
    }

    private void FixedUpdate()
    {
        UpdateSpeedMultiplier();
        float targetSpeed = speed * speedMultiplier * (relativeTransform.x / Mathf.Abs(relativeTransform.x));
        rb2d.velocity = rb2d.velocity.With(x: targetSpeed);

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
