using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerPartieMultiplayer : MonoBehaviourPunCallbacks
{
    public static bool partieCommencer = false;
    public float timer;
    public Text timerAvantFin;

    // Update is called once per frame
    void Update()
    {
        if(partieCommencer == true)
        {
            timer -= Time.deltaTime;
            timerAvantFin.text = Mathf.RoundToInt(timer).ToString(); //modifie le texte en string
            if(timer <= 0f){
                photonView.RPC("finPartie", RpcTarget.All);
            }
        }
         if(PhotonNetwork.PlayerList.Length == 2){
            partieCommencer = true;
        }
       
    }
   
    [PunRPC]
    public void finPartie(){
        partieCommencer = false;
        PhotonNetwork.LoadLevel("Fin");
        print("Fin partie");
    }

    /*
     * GetComponent<Animator>().SetBool("victoire", true);
       GetComponent<Animator>().SetBool("defaite", true);
    */
}
