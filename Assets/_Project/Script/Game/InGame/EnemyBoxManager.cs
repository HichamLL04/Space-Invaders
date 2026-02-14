using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBoxManager : MonoBehaviour
{
    [SerializeField] HorizontalLayoutGroup[] horizontalLayoutGroup;
    [SerializeField] GameObject[] alien;
    // Del 0 al 1 Alien 1. El 2 es Alien 2 y asi continua hasta el 5. Debe haber 8 por fila
    void Start()
    {

    }

    void Update()
    {

    }

    void EnableLayoutGroup()
    {
        foreach (HorizontalLayoutGroup group in horizontalLayoutGroup)
        {
            group.GetComponent<RowManager>().EnableLayoutGroup();
        }
    }

    void DisableLayoutGroup()
    {
        foreach (HorizontalLayoutGroup group in horizontalLayoutGroup)
        {
            group.GetComponent<RowManager>().DisableLayoutGroup();
        }
    }

    public void GenerateEnemy(int wave)
    {
        EnableLayoutGroup();

        if (wave == 1)
        {
            GenerateLine(0, 0);
            GenerateLine(0, 1);
            GenerateLine(1, 2);
        }
        else if (wave > 1)
        {
            GenerateLine(0, 0);
            GenerateLine(0, 1);
            GenerateLine(1, 2);
            GenerateLine(3, 3);
            GenerateLine(3, 3);
            GenerateLine(4, 4);
            GenerateLine(5, 5);
        }

        StartCoroutine(DisableLayoutGroupDelayed());
    }

    IEnumerator DisableLayoutGroupDelayed()
    {
        yield return null;
        DisableLayoutGroup();
    }

    void GenerateLine(int idAlien, int idRow)
    {
        for (int i = 0; i < 9; i++)
        {
            Instantiate(alien[idAlien], horizontalLayoutGroup[idRow].transform);
        }
    }
}
