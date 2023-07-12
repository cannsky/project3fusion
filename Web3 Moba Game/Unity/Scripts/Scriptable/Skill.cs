using Unity.Netcode;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class Skill : ScriptableObject
{
    public enum SkillType { AD, AP }
    public int ID;
    public SkillType skillType;
    public float radius;
    public float range;
    public float attackDamage;
    public float armor;
    public float startTime;
    public float endTime;
}