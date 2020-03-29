using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{

    [Tooltip("The maximum number of players per room. When a room is full it can't be joined by new players.")]
    [SerializeField]
    private byte maxPlayersPerRoom = 2;

    [Tooltip("The UI Panel to let the user enter name, connect and play.")]
    [SerializeField]
    private GameObject controlPanel;

    [Tooltip("The UI Label to inform the user that the connection is in progress.")]
    [SerializeField]
    private GameObject progressLabel;

    bool isConnecting;

    string gameVersion = "1"; //the game version

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Connect()
    {
        isConnecting = PhotonNetwork.ConnectUsingSettings();
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.JoinRandomRoom(); //if we are connected try to join a random room
        else
        {
            PhotonNetwork.ConnectUsingSettings(); //if not then try to connect and set the game version
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master."); //log that we connected to master room. 
                                           //this will happen here since only two people are playing the game
        if(isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }
        
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        Debug.LogWarningFormat("Disconnected for reason {0}", cause); //log this if we get accidentally disc. from room
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No Random Room Available.");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("This client is now in room " + PhotonNetwork.CurrentRoom.Name);

        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("Loading arena...");

            PhotonNetwork.LoadLevel("TankBattle");
        }
    }
}
