using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private GameObject _scoreSection;
    private List<TextMeshProUGUI> _scoreTexts = new List<TextMeshProUGUI>();
    private int[] _scores = new int[4];

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
    }

    private void UpdateScores()
    {
        for ( var i = 0; i < GameConfig.NumberOfPlayers; i++ )
        {
            _scoreTexts[i].text = "Score: " + _scores[i];
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
