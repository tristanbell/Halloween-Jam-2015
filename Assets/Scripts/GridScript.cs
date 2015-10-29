using UnityEngine;
using System.Collections.Generic;

// Configuration numbers for the grid
class GridConfig
{
    
}

public class GridScript : MonoBehaviour 
{

    ////////////////////////////////////////////////////////////////////
    // Public values
    ////////////////////////////////////////////////////////////////////

    // Public config
    public int width = 40;
    public int height = 40;
    public int tileSize = 32;

    // Timing
    public float initialMovementTimeInterval = 1.0f;

    // Player spawning
    public bool spawnPlayerInCentre = true;
    public Vector2 playerStartPositionOverride; // If the player isn't set to spawn in the centre, spawn in this position instead.

    // Prefabs!
    public Transform grassPrefab;
    public Transform survivorPrefab;



    ////////////////////////////////////////////////////////////////////
    // Private values
    ////////////////////////////////////////////////////////////////////

    // The 2D array of grid objects
    private Object[,] m_pFloorSprites;
    private Transform m_pCongaHeadSurvivor;
    private SurvivorScript m_scriptCongoHead;

    // Countdown to movement update
    private float m_fMovementUpdateInterval;
    private float m_fMovementUpdateCountdown;

    // TODO Add an "Add" function, which will take a grid position and add instantiate a prefab at that position

    public Transform brick;

    void foo()
    {
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                Instantiate(brick, new Vector2(x, y), Quaternion.identity);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        // Set the movement countdown to the initial value
        m_fMovementUpdateInterval = initialMovementTimeInterval;
        ResetMovementUpdateCountdown();

        // Populate grid with grass sprites
        m_pFloorSprites = new Object[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                m_pFloorSprites[i,j] = Instantiate(grassPrefab, GridToRenderPosition(new Vector2(i, j)), Quaternion.identity);
            }
        }

        // Add the player to the center
        Vector2 vPlayerSpawnPosition = spawnPlayerInCentre ? new Vector2(width / 2, height / 2) : playerStartPositionOverride;
        m_pCongaHeadSurvivor = (Transform) Instantiate(survivorPrefab, GridToRenderPosition(vPlayerSpawnPosition), Quaternion.identity);
        m_scriptCongoHead = m_pCongaHeadSurvivor.GetComponent<SurvivorScript>();
    }

    void ResetMovementUpdateCountdown()
    {
        // Reset to currnet time interval
        m_fMovementUpdateCountdown = m_fMovementUpdateInterval;
    }

    void SetTransformPosition(Transform i_childTransform)
    {
        GridObject gridObjectScript = i_childTransform.GetComponent<GridObject>();
    }

    Vector2 GridToRenderPosition(Vector2 i_vGridPosition)
    {
        return i_vGridPosition * tileSize;
    }
	
	// Update is called once per frame
	void Update () 
    {
        PollInput();

        if (m_fMovementUpdateCountdown > 0)
        {
            m_fMovementUpdateCountdown -= Time.deltaTime;

            if (m_fMovementUpdateCountdown <= 0)
            {
                OnMovementUpdate();
                ResetMovementUpdateCountdown();
            }
        }

        //// Loop through all grid objects
        //int children = transform.childCount;
        //for (int i = 0; i < children; ++i)
        //{
        //    // Set their render position based on their grid position.
        //    Transform childTransform = transform.GetChild(i);
        //    SetTransformPosition(childTransform);
        //}
    }

    void OnMovementUpdate()
    {
        // Move the congo head, this will fall through everyone in the congo
        m_scriptCongoHead.GetMovementComponent().DoMovement();
    }

    void PollInput()
    {
        if (Input.GetKey("w"))
        {
            m_scriptCongoHead.GetMovementComponent().SetDirection(EDirection.UP);
        }

        if (Input.GetKey("a"))
        {
            m_scriptCongoHead.GetMovementComponent().SetDirection(EDirection.LEFT);
        }

        if (Input.GetKey("s"))
        {
            m_scriptCongoHead.GetMovementComponent().SetDirection(EDirection.DOWN);
        }

        if (Input.GetKey("d"))
        {
            m_scriptCongoHead.GetMovementComponent().SetDirection(EDirection.RIGHT);
        }
    }
}
