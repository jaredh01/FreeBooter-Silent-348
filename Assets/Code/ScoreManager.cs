using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int VictoryScore;

    private GameObject _scoreSection;
    private List<TextMeshProUGUI> _scoreTexts = new List<TextMeshProUGUI>();
    private int[] _scores = new int[4];
    private Player _winningPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _scoreSection = GameObject.FindGameObjectWithTag( "Score" );
        ActivatePlayerScores();

        Array.Clear( _scores, 0, 4 );
    }

    public void ScorePoints( int score, int playerNumber )
    {
        _scores[playerNumber - 1] += score;
        UpdateScores();
        var highestScore = 0;
        var winningPlayerNumber = 0;
        if (_scores[playerNumber - 1] >= VictoryScore)
        {
            FindObjectOfType<GameManager>().GameOver();
        }

        foreach ( var s in _scores )
        {
            if ( s > highestScore )
            {
                highestScore = s;
                winningPlayerNumber = _scores.ToList().IndexOf( highestScore ) + 1;
            }
            else if ( s == highestScore )
            {
                winningPlayerNumber = 0;
            }
        }

        if (_winningPlayer != null && winningPlayerNumber != _winningPlayer.PlayerNumber )
        {
            _winningPlayer.transform.Find( "Crown" ).gameObject.SetActive( false );
        }

        if ( winningPlayerNumber == 0 ) return;
        _winningPlayer = GameObject.Find( "Player" + winningPlayerNumber ).GetComponent<Player>();
        _winningPlayer.transform.Find( "Crown" ).gameObject.SetActive( true );
    }

    /// <summary>
    /// Return the winning player, or null if a tie
    /// </summary>
    public Player ReturnWinner()
    {
        var winnerNum = 0;
        var bestScore = 0;
        for (var i = 0; i < GameConfig.NumberOfPlayers; i++)
        {
            var score = _scores[i];
            if ( bestScore < score )
            {
                bestScore = _scores[i];
                winnerNum = i + 1;
            }
            else if ( bestScore == score )
            {
                winnerNum = 0;
            }
        }

        return winnerNum == 0 ? null : GameObject.Find( "Player" + winnerNum ).GetComponent<Player>();
    }

    private void UpdateScores()
    {
        for ( var i = 0; i < GameConfig.NumberOfPlayers; i++ )
        {
            _scoreTexts[i].text = _scores[i].ToString();
        }
    }

    private void ActivatePlayerScores()
    {
        foreach ( Transform section in _scoreSection.transform )
        {
            var sectionNumber = section.gameObject.name[section.gameObject.name.Length - 1] - '0';
            if ( sectionNumber <= GameConfig.NumberOfPlayers )
            {
                _scoreTexts.Add( GameObject.Find("Player " + sectionNumber + " Score Text").GetComponent<TextMeshProUGUI>() );
            }
            else
            {
                section.gameObject.SetActive( false );
            }
        }
    }
}
