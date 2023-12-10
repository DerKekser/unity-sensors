using System.Collections.Generic;
using UnityEngine;

namespace Kekser.Sensors
{
    public class TriggerSensor2D : Sensor2D
    {
        private List<Collider2D> _triggerObjects = new List<Collider2D>();
        
        public override Collider2D[] GetComponentsInSensor()
        {
            return _triggerObjects.ToArray();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(((1 << other.gameObject.layer) & _scanLayer) == 0)
                return;
            _triggerObjects.Add(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(((1 << other.gameObject.layer) & _scanLayer) == 0)
                return;
            _triggerObjects.Remove(other);
        }
    }
}
