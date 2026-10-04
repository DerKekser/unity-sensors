using System;
using UnityEngine;

namespace Kekser.Sensors
{
    public class VisibleTarget : MonoBehaviour
    {
        [SerializeField] 
        private Transform[] _targets = Array.Empty<Transform>();

        public Transform[] Targets
        {
            get => _targets;
            set => _targets = value;
        }
    }
}