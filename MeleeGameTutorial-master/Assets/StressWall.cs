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
    public Sprite lightWorldWall, darkWorldWall;
    public Sprite orangeOrbOpen, orangeOrbClosed, blueOrbOpen, blueOrbClosed;
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
                        sprites[i].GetComponent<SpriteRenderer>().sprite = blueOrbClosed;
                    else
                        sprites[i].GetComponent<SpriteRenderer>().sprite = blueOrbOpen;
                   
                }
            }
            if (amountOfWhiteOrbs() >= -stressRequired)
            {
                gameObject.layer = LayerMask.NameToLayer("EnergyWallOpen");
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.15f);
                if (GameManager.Instance.isChaosWorld)
                    GetComponent<SpriteRenderer>().sprite = darkWorldWall;
                else
                    GetComponent<SpriteRenderer>().sprite = lightWorldWall;
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("EnergyWallClosed");
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
                if (GameManager.Instance.isChaosWorld)
                    GetComponent<SpriteRenderer>().sprite = darkWorldWall;
                else
                    GetComponent<SpriteRenderer>().sprite = lightWorldWall;
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
                        sprites[i].GetComponent<SpriteRenderer>().sprite = orangeOrbClosed;
                    else
                        sprites[i].GetComponent<SpriteRenderer>().sprite = orangeOrbOpen;
                }
            }
            if (amountOfBlackOrbs() >= stressRequired)
            {
                gameObject.layer = LayerMask.NameToLayer("EnergyWallOpen");
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.15f);
                if (GameManager.Instance.isChaosWorld)
                    GetComponent<SpriteRenderer>().sprite = darkWorldWall;
                else
                    GetComponent<SpriteRenderer>().sprite = lightWorldWall;
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("EnergyWallClosed");
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
                if (GameManager.Instance.isChaosWorld)
                    GetComponent<SpriteRenderer>().sprite = darkWorldWall;
                else
                    GetComponent<SpriteRenderer>().sprite = lightWorldWall;
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
