using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    //[SerializeField]

    public void OnStartButton()
    {
        SceneManager.LoadScene("Main");
    }
}