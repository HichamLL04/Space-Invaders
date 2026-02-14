using UnityEngine;
using UnityEngine.UI;

public class RowManager : MonoBehaviour
{
    void Start()
    {
        
    }

    public void DisableLayoutGroup()
    {
        var layoutGroup = GetComponent<HorizontalLayoutGroup>();
        if (layoutGroup != null)
        {
            layoutGroup.enabled = false;
        }
    }

    public void EnableLayoutGroup()
    {
        var layoutGroup = GetComponent<HorizontalLayoutGroup>();
        if (layoutGroup != null)
        {
            layoutGroup.enabled = true;
        }
    }
}
