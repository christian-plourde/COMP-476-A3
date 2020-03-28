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

    [Tooltip("The prefab that represents a wall. For destruction it must be instantiated by Photon.")]
    [SerializeField]
    private GameObject wallPrefab;

    [Tooltip("The UI text component that says which room you are in.")]
    [SerializeField]
    private Text roomText;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

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

        if (wallPrefab == null)
            Debug.Log("Level Prefab is missing.");

        else if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(9, 0.5f, -3), Quaternion.identity); //Cube(4)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(8, 0.5f, -3), Quaternion.identity); //Cube(5)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(7, 0.5f, -3), Quaternion.identity); //Cube(6)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(6, 0.5f, -3), Quaternion.identity); //Cube(7)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(5, 0.5f, -3), Quaternion.identity); //Cube(8)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(4, 0.5f, -3), Quaternion.identity); //Cube(9)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(3, 0.5f, -3), Quaternion.identity); //Cube(10)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(0, 0.5f, -3), Quaternion.identity); //Cube(11)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-1, 0.5f, -3), Quaternion.identity); //Cube(12)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-2, 0.5f, -3), Quaternion.identity); //Cube(13)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-3, 0.5f, -3), Quaternion.identity); //Cube(14)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-7, 0.5f, -3), Quaternion.identity); //Cube(15)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-8, 0.5f, -3), Quaternion.identity); //Cube(16)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-9, 0.5f, -3), Quaternion.identity); //Cube(17)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(6, 0.5f, 0), Quaternion.identity); //Cube(18)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(5, 0.5f, 0), Quaternion.identity); //Cube(19)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(4, 0.5f, 0), Quaternion.identity); //Cube(20)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(3, 0.5f, 0), Quaternion.identity); //Cube(21)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(2, 0.5f, 0), Quaternion.identity); //Cube(22)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(1, 0.5f, 0), Quaternion.identity); //Cube(23)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(0, 0.5f, 0), Quaternion.identity); //Cube(24)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(6, 0.5f, 3), Quaternion.identity); //Cube(25)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(5, 0.5f, 3), Quaternion.identity); //Cube(26)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(4, 0.5f, 3), Quaternion.identity); //Cube(27)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(3, 0.5f, 3), Quaternion.identity); //Cube(28)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(2, 0.5f, 3), Quaternion.identity); //Cube(29)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(1, 0.5f, 3), Quaternion.identity); //Cube(30)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(0, 0.5f, 3), Quaternion.identity); //Cube(31)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(9, 0.5f, 6), Quaternion.identity); //Cube(32)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(8, 0.5f, 6), Quaternion.identity); //Cube(33)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(7, 0.5f, 6), Quaternion.identity); //Cube(34)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(6, 0.5f, 6), Quaternion.identity); //Cube(35)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(3, 0.5f, 6), Quaternion.identity); //Cube(36)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(2, 0.5f, 6), Quaternion.identity); //Cube(37)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(1, 0.5f, 6), Quaternion.identity); //Cube(38)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(0, 0.5f, 6), Quaternion.identity); //Cube(39)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(3, 0.5f, -6), Quaternion.identity); //Cube(40)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(2, 0.5f, -6), Quaternion.identity); //Cube(41)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(1, 0.5f, -6), Quaternion.identity); //Cube(42)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(0, 0.5f, -6), Quaternion.identity); //Cube(43)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-4, 0.5f, 0), Quaternion.identity); //Cube(44)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-5, 0.5f, 0), Quaternion.identity); //Cube(45)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-6, 0.5f, 0), Quaternion.identity); //Cube(46)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-7, 0.5f, 0), Quaternion.identity); //Cube(47)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-6, 0.5f, 7), Quaternion.identity); //Cube(48)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-7, 0.5f, 7), Quaternion.identity); //Cube(49)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-0.5f, 0.5f, 6.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(50)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-0.5f, 0.5f, 7.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(51)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-0.5f, 0.5f, -1.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(52)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-0.5f, 0.5f, -0.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(53)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-0.5f, 0.5f, -2.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(54)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-3.25f, 0.5f, -3.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(55)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-3.25f, 0.5f, -4.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(56)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-3.25f, 0.5f, -5.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(57)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-3.25f, 0.5f, -9.0f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(58)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-6.25f, 0.5f, -3.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(59)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-6.25f, 0.5f, -4.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(60)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-6.25f, 0.5f, -5.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(61)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-6.25f, 0.5f, -6.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(62)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-3.5f, 0.5f, 0.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(63)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-3.5f, 0.5f, 1.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(64)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-3.5f, 0.5f, 2.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(65)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(6.25f, 0.5f, 0.5f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(66)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(6.25f, 0.5f, 1.5f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(67)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(6.25f, 0.5f, 2.5f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(68)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-3.5f, 0.5f, 3.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(69)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-3.5f, 0.5f, 4.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(70)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-3.5f, 0.5f, 9.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(71)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-3.5f, 0.5f, 8.25f), Quaternion.Euler(0.0f, 90.0f, 0.0f)); //Cube(72)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(3.64f, 0.5f, 8.34f), Quaternion.Euler(0.0f, -45.0f, 0.0f)); //Cube(73)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(3.14f, 0.5f, 7.84f), Quaternion.Euler(0.0f, -45.0f, 0.0f)); //Cube(74)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-5.72f, 0.5f, 2.75f), Quaternion.Euler(0.0f, -45.0f, 0.0f)); //Cube(75)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(-6.22f, 0.5f, 2.25f), Quaternion.Euler(0.0f, -45.0f, 0.0f)); //Cube(76)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(7.04f, 0.5f, -5.93f), Quaternion.Euler(0.0f, -45.0f, 0.0f)); //Cube(77)
            PhotonNetwork.Instantiate(this.wallPrefab.name, new Vector3(6.54f, 0.5f, -6.429999f), Quaternion.Euler(0.0f, -45.0f, 0.0f)); //Cube(78)
        }
    }

    private void Update()
    {
        roomText.text = "Connected to room: " + PhotonNetwork.CurrentRoom.Name + "\nPlayer Count: " + PhotonNetwork.CurrentRoom.PlayerCount + "\n" + PhotonNetwork.NickName + (PhotonNetwork.IsMasterClient ? " MASTER CLIENT" : " SLAVE");
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0); //load the launcher scene when we leave the room
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    /*
    public void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
            Debug.LogError("Photon Network: Trying to load a level but we are not the master client.");
        else
        {
            Debug.Log("Loading level...");
            PhotonNetwork.LoadLevel("TankBattle");
        }
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
    */
}
