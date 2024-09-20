using System;
using System.Collections.Generic;
using Systems.Level;
using Systems.Level.Configs;
using Tools;
using UnityEngine;

namespace Logic.Other
{
    [ExecuteInEditMode]
    public class BezierLinesDrawer : MonoBehaviour
    {
        [SerializeField] private MapPrefab _mapPrefab;

        private void OnDrawGizmos() => Draw(_mapPrefab.Ways);

        private void Draw(List<Way> ways)
        {
            if (ways.Count <= 0 || ways[0].Points.Count <= 1)
                return;

            var segmentsNumber = 20;
            var preveousePoint = ways[0].Points[0].position;

            for (var index = 0; index < ways.Count; index++)
            {
                for (var i = 0; i < segmentsNumber + 1; i++)
                {
                    var parameter = (float) i / segmentsNumber;
                    var point = BezierTools.GetPoint(ways[index].Points, parameter);
                    Gizmos.DrawLine(preveousePoint, point);
                    preveousePoint = point;
                }
            }
        }
    }
}