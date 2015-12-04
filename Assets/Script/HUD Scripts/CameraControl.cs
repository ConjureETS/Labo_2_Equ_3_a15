using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public PlayerControl megaman;
	// Use this for initialization
	void Start () {
		megaman = GameObject.FindGameObjectWithTag ("player").GetComponent<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update () {
		float newX = megaman.transform.position.x;
		float newY = megaman.transform.position.y;
		transform.position = new Vector3(newX, newY, transform.position.z);
	}
}
