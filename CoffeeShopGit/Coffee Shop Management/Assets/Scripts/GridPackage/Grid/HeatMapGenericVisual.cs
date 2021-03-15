using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMapGenericVisual : MonoBehaviour
{
    private Grid<HeatMaptesting.HeatMapGridObject> grid;
    private Mesh mesh;
    private bool updateMesh;

    private void Awake() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetGrid(Grid<HeatMaptesting.HeatMapGridObject> grid) {
        this.grid = grid;
        UpdateHeatMapVisual(); 

        grid.OnGridValueChanged += Grid_OnGridValueChanged;
    }

    private void Grid_OnGridValueChanged(object sender, Grid<HeatMaptesting.HeatMapGridObject>.OnGridValueChangedEventArgs e) {
        //UpdateHeatMapVisual();
        updateMesh = true;
    }

    private void LateUpdate() { // uses late up date to avoid UV mesh updating once in each frame regardless of how many times its called 
        if (updateMesh) {
            updateMesh = false;
            UpdateHeatMapVisual();
        }
    }

    private void UpdateHeatMapVisual() {
        MeshUtils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for (int x = 0; x < grid.GetWidth(); x++) {
            for (int y = 0; y < grid.GetHeight(); y++) {
                int index = x * grid.GetHeight() + y;
                Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();

                HeatMaptesting.HeatMapGridObject gridObject = grid.GetGridObject(x, y); // gets grid value
                float gridValueNormalized = gridObject.GetNormalized(); 
                Vector2 gridValueUV = new Vector2(gridValueNormalized, 0f); // converts value to position on the UV texture 
                MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + quadSize * .5f, 0f, quadSize, gridValueUV, gridValueUV); // Maps UV texture depending on what value the given grid position is at
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

}
