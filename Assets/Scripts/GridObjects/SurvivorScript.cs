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

    public void SetState(ESurvivorState i_eNewState)
    {
        m_eState = i_eNewState;
    }
	
	// Update is called once per frame
	public void Update () 
    {
        base.Update();

        // On Movement
        if (survivorFront)
        {
            //m_vNextPosition = survivorFront.GetGridPosition();
        }
        if (survivorBack)
        {
            
        }
        
	}
}
