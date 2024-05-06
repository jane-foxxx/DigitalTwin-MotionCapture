using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Votanic.vXR.vCast;

public class vCastTemp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        vCast.Cmd.Send("Custom");
    }

    // Update is called once per frame
    void Update()
    {
        //Get Command
        if (vCast.Cmd.Received("Custom"))
        {
            CustomMethod();
        }
        //Get Controller Input
        if (vCast.Ctrl.ButtonUp(0))
        {
            ControllerMethod();
        }

        //Get Device Input
        if (vCast.Input.KeyboardUp(KeyCode.Space))
        {

        }

        //vCast.Frame.Transform(targetPos, targetRot);
    }

    void CustomMethod()
    {

    }

    void ControllerMethod()
    {

    }
}
