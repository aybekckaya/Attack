using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AnaMenü : MonoBehaviour
{
  public void OyunaBasla()
    {
        SceneManager.LoadScene(1);
    }
    public void OyunCikis()
    {
        Application.Quit();
    }
}
