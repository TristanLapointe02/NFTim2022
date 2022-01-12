using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirRoche : MonoBehaviour
{
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
        }
        Physics.IgnoreCollision(rocheATiree.GetComponent<Collider>(), personnage.GetComponent<Collider>());
        
    }
    void Tir(){
        peutTirer = false;
        roche.SetActive(false);
        Invoke("ActiveTir", 2f);
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
}
