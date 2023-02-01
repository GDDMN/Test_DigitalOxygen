using UnityEngine;
using CodeMonkey.Utils;

public class Grid 
{
    private int _width;
    private int _height;
    private Vector3 _originPosition;
    private float _cellSize;
    private int[,] _gridArray;
    private TextMesh[,] _debugTextArray;
    public Grid(int width, int height, float cellSize, Vector3 origiPosition)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _originPosition = origiPosition;

        _gridArray = new int[width, height];
        _debugTextArray = new TextMesh[width, height];

        for(int i=0;i<_gridArray.GetLength(0); i++)
        {
            for(int j=0;j<_gridArray.GetLength(1);j++)
            {
                _debugTextArray[i, j] = UtilsClass.CreateWorldText(_gridArray[i, j].ToString(), null, GetWorldPosition(i, j) + new Vector3(cellSize, cellSize) * .5f, 5, Color.black, TextAnchor.UpperRight);
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

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        y = Mathf.FloorToInt((worldPosition - _originPosition).y / _cellSize);
    }

    public void SetValue(int x, int y, int value)
    {
        if(x >= 0 && y >= 0 && x < _width && y < _height)
        {
            _gridArray[x, y] = value;
            _debugTextArray[x, y].text = _gridArray[x, y].ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            return _gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }
}
