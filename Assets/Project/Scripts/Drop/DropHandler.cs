using UnityEngine;

namespace Scripts.Drop
{
    public class DropHandler : MonoBehaviour
    {
        [SerializeField] private uint _scoreValue;

        public uint scoreValue => _scoreValue;

        void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}