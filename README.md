# Unity_Panorama180View

[To Japanese document](README_jp.md)

A sample project for viewing still image of Equirectangular 180 degrees Side By Side or VR180 video of fisheye as VR on Unity.    
For VR180 still image, please convert them to 2 eyes layout in advance with "VR180 Creator" (https://vr.google.com/vr180/apps/).    

## Development environment

Unity 2018.3.7 (Windows)     

## Folder structure

    [Assets]    
      [Panorama180View]    Required files (copy and use)    
        [Panorama180View]
          [Resources]
            [Objects]
            [Shaders]
          [Scripts]
      [Images]             sample image
      [Scenes]
        SampleScene        sample scene    

## How to use

Please turn on "Virtual Reality Supported" in "XR Settings" of "Project Settings".    

1. Copy the "Panorama180View" folder to the project.    
2. Added "Scripts/Panorama180View/Panorama180View" to the MainCamera component.    
3. In the Inspector of MainCamera, specify the parameter of "Panorama180View".    

| Parameter name| Description |
| :--- | :--- |
|File Type|Image or Video |
|Image|Still image|
|Video|Video Clip|
|ProjectionType|Equirectangular360TopAndBottom : Place left eye / right eye equirectangular 360 degrees image on the top and bottom<br>Equirectangular180SideBySide : Place left eye / right eye equirectangular 180 degrees image on the left and right<br>FishEye180SideBySide : Place left eye / right eye fish eye 180 degrees image on the left and right<br>|
|Radius|Background sphere radius|
|Intensity|Background brightness|

![img00](images/p180view_img_00.jpg)    

## Type of projection

### Equirectangular360 TopAndBottom

Place left eye / right eye equirectangular 360 degrees image on the top and bottom.    
![img01](images/background_vr180_type_01.jpg)    

### Equirectangular180 SideBySide

Place left eye / right eye equirectangular 180 degrees image on the left and right.    
![img02](images/background_vr180_type_02.jpg)    

### FishEye180 SideBySide

Place left eye / right eye fish eye 180 degrees image on the left and right.    
![img03](images/background_vr180_type_03.jpg)    

