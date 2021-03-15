using System.Collections;
using System.Collections.Generic;
using Jessy.CodeMonkey.Utils;
using UnityEngine; // namespace of utils class

// codemonkey Grid Testing system https://www.youtube.com/watch?v=waEsGu--9P8&list=PLzDRvYVwl53uhO8yhqxcyjDImRjO9W722&index=1
public class HeatMaptesting : MonoBehaviour {
  
  [SerializeField] private HeatMapGenericVisual heatMapGenericVisual;
  private Grid<HeatMapGridObject> grid;

  // Start is called before the first frame update
  void Start () {
    grid = new Grid<HeatMapGridObject> (100, 100, 10f, Vector3.zero, (Grid<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject (g, x, y)); // tests grid in Grid.cs
    heatMapGenericVisual.SetGrid(grid);
    
  }

  private void Update () { // get mosue postion 
    if (Input.GetMouseButtonDown (0)) {

      Vector3 position = GridUtils.GetMouseWorldPosition ();
      HeatMapGridObject heatMapGridObject = grid.GetGridObject (position);
      if (heatMapGridObject != null) {
        heatMapGridObject.AddAvalue (5);
      }

    }

  }

  public class HeatMapGridObject {
    private const int MIN = 0;
    private const int MAX = 100;
    public int value;

    private int x;
    private int y; 

    public Grid<HeatMapGridObject> grid;

    public HeatMapGridObject(Grid<HeatMapGridObject> grid, int x, int y){
      this.grid = grid; 
      this.x = x;
      this.y = y; 
    }

    public void AddAvalue (int addValue) {
      value += addValue;
      value = Mathf.Clamp (value, MIN, MAX);
      grid.TriggerGridObjectChanged(x, y); 
    }

    public float GetNormalized () {
      return (float) value / MAX;
    }
    public override string ToString () {
      return value.ToString ();
    }
  }

}