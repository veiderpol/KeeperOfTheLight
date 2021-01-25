using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour, IAttackable
{
    public static CharacterStats instance;
    public Character_SO characterDef;
    public static Action<int> onClickHB;

    public Slider sliderHB;
    public Gradient gradient;
    public Image fill;

    void Start()
    {
        instance = this;
        characterDef.currentHealth = 100;
        ItemManager.onHealth = ApplyHealth;
        ItemManager.onDamage = ApplyDamage;
        EnemyAI.onTryAttack = OnAttack;
        sliderHB.maxValue = characterDef.maxHealth;
        sliderHB.value = characterDef.maxHealth;
        fill.color = gradient.Evaluate(1f);
    }
    public void ApplyHealth(int healthAmount)
    {
        print("HEALTH RESTORED: " + healthAmount);
        characterDef.ApplyHealth(healthAmount);
        UpdateHealthBar();
    }
    public void ApplyDamage(int damageAmount) 
    {
        print("DAMAGE DEALT: " + damageAmount);
        characterDef.ApplyDamage(damageAmount);
        UpdateHealthBar();
    }

    public int GetDamage()
    {
        return characterDef.currentDamage;
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        if (characterDef.currentHealth > 0)
        {
            characterDef.ApplyDamage(attack.Damage);
            UpdateHealthBar();
        }
        else 
        {
            print("Muerto!!");
        }
        
    }
    public void UpdateHealthBar()
    {
        sliderHB.value = characterDef.currentHealth;
        fill.color = gradient.Evaluate(sliderHB.normalizedValue);
    }
    public void ClickHotBar(int itemID) 
    {
        onClickHB(itemID);
    }
}
