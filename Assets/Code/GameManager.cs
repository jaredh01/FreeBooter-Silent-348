using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Time = UnityEngine.Time;

public class GameManager : MonoBehaviour
{
    public float RoundLength;

    private Text _statusText;
    private Text _timeText;
    private float _timeRemaining;
    private bool _gameOver = false;
    private bool _gamePaused = false;

    // Currently, start a round immediately
    private void Start()
    {
        _timeRemaining = RoundLength;
        _statusText = GameObject.FindGameObjectWithTag( "Status" ).GetComponent<Text>();
        _timeText = GameObject.FindGameObjectWithTag( "Time" ).GetComponent<Text>();

        _timeText.text = "Time Left: " + string.Format( "{0}:{1:00}", (int) _timeRemaining / 60, (int) _timeRemaining % 60 );
    }

    private void Update()
    {
        if ( Input.GetKeyDown( KeyCode.JoystickButton7 ) )
        {
            if (_gameOver)
            {
                GameRestart();
            }
            else if (_gamePaused)
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
    /// Freezes the game when time runs out
    /// </summary>
    private void GameOver()
    {
        Time.timeScale = 0;
        _gameOver = true;
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
        _gamePaused = true;
        _statusText.text = "Game Paused";
    }

    /// <summary>
    /// Unpause the game
    /// </summary>
    private void GameUnpause()
    {
        Time.timeScale = 1;
        _gamePaused = false;
        _statusText.text = "";
    }
}
