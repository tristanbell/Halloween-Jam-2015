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
        print("Movement!");
        m_bShouldMove = true;
    }

    public Vector2 GetPosition()
    {
        return m_vGridPosition;
    }

    public void SetDirection(EDirection i_eNewDirection)
    {
        m_eMovementDirection = i_eNewDirection;
    }

    private void Move(EDirection m_eMovementDirection)
    {
        Vector3 translation = new Vector3(0, 0, 0);
        GridScript gridScript = gameObject.GetComponentInParent<GridScript>();

        switch (m_eMovementDirection)
        {
            // TODO Add bounds checking
            case EDirection.UP:
                {
                    translation.y = -gridScript.tileSize;
                }
                break;
            case EDirection.DOWN:
                {
                    translation.y = gridScript.tileSize;
                }
                break;
            case EDirection.LEFT:
                {
                    translation.x = -gridScript.tileSize;
                }
                break;
            case EDirection.RIGHT:
                {
                    translation.x = gridScript.tileSize;
                }
                break;
            default:
                break;
        }

        GridObject go = gameObject.GetComponent<GridObject>();
        go.transform.Translate(translation);
    }
}
