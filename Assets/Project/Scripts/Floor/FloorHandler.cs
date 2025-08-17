using Scripts.Main;
using UnityEngine;

namespace Scripts.Floor
{
    public class FloorHandler : MonoBehaviour
    {
        private StatCounter _statCounter;

        public void Initialization(StatCounter statCounter) => _statCounter = statCounter;

        void OnCollisionEnter(Collision collision)
        {
            _statCounter.ChangeHealth(-1);
            Destroy(collision.gameObject);
        }
    }
}