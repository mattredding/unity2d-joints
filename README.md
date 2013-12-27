unity2d-joints - English below
==============

Unity 4.3にBox2DのオープンソースC++物理演算エンジンが統合しています。2D物理コンポーネントの使い方は3Dのに似ているから判りやすいです。3Dより2D物理演算のパフォーマンスが良いし、安定し、ゲームビルドは小さいです。
統合されたBox2D物理演算と物理のコンポーネント

リジッドボディコンポーネント(RigidBody2D)supporting static/kinematic/dynamic body, mass, linear/angular velocities, drag and auto-sleeping, and fixed-angle constraint.

サークルコライダー(CircleCollider2D)重心と半径プロパティがあります。

ボックスコライダー(BoxCollider2D)重心とサイズプロパティがあります。

ポリゴンコライダー(PolygonCollider2D)初期化するためにコンポーネントのスプライトをドラッグできます。

ディスタンスジョイント(DistanceJoint2D)リジッドボディの最大距離設定。

ヒンジジョイント(HingeJoint2D)回転角度の リミットとモーター

スライダージョイント(SliderJoint2D)直線の リミットとモーター

スプリングジョイント(SpringJoint2D)リジッドボディの柔らかい最大距離設定新しい物理マテリアルPhysicsMaterial2Dを追加しました。

シーンの2D物理の設定を管理するために2Dphysics managerを追加しました。

Added spatial queries in Physics2D scripting class for line and ray-casting and geometry overlap checks.

2Dコライダーのトリガーおようびコリジョンコールバックを追加しました。

プロファイラーに2D物理プロファイリング情報を追加しました。

エディター：シフトキーを押すと2Dコライダーを変更できます。



ジョイントの使い方

2Dの物理演算の新機能を調査するために実験シーンを作成しました。新しい2Dジョイント別に分類しましたけど、全部のシーンに2Dのリジッドボディも2Dのコライダーもを利用しています。

DistanceJoint2D　ディスタンスジョイント

anchor
DistanceJoint2D コンポーネントを持つほうのオブジェクトのジョイントのアンカー地点


connectedAnchor
二つめのオブジェクト上のジョイントのアンカー地点（すなわちコンポーネントを持たない方）


distance
ジョイントの二つの端を隔てた距離


ユニティのマニュアルで、DistanceJoint2Dの説明は「二つのRigidbody2Dを固定の距離だけ離れた位置のままとするジョイント。」ですが、少し違います。最大距離は固定だけど、最小距離の設定がないです。まるで二つオブジェクトは紐で結んでいる状態です。

SpringJoint2Dコンポーネントと異なり、オブジェクトを隔てる距離は完全に固定されていて、ストレッチなども許容していません。

実験のシーンでニュートンのゆりかごを作成しました。Chris Drumの良いアイディアでした、ありがとうございます。

それぞれのボールのディスタンスジョイントの接続しているリジッドボディーは「rack」と言う開いているゲームオブジェクトです。そのリジッドボディーを物理の力で動かないようにIsKinematicを選択しています。コーディングでボールの位置の上の点にConnected Anchorを設定しています。

他の「ballLift」と言う開いているゲームオブジェクトがあって、ディスタンスジョイントで一つのボールに接続しています。キーを押すとそのディスタンスジョイントのActiveステートを切り替えって、ボールは動きます。

衝突判定について、ボールの反応の調整は難しかったです。ボールはそれぞれにサークルコライダーがあって、コライダーに「Clacker」と言う物理マテリアルが付けています。Clackerマテリアルの設定はFriction 0, Bounciness 1です。

まだ変な動き方があったので色々な調整とを試してボールのリジッドボディーのCollision DetectionをContinuousに変更した時良くなりました。ユニティマニュアルでこのモードはstaticのメッシュに関係がある設定って書いてるからなぜ効果があるかは判りません？！

ちょっとしたことですけど紐を表示することはLine Rendererでできました。


SpringJoint2D　スプリングジョイント

anchor
SpringJoint2D コンポーネントを持つオブジェクトのジョイント上のアンカーポイント


connectedAnchor
二つめのオブジェクト (すなわちコンポーネントがない方) のジョイント上のアンカーポイント


dampingRatio
移動速度に比例して減少する量


distance
ジョイントの二つの端を隔てた距離


