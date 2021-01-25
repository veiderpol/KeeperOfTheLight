using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewAttack", menuName ="Attack/BaseAttack", order =2)]
public class AttackDefinition:ScriptableObject
{

    public float coolDown;
    public float range;
    public float minDamage;
    public float maxDamage;
    public float criticalMultiplier;

    public float criticalChance;

    public Attack CreateAttack(CharacterStats enemyStats, CharacterStats defenderStats)
    {
        float coreDamage = enemyStats.characterDef.baseDamage;
        coreDamage += Random.Range(minDamage, maxDamage);

        bool isCritical = Random.value < criticalChance;

        if (isCritical)
            coreDamage *= criticalMultiplier;
        if (defenderStats != null)
            coreDamage -= defenderStats.characterDef.currentDamage;
        return new Attack((int)coreDamage, isCritical);
    }
}
