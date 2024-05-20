using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys the game object when the particle system has finished playing.
/// </summary>
public class DestroyParticles : MonoBehaviour
{
    private ParticleSystem ps;

    /// <summary>
    /// Get the particle system component
    /// </summary>
    public void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// Destroy the game object when the particle system has finished playing.
    /// </summary>
    public void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}