using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gun1 : MonoBehaviour

{
    Animator Animatorum;

    [Header("AYARLAR")]
    public bool atesEdebilirmi;
    float iceridenAtesEtmeSikligi;
    public float disaridanAtesEtmeSiklik;
    public float menzil;

    [Header("SESLER")]
    public AudioSource atesSesi;
    public AudioSource SarjorDegisme;
    public AudioSource MermiBitti;
    public AudioSource MermiAlmaSesi;

    [Header("EFECTS")]
    public ParticleSystem AtesEfekt;
    public ParticleSystem Mermi›zi;
    public ParticleSystem KanEfekt;

    [Header("S›LAH AYARLARI")]
    public int ToplamMermiSayisi;
    public int SarjorKapasitesi;
    public int KalanMermi;
    public TextMeshProUGUI ToplamMermi_Text;
    public TextMeshProUGUI KalanMermi_Text;

    public bool Kovan_ciksinmi;
    public GameObject Kovan_Objesi;
    public GameObject Kovan_CikisNoktasi;
    [Header("OTHERS")]
    int AtilmisOlanMermi;
    public Camera myCam;
   
    
    // Start is called before the first frame update
    void Start()
    {
        Kovan_ciksinmi = true;
        KalanMermi = SarjorKapasitesi;

        SajorDegistirmeTeknik("NormalYaz");
        Animatorum = GetComponent<Animator>();
    }
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (atesEdebilirmi && Time.time > iceridenAtesEtmeSikligi && KalanMermi != 0)
            {
                AtesEt();
                iceridenAtesEtmeSikligi = Time.time + disaridanAtesEtmeSiklik;

            }
            if (KalanMermi == 0)
            {
                MermiBitti.Play();
            }

        }
        if (Input.GetKey(KeyCode.R))
        {

            StartCoroutine(ReloadYap());
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            MermiAL();
        }
    }
    void MermiKaydet(string silahturu, int mermisayisi)
    {

        MermiAlmaSesi.Play();

        switch (silahturu)
        {
            case "Taramali":

                ToplamMermiSayisi += mermisayisi;
                SajorDegistirmeTeknik("NormalYaz");

                break;

            case "Pompali":

                break;

            case "Magnum":

                break;

            case "Sniper":

                break;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mermi"))
        {
            MermiKaydet(other.transform.gameObject.GetComponent<MermiKutusu>().Olusan_Silahin_Turu, other.transform.gameObject.GetComponent<MermiKutusu>().Olusan_Mermi_sayisi);
            MermiKutusu.Mermi_kutusu_varmi = false;
            Destroy(other.transform.parent.gameObject);
        }
    }
    
        
    IEnumerator ReloadYap()
    {
        if (KalanMermi < SarjorKapasitesi && ToplamMermiSayisi != 0)
        {
            Animatorum.Play("sarjordegis");
        }
        yield return new WaitForSeconds(.4f);
        if (KalanMermi < SarjorKapasitesi && ToplamMermiSayisi != 0)
        {
            if (KalanMermi != 0)
            {
                SajorDegistirmeTeknik("MermiVar");
            }
            else
            {
                SajorDegistirmeTeknik("MermiYok");
            }

        }
    }


    void AtesEt()
    {
        AtesEtmeTeknik›slem();
        RaycastHit hit;

        if (Physics.Raycast(myCam.transform.position, myCam.transform.forward, out hit, menzil))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                Instantiate(KanEfekt, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if (hit.transform.gameObject.CompareTag("Devrik"))
            {
                Rigidbody rg = hit.transform.gameObject.GetComponent<Rigidbody>();
                rg.AddForce(-hit.normal * 20f);
                
                Instantiate(Mermi›zi, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                Instantiate(Mermi›zi, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
       
    }
    void MermiAL()
    {

        RaycastHit hit;

        if (Physics.Raycast(myCam.transform.position, myCam.transform.forward, out hit, 4))
        {

            if (hit.transform.gameObject.CompareTag("Mermi"))
            {

                MermiKaydet(hit.transform.gameObject.GetComponent<MermiKutusu>().Olusan_Silahin_Turu, hit.transform.gameObject.GetComponent<MermiKutusu>().Olusan_Mermi_sayisi);
                MermiKutusu.Mermi_kutusu_varmi = false;
                Destroy(hit.transform.parent.gameObject);


            }
        }
    }

    void SajorDegistirmeTeknik(string tur)
    {
        switch (tur)
        {
            case "MermiVar":
                if (ToplamMermiSayisi <= SarjorKapasitesi)
                {
                    int olusanTopDeger = KalanMermi + ToplamMermiSayisi;
                    if (olusanTopDeger > SarjorKapasitesi)
                    {
                        KalanMermi = SarjorKapasitesi;
                        ToplamMermiSayisi = olusanTopDeger - SarjorKapasitesi;
                    }
                    else
                    {
                        KalanMermi += ToplamMermiSayisi;
                        ToplamMermiSayisi = 0;
                    }
                }
                else
                {
                    ToplamMermiSayisi -= SarjorKapasitesi - KalanMermi;
                    KalanMermi = SarjorKapasitesi;
                }

                ToplamMermi_Text.text = ToplamMermiSayisi.ToString();
                KalanMermi_Text.text = KalanMermi.ToString();
                break;

            case "MermiYok":
                if (ToplamMermiSayisi <= SarjorKapasitesi)
                {
                    KalanMermi = ToplamMermiSayisi;
                    ToplamMermiSayisi = 0;
                }
                else
                {
                    ToplamMermiSayisi -= SarjorKapasitesi;
                    KalanMermi = SarjorKapasitesi;
                }

                ToplamMermi_Text.text = ToplamMermiSayisi.ToString();
                KalanMermi_Text.text = KalanMermi.ToString();
                break;
            case "NormalYaz":
                ToplamMermi_Text.text = ToplamMermiSayisi.ToString();
                KalanMermi_Text.text = KalanMermi.ToString();
                break;
        }
    }
    void Sarjordegistir()
    {
        SarjorDegisme.Play();

    }
    void AtesEtmeTeknik›slem()
    {
        if (Kovan_ciksinmi)
        {
            GameObject obje = Instantiate(Kovan_Objesi, Kovan_CikisNoktasi.transform.position, Kovan_CikisNoktasi.transform.rotation);
            Rigidbody rb = obje.GetComponent<Rigidbody>();
            rb.AddRelativeForce(new Vector3(-10f, 1, 0) * 60);
        }
        atesSesi.Play();
        AtesEfekt.Play();
        Animatorum.Play("ak47");

        KalanMermi--;
        KalanMermi_Text.text = KalanMermi.ToString();

    }
}
    
