#include "pch-cpp.hpp"





template <typename T1>
struct VirtualActionInvoker1Invoker;
template <typename T1>
struct VirtualActionInvoker1Invoker<T1*>
{
	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1* p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		void* params[1] = { p1 };
		invokeData.method->invoker_method(il2cpp_codegen_get_method_pointer(invokeData.method), invokeData.method, obj, params, params[0]);
	}
};
template <typename T1, typename T2>
struct VirtualActionInvoker2Invoker;
template <typename T1, typename T2>
struct VirtualActionInvoker2Invoker<T1*, T2*>
{
	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1* p1, T2* p2)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		void* params[2] = { p1, p2 };
		invokeData.method->invoker_method(il2cpp_codegen_get_method_pointer(invokeData.method), invokeData.method, obj, params, params[1]);
	}
};
template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*,const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj,invokeData.method);
	}
};
struct GenericInterfaceActionInvoker0
{
	typedef void (*Action)(void*,const RuntimeMethod*);

	static inline void Invoke (const RuntimeMethod* method, RuntimeObject* obj)
	{
		VirtualInvokeData invokeData;
		il2cpp_codegen_get_generic_interface_invoke_data(method, obj, &invokeData);
		((Action)invokeData.methodPtr)(obj,invokeData.method);
	}
};
template <typename T1, typename T2>
struct GenericInterfaceActionInvoker2
{
	typedef void (*Action)(void*,T1,T2,const RuntimeMethod*);

	static inline void Invoke (const RuntimeMethod* method, RuntimeObject* obj, T1 p1, T2 p2)
	{
		VirtualInvokeData invokeData;
		il2cpp_codegen_get_generic_interface_invoke_data(method, obj, &invokeData);
		((Action)invokeData.methodPtr)(obj,p1,p2,invokeData.method);
	}
};

struct Comparison_1_tF2EA38711BF4E71781108F0D7BD2B99D225BCE80;
struct Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588;
struct Dictionary_2_t6682243BDABA638FCBE4F1D9875B5B1455A7686E;
struct HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB;
struct HashSet_1_t0F16759FF079A28DCA2DCF39BA6A61B5D502A475;
struct IComparer_1_tFAD3AE9FE3CE1FB3CBB781C55DC57C986D71521E;
struct IEnumerable_1_t5C44357F6823EA78E35F0F1BD4086AA0F3408934;
struct IEnumerable_1_tD812E1F441800A1F228EE78FB23A163292743F77;
struct IEnumerable_1_t56F12DA073DC04074E2A46656A20FE93606A260A;
struct IEnumerable_1_tDA3DB14A8C53E0EFBEA9554E60CD303E733E672C;
struct IIndexedCollectionPropertyBagEnumerator_1_t218933170637CF5A1E8CB4453FFBF1668C543F16;
struct IIndexedCollectionPropertyBagEnumerator_1_t6B0CBDF76B7A74D14E91F0E734F3EC671A18EA12;
struct IIndexedCollectionPropertyBagEnumerator_1_t567D33BCD71C0C8FA81086D4821844FC9E402D9D;
struct IIndexedCollectionPropertyBagEnumerator_1_t10A3FE79120150E0A39D2649E6366925E3E7977A;
struct IPropertyBag_1_t36BB94A0E64BDC3C7B44E3C33CD0562C77748D3F;
struct KeyValuePair_2_tC24A74EF64A292F5C6BA77D0B04CD6620D2DE3AC;
struct LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E;
struct List_1_t90FE00BB2B99D64D0FC5C3B71BF96BC3FFA23AF9;
struct List_1_t4B846D5AA2F21AD00EE16A815E0984EC65E357A5;
struct List_1_t90F3642AD1F9EB0EFD8C9CA962DA5BF608F75F01;
struct List_1_t8C03D59AE9CBDEDECDE563570171B47DCB063CF4;
struct List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73;
struct List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD;
struct List_1_t41896DF347A129D13205BBFADE03DFB21E05E459;
struct List_1_t4A80BCCFB0BC8742C8BB601365DB07226750573A;
struct List_1_t70EE7982F45810D4B024CF720D910E67974A3094;
struct List_1_tB0B308D151FBF226396B20253C1FF11CF7D10D3A;
struct Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107;
struct PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104;
struct PriorityQueue_1_t9BB8C48DA8C00FA702EFB4995BC789C4EC77D2C4;
struct PropertyBag_1_t74F4963AD6B656900B7CACFC37AC3CDDDF818409;
struct RefComparison_1_tCC78A8CC5E113253AC71C9081E096010349AC8DC;
struct StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A;
struct Stack_1_t8C458D9E51035EBC322F988588DD27207004BF20;
struct Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A;
struct HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9;
struct KeyValuePair_2U5BU5D_tF8154B2302178CCE00D745DBF55F703880469DFC;
struct StackItemU5BU5D_t0E4DBF90E9CB89D0D5E2296D5E99067F4A4DC82F;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C;
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct InternedStringU5BU5D_t0B851758733FC0B118D84BE83AED10A0404C18D5;
struct NameAndParametersU5BU5D_tA6C2AC34ACDB1967A7A2CEF4BE1D717ADA695CA2;
struct NamedValueU5BU5D_tADD8F1373B88C55F68499688D72C21A97F63303A;
struct PropertyPathPartU5BU5D_t7994D542F14DDDDEABB1792C335C20149399AEBB;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
struct StyleValueHandleU5BU5D_t66B7732469E9E30B1FB9A6E386315DAB36914ADE;
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
struct __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979;
struct __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC;
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263;
struct ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129;
struct Awaitable_t690337FEC1C411606E233EA36A41337B931C23CB;
struct Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235;
struct Binding_tA1358A155852138C1926082E4F3245B6F631CBED;
struct Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3;
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
struct Entry_t475ED76E31923970E7F7A6522E570E7577C487B9;
struct EventConsumer_t6D08A21A3FF574758C289118775786B6D35754F3;
struct Font_tC95270EA3198038970422D78B74A7F2E218A96B6;
struct FontAsset_t61A6446D934E582651044E33D250EA8D306AB958;
struct Graphic_tCBFCA4585A19E2B75465AECFEAC43F4016BF7931;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct IPropertyBagVisitor_tE8B9B7E14DBF946E6E65BD0ECC8679268715BF5A;
struct ITypeVisitor_tCB238D641EEE5D928DA9FEF70EDB7C2C13ED1599;
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB;
struct LensFlareComponentSRP_tF068937A1EC9EB063AF855DB4994A1D2A5E3EFDC;
struct ManualResetEvent_t63959486AA41A113A4353D0BF4A68E77EBA0A158;
struct MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553;
struct Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4;
struct MeshHandle_tC1E9A7ECCFDAEFDE064B8D58B35B9CEE5A70A22E;
struct MethodInfo_t;
struct Page_tB4EA8095DF85BAF22AB8FCA71400121E721B57C9;
struct RenderData_t1ABE116B2B5E0409AC699E195922516606531DC2;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct SendOrPostCallback_t5C292A12062F24027A98492F52ECFE9802AA6F0E;
struct String_t;
struct StyleComplexSelector_tE46C29F65FDBA48D3152781187401C8B55B7D8AD;
struct StyleSheet_t6FAF43FCDB45BC6BED0522A222FD4C1A9BB10428;
struct TextAsset_t2C64E93DA366D9DE5A8209E1802FA4884AC1BD69;
struct Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700;
struct Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1;
struct Type_t;
struct UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7;
struct UxmlSerializedData_t5BDDB665835158F6BC2975E086891B337C3BB706;
struct VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115;
struct VisualTreeAsset_tFB5BF81F0780A412AE5A7C2C552B3EEA64EA2EEB;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
struct BindingData_t63DF4F2F4FA1BE1BD6130C2DDAE1888DCD76381A;
struct GPUBuffer_t0260DDC48C911E3B86729ABA0B6E63C064853BC3;

IL2CPP_EXTERN_C RuntimeClass* ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* PropertyBagStore_tF76E67D0FF5460D8B59B033B6EB4E2A2E446024C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Type_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral0074C49CE7D7ED9232C28459AA9DB19B1D06C223;
IL2CPP_EXTERN_C String_t* _stringLiteral146E57E580085C49FA4D7B226DB5FF8C1F7C5518;
IL2CPP_EXTERN_C String_t* _stringLiteralBFD0F59F9F38A92660652CA5BE172EFCA8501E47;
IL2CPP_EXTERN_C String_t* _stringLiteralC23725EE1ACD9996F8909AD0C76D39D2B4CB3ACA;
IL2CPP_EXTERN_C const RuntimeMethod* Array_Resize_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m6BAA7BD6F22421B894347B1476C37052FAC6C916_RuntimeMethod_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;
struct PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_com;
struct PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_pinvoke;
struct StyleValueHandle_t5831643AAA7AD8C5C43A4498C5E0A2545F78227D;

