using TMPro;
using UnityEngine;

namespace Scripts.Screens
{
    public class GamePlayScreen : UIScreen
    {
        [SerializeField] private TMP_Text _scoreCounter;
        [SerializeField] private TMP_Text _healthCounter;


        public void SetScore(int value) => _scoreCounter.text = $"{value}";
        public void SetHealth(int value) => _healthCounter.text = $"{value}";
    }
}