frequency
揺動する頻度


二つの Rigidbody2D を固定の距離離れた位置のままとするためにフォースを力をかけるジョイント。

DistanceJoint2D コンポーネントと異なり、ストレッチなども許容しています。

このジョイントだけを使えるけど規制は難しいです。SliderJoint2Dと一緒に使うことはもと実用的です。

もしこの事件シーンの作ったオブジェクトにスプリングジョイントだけを利用したら、立ち上がらないのでスライダージョイントも利用しています。スプリングの設定はとても簡単でした。両方Anchorは真ん中の店のままで、DistanceとFrequencyを調整しました。Damping Ratioも必要じゃなかったです。

スプリングの圧縮はスライダージョイントのモターの力で動いたいます。単純にスライダーのUseMotorの値を切り替えています。

HingeJoint2D　ヒンジジョイント

anchor
HingeJoint2D コンポーネントを持つオブジェクトのジョイント上のアンカーポイント


connectedAnchor
二つめのオブジェクト (すなわちコンポーネントがない方) のジョイント上のアンカーポイント


limits
ジョイント上の回転角度の リミット


motor
ジョイントに適用されたモーターの力のための引数


useLimits
回転の範囲にリミットをつけるか


useMotor
モーター トルクによりジョイントは自動回転させるか


Rigidbody2D が空間上の点または別のオブジェクト上の点、の周りを回転できるようにするジョイント。

ドアやレバー等の動きをできるし、もっと複雑な動きもできます。例えば電車の車輪を作るために回転の範囲にリミットなし、ジョイントに適用されたモーターの力で自動的に動くように設定ができます。

実験のシーンには複数使い方があります。一番簡単な使い方は真ん中にある電車のシグナルです。設定している値しだいに、決まっている角度までにモーターの力で回転します。

汽車の車輪と列車の台車の接続点にはヒンジジョイントを利用しています。

実世界のヒンジは、開く角度に限度がありますが、ヒンジジョイントでは、制限がないように設定することもできます。そのような設定で車輪のような自由に動きが実現できます。本当の汽車の同じに機関車の車輪しかモーターの力で動いていません。

多分現実的じゃないけど、チェインの例えばは全体的にヒンジジョイントで作っています。チェインのリンックごとにヒンジジョイントで次のリンクに接続しています。コーディングで作れるためにジョイントのanchorの位置はリンクの下の開いているGame Objectから参照しています。

SliderJoint2D　スライダージョイント

anchor
SliderJoint2D コンポーネントを持つオブジェクトのジョイント上のアンカーポイント


angle
制限している直線の角度


connectedAnchor
二つめのオブジェクト (すなわちコンポーネントがない方) のジョイント上のアンカーポイント


limits
ジョイント線のリミット


motor
ジョイントに適用されたモーターの力のための引数


useLimits
リミットをつけるか


useMotor
モーター トルクによりジョイントは自動回転させるか


このジョイントはRigidBody2Dの位置を直線に固定します。

実験のシーンには二つ例えばの機械を作成しました。左側のはカムの機械で右側のはピストンです。カムは、直線に回転運動に変換して、やくにピストンは回転に直線運動に変換します。

円板カムと呼ばれる中心から円周までの距離が一定でない板を回転する軸に取り付け、これの板に接する物に周期的な運動を与える。カムに接するオブジェクトはスライダージョイントで直線運動に制限しています。歯車はPolygonCollider2Dを利用していて、この機械の左の歯車の付けてあるヒンジジョイントのモーターの力で動いています。歯車とカムを衝突しないように歯車は別なレイヤーにおいておきまして、Physics2DのLayer Collision Matrixの設定で他のレイヤーの選択を解除しました。

ピストンの機械はジョイントのモーターを全然使っていません。ピストンヘッドはスライダージョイントで直線に制限をしていて、接続しているロッドがおいてあるトリガーコライダーを衝突際、ヘッドに下向きのAddForceのメソッドを実行しています。それで、ピストンのように一回転してまたコライダーに衝突します。そのままで繰り返して早くなります。





性能試験

以下のブリッグポストによると、3D物理演算と比べたら、新しい2D物理演算は５倍ぐらい早いです！その実験をパソコンでブラウザーで見るとフレームレートはあまり変えないけど2Dの動きの方が軽快です。
UNITY3D V4.3: 2D VS 3D PHYSICS
試験

