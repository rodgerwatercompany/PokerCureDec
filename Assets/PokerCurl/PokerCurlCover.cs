using UnityEngine;
using System.Collections;
using System;

namespace Rodger
{
    public class PokerCurlCover : TouchPlaneBase
    {
        private Vector2 firsttouch;
        private Vector2 Voffest = new Vector2(0.5f, 0.5f);

        private MaskObject maskObj_Cover;

        private Transform displayObj;

        string guishow = "";
        void Awake()
        {
            m_camera = GameObject.Find("Camera").GetComponent<Camera>();

            maskObj_Cover = GameObject.Find("MaskObject_DW").GetComponent<MaskObject>();

            displayObj = GameObject.Find("Display").transform;
        }

        void OnGUI()
        {
            if(guishow != "")
                GUILayout.Label(guishow);
        }
        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            hitColliderName = "TouchPlane";

            displayObj.gameObject.SetActive(false);
        }
        protected override void OnPanelDrag()
        {
            //print("OnDrag");
            string str_debug = "";
            Vector2 pos_mouse = m_hit.textureCoord - Voffest;


            pos_mouse.x = 400 * pos_mouse.x;
            pos_mouse.y = 600 * pos_mouse.y;

            guishow = "pos is " + pos_mouse.x + " , " + pos_mouse.y + "\n";

            #region Down
            str_debug += "pos is " + pos_mouse.x + " , " + pos_mouse.y + "\n";

            Vector2 dir = -(firsttouch - pos_mouse);
            str_debug += "dir " + dir.x + " " + dir.y + "\nfirsttouch is " + firsttouch + "\npos_mouse is " + pos_mouse + "\n";

            Vector2 pos_half = Vector2.zero;
            pos_half.x = pos_mouse.x - .5f * dir.magnitude * dir.normalized.x;
            pos_half.y = pos_mouse.y - .5f * dir.magnitude * dir.normalized.y;
            str_debug += "pos_half is " + pos_half.x + "," + pos_half.y + "\n";
            displayObj.transform.localPosition = new Vector3(pos_half.x, pos_half.y, 0);


            guishow += "pos_half.x is " + pos_half.x + ", " + pos_half.y;

            //pos_half.x = dir.magnitude * 
            // theta between dir , v(1,0)
            float theta_pos = GetTheta(Vector2.right, dir);

            str_debug += "theta_pos is " + theta_pos + "\n";

            //normal = new Vector2(dir.x * Mathf.Cos(-3.1416 / 2) - dir.y * Mathf.Sin (-3.1416 / 2) , dir.y * Mathf.Cos(-3.1416 / 2) + dir.x * Mathf.Sin (-3.1416 / 2));
            // To simply
            Vector2 normal = new Vector2(dir.y, -dir.x);

            str_debug += "normal " + normal.x + " , " + normal.y + "\n";
            // theta between (0,1) , nor
            float theta_rot = 360 - GetTheta(Vector2.up, normal);

            str_debug += "theta_rot is " + theta_rot + "\n";

            print(str_debug);
            if (!float.IsNaN(theta_pos))
                maskObj_Cover.DoMask(pos_half, theta_pos, -theta_rot);
            #endregion

        }

        protected override void OnPressCollider(bool state)
        {

            if (state)
            {
                if (m_hit.textureCoord.x > .1f 
                    && m_hit.textureCoord.x < .9f 
                    && m_hit.textureCoord.y > .1f 
                    && m_hit.textureCoord.y < .9f)
                {
                    m_state_drag = false;
                    return;
                }
                else
                {

                    Vector2 hit_fixed = m_hit.textureCoord;

                    if (hit_fixed.x < .1f)
                        hit_fixed.x = 0;
                    else if (hit_fixed.x > .9f)
                        hit_fixed.x = 1;
                    else if (hit_fixed.y < .1f)
                        hit_fixed.y = 0;
                    else if (hit_fixed.y > .9f)
                        hit_fixed.y = 1;

                    firsttouch = hit_fixed - Voffest;
                    firsttouch.x = 400 * firsttouch.x;
                    firsttouch.y = 600 * firsttouch.y;
                    //print(hit_fixed.x + " " + hit_fixed.y + " , " + hit.textureCoord.x + " " + hit.textureCoord.y);

                    displayObj.gameObject.SetActive(true);
                    guishow = "";
                }
            }
            else
            {
                displayObj.gameObject.SetActive(false);
                guishow = "";

                maskObj_Cover.ResetMask();
            }
        }

        // Units in degree.
        float GetTheta(Vector2 vec_a, Vector2 vec_b)
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