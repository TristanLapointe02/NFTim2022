using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class DeplacementPersonnage : MonoBehaviourPunCallbacks
{
    public Rigidbody rigidbodyPerso;
    public GameObject camera3emePersonne;
    public GameObject main;
    float vitesseDeplacement;
    public float hauteurSaut;
    public float ajoutGravite;
    private float forceDuSaut;
    private bool auSol;
    public bool saut;
    public static bool etourdi;
    public Text score1;
    public int pointage1 = 0;
    public Text score2;
    public int pointage2 = 0;
    public bool onTientAnimal;
    public GameObject rondVert;
    public GameObject rondMauve;
    public string animalPris;

    void Start()
    {
        //rondVert = GameObject.Find("RondVert");
        //rondMauve = GameObject.Find("RondMauve");
        score1 = GameObject.Find("ScoreJoueur1").GetComponent<Text>();
        score2 = GameObject.Find("ScoreJoueur2").GetComponent<Text>();
        rigidbodyPerso = GetComponent<Rigidbody>();

        //Activer la cam�ra localement
        if (photonView.IsMine)
        {
            camera3emePersonne.SetActive(true);
        }
    }

    void Update()
    {
        /*if (PhotonNetwork.CountOfPlayers == 1)
        {
            score1.text = PhotonNetwork.PlayerList[0].NickName + " " + pointage1;
        }*/
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            score1.text = PhotonNetwork.PlayerList[0].NickName + " " + pointage1.ToString();
            score2.text = PhotonNetwork.PlayerList[1].NickName + " " + pointage2.ToString();
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[0] && photonView.IsMine)
            {
                //rondVert.gameObject.SetActive(true);
                //rondMauve.gameObject.SetActive(false);
            }
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[1] && photonView.IsMine)
            {
                //rondVert.gameObject.SetActive(false);
                //rondMauve.gameObject.SetActive(true);
            }
        }


        if (photonView.IsMine)
        {
            float hDeplacement = Input.GetAxisRaw("Horizontal");
            float vDeplacement = Input.GetAxisRaw("Vertical");
            Vector3 directionDep = camera3emePersonne.transform.forward * vDeplacement + camera3emePersonne.transform.right * hDeplacement;
            directionDep.y = 0;
            if (!etourdi)
            {
                if (directionDep != Vector3.zero)
                {
                    transform.forward = directionDep;
                    rigidbodyPerso.velocity = (transform.forward * vitesseDeplacement) + new Vector3(0, rigidbodyPerso.velocity.y, 0);
                }
                else
                {
                    rigidbodyPerso.velocity = new Vector3(0, rigidbodyPerso.velocity.y, 0);
                }
            }
            else
            {
                if (directionDep != Vector3.zero)
                {
                    transform.forward = directionDep;
                    rigidbodyPerso.velocity = (-transform.forward * vitesseDeplacement) + new Vector3(0, rigidbodyPerso.velocity.y, 0);
                }
                else
                {
                    rigidbodyPerso.velocity = new Vector3(0, rigidbodyPerso.velocity.y, 0);
                }
            }

            if (vitesseDeplacement <= 7f && (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")))
            {
                vitesseDeplacement += 0.08f;
                GetComponent<Animator>().SetBool("course", true);
            }
            else if (vitesseDeplacement >= 5 && (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")))
            {
                vitesseDeplacement = 7;
            }
            else
            {
                vitesseDeplacement = 0;
                GetComponent<Animator>().SetBool("course", false);
            }

            RaycastHit infoCollision;
            auSol = Physics.SphereCast(transform.position + new Vector3(0f, 0.5f, 0f), 0.2f, -Vector3.up, out infoCollision, 0.8f);

            GetComponent<Animator>().SetBool("sauter", !auSol);

            if (Input.GetKeyDown(KeyCode.Space) && auSol)
            {
                forceDuSaut = hauteurSaut;
                saut = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            if (auSol)
            {
                GetComponent<Rigidbody>().AddRelativeForce(0f, forceDuSaut, 0f, ForceMode.VelocityChange);
                rigidbodyPerso.drag = 5;
                rigidbodyPerso.angularDrag = 5;
            }
            else
            {
                GetComponent<Rigidbody>().AddRelativeForce(0f, ajoutGravite, 0f, ForceMode.VelocityChange);
            }
            forceDuSaut = 0f;
        }
    }

    public void OnTriggerEnter(Collider infoCollision)
    {
        if ((infoCollision.gameObject.tag == "lama" || infoCollision.gameObject.tag == "cheval" ||
        infoCollision.gameObject.tag == "chien" || infoCollision.gameObject.tag == "mouton" ||
        infoCollision.gameObject.tag == "vache" || infoCollision.gameObject.tag == "zebre" ||
        infoCollision.gameObject.tag == "pug" || infoCollision.gameObject.tag == "cochon") && onTientAnimal == false && photonView.IsMine)
        {
            GetComponent<Animator>().SetBool("animaux", true);
            infoCollision.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            onTientAnimal = true;
            TirRoche.peutTirer = false;

        }
        if (onTientAnimal == true && infoCollision.gameObject.name == "CAGE1")
        {
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[0])
            {
                TirRoche.peutTirer = true;
                print("etape1");
                switch (animalPris)
                {
                    case "vache":
                        photonView.RPC("AjoutScoreJoueur1", RpcTarget.All, 3);
                        break;
                    case "mouton":
                        photonView.RPC("AjoutScoreJoueur1", RpcTarget.All, 5);
                        break;
                    case "chien":
                        photonView.RPC("AjoutScoreJoueur1", RpcTarget.All, 10);
                        break;
                    case "cheval":
                        photonView.RPC("AjoutScoreJoueur1", RpcTarget.All, 15);
                        break;
                    case "lama":
                        photonView.RPC("AjoutScoreJoueur1", RpcTarget.All, 20);
                        break;
                    case "zebre":
                        photonView.RPC("AjoutScoreJoueur1", RpcTarget.All, 25);
                        break;
                    case "cochon":
                        photonView.RPC("AjoutScoreJoueur1", RpcTarget.All, 4);
                        break;
                    default:
                        break;
                }
            }
            if (photonView.IsMine)
            {
                GetComponent<Animator>().SetBool("animaux", false);
            } 
            onTientAnimal = false;
        }

        if (infoCollision.gameObject.layer == 6)
        {
            print("tag de l'animal" + animalPris);
            animalPris = infoCollision.gameObject.tag.ToString();
        }

        /*if (onTientAnimal == true && Input.GetKeyDown("e") && infoCollision.gameObject.name == "CAGE2")
        {
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[1])
            {
                print("etape1");
                switch (animalPris)
                {
                    case "vache":
                        photonView.RPC("AjoutScoreJoueur2", RpcTarget.All, 3);
                        break;
                    case "mouton":
                        photonView.RPC("AjoutScoreJoueur2", RpcTarget.All, 5);
                        break;
                    case "chien":
                        photonView.RPC("AjoutScoreJoueur2", RpcTarget.All, 10);
                        print("etape2");
                        break;
                    case "cheval":
                        photonView.RPC("AjoutScoreJoueur2", RpcTarget.All, 15);
                        break;
                    case "lama":
                        photonView.RPC("AjoutScoreJoueur2", RpcTarget.All, 20);
                        break;
                    default:
                        break;
                }

            }
            if (photonView.IsMine)
            {
                GetComponent<Animator>().SetBool("animaux", false);
            }
            onTientAnimal = false;
        }*/
    }
    
    [PunRPC]
    public void AjoutScoreJoueur1(int score)
    {
        print("ajout score joueur 1");
        pointage1 += score;
        score1.text = PhotonNetwork.PlayerList[0].NickName + " " + pointage1.ToString();
    }

    [PunRPC]
    public void AjoutScoreJoueur2(int score)
    {
        print("ajout score joueur 2");
        pointage2 += score;
        score2.text = PhotonNetwork.PlayerList[1].NickName + " " + pointage2.ToString();
    }
}
