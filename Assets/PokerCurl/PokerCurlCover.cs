using UnityEngine;
using System.Collections;
using System;

namespace Rodger
{
    public struct MaskInput
    {
        public Vector2 pos;
        public float theta_pos;
        public float theta_rot;
    }

    public class PokerCurlCover : TouchPlaneBase
    {
        private Vector2 firsttouch;
        private Vector2 Voffest = new Vector2(0.5f, 0.5f);

        private MaskObject maskObj_Cover;

        private MaskObject maskObje_Num;

        private Transform displayObj;

        string guishow = "";
        void Awake()
        {
            m_camera = GameObject.Find("Camera").GetComponent<Camera>();

            maskObj_Cover = GameObject.Find("MaskObject_Cover").GetComponent<MaskObject>();
            maskObje_Num = GameObject.Find("MaskObject_Num").GetComponent<MaskObject>();

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
            string str_debug = "";
            MaskInput maskin_cover = new MaskInput();
            MaskInput maskin_num = new MaskInput();

            Vector2 pos_mouse = m_hit.textureCoord - Voffest;


            pos_mouse.x = 400 * pos_mouse.x;
            pos_mouse.y = 600 * pos_mouse.y;

            guishow = "pos is " + pos_mouse.x + " , " + pos_mouse.y + "\n";

            #region Down
            str_debug += "pos is " + pos_mouse.x + " , " + pos_mouse.y + "\n";

            Vector2 dir = -(firsttouch - pos_mouse);
            str_debug += "dir " + dir.x + " " + dir.y + "\nfirsttouch is " + firsttouch + "\npos_mouse is " + pos_mouse + "\n";

            maskin_cover.pos = Vector2.zero;
            maskin_cover.pos.x = pos_mouse.x - .5f * dir.magnitude * dir.normalized.x;
            maskin_cover.pos.y = pos_mouse.y - .5f * dir.magnitude * dir.normalized.y;
            str_debug += "maskin_cover.pos is " + maskin_cover.pos.x + "," + maskin_cover.pos.y + "\n";
            displayObj.transform.localPosition = new Vector3(maskin_cover.pos.x, maskin_cover.pos.y, 0);

            guishow += "maskin_cover.pos.x is " + maskin_cover.pos.x + ", " + maskin_cover.pos.y;

            //pos_half.x = dir.magnitude * 
            // theta between dir , v(1,0)
            maskin_cover.theta_pos = GetTheta(Vector2.right, dir);

            str_debug += "theta_pos is " + maskin_cover.theta_pos + "\n";

            //normal = new Vector2(dir.x * Mathf.Cos(-3.1416 / 2) - dir.y * Mathf.Sin (-3.1416 / 2) , dir.y * Mathf.Cos(-3.1416 / 2) + dir.x * Mathf.Sin (-3.1416 / 2));
            // To simply
            Vector2 normal = new Vector2(dir.y, -dir.x);

            str_debug += "normal " + normal.x + " , " + normal.y + "\n";
            // theta between (0,1) , nor
            maskin_cover.theta_rot = 360 - GetTheta(Vector2.up, normal);

            str_debug += "theta_rot is " + maskin_cover.theta_rot + "\n";

            #endregion
            //print(str_debug);
            if (!float.IsNaN(maskin_cover.theta_pos))
            {
                maskObj_Cover.DoMask(maskin_cover.pos, maskin_cover.theta_pos, -maskin_cover.theta_rot);

                Vector2 temp = Vector2.zero;
                temp.x = firsttouch.x / 400;
                temp.y = firsttouch.y / 600;
                Vector2 firstpos_Num = temp;
                Vector2 greenpos = - maskin_cover.pos;
                //maskin_num.pos = 
            }

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

                maskObj_Cover.ResetMask(Vector2.zero);
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