using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambioCapaParticula : MonoBehaviour
{
    // Start is called before the first frame update
    public int capa = 16;
    void Start()
    {
        ParticleSystemRenderer particleRenderer = GetComponent<ParticleSystemRenderer>();
        particleRenderer.sortingOrder = capa;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
