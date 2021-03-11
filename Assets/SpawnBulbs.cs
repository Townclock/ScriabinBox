using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBulbs : MonoBehaviour
{
	
	public GameObject bulb;
	public GameObject label;
	
	public GameObject[] bulbs = new GameObject[12];
	
	public GameObject camera;
	
	public Color[] bulbColors= new Color[12];

	private string[] labels = new string[12] {"C", "G", "D", "A", "E", "B", "F#", "Db", "Ab", "Eb", "Bb", "F"};

	
    // Start is called before the first frame update
    void Start()
    {
		float radius = 2f;
		float tRadius = 3f;
		for (int i = 0;	 i < 12; i++){
			GameObject B = Instantiate(bulb, transform);
			bulbs[i] = B.transform.GetChild(0).gameObject;
			B.transform.Translate(radius*(Mathf.Sin((i/6f+0.25f)*Mathf.PI)), 0.5f, radius*(Mathf.Cos((i/6f+0.25f)*Mathf.PI)));
			bulbs[i].GetComponent<LightBulb>().lightOn = bulbColors[i];
			
		
			GameObject T = Instantiate(label, transform);
			T.transform.Translate(tRadius*(Mathf.Sin((i/6f+0.25f)*Mathf.PI)), 0f, tRadius*(Mathf.Cos((i/6f+0.25f)*Mathf.PI)));
			T.GetComponent<TextMesh>().text = labels[i];
			T.transform.LookAt(camera.transform);
			T.transform.Rotate(0, 180, 0);
		}
	}
}
    

