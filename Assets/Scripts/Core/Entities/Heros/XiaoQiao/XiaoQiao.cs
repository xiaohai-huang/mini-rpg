using Core.Game.Effects;
using UnityEngine;

namespace Xiaohai.Character.XiaoQiao
{
    public class XiaoQiao : Character
    {
        [Header("XiaoQiao")]
        private EffectSystem _effectSystem;

        public override void Awake()
        {
            base.Awake();
            _effectSystem = GetComponent<EffectSystem>();
        }

        void Start()
        {
            // Add Regenerate effect
            _effectSystem.AddEffect(new RegenerateEffect());
        }
    }
}
