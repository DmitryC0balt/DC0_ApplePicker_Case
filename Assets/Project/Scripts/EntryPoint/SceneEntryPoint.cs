using System.Collections.Generic;
using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.EntryPoint
{
    public class SceneEntryPoint : MonoBehaviour
    {
        [Header("Processable objects")]
        [SerializeField] private List<IProcessable> _processableList;
    }
}