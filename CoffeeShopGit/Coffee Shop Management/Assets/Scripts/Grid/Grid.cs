using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

        Debug.Log(width + "+" + height); 
    }

}
