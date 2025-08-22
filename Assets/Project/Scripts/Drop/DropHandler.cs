using Scripts.Pool;
using UnityEngine;

namespace Scripts.Drop
{
    public class DropHandler : PoolableObject
    {
        [SerializeField] private uint _scoreValue;

        public uint scoreValue => _scoreValue;

        public override void ShutDown()
        {
            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.linearVelocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;

            base.ShutDown();
        }
    }
}