using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NewEnemy", menuName ="Enemy/BaseEnemy", order =3)]
public class Enemy_SO : ScriptableObject
{
    public bool attackEnemy = false;

    public int health = 0;
    public float damage = 0;

    public void ApplyDamage(int damageAmount) 
    {
        health -= damageAmount;
    }
    public Attack CreateAttack() 
    {
        return new Attack((int) damage, false);
    }
}
