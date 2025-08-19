using TMPro;
using UnityEngine;

namespace Scripts.Screens
{
    public class GamePauseScreen : UIScreen
    {
        [SerializeField] private TMP_Text _currentScore;

        public TMP_Text currentScore => _currentScore;
    }
}