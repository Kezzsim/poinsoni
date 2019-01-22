using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;
using UnityEngine;

public class pulsewave : MonoBehaviour
{
    Vector3 grow, go;
    Vector3 originSize, originPos;
    Collider m_Collider;
    public string firstDataFileName = "testsample.json";
    public string secondDataFileName = "testsample.json";

    public GameObject pointed1;
    public GameObject pointed2;
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
        drawPoints(LoadPointsData(firstDataFileName), pointed1);
        drawPoints(LoadPointsData(secondDataFileName), pointed2);
        originPos = transform.localPosition;
        originSize = transform.localScale;
        m_Collider = GetComponent<Collider>();
        m_Collider.enabled = false;
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            //OnMouseClick and hold, box increaces in size, colliding with points and generating events
            //Eventually will also be mapped to vive controller trigger
            float t = Time.deltaTime;
            grow = transform.localScale;
            grow.x += t;
            grow.y += t;
            grow.z += t;

            go = transform.localPosition;
            go.z += t;

            transform.localScale = grow;
            transform.localPosition = go;
            m_Collider.enabled = true;
        }
        else
        {
            transform.localScale = originSize;
            transform.localPosition = originPos;
            m_Collider.enabled = false;
        }
    }

    private Datapoint[] LoadPointsData(string jsonName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, jsonName);
        Datapoint[] points;
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
                return points;
            }
        }
        else
        {
            Debug.LogError("Cannot load game data!");
            points = new Datapoint[0];
            return points;
        }
        points = new Datapoint[0];
        return points;
    }

    private void drawPoints(Datapoint[] dp, GameObject pto)
    {
        //Using maxNumberOfPoints instead of points.Length because the default sample data is too large to load at once
        if(maxNumberOfPoints != 0 && maxNumberOfPoints <= dp.Length)
        {
            for (int i = 0; i < maxNumberOfPoints; i++)
            {
                Instantiate(pto);
                pto.transform.position = dp[i].pos;
                pto.transform.localScale = dp[i].sca;
            }
        }
        else
        {
            for (int i = 0; i < dp.Length; i++)
            {
                Instantiate(pto);
                pto.transform.position = dp[i].pos;
                pto.transform.localScale = dp[i].sca;
            }
        }

    }

}
