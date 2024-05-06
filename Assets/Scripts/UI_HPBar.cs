using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : MonoBehaviour
{
    Monster monster;
    void Start()
    {
        Canvas canvas = GetComponent<Canvas>();

        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        monster = transform.parent.GetComponent<Monster>();
    }


    void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y+0.7f);
        transform.rotation = Camera.main.transform.rotation;

        float ratio = monster.hp / (float)monster.maxHp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        gameObject.GetComponentInChildren<Slider>().value = ratio;
    }



}
