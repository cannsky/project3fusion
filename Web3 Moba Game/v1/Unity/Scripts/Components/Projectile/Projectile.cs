using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    public enum TargetType { Player, Minion, Tower }
    public TargetType targetType;
    public Tower tower;
    public Transform target;
    public float projectileSpeed;
    public float playerAttackDamage = 10f, minionAttackDamage = 5f, towerAttackDamage = 20f;

    public void SetTarget(Transform target, TargetType targetType)
    {
        this.target = target;
        this.targetType = targetType;
    }

    public void Update()
    {
        if (IsServer)
        {
            if (target == null)
            {
                gameObject.GetComponent<NetworkObject>().Despawn();
                return;
            }
            if (target != null) transform.position = Vector3.MoveTowards(transform.position, target.position, projectileSpeed * Time.deltaTime);

            if(Vector3.Distance(transform.position, target.position) <= 0.5f)
            {
                switch(targetType)
                {
                    case TargetType.Player:
                        target.gameObject.GetComponent<Player>().playerEvent.ApplyDamage(playerAttackDamage, 0);
                        break;
                    case TargetType.Minion:
                        target.gameObject.GetComponent<Minion>().minionEvent.ApplyDamage(minionAttackDamage, 0);
                        break;
                    case TargetType.Tower:
                        target.gameObject.GetComponent<Tower>().towerEvent.ApplyDamage(towerAttackDamage, 0);
                        break;
                    default:
                        Debug.Log("Default...");
                        break;
                }
                gameObject.GetComponent<NetworkObject>().Despawn();
            }
        }
    }
}