struct HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9;
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C;
struct __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB  : public RuntimeObject
{
	Il2CppSharedGenericObject* ____key;
	int32_t ____node;
};
struct PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104  : public RuntimeObject
{
	LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* ____leq;
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____nodes;
	HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* ____handles;
	int32_t ____size;
	int32_t ____max;
	int32_t ____freeList;
	bool ____initialized;
};
struct PriorityQueue_1_t9BB8C48DA8C00FA702EFB4995BC789C4EC77D2C4  : public RuntimeObject
{
	LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* ____leq;
	PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* ____heap;
	__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ____keys;
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____order;
	int32_t ____size;
	int32_t ____max;
	bool ____initialized;
};
struct StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A  : public RuntimeObject
{
	int32_t ___p;
	int32_t ___r;
};
struct Stack_1_t8C458D9E51035EBC322F988588DD27207004BF20  : public RuntimeObject
{
	StackItemU5BU5D_t0E4DBF90E9CB89D0D5E2296D5E99067F4A4DC82F* ____array;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A  : public RuntimeObject
{
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ____array;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct MemberInfo_t  : public RuntimeObject
{
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
typedef Il2CppFullySharedGenericStruct IndexedCollectionPropertyBagEnumerable_1_t2B3DE6D978823100869CD2618FBDF5807E3B997E;
typedef Il2CppFullySharedGenericStruct KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669;
struct NativeSlice_1_t135E2A23AA4EBDBCA846942F90F29A5004E8ACB5 
{
	uint8_t* ___m_Buffer;
	int32_t ___m_Stride;
	int32_t ___m_Length;
};
struct Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28 
{
	bool ___hasValue;
	int32_t ___value;
};
struct ProfilerCounterValue_1_tBDDA3FED817E45C8F5AEAFCA9A6AC487DEBF29B7 
{
	union
	{
		struct
		{
		};
		uint8_t ProfilerCounterValue_1_tBB35A6A34F978A9543846AE7275745F3C588928A__padding[1];
	};
};
struct ProfilerCounterValue_1_t83A85DB82AFFCFD2CF3AEF7012D7794FEDC031D7 
{
	union
	{
		struct
		{
		};
		uint8_t ProfilerCounterValue_1_tBB35A6A34F978A9543846AE7275745F3C588928A__padding[1];
	};
};
struct ReadOnlyArray_1_t1C2864D7CF4D444AB2616316AC8DD33932F77064 
{
	InternedStringU5BU5D_t0B851758733FC0B118D84BE83AED10A0404C18D5* ___m_Array;
	int32_t ___m_StartIndex;
	int32_t ___m_Length;
};
struct ReadOnlyArray_1_t1B44D48F2E9F425D218BABD5DE616117E8725D41 
{
	NameAndParametersU5BU5D_tA6C2AC34ACDB1967A7A2CEF4BE1D717ADA695CA2* ___m_Array;
	int32_t ___m_StartIndex;
	int32_t ___m_Length;
};
struct ReadOnlyArray_1_t4A15F7D15ACB297B45A08889D51E4CACEAD4EDF9 
{
	NamedValueU5BU5D_tADD8F1373B88C55F68499688D72C21A97F63303A* ___m_Array;
	int32_t ___m_StartIndex;
	int32_t ___m_Length;
};
struct Alloc_t78312CFE58F38082281E80E297AE6176BD2BD8AE 
{
	uint32_t ___start;
	uint32_t ___size;
	RuntimeObject* ___handle;
	bool ___shortLived;
};
struct Alloc_t78312CFE58F38082281E80E297AE6176BD2BD8AE_marshaled_pinvoke
{
	uint32_t ___start;
	uint32_t ___size;
	Il2CppIUnknown* ___handle;
	int32_t ___shortLived;
};
struct Alloc_t78312CFE58F38082281E80E297AE6176BD2BD8AE_marshaled_com
{
	uint32_t ___start;
	uint32_t ___size;
	Il2CppIUnknown* ___handle;
	int32_t ___shortLived;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
};
struct Color_tD001788D726C3A7F1379BEED0260B9591F440C1F 
{
	float ___r;
	float ___g;
	float ___b;
	float ___a;
};
struct Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B 
{
	union
	{
		#pragma pack(push, tp, 1)
		struct
		{
			int32_t ___rgba;
		};
		#pragma pack(pop, tp)
		struct
		{
			int32_t ___rgba_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			uint8_t ___r;
		};
		#pragma pack(pop, tp)
		struct
		{
			uint8_t ___r_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___g_OffsetPadding[1];
			uint8_t ___g;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___g_OffsetPadding_forAlignmentOnly[1];
			uint8_t ___g_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___b_OffsetPadding[2];
			uint8_t ___b;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___b_OffsetPadding_forAlignmentOnly[2];
			uint8_t ___b_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___a_OffsetPadding[3];
			uint8_t ___a;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___a_OffsetPadding_forAlignmentOnly[3];
			uint8_t ___a_forAlignmentOnly;
		};
	};
};
struct ColorId_t6B928BC1978476896DCF48B8577984FD37D89F9B 
{
	bool ___isValid;
	uint16_t ___id;
};
struct ColorId_t6B928BC1978476896DCF48B8577984FD37D89F9B_marshaled_pinvoke
{
	int32_t ___isValid;
	uint16_t ___id;
};
struct ColorId_t6B928BC1978476896DCF48B8577984FD37D89F9B_marshaled_com
{
	int32_t ___isValid;
	uint16_t ___id;
};
struct EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 
{
	union
	{
		struct
		{
			uint64_t ___m_rawData;
		};
		uint8_t EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8__padding[8];
	};
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2  : public ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_pinvoke
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_com
{
};
struct FourCC_tA6CAA4015BC25A7F1053B6C512202D57A9C994ED 
{
	int32_t ___m_Code;
};
struct InputDeviceMatcher_tF9BA551C8BB4AE41672366A7EBEA951543E0C555 
{
	KeyValuePair_2U5BU5D_tF8154B2302178CCE00D745DBF55F703880469DFC* ___m_Patterns;
};
struct InputDeviceMatcher_tF9BA551C8BB4AE41672366A7EBEA951543E0C555_marshaled_pinvoke
{
	KeyValuePair_2_tC24A74EF64A292F5C6BA77D0B04CD6620D2DE3AC* ___m_Patterns;
};
struct InputDeviceMatcher_tF9BA551C8BB4AE41672366A7EBEA951543E0C555_marshaled_com
{
	KeyValuePair_2_tC24A74EF64A292F5C6BA77D0B04CD6620D2DE3AC* ___m_Patterns;
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct InternedString_t8D62A48CB7D85AAE9CFCCCFB0A77AC2844905735 
{
	String_t* ___m_StringOriginalCase;
	String_t* ___m_StringLowerCase;
};
struct InternedString_t8D62A48CB7D85AAE9CFCCCFB0A77AC2844905735_marshaled_pinvoke
{
	char* ___m_StringOriginalCase;
	char* ___m_StringLowerCase;
};
struct InternedString_t8D62A48CB7D85AAE9CFCCCFB0A77AC2844905735_marshaled_com
{
	Il2CppChar* ___m_StringOriginalCase;
	Il2CppChar* ___m_StringLowerCase;
};
struct Matrix4x4_tDB70CF134A14BA38190C59AA700BCE10E2AED3E6 
{
	float ___m00;
	float ___m10;
	float ___m20;
	float ___m30;
	float ___m01;
	float ___m11;
	float ___m21;
	float ___m31;
	float ___m02;
	float ___m12;
	float ___m22;
	float ___m32;
	float ___m03;
	float ___m13;
	float ___m23;
	float ___m33;
};
struct PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F 
{
	int32_t ____handle;
};
#pragma pack(push, tp, 1)
struct ProfilerCategory_tA55212CD512C618AF6D2147791F20319896592AC 
{
	union
	{
		struct
		{
			union
			{
				#pragma pack(push, tp, 1)
				struct
				{
					uint16_t ___m_CategoryId;
				};
				#pragma pack(pop, tp)
				struct
				{
					uint16_t ___m_CategoryId_forAlignmentOnly;
				};
			};
		};
		uint8_t ProfilerCategory_tA55212CD512C618AF6D2147791F20319896592AC__padding[2];
	};
};
#pragma pack(pop, tp)
struct Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D 
{
	float ___m_XMin;
	float ___m_YMin;
	float ___m_Width;
	float ___m_Height;
};
struct RectInt_t1744D10E1063135DA9D574F95205B98DAC600CB8 
{
	int32_t ___m_XMin;
	int32_t ___m_YMin;
	int32_t ___m_Width;
	int32_t ___m_Height;
};
struct StyleSelectorMatch_tA4C195A8F23EE7E672CEE7042BA64189DBC2E6DE 
{
	StyleSheet_t6FAF43FCDB45BC6BED0522A222FD4C1A9BB10428* ___sheet;
	int32_t ___styleSheetIndexInStack;
	int32_t ___importedStyleSheetIndex;
	StyleComplexSelector_tE46C29F65FDBA48D3152781187401C8B55B7D8AD* ___complexSelector;
};
struct StyleSelectorMatch_tA4C195A8F23EE7E672CEE7042BA64189DBC2E6DE_marshaled_pinvoke
{
	StyleSheet_t6FAF43FCDB45BC6BED0522A222FD4C1A9BB10428* ___sheet;
	int32_t ___styleSheetIndexInStack;
	int32_t ___importedStyleSheetIndex;
	StyleComplexSelector_tE46C29F65FDBA48D3152781187401C8B55B7D8AD* ___complexSelector;
};
struct StyleSelectorMatch_tA4C195A8F23EE7E672CEE7042BA64189DBC2E6DE_marshaled_com
{
	StyleSheet_t6FAF43FCDB45BC6BED0522A222FD4C1A9BB10428* ___sheet;
	int32_t ___styleSheetIndexInStack;
	int32_t ___importedStyleSheetIndex;
	StyleComplexSelector_tE46C29F65FDBA48D3152781187401C8B55B7D8AD* ___complexSelector;
};
struct StyleVariable_t5D4DEC936102A13961F4F2C6214B83D6CDC56269 
{
	int32_t ___nameId;
	int32_t ___hash;
	StyleSheet_t6FAF43FCDB45BC6BED0522A222FD4C1A9BB10428* ___sheet;
	StyleValueHandleU5BU5D_t66B7732469E9E30B1FB9A6E386315DAB36914ADE* ___handles;
};
struct StyleVariable_t5D4DEC936102A13961F4F2C6214B83D6CDC56269_marshaled_pinvoke
{
	int32_t ___nameId;
	int32_t ___hash;
	StyleSheet_t6FAF43FCDB45BC6BED0522A222FD4C1A9BB10428* ___sheet;
	StyleValueHandle_t5831643AAA7AD8C5C43A4498C5E0A2545F78227D* ___handles;
};
struct StyleVariable_t5D4DEC936102A13961F4F2C6214B83D6CDC56269_marshaled_com
{
	int32_t ___nameId;
	int32_t ___hash;
	StyleSheet_t6FAF43FCDB45BC6BED0522A222FD4C1A9BB10428* ___sheet;
	StyleValueHandle_t5831643AAA7AD8C5C43A4498C5E0A2545F78227D* ___handles;
};
struct Substring_t2E16755269E6716C22074D6BC0A9099915E67849 
{
	String_t* ___m_String;
	int32_t ___m_Index;
	int32_t ___m_Length;
};
struct Substring_t2E16755269E6716C22074D6BC0A9099915E67849_marshaled_pinvoke
{
	char* ___m_String;
	int32_t ___m_Index;
	int32_t ___m_Length;
};
struct Substring_t2E16755269E6716C22074D6BC0A9099915E67849_marshaled_com
{
	Il2CppChar* ___m_String;
	int32_t ___m_Index;
	int32_t ___m_Length;
};
struct UILineInfo_tC6FF4F85BD2316FADA2148A1789B3FF0B05A6CAC 
{
	int32_t ___startCharIdx;
	int32_t ___height;
	float ___topY;
	float ___leading;
};
struct UInt32_t1833D51FFA667B18A5AA4B8D34DE284F8495D29B 
{
	uint32_t ___m_value;
};
struct UInt64_t8F12534CC8FC4B5860F2A2CD1EE79D322E7A41AF 
{
	uint64_t ___m_value;
};
struct UniqueStyleString_tE980AA033ABB00413A3139B42FF22750952DAECF 
{
	int32_t ___m_Id;
};
struct UsageHint_tFD7259AF9E91500CF1D0CE602394B40FD2397AB1 
{
	String_t* ___content;
};
struct UsageHint_tFD7259AF9E91500CF1D0CE602394B40FD2397AB1_marshaled_pinvoke
{
	char* ___content;
};
struct UsageHint_tFD7259AF9E91500CF1D0CE602394B40FD2397AB1_marshaled_com
{
	Il2CppChar* ___content;
};
struct UxmlNamespaceDefinition_t6B70CA125A4C1D5F74F1C901A6F4F72637F6621B 
{
	String_t* ___prefix;
	String_t* ___resolvedNamespace;
};
struct UxmlNamespaceDefinition_t6B70CA125A4C1D5F74F1C901A6F4F72637F6621B_marshaled_pinvoke
{
	char* ___prefix;
	char* ___resolvedNamespace;
};
struct UxmlNamespaceDefinition_t6B70CA125A4C1D5F74F1C901A6F4F72637F6621B_marshaled_com
{
	Il2CppChar* ___prefix;
	Il2CppChar* ___resolvedNamespace;
};
struct UxmlProperty_t98965D0AC619F23715A51F072A885C7F07F15FE4 
{
	String_t* ___name;
	String_t* ___value;
};
struct UxmlProperty_t98965D0AC619F23715A51F072A885C7F07F15FE4_marshaled_pinvoke
{
	char* ___name;
	char* ___value;
};
struct UxmlProperty_t98965D0AC619F23715A51F072A885C7F07F15FE4_marshaled_com
{
	Il2CppChar* ___name;
	Il2CppChar* ___value;
};
struct Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 
{
	float ___x;
	float ___y;
};
struct Vector2Int_t69B2886EBAB732D9B880565E18E7568F3DE0CE6A 
{
	int32_t ___m_X;
	int32_t ___m_Y;
};
struct Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 
{
	float ___x;
	float ___y;
	float ___z;
};
struct Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 
{
	float ___x;
	float ___y;
	float ___z;
	float ___w;
};
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};
struct AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 
{
	Awaitable_t690337FEC1C411606E233EA36A41337B931C23CB* ___U3CAwaitableU3Ek__BackingField;
	int32_t ___U3CFrameIndexU3Ek__BackingField;
};
struct AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_marshaled_pinvoke
{
	Awaitable_t690337FEC1C411606E233EA36A41337B931C23CB* ___U3CAwaitableU3Ek__BackingField;
	int32_t ___U3CFrameIndexU3Ek__BackingField;
};
struct AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_marshaled_com
{
	Awaitable_t690337FEC1C411606E233EA36A41337B931C23CB* ___U3CAwaitableU3Ek__BackingField;
	int32_t ___U3CFrameIndexU3Ek__BackingField;
};
struct OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 
{
	int32_t ___order;
	UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7* ___callback;
};
struct OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_marshaled_pinvoke
{
	int32_t ___order;
	Il2CppMethodPointer ___callback;
};
struct OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_marshaled_com
{
	int32_t ___order;
	Il2CppMethodPointer ___callback;
};
struct Page_t04FE552A388BF55B12C8868E19589136957E00A5 
{
	uint16_t ___x;
	uint16_t ___y;
	int32_t ___freeSlots;
};
struct AttributeOverrideRange_t47A9929769CAD72883980AE2C29FACC19B310D49 
{
	VisualTreeAsset_tFB5BF81F0780A412AE5A7C2C552B3EEA64EA2EEB* ___sourceAsset;
	List_1_t70EE7982F45810D4B024CF720D910E67974A3094* ___attributeOverrides;
};
struct AttributeOverrideRange_t47A9929769CAD72883980AE2C29FACC19B310D49_marshaled_pinvoke
{
	VisualTreeAsset_tFB5BF81F0780A412AE5A7C2C552B3EEA64EA2EEB* ___sourceAsset;
	List_1_t70EE7982F45810D4B024CF720D910E67974A3094* ___attributeOverrides;
};
struct AttributeOverrideRange_t47A9929769CAD72883980AE2C29FACC19B310D49_marshaled_com
{
	VisualTreeAsset_tFB5BF81F0780A412AE5A7C2C552B3EEA64EA2EEB* ___sourceAsset;
	List_1_t70EE7982F45810D4B024CF720D910E67974A3094* ___attributeOverrides;
};
struct SerializedDataOverrideRange_t3A1FFF9457378F017DD8F445C7AF421B9AB65014 
{
	VisualTreeAsset_tFB5BF81F0780A412AE5A7C2C552B3EEA64EA2EEB* ___sourceAsset;
	int32_t ___templateId;
	List_1_tB0B308D151FBF226396B20253C1FF11CF7D10D3A* ___attributeOverrides;
};
struct SerializedDataOverrideRange_t3A1FFF9457378F017DD8F445C7AF421B9AB65014_marshaled_pinvoke
{
	VisualTreeAsset_tFB5BF81F0780A412AE5A7C2C552B3EEA64EA2EEB* ___sourceAsset;
	int32_t ___templateId;
	List_1_tB0B308D151FBF226396B20253C1FF11CF7D10D3A* ___attributeOverrides;
};
struct SerializedDataOverrideRange_t3A1FFF9457378F017DD8F445C7AF421B9AB65014_marshaled_com
{
	VisualTreeAsset_tFB5BF81F0780A412AE5A7C2C552B3EEA64EA2EEB* ___sourceAsset;
	int32_t ___templateId;
	List_1_tB0B308D151FBF226396B20253C1FF11CF7D10D3A* ___attributeOverrides;
};
struct ChangesFromUI_t56B0B09645F7D36924555D832D21882400771022 
{
	int64_t ___version;
	Binding_tA1358A155852138C1926082E4F3245B6F631CBED* ___binding;
	BindingData_t63DF4F2F4FA1BE1BD6130C2DDAE1888DCD76381A* ___bindingData;
};
struct ChangesFromUI_t56B0B09645F7D36924555D832D21882400771022_marshaled_pinvoke
{
	int64_t ___version;
	Binding_tA1358A155852138C1926082E4F3245B6F631CBED* ___binding;
	BindingData_t63DF4F2F4FA1BE1BD6130C2DDAE1888DCD76381A* ___bindingData;
};
struct ChangesFromUI_t56B0B09645F7D36924555D832D21882400771022_marshaled_com
{
	int64_t ___version;
	Binding_tA1358A155852138C1926082E4F3245B6F631CBED* ___binding;
	BindingData_t63DF4F2F4FA1BE1BD6130C2DDAE1888DCD76381A* ___bindingData;
};
struct Range_tD224E9E0AED0FDE1D3F2FC611A81313E46779045 
{
	uint32_t ___start;
	uint32_t ___count;
};
struct AllocSize_t906784497648EDC3F45F4EA656C6858238BA2F7C 
{
	int32_t ___vertexCount;
	int32_t ___indexCount;
};
struct FocusedElement_t1EE083A1C5276213C533A38C6B5DC02E9DE5CBEF 
{
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___m_SubTreeRoot;
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___m_FocusedElement;
};
struct FocusedElement_t1EE083A1C5276213C533A38C6B5DC02E9DE5CBEF_marshaled_pinvoke
{
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___m_SubTreeRoot;
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___m_FocusedElement;
};
struct FocusedElement_t1EE083A1C5276213C533A38C6B5DC02E9DE5CBEF_marshaled_com
{
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___m_SubTreeRoot;
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___m_FocusedElement;
};
struct LensFlareCompInfo_tF75BC6BAED886C71425763DEA827F3794E718F37 
{
	int32_t ___index;
	LensFlareComponentSRP_tF068937A1EC9EB063AF855DB4994A1D2A5E3EFDC* ___comp;
};
struct LensFlareCompInfo_tF75BC6BAED886C71425763DEA827F3794E718F37_marshaled_pinvoke
{
	int32_t ___index;
	LensFlareComponentSRP_tF068937A1EC9EB063AF855DB4994A1D2A5E3EFDC* ___comp;
};
struct LensFlareCompInfo_tF75BC6BAED886C71425763DEA827F3794E718F37_marshaled_com
{
	int32_t ___index;
	LensFlareComponentSRP_tF068937A1EC9EB063AF855DB4994A1D2A5E3EFDC* ___comp;
};
struct NativeColorId_tC2A21D165C258872CC3EEB668F4CF4D08C37237A 
{
	int32_t ___isValid;
	uint16_t ___id;
	uint16_t ____pad;
};
struct BrickChunkAlloc_t95EB283E186F5DA1E74A8DDE415EB8E7ABFDF51B 
{
	int32_t ___x;
	int32_t ___y;
	int32_t ___z;
};
struct SerializedPerSceneCellList_tDF6654463D2F5052472326DE949ABE002ED92591 
{
	String_t* ___sceneGUID;
	List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* ___cellList;
};
struct SerializedPerSceneCellList_tDF6654463D2F5052472326DE949ABE002ED92591_marshaled_pinvoke
{
	char* ___sceneGUID;
	List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* ___cellList;
};
struct SerializedPerSceneCellList_tDF6654463D2F5052472326DE949ABE002ED92591_marshaled_com
{
	Il2CppChar* ___sceneGUID;
	List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* ___cellList;
};
struct ObsoletePerScenarioData_tB1EFC0C7B7EA6D0DD0CACC22888799725FC6787C 
{
	int32_t ___sceneHash;
	TextAsset_t2C64E93DA366D9DE5A8209E1802FA4884AC1BD69* ___cellDataAsset;
	TextAsset_t2C64E93DA366D9DE5A8209E1802FA4884AC1BD69* ___cellOptionalDataAsset;
};
struct ObsoletePerScenarioData_tB1EFC0C7B7EA6D0DD0CACC22888799725FC6787C_marshaled_pinvoke
{
	int32_t ___sceneHash;
	TextAsset_t2C64E93DA366D9DE5A8209E1802FA4884AC1BD69* ___cellDataAsset;
	TextAsset_t2C64E93DA366D9DE5A8209E1802FA4884AC1BD69* ___cellOptionalDataAsset;
};
struct ObsoletePerScenarioData_tB1EFC0C7B7EA6D0DD0CACC22888799725FC6787C_marshaled_com
{
	int32_t ___sceneHash;
	TextAsset_t2C64E93DA366D9DE5A8209E1802FA4884AC1BD69* ___cellDataAsset;
	TextAsset_t2C64E93DA366D9DE5A8209E1802FA4884AC1BD69* ___cellOptionalDataAsset;
};
struct StreamableCellDesc_t30C9692E80F3E2FD12CF286548763F9F4DE5048A 
{
	int32_t ___offset;
	int32_t ___elementCount;
};
struct SingleRange_tB50C1C2B62BDC445BDBA41FD3CDC77A45A211BBC 
{
	Il2CppChar ___First;
	Il2CppChar ___Last;
};
struct SingleRange_tB50C1C2B62BDC445BDBA41FD3CDC77A45A211BBC_marshaled_pinvoke
{
	uint8_t ___First;
	uint8_t ___Last;
};
struct SingleRange_tB50C1C2B62BDC445BDBA41FD3CDC77A45A211BBC_marshaled_com
{
	uint8_t ___First;
	uint8_t ___Last;
};
struct ElementInsertionData_tA691432209201F6E803ADDFD410186B673527959 
{
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___element;
	bool ___canceled;
};
struct ElementInsertionData_tA691432209201F6E803ADDFD410186B673527959_marshaled_pinvoke
{
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___element;
	int32_t ___canceled;
};
struct ElementInsertionData_tA691432209201F6E803ADDFD410186B673527959_marshaled_com
{
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___element;
	int32_t ___canceled;
};
struct TransformData_t1EB326D4F0A7D2FABB34DAB3D379349B5F623646 
{
	String_t* ___fullName;
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___transform;
};
struct TransformData_t1EB326D4F0A7D2FABB34DAB3D379349B5F623646_marshaled_pinvoke
{
	char* ___fullName;
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___transform;
};
struct TransformData_t1EB326D4F0A7D2FABB34DAB3D379349B5F623646_marshaled_com
{
	Il2CppChar* ___fullName;
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___transform;
};
struct AttributeOverride_t58F1DF22E69714D48ECBEEAD266D443A858BADEF 
{
	String_t* ___m_ElementName;
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ___m_NamesPath;
	String_t* ___m_AttributeName;
	String_t* ___m_Value;
};
struct AttributeOverride_t58F1DF22E69714D48ECBEEAD266D443A858BADEF_marshaled_pinvoke
{
	char* ___m_ElementName;
	char** ___m_NamesPath;
	char* ___m_AttributeName;
	char* ___m_Value;
};
struct AttributeOverride_t58F1DF22E69714D48ECBEEAD266D443A858BADEF_marshaled_com
{
	Il2CppChar* ___m_ElementName;
	Il2CppChar** ___m_NamesPath;
	Il2CppChar* ___m_AttributeName;
	Il2CppChar* ___m_Value;
};
struct UxmlSerializedDataOverride_t612A2DF19F1F3AF231244900F30A331CFB9CAD67 
{
	int32_t ___m_ElementId;
	List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* ___m_ElementIdsPath;
	UxmlSerializedData_t5BDDB665835158F6BC2975E086891B337C3BB706* ___m_SerializedData;
};
struct UxmlSerializedDataOverride_t612A2DF19F1F3AF231244900F30A331CFB9CAD67_marshaled_pinvoke
{
	int32_t ___m_ElementId;
	List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* ___m_ElementIdsPath;
	UxmlSerializedData_t5BDDB665835158F6BC2975E086891B337C3BB706* ___m_SerializedData;
};
struct UxmlSerializedDataOverride_t612A2DF19F1F3AF231244900F30A331CFB9CAD67_marshaled_com
{
	int32_t ___m_ElementId;
	List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* ___m_ElementIdsPath;
	UxmlSerializedData_t5BDDB665835158F6BC2975E086891B337C3BB706* ___m_SerializedData;
};
struct FontReferenceMap_t1C0CECF3F0F650BE4A881A50A25EFB26965E7831 
{
	Font_tC95270EA3198038970422D78B74A7F2E218A96B6* ___font;
	FontAsset_t61A6446D934E582651044E33D250EA8D306AB958* ___fontAsset;
};
struct FontReferenceMap_t1C0CECF3F0F650BE4A881A50A25EFB26965E7831_marshaled_pinvoke
{
	Font_tC95270EA3198038970422D78B74A7F2E218A96B6* ___font;
	FontAsset_t61A6446D934E582651044E33D250EA8D306AB958* ___fontAsset;
};
struct FontReferenceMap_t1C0CECF3F0F650BE4A881A50A25EFB26965E7831_marshaled_com
{
	Font_tC95270EA3198038970422D78B74A7F2E218A96B6* ___font;
	FontAsset_t61A6446D934E582651044E33D250EA8D306AB958* ___fontAsset;
};
struct TextureInfo_t581C305A0444F786E0E7405054714685BE3A5A5B 
{
	Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* ___texture;
	bool ___dynamic;
	int32_t ___refCount;
};
struct TextureInfo_t581C305A0444F786E0E7405054714685BE3A5A5B_marshaled_pinvoke
{
	Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* ___texture;
	int32_t ___dynamic;
	int32_t ___refCount;
};
struct TextureInfo_t581C305A0444F786E0E7405054714685BE3A5A5B_marshaled_com
{
	Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* ___texture;
	int32_t ___dynamic;
	int32_t ___refCount;
};
struct WorkRequest_t8AF542F2E248D9234341817CDB5F76C27D348B44 
{
	SendOrPostCallback_t5C292A12062F24027A98492F52ECFE9802AA6F0E* ___m_DelagateCallback;
	RuntimeObject* ___m_DelagateState;
	ManualResetEvent_t63959486AA41A113A4353D0BF4A68E77EBA0A158* ___m_WaitHandle;
};
struct WorkRequest_t8AF542F2E248D9234341817CDB5F76C27D348B44_marshaled_pinvoke
{
	Il2CppMethodPointer ___m_DelagateCallback;
	Il2CppIUnknown* ___m_DelagateState;
	ManualResetEvent_t63959486AA41A113A4353D0BF4A68E77EBA0A158* ___m_WaitHandle;
};
struct WorkRequest_t8AF542F2E248D9234341817CDB5F76C27D348B44_marshaled_com
{
	Il2CppMethodPointer ___m_DelagateCallback;
	Il2CppIUnknown* ___m_DelagateState;
	ManualResetEvent_t63959486AA41A113A4353D0BF4A68E77EBA0A158* ___m_WaitHandle;
};
struct SlotDefinition_t2E39E965BBE5A336DD1B93A115DD01044D1A66F8 
{
	String_t* ___name;
	int32_t ___insertionPointId;
};
struct SlotDefinition_t2E39E965BBE5A336DD1B93A115DD01044D1A66F8_marshaled_pinvoke
{
	char* ___name;
	int32_t ___insertionPointId;
};
struct SlotDefinition_t2E39E965BBE5A336DD1B93A115DD01044D1A66F8_marshaled_com
{
	Il2CppChar* ___name;
	int32_t ___insertionPointId;
};
struct SlotUsageEntry_t73A628038C799E4FD44436E093EC19D2B9EA1B76 
{
	String_t* ___slotName;
	int32_t ___assetId;
};
struct SlotUsageEntry_t73A628038C799E4FD44436E093EC19D2B9EA1B76_marshaled_pinvoke
{
	char* ___slotName;
	int32_t ___assetId;
};
struct SlotUsageEntry_t73A628038C799E4FD44436E093EC19D2B9EA1B76_marshaled_com
{
	Il2CppChar* ___slotName;
	int32_t ___assetId;
};
struct UsingEntry_t0454AD34026FDFD1733CE07BD4AE807B0FBCE484 
{
	String_t* ___alias;
	String_t* ___path;
	VisualTreeAsset_tFB5BF81F0780A412AE5A7C2C552B3EEA64EA2EEB* ___asset;
};
struct UsingEntry_t0454AD34026FDFD1733CE07BD4AE807B0FBCE484_marshaled_pinvoke
{
	char* ___alias;
	char* ___path;
	VisualTreeAsset_tFB5BF81F0780A412AE5A7C2C552B3EEA64EA2EEB* ___asset;
};
struct UsingEntry_t0454AD34026FDFD1733CE07BD4AE807B0FBCE484_marshaled_com
{
	Il2CppChar* ___alias;
	Il2CppChar* ___path;
	VisualTreeAsset_tFB5BF81F0780A412AE5A7C2C552B3EEA64EA2EEB* ___asset;
};
struct VersionInfo_tD8EF94E6EB7FCB9167CE193F9FFAAB6DE0518DE8 
{
	RuntimeObject* ___source;
	int64_t ___version;
};
struct VersionInfo_tD8EF94E6EB7FCB9167CE193F9FFAAB6DE0518DE8_marshaled_pinvoke
{
	Il2CppIUnknown* ___source;
	int64_t ___version;
};
struct VersionInfo_tD8EF94E6EB7FCB9167CE193F9FFAAB6DE0518DE8_marshaled_com
{
	Il2CppIUnknown* ___source;
	int64_t ___version;
};
struct EnumeratorType_tF9B38F775993F372FAFD0B91B3CAF0EA72BBBAD2 
{
	int32_t ___value__;
};
struct EnumeratorType_t485827C413BC30C2D7020F107121D70CB8AB8F69 
{
	int32_t ___value__;
};
struct EnumeratorType_t2CF2CC28C8A83C948ED3D7CCBC095EE010D23786 
{
	int32_t ___value__;
};
struct EnumeratorType_t213060DC702D8C5D779B12D7E6CE6AFC2D7BB6CA 
{
	int32_t ___value__;
};
typedef Il2CppFullySharedGenericStruct IndexedCollectionPropertyBagEnumerable_1_tF48235F7DD263F1A98B33EBEFC4E192DAC3E2DE8;
struct LazyLoadReference_1_tD57A582D5A28F3AE34FBB810985EF171325FAF1B 
{
	EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 ___m_EntityId;
};
struct Align_t293AAB5F8D4239F304F48DA84CAAB4071C78902C 
{
	int32_t ___value__;
};
struct Allocator_t996642592271AAD9EE688F142741D512C07B5824 
{
	int32_t ___value__;
};
struct AnimationPlayState_t0379C839FFED72546E418CB2F8721A8FF574F944 
{
	int32_t ___value__;
};
struct BackgroundPositionKeyword_tE680A05B983D256AADC8E2CF1CA169D004B8641B 
{
	int32_t ___value__;
};
struct Delegate_t  : public RuntimeObject
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	RuntimeObject* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	bool ___method_is_virtual;
};
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct DrawPhase_t486494E09E3D14AEF7D5544440BD869D73BB7DA7 
{
	uint8_t ___value__;
};
struct Exception_t  : public RuntimeObject
{
	String_t* ____className;
	String_t* ____message;
	RuntimeObject* ____data;
	Exception_t* ____innerException;
	String_t* ____helpURL;
	RuntimeObject* ____stackTrace;
	String_t* ____stackTraceString;
	String_t* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	RuntimeObject* ____dynamicMethods;
	int32_t ____HResult;
	String_t* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_pinvoke
{
	char* ____className;
	char* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_pinvoke* ____innerException;
	char* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	char* ____stackTraceString;
	char* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	char* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className;
	Il2CppChar* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_com* ____innerException;
	Il2CppChar* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	Il2CppChar* ____stackTraceString;
	Il2CppChar* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	Il2CppChar* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct FeatureType_t794EFAD24C687FA0CDC5A313581A39F5CB61CC41 
{
	int32_t ___value__;
};
struct GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC 
{
	intptr_t ___handle;
};
struct InstantiationKind_t9B77929786BCA193B4A916F2F25793598CF0DF7D 
{
	int32_t ___value__;
};
struct LayoutUnit_tF18EC17FE8588A01C72784546410EA0D9B1D2F22 
{
	int32_t ___value__;
};
struct ProfilerCounterOptions_tA8DF3BEB57016991677DF3D3CF3B8D61F3378A52 
{
	uint16_t ___value__;
};
struct ProfilerMarkerDataUnit_t541C3F1456660AC98D107E8F64DA0E613A2F7753 
{
	uint8_t ___value__;
};
struct PropertyPathPartKind_t82152825D88A0E450DDCE8503272A10595047F87 
{
	int32_t ___value__;
};
struct Repeat_tC0330B75B12D24B063BA5151AF3BB73B85D8B840 
{
	int32_t ___value__;
};
struct RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B 
{
	intptr_t ___value;
};
struct ScaleMode_t16AD656758EE54C56B3DA34FE4F2033C9C2EE13D 
{
	int32_t ___value__;
};
struct StyleKeyword_t2812E72266C15CBA8927586972DC2FD27B10E705 
{
	int32_t ___value__;
};
struct StylePropertyId_tA3B8A5213F5BA43F9C5443B27B165D744713BE69 
{
	int32_t ___value__;
};
struct StyleSelectorType_t425962DE6D175F785FA2B5554D793B71D39430A3 
{
	int32_t ___value__;
};
struct StyleSyntaxTokenType_tFB5906557ADB62467788C6C7F28D771374EC4834 
{
	int32_t ___value__;
};
struct StyleValueType_tC3253FE046DBB95224A74D13B534D015CC4AADDE 
{
	int32_t ___value__;
};
struct TimeUnit_t56A79CDB672E98A4EE28002BD23B6D5E0BAA2649 
{
	int32_t ___value__;
};
struct TypeCode_tBEF9BE86C8BCF5A6B82F3381219738D27804EF79 
{
	int32_t ___value__;
};
struct UICharInfo_t24C2EA0F2F3A938100C271891D9DEB015ABA5FBD 
{
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___cursorPos;
	float ___charWidth;
};
struct UIVertex_tF5C663F4BBC786C9D56C28016FF66E6C6BF85207 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___position;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___normal;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___tangent;
	Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B ___color;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___uv0;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___uv1;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___uv2;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___uv3;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___prevPosition;
};
struct VertexAttribute_tF34C1B76F20CA4AEC9D606BCD37A8A0C4A24C9A6 
{
	int32_t ___value__;
};
struct VertexAttributeFormat_tD714C51E671502B116ACE5E23F042BA80649D32F 
{
	int32_t ___value__;
};
struct XRView_t70E23918C68E24DB22A8614AA8717B3BA213FAB5 
{
	Matrix4x4_tDB70CF134A14BA38190C59AA700BCE10E2AED3E6 ___projMatrix;
	Matrix4x4_tDB70CF134A14BA38190C59AA700BCE10E2AED3E6 ___viewMatrix;
	Matrix4x4_tDB70CF134A14BA38190C59AA700BCE10E2AED3E6 ___prevViewMatrix;
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___viewport;
	Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4* ___occlusionMesh;
	Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4* ___visibleMesh;
	int32_t ___textureArraySlice;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___eyeCenterUV;
	bool ___isPrevViewMatrixValid;
};
struct XRView_t70E23918C68E24DB22A8614AA8717B3BA213FAB5_marshaled_pinvoke
{
	Matrix4x4_tDB70CF134A14BA38190C59AA700BCE10E2AED3E6 ___projMatrix;
	Matrix4x4_tDB70CF134A14BA38190C59AA700BCE10E2AED3E6 ___viewMatrix;
	Matrix4x4_tDB70CF134A14BA38190C59AA700BCE10E2AED3E6 ___prevViewMatrix;
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___viewport;
	Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4* ___occlusionMesh;
	Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4* ___visibleMesh;
	int32_t ___textureArraySlice;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___eyeCenterUV;
	int32_t ___isPrevViewMatrixValid;
};
struct XRView_t70E23918C68E24DB22A8614AA8717B3BA213FAB5_marshaled_com
{
	Matrix4x4_tDB70CF134A14BA38190C59AA700BCE10E2AED3E6 ___projMatrix;
	Matrix4x4_tDB70CF134A14BA38190C59AA700BCE10E2AED3E6 ___viewMatrix;
	Matrix4x4_tDB70CF134A14BA38190C59AA700BCE10E2AED3E6 ___prevViewMatrix;
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___viewport;
	Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4* ___occlusionMesh;
	Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4* ___visibleMesh;
	int32_t ___textureArraySlice;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___eyeCenterUV;
	int32_t ___isPrevViewMatrixValid;
};
struct Type_tDE4005CDD70E001C9402FEE8D2F2431154FED092 
{
	int32_t ___value__;
};
struct Registration_t4882E3A82099DD20B2E91917C08BD4D2216F21D7 
{
	EventConsumer_t6D08A21A3FF574758C289118775786B6D35754F3* ___handler;
	int32_t ___priority;
	Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28 ___playerId;
	HashSet_1_t0F16759FF079A28DCA2DCF39BA6A61B5D502A475* ____types;
};
struct Registration_t4882E3A82099DD20B2E91917C08BD4D2216F21D7_marshaled_pinvoke
{
	Il2CppMethodPointer ___handler;
	int32_t ___priority;
	Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28 ___playerId;
	HashSet_1_t0F16759FF079A28DCA2DCF39BA6A61B5D502A475* ____types;
};
struct Registration_t4882E3A82099DD20B2E91917C08BD4D2216F21D7_marshaled_com
{
	Il2CppMethodPointer ___handler;
	int32_t ___priority;
	Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28 ___playerId;
	HashSet_1_t0F16759FF079A28DCA2DCF39BA6A61B5D502A475* ____types;
};
struct CopyInfo_t633133D45AECBBD5429A958816DD143830F31B29 
{
	void* ___srcCpuBuffer;
	GPUBuffer_t0260DDC48C911E3B86729ABA0B6E63C064853BC3* ___dstGpuBuffer;
	NativeSlice_1_t135E2A23AA4EBDBCA846942F90F29A5004E8ACB5 ___pendingGpuCopies;
};
struct CopyInfo_t633133D45AECBBD5429A958816DD143830F31B29_marshaled_pinvoke
{
	void* ___srcCpuBuffer;
	GPUBuffer_t0260DDC48C911E3B86729ABA0B6E63C064853BC3* ___dstGpuBuffer;
	NativeSlice_1_t135E2A23AA4EBDBCA846942F90F29A5004E8ACB5 ___pendingGpuCopies;
};
struct CopyInfo_t633133D45AECBBD5429A958816DD143830F31B29_marshaled_com
{
	void* ___srcCpuBuffer;
	GPUBuffer_t0260DDC48C911E3B86729ABA0B6E63C064853BC3* ___dstGpuBuffer;
	NativeSlice_1_t135E2A23AA4EBDBCA846942F90F29A5004E8ACB5 ___pendingGpuCopies;
};
struct HIDCollectionType_t7AC7F0F059AEAEEFBD54D5F4DB1DFCB85254C854 
{
	int32_t ___value__;
};
struct HIDElementFlags_t3C41C626435310A0F354E862D4747C2369E8720C 
{
	int32_t ___value__;
};
struct HIDReportType_t37249B7677BD7083BEB296DEC42B8A0C0ADE562F 
{
	int32_t ___value__;
};
struct UsagePage_t6EF545F107AB1DED99B07C377BD0B8457DC6F7B8 
{
	int32_t ___value__;
};
struct JsonString_tE22CDDA995FEFF514F3F334C93B6AB31B49773CB 
{
	Substring_t2E16755269E6716C22074D6BC0A9099915E67849 ___text;
	bool ___hasEscapes;
};
struct JsonString_tE22CDDA995FEFF514F3F334C93B6AB31B49773CB_marshaled_pinvoke
{
	Substring_t2E16755269E6716C22074D6BC0A9099915E67849_marshaled_pinvoke ___text;
	int32_t ___hasEscapes;
};
struct JsonString_tE22CDDA995FEFF514F3F334C93B6AB31B49773CB_marshaled_com
{
	Substring_t2E16755269E6716C22074D6BC0A9099915E67849_marshaled_com ___text;
	int32_t ___hasEscapes;
};
struct JsonValueType_t36BA339F107E5E9C0966C45F896E27F3BCC5A2AB 
{
	int32_t ___value__;
};
struct BorderParams_tA3F0684BE19E681B68F40A773737978EB3FD1509 
{
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___rect;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___playmodeTintColor;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___leftColor;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___topColor;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___rightColor;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___bottomColor;
	float ___leftWidth;
	float ___topWidth;
	float ___rightWidth;
	float ___bottomWidth;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___topLeftRadius;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___topRightRadius;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___bottomRightRadius;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___bottomLeftRadius;
	ColorId_t6B928BC1978476896DCF48B8577984FD37D89F9B ___leftColorId;
	ColorId_t6B928BC1978476896DCF48B8577984FD37D89F9B ___topColorId;
	ColorId_t6B928BC1978476896DCF48B8577984FD37D89F9B ___rightColorId;
	ColorId_t6B928BC1978476896DCF48B8577984FD37D89F9B ___bottomColorId;
};
struct BorderParams_tA3F0684BE19E681B68F40A773737978EB3FD1509_marshaled_pinvoke
{
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___rect;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___playmodeTintColor;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___leftColor;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___topColor;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___rightColor;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___bottomColor;
	float ___leftWidth;
	float ___topWidth;
	float ___rightWidth;
	float ___bottomWidth;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___topLeftRadius;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___topRightRadius;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___bottomRightRadius;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___bottomLeftRadius;
	ColorId_t6B928BC1978476896DCF48B8577984FD37D89F9B_marshaled_pinvoke ___leftColorId;
	ColorId_t6B928BC1978476896DCF48B8577984FD37D89F9B_marshaled_pinvoke ___topColorId;
	ColorId_t6B928BC1978476896DCF48B8577984FD37D89F9B_marshaled_pinvoke ___rightColorId;
	ColorId_t6B928BC1978476896DCF48B8577984FD37D89F9B_marshaled_pinvoke ___bottomColorId;
};
struct BorderParams_tA3F0684BE19E681B68F40A773737978EB3FD1509_marshaled_com
{
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___rect;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___playmodeTintColor;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___leftColor;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___topColor;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___rightColor;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___bottomColor;
	float ___leftWidth;
	float ___topWidth;
	float ___rightWidth;
	float ___bottomWidth;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___topLeftRadius;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___topRightRadius;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___bottomRightRadius;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___bottomLeftRadius;
	ColorId_t6B928BC1978476896DCF48B8577984FD37D89F9B_marshaled_com ___leftColorId;
	ColorId_t6B928BC1978476896DCF48B8577984FD37D89F9B_marshaled_com ___topColorId;
	ColorId_t6B928BC1978476896DCF48B8577984FD37D89F9B_marshaled_com ___rightColorId;
	ColorId_t6B928BC1978476896DCF48B8577984FD37D89F9B_marshaled_com ___bottomColorId;
};
struct RepeatRectUV_tD14FA437CD80FC82B2ED78B34A201F3C91416A7C 
{
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___rect;
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___uv;
};
struct AllocToFree_tE4DBFDF44B21D35677B13570BA217FDAE35476CB 
{
	Alloc_t78312CFE58F38082281E80E297AE6176BD2BD8AE ___alloc;
	Page_tB4EA8095DF85BAF22AB8FCA71400121E721B57C9* ___page;
	bool ___vertices;
};
struct AllocToFree_tE4DBFDF44B21D35677B13570BA217FDAE35476CB_marshaled_pinvoke
{
	Alloc_t78312CFE58F38082281E80E297AE6176BD2BD8AE_marshaled_pinvoke ___alloc;
	Page_tB4EA8095DF85BAF22AB8FCA71400121E721B57C9* ___page;
	int32_t ___vertices;
};
struct AllocToFree_tE4DBFDF44B21D35677B13570BA217FDAE35476CB_marshaled_com
{
	Alloc_t78312CFE58F38082281E80E297AE6176BD2BD8AE_marshaled_com ___alloc;
	Page_tB4EA8095DF85BAF22AB8FCA71400121E721B57C9* ___page;
	int32_t ___vertices;
};
struct AllocToUpdate_t7E5493AF0DAEBF6C8D1E005C93179042069A46A3 
{
	uint32_t ___id;
	uint32_t ___allocTime;
	MeshHandle_tC1E9A7ECCFDAEFDE064B8D58B35B9CEE5A70A22E* ___meshHandle;
	Alloc_t78312CFE58F38082281E80E297AE6176BD2BD8AE ___permAllocVerts;
	Alloc_t78312CFE58F38082281E80E297AE6176BD2BD8AE ___permAllocIndices;
	Page_tB4EA8095DF85BAF22AB8FCA71400121E721B57C9* ___permPage;
	bool ___copyBackIndices;
};
struct AllocToUpdate_t7E5493AF0DAEBF6C8D1E005C93179042069A46A3_marshaled_pinvoke
{
	uint32_t ___id;
	uint32_t ___allocTime;
	MeshHandle_tC1E9A7ECCFDAEFDE064B8D58B35B9CEE5A70A22E* ___meshHandle;
	Alloc_t78312CFE58F38082281E80E297AE6176BD2BD8AE_marshaled_pinvoke ___permAllocVerts;
	Alloc_t78312CFE58F38082281E80E297AE6176BD2BD8AE_marshaled_pinvoke ___permAllocIndices;
	Page_tB4EA8095DF85BAF22AB8FCA71400121E721B57C9* ___permPage;
	int32_t ___copyBackIndices;
};
struct AllocToUpdate_t7E5493AF0DAEBF6C8D1E005C93179042069A46A3_marshaled_com
{
	uint32_t ___id;
	uint32_t ___allocTime;
	MeshHandle_tC1E9A7ECCFDAEFDE064B8D58B35B9CEE5A70A22E* ___meshHandle;
	Alloc_t78312CFE58F38082281E80E297AE6176BD2BD8AE_marshaled_com ___permAllocVerts;
	Alloc_t78312CFE58F38082281E80E297AE6176BD2BD8AE_marshaled_com ___permAllocIndices;
	Page_tB4EA8095DF85BAF22AB8FCA71400121E721B57C9* ___permPage;
	int32_t ___copyBackIndices;
};
struct ObsoleteSerializablePerScenarioDataItem_t4DB1ADB8C2522C62B43309AF1874419D8572EEB7 
{
	String_t* ___scenario;
	ObsoletePerScenarioData_tB1EFC0C7B7EA6D0DD0CACC22888799725FC6787C ___data;
};
struct ObsoleteSerializablePerScenarioDataItem_t4DB1ADB8C2522C62B43309AF1874419D8572EEB7_marshaled_pinvoke
{
	char* ___scenario;
	ObsoletePerScenarioData_tB1EFC0C7B7EA6D0DD0CACC22888799725FC6787C_marshaled_pinvoke ___data;
};
struct ObsoleteSerializablePerScenarioDataItem_t4DB1ADB8C2522C62B43309AF1874419D8572EEB7_marshaled_com
{
	Il2CppChar* ___scenario;
	ObsoletePerScenarioData_tB1EFC0C7B7EA6D0DD0CACC22888799725FC6787C_marshaled_com ___data;
};
struct BlitInfo_t6D4C0580BBEF65F5EAD39FB6DBC85F360CF6A357 
{
	Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* ___src;
	RectInt_t1744D10E1063135DA9D574F95205B98DAC600CB8 ___srcRect;
	Vector2Int_t69B2886EBAB732D9B880565E18E7568F3DE0CE6A ___dstPos;
	int32_t ___border;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___tint;
};
struct BlitInfo_t6D4C0580BBEF65F5EAD39FB6DBC85F360CF6A357_marshaled_pinvoke
{
	Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* ___src;
	RectInt_t1744D10E1063135DA9D574F95205B98DAC600CB8 ___srcRect;
	Vector2Int_t69B2886EBAB732D9B880565E18E7568F3DE0CE6A ___dstPos;
	int32_t ___border;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___tint;
};
struct BlitInfo_t6D4C0580BBEF65F5EAD39FB6DBC85F360CF6A357_marshaled_com
{
	Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* ___src;
	RectInt_t1744D10E1063135DA9D574F95205B98DAC600CB8 ___srcRect;
	Vector2Int_t69B2886EBAB732D9B880565E18E7568F3DE0CE6A ___dstPos;
	int32_t ___border;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___tint;
};
struct RaycastHitData_t5EAB266730389AB27715962670C2512800451841 
{
	Graphic_tCBFCA4585A19E2B75465AECFEAC43F4016BF7931* ___U3CgraphicU3Ek__BackingField;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___U3CworldHitPositionU3Ek__BackingField;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___U3CscreenPositionU3Ek__BackingField;
	float ___U3CdistanceU3Ek__BackingField;
};
struct RaycastHitData_t5EAB266730389AB27715962670C2512800451841_marshaled_pinvoke
{
	Graphic_tCBFCA4585A19E2B75465AECFEAC43F4016BF7931* ___U3CgraphicU3Ek__BackingField;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___U3CworldHitPositionU3Ek__BackingField;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___U3CscreenPositionU3Ek__BackingField;
	float ___U3CdistanceU3Ek__BackingField;
};
struct RaycastHitData_t5EAB266730389AB27715962670C2512800451841_marshaled_com
{
	Graphic_tCBFCA4585A19E2B75465AECFEAC43F4016BF7931* ___U3CgraphicU3Ek__BackingField;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___U3CworldHitPositionU3Ek__BackingField;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___U3CscreenPositionU3Ek__BackingField;
	float ___U3CdistanceU3Ek__BackingField;
};
struct SettingsOptions_t935A71692F21EE9D959FCAC00574AA018F8BA2CB 
{
	uint16_t ___value__;
};
struct LayoutMatcher_t0D8F5E32A7C82BC292EBA0FAA3EB2D9F1F72A0A6 
{
	InternedString_t8D62A48CB7D85AAE9CFCCCFB0A77AC2844905735 ___layoutName;
	InputDeviceMatcher_tF9BA551C8BB4AE41672366A7EBEA951543E0C555 ___deviceMatcher;
};
struct LayoutMatcher_t0D8F5E32A7C82BC292EBA0FAA3EB2D9F1F72A0A6_marshaled_pinvoke
{
	InternedString_t8D62A48CB7D85AAE9CFCCCFB0A77AC2844905735_marshaled_pinvoke ___layoutName;
	InputDeviceMatcher_tF9BA551C8BB4AE41672366A7EBEA951543E0C555_marshaled_pinvoke ___deviceMatcher;
};
struct LayoutMatcher_t0D8F5E32A7C82BC292EBA0FAA3EB2D9F1F72A0A6_marshaled_com
{
	InternedString_t8D62A48CB7D85AAE9CFCCCFB0A77AC2844905735_marshaled_com ___layoutName;
	InputDeviceMatcher_tF9BA551C8BB4AE41672366A7EBEA951543E0C555_marshaled_com ___deviceMatcher;
};
struct Flags_t36D5070200D1061BA68BF41808748FA000DEE57C 
{
	int32_t ___value__;
};
struct VisualsProcessingType_t19983E0076886C15ED2761057B9992989A710ECC 
{
	int32_t ___value__;
};
struct NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct PropertyBag_1_t9B0D7D63F90D24F16F87AF8413E98E8C803A81B9  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tDEB93F9DA6A09C131EDA3412D2CB2A9D59362E63  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t774BA651AE026236E6A4E65130C94C68A8F12F78  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t6840AB631DC79C250350FC6827D8311C898DE53A  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t1550A83788F13679513F394B6EDAEFDEF6DD83FD  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tD0DAAE89362D31371D9DECC2D7E4DBDEBC53E162  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t33E0C79EE0DA59E6A729E1B1A25242F129FCCEBB  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tC461C1DF80AEBE5BEB34543E22B80EDFE0D3C889  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tED064263A4CFD78CB222F5605696BFF1B974B371  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t080089E1968463ADDF3E4DCB431FDE05A2D57747  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t6958B8552CBAAD57D1EF84C1998729D995FC69C5  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t6143A06D3AE8F1E0B639EA443C13E7134182E552  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t4B28206939893D408035FF34E371970CE4A1E245  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tA2FF87306E115C430547366EF4C8AA32D97A6D9F  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tF45B9967ECAFBD74FBF226B8F821A80DB0966401  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tBA75029F0EAC25262FF2324AD8A9D84BC4CEAA7F  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tE1A06D66E8EAF44E81429199F4BAA9305FEAFFBE  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tBC60655FD5B03AF2B55D6D9739D6A08235F8294B  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tC597301DD23D1ED18DB72BC2061DF03BBC80D080  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t429DB594FC95EC58ACC43CC3D23876DF36F2A810  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t53B39CDBD832B82FEFFA488A8665E13C56DF497F  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t536F58B4E191D94DA01052EB645043A671219893  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t2D6A83576337049E1AA7A3AA7ACF2E3C905EFE6D  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t9EBF6DD4A021A9C01A37D0E549408B477F19C782  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tE874D3A323BEABED684696DA1FA1468E93D0CB6B  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t955906E20F3B8466D7B35BE8B781E5920A1641E9  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tFA29634A8AE6BDFDB406A9F297A6694DD535D13B  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t17FCC86BDC7CD460253D82E14B883AFAB2B58B69  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t48EB69946F071258B7E302B1CB1587E51AA4899B  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t3A8292E5FBAA78A4CD9958267F21BD8415907356  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tC8CC13C3835F60C196349ED876C10B1831B80321  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tFE99C1C91147C2FF5914A52ED138C48BB9F455E4  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tD413081183D831D5FD1DB047C6388E9EA8522318  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tFA4D14E31A2A501D102D88481DCBC9501A6EF390  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t0967321B2D6F146150A7805676EB01A1FD0E97B6  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tD4671A56749A7DF13214BCC142E5B63DD08DC6E2  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t24AD2D6DD8A94A3CAAAC2BF19CE9CC5FF2A5EF01  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t3A964CBC1C006687152955ADCE5E079A16FCAA64  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tB1D433798A416CD27C6D857D5ACBCE66F96F75D2  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tB5C03C61F7EE85ED25192EB102A1DD0C34FE1659  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tA8F2E4659F4B05E9EE432E67D41DF7D34D00ACBA  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t2F1387D95A1E9B93E66C397645BFB8BCD265219B  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tC24A0A7B21F69286D618A79A4F7CD6F8E9E64E46  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tBB54188F9777697633AEB26C10FCD9F2DD2069F0  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tB29236CBC4D16E03D6222DA324AB2ACF0D43B0A9  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tC860F51F8C5A65926877A98F7783DC6A935F7D1E  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t71AE3FABA64C0BDFD0A5C88C96C610D75DADFAA0  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tE41DECCC84B37A676AD4FB470E116D565F158E50  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t9A8DE563390AAE72BC4CD365302E3CF81023F56C  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tDDE34409F54B0316C92D71544B1048DCAB2C32E8  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tADBFBB103D4F7E1831F1D56F47B4A8F7909E4881  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tD0122DC44A710DD49FC41629A18C8D134C18FE98  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tB2D9FD6EEDEDCE81095EB5FE5BEDEB85C19FC587  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tE7CC94CF3AF5DBFA7C323C057A4FF56BDB716B60  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t8F79254FE423213DBAF2A7C950F4E481BC1E91B8  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t2DD9317B040ECE7B24AB61C113E75B4797E4E7F4  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t4C5B9B18BDE2E252F9CF8A6D1C186898A9930B77  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t770A3AEB214F16F29BBF110D9AC8426DF02198B2  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tF4B0D70F4EB5D361ECC58809FECA24A1759A522A  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t91769598ADBCE14E41CA455BE2D09EE13091BA08  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t5715FD36AD784697BBF54C75B9ED405E6B911E38  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t52FC6BE3D9818DD0B9B8BA9533F01AD2ECAA3A09  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tBDA52190085C86D7FEDC7F89C457AB561BD49D0C  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t2C5513E651D543BF3C3283FCF36634A508575D83  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t2EB8D9AC8781E653B6B2A84E2D2C949898F4C4B5  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t2832F2D954294F5B599B02A21FEC3063A91E345A  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t4C4BDE02A1DD1B90FCDA3BF929C73B897578522D  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t745C8514C16F4952BDE7FBF86BD26A02ECF946C7  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t79C20308C23C01C45762F8911B97E810F0515397  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tE6546B1D6A1EF896304F7F2A106E54AB94E036B7  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t3CE274E3A37C54E3388A75382A5881356D49E132  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t1E23861777A25A773264A698434477DCE18B34CF  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tA16FBB091D74A21AC539D829F65E5E8FAF2DB075  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t91378217184AE0BD640D9A003EF2CB208071D593  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tDA117C10FEABA385803A90E976050FB2D270FAD2  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_tD11940BE9DAD74F61B141A397F3D5722E7FF2601  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
struct PropertyBag_1_t74F4963AD6B656900B7CACFC37AC3CDDDF818409  : public RuntimeObject
{
	int32_t ___U3CInstantiationKindU3Ek__BackingField;
};
typedef Il2CppFullySharedGenericStruct PropertyCollection_1_t1A85A11A534CEDAC54B3831121B533C068ACD85C;
typedef Il2CppFullySharedGenericStruct PropertyCollection_1_t5813F70F52FEC5B64A85FE040869894365CCF94D;
struct StyleEnum_1_t5578A741DA42584CA127D6AF3539B91F3E8CD180 
{
	int32_t ___m_Value;
	int32_t ___m_Keyword;
};
struct StyleEnum_1_t77F383B8CB033853D8496A71CB7B6EF5333A5459 
{
	int32_t ___m_Value;
	int32_t ___m_Keyword;
};
struct BackgroundRepeat_t446EC7315DED2C6822F1047B7587C3018BFB277F 
{
	int32_t ___x;
	int32_t ___y;
};
struct Length_t90BB06D47DD6DB461ED21BD3E3241FAB6C824256 
{
	float ___m_Value;
	int32_t ___m_Unit;
};
struct MulticastDelegate_t  : public Delegate_t
{
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates;
};
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates;
};
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates;
};
struct PrimitiveValue_t1CC37566F40746757D5E3F87474A05909D85C2D4 
{
	union
	{
		#pragma pack(push, tp, 1)
		struct
		{
			int32_t ___m_Type;
		};
		#pragma pack(pop, tp)
		struct
		{
			int32_t ___m_Type_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_BoolValue_OffsetPadding[4];
			bool ___m_BoolValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_BoolValue_OffsetPadding_forAlignmentOnly[4];
			bool ___m_BoolValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_CharValue_OffsetPadding[4];
			Il2CppChar ___m_CharValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_CharValue_OffsetPadding_forAlignmentOnly[4];
			Il2CppChar ___m_CharValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_ByteValue_OffsetPadding[4];
			uint8_t ___m_ByteValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_ByteValue_OffsetPadding_forAlignmentOnly[4];
			uint8_t ___m_ByteValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_SByteValue_OffsetPadding[4];
			int8_t ___m_SByteValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_SByteValue_OffsetPadding_forAlignmentOnly[4];
			int8_t ___m_SByteValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_ShortValue_OffsetPadding[4];
			int16_t ___m_ShortValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_ShortValue_OffsetPadding_forAlignmentOnly[4];
			int16_t ___m_ShortValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_UShortValue_OffsetPadding[4];
			uint16_t ___m_UShortValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_UShortValue_OffsetPadding_forAlignmentOnly[4];
			uint16_t ___m_UShortValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_IntValue_OffsetPadding[4];
			int32_t ___m_IntValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_IntValue_OffsetPadding_forAlignmentOnly[4];
			int32_t ___m_IntValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_UIntValue_OffsetPadding[4];
			uint32_t ___m_UIntValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_UIntValue_OffsetPadding_forAlignmentOnly[4];
			uint32_t ___m_UIntValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_LongValue_OffsetPadding[4];
			int64_t ___m_LongValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_LongValue_OffsetPadding_forAlignmentOnly[4];
			int64_t ___m_LongValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_ULongValue_OffsetPadding[4];
			uint64_t ___m_ULongValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_ULongValue_OffsetPadding_forAlignmentOnly[4];
			uint64_t ___m_ULongValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_FloatValue_OffsetPadding[4];
			float ___m_FloatValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_FloatValue_OffsetPadding_forAlignmentOnly[4];
			float ___m_FloatValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_DoubleValue_OffsetPadding[4];
			double ___m_DoubleValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_DoubleValue_OffsetPadding_forAlignmentOnly[4];
			double ___m_DoubleValue_forAlignmentOnly;
		};
	};
};
struct PrimitiveValue_t1CC37566F40746757D5E3F87474A05909D85C2D4_marshaled_pinvoke
{
	union
	{
		#pragma pack(push, tp, 1)
		struct
		{
			int32_t ___m_Type;
		};
		#pragma pack(pop, tp)
		struct
		{
			int32_t ___m_Type_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_BoolValue_OffsetPadding[4];
			int32_t ___m_BoolValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_BoolValue_OffsetPadding_forAlignmentOnly[4];
			int32_t ___m_BoolValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_CharValue_OffsetPadding[4];
			uint8_t ___m_CharValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_CharValue_OffsetPadding_forAlignmentOnly[4];
			uint8_t ___m_CharValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_ByteValue_OffsetPadding[4];
			uint8_t ___m_ByteValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_ByteValue_OffsetPadding_forAlignmentOnly[4];
			uint8_t ___m_ByteValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_SByteValue_OffsetPadding[4];
			int8_t ___m_SByteValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_SByteValue_OffsetPadding_forAlignmentOnly[4];
			int8_t ___m_SByteValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_ShortValue_OffsetPadding[4];
			int16_t ___m_ShortValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_ShortValue_OffsetPadding_forAlignmentOnly[4];
			int16_t ___m_ShortValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_UShortValue_OffsetPadding[4];
			uint16_t ___m_UShortValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_UShortValue_OffsetPadding_forAlignmentOnly[4];
			uint16_t ___m_UShortValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_IntValue_OffsetPadding[4];
			int32_t ___m_IntValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_IntValue_OffsetPadding_forAlignmentOnly[4];
			int32_t ___m_IntValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_UIntValue_OffsetPadding[4];
			uint32_t ___m_UIntValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_UIntValue_OffsetPadding_forAlignmentOnly[4];
			uint32_t ___m_UIntValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_LongValue_OffsetPadding[4];
			int64_t ___m_LongValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_LongValue_OffsetPadding_forAlignmentOnly[4];
			int64_t ___m_LongValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_ULongValue_OffsetPadding[4];
			uint64_t ___m_ULongValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_ULongValue_OffsetPadding_forAlignmentOnly[4];
			uint64_t ___m_ULongValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_FloatValue_OffsetPadding[4];
			float ___m_FloatValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_FloatValue_OffsetPadding_forAlignmentOnly[4];
			float ___m_FloatValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_DoubleValue_OffsetPadding[4];
			double ___m_DoubleValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_DoubleValue_OffsetPadding_forAlignmentOnly[4];
			double ___m_DoubleValue_forAlignmentOnly;
		};
	};
};
struct PrimitiveValue_t1CC37566F40746757D5E3F87474A05909D85C2D4_marshaled_com
{
	union
	{
		#pragma pack(push, tp, 1)
		struct
		{
			int32_t ___m_Type;
		};
		#pragma pack(pop, tp)
		struct
		{
			int32_t ___m_Type_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_BoolValue_OffsetPadding[4];
			int32_t ___m_BoolValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_BoolValue_OffsetPadding_forAlignmentOnly[4];
			int32_t ___m_BoolValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_CharValue_OffsetPadding[4];
			uint8_t ___m_CharValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_CharValue_OffsetPadding_forAlignmentOnly[4];
			uint8_t ___m_CharValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_ByteValue_OffsetPadding[4];
			uint8_t ___m_ByteValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_ByteValue_OffsetPadding_forAlignmentOnly[4];
			uint8_t ___m_ByteValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_SByteValue_OffsetPadding[4];
			int8_t ___m_SByteValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_SByteValue_OffsetPadding_forAlignmentOnly[4];
			int8_t ___m_SByteValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_ShortValue_OffsetPadding[4];
			int16_t ___m_ShortValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_ShortValue_OffsetPadding_forAlignmentOnly[4];
			int16_t ___m_ShortValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_UShortValue_OffsetPadding[4];
			uint16_t ___m_UShortValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_UShortValue_OffsetPadding_forAlignmentOnly[4];
			uint16_t ___m_UShortValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_IntValue_OffsetPadding[4];
			int32_t ___m_IntValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_IntValue_OffsetPadding_forAlignmentOnly[4];
			int32_t ___m_IntValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_UIntValue_OffsetPadding[4];
			uint32_t ___m_UIntValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_UIntValue_OffsetPadding_forAlignmentOnly[4];
			uint32_t ___m_UIntValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_LongValue_OffsetPadding[4];
			int64_t ___m_LongValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_LongValue_OffsetPadding_forAlignmentOnly[4];
			int64_t ___m_LongValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_ULongValue_OffsetPadding[4];
			uint64_t ___m_ULongValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_ULongValue_OffsetPadding_forAlignmentOnly[4];
			uint64_t ___m_ULongValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_FloatValue_OffsetPadding[4];
			float ___m_FloatValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_FloatValue_OffsetPadding_forAlignmentOnly[4];
			float ___m_FloatValue_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___m_DoubleValue_OffsetPadding[4];
			double ___m_DoubleValue;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___m_DoubleValue_OffsetPadding_forAlignmentOnly[4];
			double ___m_DoubleValue_forAlignmentOnly;
		};
	};
};
struct PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF 
{
	int32_t ___m_Kind;
	String_t* ___m_Name;
	int32_t ___m_Index;
	RuntimeObject* ___m_Key;
};
struct PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_pinvoke
{
	int32_t ___m_Kind;
	char* ___m_Name;
	int32_t ___m_Index;
	Il2CppIUnknown* ___m_Key;
};
struct PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_com
{
	int32_t ___m_Kind;
	Il2CppChar* ___m_Name;
	int32_t ___m_Index;
	Il2CppIUnknown* ___m_Key;
};
struct StylePropertyName_tCBE2B561C690538C8514BF56426AC486DC35B6FF 
{
	int32_t ___U3CidU3Ek__BackingField;
	String_t* ___U3CnameU3Ek__BackingField;
};
struct StylePropertyName_tCBE2B561C690538C8514BF56426AC486DC35B6FF_marshaled_pinvoke
{
	int32_t ___U3CidU3Ek__BackingField;
	char* ___U3CnameU3Ek__BackingField;
};
struct StylePropertyName_tCBE2B561C690538C8514BF56426AC486DC35B6FF_marshaled_com
{
	int32_t ___U3CidU3Ek__BackingField;
	Il2CppChar* ___U3CnameU3Ek__BackingField;
};
struct StyleSelectorPart_tEE5B8ADC7D114C7486CC8301FF96C114FF3C9470 
{
	String_t* ___m_Value;
	int32_t ___m_Type;
	RuntimeObject* ___tempData;
	int32_t ___cachedUniqueStyleStringId;
};
struct StyleSelectorPart_tEE5B8ADC7D114C7486CC8301FF96C114FF3C9470_marshaled_pinvoke
{
	char* ___m_Value;
	int32_t ___m_Type;
	Il2CppIUnknown* ___tempData;
	int32_t ___cachedUniqueStyleStringId;
};
struct StyleSelectorPart_tEE5B8ADC7D114C7486CC8301FF96C114FF3C9470_marshaled_com
{
	Il2CppChar* ___m_Value;
	int32_t ___m_Type;
	Il2CppIUnknown* ___tempData;
	int32_t ___cachedUniqueStyleStringId;
};
struct StyleSyntaxToken_tE4474F86F800F298F966FFDE947528453E769E0C 
{
	int32_t ___type;
	String_t* ___text;
	float ___number;
};
struct StyleSyntaxToken_tE4474F86F800F298F966FFDE947528453E769E0C_marshaled_pinvoke
{
	int32_t ___type;
	char* ___text;
	float ___number;
};
struct StyleSyntaxToken_tE4474F86F800F298F966FFDE947528453E769E0C_marshaled_com
{
	int32_t ___type;
	Il2CppChar* ___text;
	float ___number;
};
struct StyleValueHandle_t5831643AAA7AD8C5C43A4498C5E0A2545F78227D 
{
	int32_t ___m_ValueType;
	int32_t ___valueIndex;
};
struct StyleValueManaged_t68DFBEC1594279E4DC56634FD5092318D1E9A5F4 
{
	int32_t ___id;
	int32_t ___keyword;
	RuntimeObject* ___value;
};
struct StyleValueManaged_t68DFBEC1594279E4DC56634FD5092318D1E9A5F4_marshaled_pinvoke
{
	int32_t ___id;
	int32_t ___keyword;
	Il2CppIUnknown* ___value;
};
struct StyleValueManaged_t68DFBEC1594279E4DC56634FD5092318D1E9A5F4_marshaled_com
{
	int32_t ___id;
	int32_t ___keyword;
	Il2CppIUnknown* ___value;
};
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};
struct TimeValue_t45AE43B219493F9459363F32C79E8986B5F82E0E 
{
	float ___m_Value;
	int32_t ___m_Unit;
};
struct Type_t  : public MemberInfo_t
{
	RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ____impl;
};
struct UnsafeMeshGenerationNode_t1FF7D38D9B39DC59A2F8E2DFC92BFAB2481E88A2 
{
	GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC ___m_Handle;
};
struct VertexAttributeDescriptor_tD4231FBF57335465D16308D2A18E8E83D36BFA76 
{
	int32_t ___U3CattributeU3Ek__BackingField;
	int32_t ___U3CformatU3Ek__BackingField;
	int32_t ___U3CdimensionU3Ek__BackingField;
	int32_t ___U3CstreamU3Ek__BackingField;
};
struct XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 
{
	String_t* ___name;
	List_1_t41896DF347A129D13205BBFADE03DFB21E05E459* ___usageHints;
	int32_t ___featureType;
	uint32_t ___customSize;
};
struct XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_marshaled_pinvoke
{
	char* ___name;
	List_1_t41896DF347A129D13205BBFADE03DFB21E05E459* ___usageHints;
	int32_t ___featureType;
	uint32_t ___customSize;
};
struct XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_marshaled_com
{
	Il2CppChar* ___name;
	List_1_t41896DF347A129D13205BBFADE03DFB21E05E459* ___usageHints;
	int32_t ___featureType;
	uint32_t ___customSize;
};
struct ShadowResolutionRequest_tC1B869ADCA139D8D7AD8A355373F84C7F5E0FCBE 
{
	uint16_t ___visibleLightIndex;
	uint16_t ___perLightShadowSliceIndex;
	uint16_t ___requestedResolution;
	uint16_t ___offsetX;
	uint16_t ___offsetY;
	uint16_t ___allocatedResolution;
	uint16_t ___m_ShadowProperties;
};
struct HIDCollectionDescriptor_t9235094BD1880EA02E4E07F9C02AC98088EFB9F7 
{
	int32_t ___type;
	int32_t ___usage;
	int32_t ___usagePage;
	int32_t ___parent;
	int32_t ___childCount;
	int32_t ___firstChild;
};
struct HIDElementDescriptor_t18CA9618F730F541BA1C873745A1AE71823D4CED 
{
	int32_t ___usage;
	int32_t ___usagePage;
	int32_t ___unit;
	int32_t ___unitExponent;
	int32_t ___logicalMin;
	int32_t ___logicalMax;
	int32_t ___physicalMin;
	int32_t ___physicalMax;
	int32_t ___reportType;
	int32_t ___collectionIndex;
	int32_t ___reportId;
	int32_t ___reportSizeInBits;
	int32_t ___reportOffsetInBits;
	int32_t ___flags;
	Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28 ___usageMin;
	Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28 ___usageMax;
};
struct HIDElementDescriptor_t18CA9618F730F541BA1C873745A1AE71823D4CED_marshaled_pinvoke
{
	int32_t ___usage;
	int32_t ___usagePage;
	int32_t ___unit;
	int32_t ___unitExponent;
	int32_t ___logicalMin;
	int32_t ___logicalMax;
	int32_t ___physicalMin;
	int32_t ___physicalMax;
	int32_t ___reportType;
	int32_t ___collectionIndex;
	int32_t ___reportId;
	int32_t ___reportSizeInBits;
	int32_t ___reportOffsetInBits;
	int32_t ___flags;
	Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28 ___usageMin;
	Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28 ___usageMax;
};
struct HIDElementDescriptor_t18CA9618F730F541BA1C873745A1AE71823D4CED_marshaled_com
{
	int32_t ___usage;
	int32_t ___usagePage;
	int32_t ___unit;
	int32_t ___unitExponent;
	int32_t ___logicalMin;
	int32_t ___logicalMax;
	int32_t ___physicalMin;
	int32_t ___physicalMax;
	int32_t ___reportType;
	int32_t ___collectionIndex;
	int32_t ___reportId;
	int32_t ___reportSizeInBits;
	int32_t ___reportOffsetInBits;
	int32_t ___flags;
	Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28 ___usageMin;
	Nullable_1_tCF32C56A2641879C053C86F273C0C6EC1B40BC28 ___usageMax;
};
struct HIDReportData_t2F0D5DFC9087B851FA3288EFBB6FF4EAA863BB4F 
{
	int32_t ___reportId;
	int32_t ___reportType;
	int32_t ___currentBitOffset;
};
struct JsonValue_t01DB320267C848E729A400EF2345979978F851D2 
{
	int32_t ___type;
	bool ___boolValue;
	double ___realValue;
	int64_t ___integerValue;
	JsonString_tE22CDDA995FEFF514F3F334C93B6AB31B49773CB ___stringValue;
	List_1_t4A80BCCFB0BC8742C8BB601365DB07226750573A* ___arrayValue;
	Dictionary_2_t6682243BDABA638FCBE4F1D9875B5B1455A7686E* ___objectValue;
	RuntimeObject* ___anyValue;
};
struct JsonValue_t01DB320267C848E729A400EF2345979978F851D2_marshaled_pinvoke
{
	int32_t ___type;
	int32_t ___boolValue;
	double ___realValue;
	int64_t ___integerValue;
	JsonString_tE22CDDA995FEFF514F3F334C93B6AB31B49773CB_marshaled_pinvoke ___stringValue;
	List_1_t4A80BCCFB0BC8742C8BB601365DB07226750573A* ___arrayValue;
	Dictionary_2_t6682243BDABA638FCBE4F1D9875B5B1455A7686E* ___objectValue;
	Il2CppIUnknown* ___anyValue;
};
struct JsonValue_t01DB320267C848E729A400EF2345979978F851D2_marshaled_com
{
	int32_t ___type;
	int32_t ___boolValue;
	double ___realValue;
	int64_t ___integerValue;
	JsonString_tE22CDDA995FEFF514F3F334C93B6AB31B49773CB_marshaled_com ___stringValue;
	List_1_t4A80BCCFB0BC8742C8BB601365DB07226750573A* ___arrayValue;
	Dictionary_2_t6682243BDABA638FCBE4F1D9875B5B1455A7686E* ___objectValue;
	Il2CppIUnknown* ___anyValue;
};
struct NativeRectParams_t18E2E456D50F027D08BF8B4BCBD83DF7FDB1C02F 
{
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___rect;
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___subRect;
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___uv;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___color;
	intptr_t ___backgroundRepeatInstanceList;
	int32_t ___backgroundRepeatInstanceListStartIndex;
	int32_t ___backgroundRepeatInstanceListEndIndex;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___topLeftRadius;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___topRightRadius;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___bottomRightRadius;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___bottomLeftRadius;
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___backgroundRepeatRect;
	intptr_t ___texture;
	intptr_t ___sprite;
	intptr_t ___vectorImage;
	intptr_t ___spriteTexture;
	intptr_t ___spriteVertices;
	intptr_t ___spriteUVs;
	intptr_t ___spriteTriangles;
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___spriteGeomRect;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___contentSize;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___textureSize;
	float ___texturePixelsPerPoint;
	int32_t ___leftSlice;
	int32_t ___topSlice;
	int32_t ___rightSlice;
	int32_t ___bottomSlice;
	float ___sliceScale;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___rectInset;
	NativeColorId_tC2A21D165C258872CC3EEB668F4CF4D08C37237A ___colorId;
	int32_t ___meshFlags;
	int32_t ___scaleMode;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___uvTopLeft;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___uvTopRight;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___uvBottomRight;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___uvBottomLeft;
	int32_t ___uvCornersValid;
};
struct AssetEntry_tEB6FC90E5BB63DCA4FF932F2D64595339A28806D 
{
	String_t* ___m_Path;
	String_t* ___m_TypeFullName;
	LazyLoadReference_1_tD57A582D5A28F3AE34FBB810985EF171325FAF1B ___m_AssetReference;
	EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 ___m_EntityId;
	Type_t* ___m_CachedType;
};
struct AssetEntry_tEB6FC90E5BB63DCA4FF932F2D64595339A28806D_marshaled_pinvoke
{
	char* ___m_Path;
	char* ___m_TypeFullName;
	LazyLoadReference_1_tD57A582D5A28F3AE34FBB810985EF171325FAF1B ___m_AssetReference;
	EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 ___m_EntityId;
	Type_t* ___m_CachedType;
};
struct AssetEntry_tEB6FC90E5BB63DCA4FF932F2D64595339A28806D_marshaled_com
{
	Il2CppChar* ___m_Path;
	Il2CppChar* ___m_TypeFullName;
	LazyLoadReference_1_tD57A582D5A28F3AE34FBB810985EF171325FAF1B ___m_AssetReference;
	EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 ___m_EntityId;
	Type_t* ___m_CachedType;
};
struct EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 
{
	RenderData_t1ABE116B2B5E0409AC699E195922516606531DC2* ___renderData;
	int32_t ___type;
	Entry_t475ED76E31923970E7F7A6522E570E7577C487B9* ___rootEntry;
};
struct EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289_marshaled_pinvoke
{
	RenderData_t1ABE116B2B5E0409AC699E195922516606531DC2* ___renderData;
	int32_t ___type;
	Entry_t475ED76E31923970E7F7A6522E570E7577C487B9* ___rootEntry;
};
struct EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289_marshaled_com
{
	RenderData_t1ABE116B2B5E0409AC699E195922516606531DC2* ___renderData;
	int32_t ___type;
	Entry_t475ED76E31923970E7F7A6522E570E7577C487B9* ___rootEntry;
};
struct IndexedCollectionPropertyBagEnumerable_1_t25D3E23348A8FB961FD509BA4EA1E5F7E7F00C86 
{
	RuntimeObject* ___m_Impl;
	StyleEnum_1_t5578A741DA42584CA127D6AF3539B91F3E8CD180 ___m_Container;
};
struct IndexedCollectionPropertyBagEnumerable_1_tF08CF58F37F195DAB19A7DB4567188C980033D9E 
{
	RuntimeObject* ___m_Impl;
	StyleEnum_1_t77F383B8CB033853D8496A71CB7B6EF5333A5459 ___m_Container;
};
struct LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E  : public MulticastDelegate_t
{
};
struct Predicate_1_t62D35E585A87CD1095C7BB83FA7A8F106882D19B  : public MulticastDelegate_t
{
};
struct Predicate_1_t0FA75C2688E250A8F997BCC1793B987042152CAD  : public MulticastDelegate_t
{
};
struct Predicate_1_t5F8B306609DB5DF7FB8663982C5398BD89094356  : public MulticastDelegate_t
{
};
struct Predicate_1_t33210454BBD5525EDCF57579B4F30B5893B8C8EC  : public MulticastDelegate_t
{
};
struct Predicate_1_tD44833D3E82DB77FCCAF428B5A05B3F717AC19D7  : public MulticastDelegate_t
{
};
struct Predicate_1_t56CBD671890A506EE17489FA02B2F252085963B5  : public MulticastDelegate_t
{
};
struct Predicate_1_t9058548C4810505B646B00E24AECF8F76512256C  : public MulticastDelegate_t
{
};
struct Predicate_1_t52040C0404BB605626DC3B4050B99A7392B681C6  : public MulticastDelegate_t
{
};
struct Predicate_1_tD5A949E7B7A12F5D7F1F939A0DC04B213ACF03CD  : public MulticastDelegate_t
{
};
struct Predicate_1_tF77354EE58DBBBB2118B440CD2E5CCC285D7509B  : public MulticastDelegate_t
{
};
struct Predicate_1_tB42DFC3FAEDCC361490394CE84FDAB0897614DCA  : public MulticastDelegate_t
{
};
struct Predicate_1_tD9C43B9F9F1E24AC8BF0B13DA518A038655CF159  : public MulticastDelegate_t
{
};
struct Predicate_1_t2F212B682F64543895DDB2A14A8816A433567EFF  : public MulticastDelegate_t
{
};
struct Predicate_1_t4CC76CDB2DE98076E841155882228372ED64954D  : public MulticastDelegate_t
{
};
struct Predicate_1_t94C77BF71BFED92EF61DE4F47EE0D1A653E89D6B  : public MulticastDelegate_t
{
};
struct Predicate_1_tEAAAB877302235E809D3416D8DD71E15AB6CA7ED  : public MulticastDelegate_t
{
};
struct Predicate_1_t203C64C908D51E90EAD0465C23AF919A7C1F55CD  : public MulticastDelegate_t
{
};
struct Predicate_1_t81974A023BAFB944D5B1430A69D464C84F544F53  : public MulticastDelegate_t
{
};
struct Predicate_1_t7744EBE2271FC2C4CBF28703CF361AF669D73B95  : public MulticastDelegate_t
{
};
struct Predicate_1_tE96ECD003F473EE07C8DCDFCCD657300A8DE453B  : public MulticastDelegate_t
{
};
struct Predicate_1_t5D3885F0B10D3F6345B19C0FFF8C67C14C7ECE1D  : public MulticastDelegate_t
{
};
struct Predicate_1_t34FC9FE4137EF0639ABE9C1FEA3E718F546B3433  : public MulticastDelegate_t
{
};
struct Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5  : public MulticastDelegate_t
{
};
struct Predicate_1_tAC2079F7E6995DA080B0BA133041858D4D7709D0  : public MulticastDelegate_t
{
};
struct Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107  : public MulticastDelegate_t
{
};
struct Predicate_1_tA6EA869EC035935C687560A7C7B2665939B2FF89  : public MulticastDelegate_t
{
};
struct Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C  : public MulticastDelegate_t
{
};
struct Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0  : public MulticastDelegate_t
{
};
struct Predicate_1_t52FF23711F254610555E47294A60791608149553  : public MulticastDelegate_t
{
};
struct Predicate_1_tEC6CC19D44A0951E3F052DFEEA23AD9617C11D98  : public MulticastDelegate_t
{
};
struct Predicate_1_tA79A10B2685C101827A7F9155FDB96CF41167EED  : public MulticastDelegate_t
{
};
struct Predicate_1_t4690EFF9D263616BDC96916FFF73A24E6EF49CF6  : public MulticastDelegate_t
{
};
struct Predicate_1_tFC928A0F14BD7F06B38F7E20ED6DD42BDC5FFF2E  : public MulticastDelegate_t
{
};
struct Predicate_1_tFAC7FC229F0A1EFE26E081595C8FCCE069E2DD4D  : public MulticastDelegate_t
{
};
struct Predicate_1_tA17BC206702BF0590B94B2C3B98F4BABBE18DE61  : public MulticastDelegate_t
{
};
struct Predicate_1_t15E52F38022348884DB7434BD48688EFBD9A7C92  : public MulticastDelegate_t
{
};
struct Predicate_1_tE7A821CD57BA00206E0206E8CE3F6FF208B143AE  : public MulticastDelegate_t
{
};
struct Predicate_1_t4389C16675D6094FE36B3680C9F080B2BB26C746  : public MulticastDelegate_t
{
};
struct Predicate_1_tFB60CD13D423EFABC2480AB1A0005D748762F6DD  : public MulticastDelegate_t
{
};
struct Predicate_1_t8DCFD1E780C42A60574281B4F7F34FD875DDB856  : public MulticastDelegate_t
{
};
struct Predicate_1_t5F82C0C3503EA1B68ECEC5CA226056D878DF53F0  : public MulticastDelegate_t
{
};
struct Predicate_1_t30B6C0EA4B49279CABE20C001AAC4DD54EAE9AB6  : public MulticastDelegate_t
{
};
struct Predicate_1_tD7983A114F281CA3351C3A13212F109FABCFCE0B  : public MulticastDelegate_t
{
};
struct Predicate_1_t81BE711230EEE3DC50CF76B00DD7457CAF4A32E1  : public MulticastDelegate_t
{
};
struct Predicate_1_t2016617F02A0CD278E2CBE15AAEECE2ABC730D96  : public MulticastDelegate_t
{
};
struct Predicate_1_t14B8A21069ACE62988B7E5E9CC64CECF7FBC0127  : public MulticastDelegate_t
{
};
struct Predicate_1_t9939AC86819E2498AF558953EFF6F269E25648A6  : public MulticastDelegate_t
{
};
struct Predicate_1_t0C55B51239C83862F44CB14125E618A3597B0712  : public MulticastDelegate_t
{
};
struct Predicate_1_tD510DAD56990F5E890E872ED2EBBA496DBED7093  : public MulticastDelegate_t
{
};
struct Predicate_1_t28959210271C40F0A0780063E8A9A93017170B8B  : public MulticastDelegate_t
{
};
struct Predicate_1_t107748E0E0D2277266234EAF67AC068394C4351C  : public MulticastDelegate_t
{
};
struct Predicate_1_t423CD24D1C97FC8453B682DD50254CBD6E713EA9  : public MulticastDelegate_t
{
};
struct Predicate_1_tCF5C61843DC88D3F367506A3DCD4EBE9BACF1294  : public MulticastDelegate_t
{
};
struct Predicate_1_t5EB227002D89EF0CF4F03AEB8D282C36CBF2EEE7  : public MulticastDelegate_t
{
};
struct Predicate_1_t3DAD00743A8030ED59E9F0E95E4904C4D1BA9300  : public MulticastDelegate_t
{
};
struct Predicate_1_t7ED9B622E51E090A6006AF45320697BC4751B3E4  : public MulticastDelegate_t
{
};
struct Predicate_1_t6BB7E4172E2E29F92C012A7C3025F88C4E728C26  : public MulticastDelegate_t
{
};
struct Predicate_1_t93A520FF4A311DBC767E0D16024BB6FCBC117E50  : public MulticastDelegate_t
{
};
struct Predicate_1_t866B5EAE9FE560007C1219A3435F35DF418529B1  : public MulticastDelegate_t
{
};
struct Predicate_1_t5A30DC3457D23124EDD16C06F92FCB77A9C8A8FD  : public MulticastDelegate_t
{
};
struct Predicate_1_t9076838CC246AB97B4898F3C3167D2368F156B60  : public MulticastDelegate_t
{
};
struct Predicate_1_t9C66117359C44DD2D158092707A1241BC3C8C88C  : public MulticastDelegate_t
{
};
struct Predicate_1_t0E4E7545667C4F2A2A04DDC6DB49E5736E94C9F1  : public MulticastDelegate_t
{
};
struct Predicate_1_t1DE99100B4CFA269FC837F0124B03EF65D7CC56A  : public MulticastDelegate_t
{
};
struct Predicate_1_t77D31E9C29315FDF86033FA263455FEE3B6606A0  : public MulticastDelegate_t
{
};
struct Predicate_1_t7E5B83C99F6527762BD489BA76550A25F6333DA8  : public MulticastDelegate_t
{
};
struct Predicate_1_t897AC601B2D18D49F8B416C6D9C4720337BE7D50  : public MulticastDelegate_t
{
};
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
	String_t* ____paramName;
};
struct BackgroundPosition_tF0822B29FC27A67205A9893EBE03D03B799B8B56 
{
	int32_t ___keyword;
	Length_t90BB06D47DD6DB461ED21BD3E3241FAB6C824256 ___offset;
};
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct PropertyPath_tA523CA2740853534DF6C009C588464B45A6D0A79 
{
	PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF ___m_Part0;
	PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF ___m_Part1;
	PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF ___m_Part2;
	PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF ___m_Part3;
	PropertyPathPartU5BU5D_t7994D542F14DDDDEABB1792C335C20149399AEBB* ___m_AdditionalParts;
	int32_t ___U3CLengthU3Ek__BackingField;
};
struct PropertyPath_tA523CA2740853534DF6C009C588464B45A6D0A79_marshaled_pinvoke
{
	PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_pinvoke ___m_Part0;
	PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_pinvoke ___m_Part1;
	PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_pinvoke ___m_Part2;
	PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_pinvoke ___m_Part3;
	PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_pinvoke* ___m_AdditionalParts;
	int32_t ___U3CLengthU3Ek__BackingField;
};
struct PropertyPath_tA523CA2740853534DF6C009C588464B45A6D0A79_marshaled_com
{
	PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_com ___m_Part0;
	PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_com ___m_Part1;
	PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_com ___m_Part2;
	PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_com ___m_Part3;
	PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_com* ___m_AdditionalParts;
	int32_t ___U3CLengthU3Ek__BackingField;
};
struct StylePropertyValue_tED32F617FABE99611B213BFCF9D1D909E7F141C2 
{
	StyleSheet_t6FAF43FCDB45BC6BED0522A222FD4C1A9BB10428* ___sheet;
	StyleValueHandle_t5831643AAA7AD8C5C43A4498C5E0A2545F78227D ___handle;
};
struct StylePropertyValue_tED32F617FABE99611B213BFCF9D1D909E7F141C2_marshaled_pinvoke
{
	StyleSheet_t6FAF43FCDB45BC6BED0522A222FD4C1A9BB10428* ___sheet;
	StyleValueHandle_t5831643AAA7AD8C5C43A4498C5E0A2545F78227D ___handle;
};
struct StylePropertyValue_tED32F617FABE99611B213BFCF9D1D909E7F141C2_marshaled_com
{
	StyleSheet_t6FAF43FCDB45BC6BED0522A222FD4C1A9BB10428* ___sheet;
	StyleValueHandle_t5831643AAA7AD8C5C43A4498C5E0A2545F78227D ___handle;
};
struct ControlItem_t25B2C46F52E78ADC5F54903F9E769364B02CD4AD 
{
	InternedString_t8D62A48CB7D85AAE9CFCCCFB0A77AC2844905735 ___U3CnameU3Ek__BackingField;
	InternedString_t8D62A48CB7D85AAE9CFCCCFB0A77AC2844905735 ___U3ClayoutU3Ek__BackingField;
	InternedString_t8D62A48CB7D85AAE9CFCCCFB0A77AC2844905735 ___U3CvariantsU3Ek__BackingField;
	String_t* ___U3CuseStateFromU3Ek__BackingField;
	String_t* ___U3CdisplayNameU3Ek__BackingField;
	String_t* ___U3CshortDisplayNameU3Ek__BackingField;
	ReadOnlyArray_1_t1C2864D7CF4D444AB2616316AC8DD33932F77064 ___U3CusagesU3Ek__BackingField;
	ReadOnlyArray_1_t1C2864D7CF4D444AB2616316AC8DD33932F77064 ___U3CaliasesU3Ek__BackingField;
	ReadOnlyArray_1_t4A15F7D15ACB297B45A08889D51E4CACEAD4EDF9 ___U3CparametersU3Ek__BackingField;
	ReadOnlyArray_1_t1B44D48F2E9F425D218BABD5DE616117E8725D41 ___U3CprocessorsU3Ek__BackingField;
	uint32_t ___U3CoffsetU3Ek__BackingField;
	uint32_t ___U3CbitU3Ek__BackingField;
	uint32_t ___U3CsizeInBitsU3Ek__BackingField;
	FourCC_tA6CAA4015BC25A7F1053B6C512202D57A9C994ED ___U3CformatU3Ek__BackingField;
	int32_t ___U3CflagsU3Ek__BackingField;
	int32_t ___U3CarraySizeU3Ek__BackingField;
	PrimitiveValue_t1CC37566F40746757D5E3F87474A05909D85C2D4 ___U3CdefaultStateU3Ek__BackingField;
	PrimitiveValue_t1CC37566F40746757D5E3F87474A05909D85C2D4 ___U3CminValueU3Ek__BackingField;
	PrimitiveValue_t1CC37566F40746757D5E3F87474A05909D85C2D4 ___U3CmaxValueU3Ek__BackingField;
};
struct ControlItem_t25B2C46F52E78ADC5F54903F9E769364B02CD4AD_marshaled_pinvoke
{
	InternedString_t8D62A48CB7D85AAE9CFCCCFB0A77AC2844905735_marshaled_pinvoke ___U3CnameU3Ek__BackingField;
	InternedString_t8D62A48CB7D85AAE9CFCCCFB0A77AC2844905735_marshaled_pinvoke ___U3ClayoutU3Ek__BackingField;
	InternedString_t8D62A48CB7D85AAE9CFCCCFB0A77AC2844905735_marshaled_pinvoke ___U3CvariantsU3Ek__BackingField;
	char* ___U3CuseStateFromU3Ek__BackingField;
	char* ___U3CdisplayNameU3Ek__BackingField;
	char* ___U3CshortDisplayNameU3Ek__BackingField;
	ReadOnlyArray_1_t1C2864D7CF4D444AB2616316AC8DD33932F77064 ___U3CusagesU3Ek__BackingField;
	ReadOnlyArray_1_t1C2864D7CF4D444AB2616316AC8DD33932F77064 ___U3CaliasesU3Ek__BackingField;
	ReadOnlyArray_1_t4A15F7D15ACB297B45A08889D51E4CACEAD4EDF9 ___U3CparametersU3Ek__BackingField;
	ReadOnlyArray_1_t1B44D48F2E9F425D218BABD5DE616117E8725D41 ___U3CprocessorsU3Ek__BackingField;
	uint32_t ___U3CoffsetU3Ek__BackingField;
	uint32_t ___U3CbitU3Ek__BackingField;
	uint32_t ___U3CsizeInBitsU3Ek__BackingField;
	FourCC_tA6CAA4015BC25A7F1053B6C512202D57A9C994ED ___U3CformatU3Ek__BackingField;
	int32_t ___U3CflagsU3Ek__BackingField;
	int32_t ___U3CarraySizeU3Ek__BackingField;
	PrimitiveValue_t1CC37566F40746757D5E3F87474A05909D85C2D4_marshaled_pinvoke ___U3CdefaultStateU3Ek__BackingField;
	PrimitiveValue_t1CC37566F40746757D5E3F87474A05909D85C2D4_marshaled_pinvoke ___U3CminValueU3Ek__BackingField;
	PrimitiveValue_t1CC37566F40746757D5E3F87474A05909D85C2D4_marshaled_pinvoke ___U3CmaxValueU3Ek__BackingField;
};
struct ControlItem_t25B2C46F52E78ADC5F54903F9E769364B02CD4AD_marshaled_com
{
	InternedString_t8D62A48CB7D85AAE9CFCCCFB0A77AC2844905735_marshaled_com ___U3CnameU3Ek__BackingField;
	InternedString_t8D62A48CB7D85AAE9CFCCCFB0A77AC2844905735_marshaled_com ___U3ClayoutU3Ek__BackingField;
	InternedString_t8D62A48CB7D85AAE9CFCCCFB0A77AC2844905735_marshaled_com ___U3CvariantsU3Ek__BackingField;
	Il2CppChar* ___U3CuseStateFromU3Ek__BackingField;
	Il2CppChar* ___U3CdisplayNameU3Ek__BackingField;
	Il2CppChar* ___U3CshortDisplayNameU3Ek__BackingField;
	ReadOnlyArray_1_t1C2864D7CF4D444AB2616316AC8DD33932F77064 ___U3CusagesU3Ek__BackingField;
	ReadOnlyArray_1_t1C2864D7CF4D444AB2616316AC8DD33932F77064 ___U3CaliasesU3Ek__BackingField;
	ReadOnlyArray_1_t4A15F7D15ACB297B45A08889D51E4CACEAD4EDF9 ___U3CparametersU3Ek__BackingField;
	ReadOnlyArray_1_t1B44D48F2E9F425D218BABD5DE616117E8725D41 ___U3CprocessorsU3Ek__BackingField;
	uint32_t ___U3CoffsetU3Ek__BackingField;
	uint32_t ___U3CbitU3Ek__BackingField;
	uint32_t ___U3CsizeInBitsU3Ek__BackingField;
	FourCC_tA6CAA4015BC25A7F1053B6C512202D57A9C994ED ___U3CformatU3Ek__BackingField;
	int32_t ___U3CflagsU3Ek__BackingField;
	int32_t ___U3CarraySizeU3Ek__BackingField;
	PrimitiveValue_t1CC37566F40746757D5E3F87474A05909D85C2D4_marshaled_com ___U3CdefaultStateU3Ek__BackingField;
	PrimitiveValue_t1CC37566F40746757D5E3F87474A05909D85C2D4_marshaled_com ___U3CminValueU3Ek__BackingField;
	PrimitiveValue_t1CC37566F40746757D5E3F87474A05909D85C2D4_marshaled_com ___U3CmaxValueU3Ek__BackingField;
};
struct TessellationJobParameters_tA2407D5C15761590BDD217DDE4861964544E8CE2 
{
	bool ___isBorderJob;
	uint8_t ___phase;
	NativeRectParams_t18E2E456D50F027D08BF8B4BCBD83DF7FDB1C02F ___rectParams;
	BorderParams_tA3F0684BE19E681B68F40A773737978EB3FD1509 ___borderParams;
	UnsafeMeshGenerationNode_t1FF7D38D9B39DC59A2F8E2DFC92BFAB2481E88A2 ___node;
};
struct TessellationJobParameters_tA2407D5C15761590BDD217DDE4861964544E8CE2_marshaled_pinvoke
{
	int32_t ___isBorderJob;
	uint8_t ___phase;
	NativeRectParams_t18E2E456D50F027D08BF8B4BCBD83DF7FDB1C02F ___rectParams;
	BorderParams_tA3F0684BE19E681B68F40A773737978EB3FD1509_marshaled_pinvoke ___borderParams;
	UnsafeMeshGenerationNode_t1FF7D38D9B39DC59A2F8E2DFC92BFAB2481E88A2 ___node;
};
struct TessellationJobParameters_tA2407D5C15761590BDD217DDE4861964544E8CE2_marshaled_com
{
	int32_t ___isBorderJob;
	uint8_t ___phase;
	NativeRectParams_t18E2E456D50F027D08BF8B4BCBD83DF7FDB1C02F ___rectParams;
	BorderParams_tA3F0684BE19E681B68F40A773737978EB3FD1509_marshaled_com ___borderParams;
	UnsafeMeshGenerationNode_t1FF7D38D9B39DC59A2F8E2DFC92BFAB2481E88A2 ___node;
};
struct Painter2DJobData_t227572FEAE4A071ED0378501E752A72FF0ACC4EF 
{
	UnsafeMeshGenerationNode_t1FF7D38D9B39DC59A2F8E2DFC92BFAB2481E88A2 ___node;
	int32_t ___snapshotIndex;
	intptr_t ___vectorImagePtr;
	intptr_t ___texturePtr;
	int32_t ___userData;
};
struct Page_tF4CAEB005DC95E53F6992D59C12E548C3C2DE981 
{
	NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF ___array;
	int32_t ___used;
};
struct Predicate_1_t2DA82FB1808C6902625283DE5A7167D87CFA99EE  : public MulticastDelegate_t
{
};
struct Predicate_1_tDA45FFEC703500A529E702D9733C1C2095D663FC  : public MulticastDelegate_t
{
};
struct Predicate_1_tC75D341DFD48D44CB7A8EA7918C14FB58A53AA61  : public MulticastDelegate_t
{
};
struct Predicate_1_t373D1D7AC67DE97C3612E0A5ADCA5CF43CD8E524  : public MulticastDelegate_t
{
};
struct Predicate_1_t0BF32D02A4719C0EC8EDC7814F5B37DDAEC52401  : public MulticastDelegate_t
{
};
struct PropertyCollection_1_t0B49E2F40FE959B1BD3004AC443133FB2334425C 
{
	int32_t ___m_Type;
	RuntimeObject* ___m_Enumerable;
	List_1_t4B846D5AA2F21AD00EE16A815E0984EC65E357A5* ___m_Properties;
	IndexedCollectionPropertyBagEnumerable_1_t25D3E23348A8FB961FD509BA4EA1E5F7E7F00C86 ___m_IndexedCollectionPropertyBag;
};
struct PropertyCollection_1_t91D76A44D231D3212DFE4EB9DFB70B11FAE9D3A6 
{
	int32_t ___m_Type;
	RuntimeObject* ___m_Enumerable;
	List_1_t90F3642AD1F9EB0EFD8C9CA962DA5BF608F75F01* ___m_Properties;
	IndexedCollectionPropertyBagEnumerable_1_tF08CF58F37F195DAB19A7DB4567188C980033D9E ___m_IndexedCollectionPropertyBag;
};
struct ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129  : public ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263
{
};
struct BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E 
{
	PropertyPath_tA523CA2740853534DF6C009C588464B45A6D0A79 ___m_PropertyPath;
	String_t* ___m_Path;
};
struct BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E_marshaled_pinvoke
{
	PropertyPath_tA523CA2740853534DF6C009C588464B45A6D0A79_marshaled_pinvoke ___m_PropertyPath;
	char* ___m_Path;
};
struct BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E_marshaled_com
{
	PropertyPath_tA523CA2740853534DF6C009C588464B45A6D0A79_marshaled_com ___m_PropertyPath;
	Il2CppChar* ___m_Path;
};
struct StyleValue_t56307594EC04E04EFBCC3220595B4AAD66FF93C5 
{
	union
	{
		#pragma pack(push, tp, 1)
		struct
		{
			int32_t ___id;
		};
		#pragma pack(pop, tp)
		struct
		{
			int32_t ___id_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___keyword_OffsetPadding[4];
			int32_t ___keyword;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___keyword_OffsetPadding_forAlignmentOnly[4];
			int32_t ___keyword_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___number_OffsetPadding[8];
			float ___number;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___number_OffsetPadding_forAlignmentOnly[8];
			float ___number_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___length_OffsetPadding[8];
			Length_t90BB06D47DD6DB461ED21BD3E3241FAB6C824256 ___length;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___length_OffsetPadding_forAlignmentOnly[8];
			Length_t90BB06D47DD6DB461ED21BD3E3241FAB6C824256 ___length_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___color_OffsetPadding[8];
			Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___color;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___color_OffsetPadding_forAlignmentOnly[8];
			Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___color_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___position_OffsetPadding[8];
			BackgroundPosition_tF0822B29FC27A67205A9893EBE03D03B799B8B56 ___position;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___position_OffsetPadding_forAlignmentOnly[8];
			BackgroundPosition_tF0822B29FC27A67205A9893EBE03D03B799B8B56 ___position_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___repeat_OffsetPadding[8];
			BackgroundRepeat_t446EC7315DED2C6822F1047B7587C3018BFB277F ___repeat;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___repeat_OffsetPadding_forAlignmentOnly[8];
			BackgroundRepeat_t446EC7315DED2C6822F1047B7587C3018BFB277F ___repeat_forAlignmentOnly;
		};
	};
};
struct Predicate_1_t86E77B1D9A29DD60AA05047257FCF6BBDE992299  : public MulticastDelegate_t
{
};
struct BindingRequest_tB47D4B85CD5F177F9D6891D44DE7958EE23A7172 
{
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___bindingId;
	Binding_tA1358A155852138C1926082E4F3245B6F631CBED* ___binding;
	bool ___shouldProcess;
};
struct BindingRequest_tB47D4B85CD5F177F9D6891D44DE7958EE23A7172_marshaled_pinvoke
{
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E_marshaled_pinvoke ___bindingId;
	Binding_tA1358A155852138C1926082E4F3245B6F631CBED* ___binding;
	int32_t ___shouldProcess;
};
struct BindingRequest_tB47D4B85CD5F177F9D6891D44DE7958EE23A7172_marshaled_com
{
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E_marshaled_com ___bindingId;
	Binding_tA1358A155852138C1926082E4F3245B6F631CBED* ___binding;
	int32_t ___shouldProcess;
};
struct Predicate_1_tEBFA44E2F0D8B114072ABA5F4A6A8C6368D57FD3  : public MulticastDelegate_t
{
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
struct IntPtr_t_StaticFields
{
	intptr_t ___Zero;
};
struct PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F_StaticFields
{
	int32_t ___Invalid;
};
struct Type_t_StaticFields
{
	Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235* ___s_defaultBinder;
	Il2CppChar ___Delimiter;
	TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* ___EmptyTypes;
	RuntimeObject* ___Missing;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterAttribute;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterName;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterNameIgnoreCase;
};
struct PropertyCollection_1_t0B49E2F40FE959B1BD3004AC443133FB2334425C_StaticFields
{
	PropertyCollection_1_t0B49E2F40FE959B1BD3004AC443133FB2334425C ___U3CEmptyU3Ek__BackingField;
};
struct PropertyCollection_1_t91D76A44D231D3212DFE4EB9DFB70B11FAE9D3A6_StaticFields
{
	PropertyCollection_1_t91D76A44D231D3212DFE4EB9DFB70B11FAE9D3A6 ___U3CEmptyU3Ek__BackingField;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C  : public RuntimeArray
{
	ALIGN_FIELD (8) int32_t m_Items[1];

	inline int32_t GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline int32_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, int32_t value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline int32_t GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline int32_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, int32_t value)
	{
		m_Items[index] = value;
	}
};
struct HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9  : public RuntimeArray
{
	ALIGN_FIELD (8) HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* m_Items[1];

