using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    
    public float lifeDuration = 2.0f;
    public float bulletRadius = .5f;

    void Awake()
    {
        
        Destroy(gameObject, lifeDuration);
    }
}


