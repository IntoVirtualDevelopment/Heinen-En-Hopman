using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creator: Brian Boersen

public class LineManeger : MonoBehaviour
{
    #region variables

    [Tooltip("linerendere sould have one point")]
    [SerializeField] private GameObject linePrefab;
    
    [Tooltip("the lengh a line needs to be before it is set")]
    [SerializeField] private float lineSegmentLenght = 0.2f;

    [SerializeField] private GameObject styles;
  
    /// <summary>
    /// line game object
    /// </summary>
    private GameObject line;
    private LineRenderer lineRend;

    private int currentLinePoint;
    private Vector3 lastLinePosition;

    private bool drawing = true;

    //Debug
    public GameObject lineObject;

    #endregion
        
	private void Start ()
	{
        //if line prefab is empty
        if (!linePrefab)
        {
            //trow error
            Debug.LogError("no prefab asigned");
        }

        //debug
        lineRend = lineObject.GetComponent<LineRenderer>();

        setLinePoint();
        addNewPoint();
	}

	private void Update ()
	{
        if (drawing)
        {
            drawLine();
        }
	}

    public void startDraw()
    {
        //instantiate a line and set as current line so it can be manipulated
        line = Instantiate(linePrefab);

        //get the line rendere
        lineRend = line.GetComponent<LineRenderer>();

        //set every variable of the linerender

        //set first line point
        setLinePoint();

        //make new point
        addNewPoint();

        //set drawing true
        drawing = true;
    }

    public void stopDraw()
    {
        //stop drawing proces
        drawing = false;

        //if the point count is to small, destroy it!
        if(lineRend.positionCount <= 1)
        {
            Destroy(line);
        }

    }

    private void drawLine()
    {

        //check if line is ling enough
        if(Vector3.Distance(lastLinePosition,styles.transform.position) >= lineSegmentLenght)
        {
            //creat new line point
            addNewPoint();
        }

        //set line position
        setLinePoint();
    }

    private void setLinePoint()
    {
        //set point postition to styles position
        lineRend.SetPosition(currentLinePoint, styles.transform.position);
    }

    private void addNewPoint()
    {
        //add a point
        lineRend.positionCount++;

        //set last line poistion
        lastLinePosition = lineRend.GetPosition(currentLinePoint);

        //set currentpoint to newpoint
        currentLinePoint++;

        //set current point position
        setLinePoint();
    }

}
