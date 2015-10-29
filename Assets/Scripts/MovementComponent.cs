using UnityEngine;
using System.Collections;

public enum EDirection
{
    UP, DOWN,
    LEFT, RIGHT,
    NONE
}

public class MovementComponent : MonoBehaviour {

    private bool m_bShouldMove = false;
    private EDirection m_eMovementDirection = EDirection.NONE;
    private Vector2 m_vGridPosition = new Vector2(0, 0);

	// Use this for initialization
	void Start () 
    {
	}

    // Update is called once per frame
    void Update()
    {
        if (m_bShouldMove)
        {
            Move(m_eMovementDirection);

            // Stop moving
            m_bShouldMove = false;
        }
    }

    public void DoMovement()
    {
        m_bShouldMove = true;
    }

    public void SetDirection(EDirection i_eNewDirection)
    {
        m_eMovementDirection = i_eNewDirection;
    }

	public EDirection GetDirection()
	{
		return m_eMovementDirection;
	}

    private void Move(EDirection m_eMovementDirection)
    {
        switch (m_eMovementDirection)
        {
            // TODO Add bounds checking
            case EDirection.UP:
                {
                    m_vGridPosition.y--;
                }
                break;
            case EDirection.DOWN:
                {
                    m_vGridPosition.y++;
                }
                break;
            case EDirection.LEFT:
                {
                    m_vGridPosition.x--;
                }
                break;
            case EDirection.RIGHT:
                {
                    m_vGridPosition.x++;
                }
                break;
            default:
                break;
        }
    }
}
