using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerNumber;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private float _maxSpeed = 10;

    internal void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        AssignPlayerColor();
    }

    internal void FixedUpdate()
    {
        HandleInput();
        ClipHorizontalSpeed();

    }

    private void HandleInput()
    {
        // Placeholder 
        if (Input.GetAxis("Horizontal" + PlayerNumber) >= 0.2 || Input.GetAxis("Horizontal" + PlayerNumber) <= -0.2)
        {
            _rb.velocity += Vector2.right * Input.GetAxis("Horizontal" + PlayerNumber);
        }
        else
        {
            _rb.velocity = .8f * _rb.velocity;
        }
        
        if (Input.GetButtonDown("AButton" + PlayerNumber))
        {
            _rb.velocity += 5 * Vector2.up;
        }
    }

    private void ClipHorizontalSpeed()
    {
        if (Mathf.Abs(_rb.velocity.x) > _maxSpeed)
        {
            _rb.velocity = _maxSpeed * _rb.velocity.normalized;
        }
    }

    /// <summary>
    /// Modifies the player's <see cref="SpriteRenderer.color"/>, based on <see cref="PlayerNumber"/>
    /// </summary>
    private void AssignPlayerColor()
    {
        Color playerColor;
        switch (PlayerNumber)
        {
            case 1:
                playerColor = Color.red;
                break;
            case 2:
                playerColor = Color.blue;
                break;
            case 3:
                playerColor = Color.yellow;
                break;
            case 4:
                playerColor = Color.green;
                break;
            default:
                playerColor = Color.white;
                break;
        }

        _spriteRenderer.color = playerColor;
    }
}
