using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermiKutusu : MonoBehaviour
{
    public List<GameObject> MermiKutusuPoint = new List<GameObject>();
    public GameObject Mermi_Kutusunun_kendisi;

    public static bool Mermi_kutusu_varmi;
    public float Kutu_cikma_suresi;
    public string Olusan_Silahin_Turu;
    public int Olusan_Mermi_sayisi;
    // Start is called before the first frame update
    void Start()
    {
        Mermi_kutusu_varmi = false;
        StartCoroutine(Mermi_Kutusu_yap());

    }
    IEnumerator Mermi_Kutusu_yap()
    {
        while (true)
        {
            yield return null;

            if (!Mermi_kutusu_varmi)
            {
                yield return new WaitForSeconds(Kutu_cikma_suresi);

                int randomsayim = Random.Range(0, 3);
                Instantiate(Mermi_Kutusunun_kendisi, MermiKutusuPoint[randomsayim].transform.position, MermiKutusuPoint[randomsayim].transform.rotation);
                Mermi_kutusu_varmi = true;

            }


        }
    }
}
