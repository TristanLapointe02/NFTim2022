using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPromener : MonoBehaviour
{
    public GameObject[] MesDestinations; // sera établit plus tard
    public UnityEngine.AI.NavMeshAgent navAgent; // réfère au navAgent
    public Animator animator; // animator de la cow
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        chercherProchaineCible();
    }

    // Update is called once per frame
    void Update()
    {
        //  navAgent.SetDestination(MesDestination.transform.position); //la destination doit �tre un Vector3
    }
    public void chercherProchaineCible(){
        int nombreAlea = Random.Range(0, MesDestinations.Length);
        navAgent.SetDestination(MesDestinations[nombreAlea].transform.position);

    }
    private void OnTriggerEnter(Collider infoCol) {
        if(infoCol.gameObject.tag == "destination"){
            chercherProchaineCible();
        }
    }
}
