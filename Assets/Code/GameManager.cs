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
    public static bool GameIsOver = false;
    public GameObject PauseMenu;
    public GameObject VictoryMenu;
    public TextMeshProUGUI VictoryNumberText;


    private TextMeshProUGUI _statusText;
    private TextMeshProUGUI _timeText;
    private Camera _camera;
    private float _timeRemaining;
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
            if ( GameIsOver )
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

        if ( Input.GetKeyDown( KeyCode.JoystickButton6 ) && GameIsOver )
        {
            BackToMenu();
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
    /// A little animation on starting the game. Will need updated frequently.
    /// </summary>
    private IEnumerator StartGameAnimation()
    {
        GamePaused = true;

        while ( _startGameDelay > 0f )
        {
            _statusText.text = string.Format("Starting in: {0}", Mathf.Ceil(_startGameDelay));
            _startGameDelay -= Time.unscaledDeltaTime;


            if ( _startGameDelay > 4f  || _startGameDelay < 1f ) yield return null;
            var slerpVar = ( 4f - _startGameDelay ) / 3f;
            var originPoint = _camera.ViewportToScreenPoint( new Vector3( 0.5f, 0.5f) );
            var destPoint = _camera.ViewportToScreenPoint( new Vector3( 0.78f, 0.8f ) );
            _timeText.transform.position =  Vector3.Slerp( originPoint, destPoint, slerpVar  );
            yield return null;
        }

        _statusText.text = "";
        _statusText.fontSize = 24;
        _statusText.color = Color.red;
        Time.timeScale = 1;
        GamePaused = false;

        while ( _startGameDelay > -0.7f )
        {
            _startGameDelay -= Time.fixedDeltaTime;
            _statusText.text = "GO!";
            yield return null;
        }

        _statusText.text = "";
        _statusText.fontSize = 20;
        _statusText.color = Color.white;
    }

    /// <summary>
    /// Freezes the game when time runs out
    /// </summary>
    private void GameOver()
    {
        Time.timeScale = 0;
        GameIsOver = true;
        VictoryMenu.SetActive( true );
        var winningNumber = FindObjectOfType<ScoreManager>().ReturnWinner().PlayerNumber;
        VictoryNumberText.color = DeterminePlayerColor( winningNumber );
        VictoryNumberText.text = winningNumber.ToString();
    }

    /// <summary>
    /// Restart the game.
    /// </summary>
    public void GameRestart()
    {
        Time.timeScale = 1;
        GameIsOver = false;
        SceneManager.LoadScene( "MainScene" );
    }

    /// <summary>
    /// Pause the game and display pause text
    /// </summary>
    public void GamePause()
    {
        Time.timeScale = 0;
        GamePaused = true;
        PauseMenu.gameObject.SetActive( true );
    }

    /// <summary>
    /// Unpause the game
    /// </summary>
    public void GameUnpause()
    {
        Time.timeScale = 1;
        GamePaused = false;
        PauseMenu.gameObject.SetActive( false );
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        GameIsOver = false;
        SceneManager.LoadScene( "MainMenu" );
    }

    private Color DeterminePlayerColor( int playerNumber )
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
                playerColor = Color.black;
                break;
        }

        return playerColor;
    }
}
