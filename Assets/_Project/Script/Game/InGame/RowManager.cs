using UnityEngine;
using UnityEngine.UI;

public class RowManager : MonoBehaviour
{
    RectTransform rectTransform;


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    public void EnableLayoutGroup()
    {
        GetComponent<HorizontalLayoutGroup>().enabled = true;
        rectTransform.anchoredPosition = Vector2.zero;
    }


    public void DisableLayoutGroup()
    {
        GetComponent<HorizontalLayoutGroup>().enabled = false;
    }
}