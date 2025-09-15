using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SocketWrench : MonoBehaviour
{
    public GameObject ParentObject;

    public float BreakDistance;
    public float NotBrakeDistenceFromCenter;

    HingeJoint socketedHinge = null;


    private void Start()
    {
    }

    private void FixedUpdate()
    {
        DoSocketDistanceCheck();
    }

    private void DoSocketDistanceCheck()
    {
        if (socketedHinge)
        {

            float LeftHandDistance = 0;
            float LeftHandDistanceFromCenter = 0;
            float RightHandDistance = 0;
            float RightHandDistanceFromCenter = 0;

            if (RightHandDistanceFromCenter > NotBrakeDistenceFromCenter && RightHandDistance > BreakDistance)
            {
                DisconnectHinge();
            }

            if (LeftHandDistanceFromCenter > NotBrakeDistenceFromCenter && LeftHandDistance > BreakDistance)
            {
                DisconnectHinge();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NutInteractable>())
        {
            if (ParentObject != null)
            {
                if (socketedHinge)
                    Destroy(socketedHinge);

                GameObject NutSnapLocation = other.GetComponent<NutInteractable>().ToolSnapPoint;

                if (NutSnapLocation == null)
                    return;

                ParentObject.transform.position = NutSnapLocation.transform.position - NutSnapLocation.transform.forward;
                ParentObject.transform.LookAt(NutSnapLocation.transform, ParentObject.transform.up);
                ParentObject.transform.position += ParentObject.transform.forward;
                var offset = ParentObject.transform.position - transform.position;
                ParentObject.transform.position += offset;

                socketedHinge = ParentObject.AddComponent<HingeJoint>();
                socketedHinge.anchor = transform.localPosition;
                socketedHinge.axis = transform.forward;
            }
        }
    }

    public void DisconnectHinge()
    {
        if (ParentObject != null)
        {
            Destroy(socketedHinge);
            socketedHinge = null;
        }
    }

}