携帯端末で実験をできるために似ていテスト、300箱を落とすアニメションテストシーンを作りました。iPhone 4Sで2Dの晩は大丈夫でしたけれど、3Dの晩は最後の方は重くなりました。

プロファイラの情報を確認して、以下の二つ項目で何が違うを見えます。

3D frametime>     min: 41.1   max: 518.4   avg: 210.4
2D frametime>     min: 39.1   max: 69.0   avg: 46.9

3D player-detail> physx: 202.3 animation
2D player-detail> physx: 41.0 animation

完全情報

3D

iPhone Unity internal profiler stats:
cpu-player>    min: 39.8   max: 512.9   avg: 206.9
cpu-ogles-drv> min:  0.2   max:  1.7   avg:  0.4
cpu-present>   min:  0.7   max:  4.6   avg:  2.4
cpu-waits-gpu> min:  0.7   max:  4.6   avg:  2.4
msaa-resolve> min:  0.0   max:  0.0   avg:  0.0
frametime>     min: 41.1   max: 518.4   avg: 210.4
draw-call #>   min:   2    max:   3    avg:   2     | batched:   153
tris #>        min:   664  max:   994  avg:   772   | batched:   768
verts #>       min:   930  max:  1392  avg:  1081   | batched:  1075
player-detail> physx: 202.3 animation:  0.0 culling  0.0 skinning:  0.0 batching:  0.6 render:  2.0 fixed-update-count: 2 .. 17
mono-scripts>  update:  0.0   fixedUpdate:  0.0 coroutines:  1.4 
mono-memory>   used heap: 200704 allocated heap: 262144  max number of collections: 0 collection total duration:  0.0

2D

iPhone Unity internal profiler stats:
cpu-player>    min: 37.7   max: 61.2   avg: 44.8
cpu-ogles-drv> min:  0.3   max:  1.1   avg:  0.4
cpu-present>   min:  0.7   max:  6.4   avg:  1.5
frametime>     min: 39.1   max: 69.0   avg: 46.9
draw-call #>   min:   3    max:   3    avg:   3     | batched:   187
tris #>        min:   914  max:   964  avg:   940   | batched:   936
verts #>       min:  1280  max:  1350  avg:  1317   | batched:  1311
player-detail> physx: 41.0 animation:  0.0 culling  0.0 skinning:  0.0 batching:  0.7 render:  2.4 fixed-update-count: 2 .. 3
mono-scripts>  update:  0.0   fixedUpdate:  0.0 coroutines:  0.2 
mono-memory>   used heap: 200704 allocated heap: 262144  max number of collections: 0 collection total duration:  0.0




==============

Unity 4.3 integrates the open source C++ Box2D physics engine. Implementation is very similar to 3D Physics components. 2D physics components provide better performance, more stable simulation and smaller game builds than existing 3D physics components.

Physics Components and features introduce include:

Rigid-body component (RigidBody2D) supporting static/kinematic/dynamic body, mass, linear/angular velocities, drag and auto-sleeping, and fixed-angle constraint.

Circle collider (CircleCollider2D) supporting a centroid and radius.

Box collider (BoxCollider2D) supporting a centroid and a size.

Polygon collider (PolygonCollider2D) supporting an arbitrary set of polygons. It can be initialized to the shape of a sprite by dragging the sprite onto the component.

Distance joint (DistanceJoint2D) supporting a hard maximum distance between rigid-bodies.

Hinge joint (HingeJoint2D) supporting linear and angular limits and motor drive.

Slider joint (SliderJoint2D) supporting an axis constraint, linear limits and motor drive.

Spring joint (SpringJoint2D) supporting a soft (spring) distance between rigid-bodies.

Added a new physics material PhysicsMaterial2D to share friction and bounciness including default material support.

Added a 2D physics manager to contain scene settings such as gravity etc.

Added spatial queries in Physics2D scripting class for line and ray-casting and geometry overlap checks.

Added both trigger and collision callbacks for 2D colliders including collision points and normal.

Added 2D physics profiling information to the profiler.

Editor: Hold down shift to quickly modify 2D colliders.



Use of 2D Joints

