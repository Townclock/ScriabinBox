using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizerManager : MonoBehaviour
{
	public int key = 0;
	private int prevKey = 0;
	public Color keyColor;
	
	[Range(0,1)] 
	public float keyConfidence = 0;
	public float[] keyConfidences = new float[12];
	private float prevKeyConfidence = 0;

	[Range(0,1)] 
	public float intensity = 0f;
	
	public ParticleSystem flame;
	
	public GameObject bulbSpawner;
	private GameObject[] bulbs;
	
	public GameObject noteBlockSpawner;
	public GameObject[] noteBlocks;
	
	public Color[] keyColors;
	private int[] relKey = new int[] {0, 7, 2, 9, 4, 11, 6, 1, 8, 3, 10, 5};
	private int[] relNote = new int[] {0, 6, 0, 6, 0, 6, 0, 6, 0, 6, 0, 6};
		
	private float x = 0;
	//AnimationCurve flameSizeCurve;
	
    // Start is called before the first frame update
    void Start()
    {
        bulbs = bulbSpawner.GetComponent<SpawnBulbs>().bulbs;
        noteBlocks = noteBlockSpawner.GetComponent<SpawnNoteBlocks>().noteBlocks;
		keyColors = bulbSpawner.GetComponent<SpawnBulbs>().bulbColors;
    }

	public void PlayNote(int note, int intensity)
	{
		int octave = (note - 36) / 12 ;
		int newNewNote = note + relNote[note%12];
		
		
		
		Debug.Log(newNewNote);
		float inten = (float)intensity/127f;
		if (newNewNote < 96 && newNewNote > 35) {
			Debug.Log(note-36);
			noteBlocks[newNewNote-36].GetComponent<NoteBlock>().on = true;	
			noteBlocks[newNewNote-36].GetComponent<NoteBlock>().intensity = inten;	
		}
		if (intensity == 0f)
			StopNote(newNewNote);
		
	}
	void StopNote (int note){
		
		if (note < 96 && note > 35) {
			noteBlocks[note-36].GetComponent<NoteBlock>().on = false;	
			noteBlocks[note-36].GetComponent<NoteBlock>().intensity = intensity;	
		}
	}
	public void SetKey (int incomingKey, float confidence){
		
		int newKey = relKey[incomingKey];
		keyConfidences[newKey] = confidence;
		int bulbIndex = newKey%12;
		
		bulbs[bulbIndex].GetComponent<LightBulb>().on = (confidence > 0.3f) ?true : false;	
		bulbs[bulbIndex].GetComponent<LightBulb>().intensity = confidence;	
	}
	
	Color GetColorFromKey (int key, float sat=1f, float val=1f)
	{
		Debug.Log((float)(relKey[((key+12)%12)]));
		float H, S, V;
		Color.RGBToHSV(keyColors[relKey[((key+12)%12)]], out H, out S, out V);
		
		Color color =  Color.HSVToRGB(H, S*sat, V);
		return color;
		
	}

	/*void UpdateParticleSystemColors (){		
        ParticleSystem.ColorOverLifetimeModule colorModule = flame.colorOverLifetime;
		Gradient ourGradient = new Gradient();
		ourGradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(keyColor, 0f), new GradientColorKey(Color.black, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1, 0.7f), new GradientAlphaKey(0, 1f) }
        );
		colorModule.color = ourGradient;
	} 
	void UpdateParticleSystemIntensity (){	
//intensity	
        ParticleSystem.MainModule main = flame.main;
		
        var emission = flame.emission;
		
		emission.rateOverTime = 10 + 10*intensity;
		//flameSizeCurve = new AnimationCurve();
        //flameSizeCurve.AddKey(intensity, 0.0f);
        //flameSizeCurve.AddKey(1.0f+intensity, 1.0f);
		main.startSize = Random.Range(intensity, intensity+1f);//new ParticleSystem.MinMaxCurve(1.0f, flameSizeCurve);
		main.startSpeed = intensity * 3;
	} */

    // Update is called once per frame
    void Update()
    {
		//UpdateParticleSystemIntensity ();
		
		if (key != prevKey || keyConfidence != prevKeyConfidence) {
			//UpdateParticleSystemColors ();
			keyColor = GetColorFromKey(key, keyConfidence);
			prevKey = key;
			prevKeyConfidence = keyConfidence;
		}
		
	
	/*
		if (x > 0.3f){
			x -= 0.3f;
			PlayNote((int)(Random.value*12), 1f);
			StopNote((int)(Random.value*12));
		}
				x += Time.deltaTime;*/
    
    }
}
