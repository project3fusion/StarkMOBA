using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerResourceFinder
{
    public enum Type { Champion, Item, Rune }

    public enum SecondaryType { CharacterPrefab }

    public static ScriptableObject Find(Type type, int id) => type switch
    {
        Type.Champion => Resources.LoadAll<Champion>("Player/Champion/" + id)[0],
        Type.Item => Resources.LoadAll<Item>("Player/Item/" + id)[0],
        Type.Rune => Resources.LoadAll<Rune>("Player/Rune/" + id)[0],
        _ => null
    };
}
