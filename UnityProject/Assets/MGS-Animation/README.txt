==========================================================================
  Copyright © 2020 Mogoson. All rights reserved.
  Name: MGS-PathAnimation
  Author: Mogoson   Version: 1.0.0-preview1   Date: 12/27/2020
==========================================================================
  [Summary]
    Unity plugin for make animation in scene.
--------------------------------------------------------------------------
  [Demand]
    Frames animation display base on Mesh Renderer.
    Frames animation display base on Sprite Renderer.
    Frames animation display base on UI component(Image, RawImage).
    Frames(an image) animation display base on Mesh Renderer.
    UV offset animation display base on Mesh Renderer.
    Play local gif animation.

    Create path base on curve.
    Play animation base on path curve.
--------------------------------------------------------------------------
  [Environment]
    Unity 5.0 or above.
    .Net Framework 3.5 or above.
--------------------------------------------------------------------------
  [Achieve]
    RFramesAnimation : Frames(multi images) animation display base on Mesh
    Renderer.

    SRFramesAnimation ：Frames(multi images) animation display base on
    Sprite Renderer.

    UIFramesAnimation ：Frames(multi images) animation display base on UI
    component(Image).

    UVFramesAnimation ：Frames(an frames image) animation display base on
    Mesh Renderer.

    UVAnimation ：(an frames image)UV offset animation display base on
    Mesh Renderer.

    BezierCurve : Define BezierCurve.
    HermiteCurve : Hermite curve in three dimensional space.
    EllipseCurve : Ellipse curve.
    HelixCurve : Helix curve.
    SinCurve : Sin curve.

    CurvePath : Define path base on curve.
    BezierPath : Define path base on cubic bezier curve.
    HermitePath :  Define curve path base on hermite curve.
    CirclePath : Path base on circle curve.
    EllipsePath : Path base on ellipse curve.
    HelixPath : Path base on helix curve.
    SinPath : Path base on sin curve.

    CurvePathAnimation : Define animation base on curve path.
--------------------------------------------------------------------------
  [Usage]
    Attach FramesAnimation(example RFramesAnimation) or UVAnimation
    component to the Renderer gameobject.

    Add the images to the "Frames" of FramesAnimation and set the main
    texture of Renderer material.

    If use UVFramesAnimation component, set the values of "Row" and
    "Column" and click the "Apply UV Maps" button in Inspector to apply
    UV maps.

    Create an empty gameobject and attach the path component BezierPath
    or HermitePath to it.

    If BezierPath attached, drag the green sphere to change tangent and
    drag the blue sphere to change it's position.

    if HermitePath attached, drag the blue sphere to change it's position,
    press the ALT key and click the green sphere to add anchor, press the
    SHIFT key and click the red sphere to remove anchor if you want.
    
    Attach the CurvePathAnimation component to the target animation
    gameobject, set the "Path" as the path component and click the
    "Align To Path" button to align the animation gameobject to the start
    point of path curve.
--------------------------------------------------------------------------
  [Demo]
    Demos in the path "MGS-Animation/Scenes" provide reference to you.
--------------------------------------------------------------------------
  [Resource]
    https://github.com/mogoson/MGS-Animation.
--------------------------------------------------------------------------
  [Contact]
    If you have any questions, feel free to contact me at mogoson@outlook.com.
--------------------------------------------------------------------------