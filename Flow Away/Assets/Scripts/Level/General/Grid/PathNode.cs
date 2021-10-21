using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PathNode
{
    private const string OBSTACLE_LAYER_MASK = "Obstacles";

    private int _x;
    private int _y;
    private GridMesh<PathNode> _grid;

    private bool _isWalkable;
    private int _gCost;
    private int _hCost;
    private int _fCost;

    private PathNode _prevNode;

    public int X
    {
        get => _x;
    }

    public int Y
    {
        get => _y;
    }

    public bool IsWalkable
    {
        get => _isWalkable;
    }

    public int GCost
    {
        get => _gCost;
        set => _gCost = value;
    }

    public int HCost
    {
        get => _hCost;
        set => _hCost = value;
    }

    public int FCost
    {
        get => _fCost;
        set => _fCost = value;
    }

    public PathNode PrevNode
    {
        get => _prevNode;
        set => _prevNode = value;
    }

    public PathNode(GridMesh<PathNode> grid, int x, int y)
    {
        _grid = grid;
        _x = x;
        _y = y;
        _isWalkable = !Physics2D.OverlapCircle(_grid.GetWorldPosition(x, y) + Vector3.one * _grid.CellSize, _grid.CellSize/2, LayerMask.GetMask(OBSTACLE_LAYER_MASK));
    }

    public void CalculateFCost()
    {
        _fCost = _gCost + _hCost;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(_isWalkable);
        return stringBuilder.ToString();
    }
}
