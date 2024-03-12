using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Celda : MonoBehaviour
{
    [SerializeField] private int x, y;
    [SerializeField] private bool bomb;
    [SerializeField] private TextMeshProUGUI tmp_text;

    //public static Celda Instance;

    public void setBomb(bool bomb)
    {
        this.bomb = bomb;
    }

    public bool getBomb()
    {
        return this.bomb;
    }

    public void setX(int x)
    {
        this.x = x;
    }

    public void setY(int y)
    {
        this.y = y;
    }

    public bool isBomb()
    {
        return this.bomb;
    }

    public void SetText(string text)
    {
        this.tmp_text.text = text;
    }

    private void OnMouseDown()
    {
        if (this.isBomb())
        {
            //Debug.Log("BOMBAZO");
            GetComponent<SpriteRenderer>().color = Color.red;
            Generator.Instance.setWinner(false);
            Scenemanager.Instance.LoseScene();
        }
        else
        {
            Debug.Log("no hay bomba");
            tmp_text.text = Generator.Instance.getBombsAround(x, y).ToString();
            Generator.Instance.addTest();
            if (((Generator.Instance.getWidth() * Generator.Instance.getHeight()) - Generator.Instance.getNBombs() == Generator.Instance.getNTest()) && Generator.Instance.isWinner())
            {
                Scenemanager.Instance.WinScene();
                Debug.Log("se gana");
            }
        }
    }
}
