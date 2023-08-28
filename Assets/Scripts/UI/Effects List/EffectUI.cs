using UnityEngine;
using UnityEngine.UI;

namespace Xiaohai.UI
{
    public class EffectUI : MonoBehaviour
    {
        [Header("Public")]
        public string Name;
        public Sprite Icon;
        /// <summary>
        /// CoolDown Time in ms
        /// </summary>
        public float CoolDownTime;
        public bool ShowCoolDown = true;

        [Header("Private")]
        [SerializeField]
        private Image _image;
        [SerializeField]
        private Image _coolDown;


        // Update is called once per frame
        void Update()
        {
            if (ShowCoolDown)
            {
                //Reduce fill amount over WaitTime seconds
                _coolDown.fillAmount -= 1.0f / (CoolDownTime / 1000) * Time.deltaTime;
            }
        }

        public void Init(string name, Sprite icon, float coolDownTime, bool showCoolDown)
        {
            Name = name;
            Icon = icon;
            CoolDownTime = coolDownTime;
            ShowCoolDown = showCoolDown;
            _image.sprite = Icon;

            if (!ShowCoolDown || CoolDownTime == 0)
            {
                _coolDown.gameObject.SetActive(false);
            }
        }

        public void Init(string name, Sprite icon, float coolDownTime)
        {
            Init(name, icon, coolDownTime, coolDownTime != 0);
        }
    }
}
