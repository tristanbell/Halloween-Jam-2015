using UnityEngine;
using System.Collections;

public class ZombieScript : GridObject {

    // The state of the survivor
    ESurvivorState m_eState = ESurvivorState.Stranded;

    // A conga line of survivors have a front and back survivor pointer
    GameObject survivorFront = null;
    GameObject survivorBack = null;

    public void SetState(ESurvivorState i_eNewState)
    {
        m_eState = i_eNewState;
    }

    public void DoMovement()
    {
        base.DoMovement();

        // Think of a random direction to move in
        SetDirection(RandomDirection());
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
		base.Start ();

		SetDirection (RandomDirection ());
    }
}
