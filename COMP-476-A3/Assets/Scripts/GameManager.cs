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
}
