using UnityEngine;

namespace Kekser.Sensors
{
    public static class SensorGizmos
    {
        private const int _circleResolution = 32;
        
        public static void DrawWireView(Vector3 position, Vector3 direction, float radius, float angle)
        {
            if (angle == 0f || radius == 0f)
                return;
            if (angle >= 360f)
            {
                Gizmos.DrawWireSphere(position, radius);
                return;
            }
            
            Vector3 right = Vector3.Cross(direction, Vector3.up).normalized;
            if (right == Vector3.zero)
                right = Vector3.Cross(direction, Vector3.right).normalized;
            Vector3 up = Vector3.Cross(right, direction).normalized;
            
            DrawWireArc(position, direction, right, radius, angle);
            DrawWireArc(position, direction, up, radius, angle);
            
            if (angle > 180)
                DrawWireDisk(position, direction, radius);
            
            float lengthDisk = radius * Mathf.Cos(angle / 2f * Mathf.Deg2Rad);
            float radiusDisk = radius * Mathf.Sin(angle / 2f * Mathf.Deg2Rad);
            DrawWireDisk(position + direction * lengthDisk, direction, radiusDisk);
            
            Gizmos.DrawLine(position, position + direction * lengthDisk + right * radiusDisk);
            Gizmos.DrawLine(position, position + direction * lengthDisk - right * radiusDisk);
            Gizmos.DrawLine(position, position + direction * lengthDisk + up * radiusDisk);
            Gizmos.DrawLine(position, position + direction * lengthDisk - up * radiusDisk);
        }
        
        public static void DrawWireDisk(Vector3 position, Vector3 normal, float radius)
        {
            if (radius == 0f)
                return;
            
            Vector3 forward = Vector3.Cross(normal, Vector3.up).normalized;
            if (forward == Vector3.zero)
                forward = Vector3.Cross(normal, Vector3.right).normalized;
            
            Vector3[] circle = new Vector3[_circleResolution];
            for (int i = 0; i < _circleResolution; i++)
            {
                float angle = 360f / _circleResolution * i;
                circle[i] = position + Quaternion.AngleAxis(angle, normal) * forward * radius;
            }

            for (int i = 0; i < _circleResolution; i++)
                Gizmos.DrawLine(circle[i], circle[(i + 1) % _circleResolution]);
        }
        
        public static void DrawWireArc(Vector3 position, Vector3 direction, Vector3 normal, float radius, float angle)
        {
            if (angle == 0f || radius == 0f)
                return;
            if (angle >= 360f)
            {
                DrawWireDisk(position, normal, radius);
                return;
            }
            
            Vector3 right = Vector3.Cross(-direction, normal).normalized;
            Vector3 forward = Vector3.Cross(right, normal).normalized;
            
            Vector3[] circle = new Vector3[_circleResolution];
            for (int i = 0; i < _circleResolution; i++)
            {
                float angleStep = angle / (_circleResolution - 1);
                float angleOffset = angle / 2f;
                float angleCurrent = angleStep * i - angleOffset;
                circle[i] = position + Quaternion.AngleAxis(angleCurrent, normal) * forward * radius;
            }

            for (int i = 0; i < _circleResolution - 1; i++)
                Gizmos.DrawLine(circle[i], circle[i + 1]);
        }
    }
}