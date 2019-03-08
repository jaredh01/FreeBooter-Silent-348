using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringZone : MonoBehaviour
{
    public float TimeToScore;
    public GameObject ProgressBar;
    public GameObject ScoringProgressIndicator;

    private float _scoringTimer;
    private Player _scoringPlayer;
    private float _maxXPosition;
    private float _maxYPosition;
    private float _timeTillMove;
    private static float _movementTime = 15f;


    void Start()
    {
        _timeTillMove = _movementTime;
        _scoringTimer = TimeToScore;
    }


    void Update()
    {
        if ( _scoringPlayer == null ) return;
        if ( _scoringPlayer.Health < 0 )
        {
            Debug.Log("F");
            _scoringPlayer = null;
            ResetScoreBar();
            ScoringProgressIndicator.SetActive(false);
        }
        if ( _scoringTimer > 0 )
        {
            _scoringTimer -= Time.fixedDeltaTime;
            var lerpVar = (TimeToScore - _scoringTimer) / TimeToScore;
            //6.8 is a magic number sadly
            ProgressBar.transform.localScale = new Vector3( Mathf.Lerp( 0, 6.8f, lerpVar ),
                ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z );
        }
        else
        {
            _scoringPlayer.ScoreAPoint();
            ResetScoreBar();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if ( _scoringPlayer != null ) return;
        var possiblePlayer = coll.gameObject.GetComponent<Player>();
        if ( possiblePlayer == null ) return;
        _scoringPlayer = possiblePlayer;
        SetProgressColor( possiblePlayer.PlayerNumber );
        ScoringProgressIndicator.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        var possiblePlayer = coll.gameObject.GetComponent<Player>();
        if (possiblePlayer == null || _scoringPlayer != possiblePlayer ) return;
        _scoringPlayer = null;
        ResetScoreBar();
        ScoringProgressIndicator.SetActive(false);
    }

    void OnTriggerStay2D( Collider2D coll )
    {
        if ( _scoringPlayer != null ) return;
        var possiblePlayer = coll.gameObject.GetComponent<Player>();
        if ( possiblePlayer == null ) return;
        _scoringPlayer = possiblePlayer;
        SetProgressColor(possiblePlayer.PlayerNumber);
        ScoringProgressIndicator.SetActive(true);
    }

    private void ResetScoreBar()
    {
        _scoringTimer = TimeToScore;
        ProgressBar.transform.localScale =
            new Vector3(0, ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z);
    }

    private void SetProgressColor( int playerNumber )
    {
        Color playerColor;
        switch (playerNumber)
        {
            case 1:
                playerColor = new Color32(215, 74, 74, 255);
                break;
            case 2:
                playerColor = new Color32(95, 101, 234, 255);
                break;
            case 3:
                playerColor = new Color32(251, 240, 151, 255);
                break;
            case 4:
                playerColor = new Color32(110, 246, 123, 255);
                break;
            default:
                playerColor = Color.white;
                break;
        }
        ProgressBar.GetComponent<SpriteRenderer>().color = playerColor;
    }
}
