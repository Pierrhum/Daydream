using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICard : MonoBehaviour
{
    
    public void OnClick()
    {
        Debug.Log("click");
    }

    public void OnDrag()
    {
        Debug.Log("drag");
    }

    public void OnDrop()
    {
        Debug.Log("drop");
    }
}
