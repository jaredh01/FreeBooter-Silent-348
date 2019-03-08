using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Code
{
    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// Attach listeners to Menu buttons
        /// </summary>
        void Start()
        {
        }


        /// <summary>
        /// Start the game, setting the config to match the selected number of players
        /// </summary>
        public void StartGame( int playerCount )
        {
            GameConfig.NumberOfPlayers = playerCount;
            SceneManager.LoadScene( "MainScene" );
        }

        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
