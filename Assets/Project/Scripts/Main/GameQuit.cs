using UnityEngine;

namespace Scripts.Main
{
    public class GameQuit : MonoBehaviour
    {
        public void TryQuitGame()
        {
            Application.Quit();
        }
    }
}