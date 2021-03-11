using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulb : MonoBehaviour
{
	
	public bool on = false;
	public bool prev = false;
	
	public Material materialOn;
	public Color lightOn;	
	public Material materialOff;
	public Color lightOff;
	
	public Material originalMaterialOn;
	public Material originalMaterialOff;

	public float intensity = 0.5f;
	
	public Light light;
	public ParticleSystem ps;
	private ParticleSystem.EmitParams emitParams;
    // Start is called before the first frame update
    void Start()
    {
		Shader shader = gameObject.GetComponent<Renderer>().material.shader;
        materialOn = new Material(shader);
		materialOn.CopyPropertiesFromMaterial( originalMaterialOn);
		materialOn.SetColor("_EmissionColor", lightOn);
        materialOff = new Material(shader);
		materialOff.CopyPropertiesFromMaterial( originalMaterialOff);
		materialOff.SetColor("_Albedo", lightOn);
		materialOff.SetColor("_Color", lightOn);
    
		light.color = lightOn;
		
		
		emitParams = new ParticleSystem.EmitParams();
		
		emitParams.startSize = 0.5f;
	}

    // Update is called once per frame
    void Update()
    {
		light.intensity = 1f + (intensity-0.3f)*15f;
		if (on && !prev){
			
			prev = true;
			
			emitParams.startColor = new Color(lightOn.r, lightOn.g, lightOn.b);
			emitParams.startSize = Random.Range(0.3f + (intensity/10f), 0.5f+intensity/(10f)	);
			
			ps.Emit(emitParams, (int )(20 + 20 * intensity/(10f)) );
			
			gameObject.GetComponent<Renderer>().material = materialOn;
			light.enabled = true;

		}
		else if (!on && prev){
			prev = false;
			gameObject.GetComponent<Renderer>().material = materialOff;
			light.enabled = false;
		}

    }
}
