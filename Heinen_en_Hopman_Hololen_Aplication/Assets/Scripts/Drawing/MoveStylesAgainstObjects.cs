using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creator: Brian Boersen

public class MoveStylesAgainstObjects : MonoBehaviour
{
    #region variables

    [SerializeField, Tooltip("script that gives the 3d position to set the styles")] private DrawPosition drawPos;
    [SerializeField, Tooltip("styles object tracked by the LineManager")] private GameObject styles;
    [SerializeField, Tooltip("linemanager to draw the line")] private LineManeger lineManager;

    #endregion
        
	private void Awake ()
	{
        if (!drawPos)
        {
            Debug.LogError("no position script assigned. Without script there is no position to put the styles");
        }

        if (!styles)
        {
            Debug.LogError("no styles script assigned. Without styles there is no drawing");
        }

        if (!lineManager)
        {
            Debug.LogError("no LineManager script assigned. Without LineManager there is no line to be drawn");
        }

    }

    /// <summary>
    /// when clicked or taped
    /// </summary>
    public void inputDown()
    {
        //start setting the styles position
        StartCoroutine(setStylesPosition());
    }

    /// <summary>
    /// when mouse up or hand open
    /// </summary>
    public void inputUp()
    {
        //stop setting the styles position
        StopCoroutine(setStylesPosition());

        //set stop drawning on linemanager
        lineManager.stopDraw();
    }

    private IEnumerator setStylesPosition()
    {
        //start drawing the line in the linemanager
        lineManager.startDraw();

        yield return new WaitForEndOfFrame();
    }

}
