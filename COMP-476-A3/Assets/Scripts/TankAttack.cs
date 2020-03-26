using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    #region Properties

    public float Health
    {
        get { return health; }
        set { health = value; }
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

    private void FireRound()
    {
        GameObject bullet = Instantiate(projectile, this.transform.position + firingPointOffset, this.transform.rotation);
        bullet.transform.Rotate(Vector3.up, 90.0f);
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
            HandleInput();
    }
}
