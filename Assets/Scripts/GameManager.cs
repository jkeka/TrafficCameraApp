using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Variables

    [SerializeField]
    GPSManager gpsManager;
    [SerializeField]
    ExcelLoader excelLoader;

    //Warning image
    public GameObject warningImage;
    //Locations
    public List<float> latitudes = new List<float>();
    public List<float> longitudes = new List<float>();

    //Audio
    public AudioSource audSource;
    public AudioClip varoKamera;

    //Add
    public double latToler = 0.00050;
    public double lonToler = 0.0040;

    public double testLat;
    public double testLon;

    public double manualLat;
    public double manualLon;

    #endregion 

    void Awake()
    {
        warningImage.SetActive(false);

        /*
        for (int i = 0; i < excelLoader.latDataList.Length - 1; i++)
        {
            Debug.Log("Cam nro " + i + " : " + excelLoader.latDataList[i] + " " + excelLoader.lonDataList[i]);
        }
        */
        //Temporary coordinate values for testing
        testLat = 63.143390;
        testLon = 27.867317;
    }


    
    
}
