﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Votanic.vXR.vCast;

public class vCastInteractablesTemp : vCast_Interactables
{
    public override void Start()
    {
        base.Start();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }    

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
    public override void OnCollisionExit(Collision collision)
    {
        base.OnCollisionExit(collision);
    }

    //void OnWandSelect()
    //{
    //    Debug.Log("OnWandSelect");
    //}
    //void OnWandDeselect()
    //{
    //    Debug.Log("OnWandDeselect");
    //}
    //void OnWandPress()
    //{
    //    Debug.Log("OnWandPress");
    //}
    //void OnWandRelease()
    //{
    //    Debug.Log("OnWandRelease");
    //}
    //void OnWandGrab(Transform transform)
    //{
    //    Debug.Log("OnWandGrab -" + transform);
    //}
    //void OnWandUngrab(Transform transform)
    //{
    //    Debug.Log("OnWandUngrab -" + transform);
    //}
    //void OnVisionEnter()
    //{
    //    Debug.Log("OnVisionEnter");
    //}
    //void OnVisionExit()
    //{
    //    Debug.Log("OnVisionExit");
    //}
}
