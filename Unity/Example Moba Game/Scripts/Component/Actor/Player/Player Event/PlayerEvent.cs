using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvent
{
    private Player player;

    public PlayerEvent(Player player) => this.player = player;

    public float RecieveDamage(float adDamage, float apDamage)
    {
        if(!player.isDead.Value && player.playerData.Value.playerHealthData.ReduceHealth(adDamage + apDamage) <= 0) Die();
        return adDamage + apDamage;
    }

    public float SendDamage(float adDamage, float apDamage, Actor target, Transform myTransform, string key)
    {
        if (player.playerData.Value.playerChampionData.championType == PlayerChampionData.ChampionType.Melee) target.RecieveDamage(adDamage, apDamage);
        else Projectile.SpawnProjectile(adDamage, apDamage, target, myTransform, key, "Player " + player.id + " Hit VFX");
        return apDamage + adDamage;
    }

    public void Die()
    {
        IEnumerator DisableAfterDeathAnimation()
        {
            player.PlayerDeathSpeechClientRpc();
            player.PlayerAnimationOrderClientRpc("Death");
            yield return new WaitForSeconds(2f);
            player.playerSpawn.Spawn();
        }
        player.isDead.Value = true;
        ServerManager.Instance.StartCoroutine(DisableAfterDeathAnimation());
    }
}
