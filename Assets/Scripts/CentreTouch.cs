using UnityEngine;
using UnityEngine.EventSystems;

public class CentreTouch : MonoBehaviour, IPointerDownHandler
{
    public PickUpSystem PickUpSystemObj;
    public void OnPointerDown(PointerEventData eventData)
    {
        PickUpSystemObj.TouchToPickUp();
    }
}
