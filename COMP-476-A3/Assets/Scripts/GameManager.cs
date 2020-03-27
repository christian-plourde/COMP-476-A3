using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Tooltip("The prefab that the player's will be controlling.")]
    [SerializeField]
    private GameObject tankPrefab;

    [Tooltip("The prefab that represents the level. For destruction it must be instantiated by Photon.")]
    [SerializeField]
    private GameObject levelPrefab; 

    [Tooltip("The UI text component that says which room you are in.")]
    [SerializeField]
    private Text roomText;

    private void Start()
    {
        if(TankMovement.LocalPlayerInstance == null)
        {
            if (tankPrefab == null)
                Debug.Log("Tank Prefab reference is missing.");
            else
            {
                PhotonNetwork.Instantiate(this.tankPrefab.name, new Vector3(-5, 0.1f, 1), Quaternion.identity);
            }
        }

        if (levelPrefab == null)
            Debug.Log("Level Prefab is missing.");
        else
            PhotonNetwork.Instantiate(this.levelPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
    }

    private void Update()
    {
        roomText.text = "Connected to room: " + PhotonNetwork.CurrentRoom.Name + "\nPlayer Count: " + PhotonNetwork.CurrentRoom.PlayerCount;
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0); //load the launcher scene when we leave the room
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
            Debug.LogError("Photon Network: Trying to load a level but we are not the master client.");
        Debug.Log("Loading level...");
        PhotonNetwork.LoadLevel("TankBattle");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " entered the room.");

        if(PhotonNetwork.IsMasterClient)
        {
            Debug.Log(newPlayer.NickName + " is master client.");
            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + " left the room.");

        if(PhotonNetwork.IsMasterClient)
        {
            Debug.Log(otherPlayer.NickName + " is master client.");
            LoadArena();
        }
    }
}
