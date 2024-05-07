using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Slider hpbar;

    [SerializeField]
    public int maxkillcount=5;

    public int currentkillcount;

    public TextMeshProUGUI killcount;    
    private float MaxHp;
    private float currentHp;
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
            GetDamaged(10);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            currentkillcount++;
            Debug.Log($"Ä«¿îÆ® :  {currentkillcount}");
        }

        if(currentHp <= 0)
        {
            SceneManager.LoadScene("DeadScene");
        }

        if(currentkillcount >= maxkillcount)
        {
            SceneManager.LoadScene("ClearScene");
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
