using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Projectile : MonoBehaviour
{
    [Tooltip("How fast the projectile travels after it has been launched.")]
    [SerializeField]
    private float travelSpeed;

    [Tooltip("How much damage the projectile does on hit.")]
    [SerializeField]
    private float damage;

    [Tooltip("How long the bullet stays in the scene.")]
    [SerializeField]
    private float lifeSpan;

    private float timeAlive = 0.0f;

    private TankAttack tank; //the tank that shot the projectile

    public TankAttack Tank
    {
        set { tank = value; }
    }

    private void CheckLifeTimer()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifeSpan)
            Destroy(this.gameObject);
    }

    private void Move()
    {
        this.transform.position += this.transform.right * -1 * travelSpeed * Time.deltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckLifeTimer();

        //each frame bullet must travel
        Move();
    }

    void OnTriggerEnter(Collider other)
    {
        //if i hit a wall, destroy that wall
        if (other.tag == "Wall")
        {
            other.gameObject.GetComponent<Wall>().PhotonView.RPC("Remove", RpcTarget.MasterClient);
            PhotonNetwork.Destroy(this.gameObject);
        }

        if(other.tag == "Tank")
        {
            if(other.gameObject.GetComponent<TankAttack>() != tank)
            {
                Debug.Log(tank.PhotonView.Owner.NickName + " hit " + other.gameObject.GetComponent<TankAttack>().PhotonView.Owner.NickName);
                other.gameObject.GetComponent<TankAttack>().PhotonView.RPC("TakeDamage", other.gameObject.GetComponent<TankAttack>().PhotonView.Owner, damage);
                PhotonNetwork.Destroy(this.gameObject);
            } 
        }       
    }
}
