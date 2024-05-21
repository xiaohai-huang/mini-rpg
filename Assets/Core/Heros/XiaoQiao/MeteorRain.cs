using UnityEngine;

namespace Xiaohai.Character.XiaoQiao
{
    public class MeteorRain : MonoBehaviour
    {
        [SerializeField]
        private int _damageAmount;

        [SerializeField]
        private float _duration;

        [SerializeField]
        private float _radius;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start() { }

        // Update is called once per frame
        void Update() { }

        public void Init(int damageAmount, float duration, float radius)
        {
            _damageAmount = damageAmount;
            _duration = duration;
            _radius = radius;
        }
    }
}
