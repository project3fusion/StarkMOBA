using System.Collections;
using UnityEngine;

[System.Serializable]
public class PlayerAttack
{
    private Player player;
    
    public bool continuouslyCheckRange;

    private Transform targetTransform;
    private Quaternion targetRotation;
    private RaycastHit hit;
    private float targetDistance;
    private Vector3 targetDirection;
    private GameObject localTarget;
    private PlayerAttackData.TargetType localTargetType;
    private Player localTargetPlayer;
    private Minion localTargetMinion;
    private Tower localTargetTower;

    public PlayerAttack(Player player)
    {
        this.player = player;
    }

    public void OnUpdate()
    {
        if (player.IsClient) ClientOnUpdate();
        if (player.IsServer) ServerOnUpdate();
    }

    private void ClientOnUpdate()
    {
        if (player.playerInput.RightClick)
        {
            if (!ClientCheckIsEnemyTargeted())
            {
                player.PlayerStopAttackRequestServerRpc();
                return;
            }
            localTarget = hit.collider.gameObject;
            if((localTargetType = ClientGetTargetType()) != PlayerAttackData.TargetType.Null) player.PlayerAttackRequestServerRpc(ClientGetTargetID(), localTargetType);
        }
    }

    private void ServerOnUpdate() => SmoothRotate();

