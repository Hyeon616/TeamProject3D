using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterHp : MonoBehaviour
{
    public Transform monstertransfrom;
    public Slider hpbar;
    public float maxHp=5f;
    public float currentHp=5f;
    public GameObject monster;
    public Transform player;


    
    void Start()
    {
        
        maxHp = monster.GetComponent<Monster>().hp;
        hpbar.value = currentHp / maxHp;
    }

    
    void Update()
    {
        transform.LookAt(player.position);
        transform.position = monstertransfrom.position;

        currentHp = monster.GetComponent<Monster>().hp;

        CurrentHp();
    }

    public void GetDamaged(float num)
    {
        currentHp -= num;
        CurrentHp();
    }
    public void CurrentHp()
    {
        hpbar.value = currentHp / maxHp;
    }

}
