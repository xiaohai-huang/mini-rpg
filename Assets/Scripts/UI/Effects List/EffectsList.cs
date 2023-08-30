using System.Collections.Generic;
using UnityEngine;

namespace Xiaohai.UI
{
    public class EffectsList : MonoBehaviour
    {
        [SerializeField]
        private EffectUI _effectUI;
        public EffectSystem EffectSys;
        private List<EffectUI> _effectUIs = new();
        // Start is called before the first frame update
        void Start()
        {
            EffectSys.OnAddEffect += OnAddEffect;
            EffectSys.OnRemoveEffect += OnRemoveEffect;
        }

        private void OnAddEffect(Effect effect)
        {
            if (effect.ShowInListUI)
            {
                var effectUI = Instantiate(_effectUI, transform);
                effectUI.Init(effect.Name, effect.Icon, effect.CoolDownTime);
                _effectUIs.Add(effectUI);
            }
        }

        private void OnRemoveEffect(Effect effect)
        {
            if (effect.ShowInListUI)
            {
                var effectToRemove = _effectUIs.Find(ui => ui.Name == effect.Name);
                _effectUIs.Remove(effectToRemove);
                if (effectToRemove != null) Destroy(effectToRemove.gameObject);
            }
        }
    }
}
