using UnityEngine;
using System.Collections;

namespace Rodger
{

    public class TouchPlane : MonoBehaviour
    {

        Ray ray;

        private Camera m_camera;
        private MaskObject maskObj;

        private bool sw_drag;


        private Vector2 firsttouch;

        Vector2 pos_mouse;
        Vector2 dir;
        Vector2 normal;


        private Vector2 Voffest;

        void Awake()
        {
            m_camera = GameObject.Find("Camera").GetComponent<Camera>();

            maskObj = GameObject.Find("MaskObject").GetComponent<MaskObject>();
        }

        // Use this for initialization
        void Start()
        {

            sw_drag = false;
            Voffest = new Vector2(0.5f, 0.5f);

            Vector2 vec = new Vector2(-0.9587f,0.1711f);

            // Test Dir
			//dir = new Vector2(-1,-1);
			
			//print("dir " + dir);
			
			// dir , v(1,0)
			/*
			float theta_pos = Mathf.Rad2Deg * Mathf.Atan(dir.y/dir.x);
			if(dir.x <= 0 && dir.y >=0)
				theta_pos += 90;
			else if(dir.x <= 0 && dir.y <=0)
				theta_pos += 180;
			else if(dir.x >= 0 && dir.y <= 0)
				theta_pos += 270;
			print ("theta_pos is " + theta_pos);*/
            // Test Normal
            /*
			normal = new Vector2 (1, 1);
			float theta_rot = Mathf.Rad2Deg * Mathf.Atan(normal.x / normal.y);
			print ("theta_rot is " + theta_rot);

			if (normal.x >= 0 && normal.y <= 0)
				theta_rot += 90;
			else if (normal.x <= 0 && normal.y <= 0)
				theta_rot += 180;
			else if (normal.x <= 0 && normal.y >= 0)
				theta_rot += 270;

			normal = new Vector2 (1, -1);
			theta_rot = Mathf.Abs(Mathf.Rad2Deg * Mathf.Atan(normal.x / normal.y));
			
			if (normal.x >= 0 && normal.y <= 0)
				theta_rot += 90;
			else if (normal.x <= 0 && normal.y <= 0)
				theta_rot += 180;
			else if (normal.x <= 0 && normal.y >= 0)
				theta_rot += 270;
			print ("theta_rot is " + theta_rot);
			
			normal = new Vector2 (-1, -1);
			theta_rot = Mathf.Abs(Mathf.Rad2Deg * Mathf.Atan(normal.x / normal.y));
			
			if (normal.x >= 0 && normal.y <= 0)
				theta_rot += 90;
			else if (normal.x <= 0 && normal.y <= 0)
				theta_rot += 180;
			else if (normal.x <= 0 && normal.y >= 0)
				theta_rot += 270;
			print ("theta_rot is " + theta_rot);
			
			normal = new Vector2 (-1, 1);
			theta_rot = Mathf.Abs(Mathf.Rad2Deg * Mathf.Atan(normal.x / normal.y));
			
			if (normal.x >= 0 && normal.y <= 0)
				theta_rot += 90;
			else if (normal.x <= 0 && normal.y <= 0)
				theta_rot += 180;
			else if (normal.x <= 0 && normal.y >= 0)
				theta_rot += 270;
			print ("theta_rot is " + theta_rot);
            */
			
        }
        /*
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
        }*/

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

						if(firsttouch.x > .1f && firsttouch.x < .9f && firsttouch.y > .1f && firsttouch.y < .9f)
						{
							print ("Not Allow");
							return;
						}
						else
						{
                        	sw_drag = true;
						}
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                pos_mouse = Vector2.zero;
                sw_drag = false;
            }

            if (sw_drag)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.name == "TouchPlane")
                    {
                        string str_debug = "";
                        pos_mouse = hit.textureCoord - Voffest;
                        Vector2 pos = Vector2.zero;
                        pos.x = 400 * pos_mouse.x;
                        pos.y = 600 * pos_mouse.y;

                        str_debug += "pos is " + pos.x + " , " + pos.y + "\n";

                        dir = -(firsttouch - pos_mouse);
                        str_debug += "dir " + dir.x + " " + dir.y + "\nfirsttouch is " + firsttouch + "\npos_mouse is " + pos_mouse + "\n";
                        //print("dir " + dir.x + " " + dir.y + "\nfirsttouch is " + firsttouch + "\npos_mouse is " + pos_mouse);

                        // theta between dir , v(1,0)
                        float theta_pos = Mathf.Rad2Deg * Mathf.Atan(dir.y / dir.x);

                        theta_pos = Mathf.Abs(theta_pos);
                        str_debug += "theta_pos is " + theta_pos + "\n";
                        //print ("theta_pos is " + theta_pos);

						if(dir.x <= 0 && dir.y >=0)
							theta_pos += 90;
						else if(dir.x <= 0 && dir.y <=0)
							theta_pos += 180;
						else if(dir.x >= 0 && dir.y <= 0)
							theta_pos += 270;

                        str_debug += "theta_pos is changed to " + theta_pos + "\n";
                        //print("theta_pos is changed to " + theta_pos);
                        //normal = new Vector2(dir.x * Mathf.Cos(-3.1416 / 2) - dir.y * Mathf.Sin (-3.1416 / 2) , dir.y * Mathf.Cos(-3.1416 / 2) + dir.x * Mathf.Sin (-3.1416 / 2));
                        // To simply
                        normal = new Vector2(dir.y , -dir.x);

                        str_debug += "normal " + normal.x + " , " + normal.y + "\n";
                        // theta between (0,1) , nor
                        float theta_rot = Mathf.Rad2Deg * Mathf.Atan(normal.x / normal.y);

                        //theta_rot = Mathf.Abs(theta_rot);

                        str_debug += "theta_rot is " + theta_rot + "\n";
                        //print("theta_rot is " + theta_rot);

                        if (normal.x >= 0 && normal.y <= 0)
                            theta_rot += 90;
                        else if (normal.x <= 0 && normal.y <= 0)
                            theta_rot += 180;
                        else if (normal.x <= 0 && normal.y >= 0)
                            theta_rot += 270;


                        str_debug += "theta_rot is changed to " + theta_rot + "\n";

                        print(str_debug);
                        if (!float.IsNaN(theta_pos))
                            maskObj.DoMask(pos, theta_pos, theta_rot);
                    }
                }
            }


        }
        /*
        void OnGUI()
        {
            GUILayout.Label("First Pos " + this.firsttouch.x + " " + this.firsttouch.y);
            GUILayout.Label("hit x is " + pos_mouse.x +  " , z is " + pos_mouse.z);
        }*/
    }
}