using UnityEngine;
using System.Collections;

public class LineBehaviourScript : MonoBehaviour 
{
    EDirection m_eMovementDirection = EDirection.UP;
	// Use this for initialization
	void Start () 
    {
	    
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
