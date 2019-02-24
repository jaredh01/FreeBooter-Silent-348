using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarAx : Weapon
{
    public float AttackLength = 2f;
    public float CoolDownLength = 0.25f;
    private float _attackTimer;
    private float _coolDownTimer;

    public override void UseWeapon()
    {
        if (_isActive || !IsCarried || _coolDownTimer > 0 ) return;
        _attackTimer = AttackLength;
        _coolDownTimer = CoolDownLength;
        StartCoroutine( "WarAxAttack" );
    }

    public override void UnuseWeapon()
    {
        _isActive = false;
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public override void DropWeapon()
    {
        if ( _isActive || _coolDownTimer > 0 )
        {
            StopCoroutine( "WarAxAttack" );
            UnuseWeapon();
            _coolDownTimer = 0;
        }
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        IsCarried = false;
        gameObject.transform.position = CarryingPlayer.transform.position;
        CarryingPlayer = null;
        gameObject.transform.parent = null;
        StartCoroutine("DespawnWeapon");
        // tweak to be in front of the player or thrown ahead
    }

    private IEnumerator WarAxAttack()
    {
        // Prep swing
        while (_attackTimer > AttackLength / 2 )
        {
            _attackTimer -= Time.fixedDeltaTime;
            var lerpVar = ( AttackLength - _attackTimer ) / ( AttackLength / 4 );
            gameObject.transform.localRotation = Quaternion.Slerp( Quaternion.Euler( Vector3.zero ),
                Quaternion.Euler( new Vector3( 0, 0, 45 ) ), lerpVar );
            yield return null;
        }

        _isActive = true;
        _audioSource.Play();
        
        // Swing forward attack
        while ( _attackTimer > 0 )
        {
            _attackTimer -= Time.fixedDeltaTime;
            var lerpVar = ( ( AttackLength / 2 ) - _attackTimer) / ( AttackLength / 2 );
            gameObject.transform.localRotation = Quaternion.Slerp( Quaternion.Euler( new Vector3( 0, 0, 45 ) ),
                Quaternion.Euler( new Vector3( 0, 0, -100 ) ), lerpVar );
            yield return null;
        }

        _isActive = false;

        //Draw back up
        while ( _coolDownTimer > 0 )
        {
            _coolDownTimer -= Time.fixedDeltaTime;
            var lerpVar = (CoolDownLength - _coolDownTimer) / CoolDownLength;
            gameObject.transform.localRotation = Quaternion.Slerp( Quaternion.Euler( new Vector3( 0, 0, -100 ) ),
                Quaternion.Euler( Vector3.zero ), lerpVar );
            yield return null;
        }
    }
}
