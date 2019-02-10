using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private List<Text> _scoreTexts;

    // Start is called before the first frame update
    void Start()
    {
        _scoreTexts.Add( GameObject.Find( "Score1Text" ).GetComponent<Text>() );
        _scoreTexts.Add( GameObject.Find( "Score2Text" ).GetComponent<Text>() );
        _scoreTexts.Add( GameObject.Find( "Score3Text" ).GetComponent<Text>() );
        _scoreTexts.Add( GameObject.Find( "Score4Text" ).GetComponent<Text>() );
    }

    private void ScorePoints( int score, int playerNumber )
    {
    }
}
