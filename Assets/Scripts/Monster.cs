using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    
    public int hp = 5;
    
    public void Damage(int damage)
    {
        hp -= damage;
        
        if(hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
