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
		m_pMovementComponent = GetComponent<MovementComponent> ();
	}
	
	// Update is called once per frame
	void Update () 
    {
		float speed = 0.01f;
		switch (m_pMovementComponent.GetDirection ()) {
		case EDirection.DOWN:
			animator.SetInteger("Direction", 0);
			transform.localPosition = transform.localPosition + new Vector3(0, -speed, 0);
			break;
		case EDirection.LEFT:
			animator.SetInteger("Direction", 1);
			transform.Translate(-speed, 0, 0);
			break;
		case EDirection.UP:
			animator.SetInteger("Direction", 2);
			transform.Translate(0, speed, 0);
			break;
		case EDirection.RIGHT:
			animator.SetInteger("Direction", 3);
			transform.Translate(speed, 0, 0);
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

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Survivor") {
			GameObject link = survivorBack;

			while(link != null) {

			}

			link = coll.gameObject;
		}
	}
}
