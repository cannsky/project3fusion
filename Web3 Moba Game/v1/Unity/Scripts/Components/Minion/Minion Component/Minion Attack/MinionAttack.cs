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
                Player targetPlayer = colliders[0].transform.GetComponent<Player>();
                if ((MinionData.MinionTeam) targetPlayer.playerData.Value.playerTeam != minion.minionData.Value.minionTeam)
                {
                    minion.minionMovement.isTargetDetected = true;
                    minion.minionMovement.target = targetPlayer.transform;
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
            else if ((colliders = Physics.OverlapSphere(minion.transform.position, minion.minionData.Value.minionAttackData.minionAttackRange, LayerMask.GetMask("Tower"))).Length > 0){
                Tower targetTower = colliders[0].transform.GetComponent<Tower>();
                if ((MinionData.MinionTeam) targetTower.towerSettings.towerTeam != minion.minionData.Value.minionTeam)
                {
                    minion.minionMovement.isTargetDetected = true;
                    minion.minionMovement.target = targetTower.transform;
                    GameObject projectileGameObject = minion.InstantiateGameObject(minion.minionSettings.minionProjectilePrefab, minion.transform.position, Quaternion.identity);
                    projectileGameObject.GetComponent<NetworkObject>().Spawn();

                    Projectile projectile = projectileGameObject.GetComponent<Projectile>();
                    projectile.SetTarget(targetTower.transform, Projectile.TargetType.Tower);
                    projectile.projectileSpeed = minion.minionSettings.minionProjectileSpeed;
                    projectile.transform.position = new Vector3(projectile.transform.position.x, projectile.transform.position.y + 0.5f, projectile.transform.position.z);

                    minion.minionData.Value.minionAttackData.UpdateData(Time.time);

                    minion.MinionAttackAnimationOrderClientRpc();
                }
            }
            else if ((colliders = Physics.OverlapSphere(minion.transform.position, minion.minionData.Value.minionAttackData.minionAttackRange, LayerMask.GetMask("Minion"))).Length > 0)
            {
                Minion targetMinion = null;
                foreach (Collider collidedMinion in colliders)
                {
                    if (collidedMinion.transform.GetComponent<Minion>().minionData.Value.minionTeam != minion.minionData.Value.minionTeam)
                    {
                        targetMinion = collidedMinion.transform.GetComponent<Minion>();
                        break;
                    }
                }
                if (targetMinion != null && targetMinion.minionData.Value.minionTeam != minion.minionData.Value.minionTeam)
                {
                    minion.minionMovement.isTargetDetected = true;
                    minion.minionMovement.target = targetMinion.transform;
                    GameObject projectileGameObject = minion.InstantiateGameObject(minion.minionSettings.minionProjectilePrefab, minion.transform.position, Quaternion.identity);
                    projectileGameObject.GetComponent<NetworkObject>().Spawn();

                    Projectile projectile = projectileGameObject.GetComponent<Projectile>();
                    projectile.SetTarget(targetMinion.transform, Projectile.TargetType.Minion);
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
