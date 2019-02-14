using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private List<Text> _scoreTexts;
    private int[] _scores = new int[4];

    // Start is called before the first frame update
    void Start()
    {
        _scoreTexts.Add( GameObject.Find( "Score1Text" ).GetComponent<Text>() );
        _scoreTexts.Add( GameObject.Find( "Score2Text" ).GetComponent<Text>() );
        _scoreTexts.Add( GameObject.Find( "Score3Text" ).GetComponent<Text>() );
        _scoreTexts.Add( GameObject.Find( "Score4Text" ).GetComponent<Text>() );
        Array.Clear(_scores, 0, 4);
    }

    public void ScorePoints( int score, int playerNumber )
    {
        _scores[playerNumber] += score;
        UpdateScores();
    }

    private void UpdateScores()
    {
        for ( var i = 0; i < 4; i++ )
        {
            _scoreTexts[i].text = "Score: " + _scores[i];
        }
    }
}
