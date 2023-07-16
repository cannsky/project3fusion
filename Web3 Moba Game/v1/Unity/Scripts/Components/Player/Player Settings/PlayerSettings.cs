using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerSettings
{
    [Header("Player General")]
    public string defaultLayerName = "Default", terrainLayerName = "Terrain";
    [Header("Player Camera")]
    public Vector3 playerCameraOffset = new Vector3(0, 10, -10);
    public float playerCameraMoveSpeed = 0.1f;
    [Header("Player Navmesh Agent")]
    public float angularSpeed = 0f;
    public float acceleration = 1000f;
    public float stoppingDistance = 0f;
    public bool autoBraking = false;
    [Header("Player Movement")]
    public float playerRotationSpeed = 8f;
    [Header("Player SFX")]
    public AudioSource playerAudioSource;
    [Header("Player VFX")]
    public GameObject playerCursorVFX;
    [Header("Player UI")]
    public TMP_Text playerNameText;
    public TMP_Text playerLevelText, playerHealthText, playerManaText;
    public Slider playerHealthSlider, playerManaSlider;
}
