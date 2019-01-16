using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DropImg : MonoBehaviour, IDropHandler,IPointerEnterHandler
{

    void Start()
    {
      
    }

    public void OnDrop(PointerEventData eventData)
    {
        print(eventData.pointerEnter.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print(eventData.pointerEnter.name);
    }
}
