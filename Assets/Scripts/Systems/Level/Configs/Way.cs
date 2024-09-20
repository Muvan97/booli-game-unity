using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Systems.Level.Configs
{
    [Serializable]
    public class Way
    {
        [field: SerializeField] public List<Transform> Points { get; private set; }
        [field: NonSerialized] public float Length { get; private set; }

        public void CalculateLength()
        {
            Length = 0;
            var segmentsNumber = 10;
            Vector2 nextPoint = BezierTools.GetPoint(Points, 0);
            for (var i = 0; i < segmentsNumber + 1; i++)
            {
                var currentPoint = nextPoint;
                nextPoint = BezierTools.GetPoint(Points, 1f / segmentsNumber * (i + 1));
                var distance = Vector2.Distance(currentPoint, nextPoint);
                Length += distance;
            }
        }
    }
}