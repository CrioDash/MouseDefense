using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTile : MonoBehaviour
{
    public enum TileType
    {
        Free, Towered
    }

    public TileType Type;
}
