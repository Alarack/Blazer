// Copyright (c) Rotorz Limited. All rights reserved.
// License: MIT

using System;
using UnityEngine;

namespace Rotorz.PixelCameraKit {

	/// <summary>
	/// The base class of a pixel snapper can be extended to prevent multiple pixel
	/// snapper components from being added to the same game object.
	/// </summary>
	[DisallowMultipleComponent]
	public abstract class PixelSnapperBase : MonoBehaviour {

		protected Transform _transform;
		protected SpriteRenderer _spriteRenderer;


		public float pixelsPerUnit = 16f;


		#region Inspectable Properties

		[SerializeField]
		private bool _snapSpritePivot = true;

		/// <summary>
		/// Gets or sets a value indicating whether the pivot point of an associated Unity
		/// sprite should also be taken into consideration when snapping the game object.
		/// </summary>
		/// <remarks>
		/// <para>This property is only applicable when the associated game object also
		/// has a <see cref="UnityEngine.SpriteRenderer"/> component.</para>
		/// </remarks>
		public bool SnapSpritePivot {
			get { return _snapSpritePivot; }
			set { _snapSpritePivot = value; }
		}

		#endregion

		#region Messages and Event Handlers

		/// <summary>
		/// Occurs each time the <see cref="PixelSnapperBase"/> is enabled.
		/// </summary>
		/// <remarks>
		/// <para>Always inherit the base functionality upon overriding:</para>
		/// <code language="csharp"><![CDATA[
		/// protected override void OnEnable() {
		///     base.OnEnable();
		///     // Add custom logic...
		/// }
		/// ]]></code>
		/// <code language="unityscript"><![CDATA[
		/// protected function OnEnable() {
		///     super.OnEnable();
		///     // Add custom logic...
		/// }
		/// ]]></code>
		/// </remarks>
		protected virtual void OnEnable() {
			_transform = transform;
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}

		#endregion

		private static Vector2 CalculateSpritePivotCorrectionOffset(Sprite sprite, float pixelsPerUnit) {
			if (sprite == null)
				throw new ArgumentNullException("sprite");

			Vector2 spritePixelPivot = sprite.bounds.min;
			spritePixelPivot.x = Mathf.Abs(spritePixelPivot.x);
			spritePixelPivot.y = Mathf.Abs(spritePixelPivot.y);

			Vector2 offset;
			offset.x = spritePixelPivot.x - Mathf.Round(spritePixelPivot.x) * pixelsPerUnit;
			offset.y = spritePixelPivot.y - Mathf.Round(spritePixelPivot.y) * pixelsPerUnit;
			return offset;
		}

		protected Vector3 SnapPosition() {
			Vector3 currentPosition = _transform.position;
			Vector3 offset = default(Vector3);

			// Correct offset of the current position by snapping pivot of sprite?
			if (SnapSpritePivot && _spriteRenderer != null) {
				var sprite = _spriteRenderer.sprite;
				if (sprite != null)
					offset = CalculateSpritePivotCorrectionOffset(sprite, this.pixelsPerUnit);
			}

			return this.AlignWorldPointToPixel(currentPosition - offset) + offset;
		}


		public Vector3 AlignWorldPointToPixel(Vector3 position)
		{
			position.x = Mathf.Round(this.pixelsPerUnit * position.x) / this.pixelsPerUnit;
			position.y = Mathf.Round(this.pixelsPerUnit * position.y) / this.pixelsPerUnit;
			return position;
		}

	}

}
