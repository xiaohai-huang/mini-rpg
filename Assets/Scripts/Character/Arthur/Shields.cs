using System;
using UnityEngine;

namespace Xiaohai.Character.Arthur
{
    public class Shields : MonoBehaviour
    {
        public float Radius = 1.5f;
        public float RotateSpeed = 10f;
        public float DamageAmount;

        [SerializeField]
        private RuntimeTransformAnchor _transformAnchor;

        [SerializeField]
        private Transform _container;

        [SerializeField]
        private Shield[] _shields;
        public bool Spinning;

        void Awake()
        {
            _transformAnchor.Provide(transform);
            PlaceShields();
            StopSpin();
        }

        // Update is called once per frame
        void Update()
        {
            if (Spinning)
            {
                _container.transform.Rotate(0f, 10f * RotateSpeed * Time.deltaTime, 0f);
            }
        }

        public void StartSpin(float damageAmount)
        {
            Array.ForEach(_shields, shield => shield.DamageAmount = (int)damageAmount);
            _container.gameObject.SetActive(true);
            Spinning = true;
        }

        public void StopSpin()
        {
            _container.gameObject.SetActive(false);
            Spinning = false;
        }

        /// <summary>
        /// Gets the coordinates of `numShields` shields evenly distributed around the origin on a circle with radius `radius`.
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="numShields"></param>
        /// <returns></returns>
        private static Vector3[] GetShieldCoordinates(float radius, int numShields)
        {
            Vector3[] coordinates = new Vector3[numShields];
            float anglePerShield = 2.0f * Mathf.PI / numShields;
            for (int i = 0; i < numShields; i++)
            {
                coordinates[i] = new Vector3(
                    radius * Mathf.Sin(anglePerShield * i),
                    0,
                    radius * Mathf.Cos(anglePerShield * i)
                );
            }

            return coordinates;
        }

        private void PlaceShields()
        {
            var coordinates = GetShieldCoordinates(Radius, _shields.Length);
            for (int i = 0; i < _shields.Length; i++)
            {
                var shield = _shields[i];
                shield.transform.localPosition = coordinates[i];
                shield.transform.LookAt(transform.position);
            }
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            PlaceShields();
        }
#endif
    }
}
