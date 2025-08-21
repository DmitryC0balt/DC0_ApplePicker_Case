using UnityEngine;

namespace Scripts.Main
{
    public class GameStateMashine
    {
        private UIRoot _uiRoot;
        private StatCounter _statCounter;


        public GameStateEnum CurrentGameState { get; private set; }
        public bool isPause { get; private set; }


        public GameStateMashine(UIRoot uiRoot, StatCounter statCounter)
        {
            _uiRoot = uiRoot;
            _statCounter = statCounter;
        }


        private void ResetState(int timeScale, bool showMouse)
        {
            _uiRoot.HideAllScreens();

            Time.timeScale = timeScale;

            Cursor.visible = showMouse;
        }


        public void SetGameState()
        {
            ResetState(1, false);

            isPause = false;

            _uiRoot.gamePlayScreen.gameObject.SetActive(true);

            CurrentGameState = GameStateEnum.GAMEPLAY;
        }


        public void SetPauseState()
        {
            if (CurrentGameState != GameStateEnum.GAMEPLAY) return;

            ResetState(0, true);

            isPause = true;

            _uiRoot.gamePauseScreen.gameObject.SetActive(true);

            CurrentGameState = GameStateEnum.PAUSE;
        }


        public void SetGameOverState()
        {
            if (CurrentGameState != GameStateEnum.GAMEPLAY) return;

            ResetState(0, true);

            isPause = true;

            _uiRoot.gameOverScreen.gameObject.SetActive(true);

            _uiRoot.gameOverScreen.totalScoreCounter.text = $"{_statCounter.currentScore}";

            _uiRoot.gameOverScreen.highScoreCounter.text = $"{_statCounter.bestScore}";

            CurrentGameState = GameStateEnum.GAMEOVER;
        }


        public void SetManualState()
        {
            ResetState(0, true);

            isPause = true;

            _uiRoot.gameManualScreen.gameObject.SetActive(true);

            CurrentGameState = GameStateEnum.MANUAL;
        }
    }
    


    public enum GameStateEnum
    {
        MANUAL,
        GAMEPLAY,
        PAUSE,
        GAMEOVER
    }
}