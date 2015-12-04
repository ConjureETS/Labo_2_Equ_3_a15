using UnityEngine;
using System.Collections;

public class BallShoot: MonoBehaviour {
	
	public float speed;
	public float distanceShoot=15;
	public GameObject theParent;
	// Use this for initialization
	void Start () {
		if (theParent.transform.localScale.x<=0) {
			speed *= -1;
			transform.rotation = new Quaternion (theParent.transform.rotation.x, theParent.transform.rotation.y, -90.0f, theParent.transform.rotation.w);
		} else
			transform.rotation = new Quaternion (theParent.transform.rotation.x, theParent.transform.rotation.y,90.0f, theParent.transform.rotation.w);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate ()
	{
		transform.position = new Vector3 (transform.position.x + speed,transform.position.y,transform.position.z);
		if (transform.position.x > theParent.transform.position.x + distanceShoot || transform.position.x < theParent.transform.position.x - distanceShoot)
			DestroyObject(gameObject);
	}
	void OnTriggerEnter2D (Collider2D col) 
	{
		if(col.tag == "mechant" && theParent.tag !="mechant")
		{
			print("Yeah");
			col.gameObject.GetComponent<BadGuy>().getShot();
			Destroy (gameObject);
		}
		if(col.tag == "player" && theParent.tag !="player")
		{
			print("Yeah");
			col.gameObject.GetComponent<PlayerControl>().getHurt(25);
			Destroy (gameObject);
		}
	}
}
