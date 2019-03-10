#pragma warning disable 0414

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Rendering;

namespace Panorama180View {
    [RequireComponent(typeof(Camera))]
    public class Panorama180View : MonoBehaviour
    {
        /**
        * 背景の種類.
        */
        public enum BackgroundType {
            Image,
            Video
        }

        /**
         * パノラマの種類.
         */
        public enum PanoramaType {
            Equirectangular360TopAndBottom,
            Equirectangular180SideBySide,
            FishEye180SideBySide
        }

        [SerializeField] BackgroundType fileType = BackgroundType.Image;        // 背景の種類.
        [SerializeField] Texture2D image = null;                                 // 背景画像のパノラマ.
        [SerializeField] VideoClip video = null;                                 // 背景動画のパノラマ.

        [SerializeField] PanoramaType projectonType = PanoramaType.Equirectangular180SideBySide;             // パノラマの種類.
        [SerializeField, Range(1.0f, 10000.0f)] float radius = 500.0f;           // 背景球の半径.
        [SerializeField, Range(0.0f, 10.0f)] float intensity = 1.0f;           // 明るさ.

        private GameObject m_backgroundSphere = null;       // 背景球.
        private Material m_backgroundSphereMat = null;      // 背景のマテリアル.
        private GameObject m_videoG = null;             // VideoClip用のGameObject.
        private VideoPlayer m_videoPlayer = null;       // Video再生用.
        private GameObject m_audioSource = null;        // Audioの発生源. 
        private RenderTexture m_renderTexture = null;   // 1フレームのキャプチャ用.

        void Start () {
            // 背景球を作成.
            m_CreateBackgroundSphere();

            // VideoPlayerの作成.
            m_CreateVideoPlayer();
        }

        void Update () {
            // 背景テクスチャを指定.
            m_SetBackgroundTexture();
        }

        void OnDestroy () {
            if (m_renderTexture != null) {
                Destroy(m_renderTexture);
                m_renderTexture = null;
            }
            if (m_videoG != null) {
                GameObject.Destroy(m_videoG);
                m_videoG = null;
            }
            if (m_audioSource != null) {
                GameObject.Destroy(m_audioSource);
                m_audioSource = null;
            }
            if (m_backgroundSphereMat != null) {
                Destroy(m_backgroundSphereMat);
            }
            if (m_backgroundSphere != null) {
                GameObject.Destroy(m_backgroundSphere);
            }
        }

        /**
         * 背景球を作成.
         */
        private void m_CreateBackgroundSphere () {
            if (m_backgroundSphereMat == null) {
                // 以下、ビルドして実行する時にShaderを読み込めるように
                // Shader.FindではなくResources.Load<Shader>を使用している.
                Shader shader = Resources.Load<Shader>("Shaders/panoramaSphereRendering");
                m_backgroundSphereMat = new Material(shader);
            }
            if (m_backgroundSphere == null) {
                Mesh mesh = Resources.Load<Mesh>("Objects/backgroundSphere_vr360");
                m_backgroundSphere = new GameObject("panorama360Sphere");

                MeshRenderer meshRenderer = m_backgroundSphere.AddComponent<MeshRenderer>();
                MeshFilter meshFilter = m_backgroundSphere.AddComponent<MeshFilter>();
                meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
                meshRenderer.receiveShadows    = false;
                meshRenderer.material = m_backgroundSphereMat;
                meshFilter.mesh = mesh;

                m_backgroundSphere.transform.localScale = new Vector3(radius, radius, radius);
                m_backgroundSphere.transform.position = this.transform.position;
 
                // Y軸中心の回転角度.
                Quaternion currentCameraRot = this.transform.rotation;
                float cRotY = currentCameraRot.eulerAngles.y;
                m_backgroundSphere.transform.rotation = Quaternion.Euler(0, cRotY + 90, 0);
            }
        }

        /**
         * VideoPlayerを作成.
         */
        private void m_CreateVideoPlayer () {
            // Audioソース用のGameObjectを作成.
            // Audioは、指定のGameObjectを中心に音が鳴る.
            if (m_audioSource == null) {
                m_audioSource = new GameObject("AudioSource");
                m_audioSource.transform.position = this.transform.position;
            }
            AudioSource audioSource = m_audioSource.AddComponent<AudioSource>();

            // Video再生用のGameObjectを作成.
            if (m_videoG == null) {
                m_videoG = new GameObject("VideoPlayer");
                //m_videoG.transform.parent = m_rootG.transform;
                if (m_videoG.GetComponent<VideoPlayer>() == null) {
                    m_videoG.AddComponent<VideoPlayer>();
                }
                m_videoPlayer = m_videoG.GetComponent<VideoPlayer>();
                m_videoPlayer.renderMode = VideoRenderMode.RenderTexture;
                m_videoPlayer.isLooping = true;

                m_videoPlayer.playOnAwake       = true;
                m_videoPlayer.waitForFirstFrame = true;     // ソースVideoの最初のフレームが表示される状態になるまで待機する.
            }
        }

        /**
        * 背景テクスチャを指定.
        */
        private void m_SetBackgroundTexture () {
            if (m_backgroundSphere == null || m_backgroundSphereMat == null) return;
            
            if (fileType == BackgroundType.Image) {
                // 静止画のパラメータを渡す.
                m_backgroundSphere.SetActive(image != null);
                m_videoG.SetActive(false);
                m_audioSource.SetActive(false);

                m_backgroundSphereMat.SetTexture("_MainTex", image);
                m_backgroundSphereMat.SetFloat("_Intensity", intensity);
                m_backgroundSphereMat.SetInt("_Mode", (int)projectonType);

            } else {
                // VideoClipのパラメータを渡す.
                m_backgroundSphere.SetActive(video != null);
                m_videoG.SetActive(video != null);
                m_audioSource.SetActive(video != null);

                if (m_videoPlayer == null) return;
                if (m_renderTexture != null) {
                    if (video == null || (m_renderTexture.width != video.width || m_renderTexture.height != video.height)) {
                        if (m_renderTexture != null) {
                            Destroy(m_renderTexture);
                            m_renderTexture = null;
                        }
                    }
                }
                m_videoPlayer.clip = video;

                if (video != null) {
                    int width  = (int)video.width;
                    int height = (int)video.height;
                    if (m_renderTexture == null) {
                        m_renderTexture = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32);
                        m_videoPlayer.targetTexture = m_renderTexture;

                        {
                            AudioSource audioSource = m_audioSource.GetComponent<AudioSource>();
                            if (audioSource != null) {
                                m_videoPlayer.EnableAudioTrack(0, true);
                                m_videoPlayer.SetTargetAudioSource(0, audioSource);
                            }
                        }
                    }

                    if (m_renderTexture != null) {
                        m_backgroundSphereMat.SetTexture("_MainTex", m_renderTexture);
                        m_backgroundSphereMat.SetFloat("_Intensity", intensity);
                        m_backgroundSphereMat.SetInt("_Mode", (int)projectonType);
                    }
                }
            }
        }
    }
}

