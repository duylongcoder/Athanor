extern "C" void RegisterStaticallyLinkedModulesGranular()
{
	void RegisterModule_Scripting();
	RegisterModule_Scripting();

	void RegisterModule_Animation();
	RegisterModule_Animation();

	void RegisterModule_Burst();
	RegisterModule_Burst();

	void RegisterModule_Core();
	RegisterModule_Core();

	void RegisterModule_ContentLoad();
	RegisterModule_ContentLoad();

	void RegisterModule_GraphicsStateCollectionSerializer();
	RegisterModule_GraphicsStateCollectionSerializer();

	void RegisterModule_Grid();
	RegisterModule_Grid();

	void RegisterModule_HierarchyCore();
	RegisterModule_HierarchyCore();

	void RegisterModule_IMGUI();
	RegisterModule_IMGUI();

	void RegisterModule_JSONSerialize();
	RegisterModule_JSONSerialize();

	void RegisterModule_Input();
	RegisterModule_Input();

	void RegisterModule_InputLegacy();
	RegisterModule_InputLegacy();

	void RegisterModule_InputForUI();
	RegisterModule_InputForUI();

	void RegisterModule_Mathematics();
	RegisterModule_Mathematics();

	void RegisterModule_ManagedKernel();
	RegisterModule_ManagedKernel();

	void RegisterModule_Physics();
	RegisterModule_Physics();

	void RegisterModule_PhysicsBackendPhysX();
	RegisterModule_PhysicsBackendPhysX();

	void RegisterModule_PhysicsCore2D();
	RegisterModule_PhysicsCore2D();

	void RegisterModule_Physics2D();
	RegisterModule_Physics2D();

	void RegisterModule_Properties();
	RegisterModule_Properties();

	void RegisterModule_RenderAs2D();
	RegisterModule_RenderAs2D();

	void RegisterModule_RuntimeInitializeOnLoadManagerInitializer();
	RegisterModule_RuntimeInitializeOnLoadManagerInitializer();

	void RegisterModule_SpriteShape();
	RegisterModule_SpriteShape();

	void RegisterModule_SpriteMask();
	RegisterModule_SpriteMask();

	void RegisterModule_Subsystems();
	RegisterModule_Subsystems();

	void RegisterModule_Tetgen();
	RegisterModule_Tetgen();

	void RegisterModule_TextRendering();
	RegisterModule_TextRendering();

	void RegisterModule_TextCoreFontEngine();
	RegisterModule_TextCoreFontEngine();

	void RegisterModule_TextCoreTextEngine();
	RegisterModule_TextCoreTextEngine();

	void RegisterModule_Tilemap();
	RegisterModule_Tilemap();

	void RegisterModule_U2DRuntime();
	RegisterModule_U2DRuntime();

	void RegisterModule_UI();
	RegisterModule_UI();

	void RegisterModule_UIElements();
	RegisterModule_UIElements();

	void RegisterModule_UnityAnalyticsCommon();
	RegisterModule_UnityAnalyticsCommon();

	void RegisterModule_WebGL();
	RegisterModule_WebGL();

	void RegisterModule_XR();
	RegisterModule_XR();

}

template <typename T> void RegisterUnityClass(const char*);
template <typename T> void RegisterStrippedType(int, const char*, const char*);

void InvokeRegisterStaticallyLinkedModuleClasses()
{
	// Do nothing (we're in stripping mode)
}

