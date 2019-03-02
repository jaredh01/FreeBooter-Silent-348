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
    public Rigidbody2D _rb;

    

    internal void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _rb = GetComponent < Rigidbody2D>();
    }

    public abstract void UseWeapon();

    public abstract void UnuseWeapon();

    public abstract void DropWeapon();

    public void StopDespawn()
    {
        StopAllCoroutines();
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
        Destroy(gameObject);
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
