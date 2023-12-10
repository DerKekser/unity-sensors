using UnityEngine;

namespace Kekser.Sensors
{
    public class RangeSensor : Sensor3D
    {
        [Header("Range")] 
        [SerializeField]
        protected float _range = 10f;
            
        public override Collider[] GetComponentsInSensor()
        {
            Collider[] colliders = Physics.OverlapSphere(
                transform.position, 
                _range, 
                _scanLayer,
                QueryTriggerInteraction.Ignore);
            return colliders;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _range);
        }
    }
}
