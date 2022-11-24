using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onStartStopClick : MonoBehaviour
{

    private void Start()
    {
        Animator[] anim = GetComponents<Animator>();
        
        foreach (Animator a in anim)
        {
            a.StopPlayback();
        }

        foreach (Animator a in anim)
        {
            a.StartPlayback();
        }

    }

    /*
    void Update()
    {
        var allAnimators = FindObjectOfType<Animation>();

        foreach (var animator in allAnimators)
        {
            // animator.Stop();
        }
    }
    */
}
