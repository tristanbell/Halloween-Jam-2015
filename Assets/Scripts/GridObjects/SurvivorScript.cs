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
    public GameObject gridObject;

    // The state of the survivor
    ESurvivorState m_eState = ESurvivorState.Stranded;

    // A conga line of survivors have a front and back survivor pointer
    GameObject survivorFront = null;
    GameObject survivorBack = null;

	protected Animator animator;

    public void SetState(ESurvivorState i_eNewState)
    {
        // If we're infected
        if (i_eNewState == ESurvivorState.Infected)
        {
            // now infected: change sprite

            // If this is the player
            if (gameObject.tag == "Player")
            {
                print("Game over!");
            }
            else
            {
                print("Survivor infected");
            }
        }

        // Change state
        m_eState = i_eNewState;

        
    }

    // Use this for initialization
	void Start () 
    {
        base.Start();

		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	public void Update () 
    {
        //base.Update();

        // Convert our grid position to the real in-game position
        GridScript parentGridScript = gridObject.GetComponent<GridScript>();

		Vector2 gridPos = m_pMovementComponent.GetPosition ();
//		if (gameObject.tag == "Player")
//			print (parentGridScript.GridToRenderPosition(gridPos));

        Vector2 newPos = parentGridScript.GridToRenderPosition(gridPos);
        transform.localPosition = new Vector3(newPos.x, newPos.y, 0);

        // Set the animation
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
        if (survivorFront)
        {
            //m_vNextPosition = survivorFront.GetGridPosition();
        }
        if (survivorBack)
        {
            
        }
        
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (gameObject.tag == "Player") 
        {
            if (coll.gameObject.tag == "Survivor")
            {
                GameObject link = survivorBack;

                if (link == null)
                {
                    AddSurvivor(coll.gameObject);
                }
                else
                {
                    while (true)
                    {
                        if (link.GetComponent<SurvivorScript>().survivorBack == null)
                        {
                            break;
                        }

                        link = link.GetComponent<SurvivorScript>().survivorBack;
                    }

                    link.GetComponent<SurvivorScript>().AddSurvivor(coll.gameObject);
                }
            }
		}

        // Zombie collision
        if (coll.gameObject.tag == "Zombie")
        {
            SetState(ESurvivorState.Infected);
        }
	}

	void AddSurvivor(GameObject survivor) {
		survivorBack = survivor;

		Vector2 backPos = m_pMovementComponent.GetPosition ();

		switch (m_pMovementComponent.GetDirection ()) {
		case EDirection.DOWN:
			backPos.y++;
			break;
		case EDirection.LEFT:
			backPos.x++;
			break;
		case EDirection.UP:
			backPos.y--;
			break;
		case EDirection.RIGHT:
			backPos.x--;
			break;
		}

		survivorBack.GetComponent<SurvivorScript>().m_pMovementComponent.SetPosition (new Vector2(0, 0));
		survivorBack.GetComponent<SurvivorScript>().m_pMovementComponent.SetDirection (m_pMovementComponent.GetDirection ());
	}
}













