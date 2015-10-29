using UnityEngine;
using System.Collections.Generic;

public class GridScript : MonoBehaviour 
{

    ////////////////////////////////////////////////////////////////////
    // Public values
    ////////////////////////////////////////////////////////////////////

    // Camera
    public Camera mainCamera;

    // Public config
    public int width = 40;
    public int height = 40;
    public float tileSize = 0.1f;

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

    // Use this for initialization
    void Start()
    {
        // Init camera
        Vector2 cameraPosition = GridToRenderPosition(new Vector2(width / 2, height / 2));
        mainCamera.transform.position.Set(cameraPosition.x, cameraPosition.y, 0);
        mainCamera.orthographicSize = width / 4;

        // Set the movement countdown to the initial value
        m_fMovementUpdateInterval = initialMovementTimeInterval;
        ResetMovementUpdateCountdown();

        // Populate grid with grass sprites
        m_pFloorSprites = new Object[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                //m_pFloorSprites[i, j] = Instantiate(grassPrefab, GridToRenderPosition(new Vector2(i, j)), Quaternion.identity);
            }
        }

        // Add the player to the center
		Vector2 vPlayerSpawnPosition = new Vector2 (0, 0);// spawnPlayerInCentre ? new Vector2(width / 2, height / 2) : playerStartPositionOverride;
        m_pCongaHeadSurvivor = (Transform) Instantiate(survivorPrefab, GridToRenderPosition(vPlayerSpawnPosition), Quaternion.identity);
        m_pCongaHeadSurvivor.parent = gameObject.transform;
        m_scriptCongoHead = m_pCongaHeadSurvivor.GetComponent<SurvivorScript>();
    }

    void ResetMovementUpdateCountdown()
    {
        // Reset to currnet time interval
        m_fMovementUpdateCountdown = m_fMovementUpdateInterval;
        print(m_fMovementUpdateInterval);
    }

    void SetTransformPosition(Transform i_childTransform)
    {
        GridObject gridObjectScript = i_childTransform.GetComponent<GridObject>();
    }

    public Vector2 GridToRenderPosition(Vector2 i_vGridPosition)
    {
        return new Vector2(transform.position.x, transform.position.y) + i_vGridPosition * tileSize;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_pCongaHeadSurvivor)
        {
            mainCamera.transform.position.Set(m_pCongaHeadSurvivor.transform.position.x, m_pCongaHeadSurvivor.transform.position.y, mainCamera.transform.position.z);
        }

        PollInput();

        if (m_fMovementUpdateCountdown > 0.0f)
        {
            m_fMovementUpdateCountdown -= Time.deltaTime;

            print("Is it less than? " + m_fMovementUpdateCountdown);
            if (m_fMovementUpdateCountdown <= 0.0f)
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
        m_scriptCongoHead.DoMovement();
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
