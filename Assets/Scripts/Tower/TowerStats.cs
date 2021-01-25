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

    void Start()
    {
        this.towerDef.health = 100;
        towerDef.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        sliderHB.maxValue = this.towerDef.health;
        sliderHB.value = this.towerDef.health;
        fill.color = gradient.Evaluate(1f);
    }
    public void OnAttack(GameObject attacker, Attack attack)
    {
        if (towerDef.health >= 0)
        {
            this.towerDef.ApplyDamage(attack.Damage);
            sliderHB.value = towerDef.health;

            fill.color = gradient.Evaluate(sliderHB.normalizedValue);
        }
        else
        {
            this.towerDef.OnTowerDestroy();
            this.enabled = false;
           // onTryUpdateTower();
        }
    }

}
