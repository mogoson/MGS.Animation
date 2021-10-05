[TOC]

# MGS.Animation

## Summary

- Unity plugin for make animation in scene.

## Platform

- Windows

## Environment

- .Net Framework 3.5 or above.
- Unity 5.0 or above.

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

### Look At

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

- Attach mono animation component to a game object.

```text
ImageAnimation RawImageAnimation SpriteAnimation UVFrameAnimation UVAnimation
```

## Demo

- Demos in the path "MGS.Packages/Animation/Demo/" provide reference to you.

## Source

- https://github.com/mogoson/MGS.Animation.

------

Copyright © 2021 Mogoson.	mogoson@outlook.com