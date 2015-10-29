using UnityEngine;
using System.Collections;

public class GridObject : MonoBehaviour
{
    // Grid position is set by the subclass; determines the render/world position relative to the grid
    EDirection m_eMovementDirection = EDirection.UP;

    MovementComponent m_pMovementComponent = new MovementComponent(this);

	// Use this for initialization
	void Start () 
    {
	    
	}

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    public Vector2 GetGridPosition()
    {
        return m_vGridPosition;
    }
}
