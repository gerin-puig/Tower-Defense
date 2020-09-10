using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    const int gridSize = 10;

    public bool isExplored = false;
    public bool isPlaceable = true;
    public Waypoint exploredFrom;

    Vector2Int gridPos;

    private void Update()
    {
        //if (isExplored)
        //{
        //    SetTopColour(Color.cyan);
        //}
    }

    private void OnMouseOver()
    {
        
    }

    private void OnMouseDown()
    {
        if (Input.GetButtonDown("Fire1")) //left click
        {
            if (isPlaceable)
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
            }
            else
            {
                Debug.Log("Cant place here");
            }
        }
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
            );
    }

    public void SetTopColour(Color colour)
    {
        MeshRenderer topMesh = transform.Find("Top").GetComponent<MeshRenderer>();
        topMesh.material.color = colour;
    }
}
