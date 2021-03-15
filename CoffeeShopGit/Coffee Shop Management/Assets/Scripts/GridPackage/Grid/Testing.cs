using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jessy.CodeMonkey.Utils; // namespace of utils class

// codemonkey Grid Testing system https://www.youtube.com/watch?v=waEsGu--9P8&list=PLzDRvYVwl53uhO8yhqxcyjDImRjO9W722&index=1
public class testing : MonoBehaviour
{
    [SerializeField] public HeatMapVisual heatMapVisual; 
    [SerializeField] private HeatMapBoolVisual heatMapBoolVisual; 
    private Grid<bool> grid;
    // Start is called before the first frame update
    void Start()
    {
         grid = new Grid<bool>(100, 100, 10f,  Vector3.zero);// tests grid in Grid.cs

        // heatMapVisual.SetGrid(grid); 
    }

     private void Update() { // get mosue postion 
        if (Input.GetMouseButtonDown(0))
        {
          // grid.SetValue(GridUtils.GetMouseWorldPosition(), 56); // sets cell value at the mouses postion converted to grid position, 56 is the value it will set;
          Vector3 position = GridUtils.GetMouseWorldPosition();
          grid.SetValue(position, true); 

        }

        
    }

    
}
