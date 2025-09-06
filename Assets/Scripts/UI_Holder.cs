using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Holder : MonoBehaviour
{
    public void Retry_Button()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
