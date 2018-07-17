using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class JsonConvertTest : MonoBehaviour
{

    // Use this for initialization
    void OnGUI()
    {
        if (GUILayout.Button("toString"))
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict.Add("a", 0);
            dict.Add("b", 1);
            dict.Add("c", 2);
            dict.Add("d", 3);
            dict.Add("e", 4);
            dict.Add("f", 5);
            Debug.LogError(dict.SerializeObject());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
