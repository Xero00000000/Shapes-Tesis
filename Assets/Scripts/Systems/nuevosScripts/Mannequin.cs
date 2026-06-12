using ImprovedTimers;
using Unity.VisualScripting;

//using UnityEditor.Playables;
using UnityEngine;
using UnityUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class Mannequin : MonoBehaviour
{

    public int currentHead;
    public int currentTorso;
    public int currentArms;
    public int currentLegs;

    public List<GameObject> headParts;
    public List<GameObject> torsoParts;
    public List<GameObject> armsParts;
    public List<GameObject> legsParts;

    public void Start()
    {
        for (int i = 0; i < headParts.Count; i++)
        {
            headParts[i].SetActive(i == currentHead);
        }
        for (int i = 0; i < torsoParts.Count; i++)
        {
            torsoParts[i].SetActive(i == currentTorso);
        }
        for (int i = 0; i < armsParts.Count; i++)
        {
            armsParts[i].SetActive(i == currentArms);
        }
        for (int i = 0; i < legsParts.Count; i++)
        {
            legsParts[i].SetActive(i == currentLegs);
        }
    }

    public void SwapPart(int partToSwap, int partNumber)
    {
        switch (partToSwap)
        {
            case 1:
                for (int i = 0; i < headParts.Count; i++)
                {
                    headParts[i].SetActive(i == partNumber);
                }
                break;
            case 2:
                for (int i = 0; i < torsoParts.Count; i++)
                {
                    torsoParts[i].SetActive(i == partNumber);
                }
                break;
            case 3:
                for (int i = 0; i < armsParts.Count; i++)
                {
                    armsParts[i].SetActive(i == partNumber);
                }
                break;
            case 4:
                for (int i = 0; i < legsParts.Count; i++)
                {
                    legsParts[i].SetActive(i == partNumber);
                }
                break;
        }
    }
}
