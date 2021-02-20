using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mermi : MonoBehaviour
{
    private Rigidbody rb;
    private float speed;
    public int hasar;
    // Start is called before the first frame update
    void Start()
    {
        speed = 15f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!karakter.bitti)
            rb.velocity = transform.forward * speed;
        else
        {
            rb.velocity = transform.forward * 0;
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "dusman" || other.gameObject.tag == "Player" || other.gameObject.tag == "duvar")
            Destroy(gameObject);
    }
}
