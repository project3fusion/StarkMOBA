using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataGenerator
{
    public static Champion GeneratePlayerChampionData(int id) => (Champion) PlayerResourceFinder.Find(PlayerResourceFinder.Type.Champion, id);
}
