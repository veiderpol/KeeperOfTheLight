using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NewStats", menuName ="Character/Stats", order=1)]
public class Character_SO : ScriptableObject
{
    public bool saveDataOnClose = false;

    public int maxHealth = 0;
    public int currentHealth = 0;

    public int baseDamage = 0;
    public int currentDamage = 0;

    public void ApplyHealth(int healthAmount) {
        currentHealth += healthAmount;

    }
    public void ApplyDamage(int damageAmount) 
    {
        currentHealth -= damageAmount;
    }
}
