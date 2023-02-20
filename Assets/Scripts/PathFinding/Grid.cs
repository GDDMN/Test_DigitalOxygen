using System;
using UnityEngine;
using CodeMonkey.Utils;

public class Grid <TGridObject>
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int _width;
    private int _height;
    private Vector3 _originPosition;
    private float _cellSize;
    private TGridObject[,] _gridArray;
    private TextMesh[,] _debugTextArray;

    public int GetWidth => _width;
    public int GetHeight => _height;

    public float GetCellSize => _cellSize;

    public Grid(int width, int height, float cellSize, Vector3 origiPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _originPosition = origiPosition;

        _gridArray = new TGridObject[width, height];

        for (int i = 0; i < _gridArray.GetLength(0); i++)
            for (int j = 0; j < _gridArray.GetLength(1); j++)
                _gridArray[i, j] = createGridObject(this, i, j);

        //_debugTextArray = new TextMesh[width, height];

        for(int i=0;i<_gridArray.GetLength(0); i++)
        {
            for(int j=0;j<_gridArray.GetLength(1);j++)
            {
                //_debugTextArray[i, j] = UtilsClass.CreateWorldText(_gridArray[i, j].ToString(), null, GetWorldPosition(i, j) + new Vector3(cellSize, cellSize) * .5f, 1, Color.white, TextAnchor.UpperRight);
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i, j + 1), Color.black, 100f);
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i + 1, j), Color.black, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.black, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.black, 100f);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * _cellSize + _originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        y = Mathf.FloorToInt((worldPosition - _originPosition).y / _cellSize);
    }

    public void SetGridObject(int x, int y, TGridObject value)
    {
        if(x >= 0 && y >= 0 && x < _width && y < _height)
        {
            _gridArray[x, y] = value;
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
        }
    }

    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            return _gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }
}
