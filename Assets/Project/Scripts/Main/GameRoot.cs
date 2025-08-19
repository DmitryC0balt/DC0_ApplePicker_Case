using Scripts.Floor;
using Scripts.Player;
using Scripts.Screens;
using Scripts.Spawner;
using UnityEngine;

namespace Scripts.Main
{
    public class GameRoot : MonoBehaviour
    {
        [Header("Manual game objects")]
        [SerializeField] private SpawnerHandler _spawnerHandler;
        [SerializeField] private PlayerHandler _playerHandler;
        [SerializeField] private FloorHandler _floorHandler;
        [Space(20)]


        [Header("UI screen settings")]
        [SerializeField] private GamePlayScreen _gamePlayScreen;
        [SerializeField] private GamePauseScreen _gamePauseScreen;
        [SerializeField] private GameOverScreen _gameOverScreen;
        [SerializeField] private GameManualScreen _gameManualScreen;


        private Vector3 _playerHandlerStartPosition;
        private Vector3 _spawnerHandlerStartPosition;


        private StatCounter _statCounter;
        private int _maxScore;


        private GameStateEnum _gameStateEnum;

        private bool _pause;


        private void Start()
        {
            SetStartPosition();
            HideAllScreens();

            _statCounter = new();
            _statCounter.HealthChangingEvent += ShowHealth;
            _statCounter.ScoreChangingEvent += ShowScore;
            _statCounter.OutOfHealthEvent += SetGameOver;

            _playerHandler.Initialization(_statCounter);
            _floorHandler.Initialization(_statCounter);

            SetManual();
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_gameStateEnum == GameStateEnum.MANUAL) return;

                _pause = !_pause;

                if (_pause)
                {
                    SetPause();
                    return;
                }

                SetPlay();
            }
        }


        private void FixedUpdate()
        {
            _spawnerHandler.Process();
            _playerHandler.Process();
        }


        private void OnDestroy()
        {
            _statCounter.HealthChangingEvent -= ShowHealth;
            _statCounter.ScoreChangingEvent -= ShowScore;
            _statCounter.OutOfHealthEvent -= SetGameOver;
        }


        private void HideAllScreens()
        {
            _gamePlayScreen.gameObject.SetActive(false);
            _gamePauseScreen.gameObject.SetActive(false);
            _gameOverScreen.gameObject.SetActive(false);
            _gameManualScreen.gameObject.SetActive(false);
        }


        public void RestartLevel()
        {
            _statCounter.SetDefaults(3);

            ShowScore();
            ShowHealth();

            ResetPosition();
            SetPlay();
        }


        private void SetStartPosition()
        {
            _playerHandlerStartPosition = _playerHandler.transform.position;
            _spawnerHandlerStartPosition = _spawnerHandler.transform.position;
        }


        private void ResetPosition()
        {
            _playerHandler.transform.position = _playerHandlerStartPosition;
            _spawnerHandler.transform.position = _spawnerHandlerStartPosition;
        }


        public void SetScore(uint value) => _statCounter.ChangeScore(value);

        public void SetHealth(int value) => _statCounter.ChangeHealth(value);

        public void ShowScore() => _gamePlayScreen.scoreCounter.text = $"{_statCounter.currentScore}";

        public void ShowHealth() => _gamePlayScreen.healthCounter.text = $"{_statCounter.currentHealth}";

        private void ShowMouse(bool isActive) => Cursor.visible = isActive;

        public void SetPlayButton() => SetPlay();


        private void CheckScore()
        {
            if (_maxScore < _statCounter.currentScore)
            {
                _maxScore = _statCounter.currentScore;
            }
        }


        private void SetPlay()
        {
            _gameStateEnum = GameStateEnum.GAMEPLAY;

            HideAllScreens();
            Time.timeScale = 1;

            _gamePlayScreen.gameObject.SetActive(true);
            ShowMouse(false);
        }


        private void SetPause()
        {
            _gameStateEnum = GameStateEnum.PAUSE;

            HideAllScreens();
            Time.timeScale = 0;

            _gamePauseScreen.gameObject.SetActive(true);
            ShowMouse(true);
        }


        private void SetManual()
        {
            _gameStateEnum = GameStateEnum.MANUAL;

            HideAllScreens();
            Time.timeScale = 0;

            _gameManualScreen.gameObject.SetActive(true);
            ShowMouse(true);
        }


        private void SetGameOver()
        {
            _gameStateEnum = GameStateEnum.GAMEOVER;

            HideAllScreens();
            Time.timeScale = 0;

            CheckScore();
            _gameOverScreen.gameObject.SetActive(true);

            _gameOverScreen.totalScoreCounter.text = $"{_statCounter.currentScore}";
            _gameOverScreen.highScoreCounter.text = $"{_maxScore}";

            ShowMouse(true);
        }


        public enum GameStateEnum
        {
            MANUAL,
            GAMEPLAY,
            PAUSE,
            GAMEOVER
        }
    }
}