using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MinionEvent
{
    private Minion minion;

    public MinionEvent(Minion minion) => this.minion = minion;

    public float RecieveDamage(float adDamage, float apDamage)
    {
        if(!minion.isDead.Value && minion.minionData.Value.minionHealthData.ReduceHealth(adDamage + apDamage) <= 0) Die();
        minion.minionData.SetDirty(true);
        return adDamage + apDamage;
    }

    public float SendDamage(float adDamage, float apDamage, Actor target, Transform myTransform, string key)
    {
        Projectile.SpawnProjectile(adDamage, apDamage, target, myTransform, key, "");
        return adDamage + apDamage;
    }

    public void Die()
    {
        IEnumerator DisableAfterDeathAnimation()
        {
            minion.OnDeath();
            ServerManager.Instance.RemoveMinion(minion, (int) minion.minionData.Value.minionTeam);
            minion.minionMovement.StopMovement();
            yield return new WaitForSeconds(1f);
            minion.minionNetworkObject.Despawn();
        }
        minion.isDead.Value = true;
        minion.StartCoroutine(DisableAfterDeathAnimation());
    }
}
