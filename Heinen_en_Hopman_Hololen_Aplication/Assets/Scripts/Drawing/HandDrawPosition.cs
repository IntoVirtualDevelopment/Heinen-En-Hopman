using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

//Creator: Brian Boersen

[RequireComponent(typeof(LineManeger),typeof(HandDrawInput))]
public class HandDrawPosition : MonoBehaviour
{
    #region variables
    //draw in on hand or on object
    public bool drawOnObject = false;

    //camera
    [SerializeField,Tooltip("usualy the camera")] private GameObject view;

    //line manager
    private LineManeger lineManager;

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
    private Vector3 rayDirection;

    private RaycastHit hitInfo;

    #endregion
        
	private void Start ()
	{
        lineManager = GetComponent<LineManeger>();
	}

	private void Update ()
	{
        if (movingStyles)
        {
            if (drawOnObject)
            {
                //draw on object
                moveStyles(GetRayPosition());
            }
            else
            {
                //draw on hand
                moveStyles(drawingHand.transform.position);           
            }
        }
	}

    /// <summary>
    /// draws a line trough the view(camera) and hand and gives back the hit position.
    /// returns a defaul if nothing is hit
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRayPosition()
    {
        //get the direction the ray is suposed to go
        rayDirection = transform.TransformDirection(drawingHand.transform.position - view.transform.position);

        //fire ray
        if (Physics.Raycast(transform.position, rayDirection, out hitInfo, rayLengt))
        {
            //when hit
            return hitInfo.transform.position;
        }
        else
        {
            //fallback when not hit
            return view.transform.position + (Vector3.Normalize(rayDirection) * fallbackLenght);
        }
    }

    /// <summary>
    /// moves the styles to given position
    /// </summary>
    /// <param name="newPosition"></param>
    private void moveStyles(Vector3 newPosition)
    {
        //move the styles
        styles.transform.position = newPosition;
    }

    private void OnEnable()
    {
        //get input from handDrawInput
        HandDrawInput.ClickedDown += HandDrawInput_ClickedDown;
        HandDrawInput.ClickedUp += HandDrawInput_ClickedUp;
    }

    private void OnDisable()
    {
        //remove input
        HandDrawInput.ClickedDown -= HandDrawInput_ClickedDown;
        HandDrawInput.ClickedUp -= HandDrawInput_ClickedUp;
    }

    private void HandDrawInput_ClickedDown(GameObject hand)
    {
        //set hand
        drawingHand = hand;

        if (drawOnObject)
        {
            //draw on object
            moveStyles(GetRayPosition());
        }
        else
        {
            //draw on hand
            moveStyles(drawingHand.transform.position);
        }

        //start moving the styles
        movingStyles = true;

        //start drawing
        lineManager.startDraw();
    }

    private void HandDrawInput_ClickedUp(GameObject hand)
    {
        //stop drawing
        lineManager.stopDraw();

        //stop moving the styles
        movingStyles = false;
    }

}
