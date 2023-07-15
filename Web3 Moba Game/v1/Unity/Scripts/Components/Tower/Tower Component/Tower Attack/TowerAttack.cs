using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TowerAttack
{
    private Tower tower;

    public TowerAttack(Tower tower) => this.tower = tower;

    public void OnUpdate()
    {
        if (!tower.IsServer) return;
        if (Time.time >= tower.towerData.Value.towerAttackData.towerLastAttackTime + tower.towerData.Value.towerAttackData.towerAttackCooldown)
        {
            Collider[] colliders = Physics.OverlapSphere(tower.transform.position, tower.towerData.Value.towerAttackData.towerAttackRange, LayerMask.GetMask(tower.towerSettings.playerLayerName));
            
            foreach (Collider collider in colliders)
            {
                Player targetPlayer = collider.transform.GetComponent<Player>();

                if ((TowerSettings.Team) targetPlayer.playerData.Value.playerTeam == tower.towerSettings.towerTeam) continue;

                GameObject projectileGameObject = tower.InstantiateGameObject(tower.towerSettings.towerProjectilePrefab, tower.transform.position, Quaternion.identity);
                projectileGameObject.GetComponent<NetworkObject>().Spawn();

                Projectile projectile = projectileGameObject.GetComponent<Projectile>();
                projectile.SetTarget(targetPlayer.transform, Projectile.TargetType.Player);
                projectile.projectileSpeed = tower.towerSettings.projectileSpeed;
                projectile.transform.position = new Vector3(projectile.transform.position.x, projectile.transform.position.y + tower.towerSettings.towerAttackHeight, projectile.transform.position.z);

                tower.towerData.Value.towerAttackData.UpdateData(Time.time);
            }

            colliders = Physics.OverlapSphere(tower.transform.position, tower.towerData.Value.towerAttackData.towerAttackRange, LayerMask.GetMask("Minion"));

            foreach (Collider collider in colliders)
            {
                Minion targetMinion = collider.transform.GetComponent<Minion>();

                if ((TowerSettings.Team) targetMinion.minionData.Value.minionTeam == tower.towerSettings.towerTeam) continue;

                GameObject projectileGameObject = tower.InstantiateGameObject(tower.towerSettings.towerProjectilePrefab, tower.transform.position, Quaternion.identity);
                projectileGameObject.GetComponent<NetworkObject>().Spawn();

                Projectile projectile = projectileGameObject.GetComponent<Projectile>();
                projectile.SetTarget(targetMinion.transform, Projectile.TargetType.Minion);
                projectile.projectileSpeed = tower.towerSettings.projectileSpeed;
                projectile.transform.position = new Vector3(projectile.transform.position.x, projectile.transform.position.y + tower.towerSettings.towerAttackHeight, projectile.transform.position.z);

                tower.towerData.Value.towerAttackData.UpdateData(Time.time);
            }
        }
    }
}
