using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creator: Brian Boersen

public class DrawInput : MonoBehaviour
{
    #region variables

    [SerializeField] private MoveStylesAgainstObjects moveStyles;

    #endregion

    private void Awake()
    {
        if (!moveStyles)
        {
            Debug.LogError("moveStyles not assigned");
        }
    }

    public void up ()
	{
        //say up to moveStyles
        moveStyles.inputUp();
	}

	public void down ()
	{
        //say you pressed down
        moveStyles.inputDown();
	}

}
