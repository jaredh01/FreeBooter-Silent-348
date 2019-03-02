using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon
{
    public float AttackLength;
    public float CoolDownLength;

    private float _attackTimer;
    private float _coolDownTimer;

    public override void UseWeapon()
    {
        if (_isActive || !IsCarried || _coolDownTimer > 0 ) return;
        _isActive = true;
        _attackTimer = AttackLength;
        _coolDownTimer = CoolDownLength;
        _audioSource.Play();
        gameObject.transform.Rotate(0, 0, -90);
        StartCoroutine( "SpearAttack" );
    }

    public override void UnuseWeapon()
    {
        _isActive = false;
        gameObject.transform.localPosition = Vector3.zero;
    }

    public override void DropWeapon()
    {
        if ( _isActive || _coolDownTimer > 0)
        {
            StopCoroutine( "SpearAttack");
            UnuseWeapon();
            _coolDownTimer = 0;
        }
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        IsCarried = false;
        gameObject.transform.position = CarryingPlayer.transform.position;
        CarryingPlayer = null;
        gameObject.transform.parent = null;
        StartCoroutine("DespawnWeapon");
    }

    private IEnumerator SpearAttack()
    {
        // Stab forward
        while ( _attackTimer > 0 )
        {
            _attackTimer -= Time.fixedDeltaTime;
            var lerpVar = ( AttackLength - _attackTimer ) / AttackLength;
            gameObject.transform.localPosition = Vector3.Slerp( 0.75f * Vector3.left, Vector3.right, lerpVar );
            yield return null;
        }

        _isActive = false;

        // Cooldown, reset rotation
        while (_coolDownTimer > 0)
        {
            _coolDownTimer -= Time.fixedDeltaTime;
            var lerpVar = (CoolDownLength - _coolDownTimer) / CoolDownLength;
            gameObject.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(new Vector3(0, 0, -90)),
                Quaternion.Euler(Vector3.zero), lerpVar);
            gameObject.transform.localPosition = Vector3.Slerp(Vector3.right, Vector3.zero,  lerpVar);
            yield return null;
        }

        UnuseWeapon();
    }
}
