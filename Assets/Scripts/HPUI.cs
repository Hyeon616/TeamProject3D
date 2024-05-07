using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HPUI : MonoBehaviour
{
    [SerializeField]
    private Slider hpbar;

    private float MaxHp;
    private float currentHp;
    public GameObject player;

    
    void Start()
    {
        currentHp = player.GetComponent<PlayerController>().BasePlayerHp;
        MaxHp = player.GetComponent<PlayerController>().BasePlayerHp;
        hpbar.value = currentHp / MaxHp;
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GetDamaged(10);
        }

        if(currentHp <= 0)
        {
            SceneManager.LoadScene("DeadScene");
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
