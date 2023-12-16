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
                if (Vector2.Angle(hitObjects[i].transform.position - transform.position, transform.right) > _angle / 2f)
                    hitObjects.RemoveAt(i);
            }

            return hitObjects.ToArray();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            SensorGizmos.DrawWireView(transform.position, transform.right, _range, _angle);
        }
    }
}
