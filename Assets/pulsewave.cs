using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;
using UnityEngine;

public class pulsewave : MonoBehaviour
{
    Vector3 value;
    Datapoint[] points;
    public string pointsDataFileName = "testsample.json";

    public GameObject pointed;
    public int maxNumberOfPoints;
    public float positionScaleFactor;
    public float sizeScaleFactor;

    public class Datapoint
    {
        public Vector3 pos;
        public Vector3 sca;

        public Datapoint(Vector3 posit, Vector3 radi)
        {
            pos = posit;
            sca = radi;
        }
    }


    void Start()
    {
        LoadPointsData();
        drawPoints();
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            //OnMouseClick and hold, box increaces in size, colliding with points and generating events
            //Eventually will also be mapped to vive controller trigger
            float t = Time.deltaTime;
            value = transform.localScale;
            value.x += t;
            value.y += t;
            value.z += t;

            transform.localScale = value;
        }
    }

    private void LoadPointsData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, pointsDataFileName);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            var js = JSON.Parse(dataAsJson);
            // Find the points object in the JSON
            foreach(JSONNode pts in js.Children)
            {
                int ptNumber = pts.Count;
                points = new Datapoint[ptNumber];
                foreach (JSONNode pt in pts.Children)
                {

                    float ptX, ptY, ptZ, ptR;

                    if (positionScaleFactor == 0)
                    {
                        ptX = (pt["x"]);
                        ptY = (pt["y"]);
                        ptZ = (pt["z"]);
                        ptR = (pt["r"]);
                    }
                    else
                    {
                        ptX = (pt["x"] / positionScaleFactor);
                        ptY = (pt["y"] / positionScaleFactor);
                        ptZ = (pt["z"] / positionScaleFactor);
                        ptR = (pt["r"] / sizeScaleFactor);
                    }
                    

                    Vector3 position = new Vector3(ptX, ptY, ptZ);
                    Vector3 radius = new Vector3(ptR, ptR, ptR);

                    ptNumber--;
                    points[ptNumber] = new Datapoint(position, radius);

                }

            }
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

    private void drawPoints()
    {
        //Using maxNumberOfPoints instead of points.Length because the default sample data is too large to load at once
        for (int i = 0; i < maxNumberOfPoints; i++)
        {
            Instantiate(pointed);
            pointed.transform.position = points[i].pos;
            pointed.transform.localScale = points[i].sca;
        }
    }

}
