using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi_Kutusu_Olustur : MonoBehaviour
{
    public List<GameObject> MermiKutusuPoint = new List<GameObject>();
    public GameObject Mermi_Kutusunun_Kendisi;
    public static bool MermiKutusuVarmi;
    public float KutuCikmaSuresi;

    // Start is called before the first frame update
    void Start()
    {
        MermiKutusuVarmi = false;
        StartCoroutine(Mermi_Kutusu_yap());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Mermi_Kutusu_yap()
    {
        while (true)
        {
            yield return null;
            yield return new WaitForSeconds(5f);
            if (!MermiKutusuVarmi)
            {
                int randomsayim = Random.Range(0, 4);
                Instantiate(Mermi_Kutusunun_Kendisi, MermiKutusuPoint[randomsayim].transform.position, MermiKutusuPoint[0].transform.rotation);
                MermiKutusuVarmi = true;
            }
        }
       /* while (true)
        {
            MermiKutusuVarmi = true;
           yield return new WaitForSeconds(5f);
            int randomsayim = Random.Range(0, 3);
            Instantiate(Mermi_Kutusunun_Kendisi, MermiKutusuPoint[0].transform.position, MermiKutusuPoint[0].transform.rotation);
        } */
        
    }
}
