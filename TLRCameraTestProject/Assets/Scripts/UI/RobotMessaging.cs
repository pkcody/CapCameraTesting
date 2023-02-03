using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RobotMessaging : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject chatBoxPar;

    [SerializeField] private float typingSpeed = 0.04f;
    public string response;

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>(true);
        //chatBoxPar = transform.parent.parent;
    }

    private IEnumerator DisplayLine(string line)
    {
        text.text = " ";
        text.gameObject.SetActive(true);
        foreach (char letter in line.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(2f);
        text.gameObject.SetActive(false);
    }

    public void RobotSpeakResource(ItemObject io)
    {
        print(io.UIimage.name);
        if (io.UIimage.name.Contains("TowerBuild2_UI"))
        {
            print("in");
            response = "So that’s how you reconnect an electric flow.";
            StartCoroutine(DisplayLine(response));
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
