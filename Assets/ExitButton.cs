using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void CloseText()
    {
        // Sembunyikan teks "Hai"
        gameObject.SetActive(false);
    }
}