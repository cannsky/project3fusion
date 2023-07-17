using Unity.Netcode;
using UnityEditor;
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
    public MinionVFX minionVFX;

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
        if (IsClient) minionUI.OnUpdate();
    }

    public GameObject InstantiateGameObject(GameObject addedGameObject, Vector3 position, Quaternion rotation) => Instantiate(addedGameObject, position, rotation);
    public void DestroyGameObject(GameObject removedGameObject) => Destroy(removedGameObject);

    [ClientRpc] public void HandleHitVFXClientRpc(Vector3 position, Quaternion rotation) => minionVFX.PlayVFX(minionSettings.hitVFX, position, rotation, 1f);

    [ClientRpc]
    public void MinionAttackAnimationOrderClientRpc()
    {
        if (minionAnimator != null) minionAnimator.PlayAttackAnimation("Normal Attack");
    }
}
