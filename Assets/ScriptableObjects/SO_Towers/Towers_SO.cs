using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
[CreateAssetMenu(fileName ="NewTower", menuName ="Tower/BaseTower",order =4)]
public class Towers_SO : ScriptableObject
{
    public bool alive = true;
    public int health = 0;
    public SpriteRenderer spriteRenderer;

    public void ApplyDamage(int damageAmount) 
    {
        health -= damageAmount;
    }
    public void OnTowerDestroy() {
        alive = false;
        spriteRenderer.color = new Color(0.5f,0.5f,0.5f,1);
    }
}

