using UnityEngine;
using System.Collections;
using System;

namespace Rodger
{
    public class CoverTouchPanel : TouchPlaneBase
    {
        void Awake()
        {
            m_camera = GameObject.Find("Camera").GetComponent<Camera>();

        }
        // Use this for initialization
        protected override void Start()
        {
            base.Start();
        }
        protected override void OnPanelDrag()
        {
        }

        protected override void OnPressCollider(bool state)
        {
            // Press
            if(state)
            {

            }
            // Release
            else
            {

            }
        }
        
    }
}