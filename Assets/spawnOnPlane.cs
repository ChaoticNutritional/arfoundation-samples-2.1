using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]



public class spawnOnPlane : MonoBehaviour
{
    [SerializeField]
    GameObject PlacedPrefab;

    GameObject spawnedObject;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    ARRaycastManager myRaycastManager;


    // Start is called before the first frame update
    void Awake()
    {
        myRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool GetTouch(out Vector2 touch_position)
    {
        if (Input.touchCount > 0)
        {
            touch_position = Input.GetTouch(0).position;
            return true;
        }
        touch_position = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetTouch(out Vector2 touch_pos) == false)
        {
            return;
        }

        if(myRaycastManager.Raycast(touch_pos, hits, TrackableType.Planes))
        {
            var hitPose = hits[0].pose;

            if(spawnedObject == null)
            {
                spawnedObject = Instantiate(PlacedPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnedObject.transform.position = hitPose.position;
            }
        }
    }
}
