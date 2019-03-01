using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringZone : MonoBehaviour
{
    private float _maxXPosition;
    private float _maxYPosition;
    private float _timeTillMove;
    private static float _scoringInterval = 1f;
    private static float _movementTime = 15f;


    void Start()
    {
        _timeTillMove = _movementTime;
    }


    void Update()
    {
        
    }
}
