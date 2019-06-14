using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstructions : MonoBehaviour {
    public List<GameObject> InstructionsList;
    public void NextInstruction(int num)
    {
        InstructionsList[num - 1].SetActive(false);
        InstructionsList[num].SetActive(true);
    }
    public void GameStart()
    {
        InstructionsList[5].SetActive(false);
        InstructionsList[0].SetActive(false);
        Time.timeScale = 1;
    }
}
