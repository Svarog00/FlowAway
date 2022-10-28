using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public static Pathfinding Instance
    {
        get;
        private set;
    }

    private GridMesh<PathNode> _grid;

    private PriorityQueue<PathNode> _openList;
    private List<PathNode> _closedList;
    
    public Pathfinding(GridMesh<PathNode> gridMesh)
    {
        Instance = this;
        _grid = gridMesh;
    }

    public Pathfinding(int width, int height, float cellSize, Vector3 origin)
    {
        Instance = this;

        _grid = new GridMesh<PathNode>(width, height, cellSize, origin, //Width, height, cell size and start point
            (GridMesh<PathNode> grid, int x, int y) => new PathNode(grid, x, y)); //Constructor func for TObject in gridMesh
    }

    public List<Vector3> FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        int startX, startY;
        int endX, endY;
        _grid.GetXY(startPosition, out startX, out startY);
        _grid.GetXY(targetPosition, out endX, out endY);

        List<PathNode> pathNodes = FindPath(startX, startY, endX, endY);
        if(pathNodes != null)
        {
            List<Vector3> path = new List<Vector3>();
            foreach(PathNode pathNode in pathNodes)
            {
                path.Add(new Vector3(pathNode.X, pathNode.Y) * _grid.CellSize + new Vector3(1, 1, 0) * _grid.CellSize * .5f);
            }
            path.Reverse();
            return path;
        }
        else
        {
            return null;
        }
    }

    //Algorithm itself
    private List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = _grid.GetGridObject(startX, startY);
        PathNode endNode = _grid.GetGridObject(endX, endY);

        _openList = new PriorityQueue<PathNode>();
        _closedList = new List<PathNode>();

        for(int x = 0; x < _grid.Width; x++)
        {
            for(int y = 0; y < _grid.Height; y++)
            {
                PathNode pathNode = _grid.GetGridObject(x, y);
                pathNode.GCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.PrevNode = null;
            }
        }

        startNode.GCost = 0;
        startNode.HCost = CalculateDistance(startNode, endNode);
        startNode.CalculateFCost();

        _openList.Enqueue(startNode, 0);

        while(_openList.Count > 0)
        {
            PathNode curNode = _openList.Dequeue();

            if(curNode == endNode)
            {
                return ReconstructPath(endNode);
            }

            _closedList.Add(curNode);

            foreach(PathNode neighbour in GetNeighbourList(curNode))
            {
                if(_closedList.Contains(neighbour))
                {
                    continue;
                }

                if(!neighbour.IsWalkable)
                {
                    _closedList.Add(neighbour);
                    continue;
                }

                int tentativeGCost = curNode.GCost + CalculateDistance(curNode, neighbour);
                if(tentativeGCost < neighbour.GCost)
                {
                    neighbour.PrevNode = curNode;
                    neighbour.GCost = tentativeGCost;
                    neighbour.HCost = CalculateDistance(neighbour, endNode);
                    neighbour.CalculateFCost();

                    _openList.Enqueue(neighbour, neighbour.FCost);
                }
            }
        }

        return null;
    }

    private List<PathNode> GetNeighbourList(PathNode curNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if(curNode.X - 1 >= 0)
        {
            neighbourList.Add(GetNode(curNode.X - 1, curNode.Y));

            if (curNode.Y - 1 >= 0)
                neighbourList.Add(GetNode(curNode.X - 1, curNode.Y - 1));

            if (curNode.Y + 1 < _grid.Height)
                neighbourList.Add(GetNode(curNode.X - 1, curNode.Y + 1));
        }

        if (curNode.X + 1 < _grid.Width)
        {
            neighbourList.Add(GetNode(curNode.X + 1, curNode.Y));

            if (curNode.Y - 1 >= 0)
                neighbourList.Add(GetNode(curNode.X + 1, curNode.Y - 1));

            if (curNode.Y + 1 < _grid.Height)
                neighbourList.Add(GetNode(curNode.X + 1, curNode.Y + 1));
        }

        if (curNode.Y - 1 >= 0)
            neighbourList.Add(GetNode(curNode.X, curNode.Y - 1));

        if (curNode.Y + 1 < _grid.Height)
            neighbourList.Add(GetNode(curNode.X, curNode.Y + 1));

        return neighbourList;
    }

    private int CalculateDistance(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.X - b.X);
        int yDistance = Mathf.Abs(a.Y - b.Y);
        int remain = Mathf.Abs(xDistance - yDistance);

        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remain;
    }

    private PathNode GetNode(int x, int y)
    {
        return _grid.GetGridObject(x, y);
    }

    private List<PathNode> ReconstructPath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode curNode = endNode;
        while(curNode.PrevNode != null)
        {
            path.Add(curNode.PrevNode);
            curNode = curNode.PrevNode;
        }

        return path;
    }
}
