using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerNumber;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    internal void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        AssignPlayerColor();
    }

    internal void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // Placeholder 
        _rb.velocity += Vector2.right * Input.GetAxis( "Horizontal" + PlayerNumber );
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
