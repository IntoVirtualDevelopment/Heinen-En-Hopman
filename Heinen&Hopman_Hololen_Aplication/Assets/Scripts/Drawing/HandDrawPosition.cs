using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creator: Brian Boersen

public class HandDrawPosition : MonoBehaviour
{
    #region variables

    [SerializeField] private GameObject view;

    #endregion
        
	private void Start ()
	{
		
	}

	private void Update ()
	{
		
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

    private void HandDrawInput_ClickedUp(uint id)
    {
        //stop drawing
    }

    private void HandDrawInput_ClickedDown(uint id)
    {
        //draw
    }
}
