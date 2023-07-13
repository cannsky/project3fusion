using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class Attack : ScriptableObject
{
    public GameObject attackVFX;
    public AudioClip attackSound;
}