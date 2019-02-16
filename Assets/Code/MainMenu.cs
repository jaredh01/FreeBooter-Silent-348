using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Code
{
    public class MainMenu : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var startButtons = GameObject.Find( "Choices/StartButtons" );
            foreach ( Transform buttonObject in startButtons.transform )
            {
                var playerCount = buttonObject.gameObject.name[buttonObject.gameObject.name.Length - 2] - '0';
                buttonObject.gameObject.GetComponent<Button>().onClick.AddListener( () => StartGame( playerCount ) );
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void StartGame( int playerCount )
        {
            GameConfig.NumberOfPlayers = playerCount;
            SceneManager.LoadScene( "MainScene" );
        }
    }
}
