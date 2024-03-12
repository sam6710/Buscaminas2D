using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject celda;
    [SerializeField] private int width, height;
    [SerializeField] private int nBombs;

    private GameObject[][] map;
    private int x, y;

    public static Generator Instance;

    private bool winner = true;
    private int nTest = 0;

    public GameObject heightGO;
    public GameObject widthGO;
    public GameObject nBombsGO;

    private TMP_InputField heightField;
    private TMP_InputField widthField;
    private TMP_InputField nBombsField;

    public GameObject ErrorCanvas;

    [SerializeField] private Camera cam;

    private void Start()
    {

    }

    public int getBombsAround(int x, int y)
    {
        int contador = 0;
        //Arriba Izquierda
        if (x > 0 && y < height -1 && map[x-1][y+1].GetComponent<Celda>().isBomb())
        {
            contador++;
        }
        //Arriba
        if (y < height - 1 && map[x][y + 1].GetComponent<Celda>().isBomb())
        {
            contador++;
        }
        //Arriba Derecha
        if (x < width - 1 && y < height - 1 && map[x + 1][y + 1].GetComponent<Celda>().isBomb())
        {
            contador++;
        }
        //Izquierda
        if (x > 0 && map[x - 1][y].GetComponent<Celda>().isBomb())
        {
            contador++;
        }
        //Derecha
        if (x < width - 1 && map[x + 1][y].GetComponent<Celda>().isBomb())
        {
            contador++;
        }
        //Abajo Izquierda
        if (x > 0 && y > 0 && map[x - 1][y - 1].GetComponent<Celda>().isBomb())
        {
            contador++;
        }
        //Abajo
        if (y > 0 && map[x][y - 1].GetComponent<Celda>().isBomb())
        {
            contador++;
        }
        //Abajo Derecha
        if (x < width - 1 && y > 0 && map[x + 1][y - 1].GetComponent<Celda>().isBomb())
        {
            contador++;
        }
        return contador;
    }

    public int getWidth()
    {
        return this.width;
    }

    public int getHeight()
    {
        return this.height;
    }

    public int getNBombs()
    {
        return this.nBombs;
    }

    public int getNTest()
    {
        return this.nTest;
    }


    public void setWinner(bool win)
    {
        winner = win;
    }

    public bool isWinner()
    {
        return this.winner;
    }

    public void addTest()
    {
        nTest++;
    }

    public void Easy()
    {
        height = 5;
        width = 5;
        nBombs = 5;
        generarTablero(height, width, nBombs);
    }

    public void Medium()
    {
        height = 7;
        width = 7;
        nBombs = 10;
        generarTablero(height, width, nBombs);
    }

    public void Hard()
    {
        height = 10;
        width = 10;
        nBombs = 20;
        generarTablero(height, width, nBombs);
    }

    public void Custom()
    {
        heightField = heightGO.GetComponent<TMP_InputField>();
        widthField = widthGO.GetComponent<TMP_InputField>();
        nBombsField = nBombsGO.GetComponent<TMP_InputField>();
        Debug.Log(heightGO);
        Debug.Log(heightField);
        string sheight = heightField.text;
        string swidth = widthField.text;
        string snBombs = nBombsField.text;
        height = int.Parse(sheight);
        width = int.Parse(swidth);
        nBombs = int.Parse(snBombs);
        if (height < 2 || height > 24 || width < 2 || width > 32 || nBombs < 1 || nBombs > ((height * width)/3))
        {
            ErrorCanvas.SetActive(true);
        }
        else
        {
            generarTablero(height, width, nBombs);
        }
    }

    public void generarTablero(int height, int width, int nBombs)
    {
        Instance = this;

        if(height > 20 || width > 26)
        {
            cam.orthographicSize = 13;
        }
        else if (height > 16 || width > 22)
        {
            cam.orthographicSize = 11;
        }
        else if (height > 12 || width > 20)
        {
            cam.orthographicSize = 8;
        }
        else if (height > 10 || width > 16)
        {
            cam.orthographicSize = 6;
        }

        map = new GameObject[width][];
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new GameObject[height];
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i][j] = Instantiate(celda, new Vector3(i, j, 0), Quaternion.identity);
                map[i][j].GetComponent<Celda>().setX(i);
                map[i][j].GetComponent<Celda>().setY(j);
            }
        }

        Camera.main.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);

        for (int i = 0; i < nBombs; i++)
        {
            x = Random.Range(0, width);
            y = Random.Range(0, height);
            if (!map[x][y].GetComponent<Celda>().isBomb())
            {
                map[x][y].GetComponent<Celda>().setBomb(true);
            }
            else
            {
                i--;
            }
        }
    }
}
