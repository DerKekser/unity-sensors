using UnityEngine;

namespace Kekser.Sensors
{
    public class RaySensor2D : Sensor2D
    {
        [Header("Ray")] 
        [SerializeField] 
        protected float _distance = Mathf.Infinity;
        
        public override Collider2D[] GetComponentsInSensor()
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(
                transform.position, 
                transform.right,
                _distance, 
                _scanLayer);
            
            Collider2D[] hitObjects = new Collider2D[hits.Length];
            for (int i = 0; i < hits.Length; i++)
                hitObjects[i] = hits[i].collider;

            return hitObjects;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, transform.right * _distance);
        }
    }
}