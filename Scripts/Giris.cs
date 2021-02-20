using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Giris : MonoBehaviour
{
    [SerializeField]
    private GameObject nasil_oynanir;
    // Start is called before the first frame update
    void Start()
    {
        nasil_oynanir.SetActive(false);
    }
    public void nasil_ac()
    {
        nasil_oynanir.SetActive(true);
    } 
    public void nasil_kapat()
    {
        nasil_oynanir.SetActive(false);
    }
    public void baslat()
    {
        SceneManager.LoadScene(1);
    }
}
