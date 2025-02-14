using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hyperlink : MonoBehaviour
{
 public void Openurl(string Urlname)
 {
    Application.OpenURL(Urlname);
 }
}
