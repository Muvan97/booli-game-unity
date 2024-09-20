using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tools
{
    public static class BezierTools
    {
        public static Vector3 GetPoint(List<Vector3> points, float t)
        {
            while (points.Count > 1)
            {
                var tempPoints = new List<Vector3>();
                for (var i = 0; i < points.Count - 1; i++)
                {
                    var newPoint = Vector3.Lerp(points[i], points[i + 1], t);
                    tempPoints.Add(newPoint);
                }

                points = tempPoints;
            }

            return points[0];
        }
        
        public static Vector3 GetPoint(List<Transform> points, float t)
        {
            var newPoints = points.Select(point => point.position).ToList();
            while (newPoints.Count > 1)
            {
                var tempPoints = new List<Vector3>();
                for (var i = 0; i < newPoints.Count - 1; i++)
                {
                    var newPoint = Vector3.Lerp(newPoints[i], newPoints[i + 1], t);
                    tempPoints.Add(newPoint);
                }

                newPoints = tempPoints;
            }

            return newPoints[0];
        }
        

        public static Vector3 GetFirstDerivative(List<Transform> transforms, float t)
        {
            var n = transforms.Count - 1;
            Vector3 result = Vector3.zero;
            for (int i = 0; i < n; i++) result += (transforms[i + 1].transform.position - transforms[i].transform.position) * (n * Bernstein(n - 1, i, t));
            return result;
        }
        
        public static Vector3 GetFirstDerivative(List<Vector3> way, float t)
        {
            var n = way.Count - 1;
            Vector3 result = Vector3.zero;
            for (int i = 0; i < n; i++) result += (way[i + 1] - way[i]) * (n * Bernstein(n - 1, i, t));
            return result;
        }

        private static float Bernstein(int n, int i, float t) =>
            BinomialCoefficient(n, i) * (float) Math.Pow(t, i) * (float) Math.Pow(1 - t, n - i);


        private static int BinomialCoefficient(int n, int k)
        {
            if (k < 0 || k > n)
                return 0;
            if (k == 0 || k == n)
                return 1;
            k = Math.Min(k, n - k);
            int c = 1;
            for (int i = 0; i < k; i++)
            {
                c = c * (n - i) / (i + 1);
            }

            return c;
        }
    }
}