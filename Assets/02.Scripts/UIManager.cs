using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void OnStartGameClick()
    {
        SceneManager.LoadScene("Level01", LoadSceneMode.Single);
        SceneManager.LoadScene("Play", LoadSceneMode.Additive);
    }
}
