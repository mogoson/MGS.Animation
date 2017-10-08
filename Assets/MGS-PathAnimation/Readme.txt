==========================================================================
  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
  Name: MGS-PathAnimation
  Author: Mogoson   Version: 0.1.0   Date: 7/9/2017
==========================================================================
  [Summary]
    Unity plugin for make path animation in scene.
--------------------------------------------------------------------------
  [Demand]
    Create path curve by anchors.

	Play animation base on path curve.
--------------------------------------------------------------------------
  [Environment]
    Unity 5.0 or above.
    .Net Framework 3.0 or above.
--------------------------------------------------------------------------
  [Achieve]
    Path£ºCreate path curve for animation.

    PathAnimation£ºPlay animation base on path curve.

    VectorAnimationCurve£ºDefine 3D curve for path.

    PathEditor£ºEdit anchors of path.

    PathAnimationEditor£ºAlign the animation gameobject to the start
    point of path curve.
--------------------------------------------------------------------------
  [Usage]
    Create a empty gameobject and attach the Path component to it.

    Click the green sphere to add a anchor and drag the handle to change
    it's position.

    Click the red sphere to remove a anchor if you want.

    Attach the PathAnimation component to the target animation gameobject
    and set the "Path" as the Path component and click the "AlignToPath"
    button to align the animation gameobject to the start point of path
    curve.
--------------------------------------------------------------------------
  [Demo]
    Demos in the path "MGS-PathAnimation/Scenes" provide reference to you.
--------------------------------------------------------------------------
  [Resource]
    https://github.com/mogoson/MGS-PathAnimation.
--------------------------------------------------------------------------
  [Contact]
    If you have any questions, feel free to contact me at mogoson@qq.com.
--------------------------------------------------------------------------