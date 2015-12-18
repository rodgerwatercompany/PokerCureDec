using UnityEngine;
using System.Collections;

namespace YCurl
{
    public class TouchPlane : MonoBehaviour
    {

        Ray ray;


        private Camera m_camera;

        private bool sw_drag;


        private Vector2 firsttouch;

        Vector2 pos_mouse;
        Vector2 dir;
        Vector2 normal;

        private Vector2 Voffest;

        void Awake()
        {
            m_camera = GameObject.Find("Camera").GetComponent<Camera>();
        }
        // Use this for initialization
        void Start()
        {
            sw_drag = false;
            Voffest = new Vector2(0.5f, 0.5f);
            /*
            dir = new Vector2(1, 1);
            print(dir.magnitude);
            normal = new Vector2(-dir.y / dir.magnitude, dir.x / dir.magnitude);

            print("normal " + normal.x + " " + normal.y);
            float dot = dir.x * normal.x + dir.y * normal.y;

            print("dot is " + dot);
            print("dir " + dir + "(" + dir.x + " , " + dir.y + ")");*/
        }

        void aUpdate()
        {

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                //ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                ray = m_camera.ScreenPointToRay(Input.mousePosition);
                print("ray " + ray);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    string str = "";
                    str += "bs is " + hit.barycentricCoordinate.x + "," + hit.barycentricCoordinate.y + "\n";
                    //str += "collider is " + hit..c + "," + hit.collider.y + "\n";
                    str += "point is " + hit.point.x + "," + hit.point.y + "\n";
                    str += "textureCoord is " + hit.textureCoord.x + "," + hit.textureCoord.y + "\n";
                    str += "textureCoord2 is " + hit.textureCoord2.x + "," + hit.textureCoord2.y + "\n";
                    str += "transform is " + hit.transform.localPosition.x + "," + hit.transform.localPosition.y + "\n";
                    str += "triangleIndex is " + hit.triangleIndex + "\n";
                    print(str);
                }
            }        
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            ray = m_camera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.name == "TouchPlane")
                    {
                        this.firsttouch = new Vector2(hit.textureCoord.x, hit.textureCoord.y) - Voffest;
                        print(firsttouch.x + " " + firsttouch.y);
                        sw_drag = true;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                pos_mouse = Vector2.zero;
                sw_drag = false;

                //pokerobject.Release();

            }

            if (sw_drag)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.name == "TouchPlane")
                    {
                        pos_mouse = hit.textureCoord - Voffest;
                        dir = -(firsttouch - pos_mouse);

                        print("dir " + dir);
                        
                        normal = new Vector2(- Mathf.Abs(dir.y) / dir.magnitude, Mathf.Abs(dir.x) / dir.magnitude);

                        print("normal " + normal.x + " " + normal.y);
                        float dot = dir.x * normal.x + dir.y * normal.y;

                        print("dot is " + dot);
                        print("dir " + dir + "(" + dir.x + " , " + dir.y + ")");

                        float temp = normal.y / normal.magnitude;
                        float theta = Mathf.Acos(temp);
                        print(theta * Mathf.Rad2Deg);
                        //pokerobject.DoBend(this.firsttouch, new Vector2(pos_mouse.x, pos_mouse.z));
                    }
                }
            }


        }

        void DoBend()
        {

            // let 0.5,0.5 to center poin.
        }
        /*
        void OnGUI()
        {
            GUILayout.Label("First Pos " + this.firsttouch.x + " " + this.firsttouch.y);
            GUILayout.Label("hit x is " + pos_mouse.x +  " , z is " + pos_mouse.z);
        }*/
    }
}