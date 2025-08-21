using System.Collections.Generic;
using Scripts.Interfaces;


namespace Scripts.Main
{
    public class MonoBehaviourCasher
    {
        private List<IProcessable> _listenersList;

        public MonoBehaviourCasher() => _listenersList = new();

        public void AddListener(IProcessable listener) => _listenersList.Add(listener);

        public void RemoveListener(IProcessable listener) => _listenersList.Remove(listener);

        public void OnInitialization() => Initialization();

        public void OnProcess() => Process();

        public void OnFixedProcess() => FixedProcess();

        public void OnPostProcess() => PostProcess();


        private void Initialization()
        {
            foreach (var listener in _listenersList)
            {
                if (listener is IInitialization initializationListener)
                {
                    initializationListener.OnInitialization();
                }
            }
        }


        private void Process()
        {
            foreach (var listener in _listenersList)
            {
                if (listener is IProcess processListener)
                {
                    processListener.OnProcess();
                }
            }
        }


        private void FixedProcess()
        {
            foreach (var listener in _listenersList)
            {
                if (listener is IFixedProcess fixedProcessListener)
                {
                    fixedProcessListener.OnFixedProcess();
                }
            }
        }


        private void PostProcess()
        {
            foreach (var listener in _listenersList)
            {
                if (listener is IPostProcess postProcessListener)
                {
                    postProcessListener.OnPostProcess();
                }
            }
        }
    }
}

