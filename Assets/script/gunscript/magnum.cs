using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class magnum : MonoBehaviour
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
    public ParticleSystem MermiIzi;
    public ParticleSystem KanEfekt;

    [Header("S?LAH AYARLARI")]
    int ToplamMermiSayisi;
    public int SarjorKapasitesi;
    public int KalanMermi;
    public string SilahinAdi;
    public TextMeshProUGUI ToplamMermi_Text;
    public TextMeshProUGUI KalanMermi_Text;
    bool ZoomVarMi;
    bool ZoomAyar;
    public float DarbeGucu;

    public bool Kovan_ciksinmi;
    public GameObject Kovan_Objesi;
    public GameObject Kovan_CikisNoktasi;
    [Header("OTHERS")]
    int AtilmisOlanMermi;
    public Camera myCam;
    float CamFieldPov;
    public float ZoomPov;

    Mermi_Kutusu_Olustur mermi_kutusu_kod;
    // Start is called before the first frame update
    void Start()
    {
        ToplamMermiSayisi = PlayerPrefs.GetInt(SilahinAdi + "_Mermi");
        Debug.Log(ToplamMermiSayisi);

        Kovan_ciksinmi = false;
        Baslangic_Mermi_Doldur();
        SajorDegistirmeTeknik("NormalYaz");
        Animatorum = GetComponent<Animator>();
        CamFieldPov = myCam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
        {
            if (atesEdebilirmi && Time.time > iceridenAtesEtmeSikligi && KalanMermi != 0)
            {
                AtesEt(false);
                iceridenAtesEtmeSikligi = Time.time + disaridanAtesEtmeSiklik;

            }
            if (KalanMermi == 0)
            {
                MermiBitti.Play();
            }

        }
        if (Input.GetKey(KeyCode.R))
        {
            Sarjordegistir();
            StartCoroutine(ReloadYap());
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            MermiAl();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ZoomVarMi = true;
            Animatorum.SetBool("ZoomYap", true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ZoomVarMi = false;
            Animatorum.SetBool("ZoomYap", false);
            myCam.fieldOfView = CamFieldPov;

        }
        if (ZoomVarMi)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (atesEdebilirmi && Time.time > iceridenAtesEtmeSikligi && KalanMermi != 0)
                {
                    AtesEt(true);
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
                MermiAl();
            }
        }
    }
    void CamZoom()
    {
        myCam.fieldOfView = ZoomPov;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mermi"))
        {
            MermiKaydet(other.transform.gameObject.GetComponent<MermiKutusu>().Olusan_Silahin_Turu, other.transform.gameObject.GetComponent<MermiKutusu>().Olusan_Mermi_Sayisi);
            Mermi_Kutusu_Olustur.MermiKutusuVarmi = false;
            Destroy(other.transform.parent.gameObject);
        }
    }

    void MermiAl()
    {
        RaycastHit hit;
        if (Physics.Raycast(myCam.transform.position, myCam.transform.forward, out hit, 4))
        {
            if (hit.transform.gameObject.CompareTag("Mermi"))
            {
                MermiKaydet(hit.transform.gameObject.GetComponent<MermiKutusu>().Olusan_Silahin_Turu, hit.transform.gameObject.GetComponent<MermiKutusu>().Olusan_Mermi_Sayisi);
                Mermi_Kutusu_Olustur.MermiKutusuVarmi = false;
                Destroy(hit.transform.parent.gameObject);
            }
        }
    }
    void Baslangic_Mermi_Doldur()
    {
        if (ToplamMermiSayisi <= SarjorKapasitesi)
        {


            KalanMermi = ToplamMermiSayisi;
            ToplamMermiSayisi = 0;
            PlayerPrefs.SetInt(SilahinAdi + "_Mermi", 0);

        }
        else
        {
            KalanMermi = SarjorKapasitesi;
            ToplamMermiSayisi -= SarjorKapasitesi;
            PlayerPrefs.SetInt(SilahinAdi + "_Mermi", ToplamMermiSayisi);

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
    void AtesEt(bool ZoomAyar)
    {
        AtesEtmeTeknikIslem( ZoomAyar);
        RaycastHit hit;

        if (Physics.Raycast(myCam.transform.position, myCam.transform.forward, out hit, menzil))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                Instantiate(KanEfekt, hit.point, Quaternion.LookRotation(hit.normal));
                hit.transform.GetComponent<Enemy>().DarbeAl(DarbeGucu);
            }
            else if (hit.transform.gameObject.CompareTag("Devrik"))
            {
                Rigidbody rg = hit.transform.gameObject.GetComponent<Rigidbody>();
                rg.AddForce(-hit.normal * 20f);

                Instantiate(MermiIzi, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                Instantiate(MermiIzi, hit.point, Quaternion.LookRotation(hit.normal));
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
                        PlayerPrefs.SetInt(SilahinAdi + "_Mermi", ToplamMermiSayisi);
                    }
                    else
                    {
                        KalanMermi += ToplamMermiSayisi;
                        ToplamMermiSayisi = 0;
                        PlayerPrefs.SetInt(SilahinAdi + "_Mermi", 0);
                    }
                }
                else
                {
                    ToplamMermiSayisi -= SarjorKapasitesi - KalanMermi;
                    KalanMermi = SarjorKapasitesi;
                    PlayerPrefs.SetInt(SilahinAdi + "_Mermi", ToplamMermiSayisi);
                }

                ToplamMermi_Text.text = ToplamMermiSayisi.ToString();
                KalanMermi_Text.text = KalanMermi.ToString();
                break;

            case "MermiYok":
                if (ToplamMermiSayisi <= SarjorKapasitesi)
                {
                    KalanMermi = ToplamMermiSayisi;
                    ToplamMermiSayisi = 0;
                    PlayerPrefs.SetInt(SilahinAdi + "_Mermi", 0);
                }
                else
                {
                    ToplamMermiSayisi -= SarjorKapasitesi;
                    PlayerPrefs.SetInt(SilahinAdi + "_Mermi", ToplamMermiSayisi);
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
    void AtesEtmeTeknikIslem(bool ZoomAyar)
    {
        if (Kovan_ciksinmi)
        {
            GameObject obje = Instantiate(Kovan_Objesi, Kovan_CikisNoktasi.transform.position, Kovan_CikisNoktasi.transform.rotation);
            Rigidbody rb = obje.GetComponent<Rigidbody>();
            rb.AddRelativeForce(new Vector3(-10f, 1, 0) * 60);
        }
        atesSesi.Play();
        AtesEfekt.Play();
        if (!ZoomAyar)
        {
            Animatorum.Play("magnumates");
            //ZoomAteset animasyon yap
        }
        else
        {
            Animatorum.Play("ZoomAteset");
        }

        KalanMermi--;
        KalanMermi_Text.text = KalanMermi.ToString();

    }
    void MermiKaydet(string silahturu, int mermisayisi)
    {

        MermiAlmaSesi.Play();

        switch (silahturu)
        {
            case "Taramali":
                PlayerPrefs.SetInt("Tarmali_Mermi", PlayerPrefs.GetInt("Tarmali_Mermi") + mermisayisi);
                break;

            case "Pompali":
                PlayerPrefs.SetInt("Pompali_Mermi", PlayerPrefs.GetInt("Pompali_Mermi") + mermisayisi);
                break;

            case "Magnum":
                ToplamMermiSayisi += mermisayisi;
                PlayerPrefs.SetInt(SilahinAdi + "_Mermi", ToplamMermiSayisi);
                SajorDegistirmeTeknik("NormalYaz");
                
                break;

            case "Sniper":
                PlayerPrefs.SetInt("Sniper_Mermi", PlayerPrefs.GetInt("Sniper_Mermi") + mermisayisi);

                break;
        }

    }
}
