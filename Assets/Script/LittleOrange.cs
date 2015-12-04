using UnityEngine;
using System.Collections;

public class LittleOrange : MonoBehaviour {

	public GameObject plateforme;
	public float speed=0.0001f;
	private int sens=1;
	private Animator anim;	
	private bool shouldWalk;
	private float positionMax;
	private float positionMin;
	private float newDescision=0.5f;
	private int vie =100;
	// Use this for initialization
	void Start () {
	
	}
	void Awake()
	{
		anim = GetComponent<Animator>();
		positionMax = plateforme.transform.position.x + plateforme.GetComponent<BoxCollider2D>().size.x;
		positionMin = plateforme.transform.position.x - plateforme.GetComponent<BoxCollider2D>().size.x;
	}
	// Update is called once per frame
	void FixedUpdate () {
		newDescision -= Time.deltaTime;
		if(newDescision<=0)
		{
			if(Random.value<=0.5)
			{
				shouldWalk=true; 
				if(Random.value<=0.3)
					flip();
			}
			else 
				shouldWalk=false;

			anim.SetBool("ShouldWalk",shouldWalk);
			newDescision = Random.Range(0.6f,5f);
		}
		if(shouldWalk && anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
		{
			transform.position = new Vector3 (transform.position.x - (speed*sens),transform.position.y,transform.position.z);
			if((transform.position.x <= positionMin && sens>0 )|| (transform.position.x >= positionMax && sens<0))
			{
				transform.localScale=new Vector2(transform.localScale.x *-1,transform.localScale.y);
				flip();
			}
		}
	}
	private void flip()
	{
		sens *= -1;
	}
	public void getShot()
	{
		vie -= 25;
		if(vie<=0)
			Destroy(gameObject);
	}
}
