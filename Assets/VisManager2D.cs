using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisManager2D : MonoBehaviour
{
		public Camera camera;
	public ParticleSystem prefab;
	
	private ParticleSystem system;
	
	private int notes = 88;
	public ParticleSystem.Particle[] reference;
	public bool[] legal;
	private ParticleSystem.Particle[] hold; 
	private float width = 180;

[Range(0,1)] 
	public float clarity = 0f;
	[Range(0,3)] 
	public float particleWidth = 0.5f;
		[Range(0,1)] 
	public float particleSaturation = 0.5f;
	

	public int key = 0;
	public int[] relKey = new int[] {0, -5, 2, -3, 4, -1, 6, 1, 8, 3, 10, 5};
	public int[] innote = new int[] {0, 2, 4, 5, 7, 9, 11, 12};
	
	Color mainColor = Color.white;
	Color offColor = Color.white;
	Color sourceColor = Color.black;
	Color targetColor;
	float blendProgress = 0;
	int pastKey = 0;

	Color GetColorFromKey (int key, float sat=1f, float val=1f)
	{
		Debug.Log((float)(relKey[((key+12)%12)]+12f));
		return Color.HSVToRGB((((float)(relKey[((key+12)%12)])+12f)%12)/12f, sat, val);
	}
	bool NoteInKey(int note, int key)
	{
		for (int i = 0; i < 8; i++)
		{
			if ((key + innote[i])%12 == note%12)
				return true;
		}
		return false;
	}


//test
	private float x = 0;

    // Start is called before the first frame update
    void Start()
    {
		camera.backgroundColor = Color.black;
		
		system = Instantiate(prefab, camera.transform);
		
		reference = new ParticleSystem.Particle[notes];
		hold = new ParticleSystem.Particle[notes];
		legal = new bool [notes];
		for (int i = 0; i < notes; i++) legal[i] = false;
    }

	// Play Note
	void PlayNote(int note)
	{
		var emitParams = new ParticleSystem.EmitParams();
        
		if (NoteInKey(note, this.key))
			emitParams.startColor = mainColor;
        else
			emitParams.startColor = offColor;
         
		
		emitParams.startSize = particleWidth;
		emitParams.startLifetime = 2;
		emitParams.position = new Vector3((	note-44f)/8f, 0.0f, 0.0f);
		system.Emit(emitParams, 1);
		system.GetParticles(hold);
		for (int i = 0; i < system.particleCount; i++)
		{
			if (hold[i].remainingLifetime == 2)
			{
				reference[note] = hold[i];
				legal[note] = true;
			}
		
			//Debug.Log(reference[i].remainingLifetime);
		}
	}
	void StopNote (int note){
		if (legal[note])
		{
			reference[note].remainingLifetime = 0.5f;
			legal[note] = false;
		}
	}

    // Update is called once per frame
    void Update()
    {
		mainColor = GetColorFromKey(key, particleSaturation);
		offColor = GetColorFromKey(key+6, particleSaturation);
		
		if (pastKey != key){
			pastKey = key;
			sourceColor = camera.backgroundColor;
			targetColor = GetColorFromKey(key, 0.6f, 0.2f);
			blendProgress = 0;
		}
		if (blendProgress < 1f) blendProgress+= 0.001f;
			targetColor = GetColorFromKey(key, 0.6f, 0.15f*blendProgress);
		camera.backgroundColor = Color.LerpUnclamped(sourceColor, targetColor, blendProgress);

		x += Time.deltaTime;
        if (x > 0.3f){
			x -= 0.3f;
			PlayNote((int)(Random.value*notes));;
			StopNote((int)(Random.value*notes));
			StopNote((int)(Random.value*notes));
			StopNote((int)(Random.value*notes));
			StopNote((int)(Random.value*notes));
		}
    }
}
