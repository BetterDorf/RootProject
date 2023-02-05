using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void StartGame()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (buildIndex > 0)
        {
            Time.timeScale = 1.0f;
            Destroy(transform.parent.gameObject);
        }
        else
        {
            SceneManager.LoadScene(buildIndex + 1);
        }
    }
}
