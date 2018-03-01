// Copyright (c) Rotorz Limited. All rights reserved.
// License: MIT

using UnityEngine;

// No undo for Unity 4.x because there is no editor-runtime assembly.
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Rotorz.PixelCameraKit {

	/// <summary>
	/// Attach this component to a game object to align its position with the "retro"
	/// pixel grid that is defined by the "Pixels Per Unit" property of the main
	/// <see cref="PixelCamera"/> instance.
	/// </summary>
	/// <remarks>
	/// <para>If the game object appears to be out of alignment when using this component
	/// then you may want to take a look at the placement of the game object's origin.</para>
	/// </remarks>
	/// <seealso cref="VisualPixelSnapper"/>
	[AddComponentMenu("Rotorz/Pixel Camera Kit/Pixel Snapper")]
	[ExecuteInEditMode]
	public sealed class PixelSnapper : PixelSnapperBase {

		#region Inspectable Properties

		[SerializeField]
		private bool _editModeOnly = false;
		[SerializeField]
		private bool _everyFrame = false;
		[SerializeField]
		private bool _destroyComponentAtStart = false;

		/// <summary>
		/// Gets or sets whether the <see cref="PixelSnapper"/> component should only be
		/// operational in edit mode.
		/// </summary>
		public bool EditModeOnly {
			get { return _editModeOnly; }
			set { _editModeOnly = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="PixelSnapper"/> component
		/// should be executed for each frame of the game's execution.
		/// </summary>
		/// <remarks>
		/// <para>If this property is not set then the <see cref="PixelSnapper"/> component
		/// will be disabled each time it is executed. The component can then be re-enabled
		/// by other scripts if desired.</para>
		/// </remarks>
		public bool EveryFrame {
			get { return _everyFrame; }
			set { _everyFrame = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="PixelSnapper"/> component
		/// should be destroyed at start after object position has been adjusted.
		/// </summary>
		public bool DestroyComponentAtStart {
			get { return _destroyComponentAtStart; }
			set { _destroyComponentAtStart = value; }
		}

		#endregion

		#region Messages and Event Handlers

		/// <inheritdoc/>
		protected override void OnEnable() {
			base.OnEnable();

			if (Application.isPlaying && EditModeOnly)
				enabled = false;
			else
				Update();

			if (DestroyComponentAtStart && Application.isPlaying)
				Destroy(this);
		}

		private void Update() {
			SnapNow();

			if (!EveryFrame && Application.isPlaying)
				enabled = false;
		}

		#endregion

		/// <summary>
		/// Applies the pixel snapping logic.
		/// </summary>
		public void SnapNow() {
			Vector3 newPosition = SnapPosition();
			if (newPosition != _transform.position) {
#if UNITY_EDITOR
				if (!Application.isPlaying)
					Undo.RecordObject(_transform, "Snap to Pixels");
#endif
				_transform.position = newPosition;
			}
		}

	}

}
