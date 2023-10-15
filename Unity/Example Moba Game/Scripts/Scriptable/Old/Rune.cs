using Unity.Netcode;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rune", menuName = "Project3Fusion/Rune")]
public class Rune : ScriptableObject
{
    public enum Type { AD, AP }
    public int ID;
    public Type type;
    public float extraTotalHealth;
    public float extraHealthRegenerationSpeed;
    public float extraHealthStoleMultiplier;
    public float extraTotalMana;
    public float extraManaRegenerationSpeed;
    public float extraAttackSpeed;
    public float extraADAttackDamage;
    public float extraAPAttackDamage;
    public float extraADArmor;
    public float extraAPArmor;
    public float extraADArmorPiercing;
    public float extraAPArmorPiercing;
    public float extraMovementSpeed;
}