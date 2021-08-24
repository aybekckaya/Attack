using System.Collections;
using System.Collections.Generic;
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
    [Header("Saglik Ayarlari")]
    float Health=100;
    public Image HealthBar;
    [Header("Diðer Ayarlar")]
    public GameObject GameOverCanvas;
    // Start is called before the first frame update
    void Start()
    {

       
        if (!PlayerPrefs.HasKey("OyunBasladiMi"))
       PlayerPrefs.SetInt("Taramali_Mermi", 70);
        PlayerPrefs.SetInt("Pompali_Mermi", 50);
        PlayerPrefs.SetInt("Magnum_Mermi", 30);
        PlayerPrefs.SetInt("Sniper_Mermi", 20); 
        PlayerPrefs.SetInt("OyunBasladiMi", 1);
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
            int enemy = Random.Range(0, 5);
            int CikisNoktasi = Random.Range(0, 2);
            int HedefNoktasi = Random.Range(0, 2);

            GameObject obje = Instantiate(enemys[enemy], CikisNoktalari[CikisNoktasi].transform.position, Quaternion.identity);
            obje.GetComponent<Enemy>().HedefBelirle(HedefNoktalari[HedefNoktasi]);
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
