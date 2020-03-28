using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileCamera : MonoBehaviour
{
    private bool _camAvailable;
    private WebCamTexture _cameraTexture;

    #region Colors
    private Color _redColor = new Color()
    {
        r = 255,
        a = 255
    };

    private Color _greenColor = new Color()
    {
        g = 255,
        a = 255
    };

    private Color _blueColor = new Color()
    {
        b = 255,
        a = 255
    };

    private Color _whiteColor = new Color()
    {
        r = 255,
        b = 255,
        g = 255,
        a = 255
    };

    private Color _blackColor = new Color()
    {
        a = 255
    };
    #endregion

    public RawImage Background;
    public AspectRatioFitter Fit;
    public bool IsFrontFacing;
    public Text DebugText;

    private WebCamTexture GetCamera()
    {
        var devices = WebCamTexture.devices;
        foreach (var camera in WebCamTexture.devices)
        {
            if (camera.isFrontFacing == IsFrontFacing)
            {
                return new WebCamTexture(camera.name, Screen.width, Screen.height);
            }
        }
        return null;
    }

    // Use this for initialization
    void Start()
    {
        if (WebCamTexture.devices.Length == 0)
        {
            var str = "Camera is not found";
            Debug.LogError(str);
            DebugText.text = str;
            DebugText.color = _redColor;
            return;
        }

        if ((_cameraTexture = GetCamera()) == null)
        {
            _camAvailable = false;
            var str = " camera isn't available";
            if (IsFrontFacing) str = "Front" + str;
            else str = "Back" + str;
            Debug.LogError(str);
            DebugText.text = str;
            Background.texture = null;
            Background.color = _blackColor;
        }
        else
        {
            // Set the camAvailable for future purposes.
            _camAvailable = true;
            // Start the camera
            _cameraTexture.Play(); 
            DebugText.text = "";
            Background.color = _whiteColor;
            // Set the texture
            Background.texture = _cameraTexture; 
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_camAvailable)
        {
            float ratio = (float)_cameraTexture.width / (float)_cameraTexture.height;
            // Set the aspect ratio
            Fit.aspectRatio = ratio; 
            // Find if the camera is mirrored or not
            float scaleY = _cameraTexture.videoVerticallyMirrored ? -1f : 1f; 
            // Swap the mirrored camera
            Background.rectTransform.localScale = new Vector3(1f, scaleY, 1f); 

            int orient = -_cameraTexture.videoRotationAngle;
            Background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
        }
        // Background.texture = _cameraTexture;
    }

    public void SwapCamera()
    {
        IsFrontFacing = !IsFrontFacing;
        if(_camAvailable && _cameraTexture.isPlaying) _cameraTexture.Stop();
        Start();
    }

    public byte[] TakePhoto()
    {
        // NOTE - you almost certainly have to do this here:
        // yield return new WaitForEndOfFrame();
        // it's a rare case where the Unity doco is pretty clear,
        // http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
        // be sure to scroll down to the SECOND long example on that doco page 

        Texture2D photo = new Texture2D(_cameraTexture.width, _cameraTexture.height);
        photo.SetPixels(_cameraTexture.GetPixels());
        photo.Apply();
        // Encode to a PNG
        return photo.EncodeToPNG();

        // Write out the PNG. Of course you have to substitute your_path for something sensible
        // File.WriteAllBytes(your_path + "photo.png", bytes);
    }
}
