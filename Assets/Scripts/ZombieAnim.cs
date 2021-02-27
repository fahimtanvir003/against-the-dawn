//using Boo.Lang.Environments;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ZombieAnim : MonoBehaviour
{
    public AudioManager audioManager;
    public ParticleSystem firstbloodParticles;
    public ParticleSystem secondbloodParticles;
    public ParticleSystem thirdbloodParticles;
    public bool isPlayed;
    //public Vector3 defaultPosition;
    //public float walkingDistance;
    //public float maxWalkingDistance;
    //public Transform destinations;
    public float zDistance;
    public float attackDistance;
    public float howMuchClose;
    //public GameObject healthBar;
    private Rigidbody[] rb;
    private Collider[] colls;
    //public CapsuleCollider capCollider;
    public BoxCollider boxCollider;
    public SphereCollider sphereCollider;
    public Rigidbody pubRig;
    public float currentHealth;
    public float maxHealth;
    public Slider healthSlider;
    //Time after the zombie will get destroyed
    public float zDestroyTime;
    //timer
    public float zombieSpeed;
    public float zombieWalkingSpeed;
    public Animator anim;
    public bool inRange, dead;
    public Transform player;
    public ILastEntered ilas;
    Collider myLastHit, myLastExit;
    bool movement;
    //public GameObject zBlood;
    //public GameObject zBloodStain;
    //public ParticleSystem pp;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponentsInChildren<Rigidbody>();
        colls = GetComponentsInChildren<Collider>();
        currentHealth = maxHealth;
        // maxWalkingDistance = Vector3.Distance(defaultPosition, destinations.position);
        audioManager = FindObjectOfType<AudioManager>();
    }
    void Start()
    {
        
        // healthBar.SetActive(true);
        pubRig.isKinematic = false;
        EnableRagdoll(false);
        isPlayed = false;
    }
    void EnableRagdoll(bool state)
    {
        //healthBar.SetActive(false);
        anim.enabled = !state;
        foreach(Rigidbody rigidbodies in rb)
        {
            rigidbodies.isKinematic = !state;
           //rigidbodies.detectCollisions = !state;
        }
        foreach(Collider coll in colls)
        {
            coll.enabled = state; 
        }
        if (!isPlayed)
        {
            firstbloodParticles.Play();
            secondbloodParticles.Play();
            thirdbloodParticles.Play();
            isPlayed = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //walkingDistance = Vector3.Distance(destinations.position, transform.position);
        // calculating attackDistance
        attackDistance = Vector3.Distance(player.position, transform.position);
        

        /*if(attackDistance <= howMuchClose && !dead)
        {
            transform.LookAt(player);
            anim.SetBool("Attack", true);
            
        }
        if(attackDistance >= howMuchClose)
        {
            anim.SetBool("Attack", false);
          
        }*/
        //Attck

        //health info
        pubRig.isKinematic = false;
        //capCollider.enabled = true;
        boxCollider.enabled = true;
        sphereCollider.enabled = true;
        if (currentHealth <= 0)
        {
            pubRig.isKinematic = false;
            dead = true;
            EnableRagdoll(true);
            ZombieDead();
            
            /*capCollider.enabled = false;
            sphereCollider.enabled = false;*/
        }
        // health info
        if (inRange && !dead)
        {

            transform.LookAt(player);

              if (attackDistance <= zDistance)
              {
                anim.SetBool("InRange", false);
                anim.SetBool("Attack", true);

              }
            //transform.Translate(Vector3.forward * zombieSpeed * Time.deltaTime);
            //pubRig.AddTorque(transform.forward * zombieSpeed);
            if (attackDistance > zDistance)
            {
                anim.SetBool("Attack", false);
                anim.SetBool("InRange", true);
                anim.SetBool("walk", false);
                movement = true;

                Run();
            }
           // anim.SetBool("InRange", false);

        }
        if (!inRange || dead)
        {
            inRange = false;
            movement = false;
        }

        anim.SetFloat("Time", Time.time);

        if (Time.time >= 5f && !inRange && !dead)
        {
            anim.SetBool("walk", true);
            //transform.Translate(Vector3.forward * zombieWalkingSpeed * Time.deltaTime);
            //pubRig.AddTorque(transform.forward * zombieWalkingSpeed);
            Walk();
        }
    }
    private void Run()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, player.position, zombieSpeed * Time.deltaTime);
        pubRig.MovePosition(pos);
    }
    private void Walk()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, player.position, zombieWalkingSpeed * Time.deltaTime);
        pubRig.MovePosition(pos);
        transform.LookAt(player);
        //pubRig.AddForce(pos * zombieWalkingSpeed);

    }
   
    void OnTriggerEnter(Collider other)
    {

        myLastHit = ilas.iLastEntered;

        if (myLastHit is SphereCollider && other.gameObject.CompareTag("Player"))
        {
            audioManager.Play("Zombiebgm");
            transform.LookAt(player);
            inRange = true;
           // anim.SetBool("InRange", true);
            anim.SetBool("walk", false);
            
            if (!dead)
            {
                anim.enabled = true;
                //Debug.Log("Coroutine");
            }
        }
        if (myLastHit is BoxCollider && other.gameObject.CompareTag("Player"))
        {
            
            //EnableRagdoll(true);
            //pp.Emit(1);
            transform.LookAt(player);
            anim.enabled = false;
            //Instantiate(bloodParticles, transform.position, Quaternion.identity);
            audioManager.Play("ZombieCrash");
            audioManager.Play("ZombieCrash1");
            //Instantiate(zBloodStain, transform.position, transform.rotation);
            TakeDamage(2);
        }
        
    }
   
    private void ZombieDead()
    {
        dead = true;
        //anim.enabled = false;
        movement = false;
        //Instantiate(zBlood, transform.position, Quaternion.identity);
        Destroy(gameObject, zDestroyTime);
       
    }
    
    void OnTriggerExit(Collider other)
    {

        myLastExit = ilas.iLastExited;

        if (myLastExit is SphereCollider && other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            anim.SetBool("InRange", false);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        SetHealth(currentHealth);
    }
    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }

}
