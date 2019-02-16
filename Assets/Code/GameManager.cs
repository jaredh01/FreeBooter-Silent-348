using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Time = UnityEngine.Time;

public class GameManager : MonoBehaviour
{
    public float RoundLength;
    public static bool GamePaused = false;

    private TextMeshProUGUI _statusText;
    private TextMeshProUGUI _timeText;
    private Camera _camera;
    private float _timeRemaining;
    private bool _gameOver = false;
    private float _startGameDelay = 5f;


    // Currently, start a round immediately
    private void Start()
    {
        Time.timeScale = 0;
        _camera = FindObjectOfType<Camera>();
        SetGameTimer();
        SetActivePlayers();
        StartCoroutine(StartGameAnimation());
    }

    private void Update()
    {
        if ( _startGameDelay > 0 ) return;
        if ( Input.GetKeyDown( KeyCode.JoystickButton7 ) )
        {
            if (_gameOver)
            {
                GameRestart();
            }
            else if ( GamePaused )
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
        _statusText = GameObject.FindGameObjectWithTag("Status").GetComponent<TextMeshProUGUI>();
        _timeText = GameObject.FindGameObjectWithTag("Time").GetComponent<TextMeshProUGUI>();
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
    /// A little animation on starting the game
    /// </summary>
    private IEnumerator StartGameAnimation()
    {
        while ( _startGameDelay > 0f )
        {
            _startGameDelay -= Time.unscaledDeltaTime;
            _statusText.text = string.Format( "Starting in: {0}", Mathf.Floor( _startGameDelay ) );

            if ( _startGameDelay > 4f  || _startGameDelay < 1f ) yield return null;
            var slerpVar = ( 4f - _startGameDelay ) / 3f;
            var originPoint = _camera.ViewportToScreenPoint( new Vector3( 0.5f, 0.5f) );
            var destPoint = _camera.ViewportToScreenPoint( new Vector3( 0.8f, 0.8f ) );
            _timeText.transform.position =  Vector3.Slerp( originPoint, destPoint, slerpVar  );
            yield return null;
        }

        _statusText.text = "";
        Time.timeScale = 1;

        while ( _startGameDelay > -0.7f )
        {
            _startGameDelay -= Time.fixedDeltaTime;
            _statusText.text = "GO!";
            yield return null;
        }

        _statusText.text = "";
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
