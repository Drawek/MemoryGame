using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StressWall : MonoBehaviour
{
    public int stressRequired;
    public TextMeshPro text;
    // Update is called once per frame
    private void Start()
    {
        text.text = stressRequired.ToString();
    }

    void Update()
    {
        if(stressRequired == 0)
        {
            if (NewPlayer.Instance.curStress == 0)
                gameObject.layer = LayerMask.NameToLayer("EnergyWallOpen");
            else
                gameObject.layer = LayerMask.NameToLayer("EnergyWallClosed");
        }
        else if (stressRequired < 0)
        {
            if (NewPlayer.Instance.curStress <= stressRequired)
                gameObject.layer = LayerMask.NameToLayer("EnergyWallOpen");
            else
                gameObject.layer = LayerMask.NameToLayer("EnergyWallClosed");

        }
        else if (stressRequired > 0)
        {
            if (NewPlayer.Instance.curStress >= stressRequired)
                gameObject.layer = LayerMask.NameToLayer("EnergyWallOpen");
            else
                gameObject.layer = LayerMask.NameToLayer("EnergyWallClosed");

        }
    }
}
