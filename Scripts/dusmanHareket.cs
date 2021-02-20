using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class dusmanHareket : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject mermi, namlu, silah, kollar;
    private GameObject karakte;
    private float lastfired, firerate;
    private int mermi_sayisi;
    private bool oldu, bitti;
    private int _can = 100;
    [SerializeField]
    private bool silahli;
    [SerializeField]
    Animator kol_anim;
    private karakter k;

    void Start()
    {
        bitti = false;
        k = GameObject.Find("Karakter").GetComponent<karakter>();
        if(!silahli)
        {
            kollar.SetActive(true);
            silah.SetActive(false);
        }
        else
        {
            kollar.SetActive(false);
            silah.SetActive(true);
        }
       
        bar_guncelle(0);
        oldu = false;
        karakte = GameObject.Find("Karakter");
        mermi_sayisi = 10;
        firerate = 1f;
        agent = GetComponent<NavMeshAgent>();
    }
    void FixedUpdate()
    {
        int layerMask = 1 << 8;
        if(!karakter.bitti && !oldu)
        {
            layerMask = ~layerMask;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask) && silahli && !karakter.bitti)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                string tag = hit.collider.gameObject.tag;
                if (tag == "Player" || tag == "gun")
                {
                    agent.speed = 0;
                    if (Time.time - lastfired > 1f / firerate && mermi_sayisi > 0)
                    {
                        lastfired = Time.time;
                        Instantiate(mermi, namlu.transform.position, transform.rotation);
                        mermi_sayisi--;
                    }
                }
                else
                    agent.speed = 2.5f;
            }
            else
            {
                agent.speed = 2.5f;
                if (!silahli)
                    kol_anim.speed = 0;
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            }
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1, layerMask) && !silahli && !karakter.bitti)
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
                agent.speed = 0;
                kol_anim.speed = 1;
            }
            if (mermi_sayisi <= 0)
                Invoke("mermi_doldur", 2f);
        }
        if (!bitti)
            agent.destination = karakte.transform.position;
            
        if (_can == 0 && !karakter.bitti && !oldu)
        {
            bitti = true;
            agent.speed = 0;
            oldu = true;
            agent.enabled = false;
            transform.Rotate(-90, 0, 0);
            silah.GetComponent<BoxCollider>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            Invoke("karakter_sil", 2f);
            
        }
        
    }
    private void karakter_sil()
    {
        Destroy(gameObject);
    }
    private void bar_guncelle(int hasar)
    {        
        if (!karakter.bitti && !oldu)
            _can -= hasar;
    }
    private void mermi_doldur()
    {
        mermi_sayisi = 10;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "mermi")
        {
            bar_guncelle(20);
            k.skor_guncelle = 10;
        }
    }
}
