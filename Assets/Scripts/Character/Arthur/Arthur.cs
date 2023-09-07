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
        private const float ABILITY_THREE_CLOSE_RADIUS = 3f;
        private IEnumerator LeapTowardsEnemyCoroutine(GameObject target, Action callback)
        {
            // calculate direction towards the enemy
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // face towards the target

            // calculate the distance to leap

            // calculate the destination
            var destinations = CalculateIntersectionPoints(new Vector2(target.transform.position.x, target.transform.position.z), ABILITY_THREE_CLOSE_RADIUS, new Vector2(direction.x, direction.z)).Select(intersection => new Vector3(target.transform.position.x + intersection.x, target.transform.position.y, target.transform.position.z + intersection.y)).ToArray();

            Vector3 destination = Vector3.Distance(transform.position, destinations[0]) < Vector3.Distance(transform.position, destinations[1])
                                  ? destinations[0] : destinations[1];

            Debug.Log($"destination: {destination}");
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

        private List<Vector2> CalculateIntersectionPoints(Vector2 center, float radius, Vector2 direction)
        {
            List<Vector2> intersectionPoints = new List<Vector2>();
            var slope = direction.y / direction.x;
            // line:    y = slope * x 
            // circle:  (x - center.x)^2 + (y - center.y)^2 = radius^2
            // (x - center.x)^2 + (slope * x - center.y)^2 = radius^2

            // x^2 - 2*(center.x*x) + (center.x)^2 + (slope*x)^2 - 2*(slope * center.y*x) + center.y^2 = radius^2
            // (1+slope)x^2 + (-2*center.x -2*slope*center.y ) * x + (center.x)^2 + (center.y)^2  - radius^2 = 0
            var a = 1 + slope;
            var b = -2 * center.x - 2 * slope * center.y;
            var c = center.x * center.x + center.y * center.y - radius * radius;

            var delta = b * b - 4 * a * c;

            if (delta < 0) { }// no intersection
            else if (delta == 0) // 1 point
            {
                var x = (-b + Mathf.Sqrt(delta)) / 2;
                var y = slope * x;
                intersectionPoints.Add(new Vector2(x, y));
            }
            else if (delta > 0) // 2 points
            {
                var x_1 = (-b + Mathf.Sqrt(delta)) / 2 * a;
                var y_1 = slope * x_1;

                var x_2 = (-b - Mathf.Sqrt(delta)) / 2 * a;
                var y_2 = slope * x_2;

                intersectionPoints.Add(new Vector2(x_1, y_1));
                intersectionPoints.Add(new Vector2(x_2, y_2));
            }
            return intersectionPoints;
        }
    }
}
