using UnityEngine;
using UnityEngine.UI;

public class BrickRowManager : MonoBehaviour
{
    [SerializeField] GameObject[] bricks;

    private static int totalBricks = 0;

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
