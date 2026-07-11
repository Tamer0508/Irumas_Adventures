using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyVFX : MonoBehaviour
{
    private ParticleSystem _ps;

    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (_ps && !_ps.IsAlive()) 
        {
            DestroySelf(); 
        }
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
