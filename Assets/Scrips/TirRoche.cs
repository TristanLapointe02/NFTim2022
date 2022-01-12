using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TirRoche : MonoBehaviour
{
    public AudioSource audioCam;
    public AudioClip sonLancer;
    public GameObject roche; 
    public GameObject personnage; 
    public GameObject rocheATiree;
    public Image rocheEnCooldown;
    public float cooldownRoche;
    bool enCooldowm = false;
    public float vitesseRoche;
    private bool peutTirer; 

    void Start()
    {
        peutTirer = true;
        rocheEnCooldown.fillAmount = 0;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0) && peutTirer){
            Tir();
            GetComponent<Animator>().SetBool("lancer", true);
            enCooldowm = true;
            rocheEnCooldown.fillAmount = 1;
            Invoke("RamenerLancer", 1.3f);
            Invoke("SonLancer", 0.3f);
        }
        Physics.IgnoreCollision(rocheATiree.GetComponent<Collider>(), personnage.GetComponent<Collider>());

        if(enCooldowm){
            rocheEnCooldown.fillAmount -= 1 / cooldownRoche * Time.deltaTime;
            if(rocheEnCooldown.fillAmount <= 0){
                rocheEnCooldown.fillAmount = 0;
                enCooldowm = false;
            }
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
