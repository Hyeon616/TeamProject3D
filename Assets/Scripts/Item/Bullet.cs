using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int bulletDamage;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageAble = collider.GetComponent<IDamageable>();
        if (damageAble != null)
            damageAble.Damage(1);
    }
}
