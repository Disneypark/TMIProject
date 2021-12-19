using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SampleAudioVisualizer : MonoBehaviour {

	//	 Reference to the audio source
	AudioSource aSource;

	//	 No of samples and bands
	private int nSamples = 512;
	private int nBands = 8;

	//Array reference to the arrays
	// sampleArray[] has reference to the 512 samples,	bands[] refer to the 8 bands,	bandBuffer[] has refernce to the buffered bands
	private float[] sampleArrayLeft, sampleArrayRight;
	private float[] bands, bandBuffer, bufferDecrease;

	//Array references to audioBand (Normalised bands)
	private float[] bandHighest;
	public float profilevalue;

//	[HideInInspector]
	public float[] audioBand,audioBandBuffer;

	// Final amplitudes
//	[HideInInspector]
	public float amplitude, amplitudeBuffer;
	private float amplitudeHighest;

	//
	public List<SampleNode> sampleNodes;
	
	void Start () {
		#region initialising arrays
		sampleArrayLeft = new float[nSamples];
		sampleArrayRight = new float[nSamples];

		bands = new float[nBands];
		bandBuffer = new float[nBands];

		bufferDecrease = new float[nBands];

		bandHighest = new float[nBands];
		audioBand = new float[nBands];
		audioBandBuffer = new float[nBands];
		#endregion

		sampleNodes = GetComponentsInChildren<SampleNode>().ToList();

		for (int i = 0; i < sampleNodes.Count; i++)
		{
			int num = i + 1;
			sampleNodes[i].gameObject.name = "SampleNode" + num;
		}
		
		aSource = GetComponent<AudioSource> ();
		AudioProfile (profilevalue);

		aSource.Play ();

	}

	void AudioProfile(float highest){
		for (int i = 0; i < nBands; i++) {
			bandHighest [i] = highest;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		aSource.GetSpectrumData (sampleArrayLeft, 0, FFTWindow.BlackmanHarris);
		aSource.GetSpectrumData (sampleArrayRight, 1, FFTWindow.BlackmanHarris);
		FrequencyDivider ();
		CreateBandBuffer ();
		CreateAudioBands ();

		int index = -1;
		float min = float.MinValue;
		if (aSource.isPlaying)
		{
			for (int i = 0; i < audioBand.Length; i++)
			{
				float value = audioBand[i];
				if (value > min)
				{
					min = value;
					index = i;
				}
			}
				
			float beatPower = audioBand[index];
			float beatPowerClamp = Mathf.Clamp01(beatPower);
			Vector3 target = new Vector3(1 + beatPowerClamp, 1 + beatPowerClamp, 1 + beatPowerClamp);
			float targetGlow = beatPowerClamp * 255.0f;
			GameObject beatSampleNode = sampleNodes[index].gameObject;
			beatSampleNode.GetComponent<SampleNode>().SendData(beatSampleNode.name, target, targetGlow);
		}
	}
	void CreateAudioBands()
	{
		// Getting the highest band value
		for (int i = 0; i < nBands; i++) {
			if(bands[i] > bandHighest[i])
			{
				bandHighest [i] = bands [i];
			}
		}

		for (int i = 0; i < nBands; i++) {
			audioBand [i] = Mathf.Abs(bands [i] / bandHighest [i]);
			audioBandBuffer [i] = Mathf.Abs(bandBuffer [i] / bandHighest [i]);
		}
	}
		
	void CreateBandBuffer ()
	{
		for (int i = 0; i < nBands; i++) {

			if(bands[i] > bandBuffer [i])
			{
				bandBuffer [i] = bands [i];
				bufferDecrease [i] = 0.005f;
			} 
			else if(bands[i] < bandBuffer [i])
			{
				bandBuffer [i] -= bufferDecrease [i];
				bufferDecrease [i] *= 1.05f; // 20% increase
			}
		}
	}

	void FrequencyDivider(){
		int sampleCount = 0;
		int currentCount = 0;
		float average = 0;
		for (int i = 0; i < nBands; i++) {
			sampleCount = (int)Mathf.Pow(2, i + 1);

			for (int j = 0; j < sampleCount; j++) {
				average += sampleArrayLeft [currentCount] + sampleArrayRight[currentCount];
				currentCount++;
			}
			average = average / currentCount;
			bands [i] = average;
		}
	}

	void GetAmplitude()
	{
		float currentAmplitude = 0;
		float currentAmpBuffer = 0;
		for (int i = 0; i < nBands; i++) {
			currentAmplitude += audioBand [i];
			currentAmpBuffer += audioBandBuffer [i];
		}

		if(currentAmplitude > amplitudeHighest)
		{
			amplitudeHighest = currentAmplitude;
		}

		amplitude = (currentAmplitude / amplitudeHighest);
		amplitudeBuffer = (currentAmpBuffer / amplitudeHighest);
	}
}