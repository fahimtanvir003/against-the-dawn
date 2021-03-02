using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.AI;

public class AIExample : MonoBehaviour
{

    public enum WanderType { Random, Waypoint };


    //public FirstPersonController fpsc;
    public GameObject player;
    public WanderType wanderType = WanderType.Random;
    public float wanderSpeed = 4f;
    public float chaseSpeed = 7f;
    public float fov = 120f;
    public float viewDistance = 10f;
    public float wanderRadius = 7f;
    public Transform[] waypoints; //Array of waypoints is only used when waypoint wandering is selected

    private bool isAware = false;
    private Vector3 wanderPoint;
    private NavMeshAgent agent;
    private Renderer renderer;
    private int waypointIndex = 0;
    private Animator animator;

    private bool offAnimation = false;

    private Collider[] ragdollColliders;
    private Rigidbody[] ragdollRigidbodies;


    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<Renderer>();
        animator = GetComponentInChildren<Animator>();
        wanderPoint = RandomWanderPoint();
        ragdollColliders = GetComponentsInChildren<Collider>();
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();

       

    }
    public void Update()
    {
        if (isAware)
        {
            agent.SetDestination(player.transform.position);
            animator.SetBool("Aware", true);
            agent.speed = chaseSpeed;
            //renderer.material.color = Color.red;
        }
        else
        {
            SearchForPlayer();
            Wander();
            animator.SetBool("Aware", false);
            agent.speed = wanderSpeed;
            //renderer.material.color = Color.blue;
        }

        if (offAnimation)
        {
            animator.enabled = false;
            agent.speed = 0;
            foreach (Collider col in ragdollColliders)
            {
                col.enabled = false;
            }

            foreach (Rigidbody rb in ragdollRigidbodies)
            {
                rb.isKinematic = false;
            }
        }
        else
        {
            animator.enabled = true;
        }

    }

    public void SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)) < fov / 2f)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < viewDistance)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position, player.transform.position, out hit, -1))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        OnAware();
                    }
                }
            }
        }
    }

    public void OnAware()
    {
        isAware = true;
    }

    public void Wander()
    {
        if (wanderType == WanderType.Random)
        {
            if (Vector3.Distance(transform.position, wanderPoint) < 2f)
            {
                wanderPoint = RandomWanderPoint();
            }
            else
            {
                agent.SetDestination(wanderPoint);
            }
        }
        else
        {
            //Waypoint wandering
            if (waypoints.Length >= 2)
            {
                if (Vector3.Distance(waypoints[waypointIndex].position, transform.position) < 2f)
                {
                    if (waypointIndex == waypoints.Length - 1)
                    {
                        waypointIndex = 0;
                    }
                    else
                    {
                        waypointIndex++;
                    }
                }
                else
                {
                    agent.SetDestination(waypoints[waypointIndex].position);
                }
            }
            else
            {
                Debug.LogWarning("Please assign more than 1 waypoint to the AI: " + gameObject.name);
            }
        }
    }

    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            offAnimation = true;
            
        }

        if (collision.gameObject.tag == "DestroyZombie")
        {
            Destroy(gameObject);

        }
    }
}
