using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public static Player Owner;

    public PlayerSettings playerSettings;

    public NetworkVariable<PlayerData> playerData = new NetworkVariable<PlayerData>();

    public PlayerAnimator playerAnimator;
    public PlayerAttack playerAttack;
    public PlayerCoroutine playerCoroutine;
    public PlayerCamera playerCamera;
    public PlayerCursor playerCursor;
    public PlayerEvent playerEvent;
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;
    public PlayerSFX playerSFX;
    public PlayerSpawn playerSpawn;
    public PlayerUI playerUI;
    public PlayerVFX playerVFX;

    public bool isReady;

    public override void OnNetworkSpawn()
    {
        if (IsServer) GeneratePlayerData();
        if (IsClient) playerData.OnValueChanged += OnValueChanged;
        if (IsServer) ServerManager.Instance.players[(int)OwnerClientId] = this;
        StartCoroutine((playerCoroutine = new PlayerCoroutine(this)).playerAwaitSetupCoroutine.Coroutine());
    }

    private void Update()
    {
        if (!isReady) return;
        if (IsOwner || IsServer) playerAttack.OnUpdate();
        if (IsOwner) playerCamera.OnUpdate();
        if (IsOwner) playerCursor.OnUpdate();
        if (IsClient) playerUI.OnUpdate();
        playerMovement.OnUpdate();
    }

    private void LateUpdate()
    {
        if (!isReady) return;
        if (IsOwner) playerInput.OnLateUpdate();
        if (IsOwner) playerCamera.OnLateUpdate();
        if (IsOwner) playerCursor.OnLateUpdate();
    }

    public void GeneratePlayerData()
    {
        playerData.Value = new PlayerData(this, PlayerDataGenerator.GeneratePlayerChampionData(1));
    }
    public void DestroyGameObject(GameObject removedGameObject) => Destroy(removedGameObject);
    public void DestroyGameObject(Animator removedGameObject) => Destroy(removedGameObject);
    public GameObject InstantiateGameObject(GameObject addedGameObject, Vector3 position, Quaternion rotation) => Instantiate(addedGameObject, position, rotation);
    public GameObject InstantiateChampionPrefab()
    {
        return Instantiate(((Champion)PlayerResourceFinder.Find(PlayerResourceFinder.Type.Champion, playerData.Value.playerChampionData.ID)).characterPrefab, transform);
    }
    public void OnValueChanged(PlayerData a, PlayerData b)
    {
        if (!isReady) return;
    }

    //SERVER RPC'S DON'T CHECK ANYTHING!
    //THIS IS TOTALLY UNSAFE!

    [ClientRpc] public void PlayerAttackAnimationOrderClientRpc() => playerAnimator.PlayAttackAnimation("Normal Attack");
    [ClientRpc] public void HandleHitVFXClientRpc(Vector3 position, Quaternion rotation) => playerVFX.PlayVFX(playerSettings.hitVFX, position, rotation, 1f);
    [ServerRpc] public void PlayerMovementRequestServerRpc(Vector2 playerMovementDestination)
    {
        playerData.Value.playerMovementData.UpdateData(playerMovementDestination, Time.time, isMoveRequested: true, isMoving: true);
    }
    [ServerRpc] public void PlayerAnimationStateRequestServerRpc(PlayerAnimationData.PlayerAnimationState playerAnimationState) => playerData.Value.playerAnimationData.UpdateData(playerAnimationState);
    [ServerRpc] public void PlayerAttackRequestServerRpc(int playerTargetID, PlayerAttackData.TargetType playerTargetType)
    {
        playerData.Value.playerAttackData.SetAttackSequenceData(playerTargetID, playerTargetType);
    }
    [ServerRpc]
    public void PlayerStopAttackRequestServerRpc()
    {
        playerData.Value.playerAttackData.StopAttackSequenceData();
    }
}
