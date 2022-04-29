using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenu : MonoBehaviour
{
    [System.Obsolete]
    public void playBtn()
    {
        Application.LoadLevel("level1-1");
    }
    public void quitBtn()
    {
        Application.Quit();
    }
}
