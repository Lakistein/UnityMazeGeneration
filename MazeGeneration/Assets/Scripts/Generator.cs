using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject Cell;
    public int Size;
    private GameObject cell;
    void Start()
    {
        //MazeGeneratorDepthFirst.Cell[,] cells = MazeGeneratorDepthFirst.GenerateMaze(Size);
        //var a = cells;

        //for (int i = 0; i < Size; i++)
        //{
        //    for (int j = 0; j < Size; j++)
        //    {
        MazeGeneratorDepthFirst.Cell currCell = MazeGeneratorDepthFirst.GenerateMaze(Size);
        cell = GameObject.Instantiate(Cell);
        cell.name = currCell.X + "," + currCell.Y;
        cell.transform.position = new Vector3(currCell.X * 10, 0, currCell.Y * -10);
        cell.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.red;
        for (int w = 0; w < 4; w++)
        {
            if (currCell.Walls[w] == 1)
            {
                cell.transform.GetChild(w + 1).gameObject.SetActive(false);
            }
        }
        //    }
        //}
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            while (GenerateOne()) ;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateOne();
        }
    }

    bool GenerateOne()
    {
        cell.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.white;

        MazeGeneratorDepthFirst.Cell c = MazeGeneratorDepthFirst.GenerateNext();
        if (c == null) return false;
        var a = GameObject.Find(c.X + "," + c.Y);
        if (a == null)
        {
            GameObject celll = GameObject.Instantiate(Cell);
            cell = celll;
            cell.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.red;

            celll.name = c.X + "," + c.Y;
            celll.transform.position = new Vector3(c.X * 10, 0, c.Y * -10);
            for (int w = 0; w < 4; w++)
            {
                if (c.Walls[w] == 1)
                {
                    celll.transform.GetChild(w + 1).gameObject.SetActive(false);
                }
            }
        }
        else
        {
            cell = a;
            cell.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.red;
            for (int w = 0; w < 4; w++)
            {
                if (c.Walls[w] == 1)
                {
                    cell.transform.GetChild(w + 1).gameObject.SetActive(false);
                }
            }
        }
        return true;
    }

    void GenerateAll()
    {

    }
}
