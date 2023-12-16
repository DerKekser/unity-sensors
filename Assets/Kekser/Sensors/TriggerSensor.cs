using System.Collections.Generic;
using UnityEngine;

namespace Kekser.Sensors
{
    public class TriggerSensor : Sensor3D
    {
        private List<Collider> _triggerObjects = new List<Collider>();
        
        protected override Collider[] GetComponentsInSensor()
        {
            return _triggerObjects.ToArray();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(((1 << other.gameObject.layer) & _scanLayer) == 0)
                return;
            _triggerObjects.Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if(((1 << other.gameObject.layer) & _scanLayer) == 0)
                return;
            _triggerObjects.Remove(other);
        }
    }
}
