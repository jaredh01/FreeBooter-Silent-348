﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Time = UnityEngine.Time;

public class GameManager : MonoBehaviour
{
    public float RoundLength;
    public static bool GamePaused = false;
    public static bool GameIsOver = false;


    private Text _statusText;
    private Text _timeText;
    private float _timeRemaining;


    // Currently, start a round immediately
    private void Start()
    {
        SetGameTimer();
        SetActivePlayers();
    }

    private void Update()
    {
        if ( Input.GetKeyDown( KeyCode.JoystickButton7 ) )
        {
            if ( GameIsOver )
            {
                GameRestart();
            }
            else if (GamePaused)
            {
                GameUnpause();
            }
            else
            {
                GamePause();
            }
        }
    }

    private void FixedUpdate()
    {
        _timeRemaining -= Time.fixedDeltaTime;
        _timeText.text = "Time Left: " + string.Format("{0}:{1:00}", (int)_timeRemaining / 60, (int)_timeRemaining % 60);
        if ( _timeRemaining < 0 ) GameOver();
    }

    /// <summary>
    /// Helper function for configuring the game timer
    /// </summary>
    private void SetGameTimer()
    {
        _timeRemaining = RoundLength;
        _statusText = GameObject.FindGameObjectWithTag("Status").GetComponent<Text>();
        _timeText = GameObject.FindGameObjectWithTag("Time").GetComponent<Text>();
        _timeText.text = "Time Left: " + string.Format("{0}:{1:00}", (int)_timeRemaining / 60, (int)_timeRemaining % 60);
    }

    /// <summary>
    /// Deactivate the game object of players that shouldn't be active
    /// </summary>
    private void SetActivePlayers()
    {
        foreach ( var player in FindObjectsOfType<Player>() )
        {
            if (player.PlayerNumber > GameConfig.NumberOfPlayers ) player.gameObject.SetActive( false );
        }
    }

    /// <summary>
    /// Freezes the game when time runs out
    /// </summary>
    private void GameOver()
    {
        Time.timeScale = 0;
        GameIsOver = true;
        _statusText.text = "Game Over! \nPress Start to Restart";
    }

    /// <summary>
    /// Restart the game.
    /// </summary>
    private void GameRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene( "MainScene" );
    }

    /// <summary>
    /// Pause the game and display pause text
    /// </summary>
    private void GamePause()
    {
        Time.timeScale = 0;
        GamePaused = true;
        _statusText.text = "Game Paused";
    }

    /// <summary>
    /// Unpause the game
    /// </summary>
    private void GameUnpause()
    {
        Time.timeScale = 1;
        GamePaused = false;
        _statusText.text = "";
    }
}
