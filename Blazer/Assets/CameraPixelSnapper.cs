// Copyright (c) Rotorz Limited. All rights reserved.
// License: MIT

using Rotorz.PixelCameraKit;
using UnityEngine;


public sealed class CameraPixelSnapper : PixelSnapperBase
{

    private Vector3 _position;

    #region Messages and Event Handlers

    private void OnPreRender()
    {
        _position = _transform.position;

        Vector3 newPosition = SnapPosition();
        if (newPosition != _position)
        {
            bool hasTransformChanged = _transform.hasChanged;
            _transform.position = newPosition;
            _transform.hasChanged = hasTransformChanged;
        }
    }

    private void OnPostRender()
    {
        RestorePosition();
    }

    #endregion

    private void RestorePosition()
    {
        if (_transform.position != _position)
        {
            bool hasTransformChanged = _transform.hasChanged;
            _transform.position = _position;
            _transform.hasChanged = hasTransformChanged;
        }
    }

}
