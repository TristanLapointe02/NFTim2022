using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GestionConnexion : MonoBehaviourPunCallbacks
{
    public GameObject ConnexionServeur; //Interface de connexion (premier �cran)
    public GameObject InterfaceLobby; //Interface du lobby avec cr�er et joindre
    public GameObject SalleAttente; //Interface de la salle d'attente o� on peut choisir son personnage
    public TextMeshProUGUI ChampNomJoueurTexte; //Champ de texte pour le nom du joueur
    public TextMeshProUGUI InformationDebug; //Texte situ� en bas � gauche de l'�cran permettant de savoir la situation de debugage
    public TextMeshProUGUI ChampCreerPartie; //Champ de texte du nom de la partie lors de la cr�ation
    public TextMeshProUGUI ChampJoindrePartie; //Champ de texte du nom de la partie lorsqu'on veut rejoindre une salle
    public TextMeshProUGUI NombreJoueurs; //Texte visuel permettant d'afficher le nombre de joueurs dans la salle d'attente
    public TextMeshProUGUI TexteAttente; //Texte montr� au joueurs dans la salle d'attente
    public TextMeshProUGUI NomSalle; //Texte visuel permettant d'afficher le nom de la salle dans la salle d'attente
    public GameObject BoutonCommencerPartie; //Bouton qui permet au MasterClient de commencer la partie
    public static string PersonnageChoisi = "Edgar"; //String d�terminant le personnage s�lectionn�. Par d�faut AKKKKKSHAN
    public static string NomJoueurStatic; //Nom du joueur en static, pour que le champ de texte s'en rappele
    RoomOptions roomOptions = new RoomOptions(); //Options de la salle
    public GameObject modeleJoseph; //Mod�le de Joseph dans la sc�ne d'accueil
    public GameObject modeleEdgar; //Mod�le de Edgar dans la sc�ne d'accueil

    // Start is called before the first frame update
    void Start()
    {
        InformationDebug.text = "Vous n'�tes pas connect�";
    }

    //Rafra�chir le nombre de joueur � l'�cran (affichage dynamique)
    void Update()
    {
        //Rafra�chir continuellement le nombre de joueurs
        if (PhotonNetwork.InRoom == true)
        {
            NombreJoueurs.text = ("Nombre de joueurs: " + PhotonNetwork.PlayerList.Length + " / 2");
        }

        //V�rifier si le nombre de joueurs d�sir� est atteint.
        if (PhotonNetwork.PlayerList.Length == 2 && PhotonNetwork.LocalPlayer.IsMasterClient == true)
        {
            //Si je suis le master client, j'ai le bouton de d�marrage
            BoutonCommencerPartie.SetActive(true);
        }
        else if (PhotonNetwork.LocalPlayer.IsMasterClient == false)
        {
            //Sinon, j'ai juste un texte d'attente
            TexteAttente.text = "La partie va commencer bient�t :o";
        }
    }

    public void ConnexionLobby()
    {
        //Seulement si le champ n'est pas vide
        if (ChampNomJoueurTexte.text != "")
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        else
        {
            //Sinon, message d'erreur
            InformationDebug.text = "Vous devez entrer votre nom pour se connecter";
        }
    }

    //Quand il est connect� au serveur...
    public override void OnConnectedToMaster()
    {
        //Enregistrer le nom du joueur provenant du champ de texte
        NomJoueurStatic = ChampNomJoueurTexte.text;
        PhotonNetwork.LocalPlayer.NickName = NomJoueurStatic;

        //Joindre le Lobby
        PhotonNetwork.JoinLobby();

        //Activer l'interface du Lobby
        InterfaceLobby.SetActive(true);

        //D�sactiver l'interface de connexion
        ConnexionServeur.SetActive(false);

        //Indiquer au joueur qu'il est connect�
        InformationDebug.text = "Vous �tes connect�";
    }

    //Indiquer au joueur qu'il est dans le lobby
    public override void OnJoinedLobby()
    {
        //Dans le texte de debugage
        InformationDebug.text = "Vous �tes dans un lobby";
    }

    //Cr�er une partie avec un bouton
    public void CreerPartie()
    {
        //D�terminer les options de la salle (RoomOptions)
        roomOptions.MaxPlayers = 2;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;

        //Cr�er la salle avec les bonnes propri�t�s
        PhotonNetwork.CreateRoom(ChampCreerPartie.text, roomOptions, null);
    }

    //Indiquer au joueur si le nom de la salle existe d�j�
    public override void OnCreateRoomFailed(short codeErreur, string messageErreur)
    {
        //Storer cette information dans le texte de debugage
        InformationDebug.text = ("�chec de cr�ation de la salle: " + messageErreur);
    }

    //Joindre une partie avec un bouton
    public void JoindrePartie()
    {
        //Si le champ de texte est vide...
        if (ChampJoindrePartie.text == "")
        {
            //Texte d'erreur
            InformationDebug.text = ("Vous devez entrer un nom de salle");
        }

        //Rejoindre la salle avec le nom �crit dans le champ de texte
        PhotonNetwork.JoinRoom(ChampJoindrePartie.text);
    }

    //Quand on rejoint une salle...
    public override void OnJoinedRoom()
    {
        //Storer le texte d'attente dans la variable afin de l'afficher
        TexteAttente.text = "Vos amis vont se connecter d'ici peu...";

        //D�sactiver l'interface du Lobby
        InterfaceLobby.SetActive(false);

        //Activer la salle d'attente
        SalleAttente.SetActive(true);

        //Nom de la salle
        NomSalle.text = ("Nom de la salle : " + PhotonNetwork.CurrentRoom.Name);

        //Indiquer au joueur qu'il est dans le lobby
        InformationDebug.text = "Vous �tes dans une salle";

    }

    //Indiquer au joueur si la salle existe ou non
    public override void OnJoinRoomFailed(short codeErreur, string messageErreur)
    {
        //Storer cette information dans le texte de debug
        InformationDebug.text = ("Impossible de rejoindre la salle: " + messageErreur);
    }

    //Fonction appel�e lorsque le joueur appuie sur le bouton pour commencer la partie
    public void CommencerLaPartie()
    {
        //Modifier les propri�t�s de la partie pour pas que d'autres personnes se connectent
        PhotonNetwork.CurrentRoom.IsOpen = false; //!!Le garder � true si on veut qu'un joueur quitte et revient
        PhotonNetwork.CurrentRoom.IsVisible = false;

        //Load la sc�ne/Niveau
        PhotonNetwork.LoadLevel("InGame");
    }

    //S�lection du personnage 1
    public void SelectionPersonnage1()
    {
        PersonnageChoisi = "Edgar";
        modeleJoseph.SetActive(false);
        modeleEdgar.SetActive(true);
    }

    //S�lection du personnage 2
    public void SelectionPersonnage2()
    {
        PersonnageChoisi = "Joseph";
        modeleJoseph.SetActive(true);
        modeleEdgar.SetActive(false);
    }

    //D�tecter si le joueur s'est d�connect� du serveur
    public override void OnDisconnected(DisconnectCause cause)
    {
        //Le sortir de la salle
        PhotonNetwork.LeaveRoom();
    }
}
