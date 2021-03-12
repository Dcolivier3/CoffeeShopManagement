using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// codemonkey Grid Testing system https://www.youtube.com/watch?v=waEsGu--9P8&list=PLzDRvYVwl53uhO8yhqxcyjDImRjO9W722&index=1
public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Grid grid = new Grid(4, 2, 10f); // tests grid in Grid.cs
    }

     private void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }

    
}
