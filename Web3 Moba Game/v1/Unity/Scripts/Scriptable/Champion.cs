using Unity.Netcode;
using UnityEngine;

[CreateAssetMenu(fileName = "New Champion", menuName = "Champion")]
public class Champion : ScriptableObject
{
    public enum ChampionType { Melee, Ranged }
    public enum DamageType { AD, AP }
    public Skill[] skills = new Skill[4];
    public GameObject characterPrefab;
    public int ID;
    public ChampionType championType;
    public DamageType damageType;
    public float range;
    public float attackCooldownTime;
    public float adAttackDamage;
    public float apAttackDamage;
    public float totalHealth;
    public float healthRegenerationSpeed;
    public float healthStoleMultiplier;
    public float totalMana;
    public float manaRegenerationSpeed;
    public float adArmor;
    public float apArmor;
    public float adArmorPiercing;
    public float apArmorPiercing;
    public float movementSpeed;
}