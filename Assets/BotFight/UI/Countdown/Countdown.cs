using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour {

	private Text _Countdown;

	// Use this for initialization
	void Start () {
	
		_Countdown = GetComponent<Text>();

	}

	public void DestorySelf()
	{
		Destroy(this.gameObject, 0.2f);
	}

	/// <summary>
	/// Call start on all bots
	/// </summary>
	public void callStart()
	{
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
		for (int i = 0; i < allObjects.Length; i++) 
		{	
			 allObjects[i].BroadcastMessage("botStart", SendMessageOptions.DontRequireReceiver);
		}
	}


}
