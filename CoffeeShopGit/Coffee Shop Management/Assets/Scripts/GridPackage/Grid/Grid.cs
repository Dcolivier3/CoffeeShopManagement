﻿using System.Collections;
using System; 
using System.Collections.Generic;
using Jessy.CodeMonkey.Utils;
using UnityEngine;
 // namespace of utils class. 

// code monkey grid genration https://www.youtube.com/watch?v=waEsGu--9P8&list=PLzDRvYVwl53uhO8yhqxcyjDImRjO9W722&index=1
// code monkey heat map https://www.youtube.com/watch?v=mZzZXfySeFQ&list=PLzDRvYVwl53uhO8yhqxcyjDImRjO9W722&index=4
// code monkey grid generic gameobjects https://www.youtube.com/watch?v=8jrAWtI8RXg&list=PLzDRvYVwl53uhO8yhqxcyjDImRjO9W722&index=2
public class Grid<TGridObject> { //grid can work with any generic objects

    public const int HEAT_MAP_MAX_VALUE = 100;
    public const int HEAT_MAP_MIN_VALUE = 0;

     public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs {
        public int x;
        public int y;
    }

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        bool showDebug = true;
        if (showDebug) {
            TextMesh[,] debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++) {
                for (int y = 0; y < gridArray.GetLength(1); y++) {
                    debugTextArray[x, y] = GridUtils.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 30, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

            OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) => {
                debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y].ToString();
            };
        }
    }

    public int GetWidth() {
        return width;
    }

    public int GetHeight() {
        return height;
    }

    public float GetCellSize() {
        return cellSize;
    }

    public Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y) {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public void SetValue(int x, int y, TGridObject value) {
        if (x >= 0 && y >= 0 && x < width && y < height) {
            gridArray[x, y] = value; 
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }
    }

    public void SetValue(Vector3 worldPosition, TGridObject value) {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    // public void AddValue(int x, int y, int value) {
    //     SetValue(x, y, GetValue(x, y) + value);
    // }

    public TGridObject GetValue(int x, int y) {
        if (x >= 0 && y >= 0 && x < width && y < height) {
            return gridArray[x, y];
        } else {
            return default(TGridObject);
        }
    }

    public TGridObject GetValue(Vector3 worldPosition) {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    // public void AddValue(Vector3 worldPosition, int value, int fullValueRange, int totalRange) { //spefic for heat map
    //     int lowerValueAmount = Mathf.RoundToInt((float)value / (totalRange - fullValueRange));

    //     GetXY(worldPosition, out int originX, out int originY);
    //     for (int x = 0; x < totalRange; x++) {
    //         for (int y = 0; y < totalRange - x; y++) {
    //             int radius = x + y;
    //             int addValueAmount = value;
    //             if (radius >= fullValueRange) {
    //                 addValueAmount -= lowerValueAmount * (radius - fullValueRange);
    //             }

    //             AddValue(originX + x, originY + y, addValueAmount);

    //             if (x != 0) {
    //                 AddValue(originX - x, originY + y, addValueAmount); // this section adjusts the radius of grid positions that gets effected when you click 
    //             }
    //             if (y != 0) {
    //                 AddValue(originX + x, originY - y, addValueAmount);
    //                 if (x != 0) {
    //                     AddValue(originX - x, originY - y, addValueAmount);
    //                 }
    //             }
    //         }
    //     }
    // }

}
