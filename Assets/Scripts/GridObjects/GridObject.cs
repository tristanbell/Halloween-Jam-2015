using UnityEngine;
using System.Collections;

public class GridObject : MonoBehaviour
{
    protected MovementComponent m_pMovementComponent;

	// Use this for initialization
	void Start () 
    {
		m_pMovementComponent = GetComponent<MovementComponent> ();
	}

    public MovementComponent GetMovementComponent()
    {
        return m_pMovementComponent;
    }
	
	// Update is called once per frame
	void Update () 
    {
	}
}
