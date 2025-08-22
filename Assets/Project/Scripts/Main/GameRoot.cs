using Scripts.Floor;
using Scripts.Player;
using Scripts.Pool;
using Scripts.Spawner;
using UnityEngine;

namespace Scripts.Main
{
    public sealed class GameRoot : MonoBehaviour
    {
        [Header("Manual game objects")]
        [SerializeField] private SpawnerHandler _spawnerHandler;
        [SerializeField] private PlayerHandler _playerHandler;
        [SerializeField] private FloorHandler _floorHandler;
        [Space(20)]


        [Header("UI screen settings")]
        [SerializeField] private UIRoot _uiRoot;

        private StatCounter _statCounter;
        private GameStateMashine _gameStateMashine;
        private MonoBehaviourCasher _monoBehaviourCasher;
        private DropPoolHandler _dropPoolHandler;


        private void Start()
        {
            _statCounter = new();

            _gameStateMashine = new(_uiRoot, _statCounter);

            _dropPoolHandler = GetComponent<DropPoolHandler>();

            _playerHandler.SetStatCounter(_statCounter);
            _floorHandler.SetStatCounter(_statCounter);

            SetGameEvents();
            SetGameManual();

            _monoBehaviourCasher = new();
            _monoBehaviourCasher.AddListener(_dropPoolHandler);
            _monoBehaviourCasher.AddListener(_playerHandler);
            _monoBehaviourCasher.AddListener(_spawnerHandler);
            _monoBehaviourCasher.OnInitialization();

            _spawnerHandler.SetPool(_dropPoolHandler.poolService);
        }


        private void Update()
        {
            PauseLogic();

            if (_gameStateMashine.isPause) return;

            _monoBehaviourCasher.OnProcess();
        }
         

        #region CASH_LOGIC

        private void FixedUpdate() => _monoBehaviourCasher.OnFixedProcess();
        
        private void LateUpdate() => _monoBehaviourCasher.OnPostProcess();

        #endregion



        public void SetGamePlay() => _gameStateMashine.SetGameState();

        public void SetGameManual() => _gameStateMashine.SetManualState();

        private void OnDestroy() => ResetGameEvents();

        public void QuitGame() => Application.Quit();


        private void PauseLogic()
        {
            if (_gameStateMashine.CurrentGameState == GameStateEnum.MANUAL) return; 

            if (Input.GetKeyDown(KeyCode.Escape))
            {

            }
        }


        private void PoolInitialization()
        {

        }


        private void SetGameEvents()
        {
            _statCounter.HealthChangingEvent += _uiRoot.gamePlayScreen.SetHealth;
            _statCounter.ScoreChangingEvent += _uiRoot.gamePlayScreen.SetScore;
            _statCounter.OutOfHealthEvent += _gameStateMashine.SetGameOverState;
        }


        private void ResetGameEvents()
        {
            _statCounter.HealthChangingEvent -= _uiRoot.gamePlayScreen.SetHealth;
            _statCounter.ScoreChangingEvent -= _uiRoot.gamePlayScreen.SetScore;
            _statCounter.OutOfHealthEvent -= _gameStateMashine.SetGameOverState;
        }


        public void RestartLevel()
        {
            _gameStateMashine.SetGameState();

            _statCounter.ResetStats(3);

            _playerHandler.ResetPosition();
            _spawnerHandler.ResetPosition();
        }


        public void SetScore(uint value) => _statCounter.ChangeScore(value);

        public void SetHealth(int value) => _statCounter.ChangeHealth(value);
        
    }
}