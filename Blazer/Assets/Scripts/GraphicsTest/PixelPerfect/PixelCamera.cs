using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCamera : MonoBehaviour
{
	public enum CameraMethod
	{
		None,
		FitInside,
		Crop,
	}



	[SerializeField]
	private float pixelsPerUnit = 1f;
	[SerializeField]
	private float zoomFactor = 1f;
	[SerializeField]
	private float targetResolutionY = 120f;
	[SerializeField]
	private CameraMethod method = CameraMethod.FitInside;
	[SerializeField]
	private bool shouldDealWithUnevenResolutions = true;


	new private Camera camera;


	public float PixelsPerUnit {
		get { return this.pixelsPerUnit; }
		set { this.pixelsPerUnit = value; }
	}

	public float ZoomFactor {
		get { return this.zoomFactor; }
		set { this.zoomFactor = value; }
	}

	public float TargetResolutionY {
		get { return this.targetResolutionY; }
		set { this.targetResolutionY = value; }
	}

	public CameraMethod Method {
		get { return this.method; }
		set { this.method = value; }
	}

	public bool ShouldDealWithUnevenResolutions {
		get { return this.shouldDealWithUnevenResolutions; }
		set { this.shouldDealWithUnevenResolutions = value; }
	}


	private void Awake()
	{
		camera = GetComponent<Camera>();
	}

	private void Update()
	{
		MakePixelPerfect();

		if (shouldDealWithUnevenResolutions) {
			DealWithUnevenResolution();
		}
	}

    private void MakePixelPerfect()
	{
		float viewportPixelsPerUnit = pixelsPerUnit * zoomFactor;
		float actualViewportResolutionY = Screen.height;

		switch (method) {
			default:
			case CameraMethod.None:
				break;

			case CameraMethod.FitInside: {
					float coverage = Mathf.Floor(actualViewportResolutionY / targetResolutionY);
					viewportPixelsPerUnit *= Mathf.Max(1f, coverage);
				}
				break;

			case CameraMethod.Crop: {
					float coverage = Mathf.Ceil(actualViewportResolutionY / targetResolutionY);
					viewportPixelsPerUnit *= Mathf.Max(1f, coverage);
				}
				break;
		}

		camera.orthographicSize = actualViewportResolutionY / (2f * viewportPixelsPerUnit);
	}

	private void DealWithUnevenResolution()
	{
		Rect pixelRect = camera.pixelRect;
		pixelRect.width = Screen.width;
		pixelRect.height = Screen.height;

		if ((int)(pixelRect.width % 2) == 1) {
			--pixelRect.width;
		}
		if ((int)(pixelRect.height % 2) == 1) {
			--pixelRect.height;
		}

		camera.pixelRect = pixelRect;
	}
}
