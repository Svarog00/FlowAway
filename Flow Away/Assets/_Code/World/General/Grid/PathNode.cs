using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PathNode
{
    public readonly int X;
    public readonly int Y;

    public bool IsWalkable;
    public int GCost;
    public int HCost;
    public int FCost;

    public PathNode PrevNode;

    private const string ObstacleLayerMaskName = "Obstacles";

    private GridMesh<PathNode> _grid;

    public PathNode(GridMesh<PathNode> grid, int x, int y)
    {
        _grid = grid;
        X = x;
        Y = y;
        IsWalkable = !Physics2D.OverlapCircle(_grid.GetWorldPosition(X, Y) + new Vector3(_grid.CellSize / 2f, _grid.CellSize / 2f), 
            _grid.CellSize * 0.4f, 
            LayerMask.GetMask(ObstacleLayerMaskName));
    }
    public void CalculateFCost()
    {
        FCost = GCost + HCost;
    }

    public override string ToString()
    {
        return $"{_grid.GetWorldPosition(X, Y) + new Vector3(_grid.CellSize / 2f, _grid.CellSize / 2f):f2}";
    }
}
