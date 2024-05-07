using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if(PlayerUIManager.Instance != null)
        {
            if (PlayerUIManager.Instance.currentHp <= 0)
            {
                SceneManager.LoadScene("DeadScene");
            }

            if (PlayerUIManager.Instance.currentkillcount >= PlayerUIManager.Instance.maxkillcount)
            {
                SceneManager.LoadScene("ClearScene");
            }
        }
        
    }

    

}
