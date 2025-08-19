using Scripts.Drop;
using Scripts.Interfaces;
using Scripts.Main;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerHandler : MonoBehaviour, IProcessable
    {
        private StatCounter _statCounter;

        public void Initialization(StatCounter statCounter) => _statCounter = statCounter;


        public void Process()
        {
            Move();
        }


        private void Move()
        {
            var screenMousePosition = Input.mousePosition;

            screenMousePosition.z = -Camera.main.transform.position.z;

            var sceneMousePosition = Camera.main.ScreenToWorldPoint(screenMousePosition);

            var position = transform.position;

            position.x = sceneMousePosition.x;

            transform.position = position;
        }



        void OnCollisionEnter(Collision collision)
        {
            var scoreValue = collision.gameObject.GetComponent<DropHandler>().scoreValue;

            _statCounter.ChangeScore(scoreValue);

            Destroy(collision.gameObject);            
        }

    }
}