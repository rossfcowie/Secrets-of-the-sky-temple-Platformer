using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inputbutton : MonoBehaviour,IPointerExitHandler,IPointerEnterHandler, IPointerDownHandler,IPointerUpHandler
{
    public enum button{
        Left,Right,Up,Down,Action
    }
    public button myButton;
    void interactbuttonon(){
        Player.buttonDown[myButton] = true;
    }
    void interactbuttonoff(){
        Player.buttonDown[myButton] = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        interactbuttonon();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        interactbuttonoff();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        interactbuttonon();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        interactbuttonoff();
    }
}