To investigate the new 2D Physics features I’ve made some test scenes. The scenes represent the four different physics joint types to help explain how they work, but each scene also utilizes other components like rigid bodies and colliders.

DistanceJoint2D


anchor
The joint's anchor point on the object that has the DistanceJoint2D component.


connectedAnchor
The joint's anchor point on the second object (ie, the one which doesn't have the component).


distance
The distance separating the two ends of the joint.


The manual says “Joint that keeps two Rigidbody2D objects a fixed distance apart.” But this joint works a little differently. There is a maximum distance settings but no minimum settings. The effect it of two objects being tied together by a piece of string.

Note that unlike the SpringJoint2D component, the distance separating the objects is truly fixed and does not allow for any stretching.

In the test scene I’ve made a Newtons Cradle. The balls each have a Distance Joint 2D which is attached to an empty Game Object called “Rack”. The Rack’s Rigid Body is set to IsKinematic so that it is not affected by physics forces. The balls distance joint Connected Anchor is programmatically assigned to a fixed point in space above the ball. There is another empty Game Object called “Ball Lift” which has a Distance Joint 2D connected to one of the balls, this has the effect of suspending the ball between the two rigid bodies it is attached to. When a key is hit, the “LiftBall” distance joint is de-activated allowing the ball to swing.

Adjusting the balls’ collision behaviour was quite difficult. The balls each have a Circle Collider with a Physic Material 2D attached. The material just has the settings Friction 0, Bounciness 0. 

The balls would bounce off each other, but the behaviour was strange and erratic. I tried changing many settings, but when I set the balls’ rigid body Collision Detection settings to Continuous, the balls started bouncing more naturally. According to the Unity manual, this settings only works for static colliders, so I am not sure why this improved things?!

Finally, I added a Line Renderer to display the string the balls are attached by. Its a purely cosmetic addition and doesn’t affect the physics at all.


SpringJoint2D


anchor
The joint's anchor point on the object that has the SpringJoint2D component.


connectedAnchor
The joint's anchor point on the second object (ie, the one which doesn't have the component).


dampingRatio
The amount by which the spring force is reduced in proportion to the movement speed.


distance
The distance the spring will try to keep between the two objects.


frequency
The frequency at which the spring oscillates around the distance distance between the objects.

Joint that attempts to keep two Rigidbody2D objects a set distance apart by applying a force between them.

Note that unlike DistanceJoint2D, the length of the joint can stretch and oscillate.

You can use the spring joint alone, but it is very difficult to control. I found it worked best in combination with the SliderJoint2D.

If the objects created in this test scene only had a SpringJoint2D attached, they would not stand up properly, so they also use a SliderJoint2D. The spring settings were simple. Both Anchors are left at the default settings. Distance and Frequency were adjusted to get the right distance between the parts and the right level of springiness. Damping Ratio was no required.

Compressing the spring was achieved by toggling the slider motor with the UseMotor setting.
HingeJoint2D


anchor
The joint's anchor point on the object that has the HingeJoint2D component.


connectedAnchor
The joint's anchor point on the second object (ie, the one which doesn't have the component).


limits
Limit of angular rotation on the joint.


motor
Parameters for the motor force applied to the joint.


useLimits
Should limits be placed on the range of rotation?


useMotor
Should the joint be rotated automatically by a motor torque?


Joint that allows a Rigidbody2D object to rotate around a point in space or a point on another object.

This joint can be used for something simple like a door or lever, or something more complicated. For example to make a train wheel, you can leave the rotation limit unchecked and use the components motor force to turn the wheel automatically.

In the test scene there are many implementations for the hinge joint. The simplest is the train signal in the middle of the scene. The hinge joint has a maximum rotation setting, and is driven by the motor until that limit is reached.

The steam train wheels and carriage joints are made with hinge joints. 

When you think of a real hinge, it must have a maximum angle which it can open to. With a hinge joint though, you can decide whether to use the limit or not. If you don’t use the limit, the hinge acts more like a wheel, able to spin freely on an axle. Just like a real steam train, only the wheels on the locomotive are powered by a motor.

Finally, its probably not a practical usage, but the chain is made entirely with hinge joints. Each link Game Object has empty game objects as reference points for the anchors. This is a useful trick for when you want to set the anchors programmatically.
SliderJoint2D

anchor
The joint's anchor point on the object that has the HingeJoint2D component.


