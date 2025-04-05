using System.Collections;
using UnityEngine;

public class UseCoroutine : MonoBehaviour
{
    public void StartCoroutines(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }
}
