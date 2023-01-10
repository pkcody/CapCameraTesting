using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Testing : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = Application.streamingAssetsPath;
    }

}
