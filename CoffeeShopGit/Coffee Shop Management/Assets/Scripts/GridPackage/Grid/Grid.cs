using System.Collections;
using System.Collections.Generic;
using Jessy.CodeMonkey.Utils;
using UnityEngine; // namespace of utils class. 

// code monkey grid genration https://www.youtube.com/watch?v=waEsGu--9P8&list=PLzDRvYVwl53uhO8yhqxcyjDImRjO9W722&index=1
public class Grid {
    private int width;
    private int height;
    private float cellSize;

    private Vector3 originPosition; 
    private int[, ] gridArray;
    private TextMesh[, ] debugTextArray;

    public Grid (int width, int height, float cellSize, Vector3 originPosition) { // basic setup an array that takes two ints

        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition; 


        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height]; 

        for (int x = 0; x < gridArray.GetLength (0); x++) { // cycle through multi dimentional array
            for (var y = 0; y < gridArray.GetLength (1); y++) {
                debugTextArray[x, y] = GridUtils.CreateWorldText (gridArray[x, y].ToString (), null, GetWorldPosition (x, y) +
                    new Vector3 (cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter); // refrence GridUtils.cs (namepace Jessy.CodeMonkey.Utils)

                Debug.DrawLine (GetWorldPosition (x, y), GetWorldPosition (x, y + 1), Color.white, 100f); // draws grid 
                Debug.DrawLine (GetWorldPosition (x, y), GetWorldPosition (x + 1, y), Color.white, 100f);

            }

        }
        Debug.DrawLine (GetWorldPosition (0, height), GetWorldPosition (width, height), Color.white, 100f); // draws horizontal grid lines
        Debug.DrawLine (GetWorldPosition (width, 0), GetWorldPosition (width, height), Color.white, 100f); // draws vertical grid lines

       

    }

    private Vector3 GetWorldPosition (int x, int y) { // get world pos of cells, used for drawing cells

        return new Vector3 (x, y) * cellSize + originPosition;
    }

    private void  GetXY(Vector3 worldPosition, out int x, out int y){ // return x and y from single function, also assists with converting world position to a grid position
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize); // modified by an origin position 
            y = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize); 

    }

    public void SetValue (int x, int y, int value) { // set cell values 

        if (x >= 0 && y >= 0 && x < width && y < height) {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString(); 
        }

    }

    public void SetValue(Vector3 worldPosition, int value){ //speaks with GetXY to get grid positions and then set value at that position, 

        int x, y;
        GetXY(worldPosition, out x, out y); // gets converted world position in the grid position format from GetXY
        SetValue(x, y, value); // takes grid postion and uses SetCellValue to set cells value at the given gris position
    }

    public int GetValue(int x, int y) { 
        if (x >= 0 && y >= 0 && x < width && y < height) {
            return gridArray[x, y];
        } else {
            return 0;
        }
    }
                                            // these to GetValue functions allow you to print out the SetValue of a cell (uses right mouse in the Testing.cs)
    public int GetValue (Vector3 worldPosition) {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    



}