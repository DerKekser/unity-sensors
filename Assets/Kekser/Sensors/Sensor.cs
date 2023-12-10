using System.Collections.Generic;
using UnityEngine;

namespace Kekser.Sensors
{
    public abstract class Sensor<T> : MonoBehaviour where T : Component
    {
         private List<GameObject> _detectedObjects = new List<GameObject>();

        [Header("Base Sensor Settings")]
        [SerializeField] 
        protected bool _autoUpdateSensor = false;
        [SerializeField] 
        protected List<GameObject> _ignore;
        [SerializeField] 
        protected bool _checkVisibility = false;
        [SerializeField] 
        protected LayerMask _scanLayer = 0;
        [SerializeField] 
        protected LayerMask _obstructionLayer = 0;
        [SerializeField, Range(1, 10)] 
        protected int _scanRays = 1;
        [SerializeField, Range(0f, 1f)] 
        protected float _visibility = 0.5f;
        [Header("Events")] 
        [SerializeField] 
        public SensorEvent OnEnter;
        [SerializeField] 
        public SensorEvent OnStay;
        [SerializeField] 
        public SensorEvent OnExit;

        public bool AutoUpdateSensor
        {
            get => _autoUpdateSensor;
            set => _autoUpdateSensor = value;
        }

        public GameObject[] DetectedObjects => _detectedObjects.ToArray();

        public abstract T[] GetComponentsInSensor();
        public abstract float CheckVisibility(T checkObject);
        
        public void SensorUpdate()
        {
            List<GameObject> oldObjects = new List<GameObject>(_detectedObjects);
            T[] checkObjects = GetComponentsInSensor();
            for (int i = 0; i < checkObjects.Length; i++)
            {
                if (checkObjects[i] == null || _ignore.Contains(checkObjects[i].gameObject))
                    continue;
                
                if (!_checkVisibility || CheckVisibility(checkObjects[i]) >= _visibility)
                {
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
            }

            for (int i = 0; i < oldObjects.Count; i++)
            {
                _detectedObjects.Remove(oldObjects[i]);
                OnExit.Invoke(oldObjects[i]);
            }
        }
        
        protected virtual void FixedUpdate()
        {
            if (!_autoUpdateSensor)
                return;
            SensorUpdate();
        }
    }
}