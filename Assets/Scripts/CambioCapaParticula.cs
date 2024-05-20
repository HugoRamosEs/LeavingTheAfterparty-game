using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Change the layer of the particle system
/// </summary>
public class cambioCapaParticula : MonoBehaviour
{
    public int capa = 16;
    /// <summary>
    /// Start is called before the first frame update, set the sorting order of the particle system
    /// </summary>
    void Start()
    {
        ParticleSystemRenderer particleRenderer = GetComponent<ParticleSystemRenderer>();
        particleRenderer.sortingOrder = capa;
    }
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    
    void Update()
    {
        
    }
}
