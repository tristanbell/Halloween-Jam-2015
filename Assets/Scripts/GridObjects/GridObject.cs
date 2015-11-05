using UnityEngine;
using System.Collections;

public class GridObject : MonoBehaviour
{
<<<<<<< HEAD
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
	
	private void Move ()
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
		
	protected void Update()
	{

	}
=======
    protected MovementComponent m_pMovementComponent;

	// Use this for initialization
	protected void Start()
    {
        m_pMovementComponent = GetComponent<MovementComponent>();
	}

    public void DoMovement()
    {
        if (m_pMovementComponent == null)
        {
            m_pMovementComponent = gameObject.AddComponent<MovementComponent>();
        }
        m_pMovementComponent.DoMovement();
    }

    protected void Update()
    {
        //// Convert our grid position to the real in-game position
        //GridScript parentGridScript = gameObject.GetComponentInParent<GridScript>();

        //Vector2 newPos = parentGridScript.GridToRenderPosition(m_pMovementComponent.GetPosition());
        //transform.localPosition.Set(newPos.x, newPos.y, 0);
    }

    public MovementComponent GetMovementComponent()
    {
        return m_pMovementComponent;
    }
>>>>>>> cdec89b6e60b3124ad664475807ad300f8b72a37
}
