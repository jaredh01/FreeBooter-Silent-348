using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public bool IsCarried;
    public Player CarryingPlayer;

    internal SpriteRenderer _spriteRenderer;
    internal AudioSource _audioSource;
    internal bool _isActive = false;
    

    internal void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    public abstract void UseWeapon();

    public abstract void UnuseWeapon();

    public abstract void DropWeapon();

    private void OnTriggerStay2D(Collider2D other)
    {
        var temp = other.GetComponent<Player>();
        if (_isActive && temp && !(temp == CarryingPlayer))
        {
            temp.TakeDamage(100f, CarryingPlayer);
        }
    }
}