angle
The angle of the line in space.


connectedAnchor
The joint's anchor point on the second object (ie, the one which doesn't have the component).


limits
Restrictions on how far the joint can slide in each direction along the line.


motor
Parameters for a motor force that is applied automatically to the Rigibody2D along the line.


useLimits
Should motion limits be used?


useMotor
Should a motor force be applied automatically to the Rigidbody2D?

This joint fixes a RigidBody2D position to a straight line. You can set limits and use a motor.

In the test scene there are two simple machines. On the left a cam mechanism and on the right a piston. A cam converts rotary motion into linear, whereas a piston does the opposite, turning linear motion into rotary.

A cam is a rotation wheel with an irregular shape. A cam follower coming in contact with it transforms the eccentric oscillations into a sliding motion. The Slider Joint 2D attached to the cam followers in the scene do nothing but restrict their movement to a fixed line. The cogs use PolygonCollider2D colliders, and only the left-hand cog is actually driven by a motor. In order to prevent the cogs from colliding with the cam, I had to assign them to a separate layer, and in Physics2D settings Layer Collision Matrix deselect all other layers in corresponding to the cog layer.

The piston machine is not powered by a joint motor at all. The piston head’s position is regulated by a SliderJoint2D, when the connected rod collider collides with a hidden trigger collider a method is invoked, which uses AddForce to propel the head rigid body downwards. The rotary motion of the connected drive wheel rotates the rod around until it again collides with the trigger. Continuing in this fashion, the piston gradually gains speed.


Performance

According to the test carried out in the blog post below, 2D physics runs five times faster than the 3D counterpart.

UNITY3D V4.3: 2D VS 3D PHYSICS
Test

I made a similar test in order to check the performance difference on a mobile device. When testing on an iPhone 4S there was a significant performance difference. The 2D version was able to handle the animation with 300 boxes, but the 3D version animation slowed-down towards the end.

The significant performance difference is indicated by these data points:

3D frametime>     min: 41.1   max: 518.4   avg: 210.4
2D frametime>     min: 39.1   max: 69.0   avg: 46.9

3D player-detail> physx: 202.3 animation
2D player-detail> physx: 41.0 animation

The complete log is below:

3D

iPhone Unity internal profiler stats:
cpu-player>    min: 39.8   max: 512.9   avg: 206.9
cpu-ogles-drv> min:  0.2   max:  1.7   avg:  0.4
cpu-present>   min:  0.7   max:  4.6   avg:  2.4
cpu-waits-gpu> min:  0.7   max:  4.6   avg:  2.4
msaa-resolve> min:  0.0   max:  0.0   avg:  0.0
frametime>     min: 41.1   max: 518.4   avg: 210.4
draw-call #>   min:   2    max:   3    avg:   2     | batched:   153
tris #>        min:   664  max:   994  avg:   772   | batched:   768
verts #>       min:   930  max:  1392  avg:  1081   | batched:  1075
player-detail> physx: 202.3 animation:  0.0 culling  0.0 skinning:  0.0 batching:  0.6 render:  2.0 fixed-update-count: 2 .. 17
mono-scripts>  update:  0.0   fixedUpdate:  0.0 coroutines:  1.4 
mono-memory>   used heap: 200704 allocated heap: 262144  max number of collections: 0 collection total duration:  0.0

2D

iPhone Unity internal profiler stats:
cpu-player>    min: 37.7   max: 61.2   avg: 44.8
cpu-ogles-drv> min:  0.3   max:  1.1   avg:  0.4
cpu-present>   min:  0.7   max:  6.4   avg:  1.5
frametime>     min: 39.1   max: 69.0   avg: 46.9
draw-call #>   min:   3    max:   3    avg:   3     | batched:   187
tris #>        min:   914  max:   964  avg:   940   | batched:   936
verts #>       min:  1280  max:  1350  avg:  1317   | batched:  1311
player-detail> physx: 41.0 animation:  0.0 culling  0.0 skinning:  0.0 batching:  0.7 render:  2.4 fixed-update-count: 2 .. 3
mono-scripts>  update:  0.0   fixedUpdate:  0.0 coroutines:  0.2 
mono-memory>   used heap: 200704 allocated heap: 262144  max number of collections: 0 collection total duration:  0.0