	inline HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
struct __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979  : public RuntimeArray
{
	ALIGN_FIELD (8) Il2CppSharedGenericObject* m_Items[1];

	inline Il2CppSharedGenericObject* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Il2CppSharedGenericObject** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Il2CppSharedGenericObject* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Il2CppSharedGenericObject* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Il2CppSharedGenericObject** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Il2CppSharedGenericObject* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};


IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HandleElem__ctor_m804BF4614677F32FDD3943D3548AEF38193AAE26_gshared (HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool LessOrEqual_Invoke_mAA8FF1EA4893BDFD70D0835CA847FC2071099C1B_gshared_inline (LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* __this, Il2CppSharedGenericObject* ___0_lhs, Il2CppSharedGenericObject* ___1_rhs, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PriorityHeap_1_FloatDown_mA61281BC50E33A0D17F62038917A5DCD2B9B6791_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, int32_t ___0_curr, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Resize_TisIl2CppFullySharedGenericAny_m263FC41C8DB989397C43C86556D63CEBE13F4712_fshared (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** ___0_array, int32_t ___1_newSize, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PriorityHeap_1_FloatUp_m806431229F26AE9109A53DED75205958A53C22D3_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, int32_t ___0_curr, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PriorityHeap_1__ctor_m404B1174AB708DEAA67A355FC14D0C20AC37A8E3_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, int32_t ___0_initialSize, LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* ___1_leq, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Stack_1__ctor_m5C333061E1285AC5B22F556173FBE157EF1A37B7_fshared (Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StackItem__ctor_mEFEA9014C002A43DB9F6C31B82273768028C7E38_gshared (StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Stack_1_Push_m072EF82431D7D7E164213D822010141A4C00050D_fshared (Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A* __this, Il2CppFullySharedGenericAny ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Stack_1_Pop_m8E5FF1B4CFD9ADF4D8A7C4CFF4713C83E163A34A_fshared (Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PriorityQueue_1_Swap_m505AC2722E3D347A0BADA61886C2996B1FB296F3_gshared (int32_t* ___0_a, int32_t* ___1_b, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Stack_1_get_Count_mCAD00A69587F884F88C36DE7423C3C329166144E_fshared_inline (Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PriorityHeap_1_Init_m0FEAF05C5BC1D10864549569B1188EB0B0ECAC52_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F PriorityHeap_1_Insert_m9FD54E2AECAE24744F029D84BF6EAFB58C34B0BF_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, Il2CppSharedGenericObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* PriorityHeap_1_ExtractMin_m9E362C33FD48F6129D51C42CE1D593A40D5B0AC6_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool PriorityHeap_1_get_Empty_mAB1FE34E33C96A74B8495EC73C27BD1BAE307D6D_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* PriorityHeap_1_Minimum_m0BB1816D623D514009FC4820909D1F65B92E92B7_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PriorityHeap_1_Remove_mED9F949316AEF8F512926D176876F9595C7658BE_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F ___0_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PropertyBagStore_AddPropertyBag_TisIl2CppFullySharedGenericAny_mB0AA53F19CB33A3C2A29AC822452167CB955F19D_fshared (RuntimeObject* ___0_propertyBag, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PropertyBag_AcceptWithSpecializedVisitor_TisIl2CppFullySharedGenericAny_mDB91562B01F9A25F2996347A1981D24A0DFBBFF1_fshared (RuntimeObject* ___0_properties, RuntimeObject* ___1_visitor, Il2CppFullySharedGenericAny* ___2_container, const RuntimeMethod* method) ;

IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
inline void HandleElem__ctor_m804BF4614677F32FDD3943D3548AEF38193AAE26 (HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* __this, const RuntimeMethod* method)
{
	((  void (*) (HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB*, const RuntimeMethod*))HandleElem__ctor_m804BF4614677F32FDD3943D3548AEF38193AAE26_gshared)(__this, method);
}
inline bool LessOrEqual_Invoke_mAA8FF1EA4893BDFD70D0835CA847FC2071099C1B_inline (LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* __this, Il2CppSharedGenericObject* ___0_lhs, Il2CppSharedGenericObject* ___1_rhs, const RuntimeMethod* method)
{
	return ((  bool (*) (LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E*, Il2CppSharedGenericObject*, Il2CppSharedGenericObject*, const RuntimeMethod*))LessOrEqual_Invoke_mAA8FF1EA4893BDFD70D0835CA847FC2071099C1B_gshared_inline)(__this, ___0_lhs, ___1_rhs, method);
}
inline void PriorityHeap_1_FloatDown_mA61281BC50E33A0D17F62038917A5DCD2B9B6791 (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, int32_t ___0_curr, const RuntimeMethod* method)
{
	((  void (*) (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104*, int32_t, const RuntimeMethod*))PriorityHeap_1_FloatDown_mA61281BC50E33A0D17F62038917A5DCD2B9B6791_gshared)(__this, ___0_curr, method);
}
inline void Array_Resize_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m6BAA7BD6F22421B894347B1476C37052FAC6C916 (Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C** ___0_array, int32_t ___1_newSize, const RuntimeMethod* method)
{
	((  void (*) (Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C**, int32_t, const RuntimeMethod*))Array_Resize_TisIl2CppFullySharedGenericAny_m263FC41C8DB989397C43C86556D63CEBE13F4712_fshared)(___0_array, ___1_newSize, method);
}
inline void Array_Resize_TisHandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB_m8EB2A62758DF71F30AFA683A55D5E700EACB04FC (HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9** ___0_array, int32_t ___1_newSize, const RuntimeMethod* method)
{
	((  void (*) (HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9**, int32_t, const RuntimeMethod*))Array_Resize_TisIl2CppFullySharedGenericAny_m263FC41C8DB989397C43C86556D63CEBE13F4712_fshared)(___0_array, ___1_newSize, method);
}
inline void PriorityHeap_1_FloatUp_m806431229F26AE9109A53DED75205958A53C22D3 (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, int32_t ___0_curr, const RuntimeMethod* method)
{
	((  void (*) (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104*, int32_t, const RuntimeMethod*))PriorityHeap_1_FloatUp_m806431229F26AE9109A53DED75205958A53C22D3_gshared)(__this, ___0_curr, method);
}
inline void PriorityHeap_1__ctor_m404B1174AB708DEAA67A355FC14D0C20AC37A8E3 (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, int32_t ___0_initialSize, LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* ___1_leq, const RuntimeMethod* method)
{
	((  void (*) (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104*, int32_t, LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E*, const RuntimeMethod*))PriorityHeap_1__ctor_m404B1174AB708DEAA67A355FC14D0C20AC37A8E3_gshared)(__this, ___0_initialSize, ___1_leq, method);
}
inline void Stack_1__ctor_mE7517DB8D0D29AFAC6AB7EFC6759983FE098207D (Stack_1_t8C458D9E51035EBC322F988588DD27207004BF20* __this, const RuntimeMethod* method)
{
	((  void (*) (Stack_1_t8C458D9E51035EBC322F988588DD27207004BF20*, const RuntimeMethod*))Stack_1__ctor_m5C333061E1285AC5B22F556173FBE157EF1A37B7_fshared)(__this, method);
}
inline void StackItem__ctor_mEFEA9014C002A43DB9F6C31B82273768028C7E38 (StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A* __this, const RuntimeMethod* method)
{
	((  void (*) (StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A*, const RuntimeMethod*))StackItem__ctor_mEFEA9014C002A43DB9F6C31B82273768028C7E38_gshared)(__this, method);
}
inline void Stack_1_Push_m0DC95C47CDC9040A04E8E5E9961289ACB3F29581 (Stack_1_t8C458D9E51035EBC322F988588DD27207004BF20* __this, StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A* ___0_item, const RuntimeMethod* method)
{
	((  void (*) (Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A*, Il2CppFullySharedGenericAny, const RuntimeMethod*))Stack_1_Push_m072EF82431D7D7E164213D822010141A4C00050D_fshared)((Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A*)__this, (Il2CppFullySharedGenericAny)___0_item, method);
}
inline StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A* Stack_1_Pop_m7BAB8D569A0286FF4095C300887D4728E07EED33 (Stack_1_t8C458D9E51035EBC322F988588DD27207004BF20* __this, const RuntimeMethod* method)
{
	StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A* il2cppRetVal;
	((  void (*) (Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A*, Il2CppFullySharedGenericAny*, const RuntimeMethod*))Stack_1_Pop_m8E5FF1B4CFD9ADF4D8A7C4CFF4713C83E163A34A_fshared)((Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A*)__this, (Il2CppFullySharedGenericAny*)&il2cppRetVal, method);
	return il2cppRetVal;
}
inline void PriorityQueue_1_Swap_m505AC2722E3D347A0BADA61886C2996B1FB296F3 (int32_t* ___0_a, int32_t* ___1_b, const RuntimeMethod* method)
{
	((  void (*) (int32_t*, int32_t*, const RuntimeMethod*))PriorityQueue_1_Swap_m505AC2722E3D347A0BADA61886C2996B1FB296F3_gshared)(___0_a, ___1_b, method);
}
inline int32_t Stack_1_get_Count_m085255880CDBAB0FB60838D61ACC8E6AEA45043A_inline (Stack_1_t8C458D9E51035EBC322F988588DD27207004BF20* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Stack_1_t8C458D9E51035EBC322F988588DD27207004BF20*, const RuntimeMethod*))Stack_1_get_Count_mCAD00A69587F884F88C36DE7423C3C329166144E_fshared_inline)(__this, method);
}
inline void PriorityHeap_1_Init_m0FEAF05C5BC1D10864549569B1188EB0B0ECAC52 (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, const RuntimeMethod* method)
{
	((  void (*) (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104*, const RuntimeMethod*))PriorityHeap_1_Init_m0FEAF05C5BC1D10864549569B1188EB0B0ECAC52_gshared)(__this, method);
}
inline PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F PriorityHeap_1_Insert_m9FD54E2AECAE24744F029D84BF6EAFB58C34B0BF (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, Il2CppSharedGenericObject* ___0_value, const RuntimeMethod* method)
{
	return ((  PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F (*) (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104*, Il2CppSharedGenericObject*, const RuntimeMethod*))PriorityHeap_1_Insert_m9FD54E2AECAE24744F029D84BF6EAFB58C34B0BF_gshared)(__this, ___0_value, method);
}
inline void Array_Resize_TisIl2CppSharedGenericObject_m2368EE7266665D349010E73D8C23E09C635C3038 (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979** ___0_array, int32_t ___1_newSize, const RuntimeMethod* method)
{
	((  void (*) (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979**, int32_t, const RuntimeMethod*))Array_Resize_TisIl2CppFullySharedGenericAny_m263FC41C8DB989397C43C86556D63CEBE13F4712_fshared)(___0_array, ___1_newSize, method);
}
inline Il2CppSharedGenericObject* PriorityHeap_1_ExtractMin_m9E362C33FD48F6129D51C42CE1D593A40D5B0AC6 (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104*, const RuntimeMethod*))PriorityHeap_1_ExtractMin_m9E362C33FD48F6129D51C42CE1D593A40D5B0AC6_gshared)(__this, method);
}
inline bool PriorityHeap_1_get_Empty_mAB1FE34E33C96A74B8495EC73C27BD1BAE307D6D (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104*, const RuntimeMethod*))PriorityHeap_1_get_Empty_mAB1FE34E33C96A74B8495EC73C27BD1BAE307D6D_gshared)(__this, method);
}
inline Il2CppSharedGenericObject* PriorityHeap_1_Minimum_m0BB1816D623D514009FC4820909D1F65B92E92B7 (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104*, const RuntimeMethod*))PriorityHeap_1_Minimum_m0BB1816D623D514009FC4820909D1F65B92E92B7_gshared)(__this, method);
}
inline void PriorityHeap_1_Remove_mED9F949316AEF8F512926D176876F9595C7658BE (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F ___0_handle, const RuntimeMethod* method)
{
	((  void (*) (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104*, PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F, const RuntimeMethod*))PriorityHeap_1_Remove_mED9F949316AEF8F512926D176876F9595C7658BE_gshared)(__this, ___0_handle, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Type_t* Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57 (RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ___0_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TypeTraits_IsContainer_m46D04F3E3219371CC5F133E2CC54BCA46FD72505 (Type_t* ___0_type, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_mA8DBB4C2516B9723C5A41E6CB1E2FAF4BBE96DD8 (String_t* ___0_format, RuntimeObject* ___1_arg0, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162 (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* __this, String_t* ___0_message, const RuntimeMethod* method) ;
inline void PropertyBagStore_AddPropertyBag_TisIl2CppFullySharedGenericAny_mB0AA53F19CB33A3C2A29AC822452167CB955F19D (RuntimeObject* ___0_propertyBag, const RuntimeMethod* method)
{
	((  void (*) (RuntimeObject*, const RuntimeMethod*))PropertyBagStore_AddPropertyBag_TisIl2CppFullySharedGenericAny_mB0AA53F19CB33A3C2A29AC822452167CB955F19D_fshared)(___0_propertyBag, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* __this, String_t* ___0_paramName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987 (String_t* ___0_format, RuntimeObject* ___1_arg0, RuntimeObject* ___2_arg1, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465 (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* __this, String_t* ___0_message, const RuntimeMethod* method) ;
inline void PropertyBag_AcceptWithSpecializedVisitor_TisIl2CppFullySharedGenericAny_mDB91562B01F9A25F2996347A1981D24A0DFBBFF1 (RuntimeObject* ___0_properties, RuntimeObject* ___1_visitor, Il2CppFullySharedGenericAny* ___2_container, const RuntimeMethod* method)
{
	((  void (*) (RuntimeObject*, RuntimeObject*, Il2CppFullySharedGenericAny*, const RuntimeMethod*))PropertyBag_AcceptWithSpecializedVisitor_TisIl2CppFullySharedGenericAny_mDB91562B01F9A25F2996347A1981D24A0DFBBFF1_fshared)(___0_properties, ___1_visitor, ___2_container, method);
}
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Predicate_1__ctor_m2B0279AF2950764FCBFEF3BC1BF616854B3EE3AC_fshared (Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) 
{
	__this->___method_ptr = (intptr_t)il2cpp_codegen_get_direct_method_pointer((RuntimeMethod*)___1_method);
	__this->___method = ___1_method;
	__this->___m_target = ___0_object;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target), (void*)___0_object);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___1_method);
	__this->___method_code = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___1_method))
	{
		bool isOpen = parameterCount == 1;
		if (isOpen)
		{
			__this->___invoke_impl = (intptr_t)((RuntimeMethod*)(__this->___method))->invoker_method;
		}
		else
		{
			__this->___method_code = (intptr_t)__this->___m_target;
			__this->___invoke_impl = (intptr_t)il2cpp_codegen_delegate_invoke_closed_static;
		}
	}
	else
	{
		bool isOpen = parameterCount == 0;
		if (isOpen)
		{
			if (__this->___method_is_virtual)
			{
				if (il2cpp_codegen_method_is_generic_instance_method((RuntimeMethod*)___1_method))
					if (il2cpp_codegen_method_is_interface_method((RuntimeMethod*)___1_method))
						__this->___invoke_impl = (intptr_t)il2cpp_codegen_delegate_invoke_open_generic_interface;
					else
						__this->___invoke_impl = (intptr_t)il2cpp_codegen_delegate_invoke_open_generic_virtual;
				else
					if (il2cpp_codegen_method_is_interface_method((RuntimeMethod*)___1_method))
						__this->___invoke_impl = (intptr_t)il2cpp_codegen_delegate_invoke_open_interface;
					else
						__this->___invoke_impl = (intptr_t)il2cpp_codegen_delegate_invoke_open_virtual;
			}
			else
			{
				__this->___invoke_impl = (intptr_t)il2cpp_codegen_delegate_invoke_open_inst;
			}
		}
		else
		{
			if (___0_object == NULL)
				il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
			if (il2cpp_codegen_class_is_value_type(il2cpp_codegen_object_get_class(__this->___m_target)))
				__this->___method_code = (intptr_t)il2cpp_codegen_get_raw_data<void*>(__this->___m_target);
			else
				__this->___method_code = (intptr_t)__this->___m_target;
			__this->___invoke_impl = (intptr_t)((RuntimeMethod*)(__this->___method))->invoker_method;
		}
	}
	__this->___extra_arg = (intptr_t)il2cpp_codegen_delegate_invoke_multicast;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_m6F76B5644E2D6BD882F23B65434545AD90EFE5A1_fshared (Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* __this, Il2CppFullySharedGenericAny ___0_obj, const RuntimeMethod* method) 
{
	void* args[2];
	args[1] = ___0_obj;
	bool il2cppRetVal;
	((InvokerMethod)(__this->___invoke_impl))((Il2CppMethodPointer)__this->___method_ptr, (const MethodInfo*)__this->___method, (RuntimeObject*)__this->___method_code, args+1, (&il2cppRetVal));
	return il2cppRetVal;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool PriorityHeap_1_get_Empty_mAB1FE34E33C96A74B8495EC73C27BD1BAE307D6D_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____size;
		return (bool)((((int32_t)L_0) == ((int32_t)0))? 1 : 0);
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PriorityHeap_1__ctor_m404B1174AB708DEAA67A355FC14D0C20AC37A8E3_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, int32_t ___0_initialSize, LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* ___1_leq, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2((RuntimeObject*)__this, NULL);
		LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* L_0 = ___1_leq;
		__this->____leq = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____leq), (void*)L_0);
		int32_t L_1 = ___0_initialSize;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_2 = (Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C*)(Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C*)SZArrayNew(Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C_il2cpp_TypeInfo_var, (uint32_t)((int32_t)il2cpp_codegen_add(L_1, 1)));
		__this->____nodes = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____nodes), (void*)L_2);
		int32_t L_3 = ___0_initialSize;
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_4 = (HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9*)(HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0), (uint32_t)((int32_t)il2cpp_codegen_add(L_3, 1)));
		__this->____handles = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____handles), (void*)L_4);
		__this->____size = 0;
		int32_t L_5 = ___0_initialSize;
		__this->____max = L_5;
		__this->____freeList = 0;
		__this->____initialized = (bool)0;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_6 = __this->____nodes;
		(L_6)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(1), (int32_t)1);
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_7 = __this->____handles;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_8 = (HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1));
		HandleElem__ctor_m804BF4614677F32FDD3943D3548AEF38193AAE26(L_8, NULL);
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_9 = L_8;
		Il2CppSharedGenericObject** L_10 = (Il2CppSharedGenericObject**)(&L_9->____key);
		il2cpp_codegen_initobj(L_10, sizeof(Il2CppSharedGenericObject*));
		ArrayElementTypeCheck (L_7, L_9);
		(L_7)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(1), (HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB*)L_9);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PriorityHeap_1_FloatDown_mA61281BC50E33A0D17F62038917A5DCD2B9B6791_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, int32_t ___0_curr, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	int32_t V_2 = 0;
	{
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_0 = __this->____nodes;
		int32_t L_1 = ___0_curr;
		int32_t L_2 = L_1;
		int32_t L_3 = (L_0)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_2));
		V_1 = L_3;
	}

