using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IAttackable
{
    #region

    #endregion
    Enemy_SO enemyDef;
    public Slider sliderHB;
    public Gradient gradient;
    public Image fill;
    float health = 0;
    Animator anim;
    private void OnEnable()
    {
        this.health = 100;
        sliderHB.maxValue = health;
        sliderHB.value = health;
        fill.color = gradient.Evaluate(1f);
        this.anim = GetComponent<Animator>();
    }
    public void OnAttack(GameObject attacker, Attack attack)
    {
        if (this.health >= 0) 
        {
            this.health = this.health - attack.Damage;

            this.sliderHB.value = this.health;
            fill.color = gradient.Evaluate(sliderHB.normalizedValue);
        }
        else 
        {
            StartCoroutine(HandleDeath());
        }
       
        //Debug.LogFormat("{0} attacked {1} for {2} damage.", attacker.name, name, attack.Damage);
    }
    IEnumerator HandleDeath() {
        this.anim.SetTrigger("Death");
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
}
