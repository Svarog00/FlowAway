using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    private GridMesh grid;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] int cellSize;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        grid = new GridMesh(width, height, cellSize);

        player = FindObjectOfType<Player_Movement>().gameObject;


    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            grid.SetValue(UtilitiesClass.GetWorldMousePosition(), Random.Range(1, 101));
        }
    }
}
