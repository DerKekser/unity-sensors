using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kekser.Sensors
{
    public abstract class Sensor : MonoBehaviour
    {
        protected List<GameObject> _detectedObjects = new List<GameObject>();

        [Header("Base Sensor Settings")]
        [SerializeField] 
        protected bool _autoUpdateSensor = false;
        [SerializeField] 
        protected List<GameObject> _ignore;
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
        
        public void AddIgnore(GameObject ignoreObject)
        {
            if (!_ignore.Contains(ignoreObject))
                _ignore.Add(ignoreObject);
        }
        
        public void RemoveIgnore(GameObject ignoreObject)
        {
            if (_ignore.Contains(ignoreObject))
                _ignore.Remove(ignoreObject);
        }

        public GameObject[] DetectedObjects => _detectedObjects.ToArray();
        
        public abstract void SensorUpdate();
        
        protected virtual void FixedUpdate()
        {
            if (!_autoUpdateSensor)
                return;
            SensorUpdate();
        }
    }

    public abstract class Sensor<T> : Sensor where T : Component
    {
        private const int MIN_RAYCASTS = 1;
        private const int MAX_RAYCASTS = 10;

        [Header("Visibility Settings")]
        [SerializeField] 
        protected bool _checkVisibility = false;
        [SerializeField] 
        protected LayerMask _scanLayer = 0;
        [SerializeField] 
        protected LayerMask _obstructionLayer = 0;
        [SerializeField, Range(MIN_RAYCASTS, MAX_RAYCASTS)] 
        protected int _scanRays = 1;
        [SerializeField, Range(0f, 1f)] 
        protected float _visibility = 0.5f;
        
        public bool CheckVisibility
        {
            get => _checkVisibility;
            set => _checkVisibility = value;
        }
        
        public LayerMask ScanLayer
        {
            get => _scanLayer;
            set => _scanLayer = value;
        }
        
        public LayerMask ObstructionLayer
        {
            get => _obstructionLayer;
            set => _obstructionLayer = value;
        }
        
        public int ScanRays
        {
            get => _scanRays;
            set => _scanRays = Mathf.Clamp(value, MIN_RAYCASTS, MAX_RAYCASTS);
        }
        
        public float Visibility
        {
            get => _visibility;
            set => _visibility = Mathf.Clamp01(value);
        }

        protected abstract T[] GetComponentsInSensor();
        protected abstract float CheckForVisibility(T checkObject);
        
        public override void SensorUpdate()
        {
            List<GameObject> oldObjects = new List<GameObject>(_detectedObjects);
            T[] checkObjects = gameObject.activeInHierarchy && enabled ? GetComponentsInSensor() : Array.Empty<T>();
            for (int i = 0; i < checkObjects.Length; i++)
            {
                if (checkObjects[i] == null || _ignore.Contains(checkObjects[i].gameObject))
                    continue;
                
                if (!_checkVisibility || CheckForVisibility(checkObjects[i]) >= _visibility)
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
    }
}