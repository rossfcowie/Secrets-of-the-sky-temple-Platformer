using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputsTICK : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    Vector2 home;
    public float radius = 300f; // Define the radius
    private RectTransform rt;

    public void Awake()
    {
        rt = gameObject.GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, data.position, data.pressEventCamera, out globalMousePos))
        {
            // Calculate the direction and distance from the home position
            Vector3 direction = globalMousePos - (Vector3)home;
            
            if (direction.magnitude < radius)
            {
                // If within radius, set the position to the dragged position
                rt.position = globalMousePos;
            }
            else
            {
                // If beyond radius, clamp the position to the radius
                rt.position = home + (Vector2)direction.normalized * radius;
            }
        }
    }

    void Update()
    {
        // Any other logic you may need can go here
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rt.localPosition = Vector2.zero; // Reset the joystick to the home position
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        home = rt.position;
    }
}
