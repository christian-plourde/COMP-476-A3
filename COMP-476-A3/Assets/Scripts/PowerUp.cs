using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Tank")
        {
            PhotonNetwork.Destroy(this.gameObject);
            try
            {
                other.gameObject.GetComponent<TankMovement>().PhotonView.RPC("ActivatePowerUp", other.gameObject.GetComponent<TankMovement>().PhotonView.Owner);
            }

            catch { }
        }
    }
}
