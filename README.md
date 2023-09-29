# TopdownShootURP
Another Unity Top - down shooting Game base on URP

修复说明（因为没有没有BUG的代码）
2023 by sidney
===============================
1.使用Urp项目 导入.unitypack

2.PackageManger安装（新）InputSystem

3.某个sdk包的代码，要替换。。。。（如果用Unity2023打开。。。。）

'''
            //打包微信？？wx
#if UNITY_2023_1_OR_NEWER
            // 默认更改为OptimizeSize，减少代码包体积
            PlayerSettings.SetIl2CppCodeGeneration(UnityEditor.Build.NamedBuildTarget.WebGL, UnityEditor.Build.Il2CppCodeGeneration.OptimizeSize);
#elif UNITY_2021_2_OR_NEWER
            EditorUserBuildSettings.il2CppCodeGeneration = UnityEditor.Build.Il2CppCodeGeneration.OptimizeSize;
#endif
'''

4.BuildSetting 需要添加Menu,Game这两个场景




看看用互联网方式-接力做（不知道什么时候完成）
---已完成；XXXXX暂时不做；*|※重点关注；△格外增加的越做越多
===============================
1. 平面素材包（需求，概要）
2. ---动作修复（基于Anim Controller）
3. xxxxx键盘Adapter(只要是方便调试）
4. 无限关卡。。。。逻辑
5. ※自动生成2D平面场景
5. xxxxx优化压缩后放到Git(100+M->)
5. ---居然没有自动射击
5. ---场景光影
5. 要不要加一个3d版本？？
5. 引入事件系统
5. moduleManager
5. ---Camera 不会跟着人动。。。。。
5. 关卡难度曲线
5. “弹幕”多个敌人的优化（压力测试）
5. △加一些全屏特效


IssuES
=================
- ---虫攻击主角时好像会消失，主角灯光不够大，在光边缘已经出现虫（要不要做渐变）
	(原来人的Z 轴坐标必须和虫一致，否则会重叠，看起来虫消失了（人在虫上））
- xxxxx右键射击会有一个小位移
- 关卡字，可以优化一下。。。。
- xxxxx是否需要加入Boss战斗
	（每5关一个Boss）
- ※还是搞了个TopDown-Engine,文档补一下吧
	(https://blog.csdn.net/avi9111/article/details/105919819)

灯光问题
=======================
- ---改用Light2D,虽说是2D灯光，但还是可以在Scene View做调整（Editor is Great, Unity Team did well）
- *场景+灯光，如何设计？？
- *这个Kenney 素材设计，真心不错
	（https://www.kenney.nl/assets/page:11）
- Light + 后期效果，有些行货, 有空才学习
	(https://www.youtube.com/watch?v=nbOeOTs6tkY)

*Features
============================
* Top-Down Shooting Game
* Unity Urp Based(2021,2023 ready)
* Light2D
* CameraFollow
* Visual Joystick
* Auto Aim & Attack
* Textures and Animations(1 player, 2 enemies, 1 * level map)
* Navegation -2d - Third Party
* WX Platform sdk(deployment)
* New Input System
* 10000Enemies - 60Fps
