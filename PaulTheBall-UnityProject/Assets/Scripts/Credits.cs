using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Credits : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
}
