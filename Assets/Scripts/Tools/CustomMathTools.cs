using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public static class CustomMathTools
    {
        public static float GetAngleBetweenTwoPoints(Vector2 pointA, Vector2 pointB)
        {
            var target = pointB - pointA;
            var angle = Vector2.Angle(pointA, pointB);
            var orientation = Mathf.Sign(pointA.x*target.y - pointA.y*target.x);
            return (360 - orientation*angle)%360;
        }
        
        public static T GetNearestMonoBehaviourObject<T>(IEnumerable<T> objects, Vector3 from, float maximumDistance = float.MaxValue) where T : MonoBehaviour
        {
            var nearestDistance = maximumDistance;
            T nearestObject = null;

            foreach (var monoBehaviour in objects)
            {
                if (!monoBehaviour)
                    continue;
                
                var distance = Vector3.Distance(from, monoBehaviour.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestObject = monoBehaviour;
                }
            }

            return nearestObject;
        }
        
        public static T GetNearestTransform<T>(IEnumerable<T> objects, Vector3 from, float maximumDistance = float.MaxValue) where T : Transform
        {
            var nearestDistance = float.MaxValue;
            T nearestObject = null;
            
            foreach (var objectTransform in objects)
            {
                if (!objectTransform)
                    continue;
                
                var distance = Vector2.Distance(from, objectTransform.transform.position);
                if (distance < maximumDistance && distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestObject = objectTransform;
                }
            }

            return nearestObject;
        }
    }
}