    private void SmoothRotate()
    {
        if (!ServerCheckIsPlayerAttacking()) return;
        if ((targetTransform = ServerGetTarget().transform) == null) return;
        if (ServerCheckPlayerRotation())
        {
            player.transform.rotation = targetRotation;
            return;
        }
        targetDirection = targetTransform.position - player.transform.position;
        targetDirection.y = 0f;
        if (targetDirection != Vector3.zero) player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(targetDirection), 10f * Time.deltaTime);
    }

    private bool ClientCheckIsEnemyTargeted() => ClientCheckIsMinionTargeted() || ClientCheckIsTowerTargeted() || ClientCheckIsPlayerTargeted();
    private bool ClientCheckIsPlayerTargeted() => Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Player"));
    private bool ClientCheckIsMinionTargeted() => Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Minion"));
    private bool ClientCheckIsTowerTargeted() => Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Tower"));
    private bool ClientCheckPlayerAttackCooldown() => Time.time - player.playerData.Value.playerAttackData.playerLastAttackTime >= player.playerData.Value.playerDamageData.playerAttackCooldownTime;
    private bool ClientCheckPlayerRange() => targetDistance <= player.playerData.Value.playerChampionData.range;
    public bool ServerCheckPlayerAttackCooldown() => Time.time - player.playerData.Value.playerAttackData.playerLastAttackTime >= player.playerData.Value.playerDamageData.playerAttackCooldownTime;
    private bool ServerCheckIsPlayerAttacking() => player.playerData.Value.playerAttackData.isPlayerAttacking;
    private bool ServerCheckPlayerRotation() => Quaternion.Angle(player.transform.rotation, targetRotation) < 0.1f;
    public PlayerAttackData.TargetType ServerGetPlayerTargetType() => player.playerData.Value.playerAttackData.playerTargetType;
    private PlayerChampionData.ChampionType ServerGetPlayerChampionType() => player.playerData.Value.playerChampionData.championType;

    public PlayerAttackData.TargetType ClientGetTargetType()
    {
        if (localTarget.TryGetComponent(out localTargetPlayer)) return PlayerAttackData.TargetType.Player;
        else if (localTarget.TryGetComponent(out localTargetMinion)) return PlayerAttackData.TargetType.Minion;
        else if (localTarget.TryGetComponent(out localTargetTower)) return PlayerAttackData.TargetType.Tower;
        else return PlayerAttackData.TargetType.Null;
    }

    public int ClientGetTargetID() => localTargetType switch
    {
        PlayerAttackData.TargetType.Player => localTargetPlayer.playerData.Value.playerID,
        PlayerAttackData.TargetType.Minion => localTargetMinion.minionData.Value.minionID,
        PlayerAttackData.TargetType.Tower => localTargetTower.towerData.Value.towerID,
        _ => -1
    };

    public void ServerTryAttackTarget()
    {
        player.playerData.Value.playerAttackData.UpdateData(playerLastAttackTime: Time.time);
        player.SendDamage(player.playerData.Value.playerDamageData.playerADAttackDamage,
            player.playerData.Value.playerDamageData.playerAPAttackDamage, ServerGetTarget(), player.transform, "Player " + player.id + " Projectile");
        player.PlayerAnimationOrderClientRpc("Melee Attack");
    }

    public bool ServerCheckIsTargetDead() => ServerGetPlayerTargetType() switch
    {
        PlayerAttackData.TargetType.Player => ServerCheckIsTargetPlayerDead(),
        PlayerAttackData.TargetType.Minion => ServerCheckIsTargetMinionDead(),
        PlayerAttackData.TargetType.Tower => ServerCheckIsTargetTowerDead(),
        _ => true
    };
    public bool ServerCheckIsTargetPlayerDead() => ServerManager.Instance.players[player.playerData.Value.playerAttackData.playerTargetID].isDead.Value;
    public bool ServerCheckIsTargetMinionDead() => ServerManager.Instance.minions[player.playerData.Value.playerAttackData.playerTargetID].isDead.Value;
    public bool ServerCheckIsTargetTowerDead() => ServerManager.Instance.towers[player.playerData.Value.playerAttackData.playerTargetID].isDead.Value;

    public Actor ServerGetTarget() => ServerGetPlayerTargetType() switch
    {
        PlayerAttackData.TargetType.Player => ServerGetTargetPlayer(),
        PlayerAttackData.TargetType.Minion => ServerGetTargetMinion(),
        PlayerAttackData.TargetType.Tower => ServerGetTargetTower(),
        _ => null
    };
    public Actor ServerGetTargetPlayer() => ServerManager.Instance.players[player.playerData.Value.playerAttackData.playerTargetID];
    public Actor ServerGetTargetMinion() => ServerManager.Instance.minions[player.playerData.Value.playerAttackData.playerTargetID];
    public Actor ServerGetTargetTower() => ServerManager.Instance.towers[player.playerData.Value.playerAttackData.playerTargetID];

    public bool ServerCheckTargetRange() => ServerGetPlayerTargetType() switch
    {
        PlayerAttackData.TargetType.Player => ServerCheckTargetPlayerRange(),
        PlayerAttackData.TargetType.Minion => ServerCheckTargetMinionRange(),
        PlayerAttackData.TargetType.Tower => ServerCheckTargetTowerRange(),
        _ => false
    };
    public bool ServerCheckTargetPlayerRange() => Vector3.Distance(player.transform.position, ServerManager.Instance.players[player.playerData.Value.playerAttackData.playerTargetID].transform.position) <= player.playerData.Value.playerChampionData.range;
    public bool ServerCheckTargetMinionRange() => Vector3.Distance(player.transform.position, ServerManager.Instance.minions[player.playerData.Value.playerAttackData.playerTargetID].transform.position) <= player.playerData.Value.playerChampionData.range;
    public bool ServerCheckTargetTowerRange() => Vector3.Distance(player.transform.position, ServerManager.Instance.towers[player.playerData.Value.playerAttackData.playerTargetID].transform.position) <= player.playerData.Value.playerChampionData.range;

    public bool ServerCheckTargetTeam() => ServerGetPlayerTargetType() switch
    {
        PlayerAttackData.TargetType.Player => ServerCheckTargetPlayerTeam(),
        PlayerAttackData.TargetType.Minion => ServerCheckTargetMinionTeam(),
        PlayerAttackData.TargetType.Tower => ServerCheckTargetTowerTeam(),
        _ => false
    };
    public bool ServerCheckTargetPlayerTeam() => player.playerData.Value.playerTeam != ((Player) ServerManager.Instance.players[player.playerData.Value.playerAttackData.playerTargetID]).playerData.Value.playerTeam;
    public bool ServerCheckTargetMinionTeam() => player.playerData.Value.playerTeam != (PlayerData.PlayerTeam) ((Minion) ServerManager.Instance.minions[player.playerData.Value.playerAttackData.playerTargetID]).minionData.Value.minionTeam;
    public bool ServerCheckTargetTowerTeam() => player.playerData.Value.playerTeam != (PlayerData.PlayerTeam) ((Tower) ServerManager.Instance.towers[player.playerData.Value.playerAttackData.playerTargetID]).towerData.Value.towerTeam;
}