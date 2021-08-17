using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKontrol : MonoBehaviour
{ 

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("OyunBasladiMi"))
       /* PlayerPrefs.SetInt("Taramali_Mermi", 70);
        PlayerPrefs.SetInt("Pompali_Mermi", 50);
        PlayerPrefs.SetInt("Magnum_Mermi", 30);
        PlayerPrefs.SetInt("Sniper_Mermi", 20); */
        PlayerPrefs.SetInt("OyunBasladiMi", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
