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
    GameObject survivorBack = null;

	public bool m_bIsStranded = false;

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

	public void DoMovement()
	{
		Move ();
	}
	
	public void OnHitWall()
	{
		if (this.tag == "Player")
		{
			// End the game

		}
	}

	protected void Move()
	{
		// Move our back survivor in our direction
		if (survivorBack) 
		{
			survivorBack.GetComponent<SurvivorScript>().SetDirection(DirectionFromTo(survivorBack, this.gameObject));
			survivorBack.GetComponent<SurvivorScript>().Move(); // This will recurse throughout all members of the conga
		}

		// Move us
		base.Move ();
	}

	public int GetNumSurvivors()
	{
		if (survivorBack) 
		{
			return 1 + survivorBack.GetComponent<SurvivorScript> ().GetNumSurvivors ();
		} 
		else 
		{
			return 1;
		}
	}
	
	// Update is called once per frame
	protected void Update () 
    {
        base.Update();

        // On Movement
        if (survivorBack)
        {
            
        }
        
	}

	public void AddSurvivor()
	{
		GameObject link = survivorBack;
		
		if (link)
		{
			// Recurse back
			survivorBack.GetComponent<SurvivorScript>().AddSurvivor();
		}
		else // We are the back position
		{
			// Get the position behind us
			Vector2 backPos = GetPosition ();
			
			switch (GetDirection ()) {
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
			
			survivorBack = gridObject.GetComponent<GridScript>().SpawnSurvivor(backPos).gameObject;
			survivorBack.GetComponent<SurvivorScript>().SetDirection (GetDirection ());
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (gameObject.tag == "Player") 
        {
			if (coll.gameObject.tag == "Survivor") // Player hits survivor
            {
				if (coll.gameObject.GetComponent<SurvivorScript>().m_bIsStranded)
				{
					AddSurvivor();

					// Now remove survivor (coll.gameObject.tag) from the world
					Destroy (coll.gameObject);
				}
            }
        }

        // Zombie collision
        if (coll.gameObject.tag == "Zombie")
        {
			// If zombie is facing us, we are infected. Otherwise, we kill it.
			EDirection eZombieToUs = DirectionFromTo(coll.gameObject, this.gameObject);

			if (coll.gameObject.GetComponent<ZombieScript>().GetDirection () == eZombieToUs)
			{
            	this.SetState(ESurvivorState.Infected);

				// TODO: Change to infected spritesheet
			}
			else
			{
				// Animate the attack
				animator.SetBool("Is Attacking", true);

				// Remove zombie from the world.
				Destroy (coll.gameObject);
			}
        }
	}
}













