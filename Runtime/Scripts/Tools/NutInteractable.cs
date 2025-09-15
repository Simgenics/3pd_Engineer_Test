using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutInteractable : MonoBehaviour
{

    public GameObject ToolSnapPoint;

    private void Start()
    {
        if(ToolSnapPoint==null)
        {
            ToolSnapPoint = gameObject;
        }
    }

}