IL_0009:
	{
		int32_t L_4 = ___0_curr;
		V_0 = ((int32_t)(L_4<<1));
		int32_t L_5 = V_0;
		int32_t L_6 = __this->____size;
		if ((((int32_t)L_5) >= ((int32_t)L_6)))
		{
			goto IL_0051;
		}
	}
	{
		LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* L_7 = __this->____leq;
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_8 = __this->____handles;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_9 = __this->____nodes;
		int32_t L_10 = V_0;
		int32_t L_11 = ((int32_t)il2cpp_codegen_add(L_10, 1));
		int32_t L_12 = (L_9)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_11));
		int32_t L_13 = L_12;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_14 = (L_8)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_13));
		Il2CppSharedGenericObject* L_15 = L_14->____key;
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_16 = __this->____handles;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_17 = __this->____nodes;
		int32_t L_18 = V_0;
		int32_t L_19 = L_18;
		int32_t L_20 = (L_17)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_19));
		int32_t L_21 = L_20;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_22 = (L_16)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_21));
		Il2CppSharedGenericObject* L_23 = L_22->____key;
		bool L_24;
		L_24 = LessOrEqual_Invoke_mAA8FF1EA4893BDFD70D0835CA847FC2071099C1B_inline(L_7, L_15, L_23, NULL);
		if (!L_24)
		{
			goto IL_0051;
		}
	}
	{
		int32_t L_25 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_25, 1));
	}

