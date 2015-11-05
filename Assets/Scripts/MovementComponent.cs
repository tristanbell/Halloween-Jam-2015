using UnityEngine;
using System.Collections;

public enum EDirection
{
	UP,
	DOWN,
	LEFT,
	RIGHT,
	NONE
}

public class MovementComponent : MonoBehaviour
{

	private bool m_bShouldMove = false;
	private EDirection m_eMovementDirection = EDirection.NONE;
	private Vector2 m_vGridPosition = new Vector2 (0, 0);

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
		if (m_bShouldMove) {
			Move ();

			// Stop moving
			m_bShouldMove = false;
		}
	}

	public void DoMovement ()
	{
		m_bShouldMove = true;
	}

	public Vector2 GetPosition ()
	{
		return m_vGridPosition;
	}

	public void SetPosition (Vector2 pos)
	{
		m_vGridPosition = pos;
	}

	public void SetDirection (EDirection i_eNewDirection)
	{
		m_eMovementDirection = i_eNewDirection;
	}

	public EDirection GetDirection ()
	{
		return m_eMovementDirection;
	}

	private void Move ()
	{
		Vector3 translation = new Vector3 (0, 0, 0);
		GridScript gridScript = gameObject.GetComponentInParent<GridScript> ();

		switch (m_eMovementDirection) {
		// TODO Add bounds checking
		case EDirection.UP:
			{
				//translation.y = gridScript.tileSize;
				m_vGridPosition.y++;
			}
			break;
		case EDirection.DOWN:
			{
				//translation.y = -gridScript.tileSize;
				m_vGridPosition.y--;
			}
			break;
		case EDirection.LEFT:
			{
				//translation.x = -gridScript.tileSize;
				m_vGridPosition.x--;
			}
			break;
		case EDirection.RIGHT:
			{
				//translation.x = gridScript.tileSize;
				m_vGridPosition.x++;
			}
			break;
		default:
			break;
		}

		//GridObject go = gameObject.GetComponent<GridObject>();
		//go.transform.Translate(translation);
	}
}
