using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kekser.Sensors
{
    public class SensorGroup : Sensor
    {
        [SerializeField]
        private Sensor[] _sensors;
        
        public override void SensorUpdate()
        {
            List<GameObject> oldObjects = new List<GameObject>(_detectedObjects);
            List<GameObject> checkObjects = gameObject.activeInHierarchy && enabled ? _sensors.OverlapSensors() : new List<GameObject>();
            for (int i = 0; i < checkObjects.Count; i++)
            {
                if (checkObjects[i] == null || _ignore.Contains(checkObjects[i].gameObject))
                    continue;
                
                oldObjects.Remove(checkObjects[i].gameObject);
                if (!_detectedObjects.Contains(checkObjects[i].gameObject))
                {
                    _detectedObjects.Add(checkObjects[i].gameObject);
                    OnEnter.Invoke(checkObjects[i].gameObject);
                }
                else
                {
                    OnStay.Invoke(checkObjects[i].gameObject);
                }
            }

            for (int i = 0; i < oldObjects.Count; i++)
            {
                _detectedObjects.Remove(oldObjects[i]);
                OnExit.Invoke(oldObjects[i]);
            }
        }
    }
}