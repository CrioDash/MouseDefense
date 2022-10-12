using UnityEngine;

namespace Tiles
{
    public class TowerTile : MonoBehaviour
    {
        public enum TileType
        {
            Free, Towered
        }

        public TileType type;
    }
}
