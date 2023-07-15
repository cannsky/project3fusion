using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Spawner : NetworkBehaviour
{
    public enum Team { Blue, Red, Neutral }
    public enum SpawnedObjectType { Minion }

    public SpawnedObjectType spawnedObjectType;
    public Team team;

    public GameObject minionPrefab;
    public Transform enemySpawner;

    public float cooldown = 25f;

    private void Start() => StartCoroutine(SpawnMinions());

    private IEnumerator SpawnMinions()
    {
        while (true)
        {
            if (!IsServer) yield return new WaitForSeconds(2f);
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    yield return new WaitForSeconds(0.5f);
                    for (int j = 0; j < 100; j++)
                    {
                        if (ServerManager.Instance.minions[j] == null)
                        {
                            GameObject minionGameObject = Instantiate(minionPrefab, transform.position, Quaternion.identity);
                            minionGameObject.GetComponent<NetworkObject>().Spawn();
                            Minion minion = minionGameObject.GetComponent<Minion>();
                            ServerManager.Instance.minions[j] = minion;
                            minion.minionData.Value = new MinionData(j, (MinionData.MinionTeam)team, minion.minionSettings, enemySpawner.position);
                            break;
                        }
                    }
                }
                yield return new WaitForSeconds(cooldown);
            }
        }
    }
}