class AnimationClip; template <> void RegisterUnityClass<AnimationClip>(const char*);
class Animator; template <> void RegisterUnityClass<Animator>(const char*);
class AnimatorController; template <> void RegisterUnityClass<AnimatorController>(const char*);
class AnimatorOverrideController; template <> void RegisterUnityClass<AnimatorOverrideController>(const char*);
class Motion; template <> void RegisterUnityClass<Motion>(const char*);
class RuntimeAnimatorController; template <> void RegisterUnityClass<RuntimeAnimatorController>(const char*);
class Behaviour; template <> void RegisterUnityClass<Behaviour>(const char*);
class BuildSettings; template <> void RegisterUnityClass<BuildSettings>(const char*);
class Camera; template <> void RegisterUnityClass<Camera>(const char*);
namespace Unity { class Component; } template <> void RegisterUnityClass<Unity::Component>(const char*);
class ComputeShader; template <> void RegisterUnityClass<ComputeShader>(const char*);
class Cubemap; template <> void RegisterUnityClass<Cubemap>(const char*);
class CubemapArray; template <> void RegisterUnityClass<CubemapArray>(const char*);
class DelayedCallManager; template <> void RegisterUnityClass<DelayedCallManager>(const char*);
class EditorExtension; template <> void RegisterUnityClass<EditorExtension>(const char*);
class GameManager; template <> void RegisterUnityClass<GameManager>(const char*);
class GameObject; template <> void RegisterUnityClass<GameObject>(const char*);
class GlobalGameManager; template <> void RegisterUnityClass<GlobalGameManager>(const char*);
class GraphicsSettings; template <> void RegisterUnityClass<GraphicsSettings>(const char*);
class InputManager; template <> void RegisterUnityClass<InputManager>(const char*);
class LevelGameManager; template <> void RegisterUnityClass<LevelGameManager>(const char*);
class Light; template <> void RegisterUnityClass<Light>(const char*);
class LightProbes; template <> void RegisterUnityClass<LightProbes>(const char*);
class LightingSettings; template <> void RegisterUnityClass<LightingSettings>(const char*);
class LightmapSettings; template <> void RegisterUnityClass<LightmapSettings>(const char*);
class LowerResBlitTexture; template <> void RegisterUnityClass<LowerResBlitTexture>(const char*);
class Material; template <> void RegisterUnityClass<Material>(const char*);
class Mesh; template <> void RegisterUnityClass<Mesh>(const char*);
class MeshFilter; template <> void RegisterUnityClass<MeshFilter>(const char*);
class MeshRenderer; template <> void RegisterUnityClass<MeshRenderer>(const char*);
class MonoBehaviour; template <> void RegisterUnityClass<MonoBehaviour>(const char*);
class MonoManager; template <> void RegisterUnityClass<MonoManager>(const char*);
class MonoScript; template <> void RegisterUnityClass<MonoScript>(const char*);
class NamedObject; template <> void RegisterUnityClass<NamedObject>(const char*);
class Object; template <> void RegisterUnityClass<Object>(const char*);
class PlayerSettings; template <> void RegisterUnityClass<PlayerSettings>(const char*);
class PreloadData; template <> void RegisterUnityClass<PreloadData>(const char*);
class QualitySettings; template <> void RegisterUnityClass<QualitySettings>(const char*);
class RayTracingShader; template <> void RegisterUnityClass<RayTracingShader>(const char*);
namespace UI { class RectTransform; } template <> void RegisterUnityClass<UI::RectTransform>(const char*);
class ReflectionProbe; template <> void RegisterUnityClass<ReflectionProbe>(const char*);
class RenderSettings; template <> void RegisterUnityClass<RenderSettings>(const char*);
class RenderTexture; template <> void RegisterUnityClass<RenderTexture>(const char*);
class Renderer; template <> void RegisterUnityClass<Renderer>(const char*);
class ResourceManager; template <> void RegisterUnityClass<ResourceManager>(const char*);
class RuntimeInitializeOnLoadManager; template <> void RegisterUnityClass<RuntimeInitializeOnLoadManager>(const char*);
class Shader; template <> void RegisterUnityClass<Shader>(const char*);
class ShaderNameRegistry; template <> void RegisterUnityClass<ShaderNameRegistry>(const char*);
class Skybox; template <> void RegisterUnityClass<Skybox>(const char*);
class SortingGroup; template <> void RegisterUnityClass<SortingGroup>(const char*);
class Sprite; template <> void RegisterUnityClass<Sprite>(const char*);
class SpriteAtlas; template <> void RegisterUnityClass<SpriteAtlas>(const char*);
class SpriteRenderer; template <> void RegisterUnityClass<SpriteRenderer>(const char*);
class TagManager; template <> void RegisterUnityClass<TagManager>(const char*);
class TextAsset; template <> void RegisterUnityClass<TextAsset>(const char*);
class Texture; template <> void RegisterUnityClass<Texture>(const char*);
class Texture2D; template <> void RegisterUnityClass<Texture2D>(const char*);
class Texture2DArray; template <> void RegisterUnityClass<Texture2DArray>(const char*);
class Texture3D; template <> void RegisterUnityClass<Texture3D>(const char*);
class TimeManager; template <> void RegisterUnityClass<TimeManager>(const char*);
class Transform; template <> void RegisterUnityClass<Transform>(const char*);
class GridLayout; template <> void RegisterUnityClass<GridLayout>(const char*);
class BoxCollider; template <> void RegisterUnityClass<BoxCollider>(const char*);
class Collider; template <> void RegisterUnityClass<Collider>(const char*);
class PhysicsManager; template <> void RegisterUnityClass<PhysicsManager>(const char*);
class Collider2D; template <> void RegisterUnityClass<Collider2D>(const char*);
class EdgeCollider2D; template <> void RegisterUnityClass<EdgeCollider2D>(const char*);
class Physics2DSettings; template <> void RegisterUnityClass<Physics2DSettings>(const char*);
class PolygonCollider2D; template <> void RegisterUnityClass<PolygonCollider2D>(const char*);
class PhysicsCoreProjectSettings2D; template <> void RegisterUnityClass<PhysicsCoreProjectSettings2D>(const char*);
class SpriteShapeRenderer; template <> void RegisterUnityClass<SpriteShapeRenderer>(const char*);
namespace TextRendering { class Font; } template <> void RegisterUnityClass<TextRendering::Font>(const char*);
class Tilemap; template <> void RegisterUnityClass<Tilemap>(const char*);
class TilemapRenderer; template <> void RegisterUnityClass<TilemapRenderer>(const char*);
namespace UI { class Canvas; } template <> void RegisterUnityClass<UI::Canvas>(const char*);
namespace UI { class CanvasGroup; } template <> void RegisterUnityClass<UI::CanvasGroup>(const char*);
namespace UI { class CanvasRenderer; } template <> void RegisterUnityClass<UI::CanvasRenderer>(const char*);
class PanelRenderer; template <> void RegisterUnityClass<PanelRenderer>(const char*);
class UIAnimationBinder; template <> void RegisterUnityClass<UIAnimationBinder>(const char*);
class UIAnimationClip; template <> void RegisterUnityClass<UIAnimationClip>(const char*);
class UIRenderer; template <> void RegisterUnityClass<UIRenderer>(const char*);

