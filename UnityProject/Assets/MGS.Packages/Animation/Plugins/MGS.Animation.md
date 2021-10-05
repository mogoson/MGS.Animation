[TOC]

# MGS.Animation.dll

## Summary

- Unity plugin for make animation in scene.

## Platform

- Windows

## Environment

- .Net Framework 3.5 or above.
- Unity 5.0 or above.

## Dependence

- [MGS.Curve.dll](.\MGS.Curve.md)
- [MGS.MonoCurve.dll](.\MGS.MonoCurve.md)
- System.dll
- UnityEngine.dll
- UnityEngine.UI.dll

## Implemented

```C#
public abstract class MonoAnimation : MonoBehaviour, IAnimation{}
public class MonoCurveAnimation : MonoAnimation{}

public abstract class FrameAnimation : MonoAnimation{}
public abstract class SpriteFrameAnimation : FrameAnimation{}
public abstract class TextureFrameAnimation : FrameAnimation{}
public class ImageAnimation : SpriteFrameAnimation{}
public class RawImageAnimation : TextureFrameAnimation{}
public class SpriteAnimation : SpriteFrameAnimation{}
public class UVFrameAnimation : FrameAnimation{}
public class UVAnimation : MonoAnimation
```

## Technology

### Graph

```C#
//Get frames from image file.
var image = Image.FromFile(file);
var dimension = new FrameDimension(image.FrameDimensionsList[0]);
var framesCount = image.GetFrameCount(dimension);
var frames = new Bitmap[framesCount];
for (int i = 0; i < framesCount; i++)
{
    var bitmap = new Bitmap(image.Width, image.Height);
    var graphics = Graphics.FromImage(bitmap);

    image.SelectActiveFrame(dimension, i);
    graphics.DrawImage(image, Point.Empty);
    frames[i] = bitmap;
}

//Get buffer data from bitmap frame.
using (var stream = new MemoryStream())
{
    bitmap.Save(stream, format);
    return stream.GetBuffer();
}
```

### Loop Mode

```C#
timer += speed * Time.deltaTime;
if (timer < 0 || timer > curve.Length)
{
    switch (loopMode)
    {
        case LoopMode.Once:
            Stop();
            return;

        case LoopMode.Loop:
            timer -= curve.Length * TimerDirection;
            break;

        case LoopMode.PingPong:
            speed = -speed;
            timer = Mathf.Clamp(timer, 0, curve.Length);
            break;
    }
}
```

### Look Up

```C#
var worldUp = Vector3.up;
switch (upMode)
{
    case UpMode.TransformUp:
        worldUp = transform.up;
        break;

    case UpMode.ReferenceUp:
        if (reference)
        {
            worldUp = reference.up;
        }
        break;

    case UpMode.ReferenceUpAsNormal:
        if (reference)
        {
            var tangent = (nextPos - timePos).normalized;
            worldUp = Vector3.Cross(tangent, reference.up);
        }
        break;
}
```

## Usage

### Curve Animation

- Attach mono curve component to a game object.

```tex
MonoHermiteCurve MonoBezierCurve MonoHelixCurve MonoEllipseCurve MonoSinCurve
```

- Attach mono animation component to the game object.

```tex
MonoCurveAnimation
```

### 2D Animation

- Attach mono animation component to a game object.

```text
ImageAnimation RawImageAnimation SpriteAnimation UVFrameAnimation UVAnimation
```

------

[Previous](../../README.md)

------

Copyright © 2021 Mogoson.	mogoson@outlook.com