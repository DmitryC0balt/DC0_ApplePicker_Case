using UnityEngine;

namespace Scripts.Pool
{
    public abstract class PoolableObject : MonoBehaviour
    {
        public virtual void ShutDown()
        {
            gameObject.SetActive(false);
        }
    }
}