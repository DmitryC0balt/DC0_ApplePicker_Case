using Scripts.Floor;
using Scripts.Player;
using Scripts.Spawner;
using TMPro;
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

        [Header("GamePlay UI sets")]
        [SerializeField] private RectTransform _gamePlayScreen;
        [SerializeField] private TMP_Text _healthCount;
        [SerializeField] private TMP_Text _scoreCount;
        [Space(20)]

        [Header("GameOver UI sets")]
        [SerializeField] private RectTransform _gameOverScreen;
        [SerializeField] private TMP_Text _totalScore;
        [SerializeField] private TMP_Text _bestScore;


        private Transform _playerStartPosition;
        private Transform _spawnerStartPosition;

        private StatCounter _statCounter;
        private int _maxScore;

        //ОСТОРОЖНО!!! Неотрефакторенный код!
        private void Start()
        {
            _gamePlayScreen.gameObject.SetActive(true);
            _gameOverScreen.gameObject.SetActive(false);

            _playerStartPosition = _playerHandler.gameObject.transform;
            _spawnerStartPosition = _spawnerHandler.gameObject.transform;

            _statCounter = new StatCounter();
            _statCounter.RestartPreset(3);

            _playerHandler.Initialization(_statCounter);
            _floorHandler.Initialization(_statCounter);

            _statCounter.HealthChangingEvent += ChangeHealthValue;
            _statCounter.ScoreChangingEvent += ChangeScoreValue;
            _statCounter.OutOfHealthEvent += SetGameOver;

            ChangeHealthValue();
            ChangeScoreValue();
        }


        private void OnDestroy()
        {
            _statCounter.HealthChangingEvent -= ChangeHealthValue;
            _statCounter.ScoreChangingEvent -= ChangeScoreValue;
            _statCounter.OutOfHealthEvent -= SetGameOver;
        }


        private void ChangeHealthValue() => _healthCount.text = $"X{_statCounter.currentHealth}";

        private void ChangeScoreValue() => _scoreCount.text = $"{_statCounter.currentScore}";


        public void RestartLevel()
        {
            Debug.Log("Restart button pressed");

            _gamePlayScreen.gameObject.SetActive(true);
            _gameOverScreen.gameObject.SetActive(false);

            ResetPosition();

            _statCounter.RestartPreset(3);
            ChangeHealthValue();
            ChangeScoreValue();

            Time.timeScale = 1;
        }


        private void ResetPosition()
        {
            _playerHandler.transform.position = _playerStartPosition.position;
            _spawnerHandler.transform.position = _spawnerStartPosition.position;
        }


        private void SetGameOver()
        {
            Time.timeScale = 0;

            _gamePlayScreen.gameObject.SetActive(false);
            _gameOverScreen.gameObject.SetActive(true);

            _totalScore.text = $"{_statCounter.currentScore}";

            if (_maxScore < _statCounter.currentScore)
            {
                _maxScore = _statCounter.currentScore;
            }

            _bestScore.text = $"{_maxScore}";
        }
    }
}