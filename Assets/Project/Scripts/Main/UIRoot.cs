using Scripts.Screens;
using UnityEngine;

namespace Scripts.Main
{
    public class UIRoot : MonoBehaviour
    {
        [Header("Screen settings")]
        [SerializeField] private GamePlayScreen _gamePlayScreen;
        [SerializeField] private GamePauseScreen _gamePauseScreen;
        [SerializeField] private GameOverScreen _gameOverScreen;
        [SerializeField] private GameManualScreen _gameManualScreen;


        public GamePlayScreen gamePlayScreen => _gamePlayScreen;
        public GamePauseScreen gamePauseScreen => _gamePauseScreen;
        public GameOverScreen gameOverScreen => _gameOverScreen;
        public GameManualScreen gameManualScreen => _gameManualScreen;


        public void HideAllScreens()
        {
            _gamePlayScreen.gameObject.SetActive(false);
            _gamePauseScreen.gameObject.SetActive(false);
            _gameOverScreen.gameObject.SetActive(false);
            _gameManualScreen.gameObject.SetActive(false);
        }
    }
}