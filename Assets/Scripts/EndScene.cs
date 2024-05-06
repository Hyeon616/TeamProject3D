using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndScene : MonoBehaviour
{
    public static EndScene instance;


    public void VictoryScene()
    {
        SceneManager.LoadScene("ClearScene");
    }
    public void DeadScene()
    {
        SceneManager.LoadScene("DeadScene");
    }
}
