using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class particleManager : MonoBehaviour
{
    public static particleManager singleton = null;
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            return;
        }
        Destroy(this);
    }

    //We want an activate particle
    //We want a deActivate particle
    //We want a change speed
    //We want a change emission
    //We want a change sizze
    //We want a change direction



    public int startParticles(Vector3 position)
    {
        return particlePoolManager.singleton.GetParticle(position);
    }

    public void deActivateParticle(int particle)
    {
        particlePoolManager.singleton.ResetParticle(particle);
    }

    public void changeSpeed(int particle, float newSpeed)
    {
        //.simulationSpeed = newSpeed;
        ParticleSystem.MainModule mainParticleSystem = particlePoolManager.singleton.getSpecificParticle(particle).GetComponent<ParticleSystem>().main;
        mainParticleSystem.simulationSpeed = newSpeed;
    }

    public void changeRadius(int particle, float newRadius)
    {
        ParticleSystem.ShapeModule shapeParticleSystem = particlePoolManager.singleton.getSpecificParticle(particle).GetComponent<ParticleSystem>().shape;
        shapeParticleSystem.radius = newRadius;
    }

    public void changeFacing(int particle, Vector3 lookAt)
    {
        particlePoolManager.singleton.getSpecificParticle(particle).gameObject.transform.LookAt(lookAt);
    }

    public void setParent(int particle, GameObject obj)
    {
        particlePoolManager.singleton.getSpecificParticle(particle).gameObject.transform.parent = obj.transform;
    }
}
