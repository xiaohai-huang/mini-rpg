using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Game.UI
{
    public class LevelUI : MonoBehaviour
    {
        public float ProgressTweenDuration;

        [SerializeField]
        private Image _XP_Progress;
        private const string PROGRESS_REF = "_Progress";

        [SerializeField]
        private TextMeshProUGUI _levelText;

        public void HandleLevelChange(int level, int _)
        {
            _levelText.text = level.ToString();
        }

        private float _prevXP;

        private Tweener _tweener;

        public void HandleXPChange(float xp)
        {
            if (xp > _prevXP)
            {
                _tweener = DOTween.To(
                    () => _XP_Progress.material.GetFloat(PROGRESS_REF),
                    newValue => _XP_Progress.material.SetFloat(PROGRESS_REF, newValue),
                    xp,
                    ProgressTweenDuration
                );
            }
            else
            {
                _tweener.Kill();
                _XP_Progress.material.SetFloat(PROGRESS_REF, xp);
            }

            _prevXP = xp;
        }
    }
}
