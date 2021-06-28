using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    private GridMesh grid;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] float cellSize;


    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        grid = new GridMesh(width, height, cellSize);
        
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            grid.SetValue(UtilitiesClass.GetWorldMousePosition(), Random.Range(1, 101));
        }
        grid.SetValue(player.position, Random.Range(1, 101));
    }
}
