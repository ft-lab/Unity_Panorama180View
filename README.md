# Unity_Panorama180View

[To Japanese document](README_jp.md)    

A sample project for viewing still image of Equirectangular 180 degrees Side By Side or VR180 video of fisheye as VR on Unity.    
For VR180 still image, please convert them to 2 eyes layout in advance with "VR180 Creator" (https://vr.google.com/vr180/apps/).    

## Development environment

Unity 2018.3.8 (Windows)     

## Folder structure

    [Assets]    
      [Panorama180View]    Required files (copy and use)    
        [Panorama180View]
          [Resources]
            [Objects]
            [Shaders]
          [Scripts]
      [Images]             sample image
      [Scripts]            Scripts used in sample      
      [Scenes]
        SampleScene        sample scene    
        StateTransition    Sample scene of state transition

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

## Sample Scenes

| Scene name | Description |
| :--- | :--- |
| SampleScene | One that only displays panorama180 still image |
| StateTransition | Use a script to fade in and transition between two images |

## External method of Panorama180View.Panorama180View

By using the public method of Panorama180View.Panorama180View, script can control the state transition of panorama180.     
Please refer to "Assets/Scripts/StateTransition.cs" for usage.    

### Get version

     int GetVersion ();     

Get version.

### Change state transition 

     void SetStateTransition (StateTransitionType type);

Does not transition state with StateTransitionType.None.    
Fade in with StateTransitionType.FadeIn.   
Fade out with StateTransitionType.FadeOut.   
Blend two textures with StateTransitionType.Blend.   
When this method is called, it will transition to the specified state.    
Note that you can not change the value during transition.     

### Change transition source texture

    void SetSrcTexture (Texture2D tex);    

Specifies the texture of the transition source.    

### Change transition destination texture

    void SetDestTexture (Texture2D tex);    

Specifies the texture of the transition destination.    
In the case of "SetStateTransition(Panorama180View.Panorama180View.StateTransitionType.Blend);", transition from the texture specified in SetSrcTexture to the texture specified in SetDestTexture.    


### Change fade in color

    void SetFadeInColor (Color col);    
Specify the start color for fading in.     

### Change fade out color

    void SetFadeOutColor (Color col);    
Specify the end color for fading out.     

### Specify the state transition interval (seconds)

    void SetTransitionInterval (float interval);    
Specify the transition time in seconds.    

## Change log

### [03/13/2019]

- Added methods of state transition with panorama180 still image.    
- Added sample scene "StateTransition".    

### [03/08/2019]

- First verion

