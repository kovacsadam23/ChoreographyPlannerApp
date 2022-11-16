using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCursor : MonoBehaviour
{
    public GameObject cursorChildObject;
    public GameObject objectToPlace;
    private GameObject placed;

    private Animator anim;

    public ARRaycastManager raycastManager;

    public bool useCursor = true;

    
    void Start()
    {
        cursorChildObject.SetActive(useCursor);
        anim = objectToPlace.GetComponent<Animator>();
        // anim.Play("mixamo.com");
    }

    
    void Update()
    {
        if (useCursor)
        {
            UpdateCursor();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (useCursor)
            {
                GameObject.Instantiate(objectToPlace, transform.position, transform.rotation).GetComponent<Animation>().Play("mixamo.com");
                // anim.Play("mixamo.com");
                //objectToPlace.GetComponent<Animation>().Play("mixamo.com");
                // placed.GetComponent<Animator>().Play("mixamo.com");
            }

            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

                if (hits.Count > 0)
                {
                    GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation).GetComponent<Animation>().Play("mixamo.com");
                    // anim.Play("mixamo.com");
                    objectToPlace.GetComponent<Animation>().Play("mixamo.com");

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
}
