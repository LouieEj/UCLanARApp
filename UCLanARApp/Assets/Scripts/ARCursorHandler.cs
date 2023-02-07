using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARCursorHandler : MonoBehaviour
{
    [SerializeField] private GameObject cursorSpriteObject;
    [SerializeField] private GameObject objectToPlace;
    [SerializeField] private ARRaycastManager raycastManager;


    void Update()
    {
        UpdateCursor();

        if (Input.touchCount > 0) //check that the screen has been touched
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began) //check that this is a touch (and not user holding down touch on screen)
            {
                Instantiate(objectToPlace, transform.position, transform.rotation);
            }
        }
    }

    private void UpdateCursor()
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, raycastHits, TrackableType.Planes);

        if (raycastHits.Count > 0)
        {
            transform.position = raycastHits[0].pose.position;
            transform.rotation = raycastHits[0].pose.rotation;
        }
    }
}
