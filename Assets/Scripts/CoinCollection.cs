using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollection : MonoBehaviour
{
    public Text coinShower;
    private GameObject[] coins;
    private int coinActual, coinTarget;
    private bool canGoal;
    // Start is called before the first frame update
    void Start()
    {
        canGoal = false;
        coinActual = 0;
        coins = GameObject.FindGameObjectsWithTag("Coin");
        coinTarget = (int)coins.Length;
        coinShower.text = "Collected " + coinActual.ToString() + " of " + coinTarget.ToString() + " fish scrape";
    }
    private void OnTriggerEnter(Collider objeto)
    {
        if (objeto.gameObject.CompareTag("Coin"))
        {
            objeto.gameObject.SetActive(false);
            coinActual += 1;
            if (coinActual >= coinTarget)
            {
                canGoal = true;
                coinShower.text = "All fish scrap collected";
            }
        }
        if (objeto.gameObject.CompareTag("Goal") && canGoal == true)
        {
            //cambiar escena de la victoria
        }
    }
}
