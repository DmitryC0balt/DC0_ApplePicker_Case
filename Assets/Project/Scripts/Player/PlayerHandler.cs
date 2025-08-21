using Scripts.Drop;
using Scripts.Interfaces;
using Scripts.Main;
using UnityEngine;

namespace Scripts.Player
{
    public sealed class PlayerHandler : MonoBehaviour, IInitialization, IFixedProcess
    {
        [Header("Player settings")]
        [SerializeField] private float _maxDistance;

        private StatCounter _statCounter;

        private Vector3 _startPosition;

        public void SetStatCounter(StatCounter statCounter) => _statCounter = statCounter;

        public void OnInitialization() => _startPosition = gameObject.transform.position;

        public void ResetPosition() => gameObject.transform.position = _startPosition;


        public void OnFixedProcess()
        {
            Move();
        }


        private void Move()
        {
            var screenMousePosition = Input.mousePosition;

            screenMousePosition.z = -Camera.main.transform.position.z;

            var sceneMousePosition = Camera.main.ScreenToWorldPoint(screenMousePosition);

            sceneMousePosition.x = Mathf.Clamp(sceneMousePosition.x, -_maxDistance, _maxDistance);

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