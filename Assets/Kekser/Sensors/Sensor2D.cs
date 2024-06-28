using System.Collections.Generic;
using UnityEngine;

namespace Kekser.Sensors
{
    public abstract class Sensor2D : Sensor<Collider2D>
    {
        protected override float CheckForVisibility(Collider2D checkObject)
        {
            List<RaycastHit2D> hitList = new List<RaycastHit2D>();
            float hits = 0;
            
            if (checkObject.TryGetComponent(out VisibleTarget visibleTarget))
            {
                for (int i = 0; i < visibleTarget.Targets.Length; i++)
                {
                    Vector2 scanPoint = visibleTarget.Targets[i].position;

                    bool targetHit = true;
                    
                    Physics2D.Raycast((Vector2)transform.position, scanPoint - (Vector2)transform.position, new ContactFilter2D()
                    {
                        useLayerMask = true,
                        layerMask = _obstructionLayer,
                        useTriggers = false
                    }, hitList, Vector2.Distance(transform.position, scanPoint));

                    for (int j = 0; j < hitList.Count; j++)
                    {
                        if (hitList[j].collider == checkObject) 
                            continue;
                        targetHit = false;
                        break;
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
                    Vector2 scanPoint = new Vector2(
                        UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                        UnityEngine.Random.Range(bounds.min.y, bounds.max.y)
                    );

                    bool targetHit = true;
                    
                    Physics2D.Raycast((Vector2)transform.position, scanPoint - (Vector2)transform.position, new ContactFilter2D()
                    {
                        useLayerMask = true,
                        layerMask = _obstructionLayer,
                        useTriggers = false
                    }, hitList, Vector2.Distance(transform.position, scanPoint));

                    for (int j = 0; j < hitList.Count; j++)
                    {
                        if (hitList[j].collider == checkObject) 
                            continue;
                        targetHit = false;
                        break;
                    }
                    
                    if (targetHit)
                        hits++;
                }
            }
            
            return hits / _scanRays;
        }
    }
}
