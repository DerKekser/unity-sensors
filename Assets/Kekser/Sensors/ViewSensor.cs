using System.Collections.Generic;
using UnityEngine;

namespace Kekser.Sensors
{
    public class ViewSensor : RangeSensor
    {
        [Header("View")] 
        [SerializeField, Range(0f, 360f)] 
        protected float _angle = 180f;

        public override Collider[] GetComponentsInSensor()
        {
            List<Collider> hitObjects = new List<Collider>(base.GetComponentsInSensor());

            for (int i = hitObjects.Count - 1; i >= 0; i--)
            {
                if (Vector3.Angle(hitObjects[i].transform.position - transform.position, transform.forward) > _angle / 2f)
                    hitObjects.RemoveAt(i);
            }

            return hitObjects.ToArray();
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 rotatedDirection;
            Gizmos.color = Color.white;
            Gizmos.DrawRay(transform.position, transform.forward * _range);
            Gizmos.color = Color.yellow;
            rotatedDirection = Quaternion.Euler(0, _angle / 2f, 0) * Vector3.forward;
            Gizmos.DrawRay(transform.position, transform.TransformDirection(rotatedDirection) * _range);
            rotatedDirection = Quaternion.Euler(0, -_angle  / 2f, 0) * Vector3.forward;
            Gizmos.DrawRay(transform.position, transform.TransformDirection(rotatedDirection) * _range);
            rotatedDirection = Quaternion.Euler(_angle / 2f, 0, 0) * Vector3.forward;
            Gizmos.DrawRay(transform.position, transform.TransformDirection(rotatedDirection) * _range);
            rotatedDirection = Quaternion.Euler(-_angle / 2f, 0, 0) * Vector3.forward;
            Gizmos.DrawRay(transform.position, transform.TransformDirection(rotatedDirection) * _range);
        }
    }
}
