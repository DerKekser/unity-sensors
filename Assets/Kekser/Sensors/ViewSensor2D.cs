using System.Collections.Generic;
using UnityEngine;

namespace Kekser.Sensors
{
    public class ViewSensor2D : RangeSensor2D
    {
        [Header("View")] 
        [SerializeField, Range(0f, 360f)] 
        protected float _angle = 180f;
        
        public float Angle
        {
            get => _angle;
            set => _angle = value;
        }

        protected override Collider2D[] GetComponentsInSensor()
        {
            List<Collider2D> hitObjects = new List<Collider2D>(base.GetComponentsInSensor());

            for (int i = hitObjects.Count - 1; i >= 0; i--)
            {
                Vector3 closestPoint = hitObjects[i].ClosestPoint(transform.position);
                Vector3 delta = (closestPoint - transform.position);
                if (delta.sqrMagnitude < Mathf.Epsilon)
                    continue;
                if (Vector2.Angle(delta, Vector3.Scale(transform.right, transform.lossyScale).normalized) > _angle / 2f)
                    hitObjects.RemoveAt(i);
            }

            return hitObjects.ToArray();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            SensorGizmos.DrawWireView(transform.position, Vector3.Scale(transform.right, transform.lossyScale).normalized, _range, _angle);
        }
    }
}
