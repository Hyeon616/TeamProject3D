using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
   
    public GameObject mainTitle;
    public GameObject setting;
    private bool isTitle = true;


    void Update()
    {
        if(isTitle == false)
        {
            mainTitle.SetActive(false);
            setting.SetActive(true);
        }
        else
        {
            mainTitle.SetActive(true);
            setting.SetActive(false);
        }
        

    }
    public void OnClickGameStrat()
    {
        SceneManager.LoadScene("Main");
    }
    public void OnClickSetting()
    {
        isTitle = false;
    }
    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void OnClickX()
    {
        isTitle = true;
    }
    public void OnClickTitle()
    {
        SceneManager.LoadScene("Title");
    }    
    
}
