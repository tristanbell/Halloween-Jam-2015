using UnityEngine;
using System.Collections;

public class LineBehaviourScript : MonoBehaviour 
{
    EDirection m_eMovementDirection = EDirection.UP;
	// Use this for initialization
	void Start () 
    {
	    
	}

    private void Move(m_eMovementDirection)
    {
        switch (m_eMovementDirection)
        {
            case EDirection.UP:
                {

                }
                break;
            case EDirection.DOWN:
                {

                }
                break;
            case EDirection.RIGHT:
                {

                }
                break;
            case EDirection.LEFT:
                {

                }
                break;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        PollInput();
	}

    void PollInput()
    {
        if (Input.GetKey("w"))
        {
            m_eMovementDirection = EDirection.UP;
        }

        if (Input.GetKey("a"))
        {
            m_eMovementDirection = EDirection.LEFT;
        }

        if (Input.GetKey("s"))
        {
            m_eMovementDirection = EDirection.DOWN;
        }

        if (Input.GetKey("d"))
        {
            m_eMovementDirection = EDirection.RIGHT;
        }
    }
}
