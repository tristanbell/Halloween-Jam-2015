using UnityEngine;
using System.Collections;

enum EDirection
{
    UP, DOWN,
    LEFT, RIGHT
}

public class MovementComponent : MonoBehaviour {

    GridObject m_gridObjectOwner = null;
    bool m_bShouldMove = false;
    EDirection m_eMovementDirection = EDirection.UP;

    public MovementComponent()
    {
    }

	// Use this for initialization
	void Start () 
    {
        Vector2 m_vGridPosition = new Vector2(0, 0);
	}

    private void DoMovement(EDirection m_eMovementDirection)
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
            default:
                break;
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
	    if (m_bShouldMove)
        {
            DoMovement(m_eMovementDirection);
        }
	}
}
