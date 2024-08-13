using UnityEngine;

namespace Core.Game.Common
{
    public static class Constants
    {
        public const int DAMAGE_FORMULA_COEFFICIENT = 600;
        public const float TOLERANCE = 0.001F;
        public static Vector2 MAP_SIZE = new(300f, 200f);
        public static readonly string PLAYER_HERO_ID = "hero-player";
    }
}
