using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
public class RemoveTouch : MonoBehaviour
{
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Button " + eventData.pointerId + " up");
    }
}