IL_0051:
	{
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_26 = __this->____nodes;
		int32_t L_27 = V_0;
		int32_t L_28 = L_27;
		int32_t L_29 = (L_26)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_28));
		V_2 = L_29;
		int32_t L_30 = V_0;
		int32_t L_31 = __this->____size;
		if ((((int32_t)L_30) > ((int32_t)L_31)))
		{
			goto IL_008a;
		}
	}
	{
		LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* L_32 = __this->____leq;
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_33 = __this->____handles;
		int32_t L_34 = V_1;
		int32_t L_35 = L_34;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_36 = (L_33)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_35));
		Il2CppSharedGenericObject* L_37 = L_36->____key;
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_38 = __this->____handles;
		int32_t L_39 = V_2;
		int32_t L_40 = L_39;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_41 = (L_38)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_40));
		Il2CppSharedGenericObject* L_42 = L_41->____key;
		bool L_43;
		L_43 = LessOrEqual_Invoke_mAA8FF1EA4893BDFD70D0835CA847FC2071099C1B_inline(L_32, L_37, L_42, NULL);
		if (!L_43)
		{
			goto IL_00a2;
		}
	}

IL_008a:
	{
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_44 = __this->____nodes;
		int32_t L_45 = ___0_curr;
		int32_t L_46 = V_1;
		(L_44)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(L_45), (int32_t)L_46);
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_47 = __this->____handles;
		int32_t L_48 = V_1;
		int32_t L_49 = L_48;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_50 = (L_47)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_49));
		int32_t L_51 = ___0_curr;
		L_50->____node = L_51;
		return;
	}

