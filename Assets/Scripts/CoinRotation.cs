using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(360, 0, 0) * Time.deltaTime);
    }
}
