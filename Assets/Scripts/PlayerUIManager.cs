using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUIManager : Singleton<PlayerUIManager>
{
    [SerializeField]
    private Slider hpbar;

    [SerializeField]
    public int maxkillcount = 5;

    public int currentkillcount;

    public TextMeshProUGUI killcount;
    public float MaxHp;
    public float currentHp;
    public GameObject player;


    void Start()
    {
        currentHp = player.GetComponent<PlayerController>().BasePlayerHp;
        MaxHp = player.GetComponent<PlayerController>().BasePlayerHp;
        hpbar.value = currentHp / MaxHp;
        currentkillcount = 0;
    }


    void Update()
    {
        killcount.text = $" {currentkillcount} / {maxkillcount}";

        if (Input.GetKeyDown(KeyCode.L))
        {
            GetDamaged(1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            currentkillcount++;
            Debug.Log($"Ä«¿îÆ® :  {currentkillcount}");
        }

    }

    public void GetDamaged(float num)
    {
        currentHp -= num;

        CurrentHp();
    }
    public void CurrentHp()
    {
        hpbar.value = currentHp / MaxHp;
    }
}
