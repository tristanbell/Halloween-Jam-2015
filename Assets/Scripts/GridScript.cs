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
    public int width = 150;
    public int height = 150;
    public float tileSize = 0.32f;

    // Timing
    public float initialMovementTimeInterval = 1.0f;
	public float minimumMovementTimeInterval = 0.1f;

    // Player spawning
    public bool spawnPlayerInCentre = true;
    public Vector2 playerStartPositionOverride; // If the player isn't set to spawn in the centre, spawn in this position instead.

    // Prefabs!
	public GameObject grassPrefab;
	public GameObject heroPrefab;
	public GameObject survivorMalePrefab;
	public GameObject survivorFemalePrefab;
	public GameObject zombieMalePrefab;
	public GameObject zombieFemalePrefab;
	
    ////////////////////////////////////////////////////////////////////
    // Private values
	////////////////////////////////////////////////////////////////////
	
	// collision map
	private Texture2D collision_texture;
	private List<Vector2> validSpawnPoints = new List<Vector2>();

    // The 2D array of grid objects
    private GameObject[,] m_pFloorSprites;
	private GameObject m_pCongaHeadSurvivor;
    private SurvivorScript m_scriptCongoHead;

	private List<GameObject> m_zombieList = new List<GameObject>();

    // Countdown to movement update
    private float m_fMovementUpdateInterval;
    private float m_fMovementUpdateCountdown;
	private bool m_bIsPaused;
	
    // Use this for initialization
    void Start()
    {
		m_bIsPaused = false;

        // Init camera
        Vector2 cameraPosition = GridToRenderPosition(new Vector2(width / 2, height / 2));
        mainCamera.transform.position.Set(cameraPosition.x, cameraPosition.y, 0);

		// create collision texture
		collision_texture = Resources.Load("collision") as Texture2D;

		// Build a list of valid positions (for spawning)
		for (int i = 0; i < collision_texture.width; i++) 
		{
			for (int j = 0; j < collision_texture.height; j++) 
			{
				if (!get_collision(i, j))
				{
					// If no collision here, add the position
					validSpawnPoints.Add(new Vector2(i, j));
				}
			}
		}

        // Populate grid with grass sprites
        m_pFloorSprites = new GameObject[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
				if(get_collision(i, j))
				{
					// TODO: Add a collision box here which will register as a wall
					//m_pFloorSprites[i, j] = (GameObject) Instantiate(grassPrefab, GridToRenderPosition(GetRandomSpawnPosition()), Quaternion.identity);
				}
            }
		}
		
		SpawnPlayer ();

