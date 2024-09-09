using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
    [SerializeField] ParticleSystem hitParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHitParticle(Transform spawnLocation)
    {
        hitParticle.transform.position = spawnLocation.position;
        hitParticle.Play();
    }
    public void StopParticle()
    {
        hitParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

}
