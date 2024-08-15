using UnityEngine;
using UnityEngine.Scripting;

namespace Core.Game.Common
{
    public class Preserve
    {
        [Preserve]
        private void DontStrip()
        {
            var orientation = Screen.orientation;
        }
    }
}
