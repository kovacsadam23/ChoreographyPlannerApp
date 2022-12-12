using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onStartStopClick : MonoBehaviour
{
    int clickCount = 1;
    GameObject[] gameObjects;

    private void Start()
    {
        gameObjects = GetComponents<GameObject>();
        Debug.Log(clickCount);

    }

    
    void Update()
    {
        if (clickCount % 2 == 0)
        {
            foreach (GameObject go in gameObjects)
            {
                Animator a = go.GetComponentInChildren<Animator>();

                a.enabled = false;
            }
        }

        else
        {
            foreach (GameObject go in gameObjects)
            {
                Animator a = go.GetComponentInChildren<Animator>();
                a.enabled = true;

                a.Rebind();
                a.Update(0f);
            }
        }

        clickCount++;
        Debug.Log(clickCount.ToString());
    }
    
}
