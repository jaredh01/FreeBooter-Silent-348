using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public bool IsCarried;
    public Player CarryingPlayer;
    public WeaponRespawnPoint SourcePoint;

    internal SpriteRenderer _spriteRenderer;
    internal AudioSource _audioSource;
    internal bool _isActive = false;
    internal float _DespawnTime = 3f;
    internal float _despawnTimer;
    //public Rigidbody2D _rb;

    

    internal void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        StartIdleAnimation();
    }

    public abstract void UseWeapon();

    public abstract void UnuseWeapon();

    public void PickupWeapon()
    {
        IsCarried = true;
        transform.Find("Sparkles").GetComponent<ParticleSystem>().Stop();
        StopCoroutine( "IdleAnimation" );
        StopDespawn();
    }

    public abstract void DropWeapon();

    public void StopDespawn()
    {
        StopAllCoroutines();
    }

    internal void StartIdleAnimation()
    {
        transform.Find("Sparkles").GetComponent<ParticleSystem>().Play();
        GetComponent<SpriteRenderer>().flipX = false;
        transform.rotation = Quaternion.Euler( Vector3.zero );
        StartCoroutine( "IdleAnimation" );
    }

    private IEnumerator DespawnWeapon()
    {
        _despawnTimer = _DespawnTime;
        while ( _despawnTimer > 0 )
        {
            _despawnTimer -= Time.deltaTime;
            yield return null;
        }

        SourcePoint.FreePoint();
        transform.Find("Despawn Smoke").GetComponent<ParticleSystem>().Emit(10);
        GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, 0.4f);
    }

    private IEnumerator IdleAnimation()
    {
        float initialHeight = transform.position.y;
        while ( true )
        {
            transform.position = new Vector3( transform.position.x,
                initialHeight + Mathf.Lerp( 0, 0.2f, Mathf.PingPong( Time.time, 1 ) ),
                transform.position.z );
            yield return null;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        var temp = other.GetComponent<Player>();
        if (_isActive && temp && !(temp == CarryingPlayer))
        {
            temp.TakeDamage(100f, CarryingPlayer);
        }
    }
}
