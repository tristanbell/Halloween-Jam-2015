using UnityEngine;
using System.Collections;

public class ZombieScript : GridObject {
	
	public GameObject gridObject;

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

    public void DoMovement()
    {
        base.DoMovement();

        // Think of a random direction to move in
        GetMovementComponent().SetDirection(RandomDirection());
    }

    EDirection RandomDirection()
    {
        switch (Random.Range(0, 3))
        {
            case 0: return EDirection.UP;
            case 1: return EDirection.DOWN;
            case 2: return EDirection.LEFT;
            case 3: return EDirection.RIGHT;
        }
        return EDirection.NONE;
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
        if (m_pMovementComponent == null)
        {
            m_pMovementComponent = gameObject.AddComponent<MovementComponent>();
        }

		m_pMovementComponent.SetDirection (RandomDirection ());

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
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
        //switch (m_pMovementComponent.GetDirection())
        //{
        //    case EDirection.DOWN:
        //        animator.SetInteger("Direction", 0);
        //        break;
        //    case EDirection.LEFT:
        //        animator.SetInteger("Direction", 1);
        //        break;
        //    case EDirection.UP:
        //        animator.SetInteger("Direction", 2);
        //        break;
        //    case EDirection.RIGHT:
        //        animator.SetInteger("Direction", 3);
        //        break;
        //}
    }
}
