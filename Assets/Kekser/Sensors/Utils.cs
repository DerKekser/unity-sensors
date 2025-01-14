using System.Collections.Generic;
using UnityEngine;

namespace Kekser.Sensors
{
    public static class Utils
    {
        public static List<GameObject> OverlapSensors(this IEnumerable<Sensor> sensors)
        {
            List<GameObject> objects = new List<GameObject>();
            foreach (Sensor sensor in sensors)
            {
                if (!sensor.AutoUpdateSensor)
                    sensor.SensorUpdate();
                foreach (GameObject obj in sensor.DetectedObjects)
                {
                    if (!objects.Contains(obj))
                        objects.Add(obj);
                }
            }
            return objects;
        }

        public static GameObject ClosestObject(this IEnumerable<GameObject> objects, Vector3 position)
        {
            GameObject closest = null;
            float minDistance = Mathf.Infinity;
            foreach (GameObject obj in objects)
            {
                float distance = Vector2.Distance(position, obj.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = obj;
                }
            }
            return closest;
        }

        public static GameObject ClosestObject(this IEnumerable<Sensor> sensors, Vector3 position)
        {
            return sensors.OverlapSensors().ClosestObject(position);
        }
    }
}