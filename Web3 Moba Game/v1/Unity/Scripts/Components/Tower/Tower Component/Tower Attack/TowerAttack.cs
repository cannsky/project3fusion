using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack
{
    private Tower tower;

    public TowerAttack(Tower tower) => this.tower = tower;

    public void OnUpdate()
    {
        if (!tower.IsServer) return;
        if (Time.time <= tower.towerData.Value.towerAttackData.towerLastAttackTime + tower.towerData.Value.towerAttackData.towerAttackCooldown)
        {
            Collider[] colliders = Physics.OverlapSphere(tower.transform.position, tower.towerData.Value.towerAttackData.towerAttackRange, LayerMask.GetMask(tower.towerSettings.playerLayerName));

            foreach (Collider collider in colliders)
            {
                Player targetPlayer = collider.transform.GetComponent<Player>();

                GameObject projectileGameObject = tower.InstantiateGameObject(tower.towerSettings.towerProjectilePrefab, tower.transform.position, Quaternion.identity);

                Projectile projectile = projectileGameObject.GetComponent<Projectile>();
                projectile.SetTarget(targetPlayer);

                tower.towerData.Value.towerAttackData.UpdateData(Time.time);
            }
        }
    }
}
