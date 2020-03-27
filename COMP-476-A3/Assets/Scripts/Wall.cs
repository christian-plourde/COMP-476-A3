using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Wall : MonoBehaviour
{
    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = this.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Remove()
    {
        PhotonNetwork.Destroy(this.gameObject);
    }
}
