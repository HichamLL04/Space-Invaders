using UnityEngine;
using UnityEngine.UI;

public class BrickRowManager : MonoBehaviour
{
    void Start()
    {
        Invoke("DisableLayoutGroup", 0.5f);
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
