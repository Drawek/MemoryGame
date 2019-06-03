using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StressWall : MonoBehaviour
{
    public int stressRequired;
    private int lastStress;
    private bool lastWorldState;
    public TextMeshPro text;
    public GameObject[] sprites;
    // Update is called once per frame
    private void Start()
    {
        CalculateOpening();
        if (text != null)
        text.text = stressRequired.ToString();
        lastStress = NewPlayer.Instance.curStress;
    }

    void Update()
    {
        if(lastStress != NewPlayer.Instance.curStress || GameManager.Instance.isChaosWorld != lastWorldState)
        {
            print("SwitchWorld");
            CalculateOpening();
            lastWorldState = GameManager.Instance.isChaosWorld;
            lastStress = NewPlayer.Instance.curStress;
        }        
    }
    private void CalculateOpening()
    {
        //Neutral Stress
        if (stressRequired == 0)
        {
            if (NewPlayer.Instance.curStress == 0)
                gameObject.layer = LayerMask.NameToLayer("EnergyWallOpen");
            else
                gameObject.layer = LayerMask.NameToLayer("EnergyWallClosed");
        }
        //Negative Stress
        else if (stressRequired < 0)
        {
            if (sprites.Length != 0)
            {
                for (int i = 0; i < sprites.Length; i++)
                {
                    if (i < amountOfWhiteOrbs())
                        sprites[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.25f);
                    else
                        sprites[i].GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 1f);
                }
            }
            if (amountOfWhiteOrbs() >= -stressRequired)
            {
                gameObject.layer = LayerMask.NameToLayer("EnergyWallOpen");
                if (GameManager.Instance.isChaosWorld)
                    GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.15f);
                else
                    GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.15f);
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("EnergyWallClosed");
                if (GameManager.Instance.isChaosWorld)
                    GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.9f);
                else
                    GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.9f);
            }


        }
        //Positive Stress
        else if (stressRequired > 0)
        {
            if (sprites.Length != 0)
            {
                for (int i = 0; i < sprites.Length; i++)
                {
                    if (i < amountOfBlackOrbs())
                        sprites[i].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.25f);
                    else
                        sprites[i].GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0, 1f);
                }
            }
            if (amountOfBlackOrbs() >= stressRequired)
            {
                gameObject.layer = LayerMask.NameToLayer("EnergyWallOpen");
                if(GameManager.Instance.isChaosWorld)
                    GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.15f);
                else
                    GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.15f);
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("EnergyWallClosed");
                if (GameManager.Instance.isChaosWorld)
                    GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.9f);
                else
                    GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.9f);
            }
        }
    }

    private int amountOfBlackOrbs()
    {
        return NewPlayer.Instance.curStress + NewPlayer.Instance.stressTolerance;
    }

    private int amountOfWhiteOrbs()
    {
        return -NewPlayer.Instance.curStress + NewPlayer.Instance.stressTolerance;
    }
}
