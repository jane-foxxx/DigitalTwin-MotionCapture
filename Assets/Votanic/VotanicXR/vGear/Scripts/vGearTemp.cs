using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Votanic.vMedia.MediaPlayer;
using Votanic.vXR.vCast;
using Votanic.vXR.vGear;
using Votanic.vXR.vGear.Networking;

public class vGearTemp : MonoBehaviour
{
    public vGear_Menu menu;
    public MediaPlayer mediaPlayer;

    public vGear_Networking networking;
    public vGear_Panel inputPanel;
    public vGear_Panel messagePanel;
    public InputField inputText;
    public Text receivedMessage;
    public vGear_VirtualKeyboard virtualKeyboard;

    // Start is called before the first frame update
    void Start()
    {
        vGear.Cmd.Send("Custom");
    }

    // Update is called once per frame
    void Update()
    {
        //Get Command
        if (vGear.Cmd.Received("Custom"))
        {
            CustomMethod();
        }

        //Get Controller Input
        if (vGear.Ctrl.ButtonUp(0))
        {
            ControllerMethod();
        }

        //Get Device Input
        if (vGear.Input.KeyboardUp(KeyCode.C))
        {
            inputPanel.Close();
            virtualKeyboard.Close();
        }

        if (networking)
        {
            if (vGear.Ctrl.ButtonUp(2) || vGear.Input.KeyboardUp(KeyCode.T))
            {
                Open();
            }
            if (networking.messages.Count > 0)
            {
                receivedMessage.text = networking.messages[0];
                messagePanel.Open(vGear.user.TransformPoint(0, 1.25f, 1), vGear.user.eulerAngles + new Vector3(30, 0, 0));
                networking.messages.Clear();
                inputPanel.Close();
                virtualKeyboard.Close();
            }
        }

        //vGear.Frame.Transform(targetPos, targetRot);
    }
    void Open()
    {
        inputPanel.Open(vGear.user.TransformPoint(0, 1.25f, 1), vGear.user.eulerAngles + new Vector3(30, 0, 0));
        messagePanel.Close();
        inputText.ActivateInputField();
        inputText.Select();
        inputText.MoveTextEnd(true);
        //vGear.controller.SetTool("DrumStick");
    }

    public void Send()
    {
        List<int> id = new List<int>();
        foreach (vGear_NetworkUser u in networking.GetAllNetworkUsers())
        {
            if (u.userID != networking.networkID)
            {
                id.Add((int)u.userID);
            }
        }
        if (id.Count == 0) id.Add(-1);
        networking.Send(inputText.text, true, false, id.ToArray());
    }

    void CustomMethod()
    {
        if (menu)
        {
            menu.Open();
            //Direct Commands: Open, Close, Enable, Disable
        }
    }

    void ControllerMethod()
    {
        if (mediaPlayer)
        {
            mediaPlayer.Play();
            //Direct Commands: Play, Stop, StopAll, Pause, UnPause, Next, Previous, Last, First, Volume, Mute, Loop, AutoPlay, AutoNext
        }
    }
}
