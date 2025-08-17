using System;
using UnityEngine;

namespace Scripts.Main
{
    public class StatCounter
    {
        public event Action ScoreChangingEvent;
        public event Action HealthChangingEvent;
        public event Action OutOfHealthEvent;


        public int currentHealth { get; private set; }
        public int currentScore { get; private set; }


        public void RestartPreset(uint value)
        {
            currentScore = 0;
            currentHealth = (int)value;
            currentHealth = Mathf.Clamp(currentHealth, 0, 5);
        }


        public void ChangeHealth(int value)
        {
            currentHealth += value;
            HealthChangingEvent?.Invoke();

            if (currentHealth == 0)
            {
                OutOfHealthEvent?.Invoke();
            }
        }


        public void ChangeScore(uint value)
        {
            currentScore += (int)value;

            ScoreChangingEvent?.Invoke();
        }

    }
}