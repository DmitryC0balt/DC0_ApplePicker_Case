using TMPro;
using UnityEngine;

namespace Scripts.Screens
{
    public class GamePlayScreen : UIScreen
    {
        [SerializeField] private TMP_Text _scoreCounter;
        [SerializeField] private TMP_Text _healthCounter;


        public TMP_Text scoreCounter => _scoreCounter;
        public TMP_Text healthCounter => _healthCounter;
    }
}