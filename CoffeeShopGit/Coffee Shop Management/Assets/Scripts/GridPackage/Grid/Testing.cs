using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jessy.CodeMonkey.Utils; // namespace of utils class

// codemonkey Grid Testing system https://www.youtube.com/watch?v=waEsGu--9P8&list=PLzDRvYVwl53uhO8yhqxcyjDImRjO9W722&index=1
public class Testing : MonoBehaviour
{

    private Grid grid;
    // Start is called before the first frame update
    void Start()
    {
         grid = new Grid(4, 2, 10f, new Vector3(20 ,0)); // tests grid in Grid.cs
    }

     private void Update() { // get mosue postion 
        if (Input.GetMouseButtonDown(0))
        {
           grid.SetValue(GridUtils.GetMouseWorldPosition(), 56); // sets cell value at the mouses postion converted to grid position, 56 is the value it will set;
        }

        if(Input.GetMouseButtonDown(1)){ // prints value of a cell on mouses grid postion 
            Debug.Log(grid.GetValue(GridUtils.GetMouseWorldPosition()));
        }
    }

    
}
