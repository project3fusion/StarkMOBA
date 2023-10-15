using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public struct PlayerDataOld : INetworkSerializable
{
    public enum PlayerDataType { Animation, Attack, Movement }
    public enum PlayerTeam { TeamBlue, TeamRed }
    
    /*
     * Please remember that the data being transferred to the player (or client) as well.
     * Since this is a web3 project, all data (maybe some will be hashed) will be visible to the other players.
     * If you are planning to make it web2 server, consider that some data shouldn't be known by the other player.
     */

    public int playerID;
    public PlayerTeam playerTeam;
    public PlayerChampionData playerChampionData;
    public PlayerItemData[] items;
    public PlayerRuneData[] runes;
    public float playerMovementSpeed;

    public PlayerDataOld(int ID, PlayerChampionData networkChampion, PlayerTeam team)
    {
        playerID = ID;
        playerTeam = team;
        playerChampionData = networkChampion;
        items = new PlayerItemData[5]; for (int i = 0; i < items.Length; i++) items[i] = new PlayerItemData();
        runes = new PlayerRuneData[5]; for (int i = 0; i < runes.Length; i++) runes[i] = new PlayerRuneData();
        playerMovementSpeed = 0f;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerID);
        serializer.SerializeValue(ref playerTeam);
        serializer.SerializeValue(ref playerMovementSpeed);
        playerChampionData.NetworkSerialize(serializer);

        /*
         * NetworkSerializer is not getting items or runes set? We set it again.
         */

        items = new PlayerItemData[5]; for (int i = 0; i < items.Length; i++) items[i] = new PlayerItemData();
        runes = new PlayerRuneData[5]; for (int i = 0; i < runes.Length; i++) runes[i] = new PlayerRuneData();
        foreach (PlayerItemData item in items) item.NetworkSerialize(serializer);
        foreach (PlayerRuneData rune in runes) rune.NetworkSerialize(serializer);
    }

    //private float UpdateTotalMana() => playerTotalMana = playerChampionData.totalMana + SumExtraTotalMana();
    //private float UpdateManaRegenerationSpeed() => playerManaRegenerationSpeed = playerChampionData.manaRegenerationSpeed + (playerChampionData.manaRegenerationSpeed * SumExtraManaRegenerationSpeed() / 100);
    /*
    private float UpdateMovementSpeed() => playerMovementSpeed = playerChampionData.movementSpeed + (playerChampionData.movementSpeed * SumExtraMovementSpeed() / 100);
    private float SumExtraTotalMana() => items.Sum(item => item.extraTotalMana) + runes.Sum(rune => rune.extraTotalMana);
    private float SumExtraManaRegenerationSpeed() => items.Sum(item => item.extraManaRegenerationSpeed) + runes.Sum(rune => rune.extraManaRegenerationSpeed);
    private float SumExtraAttackSpeed() => items.Sum(item => item.extraAttackSpeed) + runes.Sum(rune => rune.extraAttackSpeed);
    private float SumExtraADAttackDamage() => items.Sum(item => item.extraADAttackDamage) + runes.Sum(rune => rune.extraADAttackDamage);
    private float SumExtraAPAttackDamage() => items.Sum(item => item.extraAPAttackDamage) + runes.Sum(rune => rune.extraAPAttackDamage);
    private float SumExtraADArmor() => items.Sum(item => item.extraADArmor) + runes.Sum(rune => rune.extraADArmor);
    private float SumExtraAPArmor() => items.Sum(item => item.extraAPArmor) + runes.Sum(rune => rune.extraAPArmor);
    private float SumExtraADArmorPiercing() => items.Sum(item => item.extraADArmorPiercing) + runes.Sum(rune => rune.extraADArmorPiercing);
    private float SumExtraAPArmorPiercing() => items.Sum(item => item.extraAPArmorPiercing) + runes.Sum(rune => rune.extraAPArmorPiercing);
    private float SumExtraMovementSpeed() => items.Sum(item => item.extraMovementSpeed) + runes.Sum(rune => rune.extraMovementSpeed);*/

    public PlayerDataOld GeneratePlayerData() => new PlayerDataOld()
    {
        playerID = playerID,
        playerTeam = playerTeam,
        playerChampionData = playerChampionData,
        items = items,
        runes = runes,
        playerMovementSpeed = playerMovementSpeed,
    };
}