IL_00a2:
	{
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_52 = __this->____nodes;
		int32_t L_53 = ___0_curr;
		int32_t L_54 = V_2;
		(L_52)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(L_53), (int32_t)L_54);
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_55 = __this->____handles;
		int32_t L_56 = V_2;
		int32_t L_57 = L_56;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_58 = (L_55)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_57));
		int32_t L_59 = ___0_curr;
		L_58->____node = L_59;
		int32_t L_60 = V_0;
		___0_curr = L_60;
		goto IL_0009;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PriorityHeap_1_FloatUp_m806431229F26AE9109A53DED75205958A53C22D3_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, int32_t ___0_curr, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	int32_t V_2 = 0;
	{
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_0 = __this->____nodes;
		int32_t L_1 = ___0_curr;
		int32_t L_2 = L_1;
		int32_t L_3 = (L_0)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_2));
		V_1 = L_3;
	}

IL_0009:
	{
		int32_t L_4 = ___0_curr;
		V_0 = ((int32_t)(L_4>>1));
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_5 = __this->____nodes;
		int32_t L_6 = V_0;
		int32_t L_7 = L_6;
		int32_t L_8 = (L_5)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_7));
		V_2 = L_8;
		int32_t L_9 = V_0;
		if (!L_9)
		{
			goto IL_0040;
		}
	}
	{
		LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* L_10 = __this->____leq;
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_11 = __this->____handles;
		int32_t L_12 = V_2;
		int32_t L_13 = L_12;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_14 = (L_11)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_13));
		Il2CppSharedGenericObject* L_15 = L_14->____key;
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_16 = __this->____handles;
		int32_t L_17 = V_1;
		int32_t L_18 = L_17;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_19 = (L_16)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_18));
		Il2CppSharedGenericObject* L_20 = L_19->____key;
		bool L_21;
		L_21 = LessOrEqual_Invoke_mAA8FF1EA4893BDFD70D0835CA847FC2071099C1B_inline(L_10, L_15, L_20, NULL);
		if (!L_21)
		{
			goto IL_0058;
		}
	}

IL_0040:
	{
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_22 = __this->____nodes;
		int32_t L_23 = ___0_curr;
		int32_t L_24 = V_1;
		(L_22)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(L_23), (int32_t)L_24);
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_25 = __this->____handles;
		int32_t L_26 = V_1;
		int32_t L_27 = L_26;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_28 = (L_25)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_27));
		int32_t L_29 = ___0_curr;
		L_28->____node = L_29;
		return;
	}

IL_0058:
	{
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_30 = __this->____nodes;
		int32_t L_31 = ___0_curr;
		int32_t L_32 = V_2;
		(L_30)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(L_31), (int32_t)L_32);
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_33 = __this->____handles;
		int32_t L_34 = V_2;
		int32_t L_35 = L_34;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_36 = (L_33)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_35));
		int32_t L_37 = ___0_curr;
		L_36->____node = L_37;
		int32_t L_38 = V_0;
		___0_curr = L_38;
		goto IL_0009;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PriorityHeap_1_Init_m0FEAF05C5BC1D10864549569B1188EB0B0ECAC52_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->____size;
		V_0 = L_0;
		goto IL_0014;
	}

IL_0009:
	{
		int32_t L_1 = V_0;
		PriorityHeap_1_FloatDown_mA61281BC50E33A0D17F62038917A5DCD2B9B6791(__this, L_1, NULL);
		int32_t L_2 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_subtract(L_2, 1));
	}

