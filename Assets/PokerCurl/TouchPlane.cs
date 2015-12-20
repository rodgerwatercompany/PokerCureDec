using UnityEngine;
using System.Collections;

namespace Rodger
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
			
			dir = new Vector2(-1,-1);
			
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
                        
						// theta between dir , v(1,0)
						float theta_pos = Mathf.Rad2Deg * Mathf.Atan(dir.y / dir.x);
						print ("theta_pos is " + theta_pos);

						if(dir.x <= 0 && dir.y >=0)
							theta_pos += 90;
						else if(dir.x <= 0 && dir.y <=0)
							theta_pos += 180;
						else if(dir.x >= 0 && dir.y <= 0)
							theta_pos += 270;

						//normal = new Vector2(dir.x * Mathf.Cos(-3.1416 / 2) - dir.y * Mathf.Sin (-3.1416 / 2) , dir.y * Mathf.Cos(-3.1416 / 2) + dir.x * Mathf.Sin (-3.1416 / 2));
                        // To simply
						normal = new Vector2(dir.y , -dir.x);

						// theta between (0,1) , nor
						float theta_rot = Mathf.Rad2Deg * Mathf.Atan(normal.x / normal.y);


						//normal = new Vector2(- Mathf.Abs(dir.y) / dir.magnitude, Mathf.Abs(dir.x) / dir.magnitude);

						/*
                        print("normal " + normal.x + " " + normal.y);
                        float dot = dir.x * normal.x + dir.y * normal.y;

                        print("dot is " + dot);
                        print("dir " + dir + "(" + dir.x + " , " + dir.y + ")");

						float temp = normal.y / Mathf.Abs(normal.magnitude);
                        float theta = Mathf.Acos(temp);
                        print(theta * Mathf.Rad2Deg);*/
                        //pokerobject.DoBend(this.firsttouch, new Vector2(pos_mouse.x, pos_mouse.z));
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