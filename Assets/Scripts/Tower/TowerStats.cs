using System;
using UnityEngine.UI;
using UnityEngine;

public class TowerStats : MonoBehaviour, IAttackable
{
    public Towers_SO towerDef;

    public Slider sliderHB;
    public Gradient gradient;
    public Image fill;

    public static Action onTryUpdateTower;
    public static Action onTowerLose;

    void Start()
    {
        this.towerDef.health = 100;
        this.towerDef.alive = true;
        
        towerDef.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        sliderHB.maxValue = this.towerDef.health;
        sliderHB.value = this.towerDef.health;
        fill.color = gradient.Evaluate(1f);
    }
    public void OnAttack(GameObject attacker, Attack attack)
    {
        if (this.towerDef.health >= 0)
        {
            print(attack.Damage);
            this.towerDef.ApplyDamage(attack.Damage);
            sliderHB.value = towerDef.health;

            fill.color = gradient.Evaluate(sliderHB.normalizedValue);
        }
        else
        {
            this.towerDef.OnTowerDestroy();
            if (this.towerDef.spriteRenderer.name == "Main_House")
                onTowerLose();
            this.enabled = false;
           // onTryUpdateTower();
        }
    }
    
}
