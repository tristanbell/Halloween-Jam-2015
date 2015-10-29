using UnityEngine;
using System.Collections;

public enum ESurvivorState
{
    Stranded,
    Conga,
    Infected
}

public class SurvivorScript : GridObject 
{
    // The state of the survivor
    ESurvivorState m_eState = ESurvivorState.Stranded;

    // A conga line of survivors have a front and back survivor pointer
    GameObject survivorFront = null;
    GameObject survivorBack = null;

	protected Animator animator;

    public void SetState(ESurvivorState i_eNewState)
    {
        m_eState = i_eNewState;
    }

    // Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
    {
		switch (m_pMovementComponent.GetDirection ()) {
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

        // On Movement
        m_pMovementComponent.DoMovement();
        if (survivorFront)
        {
            //m_vNextPosition = survivorFront.GetGridPosition();
        }
        if (survivorBack)
        {
            
        }
        
	}
}
