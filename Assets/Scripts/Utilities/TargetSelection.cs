using System;
using System.Collections.Generic;
using UnityEngine;

namespace Xiaohai.Utilities
{
    public abstract class TargetSelection<T> : MonoBehaviour where T : MonoBehaviour
    {
        public LayerMask TargetLayer;
        public readonly List<TargetInfo<T>> Targets = new();

        void OnTriggerEnter(Collider other)
        {
            // Debug.Log($"target layer value: {TargetLayer.value} | other.gameObject.layer:{other.gameObject.layer}", other);
            if ((TargetLayer.value & (1 << other.gameObject.layer)) != 0)
            {
                if (other.TryGetComponent<T>(out var target))
                {
                    Targets.Add(new TargetInfo<T>()
                    {
                        EnteredTime = Time.time,
                        target = target,
                        GO = other.gameObject
                    });
                }
            }
        }

        void OnTriggerExit(Collider other)
        {
            if ((TargetLayer.value & (1 << other.gameObject.layer)) != 0)
            {
                var index = Targets.FindIndex(t => t.GO == other.gameObject);
                if (index != -1)
                {
                    Targets.RemoveAt(index);
                }
            }
        }

#if UNITY_EDITOR
        [Header("Editor Only")]
        [SerializeField]
        [ReadOnly]
        private int NumTargets;

        [SerializeField]
        private TargetInfo<T>[] TargetInfos = new TargetInfo<T>[12];
        void Update()
        {
            Array.Clear(TargetInfos, 0, TargetInfos.Length);
            for (int i = 0; i < Targets.Count; i++)
            {
                TargetInfos[i] = Targets[i];
            }
            NumTargets = Targets.Count;
        }
#endif    
    }
    [System.Serializable]
    public struct TargetInfo<T>
    {
        public float EnteredTime;
        public T target;
        [HideInInspector]
        public GameObject GO;
    }
}