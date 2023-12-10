using UnityEngine;

namespace Kekser.Sensors
{
    public class RaySensor : Sensor3D
    {
        [Header("Ray")] 
        [SerializeField] 
        protected float _distance = Mathf.Infinity;
        
        public override Collider[] GetComponentsInSensor()
        {
            RaycastHit[] hits = Physics.RaycastAll(
                transform.position, 
                transform.forward, 
                _distance, 
                _scanLayer,
                QueryTriggerInteraction.Ignore);
            
            Collider[] hitObjects = new Collider[hits.Length];
            for (int i = 0; i < hits.Length; i++)
                hitObjects[i] = hits[i].collider;

            return hitObjects;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, transform.forward * _distance);
        }
    }
}