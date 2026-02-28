using UnityEngine;
using UnityEngine.UI;


public class LifeManager : MonoBehaviour
{
    [SerializeField] GameObject[] vidas;
    int numVidas = 2;
    GameManager gameManager;


    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }


    void Update()
    {

    }


    public void RestarVida()
    {
        if (numVidas >= 1)
        {
            vidas[numVidas].GetComponent<SpriteRenderer>().enabled = false;
            numVidas -= 1;
        }
        else
        {
            gameManager.GameOver();
            Debug.Log("GameOver life");
        }
    }
}
