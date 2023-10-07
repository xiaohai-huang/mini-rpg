using System.Collections.Generic;
using UnityEngine;

namespace Xiaohai.UI
{
    public class EffectsList : MonoBehaviour
    {
        [SerializeField]
        private EffectUI _effectUI;
        private EffectSystem _effectSys;
        [SerializeField] private RuntimeCharacterAnchor _player;
        private List<EffectUI> _effectUIs = new();

        void OnEnable()
        {
            _player.OnProvided += Init;
        }

        void OnDisable()
        {
            _player.OnProvided -= Init;
        }

        void Init(Character.Character character)
        {
            _effectSys = _player.Value.GetComponent<EffectSystem>();
            _effectSys.OnAddEffect += OnAddEffect;
            _effectSys.OnRemoveEffect += OnRemoveEffect;
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
