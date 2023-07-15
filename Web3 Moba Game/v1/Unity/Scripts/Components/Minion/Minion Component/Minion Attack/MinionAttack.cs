using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MinionAttack
{
    private Minion minion;

    public MinionAttack(Minion minion) => this.minion = minion;

    public void OnUpdate()
    {
        if (!minion.IsServer) return;
        if (Time.time >= minion.minionData.Value.minionAttackData.minionLastAttackTime + minion.minionData.Value.minionAttackData.minionAttackCooldown)
        {
            Collider[] colliders = Physics.OverlapSphere(minion.transform.position, minion.minionData.Value.minionAttackData.minionAttackRange, LayerMask.GetMask("Player"));

            if (colliders.Length > 0)
            {
                minion.minionMovement.isTargetDetected = true;
                Player targetPlayer = colliders[0].transform.GetComponent<Player>();
                minion.minionMovement.target = targetPlayer.transform;

                if ((MinionData.MinionTeam) targetPlayer.playerData.Value.playerTeam != minion.minionData.Value.minionTeam)
                {
                    GameObject projectileGameObject = minion.InstantiateGameObject(minion.minionSettings.minionProjectilePrefab, minion.transform.position, Quaternion.identity);
                    projectileGameObject.GetComponent<NetworkObject>().Spawn();

                    Projectile projectile = projectileGameObject.GetComponent<Projectile>();
                    projectile.SetTarget(targetPlayer.transform, Projectile.TargetType.Player);
                    projectile.projectileSpeed = minion.minionSettings.minionProjectileSpeed;
                    projectile.transform.position = new Vector3(projectile.transform.position.x, projectile.transform.position.y + 0.5f, projectile.transform.position.z);

                    minion.minionData.Value.minionAttackData.UpdateData(Time.time);

                    minion.MinionAttackAnimationOrderClientRpc();
                }
            }
            else
            {
                minion.minionMovement.isTargetDetected = false;
            }
        }
    }
}
