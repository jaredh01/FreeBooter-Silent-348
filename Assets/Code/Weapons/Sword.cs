using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public override void UseWeapon()
    {
        if (_isActive || !IsCarried) return;
        _isActive = true;
        gameObject.transform.Rotate(0, 0, -90);

        Invoke("UnuseWeapon", 0.4f);
    }

    public override void UnuseWeapon()
    {
        _isActive = false;
        gameObject.transform.Rotate(0, 0, 90);
    }
}
