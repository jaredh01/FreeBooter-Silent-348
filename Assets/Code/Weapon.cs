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

    void Update()
    {
        
    }
}
