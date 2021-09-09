using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

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
    public GameObject PauseCanvas;
    public static bool OyunDurduMu; 
    // Start is called before the first frame update
    void Start()
    {
        OyunDurduMu = false;
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

    public void KillEnemy(GameObject enemy)
    {
        Destroy(enemy);
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
        if (Input.GetKeyDown(KeyCode.Alpha1)&& !OyunDurduMu)
        {
            SilahDegistir(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !OyunDurduMu)
        {
            SilahDegistir(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !OyunDurduMu)
        {
            SilahDegistir(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && !OyunDurduMu)
        {
            SilahDegistir(3);
        }
        if (Input.GetKeyDown(KeyCode.P) && !OyunDurduMu)
        {
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.O) && OyunDurduMu)
        {
            DevamEt();
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

    public void SaglikAl() 
    {
        Health += 10f;
        HealthBar.fillAmount = Health / 100;
    }
    void GameOver()
    {
        GameOverCanvas.SetActive(true);
        Time.timeScale = 0;
        OyunDurduMu = true;
        Cursor.visible = true;
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
        Cursor.lockState = CursorLockMode.None;
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
    public void BastanBasla()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        OyunDurduMu = true;
        Cursor.visible = false;
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void SaglikDoldur()
    {
        Health = 100;
        HealthBar.fillAmount = Health / 100;
    }
    public void Pause()
    {
        PauseCanvas.SetActive(true);
        Time.timeScale = 0;
        OyunDurduMu = true;
        Cursor.visible = true;
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
        Cursor.lockState = CursorLockMode.None;
    }
    public void DevamEt()
    {
        PauseCanvas.SetActive(false);
        Time.timeScale = 1;
        OyunDurduMu = false;
        Cursor.visible = false;
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void AnaMenu()
    {
        SceneManager.LoadScene(0);
    }
}
