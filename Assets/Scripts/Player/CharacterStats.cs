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
    public static Action onDeath;
    public GameObject menuInGame;
    public GameObject winUI;
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
        EnemyStats.onWin = HandleWin;
        TowerStats.onTowerLose = HandleTowerLose;
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
            Time.timeScale = 0f;
            menuInGame.SetActive(true);
        }
        
    }
    public void HandleTowerLose() {
        menuInGame.SetActive(true);
    }
    public void HandleWin() {
        Time.timeScale = 0f;
        winUI.SetActive(true);
    }
    public void HandleDeath()
    {
        onDeath();
    }
    public void Exit() {
        Application.Quit();
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