void RegisterAllClasses()
{
void RegisterBuiltinTypes();
RegisterBuiltinTypes();
	//Total: 81 non stripped classes
	//0. AnimationClip
	RegisterUnityClass<AnimationClip>("Animation");
	//1. Animator
	RegisterUnityClass<Animator>("Animation");
	//2. AnimatorController
	RegisterUnityClass<AnimatorController>("Animation");
	//3. AnimatorOverrideController
	RegisterUnityClass<AnimatorOverrideController>("Animation");
	//4. Motion
	RegisterUnityClass<Motion>("Animation");
	//5. RuntimeAnimatorController
	RegisterUnityClass<RuntimeAnimatorController>("Animation");
	//6. Behaviour
	RegisterUnityClass<Behaviour>("Core");
	//7. BuildSettings
	RegisterUnityClass<BuildSettings>("Core");
	//8. Camera
	RegisterUnityClass<Camera>("Core");
	//9. Component
	RegisterUnityClass<Unity::Component>("Core");
	//10. ComputeShader
	RegisterUnityClass<ComputeShader>("Core");
	//11. Cubemap
	RegisterUnityClass<Cubemap>("Core");
	//12. CubemapArray
	RegisterUnityClass<CubemapArray>("Core");
	//13. DelayedCallManager
	RegisterUnityClass<DelayedCallManager>("Core");
	//14. EditorExtension
	RegisterUnityClass<EditorExtension>("Core");
	//15. GameManager
	RegisterUnityClass<GameManager>("Core");
	//16. GameObject
	RegisterUnityClass<GameObject>("Core");
	//17. GlobalGameManager
	RegisterUnityClass<GlobalGameManager>("Core");
	//18. GraphicsSettings
	RegisterUnityClass<GraphicsSettings>("Core");
	//19. InputManager
	RegisterUnityClass<InputManager>("Core");
	//20. LevelGameManager
	RegisterUnityClass<LevelGameManager>("Core");
	//21. Light
	RegisterUnityClass<Light>("Core");
	//22. LightProbes
	RegisterUnityClass<LightProbes>("Core");
	//23. LightingSettings
	RegisterUnityClass<LightingSettings>("Core");
	//24. LightmapSettings
	RegisterUnityClass<LightmapSettings>("Core");
	//25. LowerResBlitTexture
	RegisterUnityClass<LowerResBlitTexture>("Core");
	//26. Material
	RegisterUnityClass<Material>("Core");
	//27. Mesh
	RegisterUnityClass<Mesh>("Core");
	//28. MeshFilter
	RegisterUnityClass<MeshFilter>("Core");
	//29. MeshRenderer
	RegisterUnityClass<MeshRenderer>("Core");
	//30. MonoBehaviour
	RegisterUnityClass<MonoBehaviour>("Core");
	//31. MonoManager
	RegisterUnityClass<MonoManager>("Core");
	//32. MonoScript
	RegisterUnityClass<MonoScript>("Core");
	//33. NamedObject
	RegisterUnityClass<NamedObject>("Core");
	//34. Object
	//Skipping Object
	//35. PlayerSettings
	RegisterUnityClass<PlayerSettings>("Core");
	//36. PreloadData
	RegisterUnityClass<PreloadData>("Core");
	//37. QualitySettings
	RegisterUnityClass<QualitySettings>("Core");
	//38. RayTracingShader
	RegisterUnityClass<RayTracingShader>("Core");
	//39. RectTransform
	RegisterUnityClass<UI::RectTransform>("Core");
	//40. ReflectionProbe
	RegisterUnityClass<ReflectionProbe>("Core");
	//41. RenderSettings
	RegisterUnityClass<RenderSettings>("Core");
	//42. RenderTexture
	RegisterUnityClass<RenderTexture>("Core");
	//43. Renderer
	RegisterUnityClass<Renderer>("Core");
	//44. ResourceManager
	RegisterUnityClass<ResourceManager>("Core");
	//45. RuntimeInitializeOnLoadManager
	RegisterUnityClass<RuntimeInitializeOnLoadManager>("Core");
	//46. Shader
	RegisterUnityClass<Shader>("Core");
	//47. ShaderNameRegistry
	RegisterUnityClass<ShaderNameRegistry>("Core");
	//48. Skybox
	RegisterUnityClass<Skybox>("Core");
	//49. SortingGroup
	RegisterUnityClass<SortingGroup>("Core");
	//50. Sprite
	RegisterUnityClass<Sprite>("Core");
	//51. SpriteAtlas
	RegisterUnityClass<SpriteAtlas>("Core");
	//52. SpriteRenderer
	RegisterUnityClass<SpriteRenderer>("Core");
	//53. TagManager
	RegisterUnityClass<TagManager>("Core");
	//54. TextAsset
	RegisterUnityClass<TextAsset>("Core");
	//55. Texture
	RegisterUnityClass<Texture>("Core");
	//56. Texture2D
	RegisterUnityClass<Texture2D>("Core");
	//57. Texture2DArray
	RegisterUnityClass<Texture2DArray>("Core");
	//58. Texture3D
	RegisterUnityClass<Texture3D>("Core");
	//59. TimeManager
	RegisterUnityClass<TimeManager>("Core");
	//60. Transform
	RegisterUnityClass<Transform>("Core");
	//61. GridLayout
	RegisterUnityClass<GridLayout>("Grid");
	//62. BoxCollider
	RegisterUnityClass<BoxCollider>("Physics");
	//63. Collider
	RegisterUnityClass<Collider>("Physics");
	//64. PhysicsManager
	RegisterUnityClass<PhysicsManager>("Physics");
	//65. Collider2D
	RegisterUnityClass<Collider2D>("Physics2D");
	//66. EdgeCollider2D
	RegisterUnityClass<EdgeCollider2D>("Physics2D");
	//67. Physics2DSettings
	RegisterUnityClass<Physics2DSettings>("Physics2D");
	//68. PolygonCollider2D
	RegisterUnityClass<PolygonCollider2D>("Physics2D");
	//69. PhysicsCoreProjectSettings2D
	RegisterUnityClass<PhysicsCoreProjectSettings2D>("PhysicsCore2D");
	//70. SpriteShapeRenderer
	RegisterUnityClass<SpriteShapeRenderer>("SpriteShape");
	//71. Font
	RegisterUnityClass<TextRendering::Font>("TextRendering");
	//72. Tilemap
	RegisterUnityClass<Tilemap>("Tilemap");
	//73. TilemapRenderer
	RegisterUnityClass<TilemapRenderer>("Tilemap");
	//74. Canvas
	RegisterUnityClass<UI::Canvas>("UI");
	//75. CanvasGroup
	RegisterUnityClass<UI::CanvasGroup>("UI");
	//76. CanvasRenderer
	RegisterUnityClass<UI::CanvasRenderer>("UI");
	//77. PanelRenderer
	RegisterUnityClass<PanelRenderer>("UIElements");
	//78. UIAnimationBinder
	RegisterUnityClass<UIAnimationBinder>("UIElements");
	//79. UIAnimationClip
	RegisterUnityClass<UIAnimationClip>("UIElements");
	//80. UIRenderer
	RegisterUnityClass<UIRenderer>("UIElements");

}
