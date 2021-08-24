using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent ajan;
     GameObject hedef;
    public float health;
    public float DusmanDarbeGucu;
    void Start()
    {
        ajan = GetComponent<NavMeshAgent>();
    }
    public void HedefBelirle(GameObject objem)
    {
        hedef = objem;
    }

    // Update is called once per frame
    void Update()
    {
        ajan.SetDestination(hedef.transform.position);
    }
    public void DarbeAl(float DarbeGucu)
    {
        health -= DarbeGucu;
        if (health<=0)
        {
            Dead();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("korunanNesne"))
        {
            Debug.Log("degdi");
            GameObject anaKontrolcum = GameObject.FindWithTag("AnaKontrolcum");
            anaKontrolcum.GetComponent<GameKontrol>().Darbeal(DusmanDarbeGucu);
            Dead();
        }
    }
    void Dead()
    {
       
        Destroy(gameObject);

    }
}
