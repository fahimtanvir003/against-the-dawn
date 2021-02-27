using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BloodLauncher : MonoBehaviour
{
    public ParticleSystem particleLauncher;
    public ParticleSystem splatter;
    List<ParticleCollisionEvent> collisionEvents;
    public ParticleDecalPool splatDecalPool;
    public Gradient colourGradient;
    private void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            splatDecalPool.ParticleHit(collisionEvents[i], colourGradient);
            EmitAtLocation(collisionEvents[i]);
        }
    }
    void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        splatter.transform.position = particleCollisionEvent.intersection;
        splatter.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        splatter.Emit(1);
    }
    private void FixedUpdate()
    {
        /*if (Input.GetButton("Fire1"))
        {

        }*/
        particleLauncher.Emit(1);

    }
}
