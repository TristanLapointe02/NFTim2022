using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class TirRoche : MonoBehaviourPunCallbacks
{
    public AudioSource audioCam;
    public AudioClip sonLancer;
    public GameObject roche;
    public GameObject personnage;
    public GameObject rocheATiree;
    public GameObject rocheEnCooldown;
     public GameObject particuleEtourdi;
    public float cooldownRoche;
    bool enCooldowm = false;
    public float vitesseRoche;
    private bool peutTirer; 

    void Start()
    {
        if (photonView.IsMine)
        {
            peutTirer = true;
            rocheEnCooldown = GameObject.Find("rocheCooldown");
            rocheEnCooldown.gameObject.GetComponent<Image>().fillAmount = 0;
        }
        
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (peutTirer == true)
            {
                roche.SetActive(true);
            }

            if (Input.GetKeyUp(KeyCode.Mouse0) && peutTirer)
            {
                Tir();
                GetComponent<Animator>().SetBool("lancer", true);
                enCooldowm = true;
                rocheEnCooldown.gameObject.GetComponent<Image>().fillAmount = 1;
                Invoke("RamenerLancer", 1.3f);
                Invoke("SonLancer", 0.3f);
            }
            Physics.IgnoreCollision(rocheATiree.GetComponent<Collider>(), personnage.GetComponent<Collider>());

            if (enCooldowm)
            {
                rocheEnCooldown.gameObject.GetComponent<Image>().fillAmount -= 1 / cooldownRoche * Time.deltaTime;
                if (rocheEnCooldown.gameObject.GetComponent<Image>().fillAmount <= 0)
                {
                    rocheEnCooldown.gameObject.GetComponent<Image>().fillAmount = 0;
                    enCooldowm = false;
                }
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
        GameObject nouvelleRoche = PhotonNetwork.Instantiate("roches1", rocheATiree.transform.position, rocheATiree.transform.rotation, 0, null);
        Physics.IgnoreCollision(nouvelleRoche.GetComponent<Collider>(), personnage.GetComponent<Collider>());
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

    void OnTriggerEnter(Collider infoCollision) {
        if(infoCollision.gameObject.tag == "roche" && photonView.IsMine){
            print("je touche une roche");
            particuleEtourdi.SetActive(true);
            DeplacementPersonnage.etourdi = true;
            Invoke("DesactiverParticule", 3f);
        }
    }

    void DesactiverParticule(){
        if(photonView.IsMine){
        particuleEtourdi.SetActive(false);
        DeplacementPersonnage.etourdi = false;
        }
    }
}
