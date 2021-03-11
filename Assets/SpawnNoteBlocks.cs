using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNoteBlocks : MonoBehaviour
{
	
	public GameObject noteBlock;
	[Range(0,5)] 
	public int octaves = 5;
	
	
	
	public GameObject[] noteBlocks;
	private float radius;
	
	public Color[] noteColors= new Color[12];

	
    // Start is called before the first frame update
    void Start()
    {
		noteBlocks = new GameObject[12*octaves];
		
		radius = 2f;
		for (int j = 0; j < octaves; j++)
		for (int i = 0;	 i < 12; i++){
			radius = 3f + j*0.3f;
			GameObject B = Instantiate(noteBlock, transform);
			noteBlocks[i+(12*j)] = B;
			B.transform.Translate(radius*(Mathf.Sin((i/6f+0.25f)*Mathf.PI)), 0.5f * j, radius*(Mathf.Cos((i/6f+0.25f)*Mathf.PI)));
			noteBlocks[i+j*(12)].GetComponent<NoteBlock>().lightOn = noteColors[i];
		}
	}
}
    

