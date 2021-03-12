using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jessy.CodeMonkey.Utils; 


// code monkey grid genration https://www.youtube.com/watch?v=waEsGu--9P8&list=PLzDRvYVwl53uhO8yhqxcyjDImRjO9W722&index=1
public class Grid 
{
    private int width;
    private int height;
    private int[,] gridArray;

    public Grid(int width, int height){ // basic setup an array that takes two ints

        this.width = width;
        this.height = height; 

        gridArray = new int[width, height];

        for (int x=0; x < gridArray.GetLength(0); x++)
        {                                                   // cycle through multi dimentional array
            for (var y = 0; y < gridArray.GetLength(1); y++)
            {
                
            }
        } 
    }

}
