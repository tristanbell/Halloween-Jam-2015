using UnityEngine;
using System.Collections.Generic;

// Configuration numbers for the grid
class GridConfig
{
    public static const bool isSquare = false;
    public static const int width = 40;
    public static const int height = isSquare ? width : 40;
    public static const int tileSize = 32;
}

public class GridScript : MonoBehaviour 
{
    // The 2D array of grid objects
    GridObject[,] m_aObjects = new GridObject[GridConfig.width, GridConfig.height];

    // Use this for initialization
    void Start()
    {

    }

    void AddObject(GridObject i_pNewObject)
    {
        m_pAllObjects.Add(i_pNewObject);
        SetTransformPosition(i_pNewObject);
    }

    void SetTransformPosition(Transform i_childTransform)
    {
        GridObject gridObjectScript = i_childTransform.GetComponent<GridObject>();
        i_childTransform.transform.position = GridToRenderPosition(gridObjectScript.GetGridPosition());
    }

    Vector2 GridToRenderPosition(Vector2 i_vGridPosition)
    {
        return i_vGridPosition * GridConfig.tileSize;
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
