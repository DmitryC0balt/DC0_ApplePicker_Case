using System;
using UnityEngine;

namespace Scripts.Main
{
    public class StatCounter
    {
        public event Action<int> ScoreChangingEvent;
        public event Action<int> HealthChangingEvent;
        public event Action OutOfHealthEvent;


        public int currentHealth { get; private set; }
        public int currentScore { get; private set; }
        public int bestScore { get; private set; }


        public void ResetStats(uint healthValue)
        {
            currentScore = 0;
            currentHealth = (int)healthValue;
            currentHealth = Mathf.Clamp(currentHealth, 0, 5);

            ScoreChangingEvent?.Invoke(currentScore);
            HealthChangingEvent?.Invoke(currentHealth);
        }


        public void ChangeHealth(int value)
        {
            currentHealth += value;
            HealthChangingEvent?.Invoke(currentHealth);

            if (currentHealth == 0)
            {
                OutOfHealthEvent?.Invoke();
            }
        }


        public void ChangeScore(uint value)
        {
            currentScore += (int)value;

            CheckScore();

            ScoreChangingEvent?.Invoke(currentScore);
        }


        private void CheckScore()
        {
            if (bestScore < currentScore)
            {
                bestScore = currentScore;
            }
        }

    }
}