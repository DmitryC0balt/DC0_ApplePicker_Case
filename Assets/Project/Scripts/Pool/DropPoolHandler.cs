using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.Pool
{
    public class DropPoolHandler : MonoBehaviour, IInitialization
    {
        [Header("Pool settings")]
        [SerializeField] private PoolableObject _prefab;
        [SerializeField] private Transform _container;
        [SerializeField] private uint _capacity;
        [SerializeField] private bool _isExpandable;

        public PoolService<PoolableObject> poolService { get; private set; }

        public void OnInitialization()
        {
            var capacity = (int)_capacity;
            poolService = new(_prefab, capacity, _container, _isExpandable);
        }
    }
}