//		SpawnZombieRandom ();
//		SpawnSurvivorRandom ();
		
		// Set the movement countdown to the initial value
		m_fMovementUpdateInterval = initialMovementTimeInterval;
		ResetMovementUpdateCountdown();
	}

	private void SpawnPlayer()
	{
		// Add the player to the center
		Vector2 vPlayerSpawnPosition = GetRandomSpawnPosition ();
		m_pCongaHeadSurvivor = (GameObject) Instantiate(heroPrefab, GridToRenderPosition(vPlayerSpawnPosition), Quaternion.identity);
		m_pCongaHeadSurvivor.GetComponent<SurvivorScript> ().gridObject = gameObject;
		m_pCongaHeadSurvivor.GetComponent<SurvivorScript> ().SetPosition (vPlayerSpawnPosition);
		m_pCongaHeadSurvivor.tag = "Player";
		m_scriptCongoHead = m_pCongaHeadSurvivor.GetComponent<SurvivorScript>();
	}
	
	public bool get_collision(int x, int y)
	{
		if (x < 0 || y < 0 || x >= collision_texture.width || y >= collision_texture.height) {
			return true;
		}
		Color pixel_color = collision_texture.GetPixel (x, y);
		return pixel_color.r > 0.5f;
	}

	private Vector2 GetRandomSpawnPosition(bool i_bSpawnInSight = false)
	{
		if (i_bSpawnInSight)
		{
			// Find random points until one lies in sight (within camera screen point)
			Vector2 point = validSpawnPoints[Random.Range (0, validSpawnPoints.Count)];
			Vector3 screenPoint = mainCamera.WorldToScreenPoint(point / tileSize);
			
			//while (!(screenPoint.x > 0.0F && screenPoint.x < 1.0F && screenPoint.y > 0.0F && screenPoint.y < 1.0F))
			while (!IsWithinSight (point))
			{
				point = validSpawnPoints[Random.Range (0, validSpawnPoints.Count)];
			}
			
			return point;
		}
		else
		{
			return validSpawnPoints[Random.Range (0, validSpawnPoints.Count)];
		}
	}

	private bool IsWithinSight(Vector2 i_vPoint)
	{
		int iRangeRadius = 8;
		Vector2 vPlayerGridPos = m_scriptCongoHead.GetPosition ();

		return i_vPoint.x > vPlayerGridPos.x - iRangeRadius
			&& i_vPoint.x < vPlayerGridPos.x + iRangeRadius
			&& i_vPoint.y > vPlayerGridPos.y - iRangeRadius
			&& i_vPoint.y < vPlayerGridPos.y + iRangeRadius;

	}
	
	public GameObject SpawnSurvivorRandom()
	{
		Vector2 gridPos = GetRandomSpawnPosition(true);
		return SpawnSurvivor(gridPos);
	}

	public GameObject SpawnSurvivor(Vector2 gridPos)
	{
		GameObject survivor = (GameObject) Instantiate(Random.Range(0, 2) == 0 ? survivorMalePrefab : survivorFemalePrefab, GridToRenderPosition(gridPos), Quaternion.identity);
		survivor.GetComponent<SurvivorScript> ().SetPosition (gridPos);
		survivor.GetComponent<SurvivorScript> ().gridObject = gameObject;
		survivor.tag = "Survivor";

		return survivor;
	}
	
	void SpawnZombie(Vector2 gridPos)
	{
		GameObject newZombie = (GameObject) Instantiate(Random.Range (0, 2) == 0 ? zombieMalePrefab : zombieFemalePrefab, GridToRenderPosition(gridPos), Quaternion.identity);
		
		newZombie.GetComponent<ZombieScript> ().SetPosition (gridPos);
		newZombie.GetComponent<ZombieScript> ().gridObject = gameObject;
		m_zombieList.Add(newZombie);
	}
	
	void SpawnZombieRandom()
	{
		Vector2 gridPos = GetRandomSpawnPosition(true);
		SpawnZombie(gridPos);
	}
	
	void ResetMovementUpdateCountdown()
    {
		// Set the interval
		m_fMovementUpdateInterval = Mathf.Max(minimumMovementTimeInterval, initialMovementTimeInterval / (m_scriptCongoHead.GetNumSurvivors () * 0.5f));

        // Reset to currnet time interval
        m_fMovementUpdateCountdown = m_fMovementUpdateInterval;

		// Set animation speeds
		m_scriptCongoHead.SetWalkingSpeed (m_fMovementUpdateInterval);
    }

    public Vector2 GridToRenderPosition(Vector2 i_vGridPosition)
    {
        Vector2 result = new Vector2(transform.position.x, transform.position.y) + i_vGridPosition * tileSize;
		return result;
    }

    // Update is called once per frame
    void Update()
	{
		if (!m_bIsPaused) {
			//print ("Player GO pos: " + m_pCongaHeadSurvivor.transform.position);
			//print ("Player script pos in grid: " + m_scriptCongoHead.m_vGridPosition);

			PollInput ();

			if (m_fMovementUpdateCountdown > 0.0f) {
				m_fMovementUpdateCountdown -= Time.deltaTime;

				if (m_fMovementUpdateCountdown <= 0.0f) {
					OnMovementUpdate ();
					ResetMovementUpdateCountdown ();
				}
			}

			// Focus camera on the head survivor
			if (m_pCongaHeadSurvivor) {
				Vector3 cameraTranslation = m_pCongaHeadSurvivor.transform.position - mainCamera.transform.position;
				if (cameraTranslation.x != 0 || cameraTranslation.y != 0) {
					mainCamera.transform.Translate (cameraTranslation.x, cameraTranslation.y, 0);
				}
			}
		}
    }

    void OnMovementUpdate()
    {
        // Move the congo head, this will fall through everyone in the congo
        m_scriptCongoHead.DoMovement();

        for (int i = 0; i < m_zombieList.Count; i++)
        {
            m_zombieList.ToArray()[i].GetComponent<ZombieScript>().DoMovement();
        }
    }

    void PollInput()
    {
        if (Input.GetKey("w"))
        {
            m_scriptCongoHead.SetDirection(EDirection.UP);
        }

        if (Input.GetKey("a"))
        {
            m_scriptCongoHead.SetDirection(EDirection.LEFT);
        }

        if (Input.GetKey("s"))
        {
            m_scriptCongoHead.SetDirection(EDirection.DOWN);
        }

        if (Input.GetKey("d"))
        {
            m_scriptCongoHead.SetDirection(EDirection.RIGHT);
		}

		// Debug controls
		if (Input.GetKey ("q")) 
		{
			// Attack animation
			m_scriptCongoHead.animator.SetBool("Is Attacking", true);
		}
		if (Input.GetKey ("e")) 
		{
			// Attack animation
			m_scriptCongoHead.animator.SetBool("Is Attacking", false);
		}
		
		if (Input.GetKey("z"))
		{
			// Spawn Zombie
			SpawnZombieRandom();
		}
		
		if (Input.GetKey("l"))
		{
			// Spawn Survivor
			SpawnSurvivorRandom();
		}
		
		if (Input.GetKey("x"))
		{
			// Add a survivor to the conga
			m_scriptCongoHead.AddSurvivor();
		}
		
		if (Input.GetKey("c"))
		{
			// Infect a random survivor in the conga line
		}

		if (Input.GetKey ("v"))
		{
			// Game over - simulate death.
			m_scriptCongoHead.OnHitWall();
		}
    }
}
