using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSManager : MonoBehaviour
{

    [SerializeField]
    ExcelLoader excelLoader;
    [SerializeField]
    GameManager gameManager;

    //Coord. status texts
    public TMPro.TMP_Text latStatusText;
    public TMPro.TMP_Text lonStatusText;
    //Location text
    public TMPro.TMP_Text locationText;
    //Log text
    public TMPro.TMP_Text logText;

    // Coordinates given by GPS device
    public float devLat;
    public float devLon;


    void Awake()
    {
        //excelLoader.ReadCSV();
        StartCoroutine(GetLocation());
    }

    private void Update()
    {
        locationText.text = "Location: Lat: " + devLat.ToString() + " Lon: " + devLon.ToString();
        /*
        //Every time GPS is acquired, go through the latDataList. If match, then go through lonDataList.
        //If second match, call CamCountDown-method
        for (int i = 0; i < excelLoader.latDataList.Length; i++)
        {
            latStatusText.text = ("Lat data hit: " + excelLoader.latDataList[i]);

            if (excelLoader.latDataList[i] < (devLat + gameManager.latToler) && excelLoader.latDataList[i] > (devLat - gameManager.latToler))
            {
                for (int j = 0; j < excelLoader.lonDataList.Length; j++)
                {
                    lonStatusText.text = ("Lon data hit: " + excelLoader.lonDataList[i]);

                    if (excelLoader.lonDataList[j] < (devLon + gameManager.lonToler) && excelLoader.lonDataList[j] > (devLon - gameManager.lonToler))
                    {

                        StartCoroutine(CamCountdown());
                    }
                }
            }
        }
        */

    }

    IEnumerator CamCountdown()
    {
        float duration = 5f; // 5 seconds delay 

        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            gameManager.warningImage.SetActive(true);
            gameManager.audSource.PlayOneShot(gameManager.varoKamera);
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
    }

    // Gets GPS location from phone
    IEnumerator GetLocation()
    {
        #region GPS permissions
        //Permissions
        /* 
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            logText.text = ("GPS: Device location disabled");
            yield break;
        }
        */
        #endregion

        // Start service before querying location
        Input.location.Start(5f, 5f); //Accuracy and update distance
        logText.text = ("GPS: Input.location started");

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            logText.text = ("GPS: Waiting location status");

            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            logText.text = ("GPS: Timed out");
            yield break;
        }

        // Case connection failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            logText.text = ("GPS: Unable to determine device location");

            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            devLat = Input.location.lastData.latitude;
            devLon = Input.location.lastData.longitude;
            //deviceCoordText.text = ("devLat " + devLat + " devLon " + devLon);

            latStatusText.text = ("Lat data hit: " + excelLoader.latDataList[894]);
            lonStatusText.text = ("Lon data hit: " + excelLoader.lonDataList[894]);

            if (devLat < (excelLoader.latDataList[894] + gameManager.latToler) && devLat > (excelLoader.latDataList[894] - gameManager.latToler)
                && devLon < (excelLoader.lonDataList[894] + gameManager.lonToler) && devLon > (excelLoader.lonDataList[894] - gameManager.lonToler))
            {
                StartCoroutine(CamCountdown());

            }
            /*
            //Every time GPS is acquired, go through the latDataList. If match, then go through lonDataList.
            //If second match, call CamCountDown-method
            for (int i = 0; i < excelLoader.latDataList.Length; i++)
            {
                latStatusText.text = ("Lat data hit: " + excelLoader.latDataList[i]);

                if (excelLoader.latDataList[i] < (devLat + gameManager.latToler) && excelLoader.latDataList[i] > (devLat - gameManager.latToler))
                {
                    for (int j = 0; j < excelLoader.lonDataList.Length; j++)
                    {
                        lonStatusText.text = ("Lon data hit: " + excelLoader.lonDataList[i]);

                        if (excelLoader.lonDataList[j] < (devLon + gameManager.lonToler) && excelLoader.lonDataList[j] > (devLon - gameManager.lonToler))
                        {

                            StartCoroutine(CamCountdown());
                        }
                    }
                }
            }
            */
        }

        //Calls itself
        StartCoroutine(GetLocation());

    }

}
