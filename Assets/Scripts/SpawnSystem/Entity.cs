using UnityEngine;

namespace Core.Game
{
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// A string that can uniquely identify an entity in the game.
        /// </summary>
        public string Id;
    }
}
