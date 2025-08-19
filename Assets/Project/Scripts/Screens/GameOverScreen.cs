using TMPro;
using UnityEngine;

namespace Scripts.Screens
{
    public class GameOverScreen : UIScreen
    {
        [SerializeField] private TMP_Text _totalScoreCounter;
        [SerializeField] private TMP_Text _highScoreCounter;

        public TMP_Text totalScoreCounter => _totalScoreCounter;
        public TMP_Text highScoreCounter => _highScoreCounter;
    }
}