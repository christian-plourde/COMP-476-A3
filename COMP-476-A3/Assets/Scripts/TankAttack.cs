using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class TankAttack : MonoBehaviour
{
    [Tooltip("The maximum health of the tank.")]
    [SerializeField]
    private float health;

    [Tooltip("The projectile spawned when firing.")]
    [SerializeField]
    private GameObject projectile;

    [Tooltip("This will determine the projectile's firing position.")]
    [SerializeField]
    private Vector3 firingPointOffset;

    private PhotonView photonView;

    private Text healthText;

    private Text deathMessage;

    #region Properties

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public PhotonView PhotonView
    {
        get { return photonView; }
    }

    #endregion

    #region InputHandling

    private void Awake()
    {
        photonView = this.gameObject.GetComponent<PhotonView>();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            FireRound();
    }

    public void FireRound()
    {
        GameObject bullet = PhotonNetwork.Instantiate(this.projectile.name, this.transform.position + firingPointOffset, this.transform.rotation);
        bullet.transform.Rotate(Vector3.up, 90.0f);
        bullet.GetComponent<Projectile>().Tank = this;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        healthText = GameObject.FindGameObjectWithTag("HealthIndicator").GetComponent<Text>();
        deathMessage = GameObject.FindGameObjectWithTag("DeathMessage").GetComponent<Text>();
        deathMessage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine && this.tag == "Tank")
        {
            healthText.text = health.ToString();
            HandleInput();
        }
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        if(photonView.IsMine)
        {
            Debug.Log(photonView.Owner.NickName + " takes " + damage + " damage.");
            health -= damage;

            //check if player has lost and destroy his tank. tanks are for winners only
            if (health <= 0)
            {
                deathMessage.enabled = true;
                PhotonNetwork.Destroy(this.gameObject);
            }
                
        }
    }
}
