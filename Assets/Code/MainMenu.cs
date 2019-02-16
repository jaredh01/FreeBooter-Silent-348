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
            var startButtons = GameObject.Find( "Choices/StartButtons" );
            foreach ( Transform buttonObject in startButtons.transform )
            {
                var playerCount = buttonObject.gameObject.name[buttonObject.gameObject.name.Length - 2] - '0';
                buttonObject.gameObject.GetComponent<Button>().onClick.AddListener( () => StartGame( playerCount ) );
            }
        }


        /// <summary>
        /// Start the game, setting the config to match the selected number of players
        /// </summary>
        private void StartGame( int playerCount )
        {
            GameConfig.NumberOfPlayers = playerCount;
            SceneManager.LoadScene( "MainScene" );
        }

        private void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
