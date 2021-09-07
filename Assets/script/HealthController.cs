using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "karakter") 
        {
            FindObjectOfType<GameKontrol>().SaglikAl();
            Destroy(gameObject);

        }
    }
}
