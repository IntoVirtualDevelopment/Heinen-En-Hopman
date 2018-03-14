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

    //hands tracked
    private List<uint> hands = new List<uint>();

    //hands tracked as objects
    public Dictionary<uint, GameObject> trackedHandObjects = new Dictionary<uint, GameObject>();

    //handposition
    private Vector3 vec;

    //debug
    [SerializeField] private Text[] debugtext;

    private int debugHandAmount;
    private int handlostcount;
    private int debugupdate;

    #endregion

    private void Awake()
    {
        InteractionManager.InteractionSourceDetected += InteractionManager_SourceDetected;
        InteractionManager.InteractionSourceLost += InteractionManager_SourceLost;
        InteractionManager.InteractionSourceUpdated += InteractionManager_SourceUpdated;
    }

    private void InteractionManager_SourceUpdated(InteractionSourceUpdatedEventArgs arg)
    {
        if (arg.state.source.kind != InteractionSourceKind.Hand)
        {
            return;
        }

        arg.state.sourcePose.TryGetPosition(out vec);
        debugtext[0].text = "HandDrawPosition: " + vec;

        debugupdate++;
        debugtext[3].text = "update: " + debugupdate;

        /*uint id = state.source.id;
        Vector3 pos;

        if (state.source.kind == InteractionSourceKind.Hand)
        {
            if (trackingObject.ContainsKey(state.source.id))
            {
                if (state.properties.location.TryGetPosition(out pos))
                {
                    trackingObject[state.source.id].transform.position = pos;
                }
            }
        }*/
    }

    private void InteractionManager_SourceLost(InteractionSourceLostEventArgs arg)
    {
        if (arg.state.source.kind != InteractionSourceKind.Hand)
        {
            return;
        }

        debugHandAmount--;
        debugtext[1].text = "hand detected: " + debugHandAmount;

        handlostcount++;
        debugtext[2].text = "hand lost: " + handlostcount;
    }

    private void InteractionManager_SourceDetected(InteractionSourceDetectedEventArgs arg)
    {
       if (arg.state.source.kind != InteractionSourceKind.Hand)
        {
            return;
        }

        debugHandAmount++;
        debugtext[1].text = "Hand detected: " + debugHandAmount;

        debugtext[4].text = "Hand ID: " + arg.state.source.id;

        arg.state.sourcePose.TryGetPosition(out vec);

        hands.Add(arg.state.source.id);

        var obj = Instantiate(gameObject) as GameObject;
        Vector3 pos;

        if (arg.state.sourcePose.TryGetPosition(out pos))
        {
            obj.transform.position = pos;
        }

        trackedHandObjects.Add(arg.state.source.id, obj);

        /*trackedHands.Add(state.source.id);

        var obj = Instantiate(TrackingObject) as GameObject;
        Vector3 pos;
        if (state.properties.location.TryGetPosition(out pos))
        {
            obj.transform.position = pos;
        }
        trackingObject.Add(state.source.id, obj);*/
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
