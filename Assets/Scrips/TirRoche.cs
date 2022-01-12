using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TirRoche : MonoBehaviour
{
    public AudioSource audioCam;
    public AudioClip sonLancer;
    public Slider cooldownRoche;
    public GameObject roche; 
    public GameObject personnage; 
    public GameObject rocheATiree;
    public float vitesseRoche;
    private bool peutTirer; 

    void Start()
    {
        peutTirer = true;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0) && peutTirer){
            Tir();
            GetComponent<Animator>().SetBool("lancer", true);
            Invoke("RamenerLancer", 1.3f);
            Invoke("SonLancer", 0.3f);
            cooldownRoche.value = 0;
        }
        Physics.IgnoreCollision(rocheATiree.GetComponent<Collider>(), personnage.GetComponent<Collider>());

        if(cooldownRoche.value == 0){
            cooldownRoche.value += 1 * Time.deltaTime;
        }
        
    }
    void Tir(){
        peutTirer = false;
        roche.SetActive(false);
        Invoke("ActiveTir", 3f);
        Invoke("NouvelleRoche", 0.6f);
    }

    void ActiveTir(){
        peutTirer = true;
    }
    void NouvelleRoche()
    {
        GameObject nouvelleRoche = Instantiate(rocheATiree, rocheATiree.transform.position, rocheATiree.transform.rotation);
        nouvelleRoche.SetActive(true);
        nouvelleRoche.GetComponent<Rigidbody>().velocity = nouvelleRoche.transform.forward * vitesseRoche;
        nouvelleRoche.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; 
    }

    void RamenerLancer(){
        GetComponent<Animator>().SetBool("lancer", false);
    }

    void SonLancer(){
        audioCam.PlayOneShot(sonLancer);
    }
}
