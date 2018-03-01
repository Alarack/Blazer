using UnityEngine;

public class ParticlePixelSnapper : MonoBehaviour
{
	//!TODO: Update to meet your needs.
	public const int LargestNumberOfParticlesYouWillEverHavePerSystem = 1000;


	private static readonly ParticleSystem.Particle[] s_ParticlesBuffer = new ParticleSystem.Particle[LargestNumberOfParticlesYouWillEverHavePerSystem];
	private static readonly Vector3[] s_PreviousParticlePositions = new Vector3[LargestNumberOfParticlesYouWillEverHavePerSystem];
	private static int s_ParticlesBufferSize;


	[SerializeField]
	private float pixelsPerUnit = 16f;


	new private ParticleSystem particleSystem;



	public float PixelsPerUnit {
		get { return this.pixelsPerUnit; }
		set { this.pixelsPerUnit = value; }
	}



	private void Awake()
	{
		this.particleSystem = this.GetComponent<ParticleSystem>();
	}

	private void OnWillRenderObject()
	{
		this.SnapPositions();
	}

	private void OnBecameInvisible()
	{
		this.RestorePositions();
	}

	private void OnRenderObject()
	{
		this.RestorePositions();
	}



	private void SnapPositions()
	{
		s_ParticlesBufferSize = this.particleSystem.GetParticles(s_ParticlesBuffer);
		for (int i = 0; i < s_ParticlesBufferSize; ++i) {
			Vector3 position = s_ParticlesBuffer[i].position;
			s_PreviousParticlePositions[i] = position;
			s_ParticlesBuffer[i].position = AlignWorldPointToPixel(position);
		}
		this.particleSystem.SetParticles(s_ParticlesBuffer, s_ParticlesBufferSize);
	}

	private void RestorePositions()
	{
		for (int i = 0; i < s_ParticlesBufferSize; ++i) {
			s_ParticlesBuffer[i].position = s_PreviousParticlePositions[i];
		}
		this.particleSystem.SetParticles(s_ParticlesBuffer, s_ParticlesBufferSize);
	}


	public Vector3 AlignWorldPointToPixel(Vector3 position)
	{
		position.x = Mathf.Round(this.pixelsPerUnit * position.x) / this.pixelsPerUnit;
		position.y = Mathf.Round(this.pixelsPerUnit * position.y) / this.pixelsPerUnit;
		return position;
	}
}
