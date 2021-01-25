using System;
using UnityEngine.UI;
using UnityEngine;

public class TowerStats_Main : MonoBehaviour, IAttackable
{
    public Towers_SO towerDef;

    public Slider sliderHB;
    public Gradient gradient;
    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        EnemyAI.onTryAttackTower = OnAttack;
        towerDef.health = 100;
        towerDef.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        print(towerDef.spriteRenderer.name);

        sliderHB.maxValue = towerDef.health;
        sliderHB.value = towerDef.health;
        fill.color = gradient.Evaluate(1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnAttack(GameObject attacker, Attack attack)
    {
        if (towerDef.health >= 1)
        {
            towerDef.ApplyDamage(attack.Damage);
            sliderHB.value = towerDef.health;
            fill.color = gradient.Evaluate(sliderHB.normalizedValue);
        }
        else
        {
            towerDef.OnTowerDestroy();
        }
    }
}
