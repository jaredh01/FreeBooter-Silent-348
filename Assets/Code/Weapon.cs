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
        
        //for now, sword only:
        Quaternion change = Quaternion.AngleAxis(90, Vector3.forward);
        // this.gameObject.transform.rotation = change; //isn't rotating properly
        //this.gameObject.transform.Rotate(Vector3.forward * 90);
        //this.gameObject.transform.Rotate(0, 0, 90);
        _spriteRenderer.color = Color.red;

        Invoke("UnuseWeapon", 1f);
    }

    public void UnuseWeapon()
    {
        _isActive = false;
        //gameObject.transform.Rotate(0,0,-90);
        _spriteRenderer.color = Color.magenta;
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
