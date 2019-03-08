using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float DamageValue;
    public float TimeBetweenDamageTick;

    private float _damageTimer;

    private void OnTriggerStay2D( Collider2D other )
    {
        var possiblePlayer = other.gameObject.GetComponent<Player>();
        if ( !possiblePlayer ) return;
        if (_damageTimer > 0)
        {
            _damageTimer -= Time.fixedDeltaTime;
            return;
        }
        possiblePlayer.TakeDamage( DamageValue, null );
        _damageTimer = TimeBetweenDamageTick;
    }
}
