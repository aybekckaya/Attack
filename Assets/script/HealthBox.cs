using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    public List<GameObject> HealthboxPoint = new List<GameObject>();
    public GameObject Healthbox_Kendisi;
    public static bool HealthboxVarmi;
    public float KutuCikmaSuresi;
    // Start is called before the first frame update
    void Start()
    {
        HealthboxVarmi = false;
        StartCoroutine(Healthbox_yap());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Healthbox_yap()
    {
        while (true)
        {
            yield return null;
            yield return new WaitForSeconds(5f);
            if (!HealthboxVarmi)
            {
                int randomsayim = Random.Range(0, 3);
                Instantiate(Healthbox_Kendisi, HealthboxPoint[randomsayim].transform.position, HealthboxPoint[0].transform.rotation);
                HealthboxVarmi = true;
            }
        }
    }
}
    
