using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    //public void ToMainScene()
    //{
    //    SceneManager.LoadScene(1);
    //}

    public static void ToSelectionScene()
    {
        SceneManager.LoadScene(1);
    }

    public static void ToEndScene()
    {
        SceneManager.LoadScene(2);
    }
    public static void ToGameOverScene()
    {
        SceneManager.LoadScene(3);
    }
    //public void ToRestartScene()
    //{
    //    SceneManager.LoadScene(0);
    //}

    public static void ToSelectedLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
