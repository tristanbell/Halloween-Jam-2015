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
	
	// Update is called once per frame
	public void Update () 
    {
        base.Update();

        // On Movement
        if (survivorBack)
        {
            
        }
        
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (gameObject.tag == "Player") 
        {
			if (coll.gameObject.tag == "Survivor") // Player hits survivor
            {
                GameObject link = survivorBack;

                if (link == null)
                {
					// We have no survivor behind us; add the collision object to the line
                    AddSurvivor(coll.gameObject);
                }
                else
                {
					// Search for the final link in the line
					while (link.GetComponent<SurvivorScript>().survivorBack != null)
                    {
                        link = link.GetComponent<SurvivorScript>().survivorBack;
                    }

					// Add the collision object to the back of the final survivor
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

		survivorBack.GetComponent<SurvivorScript>().SetPosition (backPos);
		survivorBack.GetComponent<SurvivorScript>().SetDirection (GetDirection ());
	}
}













