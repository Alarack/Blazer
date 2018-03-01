// Copyright (c) Rotorz Limited. All rights reserved.
// License: MIT

using UnityEngine;

namespace Rotorz.PixelCameraKit {

	/// <summary>
	/// Attach this component to a game object to temporarily align its visual position
	/// with the "retro" pixel grid that is defiend by the "Pixels Per Unit" property of
	/// the main <see cref="PixelCamera"/> instance whilst rendering.
	/// </summary>
	/// <seealso cref="PixelSnapper"/>
	[AddComponentMenu("Rotorz/Pixel Camera Kit/Visual Pixel Snapper")]
#if UNITY_5 // Avoid continuously marking the scene dirty in older versions of Unity.
	[ExecuteInEditMode]
#endif
	public sealed class VisualPixelSnapper : PixelSnapperBase {

		private Vector3 _position;

		#region Messages and Event Handlers

		private void LateUpdate() {
			_position = _transform.position;
		}

		private void OnWillRenderObject() {
			Vector3 newPosition = SnapPosition();
			if (newPosition != _position) {
				bool hasTransformChanged = _transform.hasChanged;
				_transform.position = newPosition;
				_transform.hasChanged = hasTransformChanged;
			}
		}

		private void OnBecameInvisible() {
			RestorePosition();
		}

		private void OnRenderObject() {
			RestorePosition();
		}

		#endregion

		private void RestorePosition() {
			if (_transform.position != _position) {
				bool hasTransformChanged = _transform.hasChanged;
				_transform.position = _position;
				_transform.hasChanged = hasTransformChanged;
			}
		}

	}

}
