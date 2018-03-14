using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.Collections;
using UnityEngine.XR.WSA.Input;
using System;
using UnityEngine.UI;

//Creator: Brian Boersen

public class HandDrawInput : DrawInput, IInputHandler
{

    #region variables
    //delagate
    [HideInInspector]public delegate void clickedDelagate(uint id);

    //clicked event
    [HideInInspector] public static event clickedDelagate ClickedDown;

    //clicked up event
    [HideInInspector] public static event clickedDelagate ClickedUp;

    //pressed bool
    private bool pressed = false;

    //hands tracked
    private List<uint> hands = new List<uint>();

    //hands tracked as objects
    /// <summary>
    /// a dictionary of uints and gameobjects
    /// </summary>
    [HideInInspector] public Dictionary<uint, GameObject> trackedHandObjects = new Dictionary<uint, GameObject>();

    //handposition
    private Vector3 vec;

    //debug
    [SerializeField] private Text[] debugtext;

    private int debugHandAmount;
    private int handlostcount;
    private int debugupdate;
    //

    #endregion

    private void Awake()
    {
        InteractionManager.InteractionSourceDetected += InteractionManager_SourceDetected;
        InteractionManager.InteractionSourceLost += InteractionManager_SourceLost;
        InteractionManager.InteractionSourceUpdated += InteractionManager_SourceUpdated;

        debugtext[4].text = "pressed: " + "false";
    }

    private void InteractionManager_SourceUpdated(InteractionSourceUpdatedEventArgs arg)
    {
        uint id = arg.state.source.id;

        if (arg.state.source.kind == InteractionSourceKind.Hand)
        {
            debugupdate++;
            debugtext[3].text = "update: " + debugupdate;

            if (trackedHandObjects.ContainsKey(id))
            {
                //get/set position
                if (arg.state.sourcePose.TryGetPosition(out vec))
                {
                    trackedHandObjects[id].transform.position = vec;
                    debugtext[0].text = "HandDrawPosition: " + trackedHandObjects[id].transform.position;
                }

                if (arg.state.selectPressed)
                {
                    ClickedDown(id);
                    pressed = true;

                    debugtext[4].text = "pressed: " + id + " presed";
                }

                if (arg.state.anyPressed)
                {
                    debugtext[4].text = "pressed: " + id + " anyPressed";
                }

                if (arg.state.menuPressed)
                {
                    debugtext[4].text = "pressed: " + id + " menuPressed";
                }

                Vector3 velocity;

                if (arg.state.sourcePose.TryGetAngularVelocity(out velocity))
                {
                    debugtext[6].text = "velocity: " + velocity;
                }

                if (pressed && arg.state.selectPressed)
                {
                    ClickedUp(id);
                    pressed = false;

                    debugtext[4].text = "pressed: " + "no";
                }
            }
        }
    }

    private void InteractionManager_SourceLost(InteractionSourceLostEventArgs arg)
    {
        if (arg.state.source.kind != InteractionSourceKind.Hand)
        {
            return;
        }

        uint id = arg.state.source.id;

        if (hands.Contains(id))
        {
            //remove id
            hands.Remove(arg.state.source.id);

            //destroy object
            Destroy(trackedHandObjects[id]);

            //removeobject
            trackedHandObjects.Remove(id);

            debugHandAmount--;
            debugtext[1].text = "hand detected: " + debugHandAmount;

            handlostcount++;
            debugtext[2].text = "hand lost: " + handlostcount + " id: " + id;
        }
    }

    private void InteractionManager_SourceDetected(InteractionSourceDetectedEventArgs arg)
    {
        //check if its a hand
        if (arg.state.source.kind != InteractionSourceKind.Hand)
        {
            return;
        }

        //get hand id
        uint id = arg.state.source.id;

        //debug
        debugtext[5].text = "new Hand ID: " + id;

        //debug
        debugHandAmount++;
        debugtext[1].text = "Hand detected: " + debugHandAmount;

        //record id
        hands.Add(arg.state.source.id);

        //create a new hand object
        GameObject gObj = new GameObject(id.ToString());

        //hand position
        Vector3 pos;

        //this function sets pos position and returns true if sucsesfull
        if (arg.state.sourcePose.TryGetPosition(out pos))
        {
            //set our newly created objects position 
            gObj.transform.position = pos;
        }

        //tracked hands
        trackedHandObjects.Add(id, gObj);
    }

    public void OnInputDown(InputEventData eventData)
    {
        base.down();
    }

    public void OnInputUp(InputEventData eventData)
    {
        base.up();
    }

}
