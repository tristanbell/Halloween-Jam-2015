using UnityEngine;
using System.Collections.Generic;

// Configuration numbers for the grid
class GridConfig
{
    
}

public class GridScript : MonoBehaviour 
{
    // Public config
    public int width = 40;
    public int height = 40;
    public int tileSize = 32;

    public Transform grassPrefab;

    // The 2D array of grid objects
    private Object[,] m_floorSprites;

    // Use this for initialization
    void Start()
    {
        // Populate grid with grass sprites
        m_floorSprites = new Object[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                m_floorSprites[i,j] = Instantiate(grassPrefab, GridToRenderPosition(new Vector2(i, j)), Quaternion.identity);
            }
        }
    }

    void SetTransformPosition(Transform i_childTransform)
    {
        GridObject gridObjectScript = i_childTransform.GetComponent<GridObject>();
        //i_childTransform.transform.position = GridToRenderPosition(gridObjectScript.GetGridPosition());
    }

    Vector2 GridToRenderPosition(Vector2 i_vGridPosition)
    {
        return i_vGridPosition * tileSize;
    }
	
	// Update is called once per frame
	void Update () 
    {
	    // Loop through all grid objects
        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            // Set their render position based on their grid position.
            Transform childTransform = transform.GetChild(i);
            SetTransformPosition(childTransform);
        }
    }
}
