using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool IsCarried;

    private Transform _transform;
    

    internal void Start()
    {
        _transform = GetComponent<Transform>();
    }

    /// <summary>
    /// If player is carrying a <see cref="Weapon"/>, use it
    /// For now this is just swinging a sword, can be expanded for other weapons
    /// </summary>
    public void UseWeapon()
    {
        //if (!IsCarried) return;
        //for now, sword only:
        Quaternion change = Quaternion.AngleAxis(90, Vector3.forward);
        // this.gameObject.transform.rotation = change; //isn't rotating properly
        //this.gameObject.transform.Rotate(Vector3.forward * 90);
        this.gameObject.transform.Rotate(0, 0, 90);
    }


    void Update()
    {
        
    }
}
