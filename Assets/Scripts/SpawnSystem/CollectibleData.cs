using UnityEngine;

namespace Core.Game
{
    [CreateAssetMenu(
        fileName = "New Collectible Object Data",
        menuName = "Spawn System/New Collectible"
    )]
    public class CollectibleData : EntityData
    {
        public Texture2D Icon;
    }
}
