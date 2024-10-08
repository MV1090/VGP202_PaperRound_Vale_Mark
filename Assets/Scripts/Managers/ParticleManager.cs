using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
    [Header("House Hit")]
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem bonusHitParticle;

    [Header("Smoke")]
    [SerializeField] ParticleSystem smoke_1;
    [SerializeField] ParticleSystem smoke_2;
    
    public void PlaySmoke(Transform spawnLocation)
    {
        smoke_1.transform.position = spawnLocation.position;
        smoke_2.transform.position = spawnLocation.position;
        smoke_1.Play();
        smoke_2.Play(); 
    }

    public void PlayHitParticle(Transform spawnLocation)
    {
        hitParticle.transform.position = spawnLocation.position;
        hitParticle.Play();
    }
    public void PlayBonusHitParticle(Transform spawnLocation)
    {
        bonusHitParticle.transform.position = spawnLocation.position;
        bonusHitParticle.Play();
    }

    public void StopParticle()
    {
        hitParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        smoke_1.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        smoke_2.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        bonusHitParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

}
