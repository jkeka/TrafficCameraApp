using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class ExcelLoader : MonoBehaviour
{

    //CSV-files with traffic camera coordinates
    public TextAsset latData;
    public TextAsset lonData;

    public float[] latDataList;
    public float[] lonDataList;

    string basePathLat;
    string basePathLon;


    private void Awake()
    {
        //basePathLat = Path.Combine(Application.streamingAssetsPath, "camLatitudes.json");
        //basePathLon = Path.Combine(Application.streamingAssetsPath, "camLongitudes");


        //latData = Resources.Load("camLatitudes") as TextAsset;
        //lonData = Resources.Load("camLongitudes") as TextAsset;


        ReadCSV();

        //Works in editor, not in Android build
        //latData = (TextAsset)Resources.Load("camLatitudes", typeof(TextAsset));
        //lonData = (TextAsset)Resources.Load("camLongitudes", typeof(TextAsset));

        //Works in editor, not in Android build
        //latData = Resources.Load<TextAsset>("camLatitudes");
        //lonData = Resources.Load<TextAsset>("camLongitudes");
    }

    public void ReadCSV()
    {
        //Git test
        //string[] latDataStringList = System.IO.File.ReadAllLines(basePathLat);
        //string[] lonDataStringList = System.IO.File.ReadAllLines(basePathLon);


        string[] latDataStringList = latData.text.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        string[] lonDataStringList = lonData.text.Split(new string[] { "\n" }, System.StringSplitOptions.None);

        // convert to float
        latDataList = new float[latDataStringList.Length];
        lonDataList = new float[lonDataStringList.Length];

        for (int i = 0; i < latDataStringList.Length; i++)
        {
            float.TryParse(latDataStringList[i], out latDataList[i]);
        }

        for (int i = 0; i < lonDataStringList.Length; i++)
        {
            //lonDataList[i] = float.Parse(lonDataStringList[i]);
            float.TryParse(lonDataStringList[i], out lonDataList[i]);
        }


        Debug.Log("CSV readed");

    }

}
