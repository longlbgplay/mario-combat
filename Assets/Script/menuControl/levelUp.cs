using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelUp : MonoBehaviour
{
    public int level;
    public string levelToLoad;
    public bool useIntToLoad = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            LoadScene();
        }
    }
    void LoadScene()
    {
        if (useIntToLoad)
        {
            SceneManager.LoadScene(level);
        }
        else
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
