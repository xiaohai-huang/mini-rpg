using UnityEngine;
using UnityEngine.Scripting;

namespace Core.Game.Common
{
    public class PreserveCode
    {
        [Preserve]
        private void DontStrip()
        {
            var width = Screen.width;
            var height = Screen.height;
            var safeArea = Screen.safeArea;
            var orientation = Screen.orientation;
        }
    }
}
