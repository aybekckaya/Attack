using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameKontrol : MonoBehaviour
{ 
    [Header("Silah Ayarlari")]
        public GameObject[] guns;
    public AudioSource DegisimSes;
    [Header("Dusman Ayarlari")]
    public GameObject[] enemys;
    public GameObject[] CikisNoktalari;
    public GameObject[] HedefNoktalari;
    public float DusmanCikmaSuresi;
    public int BaslangicDusmanSayisi;
    public static int KalanDusmanSayisi;
    public TextMeshProUGUI KalanDusman_text;
    [Header("Saglik Ayarlari")]
    float Health=100;
    public Image HealthBar;
    [Header("Di?er Ayarlar")]
    public GameObject GameOverCanvas;
    public GameObject WinCanvas;
    // Start is called before the first frame update
    void Start()
    {
        KalanDusman_text.text = BaslangicDusmanSayisi.ToString();
        KalanDusmanSayisi = BaslangicDusmanSayisi;
        if (PlayerPrefs.HasKey("OyunBasladiMi"))
        {
            PlayerPrefs.SetInt("Taramali_Mermi", 70);
            PlayerPrefs.SetInt("Pompali_Mermi", 50);
            PlayerPrefs.SetInt("Magnum_Mermi", 30);
            PlayerPrefs.SetInt("Sniper_Mermi", 20);
            //PlayerPrefs.SetInt("OyunBasladiMi", 1);
        }
        //if (!PlayerPrefs.HasKey("OyunBasladiMi"))
        //{
        //    PlayerPrefs.SetInt("Taramali_Mermi", 70);
        //    PlayerPrefs.SetInt("Pompali_Mermi", 50);
        //    PlayerPrefs.SetInt("Magnum_Mermi", 30);
        //    PlayerPrefs.SetInt("Sniper_Mermi", 20);
        //    PlayerPrefs.SetInt("OyunBasladiMi", 1);
        //}
        StartCoroutine(DusmanYap());
    }

    internal void Darbeal(object darbeGucu)
    {
        throw new System.NotImplementedException();
    }

    IEnumerator DusmanYap()
    {
            while (true)
        {
            yield return new WaitForSeconds(DusmanCikmaSuresi);
            if (BaslangicDusmanSayisi!=0)
            {
                int enemy = Random.Range(0, 5);
                int CikisNoktasi = Random.Range(0, 2);
                int HedefNoktasi = Random.Range(0, 2);

                GameObject obje = Instantiate(enemys[enemy], CikisNoktalari[CikisNoktasi].transform.position, Quaternion.identity);
                obje.GetComponent<Enemy>().HedefBelirle(HedefNoktalari[HedefNoktasi]);
                BaslangicDusmanSayisi--;
            }
            
        }

    }
    public void DusmanSayisiGuncelle()
    {
        KalanDusmanSayisi--;
        if (KalanDusmanSayisi <= 0)
        {
            WinCanvas.SetActive(true);
            KalanDusman_text.text ="0";
            Time.timeScale = 0;
        }
        else
        {
            KalanDusman_text.text = KalanDusmanSayisi.ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SilahDegistir(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SilahDegistir(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SilahDegistir(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SilahDegistir(3);
        }
    }
    public void Darbeal(float DarbeGucu)
    {
        Health -= DarbeGucu;
        HealthBar.fillAmount = Health/100;
        if (Health <= 0)
        {
            GameOver();
        }
    }
    void GameOver()
    {
        GameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }
    void SilahDegistir(int SiraNo)
    {
        DegisimSes.Play();
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
        guns[SiraNo].SetActive(true);
    }
}
