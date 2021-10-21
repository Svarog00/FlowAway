using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] float cellSize;
    [SerializeField] Vector3 origin;


    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        Pathfinding pathfinding = new Pathfinding(width, height, cellSize, origin);
    }

    private void Update()
    {
        
    }
}
