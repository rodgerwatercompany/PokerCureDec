﻿using UnityEngine;
using System.Collections;

namespace Rodger
{

    public class TouchPlane : MonoBehaviour
    {

        Ray ray;

        private Camera m_camera;
        private MaskObject maskObj_Cover;
        

        private bool sw_drag;

        private Vector2 firsttouch;

        Vector2 pos_mouse;
        Vector2 dir;
        Vector2 normal;


        private Vector2 Voffest;

        void Awake()
        {
            m_camera = GameObject.Find("Camera").GetComponent<Camera>();

            maskObj_Cover = GameObject.Find("MaskObject").GetComponent<MaskObject>();
        }

        // Use this for initialization
        void Start()
        {

            sw_drag = false;
            Voffest = new Vector2(0.5f, 0.5f);
        }

        // Update is called once per frame
        void Update()
        {

            RaycastHit hit;
            
            ray = m_camera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.name == "TouchPlane")
                    {

						if(hit.textureCoord.x > .1f && hit.textureCoord.x < .9f && hit.textureCoord.y > .1f && hit.textureCoord.y < .9f)
						{
							print ("Not Allow");
							return;
						}
						else
						{
                        	sw_drag = true;
                            Vector2 hit_fixed = hit.textureCoord;
                            
                            if (hit_fixed.x < .1f)
                                hit_fixed.x = 0;
                            else if(hit_fixed.x > .9f)
                                hit_fixed.x = 1;
                            else if (hit_fixed.y < .1f)
                                hit_fixed.y = 0;
                            else if (hit_fixed.y > .9f)
                                hit_fixed.y = 1;

                            this.firsttouch = hit_fixed - Voffest;
                            //print(hit_fixed.x + " " + hit_fixed.y + " , " + hit.textureCoord.x + " " + hit.textureCoord.y);
						}
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                pos_mouse = Vector2.zero;
                sw_drag = false;

                maskObj_Cover.ResetMask(Vector2.zero);
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

                        #region Down
                        str_debug += "pos is " + pos.x + " , " + pos.y + "\n";

                        dir = -(firsttouch - pos_mouse);
                        str_debug += "dir " + dir.x + " " + dir.y + "\nfirsttouch is " + firsttouch + "\npos_mouse is " + pos_mouse + "\n";
                        
                        // theta between dir , v(1,0)
                        float theta_pos = GetTheta(Vector2.right, dir);

                        str_debug += "theta_pos is " + theta_pos + "\n";
                        
                        //normal = new Vector2(dir.x * Mathf.Cos(-3.1416 / 2) - dir.y * Mathf.Sin (-3.1416 / 2) , dir.y * Mathf.Cos(-3.1416 / 2) + dir.x * Mathf.Sin (-3.1416 / 2));
                        // To simply
                        normal = new Vector2(dir.y , -dir.x);

                        str_debug += "normal " + normal.x + " , " + normal.y + "\n";
                        // theta between (0,1) , nor
                        float theta_rot = 360 - GetTheta(Vector2.up, normal);

                        str_debug += "theta_rot is " + theta_rot + "\n";

                        //print(str_debug);
                        if (!float.IsNaN(theta_pos))
                            maskObj_Cover.DoMask(pos, theta_pos, -theta_rot,Vector2.zero);
                        #endregion
                    }
                }
            }


        }

        // Units in degree.
        float GetTheta(Vector2 vec_a,Vector2 vec_b)
        {
            float val = Vector3.Cross(vec_a, vec_b).z;
            if (val > 0)
            {
                float temp = Vector2.Dot(vec_a, vec_b) / (vec_a.magnitude * vec_b.magnitude);
                return (Mathf.Acos(temp) * Mathf.Rad2Deg);
            }
            else if (val < 0)
            {
                float temp = Vector2.Dot(vec_a, vec_b) / (vec_a.magnitude * vec_b.magnitude);
                return 360 - (Mathf.Acos(temp) * Mathf.Rad2Deg);
            }
            else
            {
                if (vec_a == -vec_b)
                    return 180;
                else
                    return 0;
            }
        }
    }
}