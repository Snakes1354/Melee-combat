using UnityEngine.AI;
using UnityEngine;

public class EnemyAiPatrol : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;

    public GameObject projectile;
    [SerializeField] private Transform projectileLaunch;

    [SerializeField] LayerMask groundLayer, playerLayer;

    //patrol
    Vector3 destPoint;
    bool walkpointSet;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    [SerializeField] float range;

    //state change
    [SerializeField] float sightRange, attackRange;
    bool playerInsight, playerInAttackRange;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        //not using defensive programming :()
    }
    

    // Update is called once per frame
    void Update()
    {
        playerInsight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if(!playerInsight && !playerInAttackRange)Patrol();
        if(playerInsight && !playerInAttackRange)Chase();
        if(playerInsight && playerInAttackRange)Attack();
    }
    
    void Chase()
    {
        agent.SetDestination(player.transform.position);
    }

    void Attack()
    {
        agent.SetDestination(player.transform.position);

        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        if(!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);



            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
    void Patrol()
    {
        if (!walkpointSet) SearchForDest();
        if (walkpointSet) agent.SetDestination(destPoint);
        if(Vector3.Distance(transform.position, destPoint) < 10) walkpointSet = false;
    }
    
    
    void SearchForDest()
    {
        float z = Random. Range(-range, range);
        float x = Random.Range(-range, range);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destPoint, Vector3.down, 5f, groundLayer))
        {
            walkpointSet = true;
        }
    }

    
    

}


