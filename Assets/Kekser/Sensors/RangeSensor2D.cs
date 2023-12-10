using System.Collections.Generic;
using UnityEngine;

namespace Kekser.Sensors
{
    public class RangeSensor2D : Sensor2D
    {
        [Header("Range")] 
        [SerializeField]
        protected float _range = 10f;
            
        public override Collider2D[] GetComponentsInSensor()
        {
            List<Collider2D> colliders = new List<Collider2D>();
            Physics2D.OverlapCircle(
                transform.position,
                _range, 
                new ContactFilter2D()
            {
                useLayerMask = true,
                layerMask = _scanLayer
            }, colliders);
            return colliders.ToArray();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _range);
        }
    }
}
