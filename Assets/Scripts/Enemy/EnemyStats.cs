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
    GameObject uiWin;
    public static Action onWin;
    private void OnEnable()
    {
        this.health = 100;
        sliderHB.maxValue = health;
        sliderHB.value = health;
        fill.color = gradient.Evaluate(1f);
        this.anim = GetComponent<Animator>();
        uiWin = GameObject.FindGameObjectWithTag("WinCanvas");
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
       
    }
    IEnumerator HandleDeath() {
        this.anim.SetTrigger("Death");
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
        bool win =  GameManager.onCountDeath();
        if (win)
            onWin();

    }
}
