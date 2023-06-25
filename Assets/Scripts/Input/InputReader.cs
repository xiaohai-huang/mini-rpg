using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xiaohai.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "My Scriptable Objects/Input Reader")]
    public class InputReader : ScriptableObject
    {
        public MyInputActions InputActions;

        private void OnEnable()
        {
            if (InputActions == null)
            {
                InputActions = new MyInputActions();
            }
        }

        private void OnDisable()
        {
            InputActions.Disable();
        }
    }
}
