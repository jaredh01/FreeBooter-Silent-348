using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool IsCarried;
    public Player CarryingPlayer;

    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    private bool _isActive = false;
    

    internal void Start()
    {
        _transform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// If player is carrying a <see cref="Weapon"/>, use it
    /// For now this is just swinging a sword, can be expanded for other weapons
    /// </summary>
    public void UseWeapon()
    {
        if (_isActive || !IsCarried ) return;
        _isActive = true;
        this.gameObject.transform.Rotate(0, 0, -90);

        Invoke("UnuseWeapon", 0.4f);
    }

    public void UnuseWeapon()
    {
        _isActive = false;
        gameObject.transform.Rotate(0,0,90);
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
