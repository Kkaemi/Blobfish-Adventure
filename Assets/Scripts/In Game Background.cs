using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBackground : MonoBehaviour
{
    [SerializeField]
    private GameObject plants;

    [SerializeField]
    private GameObject bubbles;

    private readonly int maxWidth = 300;
    private readonly int interval = 20;

    void Start()
    {
        CopyObjects(plants);
        CopyObjects(bubbles);
    }

    private void CopyObjects(GameObject gameObject)
    {
        for (int i = interval; i < maxWidth; i += interval)
        {
            GameObject copy = Instantiate(gameObject);
            copy.transform.Translate(new Vector3(i, 0f));
        }
    }
}
