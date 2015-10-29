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
    GameObject survivorBack = null;

	protected Animator animator;

    public void SetState(ESurvivorState i_eNewState)
    {
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
        if (survivorBack)
        {
            
        }
        
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (gameObject.tag == "Player" && coll.gameObject.tag == "Survivor") {
			GameObject link = survivorBack;
			
			if (link == null) {
				//AddSurvivor(coll.gameObject);
			}
			else {
				while (true) {
					if (link.GetComponent<SurvivorScript>().survivorBack == null) {
						break;
					}

					link = link.GetComponent<SurvivorScript>().survivorBack;
				}

				//link.GetComponent<SurvivorScript>().AddSurvivor(coll.gameObject);
			}
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

		survivorBack.GetComponent<SurvivorScript>().m_pMovementComponent.SetPosition (backPos);
		survivorBack.GetComponent<SurvivorScript>().m_pMovementComponent.SetDirection (m_pMovementComponent.GetDirection ());
	}
}













