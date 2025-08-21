using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.Spawner
{
    public class SpawnerHandler : MonoBehaviour, IProcess, IInitialization
    {
        [Header("Manual settings")]
        [SerializeField] private GameObject _dropPrefab;
        [SerializeField] private float _speed;
        [SerializeField] private float _distance;
        [SerializeField, Range(0, 1f)] private float _directionChangingRate;
        [SerializeField] private float _prefabDropTime;
        [SerializeField] private Transform _spawnPoint;


        private Vector3 _startPosition;


        public void ResetPosition() => gameObject.transform.position = _startPosition;


        public void OnInitialization()
        {
            _startPosition = gameObject.transform.position;
            Invoke("DropPrefab", 2f);
        }


        public void OnProcess()
        {
            Move();
            ChangeDirection();
            ChangeDirectionRandomly();
        }


        private void Move()
        {
            var position = transform.position;
            position.x += _speed * Time.deltaTime;
            transform.position = position;
        }


        private void ChangeDirection()
        {
            if (transform.position.x < -_distance)
            {
                _speed = Mathf.Abs(_speed);
            }

            if (transform.position.x > _distance)
            {
                _speed = -Mathf.Abs(_speed);
            }
        }


        private void ChangeDirectionRandomly()
        {
            if (Random.value < _directionChangingRate)
            {
                _speed *= -1;
            }
        }


        private void DropPrefab()
        {
            var prefab = Instantiate<GameObject>(_dropPrefab);
            prefab.transform.position = _spawnPoint.position;
            Invoke("DropPrefab", _prefabDropTime);
        }
    }
}