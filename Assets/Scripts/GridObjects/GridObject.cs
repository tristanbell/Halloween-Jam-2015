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

public class GridObject : MonoBehaviour
{
	private bool m_bShouldMove = false;
	private EDirection m_eMovementDirection = EDirection.NONE;
	private Vector2 m_vGridPosition = new Vector2 (0, 0);
	
	public GameObject gridObject;
	
	protected Animator animator;
	
	protected void Start()
	{
		animator = GetComponent<Animator> ();
	}
	
	public void DoMovement ()
	{
		Move ();
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
		
		if (animator) 
		{
			// Set the animation
			switch (GetDirection ()) {
			case EDirection.DOWN:
				animator.SetInteger("Direction", 0);
				break;
			case EDirection.LEFT:
				animator.SetInteger("Direction", 1);
				break;
			case EDirection.UP:
				animator.SetInteger("Direction", 2);
				break;
			case EDirection.RIGHT:
				animator.SetInteger("Direction", 3);
				break;
			}
		}
	}
	
	public EDirection GetDirection ()
	{
		return m_eMovementDirection;
	}
	
	protected void Move ()
	{
		Vector2 suggestedPosition = m_vGridPosition;
		GridScript gridScript = gridObject.GetComponent<GridScript>();
		
		switch (m_eMovementDirection) 
		{
			case EDirection.UP:
			{
				//suggestedPosition.y += gridScript.tileSize;
				suggestedPosition.y++;
			}
				break;
			case EDirection.DOWN:
			{
				//suggestedPosition.y -= gridScript.tileSize;
				suggestedPosition.y--;
			}
				break;
			case EDirection.LEFT:
			{
				//suggestedPosition.x -= gridScript.tileSize;
				suggestedPosition.x--;
			}
				break;
			case EDirection.RIGHT:
			{
				//suggestedPosition.x += gridScript.tileSize;
				suggestedPosition.x++;
			}
				break;
			default:
				break;
		}
		
		if (gridScript.get_collision ((int)suggestedPosition.x, (int)suggestedPosition.y))
		{
			// We have hit a wall; do nothing. TODO: Die upon hitting a wall
			print ("Wall!");
		} 
		else 
		{
			// Update position
			m_vGridPosition = suggestedPosition;
		}
		
		// Set the in-game position of this object
		Vector2 newPos = m_vGridPosition;
		transform.localPosition = newPos * gridScript.tileSize;
	}
	
	// Returns direction from LHS to RHS
	static public EDirection DirectionFromTo(GameObject lhs, GameObject rhs)
	{
		Vector3 lhsPos = lhs.transform.position;
		Vector3 rhsPos = rhs.transform.position;
		
		if (lhsPos.x == rhsPos.x) 
		{
			if (lhsPos.y > rhsPos.y)
			{
				// LHS is above RHS; must go down
				return EDirection.DOWN;
			}
			else if (lhsPos.y < rhsPos.y)
			{
				// LHS is below RHS; must go up
				return EDirection.UP;
			}
			else
			{
				print ("Movement direction broken! Survivors are in the same position.");
			}
		} 
		else if (lhsPos.y == rhsPos.y) 
		{
			if (lhsPos.x > rhsPos.x)
			{
				// LHS is right of RHS; must go left
				return EDirection.LEFT;
			}
			else if (lhsPos.x < rhsPos.x)
			{
				// LHS is left of RHS; must go right
				return EDirection.RIGHT;
			}
			else
			{
				print ("Movement direction broken! Survivors are in the same position.");
			}
		} 
		else 
		{
			print ("Movement direction broken! Survivors not adjacent.");
		}
		
		// Error case:
		return EDirection.NONE;
	}

	public void Update()
	{

	}
}
