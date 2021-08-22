using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent ajan;
     GameObject hedef;
    public float health;
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
        if(other.gameObject.CompareTag)
    }
    void Dead()
    {
       
        Destroy(gameObject);

    }
}
