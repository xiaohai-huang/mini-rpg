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
        private static readonly int PROGRESS_REF = Shader.PropertyToID("_Progress");

        [SerializeField]
        private TextMeshProUGUI _levelText;

        public void HandleLevelChange(int level, int _)
        {
            _levelText.text = level.ToString();
        }

        private float _prevXP;

        private Tweener _tweener;

        private Material _progressMat;

        public void HandleXPChange(float xp)
        {
            if (_progressMat == null)
            {
                _XP_Progress.material = Instantiate(_XP_Progress.material);
                _progressMat = _XP_Progress.material;
            }

            if (xp > _prevXP)
            {
                _tweener = DOTween.To(
                    () => _progressMat.GetFloat(PROGRESS_REF),
                    newValue => _progressMat.SetFloat(PROGRESS_REF, newValue),
                    xp,
                    ProgressTweenDuration
                );
            }
            else
            {
                _tweener.Kill();
                _progressMat.SetFloat(PROGRESS_REF, xp);
            }

            _prevXP = xp;
        }
    }
}
