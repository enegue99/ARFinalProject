using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BezierSolution;
using System;

namespace Control
{
    public class Controller : MonoBehaviour
    {
        public GameObject obj1;
        public GameObject obj2;

        public bool obj1Exists;
        public bool obj2Exists;

        public List<BezierSpline> obj1FieldLines;
        public List<BezierSpline> obj2FieldLines;

        public float obj1Magnitude;
        public float obj2Magnitude;

        public List<Vector3> startingPointOffset;

        const float radius = 0.03225f;
        const float stepLength = 0.0007f;

        const int numPoints = 30;

        public UnityEngine.UI.Slider obj1slider;
        public UnityEngine.UI.Slider obj2slider;

        public GameObject allSplines1;
        public GameObject allSplines2;


        private Vector3 pos1;
        private Quaternion rot1;

        public UnityEngine.UI.Text obj1MagDisp;
        public UnityEngine.UI.Text obj2MagDisp;

        //public GameObject sliderobj;


        // Start is called before the first frame update
        void Start()
        {
            this.startingPointOffset = new List<Vector3>();
            this.obj1FieldLines = new List<BezierSpline>();
            this.obj2FieldLines = new List<BezierSpline>();

            this.startingPointOffset.Add(new Vector3(0, radius, 0));
            this.startingPointOffset.Add(new Vector3(0, -radius, 0));
            for (int y = 30; y <= 150; y += 30)
            {
                for (int z = 0; z <= 330; z += 30)
                {
                    Vector3 offset = new Vector3(radius * (float)Math.Sin(y) * (float)Math.Sin(z), radius * (float)Math.Cos(y), radius * (float)Math.Sin(y) * (float)Math.Cos(z));
                    this.startingPointOffset.Add(offset);
                }

            }

            this.obj1Exists = false;
            this.obj2Exists = false;
            this.obj1Magnitude = 5.0f;
            this.obj2Magnitude = 5.0f;

           
            allSplines1.gameObject.SetActive(false);
            allSplines2.gameObject.SetActive(false);

            foreach (Vector3 offset in startingPointOffset)
            {
                //GameObject splineholer = new GameObject();
                //allSplines1.AddComponent<>(splineholer);

                GameObject splineholder1 = new GameObject();
                splineholder1.gameObject.SetActive(true);
                BezierSpline spline1 = splineholder1.AddComponent<BezierSpline>();
                spline1.Initialize(numPoints);
                spline1.gameObject.SetActive(true);
                this.obj1FieldLines.Add(spline1);
                splineholder1.transform.SetParent(allSplines1.transform);

                GameObject splineholder2 = new GameObject();
                splineholder2.gameObject.SetActive(true);
                BezierSpline spline2 = splineholder2.AddComponent<BezierSpline>();
                spline2.Initialize(numPoints);
                spline2.gameObject.SetActive(true);
                this.obj2FieldLines.Add(spline2);
                splineholder2.transform.SetParent(allSplines2.transform);
            }

            //slider = sliderobj.GetComponent<UnityEngine.UI.Slider>();

        }

        void resetFieldLines()
        {
            foreach (BezierSpline spline in obj1FieldLines)
            {
                for (int j = 0; j < numPoints; j++)
                {
                    spline[j].position = new Vector3(0f, 0f, 0f);
                }
            }
            foreach (BezierSpline spline in obj2FieldLines)
            {
                for (int j = 0; j < numPoints; j++)
                {
                    spline[j].position = new Vector3(0f, 0f, 0f);
                }
            }
        }

