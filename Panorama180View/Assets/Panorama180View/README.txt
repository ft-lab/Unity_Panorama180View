//------------------------------------------------.
// Panorama180View
//------------------------------------------------.

[ How to use ] ----------------

Please turn on "Virtual Reality Supported" in "XR Settings" of "Project Settings".

1. Copy the "Panorama180View" folder to the project.
2. Added "Scripts/Panorama180View/Panorama180View" to the MainCamera component.
3. In the Inspector of MainCamera, specify the parameter of "Panorama180View".

   File Type      : Image or Video
   Image          : Still image
   Video          : Video Clip
   ProjectionType : Equirectangular360TopAndBottom  Place left eye / right eye equirectangular 360 degrees image on the top and bottom
                    Equirectangular180SideBySide    Place left eye / right eye equirectangular 180 degrees image on the left and right
                    FishEye180SideBySide            Place left eye / right eye fish eye 180 degrees image on the left and right
   Radius         : Background sphere radius
   Intensity      : Background brightness


[ 使い方 ] ----------------

"Project Settings"の"XR Settings"で"Virtual Reality Supported"をOnにして使用してください。

1. "Panorama180View"フォルダをプロジェクトにコピー。
2. MainCameraのコンポーネントに"Scripts/Panorama180View/Panorama180View"を追加。
3. MainCameraのInspectorで、"Panorama180View"のパラメータを指定。
   File Type      : Image(静止画像)/Vodeo(動画)
   Image          : 静止画のTexture
   Video          : 動画のVideo Clip
   ProjectionType : Equirectangular360TopAndBottom  上下に左目/右目のEquirectangular360画像を配置
                    Equirectangular180SideBySide    左右に左目/右目のEquirectangular180画像を配置
                    FishEye180SideBySide            左右に左目/右目の魚眼180画像を配置
   Radius         : 背景球の半径
   Intensity      : 明るさ

[ Change Log ] ----------------

[03/13/2019]

- Added methods of state transition with panorama180 still image.
- Added sample scene "StateTransition".

[03/08/2019]

- first version
