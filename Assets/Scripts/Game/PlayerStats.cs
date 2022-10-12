using System.Collections;
using System.Collections.Generic;
using Towers;
using UnityEngine;

public static class PlayerStats
{
    //Base Stats
    public static int Health = 10;
    public static int Gold = 150;

    //Bonus Stats
    public static int BonusHealth = 0;
    public static int BonusGold = 0;

    //Towers
    public static TowerType[] Towers = {TowerType.Gun, TowerType.Artillery};

}
