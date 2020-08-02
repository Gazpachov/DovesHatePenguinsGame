using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public Animator anim;
    public Transform lastCheckpoint;
    public GameObject scrapImage;

    public Text coinShower,tutoShower;
    private GameObject[] coins;
    private int coinActual, coinTarget;
    private bool canGoal;
    // Start is called before the first frame update
    void Start()
    {
        canGoal = false;
        //coinActual = 0;
        coins = GameObject.FindGameObjectsWithTag("Coin");
        coinTarget = (int)coins.Length;
        coinActual = coinTarget;
        coinShower.text = "X " + coinActual.ToString();
        tutoShower.text = "Make click on any point of screen to move, press S to save the game or pres L to load game saved";
        Invoke("Untext", 5);
        lastCheckpoint.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(camRay, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
        if (agent.remainingDistance <= 2)
        {
            anim.SetBool("isRunning", false);

        }
        else
        {
            anim.SetBool("isRunning", true);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame(lastCheckpoint.position, coinActual);
            tutoShower.text = "Your game save has been updated";
            Invoke("Untext", 2);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            agent.enabled = false;
            transform.position = LoadGamePos();
            agent.enabled = true;
            coinActual = LoadGameCoin();
            tutoShower.text = "Your game has been recovered";
            Invoke("Untext", 2);
        }
    }
    private void OnTriggerEnter(Collider objeto)
    {
        if (objeto.gameObject.CompareTag("Coin"))
        {
            objeto.gameObject.SetActive(false);
            coinActual -= 1;
            if (coinActual <= 0)
            {
                canGoal = true;
                scrapImage.SetActive(false);
                coinShower.text = "All fish scrap collected";
            }
            else
            {
                coinShower.text = "X " + coinActual.ToString();
            }
        }
        if (objeto.gameObject.CompareTag("Goal"))
        {
            if(canGoal == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                tutoShower.text = "You have to collect all the fish scrap first";
                Invoke("Untext", 3);
            }
        }
        if (objeto.gameObject.CompareTag("Enemy"))
        {
            comeBack();
            tutoShower.text = "Enemies bring you back to your last checkpoint";
            Invoke("Untext", 3);
        }
        if (objeto.gameObject.CompareTag("CheckPoint"))
        {
            lastCheckpoint.position = new Vector3(objeto.GetComponent<Transform>().position.x, objeto.GetComponent<Transform>().position.y, objeto.GetComponent<Transform>().position.z);
            tutoShower.text = "Checkpoint reached";
            Invoke("Untext", 1);
        }
    }
    void comeBack()
    {
        agent.enabled = false;
        transform.position = new Vector3(lastCheckpoint.position.x, lastCheckpoint.position.y, lastCheckpoint.position.z);
        agent.enabled = true;
    }
     void Untext()
    {
        tutoShower.text = "";
    }
    public static void SaveGame(Vector3 Posisave,int CoinSave)
    {
        PlayerPrefs.SetFloat("x", Posisave.x);
        PlayerPrefs.SetFloat("y", Posisave.y);
        PlayerPrefs.SetFloat("z", Posisave.z);
        PlayerPrefs.SetInt("CoinRe",CoinSave);
    }
    public static Vector3 LoadGamePos()
    {
        Vector3 setPos;
        setPos.x = PlayerPrefs.GetFloat("x");
        setPos.y = PlayerPrefs.GetFloat("y");
        setPos.z = PlayerPrefs.GetFloat("z");
        return setPos;
    }
    public static int LoadGameCoin()
    {
        int gCoins;
        gCoins = PlayerPrefs.GetInt("CoinRe");
        return gCoins;
    }

}
