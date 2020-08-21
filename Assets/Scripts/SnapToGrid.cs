using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class SnapToGrid : MonoBehaviour
{
    [SerializeField] int gridSize = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x / gridSize) * gridSize, 0f, 
            Mathf.RoundToInt(transform.position.z / gridSize) * gridSize);
    }
}
