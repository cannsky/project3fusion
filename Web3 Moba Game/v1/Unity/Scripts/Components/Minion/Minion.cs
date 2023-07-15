using Unity.Netcode;
using UnityEngine;

public class Minion : NetworkBehaviour
{
    public NetworkVariable<MinionData> minionData = new NetworkVariable<MinionData>();

    public MinionSettings minionSettings;

    public MinionAnimator minionAnimator;
    public MinionAttack minionAttack;
    public MinionCoroutine minionCoroutine;
    public MinionEvent minionEvent;
    public MinionMovement minionMovement;
    public MinionUI minionUI;

    public bool isReady;

    private void Start()
    {
        StartCoroutine((minionCoroutine = new MinionCoroutine(this)).minionAwaitSetupCoroutine.Coroutine());
    }

    private void Update()
    {
        if (!isReady) return;
        if (IsServer) minionAttack.OnUpdate();
        if (IsClient) minionAnimator.OnUpdate();
        if (IsServer) minionMovement.OnUpdate();
    }

    public GameObject InstantiateGameObject(GameObject addedGameObject, Vector3 position, Quaternion rotation) => Instantiate(addedGameObject, position, rotation);

    [ClientRpc] public void MinionAttackAnimationOrderClientRpc() => minionAnimator.PlayAttackAnimation("Normal Attack");
}
