using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=alU04hvz6L4&list=PLzDRvYVwl53uhO8yhqxcyjDImRjO9W722&index=3
public class PathFinding 
{
    private const int MOVE_STARTIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14; // square root of diagonal cost or 200
    private Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closeList;

    public PathFinding(int width, int height){
        grid = new Grid<PathNode>(width, height, 10f, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g,x,y)); 
    } 

    public Grid<PathNode> GetGrid(){
        return grid;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY){
       PathNode startNode = grid.GetGridObject(startX, startY);
       PathNode endNode = grid.GetGridObject(endX, endY); 

        //   if (startNode == null || endNode == null) {
        //     // Invalid Path
        //     return null;
        // }


        openList = new List<PathNode>{startNode};
        closeList = new List<PathNode>(); 

        for (var x = 0; x < grid.GetWidth(); x++)
        {
            for (var y = 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null; 
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost(); 

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                //reached final node
                return ClaculatePath(endNode); 
            }

            openList.Remove(currentNode); // if current node isnt end node remove from list
            closeList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closeList.Contains(neighbourNode)) continue; 
                // if (!neighbourNode.isWalkable) {
                //     closeList.Add(neighbourNode);
                //     continue;
                // }
                
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost) // cycle through different option when ever neighbour node become comes current node due to its gcost being lower than the current node
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost(); 

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
         // Out of nodes on the openList
        return null;

    }

     private List<PathNode> GetNeighbourList(PathNode currentNode) {
        List<PathNode> neighbourList = new List<PathNode>();

        if (currentNode.x - 1 >= 0) {
            // Left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            // Left Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            // Left Up
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if (currentNode.x + 1 < grid.GetWidth()) {
            // Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            // Right Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            // Right Up
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        // Down
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        // Up
        if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbourList;
    }

     public PathNode GetNode(int x, int y) {
        return grid.GetGridObject(x, y);
    }


    private List<PathNode> ClaculatePath(PathNode endNode){ //cycle through all came from nodes till one doest have a parent (start node)
       List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null) {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse(); // reverses path and we have our path 
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b){
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STARTIGHT_COST * remaining; // final cost of move = amount can move diagonal + straight (hCost)

    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList){
        PathNode lowestFCostNode = pathNodeList[0];
        for (var i = 0; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode; 
    }
}
