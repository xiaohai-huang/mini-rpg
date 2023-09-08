using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Xiaohai.Character.Arthur
{
    public class Arthur : Character
    {
        private CharacterController _characterController;
        void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LeapTowardsEnemy(GameObject target, Action callback)
        {
            StartCoroutine(LeapTowardsEnemyCoroutine(target, callback));
        }
        private const float ABILITY_THREE_CLOSE_RADIUS = 2f;
        private IEnumerator LeapTowardsEnemyCoroutine(GameObject target, Action callback)
        {
            // calculate direction towards the enemy
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // face towards the target
            while (!(Vector3.Dot(transform.forward, direction) > 0.99f))
            {
                var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20f * Time.deltaTime);
                yield return null;
            }

            // calculate the destination
            var destinations = CalculateIntersectionPoints(new Vector2(target.transform.position.x, target.transform.position.z), ABILITY_THREE_CLOSE_RADIUS, new Vector2(direction.x, direction.z)).Select(intersection => new Vector3(intersection.x, target.transform.position.y, intersection.y)).ToArray();
            Vector3 destination = Vector3.zero;
            if (destinations.Length == 0)
            {
                Debug.LogError("Not intersection.");
                destination = target.transform.position;
            }
            else if (destinations.Length == 1)
            {
                destination = destinations[0];
            }
            else
            {
                destination = Vector3.Distance(transform.position, destinations[0]) < Vector3.Distance(transform.position, destinations[1])
                                  ? destinations[0] : destinations[1];
            }

            _characterController.enabled = false;
            // while the character is not at the destination
            while (transform.position != destination)
            {
                // move the character towards the destination
                transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * 10f);

                // wait until the next frame
                yield return null;
            }

            _characterController.enabled = true;

            callback();
        }

        /// <summary>
        /// Find the intersection points between a circle and a line.
        /// </summary>
        /// <param name="center">The center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="direction">The direction vector of the line.</param>
        /// <returns>Intersection points.</returns>
        private List<Vector2> CalculateIntersectionPoints(Vector2 center, float radius, Vector2 direction)
        {
            List<Vector2> intersectionPoints = new List<Vector2>();
            Vector2 lineOrigin = center - direction * radius;
            Vector2 lineEnd = center + direction * radius;

            float dx = lineEnd.x - lineOrigin.x;
            float dy = lineEnd.y - lineOrigin.y;

            float A = dx * dx + dy * dy;
            float B = 2 * (dx * (lineOrigin.x - center.x) + dy * (lineOrigin.y - center.y));
            float C = (lineOrigin.x - center.x) * (lineOrigin.x - center.x) +
                      (lineOrigin.y - center.y) * (lineOrigin.y - center.y) - radius * radius;

            float det = B * B - 4 * A * C;
            if ((A <= 0.0000001) || (det < 0))
            {
                // No real solutions, no intersection.
            }
            else if (det == 0)
            {
                // One solution, one intersection point.
                float t = -B / (2 * A);
                intersectionPoints.Add(new Vector2(lineOrigin.x + t * dx, lineOrigin.y + t * dy));
            }
            else
            {
                // Two solutions, two intersection points.
                float t = (float)((-B + Math.Sqrt(det)) / (2 * A));
                intersectionPoints.Add(new Vector2(lineOrigin.x + t * dx, lineOrigin.y + t * dy));
                t = (float)((-B - Math.Sqrt(det)) / (2 * A));
                intersectionPoints.Add(new Vector2(lineOrigin.x + t * dx, lineOrigin.y + t * dy));
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
