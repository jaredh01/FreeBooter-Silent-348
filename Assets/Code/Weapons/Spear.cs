using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon
{
    public float AttackLength = 0.5f;

    private float _attackTimer;

    public override void UseWeapon()
    {
        if (_isActive || !IsCarried) return;
        _isActive = true;
        _attackTimer = AttackLength;
        gameObject.transform.Rotate(0, 0, -90);
        StartCoroutine( "SpearAttack" );
    }

    public override void UnuseWeapon()
    {
        _isActive = false;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.Rotate(0, 0, 90);
    }

    public override void DropWeapon()
    {
        if ( _isActive )
        {
            StopCoroutine( "SpearAttack");
            UnuseWeapon();
        }
        IsCarried = false;
        gameObject.transform.position = CarryingPlayer.transform.position;
        CarryingPlayer = null;
        gameObject.transform.parent = null;
    }

    private IEnumerator SpearAttack()
    {
        while ( _attackTimer > 0 )
        {
            _attackTimer -= Time.fixedDeltaTime;
            var lerpVar = ( AttackLength - _attackTimer ) / AttackLength;
            gameObject.transform.localPosition = Vector3.Slerp( 0.75f * Vector3.left, Vector3.right, lerpVar );
            yield return null;
        }

        UnuseWeapon();
    }
}
