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

    [SerializeField] private Text[] debugtext;

    private Vector3 vec;

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
        debugtext[1].text = "hand detected: " + debugHandAmount;

        arg.state.sourcePose.TryGetPosition(out vec);

        /*trackedHands.Add(state.source.id);

        var obj = Instantiate(TrackingObject) as GameObject;
        Vector3 pos;
        if (state.properties.location.TryGetPosition(out pos))
        {
            obj.transform.position = pos;
        }
        trackingObject.Add(state.source.id, obj);*/
    }

    private void Update()
    {
        
        //print("handpos: " + vec);
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
