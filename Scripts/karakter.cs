using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class karakter : MonoBehaviour
{
    private float speed;
    public GameObject mermi;
    public GameObject namlu;
    public Image _canBar, canB, sarjor_img;
    private Vector3 canBarKonum;
    private int _can, mermi_sayisi;
    public static bool bitti;

    [SerializeField]
    private GameObject GameOverPanel;
    public static int skor;
    [SerializeField]
    private Text skor_txt, skor_txt2, highskor_txt, mermi_txt;
    private float lastfired, firerate;
    private Rigidbody rb;
    public int can { get { return _can; } set { _can -= value; } }

    public int skor_guncelle { get { return skor; } set {  skor += value; skor_txt.text = skor.ToString(); } }
    public void bar_guncelle(int hasar)
    {
        if (_can > 0 && !bitti)
        {
            _can -= hasar;
            _canBar.fillAmount = _can * .01f;
        }        
    }
    private void baslangic_degerleri()
    {
        skor = 0;
        sarjor_img.fillAmount = 0;
        _can = 100;
        mermi_sayisi = 20;
        firerate = 5f;
        bitti = false;
        skor_guncelle = 0;
        bar_guncelle(0);
        speed = .17f;
    }
    // Start is called before the first frame update
    void Awake()
    {
        baslangic_degerleri();
        rb = GetComponent<Rigidbody>();
        mermi_txt.text = mermi_sayisi.ToString();
    }
    private void Update()
    {
        if (transform.position.z > 13)
            transform.position = new Vector3(transform.position.x, 1, 13);
        if (transform.position.z < -15)
            transform.position = new Vector3(transform.position.x, 1, -15);
        if (_can <= 0) 
        {            
            bitti = true;
            transform.eulerAngles = new Vector3(-90, transform.eulerAngles.y, 0);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<BoxCollider>().enabled = false;
            GameOverPanel.SetActive(true);
            skor_txt2.text = "Score :" + skor_txt.text;
            int hs = PlayerPrefs.GetInt("hs");
            if(skor > hs)
            {
                hs = skor;
                PlayerPrefs.SetInt("hs", hs);
            }
            highskor_txt.text = "Highscore :" + hs.ToString();
        }
        if (!bitti)
        {
            canBarKonum = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
            canB.transform.position = Camera.main.WorldToScreenPoint(canBarKonum);
            if (Input.GetKey(KeyCode.Space) && Time.time - lastfired > 1f / firerate && mermi_sayisi > 0)
            {
                lastfired = Time.time;
                Instantiate(mermi, namlu.transform.position, transform.rotation);
                mermi_sayisi--;
                mermi_txt.text = mermi_sayisi.ToString();
            }
            if (mermi_sayisi <= 0 || Input.GetKeyDown(KeyCode.R))
            {
                mermi_sayisi = 0;
                mermi_txt.text = mermi_sayisi.ToString();
                Vector3 sarjor_konum = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
                sarjor_img.transform.position = Camera.main.WorldToScreenPoint(sarjor_konum);
                sarjor_img.fillAmount += .5f * Time.deltaTime;
                if (sarjor_img.fillAmount == 1)
                    mermi_doldur();
            }
        }
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!bitti)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                transform.Translate(0, 0, Input.GetAxis("Vertical") * speed);//Input.GetAxis("Horizontal") * speed
            if (Input.GetKey(KeyCode.D))
                transform.Rotate(new Vector3(0, 3f, 0));
            if (Input.GetKey(KeyCode.A))
                transform.Rotate(new Vector3(0, -3f, 0));
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "mermi_dusman" && !bitti)
            bar_guncelle(10);
        if (other.gameObject.tag == "kol" && !bitti)
            bar_guncelle(5);
    }
    private void mermi_doldur()
    {
        mermi_sayisi = 20;
        mermi_txt.text = mermi_sayisi.ToString();
        sarjor_img.fillAmount = 0;
    }
    public void restart()
    {
        bitti = false;
        SceneManager.LoadScene(1);
    }
    public void menu()
    {
        SceneManager.LoadScene(0);
    }
}
