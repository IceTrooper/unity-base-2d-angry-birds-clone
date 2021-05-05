using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public void Spawn(GameObject particlesPrefab)
    {
        Instantiate(particlesPrefab, transform.position, Quaternion.identity, transform.parent);
    }
}
