using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GridMesh<TGridObject>
{
    public EventHandler<OnValueChangedEventArgs> OnValueChanged;
    public class OnValueChangedEventArgs
    {
        public int x;
        public int y;
    }

    private TGridObject[,] _gridArray;

    [SerializeField] private bool _showDebug;
    private int _width;
    private int _height;
    private float _cellSize;
    private Vector3 _originPosition;

    private TextMesh[,] _debugTextMeshes;

    public int Width
    {
        get => _width;
    }

    public int Height
    {
        get => _height;
    }

    public float CellSize
    {
        get => _cellSize;
    }
    
    public GridMesh(int width, int height, float cellSize, Vector3 originPosition, Func<GridMesh<TGridObject>, int, int, TGridObject> createGridObject)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _originPosition = originPosition;

        _gridArray = new TGridObject[width, height];

        for (int i = 0; i < _gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < _gridArray.GetLength(1); j++)
            {
                _gridArray[i, j] = createGridObject(this, i, j);
            }
        }

        _showDebug = true;
        if (_showDebug)
        {
            _debugTextMeshes = new TextMesh[width, height];
            DrawMesh();
        }

    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * _cellSize + _originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        y = Mathf.FloorToInt((worldPosition - _originPosition).y / _cellSize);
    }

    private void DrawMesh()
    {
        for (int i = 0; i < _gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < _gridArray.GetLength(1); j++)
            {
                _debugTextMeshes[i,j] = UtilitiesClass.CreateWorldText(_gridArray[i, j]?.ToString(), null, GetWorldPosition(i, j) + new Vector3(_cellSize, _cellSize) * .5f, 5, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i, j + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i + 1, j), Color.white, 100f);
            }
            Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.white, 100f);

            OnValueChanged += (object sender, OnValueChangedEventArgs eventArgs) =>
            {
                _debugTextMeshes[eventArgs.x, eventArgs.y].text = _gridArray[eventArgs.x, eventArgs.y]?.ToString();
            };
        }
    }

    public void SetGridObject(TGridObject value, int x, int y)
    {
        if(x >= 0 && y >= 0 && x < _width && y < _height)
        {
            _gridArray[x, y] = value;
            _debugTextMeshes[x, y].text = _gridArray[x, y]?.ToString();
            OnValueChanged?.Invoke(this, new OnValueChangedEventArgs { x = x, y = y });
        }  
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(value, x, y);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            return _gridArray[x, y];
        }
        else return default(TGridObject);
    }

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }
}