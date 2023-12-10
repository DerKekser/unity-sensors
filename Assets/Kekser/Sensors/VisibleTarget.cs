using UnityEngine;

namespace Kekser.Sensors
{
    public class VisibleTarget : MonoBehaviour
    {
        [SerializeField] 
        private Transform[] _targets;

        public Transform[] Targets => _targets;
    }
}