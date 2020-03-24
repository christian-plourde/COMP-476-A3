using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
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
