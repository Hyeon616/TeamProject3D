using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class BulletCountText : MonoBehaviour
{
    
    private TextMeshProUGUI text;
    public GameObject playerInfo;
    private PlayerController playerController;
   
    void Start()
    {
      text = GetComponent<TextMeshProUGUI>();
      playerController = playerInfo.GetComponent<PlayerController>();
    }

    
    void Update()
    {
        text.text = $"Bullet : {playerController.maxBullet}";
    }
}
