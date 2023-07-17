using System.Collections;
using UnityEngine;

[System.Serializable]
public class PlayerAttack
{
    private Player player;
    
    public bool continuouslyCheckRange;

    public Player localTargetPlayer;

    private RaycastHit hit;
    private float targetDistance;

    public PlayerAttack(Player player) => this.player = player;

    public void OnUpdate()
    {
        if (player.IsClient) ClientOnUpdate();
        if (player.IsServer) ServerOnUpdate();
    }

    private void ClientOnUpdate()
    {
        if (player.playerInput.RightClick)
        {
            if (!ClientCheckIsPlayerTargeted())
            {
                player.PlayerStopAttackRequestServerRpc();
                return;
            }
            localTargetPlayer = hit.collider.gameObject.GetComponent<Player>();
            if (ClientCheckIsPlayerTargeted()) player.PlayerAttackRequestServerRpc(localTargetPlayer.playerData.Value.playerID, PlayerAttackData.TargetType.Player);
        }
    }

    private void ServerOnUpdate() => ServerTryAttack();

    public void ClientCheckTargetPlayerRequirements()
    {
        localTargetPlayer = hit.collider.gameObject.GetComponent<Player>();

        if (localTargetPlayer == null) return;

        if (localTargetPlayer.playerData.Value.playerTeam == Player.Owner.playerData.Value.playerTeam)
        {
            player.playerMovement.ClientTryMovementRequest(localTargetPlayer.transform.position);
            localTargetPlayer = null;
        }
        else
        {
            continuouslyCheckRange = true;
            player.playerInput.RightClick = false;
        }
    }
    
    public void ServerTryAttack()
    {
        if (!ServerCheckIsPlayerAttacking()) return;
        if (!ServerCheckTargetRange())
        {
            player.playerMovement.ServerTryMove(GetTargetTransform());
            return;
        }
        else player.playerMovement.StopMovement();
        if (!ServerCheckPlayerAttackCooldown()) return;
        if (!ServerCheckTargetTeam()) return;

        player.StartCoroutine(SmoothRotate());
        ServerAttackTarget();
        player.playerData.Value.playerAttackData.UpdateData(playerLastAttackTime: Time.time);
        player.PlayerAttackAnimationOrderClientRpc();
    }

    private IEnumerator SmoothRotate()
    {
        float x = 0;
        float lastTime;
        while (true)
        {
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(GetTargetTransform().position), 10f * Time.deltaTime);
            lastTime = Time.time;
            yield return null;
            x += Time.time - lastTime;
            if (x > 1f) break;
        }
    }

    private bool ClientCheckIsPlayerTargeted() => Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Player"));
    private bool ClientCheckPlayerAttackCooldown() => Time.time - player.playerData.Value.playerAttackData.playerLastAttackTime >= player.playerData.Value.playerDamageData.playerAttackCooldownTime;
    private bool ClientCheckPlayerRange() => targetDistance <= player.playerData.Value.playerChampionData.range;
    private bool ServerCheckPlayerAttackCooldown() => Time.time - player.playerData.Value.playerAttackData.playerLastAttackTime >= player.playerData.Value.playerDamageData.playerAttackCooldownTime;
    private bool ServerCheckIsPlayerAttacking() => player.playerData.Value.playerAttackData.isPlayerAttacking;
    private PlayerAttackData.TargetType ServerGetPlayerTargetType() => player.playerData.Value.playerAttackData.playerTargetType;

    public Transform GetTargetTransform() => ServerGetPlayerTargetType() switch
    {
        PlayerAttackData.TargetType.Player => ServerGetTargetPlayerTransform(),
        PlayerAttackData.TargetType.Minion => ServerGetTargetMinionTransform(),
        PlayerAttackData.TargetType.Tower => ServerGetTargetTowerTransform(),
        _ => null
    };

    public Transform ServerGetTargetPlayerTransform() => ServerManager.Instance.players[player.playerData.Value.playerAttackData.playerTargetID].transform;
    public Transform ServerGetTargetMinionTransform() => ServerManager.Instance.minions[player.playerData.Value.playerAttackData.playerTargetID].transform;
    public Transform ServerGetTargetTowerTransform() => ServerManager.Instance.towers[player.playerData.Value.playerAttackData.playerTargetID].transform;

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
    public bool ServerCheckTargetPlayerTeam() => player.playerData.Value.playerTeam != ServerManager.Instance.players[player.playerData.Value.playerAttackData.playerTargetID].playerData.Value.playerTeam;
    public bool ServerCheckTargetMinionTeam() => player.playerData.Value.playerTeam != (PlayerData.PlayerTeam) ServerManager.Instance.minions[player.playerData.Value.playerAttackData.playerTargetID].minionData.Value.minionTeam;
    public bool ServerCheckTargetTowerTeam() => player.playerData.Value.playerTeam != (PlayerData.PlayerTeam) ServerManager.Instance.towers[player.playerData.Value.playerAttackData.playerTargetID].towerData.Value.towerTeam;

    public float ServerAttackTarget() => ServerGetPlayerTargetType() switch
    {
        PlayerAttackData.TargetType.Player => ServerAttackTargetPlayer(),
        PlayerAttackData.TargetType.Minion => ServerAttackTargetMinion(),
        PlayerAttackData.TargetType.Tower => ServerAttackTargetTower(),
        _ => 0
    };
    public float ServerAttackTargetPlayer() => ServerManager.Instance.players[player.playerData.Value.playerAttackData.playerTargetID].playerEvent.ApplyDamage(player.playerData.Value.playerDamageData.playerADAttackDamage, player.playerData.Value.playerDamageData.playerAPAttackDamage);
    public float ServerAttackTargetMinion() => ServerManager.Instance.players[player.playerData.Value.playerAttackData.playerTargetID].playerEvent.ApplyDamage(player.playerData.Value.playerDamageData.playerADAttackDamage, player.playerData.Value.playerDamageData.playerAPAttackDamage);
    public float ServerAttackTargetTower() => ServerManager.Instance.players[player.playerData.Value.playerAttackData.playerTargetID].playerEvent.ApplyDamage(player.playerData.Value.playerDamageData.playerADAttackDamage, player.playerData.Value.playerDamageData.playerAPAttackDamage);
}