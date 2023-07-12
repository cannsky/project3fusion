using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAnimationData : INetworkSerializable
{
    public enum PlayerAnimationState { Idle, Run, Attack, SkillQ, SkillW, SkillE, SkillR }

    public PlayerAnimationState playerAnimationState;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter => serializer.SerializeValue(ref playerAnimationState);

    public void UpdateData(PlayerAnimationState playerAnimationState) => this.playerAnimationState = playerAnimationState;
}
