using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerNumber;
    

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Transform _transform;
    private Weapon _carriedWeapon;
    private GameObject _carryPoint;
    private Respawner _respawner;
    private bool _isCarrying = false;

    internal void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
        _respawner = GetComponent<Respawner>();
        _carryPoint = _transform.Find( "CarryPoint" ).gameObject;

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
    /// Attempts to pick up a <see cref="Weapon"/>
    /// </summary>
    /// <returns>If pick up was successful.</returns>
    public void PickUp( Weapon weapon )
    {
        if ( _isCarrying || weapon.IsCarried ) return;
        _carriedWeapon = weapon;
        _isCarrying = true;
        weapon.IsCarried = true;

        weapon.gameObject.transform.parent = _transform;
        weapon.gameObject.transform.position = _carryPoint.transform.position;
        weapon.gameObject.transform.rotation = _carryPoint.transform.rotation;
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

    /// <summary>
    /// Trigger is used to check for item pickup
    /// </summary>
    private void OnTriggerEnter2D( Collider2D other )
    {
        var otherWeapon = other.gameObject.GetComponent<Weapon>();
        if ( otherWeapon )
        {
            PickUp( otherWeapon );
        }
    }
}
