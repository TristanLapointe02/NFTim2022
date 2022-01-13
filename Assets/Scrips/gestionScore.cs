using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class gestionScore : MonoBehaviourPunCallbacks
{
    public Text score1;
    public static int pointage1 = 0;
    public Text score2;
    public static int pointage2 = 0;
    public AudioClip sonScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            score1.text = PhotonNetwork.PlayerList[0].NickName + " " + pointage1.ToString();
            score2.text = PhotonNetwork.PlayerList[1].NickName + " " + pointage2.ToString();
        }
    }    

    public void OnTriggerEnter(Collider infoCollision)
    {
        switch (infoCollision.gameObject.tag)
        {
            case "vache":
                if (infoCollision.gameObject.GetComponent<PhotonView>().Owner.NickName == PhotonNetwork.PlayerList[0].NickName)
                {
                    photonView.RPC("AjoutScoreJoueur2", RpcTarget.All, 3);
                }
                else if (infoCollision.gameObject.GetComponent<PhotonView>().Owner.NickName == PhotonNetwork.PlayerList[1].NickName)
                {
                    photonView.RPC("AjoutScoreJoueur1", RpcTarget.All, 3);
                }
                break;
            case "mouton":
                if (infoCollision.gameObject.GetComponent<PhotonView>().Owner.NickName == PhotonNetwork.PlayerList[0].NickName)
                {
                    photonView.RPC("AjoutScoreJoueur2", RpcTarget.All, 5);
                }
                else if (infoCollision.gameObject.GetComponent<PhotonView>().Owner.NickName == PhotonNetwork.PlayerList[1].NickName)
                {
                    photonView.RPC("AjoutScoreJoueur1", RpcTarget.All, 5);
                }
                break;
            case "chien":
                if (infoCollision.gameObject.GetComponent<PhotonView>().Owner.NickName == PhotonNetwork.PlayerList[0].NickName)
                {
                    photonView.RPC("AjoutScoreJoueur2", RpcTarget.All, 10);
                }
                else if (infoCollision.gameObject.GetComponent<PhotonView>().Owner.NickName == PhotonNetwork.PlayerList[1].NickName)
                {
                    photonView.RPC("AjoutScoreJoueur1", RpcTarget.All, 10);
                }
                break;
            case "cheval":
                if (infoCollision.gameObject.GetComponent<PhotonView>().Owner.NickName == PhotonNetwork.PlayerList[0].NickName)
                {
                    photonView.RPC("AjoutScoreJoueur2", RpcTarget.All, 15);
                }
                else if (infoCollision.gameObject.GetComponent<PhotonView>().Owner.NickName == PhotonNetwork.PlayerList[1].NickName)
                {
                    photonView.RPC("AjoutScoreJoueur1", RpcTarget.All, 15);
                }
                break;
            case "lama":
                if (infoCollision.gameObject.GetComponent<PhotonView>().Owner.NickName == PhotonNetwork.PlayerList[0].NickName)
                {
                    photonView.RPC("AjoutScoreJoueur2", RpcTarget.All, 20);
                }
                else if (infoCollision.gameObject.GetComponent<PhotonView>().Owner.NickName == PhotonNetwork.PlayerList[1].NickName)
                {
                    photonView.RPC("AjoutScoreJoueur1", RpcTarget.All, 20);
                }
                break;
            case "zebre":
                if (infoCollision.gameObject.GetComponent<PhotonView>().Owner.NickName == PhotonNetwork.PlayerList[0].NickName)
                {
                    photonView.RPC("AjoutScoreJoueur2", RpcTarget.All, 25);
                }
                else if (infoCollision.gameObject.GetComponent<PhotonView>().Owner.NickName == PhotonNetwork.PlayerList[1].NickName)
                {
                    photonView.RPC("AjoutScoreJoueur1", RpcTarget.All, 25);
                }
                break;
            case "cochon":
                if (infoCollision.gameObject.GetComponent<PhotonView>().Owner.NickName == PhotonNetwork.PlayerList[0].NickName)
                {
                    photonView.RPC("AjoutScoreJoueur2", RpcTarget.All, 4);
                }
                else if (infoCollision.gameObject.GetComponent<PhotonView>().Owner.NickName == PhotonNetwork.PlayerList[1].NickName)
                {
                    photonView.RPC("AjoutScoreJoueur1", RpcTarget.All, 4);
                }
                break;
            default:
                break;
        }
    }

    [PunRPC]
    public void AjoutScoreJoueur1(int score)
    {
        print("ajout score joueur 1");
        pointage1 += score;
        GetComponent<AudioSource>().PlayOneShot(sonScore);
    }

    [PunRPC]
    public void AjoutScoreJoueur2(int score)
    {
        print("ajout score joueur 2");
        pointage2 += score;
        GetComponent<AudioSource>().PlayOneShot(sonScore);
    }
}
