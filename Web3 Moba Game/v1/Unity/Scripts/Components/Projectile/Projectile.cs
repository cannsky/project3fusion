using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    public Tower tower;
    public Player targetPlayer;
    public float projectileSpeed;

    public void SetTarget(Player targetPlayer) => this.targetPlayer = targetPlayer;

    public void Update()
    {
        if (IsServer)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, projectileSpeed * Time.deltaTime);

            if(Vector3.Distance(transform.position, targetPlayer.transform.position) <= 0.5f)
            {
                targetPlayer.playerEvent.ApplyDamage(10, 0);
                gameObject.GetComponent<NetworkObject>().Despawn(); 
            }
        }
    }
}
