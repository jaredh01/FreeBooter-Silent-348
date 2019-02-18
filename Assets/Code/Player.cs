using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerNumber;
    public float Health;
    public Sprite AliveSprite;
    public Sprite DeadSprite;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Weapon _carriedWeapon;
    private GameObject _carryPoint;
    private Respawner _respawner;
    private bool _isCarrying = false;
    private float _initialHealth;
    private static float _maxSpeed = 10f;
    private static float _jumpHeight = 23f;
    private static float _epsilon = 0.1f;
    private static float _deadZone = 0.3f;

    internal void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _respawner = GetComponent<Respawner>();
        _carryPoint = transform.Find( "CarryPoint" ).gameObject;

        _initialHealth = Health;
        AssignPlayerColor();
    }

    internal void Update()
    {
        HandleInput();
    }

    internal void FixedUpdate()
    {
        //ClipHorizontalSpeed();
        CheckForFlip();
    }

    private void HandleInput()
    {
        if ( GameManager.GamePaused || GameManager.GameIsOver ) return;

        if (Input.GetAxis("Horizontal" + PlayerNumber) >= _deadZone || Input.GetAxis("Horizontal" + PlayerNumber) <= -_deadZone)
        {
            //_rb.velocity += Vector2.right * Input.GetAxis("Horizontal" + PlayerNumber);
            _rb.velocity = new Vector2( _maxSpeed * Input.GetAxis("Horizontal" + PlayerNumber), _rb.velocity.y);
        }
        else
        {
            _rb.velocity = new Vector2(.5f * _rb.velocity.x, _rb.velocity.y);
        }
        
        // If the a button is pressed and the player is not moving vertically (implying they are on
        // the ground) the player jumps.
        if (Input.GetButtonDown("AButton" + PlayerNumber) && Mathf.Abs(_rb.velocity.y) < _epsilon)
        {
            _rb.velocity += _jumpHeight * Vector2.up;
        }
        
        //when x button is pressed, swing weapon
        if (Input.GetButtonDown("XButton" + PlayerNumber))
        {
            if (_isCarrying)
            {
                _carriedWeapon.UseWeapon(); // SUPPOSEDLY rotates but actually squishes
            }
        }

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
     }

        
    private void ClipHorizontalSpeed()
    {
        if (Mathf.Abs(_rb.velocity.x) > _maxSpeed)
        {
            _rb.velocity = new Vector2(_maxSpeed * Mathf.Sign(_rb.velocity.x), _rb.velocity.y);
        }
    }

    private void CheckForFlip()
    {
        if ( _rb.velocity.x > _epsilon )
        {
            transform.rotation = Quaternion.Euler(0, 0f, 0);
        }
        else if ( _rb.velocity.x < -_epsilon )
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
    }

    /// <summary>
    /// Attempts to pick up a <see cref="Weapon"/>
    /// </summary>
    /// <returns>If pick up was successful.</returns>
    public void PickUp(Weapon weapon)
    {
        if (_isCarrying || weapon.IsCarried) return;
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
        if ( !_isCarrying ) return false;
        _carriedWeapon.DropWeapon();
        _isCarrying = false;
        _carriedWeapon = null;
        return true;
    }

    /// <summary>
    /// Take damage from another <see cref="Player"/>, and <see cref="DieAndRespawn"/> if health dips below 0.
    /// </summary>
    public void TakeDamage(float damage, Player attacker)
    {
        if (Health <= 0) return;
        Health -= damage;
        if (Health <= 0)
        {
            if ( attacker != null )
            FindObjectOfType<ScoreManager>().ScorePoints( 1, attacker.PlayerNumber );
            DieAndRespawn();
        }
    }

    /// <summary>
    /// Remove the player from the scene temporarily, and schedule an invocation of <see cref="Respawn"/>.
    /// </summary>
    private void DieAndRespawn()
    {
        DropWeapon();
        _spriteRenderer.sprite = DeadSprite;
        GetComponent<AudioSource>().Play();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Invoke("Respawn", 2f);
    }

    /// <summary>
    /// Function that calls for a respawn. Necessary for Invoke.
    /// </summary>
    private void Respawn()
    {
        _rb.velocity = Vector2.zero;
        _respawner.TryRespawn(gameObject);
        _spriteRenderer.sprite = AliveSprite;
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

    /// <summary>
    /// Trigger is used to check for item pickup
    /// </summary>
    private void OnTriggerEnter2D( Collider2D other )
    {
        //var otherWeapon = other.gameObject.GetComponent<Weapon>();
        //if ( otherWeapon )
        //{
        //    PickUp( otherWeapon );
        //}
    }
}
