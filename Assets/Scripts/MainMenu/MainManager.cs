using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public void NewGame () {
        SceneManager.LoadScene(1);
    }
}
