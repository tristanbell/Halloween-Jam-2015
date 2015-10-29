using UnityEngine;
using System.Collections;

public class GridObject : MonoBehaviour
{
    protected MovementComponent m_pMovementComponent;

	// Use this for initialization
	protected void Start()
    {
        m_pMovementComponent = gameObject.AddComponent<MovementComponent>();
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
}
