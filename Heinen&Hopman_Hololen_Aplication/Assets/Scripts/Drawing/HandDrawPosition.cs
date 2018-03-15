using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creator: Brian Boersen

public class HandDrawPosition : MonoBehaviour
{
    #region variables
    //draw in on hand or on object
    public bool drawOnObject = false;

    //camera
    [SerializeField] private GameObject view;

    //line manager
    [SerializeField] private LineManeger lineManager;

    //styles
    [SerializeField] private GameObject styles;

    //ray lengt
    [SerializeField] private float rayLengt = 20;

    //fall back length
    [SerializeField,Range(.5f,10)] private float fallbackLenght = 1.5f;

    //hand where you are drawing with
    private GameObject drawingHand;

    //bool for knowing whent to set the styles
    private bool movingStyles = false;

    //vector for calculating ray rotation
    private Vector3 rayRotation;

    #endregion
        
	private void Start ()
	{
		
	}

	private void Update ()
	{
        if (movingStyles)
        {
            if (!drawOnObject)
            {
                //draw on hand
                moveStyles(drawingHand.transform.position);
            }
            else
            {
                //draw on object
                moveStyles(GetRayPosition());
            }
        }
	}

    private Vector3 GetRayPosition()
    {
        rayRotation = transform.TransformDirection(drawingHand.transform.position - view.transform.position);

        if (Physics.Raycast(transform.position, rayRotation, rayLengt))
        {
            print("There is something in front of the object!");
        }
        else
        {
            return drawingHand.transform.position;
        }

        /*
        //get rotation from view to the hand
        rayRotation =  Vector3.Normalize(drawingHand.transform.position - view.transform.position);

        Quaternion e = Quaternion.Euler(rayRotation);
        */

        //return the new position
        return Vector3.forward;
    }

    private void moveStyles(Vector3 newPosition)
    {
        styles.transform.position = newPosition;
    }

    private void OnEnable()
    {
        HandDrawInput.ClickedDown += HandDrawInput_ClickedDown;
        HandDrawInput.ClickedUp += HandDrawInput_ClickedUp;
    }

    private void OnDisable()
    {
        HandDrawInput.ClickedDown -= HandDrawInput_ClickedDown;
        HandDrawInput.ClickedUp -= HandDrawInput_ClickedUp;
    }

    private void HandDrawInput_ClickedUp(GameObject hand)
    {
        //set hand
        drawingHand = hand;

        //stop drawing
        lineManager.stopDraw();

        //debug
        moveStyles(hand.transform.position);
    }

    private void HandDrawInput_ClickedDown(GameObject hand)
    {
        //start drawing
        lineManager.startDraw();
    }
}
