using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridView : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool _showMesh;

    [Header("Mesh parametres")]
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _cellSize;
    [SerializeField] private Vector3 _origin;

    private GridMesh<PathNode> _gridMesh;

    // Start is called before the first frame update
    void Start()
    {
        _gridMesh = new GridMesh<PathNode>(_width, _height, _cellSize, _origin, //Width, height, cell size and start point
            (GridMesh<PathNode> grid, int x, int y) => new PathNode(grid, x, y),
            _showMesh);

        Pathfinding pathfinding = new Pathfinding(_gridMesh);
    }
}
