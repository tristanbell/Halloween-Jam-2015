using UnityEngine;
using System.Collections;

public class GridObject : MonoBehaviour
{
    protected MovementComponent m_pMovementComponent = new MovementComponent();

	// Use this for initialization
	void Start () 
    {
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
