using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool IsCarried;
    public Player CarryingPlayer;

    private Transform _transform;
    private bool _isActive = false;

    

    internal void Start()
    {
        _transform = GetComponent<Transform>();
    }

    public void UseWeapon()
    {
        if ( _isActive ) return;
        _isActive = true;
        Invoke( "UnuseWeapon", 1f  );
    }

    private void UnuseWeapon()
    {
        _isActive = false;
    }

    private void OnTriggerEnter2D( Collider2D other )
    {
        var temp = other.GetComponent<Player>();
        if ( _isActive && temp && !( temp == CarryingPlayer ) )
        {
            temp.TakeDamage( 100f, CarryingPlayer );
        }
    }
}