IL_0014:
	{
		int32_t L_3 = V_0;
		if ((((int32_t)L_3) >= ((int32_t)1)))
		{
			goto IL_0009;
		}
	}
	{
		__this->____initialized = (bool)1;
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F PriorityHeap_1_Insert_m9FD54E2AECAE24744F029D84BF6EAFB58C34B0BF_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, Il2CppSharedGenericObject* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Array_Resize_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m6BAA7BD6F22421B894347B1476C37052FAC6C916_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	int32_t V_2 = 0;
	PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F V_3;
	memset((&V_3), 0, sizeof(V_3));
	{
		int32_t L_0 = __this->____size;
		V_2 = ((int32_t)il2cpp_codegen_add(L_0, 1));
		int32_t L_1 = V_2;
		__this->____size = L_1;
		int32_t L_2 = V_2;
		V_0 = L_2;
		int32_t L_3 = V_0;
		int32_t L_4 = __this->____max;
		if ((((int32_t)((int32_t)il2cpp_codegen_multiply(L_3, 2))) <= ((int32_t)L_4)))
		{
			goto IL_0051;
		}
	}
	{
		int32_t L_5 = __this->____max;
		__this->____max = ((int32_t)(L_5<<1));
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C** L_6 = (Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C**)(&__this->____nodes);
		int32_t L_7 = __this->____max;
		Array_Resize_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m6BAA7BD6F22421B894347B1476C37052FAC6C916(L_6, ((int32_t)il2cpp_codegen_add(L_7, 1)), Array_Resize_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m6BAA7BD6F22421B894347B1476C37052FAC6C916_RuntimeMethod_var);
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9** L_8 = (HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9**)(&__this->____handles);
		int32_t L_9 = __this->____max;
		Array_Resize_TisHandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB_m8EB2A62758DF71F30AFA683A55D5E700EACB04FC(L_8, ((int32_t)il2cpp_codegen_add(L_9, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
	}

IL_0051:
	{
		int32_t L_10 = __this->____freeList;
		if (L_10)
		{
			goto IL_005d;
		}
	}
	{
		int32_t L_11 = V_0;
		V_1 = L_11;
		goto IL_0077;
	}

IL_005d:
	{
		int32_t L_12 = __this->____freeList;
		V_1 = L_12;
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_13 = __this->____handles;
		int32_t L_14 = V_1;
		int32_t L_15 = L_14;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_16 = (L_13)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_15));
		int32_t L_17 = L_16->____node;
		__this->____freeList = L_17;
	}

IL_0077:
	{
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_18 = __this->____nodes;
		int32_t L_19 = V_0;
		int32_t L_20 = V_1;
		(L_18)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(L_19), (int32_t)L_20);
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_21 = __this->____handles;
		int32_t L_22 = V_1;
		int32_t L_23 = L_22;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_24 = (L_21)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_23));
		if (L_24)
		{
			goto IL_00a7;
		}
	}
	{
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_25 = __this->____handles;
		int32_t L_26 = V_1;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_27 = (HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1));
		HandleElem__ctor_m804BF4614677F32FDD3943D3548AEF38193AAE26(L_27, NULL);
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_28 = L_27;
		Il2CppSharedGenericObject* L_29 = ___0_value;
		L_28->____key = L_29;
		Il2CppCodeGenWriteBarrier((void**)(&L_28->____key), (void*)L_29);
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_30 = L_28;
		int32_t L_31 = V_0;
		L_30->____node = L_31;
		ArrayElementTypeCheck (L_25, L_30);
		(L_25)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(L_26), (HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB*)L_30);
		goto IL_00c3;
	}

IL_00a7:
	{
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_32 = __this->____handles;
		int32_t L_33 = V_1;
		int32_t L_34 = L_33;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_35 = (L_32)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_34));
		int32_t L_36 = V_0;
		L_35->____node = L_36;
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_37 = __this->____handles;
		int32_t L_38 = V_1;
		int32_t L_39 = L_38;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_40 = (L_37)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_39));
		Il2CppSharedGenericObject* L_41 = ___0_value;
		L_40->____key = L_41;
		Il2CppCodeGenWriteBarrier((void**)(&L_40->____key), (void*)L_41);
	}

IL_00c3:
	{
		bool L_42 = __this->____initialized;
		if (!L_42)
		{
			goto IL_00d2;
		}
	}
	{
		int32_t L_43 = V_0;
		PriorityHeap_1_FloatUp_m806431229F26AE9109A53DED75205958A53C22D3(__this, L_43, NULL);
	}

IL_00d2:
	{
		il2cpp_codegen_initobj((&V_3), sizeof(PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F));
		int32_t L_44 = V_1;
		(&V_3)->____handle = L_44;
		PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F L_45 = V_3;
		return L_45;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* PriorityHeap_1_ExtractMin_m9E362C33FD48F6129D51C42CE1D593A40D5B0AC6_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	Il2CppSharedGenericObject* G_B3_0 = NULL;
	Il2CppSharedGenericObject* G_B1_0 = NULL;
	Il2CppSharedGenericObject* G_B2_0 = NULL;
	{
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_0 = __this->____nodes;
		int32_t L_1 = 1;
		int32_t L_2 = (L_0)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_1));
		V_0 = L_2;
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_3 = __this->____handles;
		int32_t L_4 = V_0;
		int32_t L_5 = L_4;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_6 = (L_3)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_5));
		Il2CppSharedGenericObject* L_7 = L_6->____key;
		int32_t L_8 = __this->____size;
		if ((((int32_t)L_8) <= ((int32_t)0)))
		{
			G_B3_0 = L_7;
			goto IL_0091;
		}
		G_B1_0 = L_7;
	}
	{
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_9 = __this->____nodes;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_10 = __this->____nodes;
		int32_t L_11 = __this->____size;
		int32_t L_12 = L_11;
		int32_t L_13 = (L_10)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_12));
		(L_9)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(1), (int32_t)L_13);
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_14 = __this->____handles;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_15 = __this->____nodes;
		int32_t L_16 = 1;
		int32_t L_17 = (L_15)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_16));
		int32_t L_18 = L_17;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_19 = (L_14)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_18));
		L_19->____node = 1;
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_20 = __this->____handles;
		int32_t L_21 = V_0;
		int32_t L_22 = L_21;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_23 = (L_20)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_22));
		Il2CppSharedGenericObject** L_24 = (Il2CppSharedGenericObject**)(&L_23->____key);
		il2cpp_codegen_initobj(L_24, sizeof(Il2CppSharedGenericObject*));
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_25 = __this->____handles;
		int32_t L_26 = V_0;
		int32_t L_27 = L_26;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_28 = (L_25)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_27));
		int32_t L_29 = __this->____freeList;
		L_28->____node = L_29;
		int32_t L_30 = V_0;
		__this->____freeList = L_30;
		int32_t L_31 = __this->____size;
		V_1 = ((int32_t)il2cpp_codegen_subtract(L_31, 1));
		int32_t L_32 = V_1;
		__this->____size = L_32;
		int32_t L_33 = V_1;
		if ((((int32_t)L_33) <= ((int32_t)0)))
		{
			G_B3_0 = G_B1_0;
			goto IL_0091;
		}
		G_B2_0 = G_B1_0;
	}
	{
		PriorityHeap_1_FloatDown_mA61281BC50E33A0D17F62038917A5DCD2B9B6791(__this, 1, NULL);
		G_B3_0 = G_B2_0;
	}

IL_0091:
	{
		return G_B3_0;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* PriorityHeap_1_Minimum_m0BB1816D623D514009FC4820909D1F65B92E92B7_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, const RuntimeMethod* method) 
{
	{
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_0 = __this->____handles;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_1 = __this->____nodes;
		int32_t L_2 = 1;
		int32_t L_3 = (L_1)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_2));
		int32_t L_4 = L_3;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_5 = (L_0)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_4));
		Il2CppSharedGenericObject* L_6 = L_5->____key;
		return L_6;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PriorityHeap_1_Remove_mED9F949316AEF8F512926D176876F9595C7658BE_gshared (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* __this, PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F ___0_handle, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	int32_t V_2 = 0;
	{
		PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F L_0 = ___0_handle;
		int32_t L_1 = L_0.____handle;
		V_0 = L_1;
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_2 = __this->____handles;
		int32_t L_3 = V_0;
		int32_t L_4 = L_3;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_5 = (L_2)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_4));
		int32_t L_6 = L_5->____node;
		V_1 = L_6;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_7 = __this->____nodes;
		int32_t L_8 = V_1;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_9 = __this->____nodes;
		int32_t L_10 = __this->____size;
		int32_t L_11 = L_10;
		int32_t L_12 = (L_9)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_11));
		(L_7)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(L_8), (int32_t)L_12);
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_13 = __this->____handles;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_14 = __this->____nodes;
		int32_t L_15 = V_1;
		int32_t L_16 = L_15;
		int32_t L_17 = (L_14)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_16));
		int32_t L_18 = L_17;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_19 = (L_13)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_18));
		int32_t L_20 = V_1;
		L_19->____node = L_20;
		int32_t L_21 = V_1;
		int32_t L_22 = __this->____size;
		V_2 = ((int32_t)il2cpp_codegen_subtract(L_22, 1));
		int32_t L_23 = V_2;
		__this->____size = L_23;
		int32_t L_24 = V_2;
		if ((((int32_t)L_21) > ((int32_t)L_24)))
		{
			goto IL_009e;
		}
	}
	{
		int32_t L_25 = V_1;
		if ((((int32_t)L_25) <= ((int32_t)1)))
		{
			goto IL_008e;
		}
	}
	{
		LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* L_26 = __this->____leq;
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_27 = __this->____handles;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_28 = __this->____nodes;
		int32_t L_29 = V_1;
		int32_t L_30 = ((int32_t)(L_29>>1));
		int32_t L_31 = (L_28)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_30));
		int32_t L_32 = L_31;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_33 = (L_27)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_32));
		Il2CppSharedGenericObject* L_34 = L_33->____key;
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_35 = __this->____handles;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_36 = __this->____nodes;
		int32_t L_37 = V_1;
		int32_t L_38 = L_37;
		int32_t L_39 = (L_36)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_38));
		int32_t L_40 = L_39;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_41 = (L_35)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_40));
		Il2CppSharedGenericObject* L_42 = L_41->____key;
		bool L_43;
		L_43 = LessOrEqual_Invoke_mAA8FF1EA4893BDFD70D0835CA847FC2071099C1B_inline(L_26, L_34, L_42, NULL);
		if (!L_43)
		{
			goto IL_0097;
		}
	}

IL_008e:
	{
		int32_t L_44 = V_1;
		PriorityHeap_1_FloatDown_mA61281BC50E33A0D17F62038917A5DCD2B9B6791(__this, L_44, NULL);
		goto IL_009e;
	}

IL_0097:
	{
		int32_t L_45 = V_1;
		PriorityHeap_1_FloatUp_m806431229F26AE9109A53DED75205958A53C22D3(__this, L_45, NULL);
	}

IL_009e:
	{
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_46 = __this->____handles;
		int32_t L_47 = V_0;
		int32_t L_48 = L_47;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_49 = (L_46)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_48));
		Il2CppSharedGenericObject** L_50 = (Il2CppSharedGenericObject**)(&L_49->____key);
		il2cpp_codegen_initobj(L_50, sizeof(Il2CppSharedGenericObject*));
		HandleElemU5BU5D_t9E0F6133D78508916DDBD33EA3F0496B767ED3C9* L_51 = __this->____handles;
		int32_t L_52 = V_0;
		int32_t L_53 = L_52;
		HandleElem_tCCC7BA06527BE3B384561D2C511EAA3B49112EFB* L_54 = (L_51)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_53));
		int32_t L_55 = __this->____freeList;
		L_54->____node = L_55;
		int32_t L_56 = V_0;
		__this->____freeList = L_56;
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PriorityQueue_1__ctor_mFD3E3FD052280BE45E827223DE94422EA67E2EDA_gshared (PriorityQueue_1_t9BB8C48DA8C00FA702EFB4995BC789C4EC77D2C4* __this, int32_t ___0_initialSize, LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* ___1_leq, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2((RuntimeObject*)__this, NULL);
		LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* L_0 = ___1_leq;
		__this->____leq = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____leq), (void*)L_0);
		int32_t L_1 = ___0_initialSize;
		LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* L_2 = ___1_leq;
		PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* L_3 = (PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0));
		PriorityHeap_1__ctor_m404B1174AB708DEAA67A355FC14D0C20AC37A8E3(L_3, L_1, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 1));
		__this->____heap = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____heap), (void*)L_3);
		int32_t L_4 = ___0_initialSize;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_5 = (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*)(__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2), (uint32_t)L_4);
		__this->____keys = L_5;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____keys), (void*)L_5);
		__this->____size = 0;
		int32_t L_6 = ___0_initialSize;
		__this->____max = L_6;
		__this->____initialized = (bool)0;
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PriorityQueue_1_Swap_m505AC2722E3D347A0BADA61886C2996B1FB296F3_gshared (int32_t* ___0_a, int32_t* ___1_b, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	{
		int32_t* L_0 = ___0_a;
		int32_t L_1 = *(L_0);
		V_0 = L_1;
		int32_t* L_2 = ___0_a;
		int32_t* L_3 = ___1_b;
		int32_t L_4 = *(L_3);
		*(L_2) = L_4;
		int32_t* L_5 = ___1_b;
		int32_t L_6 = V_0;
		*(L_5) = L_6;
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PriorityQueue_1_Init_m92AD864AFA012389493EAAB6CF7C4047A48759F7_gshared (PriorityQueue_1_t9BB8C48DA8C00FA702EFB4995BC789C4EC77D2C4* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Stack_1_t8C458D9E51035EBC322F988588DD27207004BF20* V_0 = NULL;
	int32_t V_1 = 0;
	int32_t V_2 = 0;
	int32_t V_3 = 0;
	int32_t V_4 = 0;
	int32_t V_5 = 0;
	uint32_t V_6 = 0;
	{
		Stack_1_t8C458D9E51035EBC322F988588DD27207004BF20* L_0 = (Stack_1_t8C458D9E51035EBC322F988588DD27207004BF20*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 3));
		Stack_1__ctor_mE7517DB8D0D29AFAC6AB7EFC6759983FE098207D(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 4));
		V_0 = L_0;
		V_6 = ((int32_t)2016473283);
		V_1 = 0;
		int32_t L_1 = __this->____size;
		V_2 = ((int32_t)il2cpp_codegen_subtract(L_1, 1));
		int32_t L_2 = __this->____size;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_3 = (Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C*)(Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C*)SZArrayNew(Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C_il2cpp_TypeInfo_var, (uint32_t)((int32_t)il2cpp_codegen_add(L_2, 1)));
		__this->____order = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____order), (void*)L_3);
		V_5 = 0;
		int32_t L_4 = V_1;
		V_3 = L_4;
		goto IL_0046;
	}

IL_0032:
	{
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_5 = __this->____order;
		int32_t L_6 = V_3;
		int32_t L_7 = V_5;
		(L_5)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(L_6), (int32_t)L_7);
		int32_t L_8 = V_5;
		V_5 = ((int32_t)il2cpp_codegen_add(L_8, 1));
		int32_t L_9 = V_3;
		V_3 = ((int32_t)il2cpp_codegen_add(L_9, 1));
	}

IL_0046:
	{
		int32_t L_10 = V_3;
		int32_t L_11 = V_2;
		if ((((int32_t)L_10) <= ((int32_t)L_11)))
		{
			goto IL_0032;
		}
	}
	{
		Stack_1_t8C458D9E51035EBC322F988588DD27207004BF20* L_12 = V_0;
		StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A* L_13 = (StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 5));
		StackItem__ctor_mEFEA9014C002A43DB9F6C31B82273768028C7E38(L_13, NULL);
		StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A* L_14 = L_13;
		int32_t L_15 = V_1;
		L_14->___p = L_15;
		StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A* L_16 = L_14;
		int32_t L_17 = V_2;
		L_16->___r = L_17;
		Stack_1_Push_m0DC95C47CDC9040A04E8E5E9961289ACB3F29581(L_12, L_16, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 7));
		goto IL_023c;
	}

IL_0068:
	{
		Stack_1_t8C458D9E51035EBC322F988588DD27207004BF20* L_18 = V_0;
		StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A* L_19;
		L_19 = Stack_1_Pop_m7BAB8D569A0286FF4095C300887D4728E07EED33(L_18, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 8));
		StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A* L_20 = L_19;
		int32_t L_21 = L_20->___p;
		V_1 = L_21;
		int32_t L_22 = L_20->___r;
		V_2 = L_22;
		goto IL_01bb;
	}

IL_0080:
	{
		uint32_t L_23 = V_6;
		V_6 = ((int32_t)il2cpp_codegen_add(((int32_t)il2cpp_codegen_multiply((int32_t)L_23, ((int32_t)1539415821))), 1));
		int32_t L_24 = V_1;
		uint32_t L_25 = V_6;
		uint64_t L_26 = (il2cpp_codegen_conv<uint64_t,uint32_t,int32_t,false,false>(L_25,NULL));
		int32_t L_27 = V_2;
		int32_t L_28 = V_1;
		int64_t L_29 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(((int32_t)il2cpp_codegen_add(((int32_t)il2cpp_codegen_subtract(L_27, L_28)), 1)),NULL));
		int32_t L_30 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>(((int64_t)((int64_t)L_26%L_29)),NULL));
		V_3 = ((int32_t)il2cpp_codegen_add(L_24, L_30));
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_31 = __this->____order;
		int32_t L_32 = V_3;
		int32_t L_33 = L_32;
		int32_t L_34 = (L_31)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_33));
		V_5 = L_34;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_35 = __this->____order;
		int32_t L_36 = V_3;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_37 = __this->____order;
		int32_t L_38 = V_1;
		int32_t L_39 = L_38;
		int32_t L_40 = (L_37)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_39));
		(L_35)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(L_36), (int32_t)L_40);
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_41 = __this->____order;
		int32_t L_42 = V_1;
		int32_t L_43 = V_5;
		(L_41)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(L_42), (int32_t)L_43);
		int32_t L_44 = V_1;
		V_3 = ((int32_t)il2cpp_codegen_subtract(L_44, 1));
		int32_t L_45 = V_2;
		V_4 = ((int32_t)il2cpp_codegen_add(L_45, 1));
	}

IL_00c7:
	{
		int32_t L_46 = V_3;
		V_3 = ((int32_t)il2cpp_codegen_add(L_46, 1));
		LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* L_47 = __this->____leq;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_48 = __this->____keys;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_49 = __this->____order;
		int32_t L_50 = V_3;
		int32_t L_51 = L_50;
		int32_t L_52 = (L_49)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_51));
		int32_t L_53 = L_52;
		Il2CppSharedGenericObject* L_54 = (L_48)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_53));
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_55 = __this->____keys;
		int32_t L_56 = V_5;
		int32_t L_57 = L_56;
		Il2CppSharedGenericObject* L_58 = (L_55)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_57));
		bool L_59;
		L_59 = LessOrEqual_Invoke_mAA8FF1EA4893BDFD70D0835CA847FC2071099C1B_inline(L_47, L_54, L_58, NULL);
		if (!L_59)
		{
			goto IL_00c7;
		}
	}

IL_00f8:
	{
		int32_t L_60 = V_4;
		V_4 = ((int32_t)il2cpp_codegen_subtract(L_60, 1));
		LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* L_61 = __this->____leq;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_62 = __this->____keys;
		int32_t L_63 = V_5;
		int32_t L_64 = L_63;
		Il2CppSharedGenericObject* L_65 = (L_62)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_64));
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_66 = __this->____keys;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_67 = __this->____order;
		int32_t L_68 = V_4;
		int32_t L_69 = L_68;
		int32_t L_70 = (L_67)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_69));
		int32_t L_71 = L_70;
		Il2CppSharedGenericObject* L_72 = (L_66)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_71));
		bool L_73;
		L_73 = LessOrEqual_Invoke_mAA8FF1EA4893BDFD70D0835CA847FC2071099C1B_inline(L_61, L_65, L_72, NULL);
		if (!L_73)
		{
			goto IL_00f8;
		}
	}
	{
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_74 = __this->____order;
		int32_t L_75 = V_3;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_76 = __this->____order;
		int32_t L_77 = V_4;
		PriorityQueue_1_Swap_m505AC2722E3D347A0BADA61886C2996B1FB296F3(((L_74)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_75))), ((L_76)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_77))), NULL);
		int32_t L_78 = V_3;
		int32_t L_79 = V_4;
		if ((((int32_t)L_78) < ((int32_t)L_79)))
		{
			goto IL_00c7;
		}
	}
	{
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_80 = __this->____order;
		int32_t L_81 = V_3;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_82 = __this->____order;
		int32_t L_83 = V_4;
		PriorityQueue_1_Swap_m505AC2722E3D347A0BADA61886C2996B1FB296F3(((L_80)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_81))), ((L_82)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_83))), NULL);
		int32_t L_84 = V_3;
		int32_t L_85 = V_1;
		int32_t L_86 = V_2;
		int32_t L_87 = V_4;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_84, L_85))) >= ((int32_t)((int32_t)il2cpp_codegen_subtract(L_86, L_87)))))
		{
			goto IL_019b;
		}
	}
	{
		Stack_1_t8C458D9E51035EBC322F988588DD27207004BF20* L_88 = V_0;
		StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A* L_89 = (StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 5));
		StackItem__ctor_mEFEA9014C002A43DB9F6C31B82273768028C7E38(L_89, NULL);
		StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A* L_90 = L_89;
		int32_t L_91 = V_4;
		L_90->___p = ((int32_t)il2cpp_codegen_add(L_91, 1));
		StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A* L_92 = L_90;
		int32_t L_93 = V_2;
		L_92->___r = L_93;
		Stack_1_Push_m0DC95C47CDC9040A04E8E5E9961289ACB3F29581(L_88, L_92, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 7));
		int32_t L_94 = V_3;
		V_2 = ((int32_t)il2cpp_codegen_subtract(L_94, 1));
		goto IL_01bb;
	}

IL_019b:
	{
		Stack_1_t8C458D9E51035EBC322F988588DD27207004BF20* L_95 = V_0;
		StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A* L_96 = (StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 5));
		StackItem__ctor_mEFEA9014C002A43DB9F6C31B82273768028C7E38(L_96, NULL);
		StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A* L_97 = L_96;
		int32_t L_98 = V_1;
		L_97->___p = L_98;
		StackItem_tB773502C928ACDEED4C44A56AEFCCC6136843A6A* L_99 = L_97;
		int32_t L_100 = V_3;
		L_99->___r = ((int32_t)il2cpp_codegen_subtract(L_100, 1));
		Stack_1_Push_m0DC95C47CDC9040A04E8E5E9961289ACB3F29581(L_95, L_99, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 7));
		int32_t L_101 = V_4;
		V_1 = ((int32_t)il2cpp_codegen_add(L_101, 1));
	}

IL_01bb:
	{
		int32_t L_102 = V_2;
		int32_t L_103 = V_1;
		if ((((int32_t)L_102) > ((int32_t)((int32_t)il2cpp_codegen_add(L_103, ((int32_t)10))))))
		{
			goto IL_0080;
		}
	}
	{
		int32_t L_104 = V_1;
		V_3 = ((int32_t)il2cpp_codegen_add(L_104, 1));
		goto IL_0238;
	}

IL_01cb:
	{
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_105 = __this->____order;
		int32_t L_106 = V_3;
		int32_t L_107 = L_106;
		int32_t L_108 = (L_105)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_107));
		V_5 = L_108;
		int32_t L_109 = V_3;
		V_4 = L_109;
		goto IL_01f4;
	}

IL_01da:
	{
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_110 = __this->____order;
		int32_t L_111 = V_4;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_112 = __this->____order;
		int32_t L_113 = V_4;
		int32_t L_114 = ((int32_t)il2cpp_codegen_subtract(L_113, 1));
		int32_t L_115 = (L_112)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_114));
		(L_110)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(L_111), (int32_t)L_115);
		int32_t L_116 = V_4;
		V_4 = ((int32_t)il2cpp_codegen_subtract(L_116, 1));
	}

IL_01f4:
	{
		int32_t L_117 = V_4;
		int32_t L_118 = V_1;
		if ((((int32_t)L_117) <= ((int32_t)L_118)))
		{
			goto IL_0229;
		}
	}
	{
		LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* L_119 = __this->____leq;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_120 = __this->____keys;
		int32_t L_121 = V_5;
		int32_t L_122 = L_121;
		Il2CppSharedGenericObject* L_123 = (L_120)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_122));
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_124 = __this->____keys;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_125 = __this->____order;
		int32_t L_126 = V_4;
		int32_t L_127 = ((int32_t)il2cpp_codegen_subtract(L_126, 1));
		int32_t L_128 = (L_125)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_127));
		int32_t L_129 = L_128;
		Il2CppSharedGenericObject* L_130 = (L_124)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_129));
		bool L_131;
		L_131 = LessOrEqual_Invoke_mAA8FF1EA4893BDFD70D0835CA847FC2071099C1B_inline(L_119, L_123, L_130, NULL);
		if (!L_131)
		{
			goto IL_01da;
		}
	}

