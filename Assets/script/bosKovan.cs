using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosKovan : MonoBehaviour
{
    AudioSource YereDusmeSesi;
    // Start is called before the first frame update
    void Start()
    {
        YereDusmeSesi = GetComponent<AudioSource>();
        Destroy(gameObject, 2f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Yol"))
        {
            YereDusmeSesi.Play();
            if (!YereDusmeSesi.isPlaying)
            {
                Destroy(gameObject,1f);
            }
            Debug.Log("Yere Carptý");
        }
    }
}
