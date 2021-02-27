﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ParticleDecalPool : MonoBehaviour
{
    private ParticleSystem decalParticleSystem;
    private int particleDecalDataIndex;
    public int maxDecals = 100;
    private ParticleDecalData[] particleData;
    public float decalSizeMin = .5f;
    public float decalSizeMax = 1.5f;
    private ParticleSystem.Particle[] particles;
    // Start is called before the first frame update
    void Start()
    {
        decalParticleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[maxDecals];
        particleData = new ParticleDecalData[maxDecals];
        for (int i = 0; i < maxDecals; i++)
        {
            particleData[i] = new ParticleDecalData();
        }
    }
    public void ParticleHit(ParticleCollisionEvent particleCollisionEvent, Gradient colour)
    {
        SetParticleData(particleCollisionEvent, colour);
    }

    void SetParticleData(ParticleCollisionEvent particleCollisionEvent, Gradient colour)
    {
        if (particleDecalDataIndex >= maxDecals)
        {
            particleDecalDataIndex = 0;
        }

        particleData[particleDecalDataIndex].position = particleCollisionEvent.intersection;
        Vector3 particleRotationEuler = Quaternion.LookRotation(particleCollisionEvent.normal).eulerAngles;
        particleRotationEuler.z = Random.Range(0, 360);
        particleData[particleDecalDataIndex].rotation = particleRotationEuler;
        particleData[particleDecalDataIndex].size = Random.Range(decalSizeMin, decalSizeMax);
        particleData[particleDecalDataIndex].color = colour.Evaluate(Random.Range(0f, 1f));
        particleDecalDataIndex++;
    }
    void DisplayParticles()
    {
        for (int i = 0; i < particleData.Length; i++)
        {
            particles[i].position = particleData[i].position;
            particles[i].rotation3D = particleData[i].rotation;
            particles[i].startSize = particleData[i].size;
            particles[i].startColor = particleData[i].color;
        }
        decalParticleSystem.SetParticles(particles, particles.Length);
    }
}
