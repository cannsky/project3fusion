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
            //Vector3 direction = targetPlayer.transform.position - transform.position;
            //transform.rotation = Quaternion.LookRotation(direction);
            transform.position = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, 5f * Time.deltaTime);

            if(Vector3.Distance(transform.position, targetPlayer.transform.position) <= 0.5f)
            {
                gameObject.GetComponent<NetworkObject>().Despawn(); 
            }
        }
    }
}
