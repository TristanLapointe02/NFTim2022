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
    public GameObject ConnexionServeur; //Interface de connexion (premier écran)
    public GameObject InterfaceLobby; //Interface du lobby avec créer et joindre
    public GameObject SalleAttente; //Interface de la salle d'attente où on peut choisir son personnage
    public TextMeshProUGUI ChampNomJoueurTexte; //Champ de texte pour le nom du joueur
    public TextMeshProUGUI InformationDebug; //Texte situé en bas à gauche de l'écran permettant de savoir la situation de debugage
    public TextMeshProUGUI ChampCreerPartie; //Champ de texte du nom de la partie lors de la création
    public TextMeshProUGUI ChampJoindrePartie; //Champ de texte du nom de la partie lorsqu'on veut rejoindre une salle
    public TextMeshProUGUI NombreJoueurs; //Texte visuel permettant d'afficher le nombre de joueurs dans la salle d'attente
    public TextMeshProUGUI TexteAttente; //Texte montré au joueurs dans la salle d'attente
    public TextMeshProUGUI NomSalle; //Texte visuel permettant d'afficher le nom de la salle dans la salle d'attente
    public GameObject BoutonCommencerPartie; //Bouton qui permet au MasterClient de commencer la partie
    public static string PersonnageChoisi = "Edgar"; //String déterminant le personnage sélectionné. Par défaut AKKKKKSHAN
    public static string NomJoueurStatic; //Nom du joueur en static, pour que le champ de texte s'en rappele
    RoomOptions roomOptions = new RoomOptions(); //Options de la salle
    public GameObject modeleJoseph; //Modèle de Joseph dans la scène d'accueil
    public GameObject modeleEdgar; //Modèle de Edgar dans la scène d'accueil

    // Start is called before the first frame update
    void Start()
    {
        InformationDebug.text = "Vous n'êtes pas connecté";
    }

    //Rafraîchir le nombre de joueur à l'écran (affichage dynamique)
    void Update()
    {
        //Rafraîchir continuellement le nombre de joueurs
        if (PhotonNetwork.InRoom == true)
        {
            NombreJoueurs.text = ("Nombre de joueurs: " + PhotonNetwork.PlayerList.Length + " / 2");
        }

        //Vérifier si le nombre de joueurs désiré est atteint.
        if (PhotonNetwork.PlayerList.Length == 2 && PhotonNetwork.LocalPlayer.IsMasterClient == true)
        {
            //Si je suis le master client, j'ai le bouton de démarrage
            BoutonCommencerPartie.SetActive(true);
        }
        else if (PhotonNetwork.LocalPlayer.IsMasterClient == false)
        {
            //Sinon, j'ai juste un texte d'attente
            TexteAttente.text = "La partie va commencer bientôt :o";
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

    //Quand il est connecté au serveur...
    public override void OnConnectedToMaster()
    {
        //Enregistrer le nom du joueur provenant du champ de texte
        NomJoueurStatic = ChampNomJoueurTexte.text;
        PhotonNetwork.LocalPlayer.NickName = NomJoueurStatic;

        //Joindre le Lobby
        PhotonNetwork.JoinLobby();

        //Activer l'interface du Lobby
        InterfaceLobby.SetActive(true);

        //Désactiver l'interface de connexion
        ConnexionServeur.SetActive(false);

        //Indiquer au joueur qu'il est connecté
        InformationDebug.text = "Vous êtes connecté";
    }

    //Indiquer au joueur qu'il est dans le lobby
    public override void OnJoinedLobby()
    {
        //Dans le texte de debugage
        InformationDebug.text = "Vous êtes dans un lobby";
    }

    //Créer une partie avec un bouton
    public void CreerPartie()
    {
        //Déterminer les options de la salle (RoomOptions)
        roomOptions.MaxPlayers = 2;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;

        //Créer la salle avec les bonnes propriétés
        PhotonNetwork.CreateRoom(ChampCreerPartie.text, roomOptions, null);
    }

    //Indiquer au joueur si le nom de la salle existe déjà
    public override void OnCreateRoomFailed(short codeErreur, string messageErreur)
    {
        //Storer cette information dans le texte de debugage
        InformationDebug.text = ("Échec de création de la salle: " + messageErreur);
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

        //Rejoindre la salle avec le nom écrit dans le champ de texte
        PhotonNetwork.JoinRoom(ChampJoindrePartie.text);
    }

    //Quand on rejoint une salle...
    public override void OnJoinedRoom()
    {
        //Storer le texte d'attente dans la variable afin de l'afficher
        TexteAttente.text = "Vos amis vont se connecter d'ici peu...";

        //Désactiver l'interface du Lobby
        InterfaceLobby.SetActive(false);

        //Activer la salle d'attente
        SalleAttente.SetActive(true);

        //Nom de la salle
        NomSalle.text = ("Nom de la salle : " + PhotonNetwork.CurrentRoom.Name);

        //Indiquer au joueur qu'il est dans le lobby
        InformationDebug.text = "Vous êtes dans une salle";

    }

    //Indiquer au joueur si la salle existe ou non
    public override void OnJoinRoomFailed(short codeErreur, string messageErreur)
    {
        //Storer cette information dans le texte de debug
        InformationDebug.text = ("Impossible de rejoindre la salle: " + messageErreur);
    }

    //Fonction appelée lorsque le joueur appuie sur le bouton pour commencer la partie
    public void CommencerLaPartie()
    {
        //Modifier les propriétés de la partie pour pas que d'autres personnes se connectent
        PhotonNetwork.CurrentRoom.IsOpen = false; //!!Le garder à true si on veut qu'un joueur quitte et revient
        PhotonNetwork.CurrentRoom.IsVisible = false;

        //Load la scène/Niveau
        PhotonNetwork.LoadLevel("InGame");
    }

    //Sélection du personnage 1
    public void SelectionPersonnage1()
    {
        PersonnageChoisi = "Edgar";
        modeleJoseph.SetActive(false);
        modeleEdgar.SetActive(true);
    }

    //Sélection du personnage 2
    public void SelectionPersonnage2()
    {
        PersonnageChoisi = "Joseph";
        modeleJoseph.SetActive(true);
        modeleEdgar.SetActive(false);
    }

    //Détecter si le joueur s'est déconnecté du serveur
    public override void OnDisconnected(DisconnectCause cause)
    {
        //Le sortir de la salle
        PhotonNetwork.LeaveRoom();
    }
}
