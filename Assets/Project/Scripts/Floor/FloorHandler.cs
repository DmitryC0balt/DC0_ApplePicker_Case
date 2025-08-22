using Scripts.Drop;
using Scripts.Main;
using UnityEngine;

namespace Scripts.Floor
{
    public class FloorHandler : MonoBehaviour
    {
        private StatCounter _statCounter;

        public void SetStatCounter(StatCounter statCounter) => _statCounter = statCounter;

        void OnCollisionEnter(Collision collision)
        {
            _statCounter.ChangeHealth(-1);

            var dropHandler = collision.gameObject.GetComponent<DropHandler>();
            dropHandler.ShutDown();
        }
    }
}