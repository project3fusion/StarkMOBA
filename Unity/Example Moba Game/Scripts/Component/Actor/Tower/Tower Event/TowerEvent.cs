using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerEvent
{
    private Tower tower;

    public TowerEvent(Tower tower) => this.tower = tower;

    public float RecieveDamage(float adDamage, float apDamage)
    {
        if (tower.towerData.Value.towerHealthData.ReduceHealth(adDamage + apDamage) <= 0) Die();
        return adDamage + apDamage;
    }

    public float SendDamage(float adDamage, float apDamage, Actor target, Transform myTransform, string key)
    {
        Projectile.SpawnProjectile(adDamage, apDamage, target, myTransform, key, "");
        return adDamage + apDamage;
    }

    public void Die()
    {
        tower.GetComponent<NetworkObject>().enabled = false;
        tower.enabled = false;
    }
}
