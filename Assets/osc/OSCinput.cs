using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCinput : MonoBehaviour
{
    public OSC osc;

	public VisualizerManager visMan;
	//public TwoDeeVisManager twoDeeVisMan;


    // Start is called before the first frame update
    void Start()
    {
		if (transform.GetComponent<VisualizerManager>() != null)
			visMan = transform.GetComponent<VisualizerManager>();
		//if (transform.GetComponent<TwoDeeVisManager>() != null)
		//	twoDeeVisMan = transform.GetComponent<TwoDeeVisManager>();
		
        osc.SetAddressHandler("/midi" , OnReceiveMidi);
        osc.SetAddressHandler("/key" , OnReceiveKey);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	//["midi", "note<1-88>", "velocity<0.0-1.0>>"]
	//["key", "key<number 1-12>", "confidence<0.0-1.0>>"]
	//["chord", "majorNess<0.0-1.0>>", "minorNess<0.0-1.0>>", "augmentedNess<0.0-1.0>>", "diminishedNess<0.0-1.0>>"]

	void OnReceiveMidi(OscMessage message)
	{
		ArrayList values = message.values;
		Debug.Log(message.values[1]);
		visMan.PlayNote((int)values[0], (int)values[1]);
		
		
	}

    void OnReceiveKey(OscMessage message)
    {
		ArrayList values = message.values;
		visMan.SetKey((int)values[0], (float)values[1]);

    }
}
