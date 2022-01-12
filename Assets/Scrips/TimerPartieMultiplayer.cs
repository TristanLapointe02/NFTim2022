using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerPartieMultiplayer : MonoBehaviourPunCallbacks
{
    static bool partieCommencer;
    public float timer;
    public Text timerAvantFin;
    // Start is called before the first frame update
    void Start()
    {
        partieCommencer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(partieCommencer){
            timer -= Time.deltaTime;
            timerAvantFin.text = Mathf.RoundToInt(timer).ToString(); //modifie le texte en string
            if(timer < 0){
                finPartie();
            }
        }
    }
    // Fonction appelé lorsqu'un joueur join la room
    public override void OnJoinedRoom(){
        if(PhotonNetwork.PlayerList.Length == 2){
            partieCommencer = true;
        }
        print(PhotonNetwork.PlayerList.Length);
    }

    void finPartie(){
        print("Fin partie");
    }
}
