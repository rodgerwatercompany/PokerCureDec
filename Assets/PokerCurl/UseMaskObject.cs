using UnityEngine;
using System.Collections;


namespace Rodger{
	public class UseMaskObject : MonoBehaviour {


		void Awake(){
		}

		// Use this for initialization
		void Start () {
		
		}

		void Cooperation(float rotate)
		{
			transform.localRotation = Quaternion.Euler (0, 0, rotate);
		}
	}
}