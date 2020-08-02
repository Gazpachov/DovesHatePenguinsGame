using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeleporterScript : MonoBehaviour
{
    public Transform destination;
    public AudioSource mainMusic;
    public AudioClip nextClip;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<NavMeshAgent>().enabled = false;
            other.transform.position = new Vector3(destination.position.x, destination.position.y + 2, destination.position.z);
            other.GetComponent<NavMeshAgent>().enabled = true;
            Invoke("MusicSwap", 1);
        }
    }
    void MusicSwap()
    {
        mainMusic.clip =nextClip;
        mainMusic.Play();
    }
}

