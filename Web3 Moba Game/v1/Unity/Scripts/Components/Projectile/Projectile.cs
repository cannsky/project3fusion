using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    public Tower tower;
    public Player targetPlayer;

    public void SetTarget(Player targetPlayer) => this.targetPlayer = targetPlayer;

    public void Update()
    {
        if (IsServer)
        {
            Vector3 direction = targetPlayer.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
