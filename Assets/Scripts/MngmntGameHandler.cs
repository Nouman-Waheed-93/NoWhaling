using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MngmntGameHandler : MonoBehaviour {

    public static MngmntGameHandler instance;

    public Company Tasso;
    public JWH jwh;
    
    private void Awake()
    {
        instance = this;
    }
}
