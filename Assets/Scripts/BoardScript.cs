using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public float Sensitivity = 0.1f;
    public Transform FloorTile;
    public Transform Wall;
    public float Width = 10;
    public float Height = 10;

    private Vector3 _previousMousePosition = new Vector3(0, 0, 0);
    private Vector3 _rotation = new Vector3(0, 0, 0);
    private Rigidbody _rigidBody;
    private Maze _maze;

    // Start is called before the first frame update
    void Start()
    {
        _previousMousePosition = Input.mousePosition;
        _rigidBody = transform.GetComponent<Rigidbody>();

        _maze = new Maze();
        _maze.Generate();
        
        CreateOuterWalls();
        CreateFloorTiles();
        CreateInnerWalls();
    }

    private void CreateOuterWalls()
    {
        // Create walls
        var rightWall = Instantiate(Wall, new Vector3(Width / 2 + Wall.localScale.x / 2, Wall.localScale.y / 2, 0), Quaternion.identity, transform);
        var leftWall = Instantiate(Wall, new Vector3(-Width / 2 - Wall.localScale.x / 2, Wall.localScale.y / 2, 0), Quaternion.identity, transform);
        var topWall = Instantiate(Wall, new Vector3(0, Wall.localScale.y / 2, Height / 2 + Wall.localScale.z / 2), Quaternion.identity, transform);
        var bottomWall = Instantiate(Wall, new Vector3(0, Wall.localScale.y / 2, -Height / 2 - Wall.localScale.z / 2), Quaternion.identity, transform);

        // Name walls
        rightWall.name = "Right Outer Wall";
        leftWall.name = "Left Outer Wall";
        topWall.name = "Top Outer Wall";
        bottomWall.name = "Bottom Outer Wall";

        // Scale walls
        rightWall.localScale = new Vector3(Wall.localScale.x, Wall.localScale.y, Height);
        leftWall.localScale = new Vector3(Wall.localScale.x, Wall.localScale.y, Height);
        topWall.localScale = new Vector3(Width, Wall.localScale.y, Wall.localScale.z);
        bottomWall.localScale = new Vector3(Width, Wall.localScale.y, Wall.localScale.z);
    }

    private void CreateFloorTiles()
    {
        for (var x = 0; x < _maze.CellCountX; x++)
        {
            for (var z = 0; z < _maze.CellCountZ; z++)
            {
                var cell = _maze.Cells[x, z];
                if (cell.FloorType == FloorType.Solid)
                {
                    var tile = Instantiate(FloorTile, new Vector3(x - 4.5f, -FloorTile.localScale.y / 2, z - 4.5f), Quaternion.identity, transform);
                    tile.name = $"Floor Tile ({x}, {z})";
                }                
            }
        }
    }

    private void CreateInnerWalls()
    {
        for (var x = 0; x < _maze.CellCountX; x++)
        {
            for (var z = 0; z < _maze.CellCountZ; z++)
            {
                var cell = _maze.Cells[x, z];
                // Skip walls with a z index of CellCountZ - 1 to avoid creating walls that overlap with the outer walls.
                if (cell.Walls[Direction.Up] && z < _maze.CellCountZ - 1)
                {
                    var wall = Instantiate(Wall, new Vector3(x - 4.5f, Wall.localScale.y / 2, z - 4), Quaternion.identity, transform);
                    wall.name = $"Inner Wall ({x}, {z}) Up";
                    wall.localScale = new Vector3(1, wall.localScale.y, wall.localScale.z);
                    wall.localScale = new Vector3(1, wall.localScale.y, wall.localScale.z);
                }
                // Skip walls with a x index of CellCountX - 1 to avoid creating walls that overlap with the outer walls.
                if (cell.Walls[Direction.Right] && x < _maze.CellCountX - 1)
                {
                    var wall = Instantiate(Wall, new Vector3(x - 4f, Wall.localScale.y / 2, z - 4.5f), Quaternion.identity, transform);
                    wall.name = $"Inner Wall ({x}, {z}) Right";
                    wall.localScale = new Vector3(wall.localScale.x, wall.localScale.y, 1);
                    wall.localScale = new Vector3(wall.localScale.x, wall.localScale.y, 1);
                }
            }
        }
    }
   
    // Update is called once per frame
    public void Update()
    {
        var deltaMousePosition = Input.mousePosition - _previousMousePosition;
        _previousMousePosition = Input.mousePosition;

        _rotation += new Vector3(deltaMousePosition.y, 0, -deltaMousePosition.x);
        _rigidBody.MoveRotation(Quaternion.Euler(_rotation));
    }
}