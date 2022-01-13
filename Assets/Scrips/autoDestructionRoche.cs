using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class autoDestructionRoche : MonoBehaviourPunCallbacks
{
    void OnTriggerEnter(Collider infoCollision) {
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
