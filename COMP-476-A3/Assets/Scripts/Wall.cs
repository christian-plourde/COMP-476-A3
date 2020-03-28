using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Wall : MonoBehaviour
{
    PhotonView photonView;

    public PhotonView PhotonView
    {
        get { return photonView; }
    }

    // Start is called before the first frame update
    void Start()
    {
        photonView = this.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void Remove(PhotonMessageInfo info)
    {
        //Debug.Log("Reciever: " + photonView.Owner.NickName);
        PhotonNetwork.Destroy(this.gameObject);
    }
}
