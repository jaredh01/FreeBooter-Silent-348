using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public float AttackLength = 0.25f;
    private float _attackTimer;

    public override void UseWeapon()
    {
        if (_isActive || !IsCarried) return;
        _isActive = true;
        _audioSource.Play();
        _attackTimer = AttackLength;
        StartCoroutine( "SwordAttack" );
    }

    public override void UnuseWeapon()
    {
        _isActive = false;
        gameObject.transform.localRotation = Quaternion.Euler( Vector3.zero );
    }

    public override void DropWeapon()
    {
        if (_isActive)
        {
            StopCoroutine("SwordAttack");
            UnuseWeapon();
        }
        IsCarried = false;
        gameObject.transform.position = CarryingPlayer.transform.position;
        CarryingPlayer = null;
        gameObject.transform.parent = null;
        StartCoroutine("DespawnWeapon");
        // tweak to be in front of the player or thrown ahead
    }

    private IEnumerator SwordAttack()
    {
        while ( _attackTimer > AttackLength / 2 )
        {
            _attackTimer -= Time.fixedDeltaTime;
            var lerpVar = ( AttackLength - _attackTimer) / ( AttackLength / 2 );
            gameObject.transform.localRotation = Quaternion.Slerp( Quaternion.Euler( Vector3.zero ),
                Quaternion.Euler( new Vector3( 0, 0, -100 ) ), lerpVar );
            yield return null;
        }

        while ( _attackTimer > 0 )
        {
            _attackTimer -= Time.fixedDeltaTime;
            yield return null;
        }

        while ( _attackTimer > -0.1f )
        {
            _attackTimer -= Time.fixedDeltaTime;
            var lerpVar = ( -0.1f - _attackTimer ) / -0.1f;
            gameObject.transform.localRotation = Quaternion.Slerp(
                Quaternion.Euler( Vector3.zero ), Quaternion.Euler( new Vector3( 0, 0, -100 ) ),
                lerpVar );
            yield return null;
        }

        UnuseWeapon();
    }
}
