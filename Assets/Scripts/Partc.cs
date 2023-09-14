using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Partc : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitAndDestroy());
        //Destroy(gameObject,2);
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(2);
        PhotonNetwork.Destroy(gameObject);
    }
}
