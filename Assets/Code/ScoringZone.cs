using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringZone : MonoBehaviour
{
    private float _maxXPosition;
    private float _maxYPosition;
    private float _timeTillMove;
    private static float _movementTime = 15f;


    void Start()
    {
        _timeTillMove = _movementTime;
    }


    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            coll.gameObject.GetComponent<Player>().NowScoring();
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            coll.gameObject.GetComponent<Player>().NotScoring();
        }
    }
}
