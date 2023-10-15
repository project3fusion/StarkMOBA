using Unity.Netcode;
using UnityEngine;

public class Projectile : Effect
{
    public Tower tower;
    public Actor target;
    public string hitVFXKey;
    public float projectileSpeed, adDamage, apDamage;
    public NetworkObject projectileNetworkObject;

    public static void SpawnProjectile(float adDamage, float apDamage, Actor target, Transform transform, string key, string hitVFXKey)
    {
        NetworkObject networkObject = ServerManager.Instance.serverManagerNetworkObjectPools[key].GetNetworkObject(transform.position, Quaternion.identity);
        Projectile projectile = networkObject.gameObject.GetComponent<Projectile>();
        projectile.adDamage = adDamage;
        projectile.apDamage = apDamage;
        projectile.transform.position = new Vector3(projectile.transform.position.x, projectile.transform.position.y + 0.5f, projectile.transform.position.z);
        projectile.target = target;
        projectile.projectileNetworkObject = networkObject;
        projectile.hitVFXKey = hitVFXKey;
    }

    public void Update()
    {
        if (IsServer)
        {
            if (target == null || target.isDead.Value)
            {
                if(projectileNetworkObject != null) projectileNetworkObject.Despawn();
                return;
            }
            if (target != null) transform.position = Vector3.MoveTowards(transform.position, target.selfTargetPointTransform.position, projectileSpeed * Time.deltaTime);

            if ((transform.position - target.selfTargetPointTransform.position).sqrMagnitude <= 0.25f)
            {
                if (hitVFXKey != "") ClientManager.Instance.PlayCollisionVFXClientRpc(hitVFXKey, target.selfTargetPointTransform.position, target.transform.rotation);
                target.RecieveDamage(adDamage, apDamage);
                projectileNetworkObject.Despawn();
            }
        }
    }
}
