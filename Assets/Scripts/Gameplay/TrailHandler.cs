using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailHandler : MonoBehaviour {
    TrailRenderer trail;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        trail.autodestruct = false;
    }
    
    private void OnEnable()
    {
        trail.Clear();
    }

}
