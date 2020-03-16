using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour
{

    const string playerNamePrefKey = "PlayerName";

    // Start is called before the first frame update
    void Start()
    {
        //setting name of player from player prefs if possible
        string defaultName = string.Empty;
        InputField inputField = this.GetComponent<InputField>();
        if(inputField != null)
        {
            if(PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }

    /// <summary>
    /// Method to set the players name. Called from input text on changed method.
    /// </summary>
    /// <param name="value">The name of the player.</param>
    public void SetPlayerName(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player name is invalid.");
            return;
        }

        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNamePrefKey, value);
    }

}
