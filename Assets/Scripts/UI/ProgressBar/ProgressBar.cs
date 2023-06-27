using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Xiaohai.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _content;
        [SerializeField] private float _progress;
        [SerializeField] private float _speed;         
        

        // Update is called once per frame
        void Update()
        {
            _content.fillAmount = Mathf.Lerp(_content.fillAmount, _progress, _speed * Time.deltaTime);
        }

        public void SetProgress(float progress)
        {
            _progress = progress;
        }
    }
}