IL_0229:
	{
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_132 = __this->____order;
		int32_t L_133 = V_4;
		int32_t L_134 = V_5;
		(L_132)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(L_133), (int32_t)L_134);
		int32_t L_135 = V_3;
		V_3 = ((int32_t)il2cpp_codegen_add(L_135, 1));
	}

IL_0238:
	{
		int32_t L_136 = V_3;
		int32_t L_137 = V_2;
		if ((((int32_t)L_136) <= ((int32_t)L_137)))
		{
			goto IL_01cb;
		}
	}

IL_023c:
	{
		Stack_1_t8C458D9E51035EBC322F988588DD27207004BF20* L_138 = V_0;
		int32_t L_139;
		L_139 = Stack_1_get_Count_m085255880CDBAB0FB60838D61ACC8E6AEA45043A_inline(L_138, NULL);
		if ((((int32_t)L_139) > ((int32_t)0)))
		{
			goto IL_0068;
		}
	}
	{
		int32_t L_140 = __this->____size;
		__this->____max = L_140;
		__this->____initialized = (bool)1;
		PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* L_141 = __this->____heap;
		PriorityHeap_1_Init_m0FEAF05C5BC1D10864549569B1188EB0B0ECAC52(L_141, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F PriorityQueue_1_Insert_m1FFF7EBDC8634A6729AC3736CFCB73E026469E6A_gshared (PriorityQueue_1_t9BB8C48DA8C00FA702EFB4995BC789C4EC77D2C4* __this, Il2CppSharedGenericObject* ___0_value, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F V_2;
	memset((&V_2), 0, sizeof(V_2));
	{
		bool L_0 = __this->____initialized;
		if (!L_0)
		{
			goto IL_0015;
		}
	}
	{
		PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* L_1 = __this->____heap;
		Il2CppSharedGenericObject* L_2 = ___0_value;
		PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F L_3;
		L_3 = PriorityHeap_1_Insert_m9FD54E2AECAE24744F029D84BF6EAFB58C34B0BF(L_1, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13));
		return L_3;
	}

IL_0015:
	{
		int32_t L_4 = __this->____size;
		V_0 = L_4;
		int32_t L_5 = __this->____size;
		V_1 = ((int32_t)il2cpp_codegen_add(L_5, 1));
		int32_t L_6 = V_1;
		__this->____size = L_6;
		int32_t L_7 = V_1;
		int32_t L_8 = __this->____max;
		if ((((int32_t)L_7) < ((int32_t)L_8)))
		{
			goto IL_0054;
		}
	}
	{
		int32_t L_9 = __this->____max;
		__this->____max = ((int32_t)(L_9<<1));
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979** L_10 = (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979**)(&__this->____keys);
		int32_t L_11 = __this->____max;
		Array_Resize_TisIl2CppSharedGenericObject_m2368EE7266665D349010E73D8C23E09C635C3038(L_10, L_11, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 14));
	}

IL_0054:
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_12 = __this->____keys;
		int32_t L_13 = V_0;
		Il2CppSharedGenericObject* L_14 = ___0_value;
		(L_12)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(L_13), (Il2CppSharedGenericObject*)L_14);
		il2cpp_codegen_initobj((&V_2), sizeof(PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F));
		int32_t L_15 = V_0;
		(&V_2)->____handle = ((-((int32_t)il2cpp_codegen_add(L_15, 1))));
		PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F L_16 = V_2;
		return L_16;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* PriorityQueue_1_ExtractMin_m87EE71DF9673BD7AB7D83DF413F58C86B991D99A_gshared (PriorityQueue_1_t9BB8C48DA8C00FA702EFB4995BC789C4EC77D2C4* __this, const RuntimeMethod* method) 
{
	Il2CppSharedGenericObject* V_0 = NULL;
	Il2CppSharedGenericObject* V_1 = NULL;
	{
		int32_t L_0 = __this->____size;
		if (L_0)
		{
			goto IL_0014;
		}
	}
	{
		PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* L_1 = __this->____heap;
		Il2CppSharedGenericObject* L_2;
		L_2 = PriorityHeap_1_ExtractMin_m9E362C33FD48F6129D51C42CE1D593A40D5B0AC6(L_1, NULL);
		return L_2;
	}

IL_0014:
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_3 = __this->____keys;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_4 = __this->____order;
		int32_t L_5 = __this->____size;
		int32_t L_6 = ((int32_t)il2cpp_codegen_subtract(L_5, 1));
		int32_t L_7 = (L_4)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_6));
		int32_t L_8 = L_7;
		Il2CppSharedGenericObject* L_9 = (L_3)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_8));
		V_0 = L_9;
		PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* L_10 = __this->____heap;
		bool L_11;
		L_11 = PriorityHeap_1_get_Empty_mAB1FE34E33C96A74B8495EC73C27BD1BAE307D6D(L_10, NULL);
		if (L_11)
		{
			goto IL_0063;
		}
	}
	{
		PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* L_12 = __this->____heap;
		Il2CppSharedGenericObject* L_13;
		L_13 = PriorityHeap_1_Minimum_m0BB1816D623D514009FC4820909D1F65B92E92B7(L_12, NULL);
		V_1 = L_13;
		LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* L_14 = __this->____leq;
		Il2CppSharedGenericObject* L_15 = V_1;
		Il2CppSharedGenericObject* L_16 = V_0;
		bool L_17;
		L_17 = LessOrEqual_Invoke_mAA8FF1EA4893BDFD70D0835CA847FC2071099C1B_inline(L_14, L_15, L_16, NULL);
		if (!L_17)
		{
			goto IL_0063;
		}
	}
	{
		PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* L_18 = __this->____heap;
		Il2CppSharedGenericObject* L_19;
		L_19 = PriorityHeap_1_ExtractMin_m9E362C33FD48F6129D51C42CE1D593A40D5B0AC6(L_18, NULL);
		return L_19;
	}

IL_0063:
	{
		int32_t L_20 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_20, 1));
		int32_t L_21 = __this->____size;
		if ((((int32_t)L_21) <= ((int32_t)0)))
		{
			goto IL_009b;
		}
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_22 = __this->____keys;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_23 = __this->____order;
		int32_t L_24 = __this->____size;
		int32_t L_25 = ((int32_t)il2cpp_codegen_subtract(L_24, 1));
		int32_t L_26 = (L_23)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_25));
		int32_t L_27 = L_26;
		Il2CppSharedGenericObject* L_28 = (L_22)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_27));
		if (!L_28)
		{
			goto IL_0063;
		}
	}

IL_009b:
	{
		Il2CppSharedGenericObject* L_29 = V_0;
		return L_29;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* PriorityQueue_1_Minimum_m3CACE160557CA87C00BAB68E547368F41A8AC587_gshared (PriorityQueue_1_t9BB8C48DA8C00FA702EFB4995BC789C4EC77D2C4* __this, const RuntimeMethod* method) 
{
	Il2CppSharedGenericObject* V_0 = NULL;
	Il2CppSharedGenericObject* V_1 = NULL;
	{
		int32_t L_0 = __this->____size;
		if (L_0)
		{
			goto IL_0014;
		}
	}
	{
		PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* L_1 = __this->____heap;
		Il2CppSharedGenericObject* L_2;
		L_2 = PriorityHeap_1_Minimum_m0BB1816D623D514009FC4820909D1F65B92E92B7(L_1, NULL);
		return L_2;
	}

IL_0014:
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_3 = __this->____keys;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_4 = __this->____order;
		int32_t L_5 = __this->____size;
		int32_t L_6 = ((int32_t)il2cpp_codegen_subtract(L_5, 1));
		int32_t L_7 = (L_4)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_6));
		int32_t L_8 = L_7;
		Il2CppSharedGenericObject* L_9 = (L_3)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_8));
		V_0 = L_9;
		PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* L_10 = __this->____heap;
		bool L_11;
		L_11 = PriorityHeap_1_get_Empty_mAB1FE34E33C96A74B8495EC73C27BD1BAE307D6D(L_10, NULL);
		if (L_11)
		{
			goto IL_0059;
		}
	}
	{
		PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* L_12 = __this->____heap;
		Il2CppSharedGenericObject* L_13;
		L_13 = PriorityHeap_1_Minimum_m0BB1816D623D514009FC4820909D1F65B92E92B7(L_12, NULL);
		V_1 = L_13;
		LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* L_14 = __this->____leq;
		Il2CppSharedGenericObject* L_15 = V_1;
		Il2CppSharedGenericObject* L_16 = V_0;
		bool L_17;
		L_17 = LessOrEqual_Invoke_mAA8FF1EA4893BDFD70D0835CA847FC2071099C1B_inline(L_14, L_15, L_16, NULL);
		if (!L_17)
		{
			goto IL_0059;
		}
	}
	{
		Il2CppSharedGenericObject* L_18 = V_1;
		return L_18;
	}

IL_0059:
	{
		Il2CppSharedGenericObject* L_19 = V_0;
		return L_19;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PriorityQueue_1_Remove_m9A8BE5F888D5CDAE7EEE7E42B2EDD23FC3A6346B_gshared (PriorityQueue_1_t9BB8C48DA8C00FA702EFB4995BC789C4EC77D2C4* __this, PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F ___0_handle, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	Il2CppSharedGenericObject* V_1 = NULL;
	{
		PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F L_0 = ___0_handle;
		int32_t L_1 = L_0.____handle;
		V_0 = L_1;
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_0018;
		}
	}
	{
		PriorityHeap_1_t479A82584B269C1AFAD7E2AF32318A4695961104* L_3 = __this->____heap;
		PQHandle_tD8B673DD5F9F2EF4E72C8A86AD41A78DE7F6C42F L_4 = ___0_handle;
		PriorityHeap_1_Remove_mED9F949316AEF8F512926D176876F9595C7658BE(L_3, L_4, NULL);
		return;
	}

IL_0018:
	{
		int32_t L_5 = V_0;
		V_0 = ((-((int32_t)il2cpp_codegen_add(L_5, 1))));
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_6 = __this->____keys;
		int32_t L_7 = V_0;
		il2cpp_codegen_initobj((&V_1), sizeof(Il2CppSharedGenericObject*));
		Il2CppSharedGenericObject* L_8 = V_1;
		(L_6)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(L_7), (Il2CppSharedGenericObject*)L_8);
		goto IL_0042;
	}

IL_0034:
	{
		int32_t L_9 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_9, 1));
	}

IL_0042:
	{
		int32_t L_10 = __this->____size;
		if ((((int32_t)L_10) <= ((int32_t)0)))
		{
			goto IL_006c;
		}
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_11 = __this->____keys;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_12 = __this->____order;
		int32_t L_13 = __this->____size;
		int32_t L_14 = ((int32_t)il2cpp_codegen_subtract(L_13, 1));
		int32_t L_15 = (L_12)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_14));
		int32_t L_16 = L_15;
		Il2CppSharedGenericObject* L_17 = (L_11)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_16));
		if (!L_17)
		{
			goto IL_0034;
		}
	}

IL_006c:
	{
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ProfilerCounterValue_1__ctor_mAE7E1C99FD4CC68319C5BA7AC7176C810C2993DC_fshared (ProfilerCounterValue_1_t83A85DB82AFFCFD2CF3AEF7012D7794FEDC031D7* __this, ProfilerCategory_tA55212CD512C618AF6D2147791F20319896592AC ___0_category, String_t* ___1_name, uint8_t ___2_dataUnit, uint16_t ___3_counterOptions, const RuntimeMethod* method) 
{
	{
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PropertyBag_1__cctor_m7132EB86C61D9CFCD8D60094ABF4791FCB0CD0B5_fshared (const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		bool L_2;
		L_2 = TypeTraits_IsContainer_m46D04F3E3219371CC5F133E2CC54BCA46FD72505(L_1, NULL);
		if (L_2)
		{
			goto IL_002b;
		}
	}
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_3 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_4;
		L_4 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_3, NULL);
		String_t* L_5;
		L_5 = String_Format_mA8DBB4C2516B9723C5A41E6CB1E2FAF4BBE96DD8(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralBFD0F59F9F38A92660652CA5BE172EFCA8501E47)), L_4, NULL);
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_6 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_6, L_5, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_6, method);
	}

IL_002b:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PropertyBag_1_Unity_Properties_Internal_IPropertyBagRegister_Register_m54B6F69083B35A3F7C301A639D48050B235A6D7F_fshared (PropertyBag_1_t74F4963AD6B656900B7CACFC37AC3CDDDF818409* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PropertyBagStore_tF76E67D0FF5460D8B59B033B6EB4E2A2E446024C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(PropertyBagStore_tF76E67D0FF5460D8B59B033B6EB4E2A2E446024C_StaticInit);
	{
		CHECKED_LOCAL_INIT(PropertyBagStore_tF76E67D0FF5460D8B59B033B6EB4E2A2E446024C_StaticInit,(PropertyBagStore_tF76E67D0FF5460D8B59B033B6EB4E2A2E446024C_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		PropertyBagStore_AddPropertyBag_TisIl2CppFullySharedGenericAny_mB0AA53F19CB33A3C2A29AC822452167CB955F19D((RuntimeObject*)__this, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 2));
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PropertyBag_1_Accept_mB0872E61EC1AD02FC6F72F936F04557118FF271A_fshared (PropertyBag_1_t74F4963AD6B656900B7CACFC37AC3CDDDF818409* __this, RuntimeObject* ___0_visitor, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = ___0_visitor;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_1 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralC23725EE1ACD9996F8909AD0C76D39D2B4CB3ACA)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, method);
	}

IL_000e:
	{
		RuntimeObject* L_2 = ___0_visitor;
		GenericInterfaceActionInvoker0::Invoke(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 3), L_2);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PropertyBag_1_Unity_Properties_IPropertyBag_Accept_mF954ED782F35F4B80660336C424100D6B458F975_fshared (PropertyBag_1_t74F4963AD6B656900B7CACFC37AC3CDDDF818409* __this, RuntimeObject* ___0_visitor, RuntimeObject** ___1_container, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_TContainer_tDD41E5BDFD434E31190676C56EDFF26DCF9FB837 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1));
	CHECKED_LOCAL(Type_t_StaticInit);
	const Il2CppFullySharedGenericAny L_7 = alloca(SizeOf_TContainer_tDD41E5BDFD434E31190676C56EDFF26DCF9FB837);
	const Il2CppFullySharedGenericAny L_18 = L_7;
	Il2CppFullySharedGenericAny V_0 = alloca(SizeOf_TContainer_tDD41E5BDFD434E31190676C56EDFF26DCF9FB837);
	memset(V_0, 0, SizeOf_TContainer_tDD41E5BDFD434E31190676C56EDFF26DCF9FB837);
	RuntimeObject* V_1 = NULL;
	{
		RuntimeObject** L_0 = ___1_container;
		RuntimeObject* L_1 = *(L_0);
		if (L_1)
		{
			goto IL_000f;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_2 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_2, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral0074C49CE7D7ED9232C28459AA9DB19B1D06C223)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_2, method);
	}

IL_000f:
	{
		RuntimeObject** L_3 = ___1_container;
		RuntimeObject* L_4 = *(L_3);
		V_1 = L_4;
		RuntimeObject* L_5 = V_1;
		if (!((RuntimeObject*)IsInst((RuntimeObject*)L_5, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1))))
		{
			goto IL_0023;
		}
	}
	{
		RuntimeObject* L_6 = V_1;
		void* L_8 = UnBox_Any(L_6, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), L_7);
		il2cpp_codegen_memcpy(V_0, (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_8)), SizeOf_TContainer_tDD41E5BDFD434E31190676C56EDFF26DCF9FB837);
		goto IL_0044;
	}

IL_0023:
	{
		RuntimeObject** L_9 = ___1_container;
		RuntimeObject* L_10 = *(L_9);
		Type_t* L_11;
		L_11 = il2cpp_codegen_object_get_type(L_10);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_12 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_13;
		L_13 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_12, NULL);
		String_t* L_14;
		L_14 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral146E57E580085C49FA4D7B226DB5FF8C1F7C5518)), L_11, L_13, NULL);
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_15 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_15, L_14, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_15, method);
	}

IL_0044:
	{
		RuntimeObject* L_16 = ___0_visitor;
		PropertyBag_AcceptWithSpecializedVisitor_TisIl2CppFullySharedGenericAny_mDB91562B01F9A25F2996347A1981D24A0DFBBFF1((RuntimeObject*)__this, L_16, (Il2CppFullySharedGenericAny*)V_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 4));
		RuntimeObject** L_17 = ___1_container;
		il2cpp_codegen_memcpy(L_18, V_0, SizeOf_TContainer_tDD41E5BDFD434E31190676C56EDFF26DCF9FB837);
		RuntimeObject* L_19 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), L_18);
		*(L_17) = L_19;
		Il2CppCodeGenWriteBarrier((void**)L_17, (void*)L_19);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PropertyBag_1_Unity_Properties_IPropertyBagU3CTContainerU3E_Accept_m0947C6DD88E15158A0D96620D397C94711A31657_fshared (PropertyBag_1_t74F4963AD6B656900B7CACFC37AC3CDDDF818409* __this, RuntimeObject* ___0_visitor, Il2CppFullySharedGenericAny* ___1_container, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = ___0_visitor;
		Il2CppFullySharedGenericAny* L_1 = ___1_container;
		GenericInterfaceActionInvoker2< RuntimeObject*, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5), L_0, (RuntimeObject*)__this, L_1);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PropertyBag_1_Unity_Properties_IPropertyBagU3CTContainerU3E_GetProperties_m1F6375B2ED52299AEBC0E2B190FD958394F8BB46_fshared (PropertyBag_1_t74F4963AD6B656900B7CACFC37AC3CDDDF818409* __this, PropertyCollection_1_t5813F70F52FEC5B64A85FE040869894365CCF94D* il2cppRetVal, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_PropertyCollection_1_t023CA0F2B326ED320DF74F5E011F7F8008A5DC80 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const PropertyCollection_1_t5813F70F52FEC5B64A85FE040869894365CCF94D L_0 = alloca(SizeOf_PropertyCollection_1_t023CA0F2B326ED320DF74F5E011F7F8008A5DC80);
	{
		VirtualActionInvoker1Invoker< PropertyCollection_1_t5813F70F52FEC5B64A85FE040869894365CCF94D* >::Invoke(12, __this, (PropertyCollection_1_t5813F70F52FEC5B64A85FE040869894365CCF94D*)L_0);
		il2cpp_codegen_memcpy(il2cppRetVal, L_0, SizeOf_PropertyCollection_1_t023CA0F2B326ED320DF74F5E011F7F8008A5DC80);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PropertyBag_1_Unity_Properties_IPropertyBagU3CTContainerU3E_GetProperties_mF6BCB70A56083644373C4BEC94C1F6745E0FBE99_fshared (PropertyBag_1_t74F4963AD6B656900B7CACFC37AC3CDDDF818409* __this, Il2CppFullySharedGenericAny* ___0_container, PropertyCollection_1_t5813F70F52FEC5B64A85FE040869894365CCF94D* il2cppRetVal, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_PropertyCollection_1_t023CA0F2B326ED320DF74F5E011F7F8008A5DC80 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const PropertyCollection_1_t5813F70F52FEC5B64A85FE040869894365CCF94D L_1 = alloca(SizeOf_PropertyCollection_1_t023CA0F2B326ED320DF74F5E011F7F8008A5DC80);
	{
		Il2CppFullySharedGenericAny* L_0 = ___0_container;
		VirtualActionInvoker2Invoker< Il2CppFullySharedGenericAny*, PropertyCollection_1_t5813F70F52FEC5B64A85FE040869894365CCF94D* >::Invoke(13, __this, L_0, (PropertyCollection_1_t5813F70F52FEC5B64A85FE040869894365CCF94D*)L_1);
		il2cpp_codegen_memcpy(il2cppRetVal, L_1, SizeOf_PropertyCollection_1_t023CA0F2B326ED320DF74F5E011F7F8008A5DC80);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t PropertyBag_1_Unity_Properties_IConstructor_get_InstantiationKind_m2C299F894780F5CCB18B26BEF99C95EAC40D2C95_fshared (PropertyBag_1_t74F4963AD6B656900B7CACFC37AC3CDDDF818409* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0;
		L_0 = VirtualFuncInvoker0< int32_t >::Invoke(14, __this);
		return L_0;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PropertyBag_1_Unity_Properties_IConstructorU3CTContainerU3E_Instantiate_m8AB68F3EC8FB382C1A64FD54DF8E23CC2543E5EA_fshared (PropertyBag_1_t74F4963AD6B656900B7CACFC37AC3CDDDF818409* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_TContainer_tDD41E5BDFD434E31190676C56EDFF26DCF9FB837 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1));
	const Il2CppFullySharedGenericAny L_0 = alloca(SizeOf_TContainer_tDD41E5BDFD434E31190676C56EDFF26DCF9FB837);
	{
		VirtualActionInvoker1Invoker< Il2CppFullySharedGenericAny* >::Invoke(15, __this, (Il2CppFullySharedGenericAny*)L_0);
		il2cpp_codegen_memcpy(il2cppRetVal, L_0, SizeOf_TContainer_tDD41E5BDFD434E31190676C56EDFF26DCF9FB837);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t PropertyBag_1_get_InstantiationKind_m12E943B094001EC2736190825F084562FBE84235_fshared (PropertyBag_1_t74F4963AD6B656900B7CACFC37AC3CDDDF818409* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___U3CInstantiationKindU3Ek__BackingField;
		return L_0;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PropertyBag_1_Instantiate_mD74F4CF48BC2E122C974C038AB838190C3988783_fshared (PropertyBag_1_t74F4963AD6B656900B7CACFC37AC3CDDDF818409* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_TContainer_tDD41E5BDFD434E31190676C56EDFF26DCF9FB837 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1));
	const Il2CppFullySharedGenericAny L_0 = alloca(SizeOf_TContainer_tDD41E5BDFD434E31190676C56EDFF26DCF9FB837);
	Il2CppFullySharedGenericAny V_0 = alloca(SizeOf_TContainer_tDD41E5BDFD434E31190676C56EDFF26DCF9FB837);
	memset(V_0, 0, SizeOf_TContainer_tDD41E5BDFD434E31190676C56EDFF26DCF9FB837);
	{
		il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_0, SizeOf_TContainer_tDD41E5BDFD434E31190676C56EDFF26DCF9FB837);
		il2cpp_codegen_memcpy(L_0, V_0, SizeOf_TContainer_tDD41E5BDFD434E31190676C56EDFF26DCF9FB837);
		il2cpp_codegen_memcpy(il2cppRetVal, L_0, SizeOf_TContainer_tDD41E5BDFD434E31190676C56EDFF26DCF9FB837);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PropertyBag_1__ctor_mF9D3CBA5488FDEB46B255A12E5CF4DDF4BD76175_fshared (PropertyBag_1_t74F4963AD6B656900B7CACFC37AC3CDDDF818409* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2((RuntimeObject*)__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool LessOrEqual_Invoke_mAA8FF1EA4893BDFD70D0835CA847FC2071099C1B_gshared_inline (LessOrEqual_t9223CA4F63316B0C8CB7E7A818C3ACFC23195A8E* __this, Il2CppSharedGenericObject* ___0_lhs, Il2CppSharedGenericObject* ___1_rhs, const RuntimeMethod* method) 
{
	typedef bool (*FunctionPointerType) (RuntimeObject*, Il2CppSharedGenericObject*, Il2CppSharedGenericObject*, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_lhs, ___1_rhs, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Stack_1_get_Count_mCAD00A69587F884F88C36DE7423C3C329166144E_fshared_inline (Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____size;
		return L_0;
	}
}
