using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ARCursor : MonoBehaviour
{
    public GameObject cursorChildObject;
    public GameObject objectToPlaceCsardas;
    public GameObject objectToPlaceHipHop;
    public GameObject objectToPlaceSamba;

    public List<GameObject> placedObjects;

    public RectTransform dropDown;
    public RectTransform startStop;
    public Button startStopButton;

    public ARRaycastManager raycastManager;

    public bool useCursor = true;

    private Animator[] anim;
    private int clickCount = 0;
    

    
    void Start()
    {
        cursorChildObject.SetActive(useCursor);

        anim = GetComponents<Animator>();

        startStopButton = startStopButton.GetComponent<Button>();
        startStopButton.onClick.AddListener(this.StartStopOnClick);
    }

    
    void Update()
    {
        if (useCursor)
        {
            UpdateCursor();
        }

        TMPro.TMP_Dropdown dd = dropDown.GetComponent<TMPro.TMP_Dropdown>();
        int menuIndex = dd.value;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            anim = GetComponents<Animator>();

            if (useCursor)
            {

                if (menuIndex == 0)
                {
                    GameObject placed = GameObject.Instantiate(objectToPlaceCsardas, transform.position, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
                    placedObjects.Add(placed);
                }

                if (menuIndex == 1)
                {
                    GameObject placed = GameObject.Instantiate(objectToPlaceHipHop, transform.position, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
                    placedObjects.Add(placed);
                }

                if (menuIndex == 2)
                {
                    GameObject placed = GameObject.Instantiate(objectToPlaceSamba, transform.position, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
                    placedObjects.Add(placed);
                }


                foreach (GameObject go in placedObjects)
                {
                    Animator a = go.GetComponentInChildren<Animator>();
                    // a.enabled = true;
                    a.Rebind();
                    a.Update(0f);
                }

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
                    

                    if (menuIndex == 0)
                    {
                        GameObject placed = GameObject.Instantiate(objectToPlaceCsardas, hits[0].pose.position, hits[0].pose.rotation * Quaternion.Euler(0f, 180f, 0f));
                        placedObjects.Add(placed);
                    }

                    if (menuIndex == 1)
                    {
                        GameObject placed = GameObject.Instantiate(objectToPlaceHipHop, hits[0].pose.position, hits[0].pose.rotation * Quaternion.Euler(0f, 180f, 0f));
                        placedObjects.Add(placed);
                    }

                    if (menuIndex == 2)
                    {
                        GameObject placed = GameObject.Instantiate(objectToPlaceSamba, hits[0].pose.position, hits[0].pose.rotation * Quaternion.Euler(0f, 180f, 0f));
                        placedObjects.Add(placed);
                    }


                    foreach (GameObject go in placedObjects)
                    {
                        Animator a = go.GetComponentInChildren<Animator>();
                        // a.enabled = true;
                        a.Rebind();
                        a.Update(0f);
                    }
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

    void StartStopOnClick()
    {
        Debug.Log("Click");

        if (clickCount % 2 == 0)
        {
            foreach (GameObject go in placedObjects)
            {
                Animator a = go.GetComponentInChildren<Animator>();

                a.enabled = false;
            }
        }

        else
        {
            foreach (GameObject go in placedObjects)
            {
                Animator a = go.GetComponentInChildren<Animator>();
                a.enabled = true;

                a.Rebind();
                a.Update(0f);
            }
        }

        clickCount++;
    }
}
