using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LazerPoint : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] LayerMask PointerTraceLayerMask;
    public bool GetFocusedObject(out GameObject objectInFocus,out Vector3 contactPoint)
    {
        objectInFocus = null;
        contactPoint = Vector3.zero;
        if(Physics.Raycast(transform.position,
            transform.forward,out RaycastHit hitInfo, 
            (lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0)).magnitude,
            PointerTraceLayerMask))
        {
            objectInFocus = hitInfo.collider.gameObject;
            contactPoint = hitInfo.point;
            Debug.Log(objectInFocus);
            return true;
        }
        return false;
    }


    private void Start()
    {
        if(lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
    }

    internal Vector2 GetPointerScreenPosition()
    {
        float traceLength = (lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0)).magnitude;
        Vector3 POinterPOs = transform.position + transform.forward * traceLength;
        return Camera.main.WorldToScreenPoint(POinterPOs);
    }

    public GameObject GetCurrentPointingUI()
    {
        List<RaycastResult> UIRaycastResults = new List<RaycastResult>();
        PointerEventData EventData = new PointerEventData(EventSystem.current);
        EventData.position = GetPointerScreenPosition();
        EventSystem.current.RaycastAll(EventData, UIRaycastResults);
        if(UIRaycastResults.Count > 0)
        {
            return UIRaycastResults[0].gameObject;
        }
        return null;
    }
}