        private void OnEnable()
        {
            for (int i = 0; i < allSplines1.transform.childCount; i++)
            {
                allSplines1.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        // Update is called once per frame
        void Update()
        {
            //display magnitude for both objects
            if(obj1Magnitude == 0f || obj2Magnitude == 0f)
            {

            }
            obj1MagDisp.text = "Object 1 Magnitude: " + obj1Magnitude.ToString();
            obj2MagDisp.text = "Object 2 Magnitude: " + obj2Magnitude.ToString();


            resetFieldLines();
            allSplines1.gameObject.SetActive(false);
            allSplines2.gameObject.SetActive(false);

            pos1 = obj1.transform.position;
            rot1 = obj1.transform.rotation;

            if (obj1Exists && !obj2Exists)
            {
                for (int i = 0; i < this.obj1FieldLines.Count; i++)
                {
                    BezierSpline spline = this.obj1FieldLines[i];
                    spline[0].position = pos1 + (rot1 * this.startingPointOffset[i]);
                    for (int j = 0; j < numPoints-1; j++)
                    {
                        Vector3 dir = getDirection(spline[j].position);
                      

                        if (this.obj1Magnitude > 0)
                        {
                           spline[j + 1].position = spline[j].position + dir * Controller.stepLength;
                           
                        }
                        else
                        {
                            spline[j + 1].position = spline[j].position - dir * Controller.stepLength;
                            
                        }
                    }

                    spline.ConstructLinearPath();
                }

                allSplines1.gameObject.SetActive(true);
                allSplines2.gameObject.SetActive(false);
            }
            else if (!obj1Exists && obj2Exists)
            {
                for (int i = 0; i < this.obj2FieldLines.Count; i++)
                {
                    BezierSpline spline = this.obj2FieldLines[i];
                    spline[0].position = this.obj2.transform.position + (this.obj2.transform.rotation * this.startingPointOffset[i]);
                    for (int j = 0; j < numPoints - 1; j++)
                    {
                        Vector3 dir = getDirection(spline[j].position);
                        if (this.obj2Magnitude > 0)
                        {
                            spline[j + 1].position = spline[j].position + dir * Controller.stepLength;
                        }
                        else
                        {
                            spline[j + 1].position = spline[j].position - dir * Controller.stepLength;
                        }
                    }


                    spline.ConstructLinearPath();

                }

                allSplines1.gameObject.SetActive(false);
                allSplines2.gameObject.SetActive(true);
            }
            else if (obj1Exists && obj2Exists)
            {
                for (int i = 0; i < this.obj1FieldLines.Count; i++)
                {
                    BezierSpline spline = this.obj1FieldLines[i];
                    spline[0].position = this.obj1.transform.position + (this.obj1.transform.rotation * this.startingPointOffset[i]);
                    for (int j = 0; j < numPoints - 1; j++)
                    {
                        Vector3 dir = getDirection(spline[j].position);
                        if (this.obj1Magnitude > 0)
                        {
                            spline[j + 1].position = spline[j].position + dir * Controller.stepLength;
                        }
                        else
                        {
                            spline[j + 1].position = spline[j].position - dir * Controller.stepLength;
                        }
                    }

                    spline.AutoConstructSpline2();

                }

                for (int i = 0; i < this.obj2FieldLines.Count; i++)
                {
                    BezierSpline spline = this.obj2FieldLines[i];
                    spline[0].position = this.obj2.transform.position + (this.obj2.transform.rotation * this.startingPointOffset[i]);
                    for (int j = 0; j < numPoints - 1; j++)
                    {
                        Vector3 dir = getDirection(spline[j].position);
                        if (this.obj2Magnitude > 0)
                        {
                            spline[j + 1].position = spline[j].position + dir * Controller.stepLength;
                        }
                        else
                        {
                            spline[j + 1].position = spline[j].position - dir * Controller.stepLength;
                        }
                    }

                    spline.AutoConstructSpline2();
                }

                allSplines1.gameObject.SetActive(true);
                allSplines2.gameObject.SetActive(true);
            }
            else
            {
                allSplines1.gameObject.SetActive(false);
                allSplines2.gameObject.SetActive(false);
  
            }

        }

        public void obj1Found()
        {
            obj1Exists = true;
        }

        public void obj1Lost()
        {
            obj1Exists = false;
        }

        public void obj2Found()
        {
            obj2Exists = true;
        }

        public void obj2Lost()
        {
            obj2Exists = false;
        }

        public void slider1Changed()
        {
            obj1Magnitude = obj1slider.value;

        }

        public void slider2Changed()
        {
            obj2Magnitude = obj2slider.value;
        }

        private Vector3 getDirection(Vector3 p)
        {
            Vector3 result = new Vector3(0.0f, 0.0f, 0.0f);
            if (obj1Exists && obj2Exists)
            {
                float d1 = (p - obj1.transform.position).magnitude;
                result = (obj1Magnitude / (d1 * d1)) * (p - obj1.transform.position).normalized;
                float d2 = (p - obj2.transform.position).magnitude;
                result = result + (obj2Magnitude / (d2 * d2)) * (p - obj2.transform.position).normalized;
            }
            else if (obj1Exists && !obj2Exists)
            {
                float d1 = (p - pos1).magnitude;
                result = (obj1Magnitude / (d1 * d1)) * (p - pos1).normalized;
            }
            else if (!obj1Exists && obj2Exists)
            {
                float d2 = (p - obj2.transform.position).magnitude;
                result = (obj2Magnitude / (d2 * d2)) * (p - obj2.transform.position).normalized;
            }

            return result.normalized * 10;
        }


    }
}





