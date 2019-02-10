using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerNumber;
    public float Health;
    

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Transform _transform;
    private Weapon _carriedWeapon;
    private GameObject _carryPoint;
    private Respawner _respawner;
    private bool _isCarrying = false;
    private float _initialHealth;

    internal void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
        _respawner = GetComponent<Respawner>();
        _carryPoint = _transform.Find( "CarryPoint" ).gameObject;

        _initialHealth = Health;
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

        //when y button is pressed, check circle collider and weapon
        if (Input.GetButtonDown("YButton" + PlayerNumber))
        {
            if (!DropWeapon())
            {
                Collider2D[] others = Physics2D.OverlapCircleAll(_rb.position, 1); // we can tweak this radius as necessary
                for (int i = 0; i < others.Length; i++) // what do we do about multiple weapons being in the circle collider? rn this will just get the first one
                {
                    Weapon otherWeapon = others[i].gameObject.GetComponent<Weapon>();
                    if (otherWeapon)
                    {
                        PickUp(otherWeapon);
                    }
                }

            }
        }

        //when x button is pressed, swing weapon
        if (Input.GetButtonDown("XButton" + PlayerNumber))
        {
            UseWeapon();
        }


    }

    /// <summary>
    /// Attempts to pick up a <see cref="Weapon"/>
    /// </summary>
    /// <returns>If pick up was successful.</returns>
    public void PickUp( Weapon weapon )
    {
        if (_isCarrying || weapon.IsCarried ) return;
        _carriedWeapon = weapon;
        _isCarrying = true;
        weapon.IsCarried = true;
        weapon.CarryingPlayer = this;

        weapon.gameObject.transform.parent = _carryPoint.transform;
        weapon.gameObject.transform.position = _carryPoint.transform.position;
        weapon.gameObject.transform.rotation = _carryPoint.transform.rotation;
    }

    /// <summary>
    /// If player is carrying a <see cref="Weapon"/>, drop it
    /// </summary>
    /// <returns> Returns true if weapon was dropped</returns>
    public bool DropWeapon()
    {
        if (!_isCarrying) return false;
        _carriedWeapon.IsCarried = false;
        _carriedWeapon.CarryingPlayer = null;
        _carriedWeapon.gameObject.transform.parent = null;
        _carriedWeapon.gameObject.transform.position = _transform.position; // tweak to be in front of the player or thrown ahead
        _isCarrying = false;
        _carriedWeapon = null;
        return true;

    }

    /// <summary>
    /// If player is carrying a <see cref="Weapon"/>, use it
    /// For now this is just swinging a sword, can be expanded for other weapons
    /// </summary>
    public void UseWeapon()
    {
        if (!_isCarrying) return;
        _carriedWeapon.UseWeapon();
    }

    /// <summary>
    /// Take damage from another <see cref="Player"/>, and <see cref="DieAndRespawn"/> if health dips below 0.
    /// </summary>
    public void TakeDamage( float damage, Player attacker )
    {
        if ( Health < 0 ) return;
        Health -= damage;
        if ( Health <= 0 )
        {
            DieAndRespawn();
        }
    }

    /// <summary>
    /// Remove the player from the scene temporarily, and schedule an invocation of <see cref="Respawn"/>.
    /// </summary>
    private void DieAndRespawn()
    {
        DropWeapon();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Invoke( "Respawn", 2f );
    }

    /// <summary>
    /// Function that calls for a respawn. Necessary for Invoke.
    /// </summary>
    private void Respawn()
    {
        _respawner.TryRespawn( gameObject );
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        Health = _initialHealth;
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
