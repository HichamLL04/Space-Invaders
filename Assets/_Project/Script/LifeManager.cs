using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] GameObject[] vidas;
    int numVidas = 2;
    GameManager gameManager;
    BrickRowManager brickRowManager;
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        brickRowManager = FindFirstObjectByType<BrickRowManager>();
        brickRowManager.Invoke("DisableLayoutGroup", 0.5f);
    }

    void Update()
    {

    }

    public void RestarVida()
    {
        if (numVidas >= 0)
        {
            Destroy(vidas[numVidas]);
            numVidas -= 1;
        }
        else
        {
            gameManager.GameOver();
        }

    }
}
