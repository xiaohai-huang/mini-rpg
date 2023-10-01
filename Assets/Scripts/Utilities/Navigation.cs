using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Xiaohai.Utilities
{
    public static class Navigation
    {
        /// <summary>
        /// Find the location of target with a offset of `radius`. 
        /// This can prevent the character from colliding with the target.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="origin"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static Vector3 GetLocationWithOffset(Vector3 target, Vector3 origin, float radius)
        {
            Vector3 direction = (target - origin).normalized;

            var destinations = CalculateIntersectionPoints(
                                         new Vector2(target.x, target.z),
                                         radius,
                                         new Vector2(direction.x, direction.z)).
                                     Select(intersection => new Vector3(intersection.x, target.y, intersection.y)).ToArray();

            Vector3 destination = Vector3.zero;
            if (destinations.Length == 0)
            {
                Debug.LogError($"Not intersection. target:{target}, origin:{origin}");
                destination = target;
            }
            else if (destinations.Length == 1)
            {
                destination = destinations[0];
            }
            else
            {
                // pick the one that is close to the origin
                destination = Vector3.Distance(origin, destinations[0]) < Vector3.Distance(origin, destinations[1])
                                  ? destinations[0] : destinations[1];
            }

            return destination;
        }

        /// <summary>
        /// Find the intersection points between a circle and a line.
        /// </summary>
        /// <param name="center">The center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="direction">The direction vector of the line.</param>
        /// <returns>Intersection points.</returns>
        private static List<Vector2> CalculateIntersectionPoints(Vector2 center, float radius, Vector2 direction)
        {
            // Formula inspired by https://math.stackexchange.com/a/228855
            List<Vector2> intersectionPoints = new List<Vector2>();
            var slope = direction.y / direction.x;
            var yIntercept = center.y - slope * center.x;
            // line:    y = slope * x + yIntercept
            // circle:  (x - center.x)^2 + (y - center.y)^2 = radius^2
            // substitute the line equation into circle equation
            // (x - center.x)^2 + (slope * x + yIntercept - center.y)^2 = radius^2

            var a = 1 + slope * slope;
            var b = 2 * (slope * yIntercept - slope * center.y - center.x);
            var c = center.y * center.y - radius * radius + center.x * center.x - 2 * (yIntercept * center.y) + yIntercept * yIntercept;

            var delta = b * b - 4 * a * c;

            if (delta < 0) { }// no intersection
            else if (delta == 0) // 1 point
            {
                var x = -b / (2 * a);
                var y = slope * x + yIntercept;
                intersectionPoints.Add(new Vector2(x, y));
            }
            else if (delta > 0) // 2 points
            {
                var x_1 = (-b + Mathf.Sqrt(delta)) / (2 * a);
                var y_1 = slope * x_1 + yIntercept;
                var v1 = new Vector2(x_1, y_1);

                var x_2 = (-b - Mathf.Sqrt(delta)) / (2 * a);
                var y_2 = slope * x_2 + yIntercept;
                var v2 = new Vector2(x_2, y_2);

                intersectionPoints.Add(v1);
                intersectionPoints.Add(v2);
            }

#if UNITY_EDITOR
            intersectionPoints.ForEach(point =>
            {
                Vector3 pointV3 = new Vector3(point.x, 0, point.y);
                Debug.DrawRay(pointV3, Vector3.up, Color.red, 5f);
            });
#endif
            return intersectionPoints;
        }

    }
}