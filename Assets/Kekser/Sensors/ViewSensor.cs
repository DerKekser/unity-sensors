using System.Collections.Generic;
using UnityEngine;

namespace Kekser.Sensors
{
    public class ViewSensor : RangeSensor
    {
        [Header("View")] 
        [SerializeField, Range(0f, 360f)] 
        protected float _angle = 180f;
        
        public float Angle
        {
            get => _angle;
            set => _angle = value;
        }

        protected override Collider[] GetComponentsInSensor()
        {
            List<Collider> hitObjects = new List<Collider>(base.GetComponentsInSensor());

            for (int i = hitObjects.Count - 1; i >= 0; i--)
            {
                if (Vector3.Angle(hitObjects[i].transform.position - transform.position, Vector3.Scale(transform.forward, transform.lossyScale).normalized) > _angle / 2f)
                    hitObjects.RemoveAt(i);
            }

            return hitObjects.ToArray();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            SensorGizmos.DrawWireView(transform.position, Vector3.Scale(transform.forward, transform.lossyScale).normalized, _range, _angle);
        }
    }
}
