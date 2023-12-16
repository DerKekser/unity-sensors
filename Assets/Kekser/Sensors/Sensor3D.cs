using UnityEngine;

namespace Kekser.Sensors
{
    public abstract class Sensor3D : Sensor<Collider>
    {
        protected override float CheckForVisibility(Collider checkObject)
        {
            RaycastHit hit;
            float hits = 0;
            
            if (checkObject.TryGetComponent(out VisibleTarget visibleTarget))
            {
                for (int i = 0; i < visibleTarget.Targets.Length; i++)
                {
                    Vector3 scanPoint = visibleTarget.Targets[i].position;

                    bool targetHit = true;
                    
                    if (Physics.Raycast(transform.position, (scanPoint - transform.position).normalized, out hit,
                            Vector3.Distance(scanPoint, transform.position), _obstructionLayer,
                            QueryTriggerInteraction.Ignore))
                    {
                        targetHit = hit.collider == checkObject;
                    }

                    if (targetHit)
                        hits++;
                }
            }
            else
            {
                Bounds bounds = checkObject.bounds;
                for (int i = 0; i < _scanRays; i++)
                {
                    Vector3 scanPoint = new Vector3(
                        UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                        UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
                        UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
                    );

                    bool targetHit = true;
                    
                    if (Physics.Raycast(transform.position, (scanPoint - transform.position).normalized, out hit,
                            Vector3.Distance(scanPoint, transform.position), _obstructionLayer,
                            QueryTriggerInteraction.Ignore))
                    {
                        targetHit = hit.collider == checkObject;
                    }

                    if (targetHit)
                        hits++;
                }
            }
            
            return hits / _scanRays;
        }
    }
}
