using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Pause : MonoBehaviour
{
    bool IsPause;
    public GameObject pauseMenu;
    public GameObject player;
    void Start()
    {
        IsPause = false;
        player.GetComponent<PlayerController>();
        player.GetComponent<PlayerInput>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsPause ==false)
            {
                PauseGame();
                
                
            }
            else if(IsPause ==true)
            {
                
                ResumeGame();
            }
            
        }
    }
    void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        IsPause = true;
        player.GetComponent<PlayerController>().enabled = false;
        

    }
    void ResumeGame()
    {
        Time.timeScale = 1;
        IsPause = false;
        pauseMenu.SetActive(false);
        player.GetComponent<PlayerController>().enabled = true;
        
    }
}
