using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Particle : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;

    //[SerializeField] private GameObject VFXPrefab;
    private void Start()
    {
        particleSystem.Stop();
    }

    public void StartParticleSystem() => particleSystem.Play();

    //viet corutine stop after life time
    //private IEnumerator StopParticleSystem(ParticleSystem particleSystem)
    //{
    //    yield return new WaitForSeconds(particleSystem.main.duration);
    //    //stop
    //    particleSystem.Stop();
    //    //neu ko dc destroy tai day
    //    Destroy(particleSystem.gameObject);
        
    //}

    //public void StartParticleSystem()
    //{
    //    var VFX = Instantiate(VFXPrefab, transform.position, transform.rotation);

    //    var particleSystem = VFX.GetComponent<ParticleSystem>();

    //    particleSystem.Play();
    //    StartCoroutine(StopParticleSystem(particleSystem));

    //}

    //stop sau khi danh

    // instanciate partical >> destroy after duration
}
