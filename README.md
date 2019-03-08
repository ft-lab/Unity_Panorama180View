# Unity_Panorama180View

UnityでEquirectangular180のSide By SideをVRとして見るためのサンプルです。    

## 開発環境

Unity 2018.3.7 (Windows)     

## 使い方

"Project Settings"の"XR Settings"で"Virtual Reality Supported"をOnにして使用してください。    

1. "Panorama180View"フォルダをプロジェクトにコピー。    
2. MainCameraのコンポーネントに"Scripts/Panorama180View/Panorama180View"を追加。    
3. MainCameraのInspectorで、"Panorama180View"のパラメータを指定。    

| パラメータ名| 内容 |
| :--- | :--- |
|File Type|Image(静止画像)/Vodeo(動画) |
|Image|静止画のTexture|
|Video|動画のVideo Clip|
|ProjectionType|Equirectangular360TopAndBottom:上下に左目/右目のEquirectangular360画像を配置<br>Equirectangular180SideBySide:左右に左目/右目のEquirectangular180画像を配置<br>FishEye180SideBySide:左右に左目/右目の魚眼180画像を配置<br>|
|Radius|背景球の半径|
|Intensity|明るさ|
