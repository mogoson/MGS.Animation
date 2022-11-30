[TOC]

# MGS.Animation

## Summary

- Unity plugin for make animation in scene.

## Platform

- Windows

## Environment

- .Net Framework 3.5 or above.
- Unity 5.0 or above.

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

- Attach mono component to a game object. [Learn More](https://github.com/mogoson/MGS.Curve)

```tex
MonoHermiteCurve MonoBezierCurve MonoHelixCurve MonoEllipseCurve MonoSinCurve
```

- Attach animation component to the game object.

```tex
MonoCurveAnimation
```

### 2D Animation

- Attach mono animation component to a game object.

```text
ImageAnimation RawImageAnimation SpriteAnimation UVFrameAnimation UVAnimation
```

## Demo

- Demos in the path "MGS.Packages/Animation/Demo/" provide reference to you.

------

Copyright © 2022 Mogoson.	mogoson@outlook.com