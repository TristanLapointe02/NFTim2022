using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class SpawnAnimaux : MonoBehaviourPunCallbacks
{
    public GameObject vache;
    public GameObject cheval;
    public GameObject lama;
    public GameObject cochon;
    public GameObject chien;
    public GameObject mouton;
    public GameObject zebre;
    public GameObject cochonDeux;
    public GameObject moutonDeux;
    public GameObject lamaDeux;
    public GameObject[] animaux;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(){
        if(photonView.IsMine){
            int animalHasard = Random.Range(0, animaux.Length);
            GameObject nouvelAnimal = PhotonNetwork.Instantiate(animaux[animalHasard].gameObject.name, animaux[animalHasard].transform.position, animaux[animalHasard].transform.rotation, 0, null);
            nouvelAnimal.SetActive(true); 
        }
    }
}
