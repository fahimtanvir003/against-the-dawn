using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieChase : MonoBehaviour
{
    private NavMeshAgent Zombie;
    public GameObject Player;
    public float MobDistanceRun = 4.0f;
    
    void Start()
    {
        Zombie = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if(distance< MobDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 newPos = transform.position - dirToPlayer;
            Zombie.SetDestination(newPos);
        }
    }
}
