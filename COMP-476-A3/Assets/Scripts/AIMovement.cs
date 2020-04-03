using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [Tooltip("The minimum distance at which a player is considered to be within range of the AI character.")]
    [SerializeField]
    private float attackRange = 2.0f;

    [Tooltip("The attention span of the AI with regards to seeing the player.")]
    [SerializeField]
    private float attentionSpan = 5.0f;

    [Tooltip("How fast the tank can fire.")]
    [SerializeField]
    private float fireRate = 1.0f;

    [Tooltip("The radius of the sphere used when figuring out a random direction to walk in for wander behaviour.")]
    [SerializeField]
    private float wanderRadius = 1.0f;

    NavMeshAgent agent; //the nav mesh agent reference on the ai
    DecisionTree decisionTree; //the decision tree for the ai
    GameObject lastSeenPlayer; //last person the ai saw
    TankAttack tankAttack; //a reference to the tank attack script on this prefab

    private float timeSinceLastPlayerSpotted; //the time since the last player was spotted

    private float timeSinceLastRoundFired; //the time since the last round was fired

    #region Decision Tree

    private bool PlayerVisible()
    {
        //for each of the player, if i see one, return true and assign the last seen player to our reference
        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Tank"))
        {
            RaycastHit hit;
            if(Physics.Raycast(this.transform.position, this.transform.forward, out hit))
            {
                if(hit.collider.tag == "Tank")
                {
                    lastSeenPlayer = hit.collider.gameObject;
                    timeSinceLastPlayerSpotted = 0.0f;
                    Debug.Log(lastSeenPlayer.GetComponent<TankMovement>().PhotonView.Owner);
                    return true;
                }
            }
        }

        return false;
    }

    private bool PlayerWithinRange()
    {
        if (lastSeenPlayer != null && (this.transform.position - lastSeenPlayer.transform.position).magnitude < attackRange)
            return true;

        else
            return false;
    }

    private bool PlayerSeenRecently()
    {
        if (timeSinceLastPlayerSpotted < attentionSpan)
            return true;

        return false;
    }

    private void Attack()
    {
        //Debug.Log("Attacking");
        if(timeSinceLastRoundFired > fireRate)
        {
            tankAttack.FireRound();
            timeSinceLastRoundFired = 0.0f;
        }
        
    }

    private void MoveToPlayer()
    {
        if (lastSeenPlayer != null)
            agent.SetDestination(lastSeenPlayer.transform.position);
    }

    private void Wander()
    {
        //we need to check to make sure there is a path to the location by chekcing if the distance is not infinity
        float dist = agent.remainingDistance;
        //if the path is complete and distance remaining is less than our bias we reset the destination of agent
        if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance <= 1.0f)
        {
            //get the triangulation of the navmesh and pick a random triangle
            NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
            int t = Random.Range(0, navMeshData.indices.Length - 3);

            //Now pick a random point within that triangle
            Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);

            agent.SetDestination(point);
        }
    }

    private void SetDecisionTree()
    {
        CheckNode playerVisible = new CheckNode(PlayerVisible);
        CheckNode playerWithinRange = new CheckNode(PlayerWithinRange);
        CheckNode playerSeenRecently = new CheckNode(PlayerSeenRecently);

        ActionNode attack = new ActionNode(Attack);
        ActionNode moveToPlayer = new ActionNode(MoveToPlayer);
        ActionNode wander = new ActionNode(Wander);

        playerVisible.True = playerWithinRange;
        playerVisible.False = playerSeenRecently;

        playerWithinRange.True = attack;
        playerWithinRange.False = moveToPlayer;

        playerSeenRecently.True = moveToPlayer;
        playerSeenRecently.False = wander;

        decisionTree = new DecisionTree(playerVisible);
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        this.agent = this.GetComponent<NavMeshAgent>();
        this.tankAttack = this.GetComponent<TankAttack>();
        SetDecisionTree();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastPlayerSpotted += Time.deltaTime; //add time since the last player was spotted. If the player is visible again
                                                      //this will be reset

        timeSinceLastRoundFired += Time.deltaTime; //add time since last round was fired. If a round is fired, this will be reset

        decisionTree.Evaluate();

        //agent.SetDestination(new Vector3(0, 0, 0));
    }
}
