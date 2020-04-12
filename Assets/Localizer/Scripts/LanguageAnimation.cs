using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageAnimation : MonoBehaviour {
    public StartupManager startup;
    
    public void AnimationComplete()
    {
        startup.AnimationComplete();
    }
}
