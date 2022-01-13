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
    public int pointage1;
    public Text score2;
    public int pointage2;
    public bool onTientAnimal;
    public GameObject rondVert;
    public GameObject rondMauve;

    void Start()
    {
        rondVert = GameObject.Find("RondVert");
        rondMauve = GameObject.Find("RondMauve");
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
            score1.text = PhotonNetwork.PlayerList[0].NickName + " " + pointage1;
            score2.text = PhotonNetwork.PlayerList[1].NickName + " " + pointage2;
            print("test");
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[0] && photonView.IsMine)
            {
                rondVert.gameObject.SetActive(true);
                rondMauve.gameObject.SetActive(false);
            }
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[1] && photonView.IsMine)
            {
                rondVert.gameObject.SetActive(false);
                rondMauve.gameObject.SetActive(true);
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

    void OnTriggerStay(Collider infoCollision)
    {
        if ((infoCollision.gameObject.tag == "lama" || infoCollision.gameObject.tag == "cheval" ||
        infoCollision.gameObject.tag == "chien" || infoCollision.gameObject.tag == "mouton" ||
        infoCollision.gameObject.tag == "vache" || infoCollision.gameObject.tag == "zebre" ||
        infoCollision.gameObject.tag == "pug" || infoCollision.gameObject.tag == "cochon") && Input.GetKey("e") && onTientAnimal == false && photonView.IsMine)
        {
            GetComponent<Animator>().SetBool("animaux", true);
            infoCollision.transform.parent = main.transform;
            //infoCollision.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            onTientAnimal = true;
        }
        if (photonView.IsMine && onTientAnimal == true && Input.GetKey("e") && infoCollision.gameObject.name == "CAGE1")
        {
            if(PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[0])
            {
                print("etape1");
                switch (testPointsAndy.animalPris)
                {
                    case "vache":
                        pointage1 += 3;
                        break;
                    case "mouton":
                        pointage1 += 5;
                        break;
                    case "chien":
                        pointage1 += 10;
                        print("etape2");
                        break;
                    case "cheval":
                        pointage1 += 15;
                        break;
                    case "lama":
                        pointage1 += 20;
                        break;
                    default:
                        break;
                }
               
            }
            onTientAnimal = false;
        }
        if (photonView.IsMine && onTientAnimal == true && Input.GetKey("e") && infoCollision.gameObject.name == "CAGE2")
        {
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[1])
            {
                switch (testPointsAndy.animalPris)
                {
                    case "vache":
                        pointage2 += 3;
                        break;
                    case "mouton":
                        pointage2 += 5;
                        break;
                    case "chien":
                        pointage2 += 10;
                        print("etape2");
                        break;
                    case "cheval":
                        pointage2 += 15;
                        break;
                    case "lama":
                        pointage2 += 20;
                        break;
                    default:
                        break;
                }

            }
            onTientAnimal = false;
        }
    }
}
