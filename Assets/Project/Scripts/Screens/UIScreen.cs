using UnityEngine;

namespace Scripts.Screens
{
    public abstract class UIScreen : MonoBehaviour
    {
        public virtual void Show(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}