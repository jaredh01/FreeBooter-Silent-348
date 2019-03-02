using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Time = UnityEngine.Time;

public class GameManager : MonoBehaviour
{
    public static bool GamePaused = false;
    public static bool GameIsOver = false;
    public GameObject PauseMenu;
    public GameObject VictoryMenu;
    public GameObject StartMenu;
    public GameObject StartCountdown;
    public TextMeshProUGUI VictoryNumberText;
    public TextMeshProUGUI StartNumberText;
    public TextMeshProUGUI StartStartText;

    private float _startGameDelay = 5f;


    // Currently, start a round immediately
    private void Start()
    {
        Time.timeScale = 0;
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
        StartMenu.SetActive( true );
        while ( _startGameDelay > 0f )
        {
            StartNumberText.text = string.Format("{0}", (int) _startGameDelay + 1);
            _startGameDelay -= Time.unscaledDeltaTime;
            yield return null;
        }

        StartCountdown.SetActive( false );
        StartStartText.gameObject.SetActive( true );
        Time.timeScale = 1;
        GamePaused = false;

        while ( _startGameDelay > -1.5f )
        {
            _startGameDelay -= Time.fixedDeltaTime;
            yield return null;
        }

        StartMenu.SetActive( false );
    }

    /// <summary>
    /// Freezes the game when time runs out
    /// </summary>
    public void GameOver()
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
