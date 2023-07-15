using Unity.Netcode;

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
    }

    private void GenerateMinionData() => minionData.Value = new MinionData();
}
