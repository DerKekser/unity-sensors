using UnityEngine;

namespace Kekser.Sensors
{
    public class RaySensor : Sensor3D
    {
        [Header("Ray")] 
        [SerializeField] 
        protected float _distance = Mathf.Infinity;
        
        public float Distance
        {
            get => _distance;
            set => _distance = value;
        }
        
        protected override Collider[] GetComponentsInSensor()
        {
            RaycastHit[] hits = Physics.RaycastAll(
                transform.position, 
                Vector3.Scale(transform.forward, transform.localScale).normalized, 
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
            Gizmos.DrawRay(transform.position, Vector3.Scale(transform.forward, transform.localScale).normalized * _distance);
        }
    }
}