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
    public Vector3 playerCameraOffset = new Vector3(0, 7, -7);
    public float playerCameraMoveSpeed = 0.1f;
    [Header("Player Movement")]
    public float playerRotationSpeed = 8f;
    [Header("Player VFX")]
    public GameObject playerCursorVFX;
    [Header("Player UI")]
    public TMP_Text playerNameText;
    public TMP_Text playerLevelText, playerHealthText, playerManaText, playerTotalManaText;
    public Slider playerHealthSlider, playerManaSlider;
}
