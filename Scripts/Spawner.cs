using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] dusmanlar, konumlar;

    private void Start()
    {
        StartCoroutine(spawn());
    }
    private IEnumerator spawn()
    {        
        while (!karakter.bitti)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
                Instantiate(dusmanlar[0], konumlar[1].transform.position, transform.rotation);
            if (rand == 1)
                Instantiate(dusmanlar[1], konumlar[0].transform.position, transform.rotation);
            yield return new WaitForSeconds(6);
        }
    }
}
