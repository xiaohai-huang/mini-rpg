using DG.Tweening;
using UnityEngine;

namespace Core.Game
{
    public class SpawnEffects : MonoBehaviour
    {
        void Start()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack);
        }
    }
}
