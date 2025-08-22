using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Pool
{
    public class PoolService<Formular> where Formular : PoolableObject
    {
        private Formular _formular;
        private int _capacity;
        private Transform _container;
        private bool _isExpandable;


        private List<Formular> _formularList;


        public PoolService(Formular prefab, int capacity, Transform container, bool isExpandable)
        {
            _formular = prefab;
            _capacity = capacity;
            _container = container;
            _isExpandable = isExpandable;

            CreatePool();
        }


        private void CreatePool()
        {
            _formularList = new();

            for (int i = 0; i < _capacity; i++)
            {
                CreateFormular();
            }
        }


        private Formular CreateFormular(bool isActive = false)
        {
            var createdFormular = Object.Instantiate(_formular, _container);
            createdFormular.gameObject.SetActive(isActive);
            _formularList.Add(createdFormular);
            return createdFormular;
        }


        private void RemoveFormular(Formular formular)
        {
            _formularList.Remove(formular);
            Object.Destroy(formular.gameObject);
        }


        private bool TryGetFormular(out Formular formular)
        {
            foreach (var prefab in _formularList)
            {
                if (!prefab.gameObject.activeInHierarchy)
                {
                    prefab.gameObject.SetActive(true);
                    formular = prefab;
                    return true;
                }
            }

            formular = null;
            return false;
        }


        public Formular GetFormular()
        {
            if (TryGetFormular(out var formular))
            {
                return formular;
            }

            if (_isExpandable)
            {
                return CreateFormular();
            }

            throw new System.Exception($"No more {_formular.name} in pool!");
        }


        private void CheckFormularList()
        {

        }
    }
}