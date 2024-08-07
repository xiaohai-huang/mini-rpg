using UnityEngine;

namespace Xiaohai.Character.Arthur
{
    public class HolySeal : MonoBehaviour
    {
        [Range(1f, 10f)]
        public float Radius;
        public float DamageAmount;
        public float DamagePerSecond = 1f;

        [SerializeField]
        private Transform _sealMesh;

        [SerializeField]
        private LayerMask _layer;

        void Awake()
        {
            UpdateMeshSize();
        }

        void Start()
        {
            Init();
        }

        private void DealDamage()
        {
            var colliders = Physics.OverlapCapsule(
                transform.position,
                transform.position + Vector3.up * 2,
                Radius,
                _layer
            );

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<Damageable>(out var damageable))
                {
                    damageable.TakeDamage((int)DamageAmount);
                }
            }
        }

        private int _repeatTimer;

        /// <summary>
        /// Start to deal the damage repeatedly.
        /// </summary>
        private void Init()
        {
            _repeatTimer = Timer.Instance.SetInterval(
                () =>
                {
                    DealDamage();
                },
                1000f / DamagePerSecond,
                immediate: true
            );
        }

        void OnDestroy()
        {
            Timer.Instance.ClearInterval(_repeatTimer);
        }

        void UpdateMeshSize()
        {
            _sealMesh.localScale = new Vector3(Radius * 2, _sealMesh.localScale.y, Radius * 2);
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (_sealMesh == null)
                return;
            UpdateMeshSize();
        }
#endif
    }
}
