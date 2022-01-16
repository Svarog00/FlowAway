using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PathNode
{
    private const string OBSTACLE_LAYER_MASK = "Obstacles";

    public readonly int X;
    public readonly int Y;
    private GridMesh<PathNode> _grid;

    private bool _isWalkable;
    private int _gCost;
    private int _hCost;
    private int _fCost;

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

    public PathNode PrevNode { get; set;  }

    public PathNode(GridMesh<PathNode> grid, int x, int y)
    {
        _grid = grid;
        X = x;
        Y = y;
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
