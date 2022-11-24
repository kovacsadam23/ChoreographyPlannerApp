using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ARCursor : MonoBehaviour
{
    public GameObject cursorChildObject;
    public GameObject objectToPlace;
    public List<GameObject> placedObjects;

    // private Animator anim;

    private Animator[] anim;

    public ARRaycastManager raycastManager;

    public bool useCursor = true;

    
    void Start()
    {
        // GameObject.Find("").GetComponentInChildren<Text>().text = "Sync";
        cursorChildObject.SetActive(useCursor);

        anim = GetComponents<Animator>();

    }

    
    void Update()
    {
        if (useCursor)
        {
            UpdateCursor();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            anim = GetComponents<Animator>();

            if (useCursor)
            {
                foreach (Animator a in anim)
                {
                    a.speed = 0;
                }

                GameObject placed = GameObject.Instantiate(objectToPlace, transform.position, transform.rotation);
                // placed.GetComponentInChildren<Animator>().speed = 0;
                // anim = GetComponents<Animator>();
                placedObjects.Add(placed);

                foreach (GameObject go in placedObjects)
                {
                    Animator a = go.GetComponentInChildren<Animator>();
                    a.Rebind();
                    a.Update(0f);
                }

                /*
                for (int i = 0; i < placedObjects.Count; i++)
                {
                    if (i == placedObjects.Count - 1)
                    {
                        GameObject go = placedObjects[i];
                        GameObject.Instantiate(go, transform.position, transform.rotation);
                        go.GetComponent<Animator>().StopPlayback();
                    }
                }
                */

            }

            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

                if (hits.Count > 0)
                {
                    foreach (Animator a in anim)
                    {
                        a.speed = 0;
                    }

                    GameObject placed = GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                    // placed.GetComponentInChildren<Animator>().speed = 0;
                    // anim = GetComponents<Animator>();
                    placedObjects.Add(placed);

                    foreach (GameObject go in placedObjects)
                    {
                        Animator a = go.GetComponentInChildren<Animator>();
                        a.Rebind();
                        a.Update(0f);
                    }

                    /*
                    for (int i = 0; i < placedObjects.Count; i++)
                    {
                        if (i == placedObjects.Count - 1)
                        {
                            GameObject go = placedObjects[i];
                            GameObject.Instantiate(go, transform.position, transform.rotation);
                            go.GetComponent<Animator>().StopPlayback();
                        }
                    }
                    */
                }
            }
        }
    }

    void UpdateCursor()
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }


    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(5);
    }
}
