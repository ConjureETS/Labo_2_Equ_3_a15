using UnityEngine;
using System.Collections;

public class BadGuy : MonoBehaviour {

	public bool isMovingEnemy;
	public bool isIntelligentEnemy;	
	public BallShoot ballPrefab;
	private int vie =100;
	private float canShoot;
	private Transform gunCheck;
	// Use this for initialization
	void Start () {

	}
	void Awake()
	{
		gunCheck = transform.Find("gunCheck");
	}
	// Update is called once per frame
	void Update () 
	{
		if(!isMovingEnemy)
		{
			if(canShoot<=0)
			{
				Instantiate(ballPrefab,gunCheck.position,Quaternion.identity);
				GameObject[] ball = GameObject.FindGameObjectsWithTag("ball");
				BallShoot ballR = ball[ball.Length-1].GetComponent<BallShoot>();
				ballR.theParent = this.gameObject;
				canShoot=0.3f;
			}	
		}
		canShoot -= Time.deltaTime;
	}
	public void getShot()
	{
		vie -= 25;
		if(vie<=0)
			Destroy(gameObject);
	}
}
