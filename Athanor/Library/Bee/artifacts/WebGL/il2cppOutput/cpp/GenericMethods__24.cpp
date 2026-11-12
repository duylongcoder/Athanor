#include "pch-cpp.hpp"





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
template <typename T1>
struct GenericVirtualActionInvoker1
{
	typedef void (*Action)(void*,T1,const RuntimeMethod*);

	static inline void Invoke (const RuntimeMethod* method, RuntimeObject* obj, T1 p1)
	{
		VirtualInvokeData invokeData;
		il2cpp_codegen_get_generic_virtual_invoke_data(method, obj, &invokeData);
		((Action)invokeData.methodPtr)(obj,p1,invokeData.method);
	}
};
struct InterfaceActionInvoker0
{
	typedef void (*Action)(void*,const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj,invokeData.method);
	}
};
template <typename T1>
struct InterfaceActionInvoker1Invoker;
template <typename T1>
struct InterfaceActionInvoker1Invoker<T1*>
{
	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1* p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		void* params[1] = { p1 };
		invokeData.method->invoker_method(il2cpp_codegen_get_method_pointer(invokeData.method), invokeData.method, obj, params, params[0]);
	}
};
template <typename R>
struct InterfaceFuncInvoker0
{
	typedef R (*Func)(void*,const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj,invokeData.method);
	}
};
template <typename R, typename T1, typename T2>
struct InterfaceFuncInvoker2Invoker;
template <typename R, typename T1, typename T2>
struct InterfaceFuncInvoker2Invoker<R, T1*, T2*>
{
	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1* p1, T2* p2)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		R ret;
		void* params[2] = { p1, p2 };
		invokeData.method->invoker_method(il2cpp_codegen_get_method_pointer(invokeData.method), invokeData.method, obj, params, &ret);
		return ret;
	}
};

struct Action_1_tF18EC740BBE61E110C0C1270C9A2B66A1A72DBE1;
struct Action_1_t3DC3411926243F1DB9C330F8E105B904E38C1A0B;
struct Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505;
struct Action_1_t923A20D1D4F6B55B2ED5AE21B90F1A0CE0450D99;
struct ConstantBufferSingleton_1_t800A1489DAFDDB639BB1BC6406F901FBDE4F0F69;
struct ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D;
struct Dictionary_2_t7E2B528AFC59C90DE8916832B84A9936D8967195;
struct Dictionary_2_tD77528C78AECDC8F6DF50F36B79092A52A270629;
struct Dictionary_2_t6B4B40DA71BA0031F5736DF4459253299D8AF5F2;
struct Dictionary_2_tBCCCFBCAC02A3C03E3C84D75696D4860D7444A35;
struct Dictionary_2_t4DF54DED090D74EA3A4143562494256C758AB504;
struct Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588;
struct Dictionary_2_t4055F6540F36F21F9FEDAFB92D8E0089B38EBBC8;
struct EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482;
struct EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE;
struct Func_1_tE7A32FE928894E3B981CAD581AC7D8ECD5CC262A;
struct Func_1_t566D4A654882E282E54CDC3A001E05486F913903;
struct Func_1_tBB8824FA8746333BFFF3AB3CE4A41B58450AF431;
struct Func_2_t3793651739D8B94095DFFF75C3BD348E5E2C455C;
struct Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0;
struct HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2;
struct IEnumerable_1_tF5487DDC9BFE72D77C0A279762BD5851F02B4F03;
struct IEnumerable_1_t6D5FFB1B4A97F30426C5D394135AD0D2FAEAE680;
struct IEnumerable_1_t29E7244AE33B71FA0981E50D5BC73B7938F35C66;
struct IEnumerator_1_t75CB2681E18F7F2791528FA2CA60361FDB5DA08D;
struct IList_1_t09217E1EDF7CCAED72B667F406AAC92AB64B8790;
struct IList_1_t1E95A05AFA88C09BA96A40B431F7D5867BD0D622;
struct IPropertyBag_1_t36BB94A0E64BDC3C7B44E3C33CD0562C77748D3F;
struct IReadOnlyDictionary_2_tAAE73DD0F1693587789EB2283CCA3AEA0DFB6839;
struct List_1_t0CA3CB19410DCFCBF6D92C5A74C84FC883A36CD8;
struct List_1_tAF3FEA7536A43A4D9943A753A42160EEFC935955;
struct List_1_t584CB490C8F4C21E0A0D5545409ED60BF71F3FE4;
struct List_1_t888EAF80C1646E9D51193F9F6E183344845FB66C;
struct List_1_t763DE56D8B401C117651B07CA70E79E840BD9B3F;
struct List_1_t96E9133B70FB6765E6B138E810D33E18901715DA;
struct List_1_tB1D3AE6638063900DFF2ED463335EF8912FBBF7B;
struct List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD;
struct List_1_tEA16F82F7871418E28EB6F551D77A8AD9F2E337F;
struct List_1_t9B68833848E4C4D7F623C05F6B77F0449396354A;
struct List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95;
struct List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E;
struct List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A;
struct List_1_tBDD12EAD3C5C46706730C230F223EE020C6822D6;
struct ObjectPool_1_t28F249C599ED2EFD04F6B973FEFA2541B52062DF;
struct ObjectPool_1_tC6F0B8E0BAAEC07474AABA8496261838C79CE1F3;
struct ObjectPool_1_tE788CDA6BB77BAACFA8AF7030ABD8929A4C0989C;
struct ObjectPool_1_t5CBB55FD447658CA0C9C56C5AB2D197BCBDF6C1F;
struct ObjectPool_1_t9CFCA41CD780F7424037FA2A3EE846F2FBFEE4D0;
struct PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816;
struct Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107;
struct SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660;
struct TypeConverter_2_tA6276E0BDB8DB9E3B10AF0C31301ACC071589845;
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct ComponentU5BU5D_t40ECDBC5CC15EA282AF49771C20EBFDADC532D0E;
struct ComputedTransitionPropertyU5BU5D_t25B9E78F5276CDA297C8215C316452CAB8219E82;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct PropertyPathPartU5BU5D_t7994D542F14DDDDEABB1792C335C20149399AEBB;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
struct UInt32U5BU5D_t02FBD658AD156A17574ECE6106CF1FBFCC9807FA;
struct Vector3U5BU5D_tFF1859CCE176131B909E2044F76443064254679C;
struct __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979;
struct __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC;
struct ButtonClickStatusU5BU5D_t9327A993382F70450196067EE5DED53834E9DB7B;
struct ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB;
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263;
struct ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129;
struct ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F;
struct Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA;
struct BaseVisualElementPanel_tE3811F3D1474B72CB6CD5BCEECFF5B5CBEC1E303;
struct Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235;
struct BurstCompilerOptions_t1AFF6FD4375BCFD72E3B1C4ABF5AA4888C3DF05D;
struct CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4;
struct ClickDetector_t6B5A82C99CFD12E051D8E84A7C8F7488355B8F31;
struct CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7;
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3;
struct ComputeBuffer_t51EADA9015EBCC1B982C5584E9AB2734415A8233;
struct ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8;
struct ConcreteTypeVisitor_tC41932325C8C25F9B634923360AE061521744BB2;
struct ConstructorInfo_t1B5967EE7E5554272F79F8880183C70AD240EEEB;
struct ContextContainer_t384E08F63FABEFF6A7F1A753F5EF02699DAD414C;
struct ContextItem_tFC5ECB349F0410923AE6BC2A9CAACA3589B51086;
struct ConverterGroup_t2D7DEDAC22662EF5C020DE54231DC23CD0B5268A;
struct CullingAllocationInfo_tB260F5CD0B290F74E145EB16E54B901CC68D9D5A;
struct Delegate_t;
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
struct Event_tEBC6F24B56CE22B9C9AD1AC6C24A6B83BC3860CB;
struct EventArgValue_t12B444AF86832453534803922BF5FCE39CFD0E85;
struct EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C;
struct EventCallbackInternal_t74403797F0007ACB72019EE51D2C4DC33E5F63AF;
struct EventCallbackList_tC6B518C9C5192317B413145F9B9A3678EEE5BF1E;
struct EventCallbackListPool_t258FFD0B864AC29862A6ACF4F56A0340621783B2;
struct EventCallbackPool_t337A1A0692B33BCB6B3D2C662666D78276F9CD3C;
struct EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85;
struct GameObject_t76FEDD663AB33C991A9C9A23129337651094216F;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct IEventHandler_tB1627CA1B7729F3E714572E69A79C91A1578C9A3;
struct IMouseEvent_tF59A3DD3CA593DB23B47764DF0521996D509A59E;
struct IProperty_t9DAAF3D1ACD042DA2D622152506F58CE625EF697;
struct IPropertyBagVisitor_tE8B9B7E14DBF946E6E65BD0ECC8679268715BF5A;
struct IndexOutOfRangeException_t7ECB35264FB6CA8FAA516BD958F4B2ADC78E8A82;
struct InlineStyleAccess_t5CA7877999C9442491A220AE50D605C84D09A165;
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB;
struct MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553;
struct MemberInfo_t;
struct MethodInfo_t;
struct PathRef_t76F7677792A14AF9D6A6EAB7F08C1A3DC2B27A55;
struct PathVisitor_tCD0947C41CDBDB774D22D560E5FFA01EF61C4446;
struct Regex_tE773142C2BE45C5D362B0F815AFF831707A51772;
struct RenderData_t1ABE116B2B5E0409AC699E195922516606531DC2;
struct ResolvedStyleAccess_t226CC840EBACEE31CE1139ED5F717532AFFAEB45;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct String_t;
struct StringBuilder_t;
struct StyleVariableContext_tF74F2787CE1F6BEBBFBFF0771CF493AC9E403527;
struct Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1;
struct Type_t;
struct VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115;
struct VisualElementSelectorData_t4C4043092A19A653CE8637C7659E36E0D3CC23A0;
struct VisualElementTransformData_t3DD575B5990B68FF956673EFF036171C86A38DF3;
struct VisualTreeAsset_tFB5BF81F0780A412AE5A7C2C552B3EEA64EA2EEB;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
struct ButtonClickStatus_tD4248512DB59E16501C4DDB6EDC2C6DE04290470;
struct LazyCAttrData_t4F8036F83C070762609DB213B69F7FAA2D34ACA3;
struct TypeData_t9C5DC5F5DD8F0C04131F359229ED4738117BA396;
struct Record_tD4D528E1BFF948E92C590DCE7A010BA356D50049;
struct EqualityComparer_tF8FCE99C5DBB2F35D74728CB65346435381CFD5B;
struct Data_t6BD087CC0FA9794D342D260035A70E365224C66E;
struct TypeData_t01D670B4E71B5571B38C7412B1E652A47D6AF66A;

IL2CPP_EXTERN_C RuntimeClass* AllocatorManager_tFB15A22029C8159A3DCD4C08935BE57D3E6B3C2C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* BurstCompiler_t6972AFDB97C8649095A1B7EE54AAADB676BE2C37_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ContextContainer_t384E08F63FABEFF6A7F1A753F5EF02699DAD414C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ConverterGroups_tA746073C90C188444FFC6A55278138FEE2BBDF0E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* EventBase_1_t2637736704939772A86A2904BCFD1B314097DD1E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* EventBase_1_t4941B5AE834AA028696C83756491CE8884F30DDE_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* EventBase_1_t7D3D212E8AD9C53105AAC51DBD4FA5F700586F68_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* EventBase_1_tB8AF17419B0E6B39E32F8925FCA535F5CF5C79D2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IndexOutOfRangeException_t7ECB35264FB6CA8FAA516BD958F4B2ADC78E8A82_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* StringBuilder_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* TypeConversion_tC95A24AF93E22908A5717FBF24E5089C963863E9_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Type_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral01A849374C26F36DBF4641EFCBB31ABDCEFF10D2;
IL2CPP_EXTERN_C String_t* _stringLiteral19185977762F1D370D03E76921A267F6A5FA9F81;
IL2CPP_EXTERN_C String_t* _stringLiteral2B6D6F48C27C60C3B55391AB377D9DC8F5639AA1;
IL2CPP_EXTERN_C String_t* _stringLiteral469F05BE9BB4C7903C353D0EB9F6384C84A48B25;
IL2CPP_EXTERN_C String_t* _stringLiteral49AE794C00022ECA141068DEA9531BF6E0D342B7;
IL2CPP_EXTERN_C String_t* _stringLiteral5BEFD8CC60A79699B5BB00E37BAC5B62D371E174;
IL2CPP_EXTERN_C String_t* _stringLiteral7A0A3608A3C4012D35E5DE3B2BA646379CCF198F;
IL2CPP_EXTERN_C String_t* _stringLiteral7EE837B2FC81E79F9F96BEFD9CD8B64870F5C628;
IL2CPP_EXTERN_C String_t* _stringLiteral8C80868ED807D30CD87E7BACE1DFC57B2C47C706;
IL2CPP_EXTERN_C String_t* _stringLiteralC0E02A0440A6BB4475B7E59901C37A6A25E773C8;
IL2CPP_EXTERN_C String_t* _stringLiteralD9691C4FD8A1F6B09DB1147CA32B442772FB46A1;
IL2CPP_EXTERN_C String_t* _stringLiteralDDFD5BA0EE5BD56B383E144892054DCB0A59D3ED;
IL2CPP_EXTERN_C String_t* _stringLiteralE166C9564FBDE461738077E3B1B506525EB6ACCC;
IL2CPP_EXTERN_C String_t* _stringLiteralECC4BC659FB1526108BFFD6AD5E7F03E47806740;
IL2CPP_EXTERN_C String_t* _stringLiteralFA7B055D415896E5E76FFEE1D010B524A9717B00;
IL2CPP_EXTERN_C const RuntimeMethod* EventBase_1_TypeId_m08396DED606ACD1093BEEA8D939E5DA37B797C12_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* EventBase_1_TypeId_m1A2720F11AB0D624809678ADE4E6D8C250E43741_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* EventBase_1_TypeId_m3FC722D85FBD1538CE6B96AD3364192799BF1CA6_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* EventBase_1_TypeId_mA90FE9E21D00125CFC53652D23DB65FD2574D60D_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Add_m084F52E7C436446A23080F15F803212491693F6B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Item_m97E4330A4B3A01D99AD2ACC1BAE011F8FD18C7BB_RuntimeMethod_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;
struct PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_com;
struct PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_pinvoke;

struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC;
struct ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct List_1_t584CB490C8F4C21E0A0D5545409ED60BF71F3FE4  : public RuntimeObject
{
	ComponentU5BU5D_t40ECDBC5CC15EA282AF49771C20EBFDADC532D0E* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_t9B68833848E4C4D7F623C05F6B77F0449396354A  : public RuntimeObject
{
	UInt32U5BU5D_t02FBD658AD156A17574ECE6106CF1FBFCC9807FA* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E  : public RuntimeObject
{
	__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A  : public RuntimeObject
{
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_tBDD12EAD3C5C46706730C230F223EE020C6822D6  : public RuntimeObject
{
	ButtonClickStatusU5BU5D_t9327A993382F70450196067EE5DED53834E9DB7B* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct ObjectPool_1_t28F249C599ED2EFD04F6B973FEFA2541B52062DF  : public RuntimeObject
{
	List_1_t0CA3CB19410DCFCBF6D92C5A74C84FC883A36CD8* ___m_List;
	Func_1_tE7A32FE928894E3B981CAD581AC7D8ECD5CC262A* ___m_CreateFunc;
	Action_1_tF18EC740BBE61E110C0C1270C9A2B66A1A72DBE1* ___m_ActionOnGet;
	Action_1_tF18EC740BBE61E110C0C1270C9A2B66A1A72DBE1* ___m_ActionOnRelease;
	Action_1_tF18EC740BBE61E110C0C1270C9A2B66A1A72DBE1* ___m_ActionOnDestroy;
	int32_t ___m_MaxSize;
	bool ___m_CollectionCheck;
	SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660* ___m_FreshlyReleased;
	int32_t ___U3CCountAllU3Ek__BackingField;
};
struct ObjectPool_1_tC6F0B8E0BAAEC07474AABA8496261838C79CE1F3  : public RuntimeObject
{
	List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* ___m_List;
	Func_1_t566D4A654882E282E54CDC3A001E05486F913903* ___m_CreateFunc;
	Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* ___m_ActionOnGet;
	Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* ___m_ActionOnRelease;
	Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* ___m_ActionOnDestroy;
	int32_t ___m_MaxSize;
	bool ___m_CollectionCheck;
	Il2CppSharedGenericObject* ___m_FreshlyReleased;
	int32_t ___U3CCountAllU3Ek__BackingField;
};
struct TypeId_1_tE871E3D998839394119381084FAB516D78C53198  : public RuntimeObject
{
};
struct Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA  : public RuntimeObject
{
};
struct Buffer_tF5DDA52AA39735125D710A126AC80BADEB900CD4  : public RuntimeObject
{
};
struct BurstCompiler_t6972AFDB97C8649095A1B7EE54AAADB676BE2C37  : public RuntimeObject
{
};
struct BurstRuntime_t934D9A0D258BC96E94A4B78AD085FA38BFEFFF82  : public RuntimeObject
{
};
struct CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4  : public RuntimeObject
{
	bool ___isIMGUIContainer;
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* ___m_CallbackRegistry;
};
struct ClickDetector_t6B5A82C99CFD12E051D8E84A7C8F7488355B8F31  : public RuntimeObject
{
	List_1_tBDD12EAD3C5C46706730C230F223EE020C6822D6* ___m_ClickStatus;
};
struct CollectionExtensions_t182A00BC503C9791010A3F71F412C76ABDD4D10D  : public RuntimeObject
{
};
struct CollectionExtensions_t9F552E0DA1B3C5AA3C6EF09DA633314D7A535D90  : public RuntimeObject
{
};
struct CollectionHelper_tFFE15A31BD8AE2BE545F4A412EC3C11352298482  : public RuntimeObject
{
};
struct CollectionMarshallingAccessorsAsserts_t4FEDE72BD20CFC6A500791EC58C2F350F69CAA1A  : public RuntimeObject
{
};
struct ComputedTransitionUtils_tC9191F301DCE1CB02C3FFC174E651E14204F1C7A  : public RuntimeObject
{
};
struct ConcreteTypeVisitor_tC41932325C8C25F9B634923360AE061521744BB2  : public RuntimeObject
{
};
struct ConstantBuffer_tB166ADD4FDA2C484CB3C1407D8129F97DF1BCC26  : public RuntimeObject
{
};
struct ConstantBufferBase_t20014DD79FCE2106360B8D0A97014A1B69B8CA8C  : public RuntimeObject
{
};
struct ContextContainer_t384E08F63FABEFF6A7F1A753F5EF02699DAD414C  : public RuntimeObject
{
	ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB* ___m_Items;
	List_1_t9B68833848E4C4D7F623C05F6B77F0449396354A* ___m_ActiveItemIndices;
};
struct ContextItem_tFC5ECB349F0410923AE6BC2A9CAACA3589B51086  : public RuntimeObject
{
};
struct Contract_tD9FCC48E522DA4471B12BD7298721B5814BF91A6  : public RuntimeObject
{
};
struct ConverterGroups_tA746073C90C188444FFC6A55278138FEE2BBDF0E  : public RuntimeObject
{
};
struct CoreUtils_tEE57AB2B0DAE9561F4954B08800A8F957850B038  : public RuntimeObject
{
};
struct CustomAttributeData_tC851BE158358D5CFB283E630148B10B7685DDC95  : public RuntimeObject
{
	ConstructorInfo_t1B5967EE7E5554272F79F8880183C70AD240EEEB* ___ctorInfo;
	RuntimeObject* ___ctorArgs;
	RuntimeObject* ___namedArgs;
	LazyCAttrData_t4F8036F83C070762609DB213B69F7FAA2D34ACA3* ___lazyData;
};
struct CustomAttributeExtensions_t8BE4B68F928B20AC1E02ABD129534DB9004FB0FA  : public RuntimeObject
{
};
struct EventCallbackPool_t337A1A0692B33BCB6B3D2C662666D78276F9CD3C  : public RuntimeObject
{
};
struct MemberInfo_t  : public RuntimeObject
{
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
};
struct StringBuilder_t  : public RuntimeObject
{
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___m_ChunkChars;
	StringBuilder_t* ___m_ChunkPrevious;
	int32_t ___m_ChunkLength;
	int32_t ___m_ChunkOffset;
	int32_t ___m_MaxCapacity;
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
struct ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D  : public ConstantBufferBase_t20014DD79FCE2106360B8D0A97014A1B69B8CA8C
{
	HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2* ___m_GlobalBindings;
	ComputeBuffer_t51EADA9015EBCC1B982C5584E9AB2734415A8233* ___m_GPUConstantBuffer;
};
struct HashCode64_1_t25B99E9F7F998CC07CCA13124E03920779BFF803 
{
	union
	{
		struct
		{
		};
		uint8_t HashCode64_1_t9A0E69580DAA2D2CCB4BA575308077FCCE5F2E6E__padding[1];
	};
};
typedef Il2CppFullySharedGenericStruct ValueTuple_2_t7064AD711EB2A320F9C65C116B07AF16D42C8CCB;
typedef Il2CppFullySharedGenericStruct ValueTuple_2_tBAA1E1D7D97D80E0EBA7FE8773B1FF409AEA3829;
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
};
struct Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3 
{
	uint8_t ___m_value;
};
struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17 
{
	Il2CppChar ___m_value;
};
struct ConversionRegistry_tBB92E13DAE967B31A341845C05EC88A2603E6426 
{
	Dictionary_2_tD77528C78AECDC8F6DF50F36B79092A52A270629* ___m_Converters;
	Dictionary_2_t7E2B528AFC59C90DE8916832B84A9936D8967195* ___m_LazyConverters;
};
struct ConversionRegistry_tBB92E13DAE967B31A341845C05EC88A2603E6426_marshaled_pinvoke
{
	Dictionary_2_tD77528C78AECDC8F6DF50F36B79092A52A270629* ___m_Converters;
	Dictionary_2_t7E2B528AFC59C90DE8916832B84A9936D8967195* ___m_LazyConverters;
};
struct ConversionRegistry_tBB92E13DAE967B31A341845C05EC88A2603E6426_marshaled_com
{
	Dictionary_2_tD77528C78AECDC8F6DF50F36B79092A52A270629* ___m_Converters;
	Dictionary_2_t7E2B528AFC59C90DE8916832B84A9936D8967195* ___m_LazyConverters;
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
struct Focusable_t39F2BAF0AF6CA465BC2BEDAF9B5B2CF379B846D0  : public CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4
{
	bool ___m_Focusable;
	int32_t ___m_TabIndex;
	bool ___m_DelegatesFocus;
	bool ___m_ExcludeFromFocusRing;
	bool ___U3CisEligibleToReceiveFocusFromDisabledChildU3Ek__BackingField;
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3 
{
	int64_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct PropertyName_tE4B4AAA58AF3BF2C0CD95509EB7B786F096901C2 
{
	int32_t ___id;
};
struct Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D 
{
	float ___m_XMin;
	float ___m_YMin;
	float ___m_Width;
	float ___m_Height;
};
struct StyleClassList_tCA2E8910D5BCCB7029CF852421F1E07E8EDF9744 
{
	Record_tD4D528E1BFF948E92C590DCE7A010BA356D50049* ___m_Record;
};
struct StyleClassList_tCA2E8910D5BCCB7029CF852421F1E07E8EDF9744_marshaled_pinvoke
{
	Record_tD4D528E1BFF948E92C590DCE7A010BA356D50049* ___m_Record;
};
struct StyleClassList_tCA2E8910D5BCCB7029CF852421F1E07E8EDF9744_marshaled_com
{
	Record_tD4D528E1BFF948E92C590DCE7A010BA356D50049* ___m_Record;
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
struct UnmanagedDataHandle_t5295F32E122AF2E09BF729381A22BD86B72C1DD1 
{
	int32_t ___Index;
	int32_t ___Version;
};
struct Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 
{
	float ___x;
	float ___y;
};
struct Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 
{
	float ___x;
	float ___y;
	float ___z;
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
struct AllocatorHandle_t3CA09720B1F89F91A8DDBA95E74C28A1EC3E3148 
{
	uint16_t ___Index;
	uint16_t ___Version;
};
struct Item_tFA847F868B1ECD768A489638A61F7BC398DECDAC 
{
	ContextItem_tFC5ECB349F0410923AE6BC2A9CAACA3589B51086* ___storage;
	bool ___isSet;
};
struct Item_tFA847F868B1ECD768A489638A61F7BC398DECDAC_marshaled_pinvoke
{
	ContextItem_tFC5ECB349F0410923AE6BC2A9CAACA3589B51086* ___storage;
	int32_t ___isSet;
};
struct Item_tFA847F868B1ECD768A489638A61F7BC398DECDAC_marshaled_com
{
	ContextItem_tFC5ECB349F0410923AE6BC2A9CAACA3589B51086* ___storage;
	int32_t ___isSet;
};
struct ArgValueList_tF9404610631615CB400FD008E21A0CE700A1814C 
{
	EventArgValue_t12B444AF86832453534803922BF5FCE39CFD0E85* ___m_FirstArg;
	int32_t ___m_PrevLocalArgId;
};
struct ArgValueList_tF9404610631615CB400FD008E21A0CE700A1814C_marshaled_pinvoke
{
	EventArgValue_t12B444AF86832453534803922BF5FCE39CFD0E85* ___m_FirstArg;
	int32_t ___m_PrevLocalArgId;
};
struct ArgValueList_tF9404610631615CB400FD008E21A0CE700A1814C_marshaled_com
{
	EventArgValue_t12B444AF86832453534803922BF5FCE39CFD0E85* ___m_FirstArg;
	int32_t ___m_PrevLocalArgId;
};
struct DynamicCallbackList_t7289C27A86DE4BA470602692C06ADD3BEC72BB6C 
{
	EventCallbackList_tC6B518C9C5192317B413145F9B9A3678EEE5BF1E* ___m_Callbacks;
	EventCallbackList_tC6B518C9C5192317B413145F9B9A3678EEE5BF1E* ___m_TemporaryCallbacks;
	int32_t ___m_IsInvoking;
};
struct DynamicCallbackList_t7289C27A86DE4BA470602692C06ADD3BEC72BB6C_marshaled_pinvoke
{
	EventCallbackList_tC6B518C9C5192317B413145F9B9A3678EEE5BF1E* ___m_Callbacks;
	EventCallbackList_tC6B518C9C5192317B413145F9B9A3678EEE5BF1E* ___m_TemporaryCallbacks;
	int32_t ___m_IsInvoking;
};
struct DynamicCallbackList_t7289C27A86DE4BA470602692C06ADD3BEC72BB6C_marshaled_com
{
	EventCallbackList_tC6B518C9C5192317B413145F9B9A3678EEE5BF1E* ___m_Callbacks;
	EventCallbackList_tC6B518C9C5192317B413145F9B9A3678EEE5BF1E* ___m_TemporaryCallbacks;
	int32_t ___m_IsInvoking;
};
struct Hierarchy_t4CF226F0EDE9C117C51C505730FC80641B1F1677 
{
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___m_Owner;
};
struct Hierarchy_t4CF226F0EDE9C117C51C505730FC80641B1F1677_marshaled_pinvoke
{
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___m_Owner;
};
struct Hierarchy_t4CF226F0EDE9C117C51C505730FC80641B1F1677_marshaled_com
{
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___m_Owner;
};
struct ByReference_1_t21C88CEA3607E6DA2435F0E317C10A776BCA6DCC 
{
	intptr_t ____value;
};
struct ByReference_1_t607C1F3BC28B0E21B969461CDB0720FB01A82141 
{
	intptr_t ____value;
};
struct ConstantBufferSingleton_1_t800A1489DAFDDB639BB1BC6406F901FBDE4F0F69  : public ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D
{
};
struct FunctionPointer_1_tFA7B05CB8F034C67CD0B63063DD837ACC1C06AE9 
{
	intptr_t ____ptr;
};
struct FunctionPointer_1_t8F9F55036C9757966AA570D1DB1FADC59D4C7547 
{
	intptr_t ____ptr;
};
struct Allocator_t996642592271AAD9EE688F142741D512C07B5824 
{
	int32_t ___value__;
};
struct BindingMode_t14EA605BB56B087ECA15C8BF7826E98F12D30E61 
{
	int32_t ___value__;
};
struct BindingUpdateTrigger_t5237F1E29F5B5604BB57907C83EE6B1CFA261234 
{
	int32_t ___value__;
};
struct CallbackOptions_tC7358FBA59966769E45650904E9EF98376786515 
{
	int32_t ___value__;
};
struct CallbackOptionsInternal_tB4D74C7912C8FB52C02F0AF2485F1E74FF693851 
{
	int32_t ___value__;
};
struct CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7  : public RuntimeObject
{
	intptr_t ___m_Ptr;
};
struct ComputeBuffer_t51EADA9015EBCC1B982C5584E9AB2734415A8233  : public RuntimeObject
{
	intptr_t ___m_Ptr;
};
struct ConverterGroup_t2D7DEDAC22662EF5C020DE54231DC23CD0B5268A  : public RuntimeObject
{
	String_t* ___U3CidU3Ek__BackingField;
	String_t* ___U3CdisplayNameU3Ek__BackingField;
	String_t* ___U3CdescriptionU3Ek__BackingField;
	ConversionRegistry_tBB92E13DAE967B31A341845C05EC88A2603E6426 ___m_Registry;
};
struct CullingResults_tD6B7EF20B68D47DFF3A99EB2EA73F47F1D460267 
{
	intptr_t ___ptr;
	CullingAllocationInfo_tB260F5CD0B290F74E145EB16E54B901CC68D9D5A* ___m_AllocationInfo;
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
struct EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85  : public RuntimeObject
{
	ArgValueList_tF9404610631615CB400FD008E21A0CE700A1814C ___m_ArgValues;
	DynamicCallbackList_t7289C27A86DE4BA470602692C06ADD3BEC72BB6C ___m_TrickleDownCallbacks;
	DynamicCallbackList_t7289C27A86DE4BA470602692C06ADD3BEC72BB6C ___m_BubbleUpCallbacks;
	bool ___m_HasLocalCallbacks;
};
struct EventCategory_tCFC347F164A2525B4C39DA6A9B7A9B5A541E3FFA 
{
	int32_t ___value__;
};
struct EventModifiers_tB9DEEF73F8FC4C312B8E0C34336C7D9C3023B270 
{
	int32_t ___value__;
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
struct LanguageDirection_t30A3B6BBCEE6A6F57641E4E008E0DCC40603558C 
{
	int32_t ___value__;
};
struct NativeArrayOptions_t3E979EEF4B4840228A7692A97DA07553C6465F1D 
{
	int32_t ___value__;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr;
};
struct PenStatus_tCAD6543115EF443E17410B52D37EC67BCC88ABB8 
{
	int32_t ___value__;
};
struct ProfilerMarker_tA256E18DA86EDBC5528CE066FC91C96EE86501AD 
{
	intptr_t ___m_Ptr;
};
struct PropagationPhase_tF3BE8BF5ED45FC52A828B7B6F078B64F01FAE6D6 
{
	int32_t ___value__;
};
struct PropertyPathPartKind_t82152825D88A0E450DDCE8503272A10595047F87 
{
	int32_t ___value__;
};
struct Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Origin;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Direction;
};
struct RenderHints_t4032FC4AB3FD946FD2A484865B8861730D9035E7 
{
	int32_t ___value__;
};
struct RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B 
{
	intptr_t ___value;
};
struct TrickleDown_t862EABE449B6C94F79F2B1D713990C35ABADDF38 
{
	int32_t ___value__;
};
struct VisitExceptionKind_t7E6C3EABD5439F2B4CA31C246B2AA4AC16A77DF5 
{
	int32_t ___value__;
};
struct VisitReturnCode_t795FEB928E847C526F470080AA94B33FAC60BC75 
{
	int32_t ___value__;
};
struct VisualElementFlags_t4D1066E11400967A1A2DA7331391ACDC4AA14409 
{
	int32_t ___value__;
};
struct ButtonClickStatus_tD4248512DB59E16501C4DDB6EDC2C6DE04290470  : public RuntimeObject
{
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___m_Target;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_PointerDownPosition;
	int64_t ___m_LastPointerDownTime;
	int32_t ___m_ClickCount;
};
struct EventPropagation_t024AF56F7A787C03AA21B065B624553EF52E7B83 
{
	int32_t ___value__;
};
struct LifeCycleStatus_tEE500629F5431B574B8047EB70864747D348D38C 
{
	int32_t ___value__;
};
struct NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct Nullable_1_t4963285281B990B794CDEACB9C3CD3069B5FA768 
{
	bool ___hasValue;
	Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 ___value;
};
struct ReadOnlySpan_1_tE8C37D9A05FCAB953169AFFE8A0ABCA809781E25 
{
	ByReference_1_t21C88CEA3607E6DA2435F0E317C10A776BCA6DCC ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC 
{
	ByReference_1_t607C1F3BC28B0E21B969461CDB0720FB01A82141 ____pointer;
	int32_t ____length;
};
struct Binding_tA1358A155852138C1926082E4F3245B6F631CBED  : public RuntimeObject
{
	bool ___m_Dirty;
	int32_t ___m_UpdateTrigger;
};
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C  : public RuntimeObject
{
	int32_t ___U3CeventCategoriesU3Ek__BackingField;
	int64_t ___U3CtimestampU3Ek__BackingField;
	uint64_t ___U3CeventIdU3Ek__BackingField;
	uint64_t ___U3CtriggerEventIdU3Ek__BackingField;
	int32_t ___U3CpropagationU3Ek__BackingField;
	int32_t ___U3ClifeCycleStatusU3Ek__BackingField;
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___U3CelementTargetU3Ek__BackingField;
	int32_t ___U3CpropagationPhaseU3Ek__BackingField;
	RuntimeObject* ___m_CurrentTarget;
	Event_tEBC6F24B56CE22B9C9AD1AC6C24A6B83BC3860CB* ___m_ImguiEvent;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___U3CoriginalMousePositionU3Ek__BackingField;
};
struct EventCallbackInternal_t74403797F0007ACB72019EE51D2C4DC33E5F63AF  : public RuntimeObject
{
	TypeData_t9C5DC5F5DD8F0C04131F359229ED4738117BA396* ___U3CeventDataU3Ek__BackingField;
	Delegate_t* ___U3CuserCallbackU3Ek__BackingField;
	int32_t ___U3CcallbackOptionsU3Ek__BackingField;
	int32_t ___U3CargIdU3Ek__BackingField;
};
struct GameObject_t76FEDD663AB33C991A9C9A23129337651094216F  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct MemoryLabel_t29CE7AB312D2ED888B444BBE5D452F7132EC9DB2 
{
	union
	{
		struct
		{
			intptr_t ___pointer;
			int32_t ___allocator;
		};
		uint8_t MemoryLabel_t29CE7AB312D2ED888B444BBE5D452F7132EC9DB2__padding[16];
	};
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
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};
struct Type_t  : public MemberInfo_t
{
	RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ____impl;
};
struct VisitParameters_tF21AC3343F90A7925EEA69AA73269B7F2DCE66CE 
{
	int32_t ___U3CIgnoreExceptionsU3Ek__BackingField;
};
struct Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505  : public MulticastDelegate_t
{
};
struct EventBase_1_t880C2458D345D7530A8295FA56BDDF14D326DE8F  : public EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C
{
	int32_t ___m_RefCount;
};
struct EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482  : public MulticastDelegate_t
{
};
struct EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE  : public MulticastDelegate_t
{
};
struct Func_1_t566D4A654882E282E54CDC3A001E05486F913903  : public MulticastDelegate_t
{
};
struct Func_2_t3793651739D8B94095DFFF75C3BD348E5E2C455C  : public MulticastDelegate_t
{
};
struct Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107  : public MulticastDelegate_t
{
};
struct TypeConverter_2_tA6276E0BDB8DB9E3B10AF0C31301ACC071589845  : public MulticastDelegate_t
{
};
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
	String_t* ____paramName;
};
struct IndexOutOfRangeException_t7ECB35264FB6CA8FAA516BD958F4B2ADC78E8A82  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
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
struct UnmanagedDataStore_t66CFDF2DBB3C86F8A58F5B3EBEE5E9537BDF2759 
{
	MemoryLabel_t29CE7AB312D2ED888B444BBE5D452F7132EC9DB2 ___m_MemoryLabel;
	Data_t6BD087CC0FA9794D342D260035A70E365224C66E* ___m_Data;
};
struct PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816  : public EventBase_1_t880C2458D345D7530A8295FA56BDDF14D326DE8F
{
	bool ___m_AltitudeNeedsConversion;
	bool ___m_AzimuthNeedsConversion;
	float ___m_AltitudeAngle;
	float ___m_AzimuthAngle;
	bool ___m_TiltNeeded;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___m_Tilt;
	int32_t ___U3CpointerIdU3Ek__BackingField;
	String_t* ___U3CpointerTypeU3Ek__BackingField;
	bool ___U3CisPrimaryU3Ek__BackingField;
	int32_t ___U3CbuttonU3Ek__BackingField;
	int32_t ___U3CpressedButtonsU3Ek__BackingField;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___U3CpositionU3Ek__BackingField;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___U3ClocalPositionU3Ek__BackingField;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___U3CdeltaPositionU3Ek__BackingField;
	float ___U3CdeltaTimeU3Ek__BackingField;
	int32_t ___U3CclickCountU3Ek__BackingField;
	float ___U3CpressureU3Ek__BackingField;
	float ___U3CtangentialPressureU3Ek__BackingField;
	float ___U3CtwistU3Ek__BackingField;
	int32_t ___U3CpenStatusU3Ek__BackingField;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___U3CradiusU3Ek__BackingField;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___U3CradiusVarianceU3Ek__BackingField;
	int32_t ___U3CmodifiersU3Ek__BackingField;
	RuntimeObject* ___U3CcompatibilityMouseEventU3Ek__BackingField;
	int32_t ___U3CdisplayIndexU3Ek__BackingField;
	bool ___U3CrecomputeTopElementUnderPointerU3Ek__BackingField;
	Nullable_1_t4963285281B990B794CDEACB9C3CD3069B5FA768 ___U3CpanelRayU3Ek__BackingField;
};
struct ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129  : public ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263
{
};
struct ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F  : public ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263
{
	RuntimeObject* ____actualValue;
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
struct DataBinding_t4AEDF779EB49F9E11B45F99385482936B2FC34DA  : public Binding_tA1358A155852138C1926082E4F3245B6F631CBED
{
	int32_t ___m_BindingMode;
	ConverterGroup_t2D7DEDAC22662EF5C020DE54231DC23CD0B5268A* ___m_SourceToUiConverters;
	ConverterGroup_t2D7DEDAC22662EF5C020DE54231DC23CD0B5268A* ___m_UiToSourceConverters;
	RuntimeObject* ___U3CdataSourceU3Ek__BackingField;
	Type_t* ___U3CdataSourceTypeU3Ek__BackingField;
	PropertyPath_tA523CA2740853534DF6C009C588464B45A6D0A79 ___U3CdataSourcePathU3Ek__BackingField;
};
struct LayoutDataAccess_t99AA56349D0BC76F3742B927F4F0DDB21511FBBA 
{
	int32_t ___m_Manager;
	UnmanagedDataStore_t66CFDF2DBB3C86F8A58F5B3EBEE5E9537BDF2759 ___m_Nodes;
	UnmanagedDataStore_t66CFDF2DBB3C86F8A58F5B3EBEE5E9537BDF2759 ___m_Configs;
};
struct PathVisitor_tCD0947C41CDBDB774D22D560E5FFA01EF61C4446  : public RuntimeObject
{
	int32_t ___m_PathIndex;
	PropertyPath_tA523CA2740853534DF6C009C588464B45A6D0A79 ___U3CPathU3Ek__BackingField;
	RuntimeObject* ___U3CPropertyU3Ek__BackingField;
	bool ___U3CReadonlyVisitU3Ek__BackingField;
	int32_t ___U3CReturnCodeU3Ek__BackingField;
};
struct SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660 : public PathVisitor_tCD0947C41CDBDB774D22D560E5FFA01EF61C4446 {};
struct LayoutNode_tADF081B0F16F76B66459DE38F3AD8EC098F22CBE 
{
	LayoutDataAccess_t99AA56349D0BC76F3742B927F4F0DDB21511FBBA ___m_Access;
	UnmanagedDataHandle_t5295F32E122AF2E09BF729381A22BD86B72C1DD1 ___m_Handle;
};
struct VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115  : public Focusable_t39F2BAF0AF6CA465BC2BEDAF9B5B2CF379B846D0
{
	int32_t ___U3CUnityEngine_UIElements_IStylePropertyAnimations_runningAnimationCountU3Ek__BackingField;
	int32_t ___U3CUnityEngine_UIElements_IStylePropertyAnimations_completedAnimationCountU3Ek__BackingField;
	String_t* ___m_Name;
	StyleClassList_tCA2E8910D5BCCB7029CF852421F1E07E8EDF9744 ___m_ClassList;
	Dictionary_2_tBCCCFBCAC02A3C03E3C84D75696D4860D7444A35* ___m_PropertyBag;
	int32_t ___m_Flags;
	int32_t ___m_RenderHints;
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___lastLayout;
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___lastPseudoPadding;
	RenderData_t1ABE116B2B5E0409AC699E195922516606531DC2* ___renderData;
	RenderData_t1ABE116B2B5E0409AC699E195922516606531DC2* ___nestedRenderData;
	int32_t ___insertionIndex;
	int32_t ___U3CcontainedPointerIdsU3Ek__BackingField;
	String_t* ___m_ViewDataKey;
	int32_t ___m_LanguageDirection;
	LayoutNode_tADF081B0F16F76B66459DE38F3AD8EC098F22CBE ___m_LayoutNode;
	VisualElementTransformData_t3DD575B5990B68FF956673EFF036171C86A38DF3* ___m_TransformDataPTr;
	VisualElementSelectorData_t4C4043092A19A653CE8637C7659E36E0D3CC23A0* ___m_SelectorDataPtr;
	StyleVariableContext_tF74F2787CE1F6BEBBFBFF0771CF493AC9E403527* ___variableContext;
	int32_t ___inheritedStylesHash;
	uint32_t ___controlid;
	int32_t ___imguiContainerDescendantCount;
	int32_t ___m_LocalLanguageDirection;
	Action_1_t3DC3411926243F1DB9C330F8E105B904E38C1A0B* ___U3CgenerateVisualContentU3Ek__BackingField;
	List_1_t96E9133B70FB6765E6B138E810D33E18901715DA* ___m_RunningAnimations;
	RuntimeObject* ___m_DataSource;
	PathRef_t76F7677792A14AF9D6A6EAB7F08C1A3DC2B27A55* ___m_DataSourcePath;
	int32_t ___m_TrickleDownHandleEventCategories;
	int32_t ___m_BubbleUpHandleEventCategories;
	int32_t ___m_BubbleUpEventCallbackCategories;
	int32_t ___m_TrickleDownEventCallbackCategories;
	int32_t ___m_EventInterestSelfCategories;
	int32_t ___m_CachedEventInterestParentCategories;
	uint32_t ___m_NextParentCachedVersion;
	uint32_t ___m_NextParentRequiredVersion;
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___m_CachedNextParentWithEventInterests;
	Hierarchy_t4CF226F0EDE9C117C51C505730FC80641B1F1677 ___U3ChierarchyU3Ek__BackingField;
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___m_PhysicalParent;
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___m_LogicalParent;
	List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95* ___m_Children;
	BaseVisualElementPanel_tE3811F3D1474B72CB6CD5BCEECFF5B5CBEC1E303* ___U3CelementPanelU3Ek__BackingField;
	VisualTreeAsset_tFB5BF81F0780A412AE5A7C2C552B3EEA64EA2EEB* ___m_VisualTreeAssetSource;
	List_1_tB1D3AE6638063900DFF2ED463335EF8912FBBF7B* ___m_MeshModifiers;
	InlineStyleAccess_t5CA7877999C9442491A220AE50D605C84D09A165* ___inlineStyleAccess;
	ResolvedStyleAccess_t226CC840EBACEE31CE1139ED5F717532AFFAEB45* ___resolvedStyleAccess;
	List_1_tEA16F82F7871418E28EB6F551D77A8AD9F2E337F* ___styleSheetList;
	TypeData_t01D670B4E71B5571B38C7412B1E652A47D6AF66A* ___m_TypeData;
};
struct List_1_t584CB490C8F4C21E0A0D5545409ED60BF71F3FE4_StaticFields
{
	ComponentU5BU5D_t40ECDBC5CC15EA282AF49771C20EBFDADC532D0E* ___s_emptyArray;
};
struct List_1_t9B68833848E4C4D7F623C05F6B77F0449396354A_StaticFields
{
	UInt32U5BU5D_t02FBD658AD156A17574ECE6106CF1FBFCC9807FA* ___s_emptyArray;
};
struct List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E_StaticFields
{
	__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ___s_emptyArray;
};
struct List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A_StaticFields
{
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___s_emptyArray;
};
struct List_1_tBDD12EAD3C5C46706730C230F223EE020C6822D6_StaticFields
{
	ButtonClickStatusU5BU5D_t9327A993382F70450196067EE5DED53834E9DB7B* ___s_emptyArray;
};
struct TypeId_1_tE871E3D998839394119381084FAB516D78C53198_StaticFields
{
	uint32_t ___value;
};
struct BurstCompiler_t6972AFDB97C8649095A1B7EE54AAADB676BE2C37_StaticFields
{
	bool ____IsEnabled;
	BurstCompilerOptions_t1AFF6FD4375BCFD72E3B1C4ABF5AA4888C3DF05D* ___Options;
	MethodInfo_t* ___DummyMethodInfo;
};
struct ClickDetector_t6B5A82C99CFD12E051D8E84A7C8F7488355B8F31_StaticFields
{
	int32_t ___U3Cs_DoubleClickTimeU3Ek__BackingField;
};
struct ComputedTransitionUtils_tC9191F301DCE1CB02C3FFC174E651E14204F1C7A_StaticFields
{
	ComputedTransitionPropertyU5BU5D_t25B9E78F5276CDA297C8215C316452CAB8219E82* ___k_EmptyTransitions;
	List_1_t888EAF80C1646E9D51193F9F6E183344845FB66C* ___s_ComputedTransitionsBuffer;
};
struct ConstantBuffer_tB166ADD4FDA2C484CB3C1407D8129F97DF1BCC26_StaticFields
{
	List_1_t763DE56D8B401C117651B07CA70E79E840BD9B3F* ___m_RegisteredConstantBuffers;
};
struct ContextContainer_t384E08F63FABEFF6A7F1A753F5EF02699DAD414C_StaticFields
{
	uint32_t ___s_TypeCount;
};
struct ConverterGroups_tA746073C90C188444FFC6A55278138FEE2BBDF0E_StaticFields
{
	ConverterGroup_t2D7DEDAC22662EF5C020DE54231DC23CD0B5268A* ___s_GlobalConverters;
	Dictionary_2_t4DF54DED090D74EA3A4143562494256C758AB504* ___s_BindingConverterGroups;
};
struct CoreUtils_tEE57AB2B0DAE9561F4954B08800A8F957850B038_StaticFields
{
	Vector3U5BU5D_tFF1859CCE176131B909E2044F76443064254679C* ___lookAtList;
	Vector3U5BU5D_tFF1859CCE176131B909E2044F76443064254679C* ___upVectorList;
	Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1* ___m_BlackVolumeTexture;
	Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1* ___m_WhiteVolumeTexture;
};
struct EventCallbackPool_t337A1A0692B33BCB6B3D2C662666D78276F9CD3C_StaticFields
{
	ObjectPool_1_tE788CDA6BB77BAACFA8AF7030ABD8929A4C0989C* ___s_Pool;
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct HashCode64_1_t25B99E9F7F998CC07CCA13124E03920779BFF803_StaticFields
{
	int64_t ___Value;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17_StaticFields
{
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___s_categoryForLatin1;
};
struct IntPtr_t_StaticFields
{
	intptr_t ___Zero;
};
struct ConstantBufferSingleton_1_t800A1489DAFDDB639BB1BC6406F901FBDE4F0F69_StaticFields
{
	ConstantBufferSingleton_1_t800A1489DAFDDB639BB1BC6406F901FBDE4F0F69* ___s_Instance;
};
struct CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7_StaticFields
{
	bool ___ThrowOnSetRenderTarget;
};
struct EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_StaticFields
{
	EventCallbackListPool_t258FFD0B864AC29862A6ACF4F56A0340621783B2* ___k_ListPool;
	EventCallbackPool_t337A1A0692B33BCB6B3D2C662666D78276F9CD3C* ___k_LocalCallbackPool;
	List_1_tAF3FEA7536A43A4D9943A753A42160EEFC935955* ___k_UnregisteredLocalCallbacksDuringInvoke;
	int32_t ___s_GlobalInvokeCount;
	ObjectPool_1_t5CBB55FD447658CA0C9C56C5AB2D197BCBDF6C1F* ___k_RegistryPool;
};
struct EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C_StaticFields
{
	int64_t ___s_LastTypeId;
	uint64_t ___s_NextEventId;
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
struct EventBase_1_t880C2458D345D7530A8295FA56BDDF14D326DE8F_StaticFields
{
	int64_t ___s_TypeId;
	ObjectPool_1_t9CFCA41CD780F7424037FA2A3EE846F2FBFEE4D0* ___s_Pool;
	int32_t ___EventCategory;
	TypeData_t9C5DC5F5DD8F0C04131F359229ED4738117BA396* ___k_TypeData;
};
struct DataBinding_t4AEDF779EB49F9E11B45F99385482936B2FC34DA_StaticFields
{
	MethodInfo_t* ___s_UpdateUIMethodInfo;
	UniqueStyleString_tE980AA033ABB00413A3139B42FF22750952DAECF ___textElementInnerInputFieldUssClassNameUnique;
};
struct SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660_StaticFields
{
	ObjectPool_1_t28F249C599ED2EFD04F6B973FEFA2541B52062DF* ___Pool;
};
struct VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115_StaticFields
{
	uint32_t ___s_NextId;
	PropertyName_tE4B4AAA58AF3BF2C0CD95509EB7B786F096901C2 ___userDataPropertyKey;
	String_t* ___disabledUssClassName;
	UniqueStyleString_tE980AA033ABB00413A3139B42FF22750952DAECF ___disabledUssClassNameUnique;
	PropertyName_tE4B4AAA58AF3BF2C0CD95509EB7B786F096901C2 ___tooltipPropertyKey;
	int32_t ___s_FinalizerCount;
	ProfilerMarker_tA256E18DA86EDBC5528CE066FC91C96EE86501AD ___k_GenerateVisualContentMarker;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___childCountProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___contentRectProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___dataSourcePathProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___dataSourceProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___disablePlayModeTintProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___enabledInHierarchyProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___enabledSelfProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___layoutProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___languageDirectionProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___localBoundProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___nameProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___panelProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___pickingModeProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___styleSheetsProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___tooltipProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___usageHintsProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___userDataProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___viewDataKeyProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___visibleProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___visualTreeAssetSourceProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___worldBoundProperty;
	BindingId_t8BBB6188CD126EACCA07816C78760E92DC16620E ___worldTransformProperty;
	uint32_t ___s_NextParentVersion;
	List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95* ___s_EmptyList;
	Regex_tE773142C2BE45C5D362B0F815AFF831707A51772* ___s_InternalStyleSheetPath;
	Dictionary_2_t4055F6540F36F21F9FEDAFB92D8E0089B38EBBC8* ___s_TypeData;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
struct __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC  : public RuntimeArray
{
	ALIGN_FIELD (8) uint8_t m_Items[1];

	inline uint8_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + il2cpp_array_calc_byte_offset(this, index);
	}
	inline uint8_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + il2cpp_array_calc_byte_offset(this, index);
	}
};
struct ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB  : public RuntimeArray
{
	ALIGN_FIELD (8) Item_tFA847F868B1ECD768A489638A61F7BC398DECDAC m_Items[1];

	inline Item_tFA847F868B1ECD768A489638A61F7BC398DECDAC GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Item_tFA847F868B1ECD768A489638A61F7BC398DECDAC* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Item_tFA847F868B1ECD768A489638A61F7BC398DECDAC value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->___storage), (void*)NULL);
	}
	inline Item_tFA847F868B1ECD768A489638A61F7BC398DECDAC GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Item_tFA847F868B1ECD768A489638A61F7BC398DECDAC* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Item_tFA847F868B1ECD768A489638A61F7BC398DECDAC value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->___storage), (void*)NULL);
	}
};
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918  : public RuntimeArray
{
	ALIGN_FIELD (8) RuntimeObject* m_Items[1];

	inline RuntimeObject* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, RuntimeObject* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline RuntimeObject* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, RuntimeObject* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};


IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Unsafe_SizeOf_TisIl2CppFullySharedGenericAny_mD5748385042811E049C06FFD865AFD213C5FA9EB_fshared (const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void FunctionPointer_1__ctor_m539FE4A638EAB194C030CB25B52DBB1BADEA6E5A_fshared_inline (FunctionPointer_1_t8F9F55036C9757966AA570D1DB1FADC59D4C7547* __this, intptr_t ___0_ptr, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CallbackEventHandler_AddEventCategories_TisIl2CppSharedGenericObject_m48C6249122107B0206C2479BEA30DDCDEB81D426_gshared (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* __this, int32_t ___0_callbackOptions, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void CallbackEventHandler_AddListenersAndCategories_TisIl2CppSharedGenericObject_m7ACB30B716F504416F9FB1910C2CAF60F02E627F_gshared_inline (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* __this, Delegate_t* ___0_callback, int32_t ___1_callbackOptions, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void EventCallbackRegistry_RegisterCallback_TisIl2CppSharedGenericObject_m88FAF5D5C2F885FF0B1926FB47DD948259F8029B_gshared_inline (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* __this, EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* ___0_userCallback, int32_t ___1_callbackOptions, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void EventCallbackRegistry_RegisterCallback_TisIl2CppSharedGenericObject_TisIl2CppFullySharedGenericAny_m4C03FF9793B46745C6FCB27E7C02C6816E3EFB1A_fshared_inline (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* __this, EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE* ___0_userCallback, CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* ___1_element, Il2CppFullySharedGenericAny ___2_userArgs, int32_t ___3_callbackOptions, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void CallbackEventHandler_RemoveListeners_TisIl2CppSharedGenericObject_m44964C63A8123B46E7D698F80D9205E7387A53B3_gshared_inline (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* __this, Delegate_t* ___0_callback, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool EventCallbackRegistry_UnregisterCallback_TisIl2CppSharedGenericObject_m83DD8A054C7A025857863C137916EC36CC066AA6_gshared_inline (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* __this, EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* ___0_callback, int32_t ___1_callbackOptions, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool EventCallbackRegistry_UnregisterCallback_TisIl2CppSharedGenericObject_TisIl2CppFullySharedGenericAny_mA1B34BFD9C5EE81778ED146A48FA639F9CE18948_fshared_inline (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* __this, EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE* ___0_callback, int32_t ___1_callbackOptions, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int64_t EventBase_1_TypeId_m5B74C35CC6F5F2EC559BDD7A71544FD6FDE37CDE_gshared_inline (const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t PointerEventBase_1_get_button_mE854A473F8446E8E956AB6229868A8642BE30D10_gshared_inline (PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t PointerEventBase_1_get_pressedButtons_m4A6DBB316294BB3899BA348BCD895B7C69F7D1D9_gshared_inline (PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t PointerEventBase_1_get_pointerId_m96350DDBEB7BE4407122043EEF6826D54520E58A_gshared_inline (PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_get_Item_m6E4BA37C1FB558E4A62AE4324212E45D09C5C937_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_index, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Resize_TisIl2CppFullySharedGenericAny_m263FC41C8DB989397C43C86556D63CEBE13F4712_fshared (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** ___0_array, int32_t ___1_newSize, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Func_2_Invoke_m31CAC166FDC80DC5AE52A5AEFFEE2D9B27A1CA3F_fshared (Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* __this, Il2CppFullySharedGenericAny ___0_arg, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeArray_1__ctor_m788DE0F85C4051DDF092DDF96484DE655ACFB6F1_fshared (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18* __this, int32_t ___0_length, int32_t ___1_allocator, int32_t ___2_options, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeArrayExtensions_Initialize_TisIl2CppFullySharedGenericStruct_m10ABF0E8955AE4504761E521587E2D89638A1972_fshared (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18* ___0_array, int32_t ___1_length, AllocatorHandle_t3CA09720B1F89F91A8DDBA95E74C28A1EC3E3148 ___2_allocator, int32_t ___3_options, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void* NativeArrayUnsafeUtility_GetUnsafeReadOnlyPtr_TisIl2CppFullySharedGenericStruct_m13C618FD69BBAB7D8C77632BF8A9116FCD17D234_fshared_inline (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 ___0_nativeArray, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t NativeArray_1_get_Length_mBE5CC8B844994CFC4AB434235F915881575E63C8_fshared_inline (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t UnsafeUtility_SizeOf_TisIl2CppFullySharedGenericStruct_mB6FBF9DDC79FEC02EC8705E9E527E1CD495564BF_fshared_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ValueTuple_2__ctor_mCAE8E725F680FA6BE2C23B9686C9F6056BB7E5CD_fshared (ValueTuple_2_tBAA1E1D7D97D80E0EBA7FE8773B1FF409AEA3829* __this, Il2CppFullySharedGenericAny ___0_item1, Il2CppFullySharedGenericAny ___1_item2, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Il2CppFullySharedGenericAny* UnsafeUtility_As_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_mD6BECC32C10552E82C8D23115CAF774983D95F0C_fshared_inline (Il2CppFullySharedGenericAny* ___0_from, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* GameObject_GetComponents_TisIl2CppFullySharedGenericAny_mE91E75D8E2C639EF49A5401C23EEFF029D54EDF0_fshared (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* UnsafeUtility_As_TisIl2CppSharedGenericObject_m7E4A82367DE96668D1A49E6688B2ABFB2F09A6AF_gshared (RuntimeObject* ___0_from, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* Component_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mB27BBCE995DC86EC5D047AE9D0804238DA9B4E28_fshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, bool ___0_includeInactive, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* GameObject_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mE619FF82D1099A3B6ADAD0FF12F3A05E06C32C24_fshared (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, bool ___0_includeInactive, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Component_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mB3B314FA2B0FB49AB1B2E190A9FBC8337638078B_fshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, bool ___0_includeInactive, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* ___1_result, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GameObject_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mD80CAA2E88B01D4173A65A4C4C20DCCF713ADF62_fshared (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, bool ___0_includeInactive, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* ___1_results, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* Component_GetComponentsInParent_TisIl2CppFullySharedGenericAny_m66D0CCEEF66E1B1598B57995FD87FC3CEEE1CA5C_fshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, bool ___0_includeInactive, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* GameObject_GetComponentsInParent_TisIl2CppFullySharedGenericAny_m9BA598F4C76BF91ED59BD5DA257501EE1E9F6FBF_fshared (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, bool ___0_includeInactive, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GameObject_GetComponentsInParent_TisIl2CppFullySharedGenericAny_mD31B5889D11FC1B81FB10F6C35A2A6683ACB8921_fshared (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, bool ___0_includeInactive, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* ___1_results, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool GameObject_TryGetComponent_TisIl2CppFullySharedGenericAny_m8FE39C82005602F4FEA27DC201600DABE2481BD1_fshared (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, Il2CppFullySharedGenericAny* ___0_component, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_get_Length_m4430960CA0D0458B1A1106DD246CA9AB746B5DB2_fshared_inline (ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Il2CppFullySharedGenericAny* ReadOnlySpan_1_get_Item_m9143C9CF6493AF0AD667C5BDEEF1D22895283F77_fshared_inline (ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ConstantBufferSingleton_1_t800A1489DAFDDB639BB1BC6406F901FBDE4F0F69* ConstantBufferSingleton_1_get_instance_m9BCF8F5E44B966B85584D73034BF4E880D86DF2E_fshared (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConstantBuffer_1_UpdateData_m2B6875F2248E344B4C53051FAA9355CA582A8830_fshared (ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D* __this, CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* ___0_cmd, Il2CppFullySharedGenericStruct* ___1_data, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConstantBuffer_1_Set_mE00DA8AF81B1D6E70D952A4A92D6F211DCE9FC52_fshared (ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D* __this, CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* ___0_cmd, ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___1_cs, int32_t ___2_shaderId, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConstantBuffer_1_SetGlobal_mFD13E51675FB9005C153098E16D6B047A4070BA2_fshared (ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D* __this, CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* ___0_cmd, int32_t ___1_shaderId, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConstantBuffer_1_Set_m95B690077F35AFDA92F058EE5E32BE2911264A28_fshared (ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D* __this, ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___0_cs, int32_t ___1_shaderId, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* ContextContainer_CreateAndGetData_TisIl2CppSharedGenericObject_m669524DBBE31F1D7ECBD4D5F583CE48EE0AD938C_gshared (ContextContainer_t384E08F63FABEFF6A7F1A753F5EF02699DAD414C* __this, uint32_t ___0_typeId, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mD4F3498FBD3BDD3F03CBCFB38041CBAC9C28CAFC_fshared_inline (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Il2CppFullySharedGenericAny ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void Activator_CreateInstance_TisIl2CppFullySharedGenericAny_m68F3B5F53DBB1741E028DA541BE07A3D2382138D_fshared (Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_m6F76B5644E2D6BD882F23B65434545AD90EFE5A1_fshared (Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* __this, Il2CppFullySharedGenericAny ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TypeConverter_2_Invoke_m22213A88AF14CB4CFBD0CCA1547E1476B01580F9_fshared (TypeConverter_2_tA6276E0BDB8DB9E3B10AF0C31301ACC071589845* __this, Il2CppFullySharedGenericAny* ___0_value, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* ObjectPool_1_Get_m6033387D88EF9D1AD81A89F3045733BC4B98073D_gshared_inline (ObjectPool_1_tC6F0B8E0BAAEC07474AABA8496261838C79CE1F3* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SetValueVisitor_1_set_group_m94980C75D99986FB92C00A15DA3C9C1E7DF4F175_fshared_inline (SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660* __this, ConverterGroup_t2D7DEDAC22662EF5C020DE54231DC23CD0B5268A* ___0_value, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ObjectPool_1_Release_m3183E8F3194884123A027D720D37F341AF367E48_gshared_inline (ObjectPool_1_tC6F0B8E0BAAEC07474AABA8496261838C79CE1F3* __this, Il2CppSharedGenericObject* ___0_element, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool PropertyContainer_TryAccept_TisIl2CppFullySharedGenericAny_mDD1DF6BB0301B9008B88A30F25AC64BE2728F0D6_fshared (RuntimeObject* ___0_visitor, Il2CppFullySharedGenericAny* ___1_container, int32_t* ___2_returnCode, VisitParameters_tF21AC3343F90A7925EEA69AA73269B7F2DCE66CE ___3_parameters, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TypeConversion_TryConvert_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_mC18DF87EC56E149F05CE5A957357981A256F363B_fshared (ConversionRegistry_tBB92E13DAE967B31A341845C05EC88A2603E6426* ___0_registry, Il2CppFullySharedGenericAny* ___1_source, Il2CppFullySharedGenericAny* ___2_destination, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 NativeArrayUnsafeUtility_ConvertExistingDataToNativeArray_TisIl2CppFullySharedGenericStruct_m6920C14D4E38FAB84BD2B5F148CE70DF7F224F52_fshared (void* ___0_dataPointer, int32_t ___1_length, int32_t ___2_allocator, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR EventCallbackInternal_t74403797F0007ACB72019EE51D2C4DC33E5F63AF* EventCallbackPool_Get_TisIl2CppSharedGenericObject_mFF482DEA45910068B92C6850F72BFEA2DD7F5517_gshared (EventCallbackPool_t337A1A0692B33BCB6B3D2C662666D78276F9CD3C* __this, Delegate_t* ___0_userCallback, int32_t ___1_argId, int32_t ___2_callbackOptions, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ArgValueList_AcquireTemporary_TisIl2CppFullySharedGenericAny_m04BEDB27AA80C6FDF03E2790C8B975AAA5DD70FE_fshared (ArgValueList_tF9404610631615CB400FD008E21A0CE700A1814C* __this, Il2CppFullySharedGenericAny* ___0_value, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t UnsafeUtilityInternal_SizeOf_TisIl2CppFullySharedGenericStruct_m9FC95A845D303A9C72ED4044691DD435A70F994A_fshared_inline (const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Il2CppFullySharedGenericAny* UnsafeUtilityInternal_As_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_mE1CA751887466B801BE69083C2B0EA3EDE41FF9B_fshared_inline (Il2CppFullySharedGenericAny* ___0_from, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_mA6DFDBC2B22D6318212C6989A34784BD8303AF33_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Il2CppFullySharedGenericAny ___0_item, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared_inline (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Func_1_Invoke_m35CBC166F464B8A1169583672025E6D64211856C_fshared (Func_1_tBB8824FA8746333BFFF3AB3CE4A41B58450AF431* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t ObjectPool_1_get_CountAll_m8372ACA3A976C3FCB45CD77A0AEF0BDA496EBB6D_gshared_inline (ObjectPool_1_tC6F0B8E0BAAEC07474AABA8496261838C79CE1F3* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ObjectPool_1_set_CountAll_m6E7C5E741DCB73BD743FC27BACBE1886F4414440_gshared_inline (ObjectPool_1_tC6F0B8E0BAAEC07474AABA8496261838C79CE1F3* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_mF6C23E9072D8CBE2872705E7E4D08BDB78770F5C_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Action_1_Invoke_m5A038831CEB84A7E374FE59D43444412629F833F_fshared (Action_1_t923A20D1D4F6B55B2ED5AE21B90F1A0CE0450D99* __this, Il2CppFullySharedGenericAny ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ObjectPool_1_get_CountInactive_m40351B6BE62E0944A0813C07869822EC0D1E8D26_gshared (ObjectPool_1_tC6F0B8E0BAAEC07474AABA8496261838C79CE1F3* __this, const RuntimeMethod* method) ;

inline int32_t Unsafe_SizeOf_TisIl2CppFullySharedGenericAny_mD5748385042811E049C06FFD865AFD213C5FA9EB (const RuntimeMethod* method)
{
	return ((  int32_t (*) (const RuntimeMethod*))Unsafe_SizeOf_TisIl2CppFullySharedGenericAny_mD5748385042811E049C06FFD865AFD213C5FA9EB_fshared)(method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_mAA31A45BF1B1FA6490F0913B7FB59537767E5EF3 (uint8_t* ___0_dest, uint8_t* ___1_src, uint32_t ___2_len, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Type_t* Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57 (RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ___0_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RuntimeImports_Memmove_wbarrier_m8487273EE6161DB248FB65BB931279FF04F3CA79 (uint8_t* ___0_dest, uint8_t* ___1_src, uint32_t ___2_len, intptr_t ___3_type_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void* BurstCompiler_Compile_mF64F58440FAD34F65750AAF482F45E3F7C2D1B3C (RuntimeObject* ___0_delegateObj, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void IntPtr__ctor_m4F9A9B80F01996B610D5AE4797F20B98ECD0A3D9_inline (intptr_t* __this, void* ___0_value, const RuntimeMethod* method) ;
inline void FunctionPointer_1__ctor_m90A0E7C616EE3656333329253AA9678B5B69CF64_inline (FunctionPointer_1_tFA7B05CB8F034C67CD0B63063DD837ACC1C06AE9* __this, intptr_t ___0_ptr, const RuntimeMethod* method)
{
	((  void (*) (FunctionPointer_1_tFA7B05CB8F034C67CD0B63063DD837ACC1C06AE9*, intptr_t, const RuntimeMethod*))FunctionPointer_1__ctor_m539FE4A638EAB194C030CB25B52DBB1BADEA6E5A_fshared_inline)(__this, ___0_ptr, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void VisualElement_AddEventCallbackCategories_m815891F5DC080DB4EA311240774DAF57DD74AB26 (VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* __this, int32_t ___0_eventCategories, int32_t ___1_callbackOptions, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465 (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* __this, String_t* ___0_message, const RuntimeMethod* method) ;
inline void CallbackEventHandler_AddEventCategories_TisIl2CppSharedGenericObject_m48C6249122107B0206C2479BEA30DDCDEB81D426 (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* __this, int32_t ___0_callbackOptions, const RuntimeMethod* method)
{
	((  void (*) (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4*, int32_t, const RuntimeMethod*))CallbackEventHandler_AddEventCategories_TisIl2CppSharedGenericObject_m48C6249122107B0206C2479BEA30DDCDEB81D426_gshared)(__this, ___0_callbackOptions, method);
}
inline void CallbackEventHandler_AddListenersAndCategories_TisIl2CppSharedGenericObject_m7ACB30B716F504416F9FB1910C2CAF60F02E627F_inline (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* __this, Delegate_t* ___0_callback, int32_t ___1_callbackOptions, const RuntimeMethod* method)
{
	((  void (*) (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4*, Delegate_t*, int32_t, const RuntimeMethod*))CallbackEventHandler_AddListenersAndCategories_TisIl2CppSharedGenericObject_m7ACB30B716F504416F9FB1910C2CAF60F02E627F_gshared_inline)(__this, ___0_callback, ___1_callbackOptions, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* EventCallbackRegistry_GetPooled_m2AA6591F6DE9F778B7B0F30D63123589242190E2 (const RuntimeMethod* method) ;
inline void EventCallbackRegistry_RegisterCallback_TisIl2CppSharedGenericObject_m88FAF5D5C2F885FF0B1926FB47DD948259F8029B_inline (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* __this, EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* ___0_userCallback, int32_t ___1_callbackOptions, const RuntimeMethod* method)
{
	((  void (*) (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85*, EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482*, int32_t, const RuntimeMethod*))EventCallbackRegistry_RegisterCallback_TisIl2CppSharedGenericObject_m88FAF5D5C2F885FF0B1926FB47DD948259F8029B_gshared_inline)(__this, ___0_userCallback, ___1_callbackOptions, method);
}
inline void EventCallbackRegistry_RegisterCallback_TisIl2CppSharedGenericObject_TisIl2CppFullySharedGenericAny_m4C03FF9793B46745C6FCB27E7C02C6816E3EFB1A_inline (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* __this, EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE* ___0_userCallback, CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* ___1_element, Il2CppFullySharedGenericAny ___2_userArgs, int32_t ___3_callbackOptions, const RuntimeMethod* method)
{
	((  void (*) (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85*, EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE*, CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4*, Il2CppFullySharedGenericAny, int32_t, const RuntimeMethod*))EventCallbackRegistry_RegisterCallback_TisIl2CppSharedGenericObject_TisIl2CppFullySharedGenericAny_m4C03FF9793B46745C6FCB27E7C02C6816E3EFB1A_fshared_inline)((EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85*)__this, ___0_userCallback, ___1_element, ___2_userArgs, ___3_callbackOptions, method);
}
inline void CallbackEventHandler_RemoveListeners_TisIl2CppSharedGenericObject_m44964C63A8123B46E7D698F80D9205E7387A53B3_inline (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* __this, Delegate_t* ___0_callback, const RuntimeMethod* method)
{
	((  void (*) (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4*, Delegate_t*, const RuntimeMethod*))CallbackEventHandler_RemoveListeners_TisIl2CppSharedGenericObject_m44964C63A8123B46E7D698F80D9205E7387A53B3_gshared_inline)(__this, ___0_callback, method);
}
inline bool EventCallbackRegistry_UnregisterCallback_TisIl2CppSharedGenericObject_m83DD8A054C7A025857863C137916EC36CC066AA6_inline (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* __this, EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* ___0_callback, int32_t ___1_callbackOptions, const RuntimeMethod* method)
{
	return ((  bool (*) (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85*, EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482*, int32_t, const RuntimeMethod*))EventCallbackRegistry_UnregisterCallback_TisIl2CppSharedGenericObject_m83DD8A054C7A025857863C137916EC36CC066AA6_gshared_inline)(__this, ___0_callback, ___1_callbackOptions, method);
}
inline bool EventCallbackRegistry_UnregisterCallback_TisIl2CppSharedGenericObject_TisIl2CppFullySharedGenericAny_mA1B34BFD9C5EE81778ED146A48FA639F9CE18948_inline (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* __this, EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE* ___0_callback, int32_t ___1_callbackOptions, const RuntimeMethod* method)
{
	return ((  bool (*) (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85*, EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE*, int32_t, const RuntimeMethod*))EventCallbackRegistry_UnregisterCallback_TisIl2CppSharedGenericObject_TisIl2CppFullySharedGenericAny_mA1B34BFD9C5EE81778ED146A48FA639F9CE18948_fshared_inline)(__this, ___0_callback, ___1_callbackOptions, method);
}
inline int64_t EventBase_1_TypeId_m08396DED606ACD1093BEEA8D939E5DA37B797C12_inline (const RuntimeMethod* method)
{
	return ((  int64_t (*) (const RuntimeMethod*))EventBase_1_TypeId_m5B74C35CC6F5F2EC559BDD7A71544FD6FDE37CDE_gshared_inline)(method);
}
inline int32_t PointerEventBase_1_get_button_mE854A473F8446E8E956AB6229868A8642BE30D10_inline (PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816*, const RuntimeMethod*))PointerEventBase_1_get_button_mE854A473F8446E8E956AB6229868A8642BE30D10_gshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ClickDetector_StartClickTracking_mF87B35BA2F55B42F47709D89FA651EEFCE615CE7 (ClickDetector_t6B5A82C99CFD12E051D8E84A7C8F7488355B8F31* __this, EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C* ___0_evt, const RuntimeMethod* method) ;
inline int64_t EventBase_1_TypeId_m3FC722D85FBD1538CE6B96AD3364192799BF1CA6_inline (const RuntimeMethod* method)
{
	return ((  int64_t (*) (const RuntimeMethod*))EventBase_1_TypeId_m5B74C35CC6F5F2EC559BDD7A71544FD6FDE37CDE_gshared_inline)(method);
}
inline int32_t PointerEventBase_1_get_pressedButtons_m4A6DBB316294BB3899BA348BCD895B7C69F7D1D9_inline (PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816*, const RuntimeMethod*))PointerEventBase_1_get_pressedButtons_m4A6DBB316294BB3899BA348BCD895B7C69F7D1D9_gshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ClickDetector_SendClickEvent_m3AC03E996C28D79AAB47D49BEA01887AFF21B310 (ClickDetector_t6B5A82C99CFD12E051D8E84A7C8F7488355B8F31* __this, EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C* ___0_evt, const RuntimeMethod* method) ;
inline int32_t PointerEventBase_1_get_pointerId_m96350DDBEB7BE4407122043EEF6826D54520E58A_inline (PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816*, const RuntimeMethod*))PointerEventBase_1_get_pointerId_m96350DDBEB7BE4407122043EEF6826D54520E58A_gshared_inline)(__this, method);
}
inline ButtonClickStatus_tD4248512DB59E16501C4DDB6EDC2C6DE04290470* List_1_get_Item_m97E4330A4B3A01D99AD2ACC1BAE011F8FD18C7BB (List_1_tBDD12EAD3C5C46706730C230F223EE020C6822D6* __this, int32_t ___0_index, const RuntimeMethod* method)
{
	ButtonClickStatus_tD4248512DB59E16501C4DDB6EDC2C6DE04290470* il2cppRetVal;
	((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, Il2CppFullySharedGenericAny*, const RuntimeMethod*))List_1_get_Item_m6E4BA37C1FB558E4A62AE4324212E45D09C5C937_fshared)((List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*)__this, ___0_index, (Il2CppFullySharedGenericAny*)&il2cppRetVal, method);
	return il2cppRetVal;
}
inline int64_t EventBase_1_TypeId_m1A2720F11AB0D624809678ADE4E6D8C250E43741_inline (const RuntimeMethod* method)
{
	return ((  int64_t (*) (const RuntimeMethod*))EventBase_1_TypeId_m5B74C35CC6F5F2EC559BDD7A71544FD6FDE37CDE_gshared_inline)(method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ClickDetector_CancelClickTracking_m93EDACD3A68DC09BD1DB229F9D390466E25E4339 (ClickDetector_t6B5A82C99CFD12E051D8E84A7C8F7488355B8F31* __this, EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C* ___0_evt, const RuntimeMethod* method) ;
inline int64_t EventBase_1_TypeId_mA90FE9E21D00125CFC53652D23DB65FD2574D60D_inline (const RuntimeMethod* method)
{
	return ((  int64_t (*) (const RuntimeMethod*))EventBase_1_TypeId_m5B74C35CC6F5F2EC559BDD7A71544FD6FDE37CDE_gshared_inline)(method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* __this, String_t* ___0_paramName, const RuntimeMethod* method) ;
inline void Array_Resize_TisIl2CppFullySharedGenericAny_m263FC41C8DB989397C43C86556D63CEBE13F4712 (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** ___0_array, int32_t ___1_newSize, const RuntimeMethod* method)
{
	((  void (*) (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC**, int32_t, const RuntimeMethod*))Array_Resize_TisIl2CppFullySharedGenericAny_m263FC41C8DB989397C43C86556D63CEBE13F4712_fshared)(___0_array, ___1_newSize, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void IndexOutOfRangeException__ctor_mFD06819F05B815BE2D6E826D4E04F4C449D0A425 (IndexOutOfRangeException_t7ECB35264FB6CA8FAA516BD958F4B2ADC78E8A82* __this, String_t* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Copy_m4233828B4E6288B6D815F539AAA38575DE627900 (RuntimeArray* ___0_sourceArray, RuntimeArray* ___1_destinationArray, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41 (RuntimeArray* ___0_sourceArray, int32_t ___1_sourceIndex, RuntimeArray* ___2_destinationArray, int32_t ___3_destinationIndex, int32_t ___4_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentOutOfRangeException__ctor_mBC1D5DEEA1BA41DE77228CB27D6BAFEB6DCCBF4A (ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F* __this, String_t* ___0_paramName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StringBuilder__ctor_m1D99713357DE05DAFA296633639DB55F8C30587D (StringBuilder_t* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringBuilder_t* StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D (StringBuilder_t* __this, String_t* ___0_value, const RuntimeMethod* method) ;
inline String_t* Func_2_Invoke_mDA85FF37C571381390F5F42887FD3EF66434548F (Func_2_t3793651739D8B94095DFFF75C3BD348E5E2C455C* __this, Il2CppFullySharedGenericAny ___0_arg, const RuntimeMethod* method)
{
	String_t* il2cppRetVal;
	((  void (*) (Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0*, Il2CppFullySharedGenericAny, Il2CppFullySharedGenericAny*, const RuntimeMethod*))Func_2_Invoke_m31CAC166FDC80DC5AE52A5AEFFEE2D9B27A1CA3F_fshared)((Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0*)__this, ___0_arg, (Il2CppFullySharedGenericAny*)&il2cppRetVal, method);
	return il2cppRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringBuilder_t* StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1 (StringBuilder_t* __this, Il2CppChar ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t StringBuilder_get_Length_mDEA041E7357C68CC3B5885276BB403676DAAE0D8 (StringBuilder_t* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StringBuilder_set_Length_mE2427BDAEF91C4E4A6C80F3BDF1F6E01DBCC2414 (StringBuilder_t* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AllocatorManager_IsCustomAllocator_m38BCD079BAB0D64962201CD05D671C2A42CE1909 (AllocatorHandle_t3CA09720B1F89F91A8DDBA95E74C28A1EC3E3148 ___0_allocator, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AllocatorHandle_get_ToAllocator_m34C297958A940D9D35C11B3D28B8E5A08E170FDF (AllocatorHandle_t3CA09720B1F89F91A8DDBA95E74C28A1EC3E3148* __this, const RuntimeMethod* method) ;
inline void NativeArray_1__ctor_m788DE0F85C4051DDF092DDF96484DE655ACFB6F1 (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18* __this, int32_t ___0_length, int32_t ___1_allocator, int32_t ___2_options, const RuntimeMethod* method)
{
	((  void (*) (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18*, int32_t, int32_t, int32_t, const RuntimeMethod*))NativeArray_1__ctor_m788DE0F85C4051DDF092DDF96484DE655ACFB6F1_fshared)(__this, ___0_length, ___1_allocator, ___2_options, method);
}
inline void NativeArrayExtensions_Initialize_TisIl2CppFullySharedGenericStruct_m10ABF0E8955AE4504761E521587E2D89638A1972 (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18* ___0_array, int32_t ___1_length, AllocatorHandle_t3CA09720B1F89F91A8DDBA95E74C28A1EC3E3148 ___2_allocator, int32_t ___3_options, const RuntimeMethod* method)
{
	((  void (*) (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18*, int32_t, AllocatorHandle_t3CA09720B1F89F91A8DDBA95E74C28A1EC3E3148, int32_t, const RuntimeMethod*))NativeArrayExtensions_Initialize_TisIl2CppFullySharedGenericStruct_m10ABF0E8955AE4504761E521587E2D89638A1972_fshared)(___0_array, ___1_length, ___2_allocator, ___3_options, method);
}
inline void* NativeArrayUnsafeUtility_GetUnsafeReadOnlyPtr_TisIl2CppFullySharedGenericStruct_m13C618FD69BBAB7D8C77632BF8A9116FCD17D234_inline (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 ___0_nativeArray, const RuntimeMethod* method)
{
	return ((  void* (*) (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18, const RuntimeMethod*))NativeArrayUnsafeUtility_GetUnsafeReadOnlyPtr_TisIl2CppFullySharedGenericStruct_m13C618FD69BBAB7D8C77632BF8A9116FCD17D234_fshared_inline)(___0_nativeArray, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t IntPtr_op_Explicit_mE2CEC14C61FD5E2159A03EA2AD97F5CDC5BB9F4D (void* ___0_value, const RuntimeMethod* method) ;
inline int32_t NativeArray_1_get_Length_mBE5CC8B844994CFC4AB434235F915881575E63C8_inline (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18*, const RuntimeMethod*))NativeArray_1_get_Length_mBE5CC8B844994CFC4AB434235F915881575E63C8_fshared_inline)(__this, method);
}
inline int32_t UnsafeUtility_SizeOf_TisIl2CppFullySharedGenericStruct_mB6FBF9DDC79FEC02EC8705E9E527E1CD495564BF_inline (const RuntimeMethod* method)
{
	return ((  int32_t (*) (const RuntimeMethod*))UnsafeUtility_SizeOf_TisIl2CppFullySharedGenericStruct_mB6FBF9DDC79FEC02EC8705E9E527E1CD495564BF_fshared_inline)(method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CommandBuffer_InternalSetComputeBufferNativeData_m8AD3F2597301A0E6A444F36909BEC29667A08B11 (CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* __this, ComputeBuffer_t51EADA9015EBCC1B982C5584E9AB2734415A8233* ___0_buffer, intptr_t ___1_data, int32_t ___2_nativeBufferStartIndex, int32_t ___3_graphicsBufferStartIndex, int32_t ___4_count, int32_t ___5_elemSize, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* Component_GetComponent_m4352437B839B9601EB007CAC1007C8E1FB8A427D (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, Type_t* ___0_type, const RuntimeMethod* method) ;
inline void ValueTuple_2__ctor_m9C5289686C3B53268DC508F5480E6FB8006655F8 (ValueTuple_2_t7064AD711EB2A320F9C65C116B07AF16D42C8CCB* __this, Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* ___0_item1, Il2CppFullySharedGenericAny ___1_item2, const RuntimeMethod* method)
{
	((  void (*) (ValueTuple_2_tBAA1E1D7D97D80E0EBA7FE8773B1FF409AEA3829*, Il2CppFullySharedGenericAny, Il2CppFullySharedGenericAny, const RuntimeMethod*))ValueTuple_2__ctor_mCAE8E725F680FA6BE2C23B9686C9F6056BB7E5CD_fshared)((ValueTuple_2_tBAA1E1D7D97D80E0EBA7FE8773B1FF409AEA3829*)__this, (Il2CppFullySharedGenericAny)___0_item1, ___1_item2, method);
}
inline Il2CppFullySharedGenericAny* UnsafeUtility_As_TisComponent_t39FBE53E5EFCF4409111FB22C15FF73717632EC3_TisIl2CppFullySharedGenericAny_mEC3987CEFE5C29E2385827850E163034278AF68D_inline (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3** ___0_from, const RuntimeMethod* method)
{
	return ((  Il2CppFullySharedGenericAny* (*) (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3**, const RuntimeMethod*))UnsafeUtility_As_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_mD6BECC32C10552E82C8D23115CAF774983D95F0C_fshared_inline)(___0_from, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* Component_GetComponentInChildren_m4050899166730E8F6629B895597CF4ECC894B597 (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, Type_t* ___0_t, bool ___1_includeInactive, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* Component_GetComponentInParent_mA402B7F87D23E039BB045630E5EA3AC020D8A7C2 (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, Type_t* ___0_t, bool ___1_includeInactive, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, const RuntimeMethod* method) ;
inline __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* GameObject_GetComponents_TisIl2CppFullySharedGenericAny_mE91E75D8E2C639EF49A5401C23EEFF029D54EDF0 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, const RuntimeMethod* method)
{
	return ((  __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* (*) (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*, const RuntimeMethod*))GameObject_GetComponents_TisIl2CppFullySharedGenericAny_mE91E75D8E2C639EF49A5401C23EEFF029D54EDF0_fshared)(__this, method);
}
inline List_1_t584CB490C8F4C21E0A0D5545409ED60BF71F3FE4* UnsafeUtility_As_TisList_1_t584CB490C8F4C21E0A0D5545409ED60BF71F3FE4_mBBB120960A8F2691FFD08D375D14EBC37D7E8D38 (RuntimeObject* ___0_from, const RuntimeMethod* method)
{
	return ((  List_1_t584CB490C8F4C21E0A0D5545409ED60BF71F3FE4* (*) (RuntimeObject*, const RuntimeMethod*))UnsafeUtility_As_TisIl2CppSharedGenericObject_m7E4A82367DE96668D1A49E6688B2ABFB2F09A6AF_gshared)(___0_from, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Component_GetComponentsForListInternal_m7B3A61AF88E5E6F845817EC28E22FD39944EFBC7 (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, Type_t* ___0_searchType, RuntimeObject* ___1_resultList, const RuntimeMethod* method) ;
inline __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* Component_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mB27BBCE995DC86EC5D047AE9D0804238DA9B4E28 (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, bool ___0_includeInactive, const RuntimeMethod* method)
{
	return ((  __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* (*) (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3*, bool, const RuntimeMethod*))Component_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mB27BBCE995DC86EC5D047AE9D0804238DA9B4E28_fshared)(__this, ___0_includeInactive, method);
}
inline __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* GameObject_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mE619FF82D1099A3B6ADAD0FF12F3A05E06C32C24 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, bool ___0_includeInactive, const RuntimeMethod* method)
{
	return ((  __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* (*) (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*, bool, const RuntimeMethod*))GameObject_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mE619FF82D1099A3B6ADAD0FF12F3A05E06C32C24_fshared)(__this, ___0_includeInactive, method);
}
inline void Component_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mB3B314FA2B0FB49AB1B2E190A9FBC8337638078B (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, bool ___0_includeInactive, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* ___1_result, const RuntimeMethod* method)
{
	((  void (*) (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3*, bool, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, const RuntimeMethod*))Component_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mB3B314FA2B0FB49AB1B2E190A9FBC8337638078B_fshared)(__this, ___0_includeInactive, ___1_result, method);
}
inline void GameObject_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mD80CAA2E88B01D4173A65A4C4C20DCCF713ADF62 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, bool ___0_includeInactive, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* ___1_results, const RuntimeMethod* method)
{
	((  void (*) (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*, bool, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, const RuntimeMethod*))GameObject_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mD80CAA2E88B01D4173A65A4C4C20DCCF713ADF62_fshared)(__this, ___0_includeInactive, ___1_results, method);
}
inline __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* Component_GetComponentsInParent_TisIl2CppFullySharedGenericAny_m66D0CCEEF66E1B1598B57995FD87FC3CEEE1CA5C (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, bool ___0_includeInactive, const RuntimeMethod* method)
{
	return ((  __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* (*) (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3*, bool, const RuntimeMethod*))Component_GetComponentsInParent_TisIl2CppFullySharedGenericAny_m66D0CCEEF66E1B1598B57995FD87FC3CEEE1CA5C_fshared)(__this, ___0_includeInactive, method);
}
inline __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* GameObject_GetComponentsInParent_TisIl2CppFullySharedGenericAny_m9BA598F4C76BF91ED59BD5DA257501EE1E9F6FBF (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, bool ___0_includeInactive, const RuntimeMethod* method)
{
	return ((  __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* (*) (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*, bool, const RuntimeMethod*))GameObject_GetComponentsInParent_TisIl2CppFullySharedGenericAny_m9BA598F4C76BF91ED59BD5DA257501EE1E9F6FBF_fshared)(__this, ___0_includeInactive, method);
}
inline void GameObject_GetComponentsInParent_TisIl2CppFullySharedGenericAny_mD31B5889D11FC1B81FB10F6C35A2A6683ACB8921 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, bool ___0_includeInactive, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* ___1_results, const RuntimeMethod* method)
{
	((  void (*) (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*, bool, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, const RuntimeMethod*))GameObject_GetComponentsInParent_TisIl2CppFullySharedGenericAny_mD31B5889D11FC1B81FB10F6C35A2A6683ACB8921_fshared)(__this, ___0_includeInactive, ___1_results, method);
}
inline bool GameObject_TryGetComponent_TisIl2CppFullySharedGenericAny_m8FE39C82005602F4FEA27DC201600DABE2481BD1 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, Il2CppFullySharedGenericAny* ___0_component, const RuntimeMethod* method)
{
	return ((  bool (*) (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*, Il2CppFullySharedGenericAny*, const RuntimeMethod*))GameObject_TryGetComponent_TisIl2CppFullySharedGenericAny_m8FE39C82005602F4FEA27DC201600DABE2481BD1_fshared)(__this, ___0_component, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ComputeBuffer_InternalSetNativeData_m77690E79461BD938F0BA7718FD51FF9B931893AC (ComputeBuffer_t51EADA9015EBCC1B982C5584E9AB2734415A8233* __this, intptr_t ___0_data, int32_t ___1_nativeBufferStartIndex, int32_t ___2_computeBufferStartIndex, int32_t ___3_count, int32_t ___4_elemSize, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_mA0534D6E2AE4D67A6BD8D45B3321323930EB930C (String_t* ___0_format, RuntimeObject* ___1_arg0, RuntimeObject* ___2_arg1, RuntimeObject* ___3_arg2, const RuntimeMethod* method) ;
inline int32_t ReadOnlySpan_1_get_Length_m3CC53FCCDE299F21DEF7AB63EF3D378DAB954005_inline (ReadOnlySpan_1_tE8C37D9A05FCAB953169AFFE8A0ABCA809781E25* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (ReadOnlySpan_1_tE8C37D9A05FCAB953169AFFE8A0ABCA809781E25*, const RuntimeMethod*))ReadOnlySpan_1_get_Length_m4430960CA0D0458B1A1106DD246CA9AB746B5DB2_fshared_inline)(__this, method);
}
inline Il2CppFullySharedGenericStruct* ReadOnlySpan_1_get_Item_mAFFA21964234394982172838F35555A4D5681233_inline (ReadOnlySpan_1_tE8C37D9A05FCAB953169AFFE8A0ABCA809781E25* __this, int32_t ___0_index, const RuntimeMethod* method)
{
	return ((  Il2CppFullySharedGenericStruct* (*) (ReadOnlySpan_1_tE8C37D9A05FCAB953169AFFE8A0ABCA809781E25*, int32_t, const RuntimeMethod*))ReadOnlySpan_1_get_Item_m9143C9CF6493AF0AD667C5BDEEF1D22895283F77_fshared_inline)(__this, ___0_index, method);
}
inline ConstantBufferSingleton_1_t800A1489DAFDDB639BB1BC6406F901FBDE4F0F69* ConstantBufferSingleton_1_get_instance_m9BCF8F5E44B966B85584D73034BF4E880D86DF2E (const RuntimeMethod* method)
{
	return ((  ConstantBufferSingleton_1_t800A1489DAFDDB639BB1BC6406F901FBDE4F0F69* (*) (const RuntimeMethod*))ConstantBufferSingleton_1_get_instance_m9BCF8F5E44B966B85584D73034BF4E880D86DF2E_fshared)(method);
}
inline void ConstantBuffer_1_UpdateData_m2B6875F2248E344B4C53051FAA9355CA582A8830 (ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D* __this, CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* ___0_cmd, Il2CppFullySharedGenericStruct* ___1_data, const RuntimeMethod* method)
{
	((  void (*) (ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D*, CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7*, Il2CppFullySharedGenericStruct*, const RuntimeMethod*))ConstantBuffer_1_UpdateData_m2B6875F2248E344B4C53051FAA9355CA582A8830_fshared)(__this, ___0_cmd, ___1_data, method);
}
inline void ConstantBuffer_1_Set_mE00DA8AF81B1D6E70D952A4A92D6F211DCE9FC52 (ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D* __this, CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* ___0_cmd, ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___1_cs, int32_t ___2_shaderId, const RuntimeMethod* method)
{
	((  void (*) (ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D*, CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7*, ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8*, int32_t, const RuntimeMethod*))ConstantBuffer_1_Set_mE00DA8AF81B1D6E70D952A4A92D6F211DCE9FC52_fshared)(__this, ___0_cmd, ___1_cs, ___2_shaderId, method);
}
inline void ConstantBuffer_1_SetGlobal_mFD13E51675FB9005C153098E16D6B047A4070BA2 (ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D* __this, CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* ___0_cmd, int32_t ___1_shaderId, const RuntimeMethod* method)
{
	((  void (*) (ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D*, CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7*, int32_t, const RuntimeMethod*))ConstantBuffer_1_SetGlobal_mFD13E51675FB9005C153098E16D6B047A4070BA2_fshared)(__this, ___0_cmd, ___1_shaderId, method);
}
inline void ConstantBuffer_1_Set_m95B690077F35AFDA92F058EE5E32BE2911264A28 (ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D* __this, ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___0_cs, int32_t ___1_shaderId, const RuntimeMethod* method)
{
	((  void (*) (ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D*, ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8*, int32_t, const RuntimeMethod*))ConstantBuffer_1_Set_m95B690077F35AFDA92F058EE5E32BE2911264A28_fshared)(__this, ___0_cs, ___1_shaderId, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool ContextContainer_Contains_mD38FBF0FAC84169E395802CBF880BD0980490AF3_inline (ContextContainer_t384E08F63FABEFF6A7F1A753F5EF02699DAD414C* __this, uint32_t ___0_typeId, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B (String_t* ___0_str0, String_t* ___1_str1, String_t* ___2_str2, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162 (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* __this, String_t* ___0_message, const RuntimeMethod* method) ;
inline Il2CppSharedGenericObject* ContextContainer_CreateAndGetData_TisIl2CppSharedGenericObject_m669524DBBE31F1D7ECBD4D5F583CE48EE0AD938C (ContextContainer_t384E08F63FABEFF6A7F1A753F5EF02699DAD414C* __this, uint32_t ___0_typeId, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (ContextContainer_t384E08F63FABEFF6A7F1A753F5EF02699DAD414C*, uint32_t, const RuntimeMethod*))ContextContainer_CreateAndGetData_TisIl2CppSharedGenericObject_m669524DBBE31F1D7ECBD4D5F583CE48EE0AD938C_gshared)(__this, ___0_typeId, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR uint32_t math_ceilpow2_m55F757254B1339A2FAC288807A06BFF24898B214_inline (uint32_t ___0_x, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int64_t math_max_mC58900240F2A88F08AD67CA1F3F4360987100521_inline (int64_t ___0_x, int64_t ___1_y, const RuntimeMethod* method) ;
inline void List_1_Add_m084F52E7C436446A23080F15F803212491693F6B_inline (List_1_t9B68833848E4C4D7F623C05F6B77F0449396354A* __this, uint32_t ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, Il2CppFullySharedGenericAny, const RuntimeMethod*))List_1_Add_mD4F3498FBD3BDD3F03CBCFB38041CBAC9C28CAFC_fshared_inline)((List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*)__this, (Il2CppFullySharedGenericAny)&___0_item, method);
}
inline Il2CppSharedGenericObject* Activator_CreateInstance_TisIl2CppSharedGenericObject_m4C74D3D65600820EB977E87AD08D24AF782C97C3 (const RuntimeMethod* method)
{
	Il2CppSharedGenericObject* il2cppRetVal;
	((  void (*) (Il2CppFullySharedGenericAny*, const RuntimeMethod*))Activator_CreateInstance_TisIl2CppFullySharedGenericAny_m68F3B5F53DBB1741E028DA541BE07A3D2382138D_fshared)((Il2CppFullySharedGenericAny*)&il2cppRetVal, method);
	return il2cppRetVal;
}
inline bool Predicate_1_Invoke_m6F76B5644E2D6BD882F23B65434545AD90EFE5A1 (Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* __this, Il2CppFullySharedGenericAny ___0_obj, const RuntimeMethod* method)
{
	return ((  bool (*) (Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107*, Il2CppFullySharedGenericAny, const RuntimeMethod*))Predicate_1_Invoke_m6F76B5644E2D6BD882F23B65434545AD90EFE5A1_fshared)((Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107*)__this, ___0_obj, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ConversionRegistry_TryGetConverter_mA5C88244D7C991C10AE5940EA5829BC20B3781CD (ConversionRegistry_tBB92E13DAE967B31A341845C05EC88A2603E6426* __this, Type_t* ___0_source, Type_t* ___1_destination, Delegate_t** ___2_converter, const RuntimeMethod* method) ;
inline void TypeConverter_2_Invoke_m22213A88AF14CB4CFBD0CCA1547E1476B01580F9 (TypeConverter_2_tA6276E0BDB8DB9E3B10AF0C31301ACC071589845* __this, Il2CppFullySharedGenericAny* ___0_value, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (TypeConverter_2_tA6276E0BDB8DB9E3B10AF0C31301ACC071589845*, Il2CppFullySharedGenericAny*, Il2CppFullySharedGenericAny*, const RuntimeMethod*))TypeConverter_2_Invoke_m22213A88AF14CB4CFBD0CCA1547E1476B01580F9_fshared)((TypeConverter_2_tA6276E0BDB8DB9E3B10AF0C31301ACC071589845*)__this, ___0_value, il2cppRetVal, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool PropertyPath_get_IsEmpty_m5923CD28AB201F75C64F40AED27C7E42FC5DBF50 (PropertyPath_tA523CA2740853534DF6C009C588464B45A6D0A79* __this, const RuntimeMethod* method) ;
inline SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660* ObjectPool_1_Get_m3DAF7F2DB4FF067AB34CBB2573A626C6DE39AEA5_inline (ObjectPool_1_t28F249C599ED2EFD04F6B973FEFA2541B52062DF* __this, const RuntimeMethod* method)
{
	return ((  SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660* (*) (ObjectPool_1_t28F249C599ED2EFD04F6B973FEFA2541B52062DF*, const RuntimeMethod*))ObjectPool_1_Get_m6033387D88EF9D1AD81A89F3045733BC4B98073D_gshared_inline)(__this, method);
}
inline void SetValueVisitor_1_set_group_m94980C75D99986FB92C00A15DA3C9C1E7DF4F175_inline (SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660* __this, ConverterGroup_t2D7DEDAC22662EF5C020DE54231DC23CD0B5268A* ___0_value, const RuntimeMethod* method)
{
	((  void (*) (SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660*, ConverterGroup_t2D7DEDAC22662EF5C020DE54231DC23CD0B5268A*, const RuntimeMethod*))SetValueVisitor_1_set_group_m94980C75D99986FB92C00A15DA3C9C1E7DF4F175_fshared_inline)(__this, ___0_value, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PathVisitor_set_Path_m8E3F004534B73D33C70C3BD20729F5987F11627B_inline (PathVisitor_tCD0947C41CDBDB774D22D560E5FFA01EF61C4446* __this, PropertyPath_tA523CA2740853534DF6C009C588464B45A6D0A79 ___0_value, const RuntimeMethod* method) ;
inline void ObjectPool_1_Release_m8A0B50EA8E475E234775D7C5979A56CD7AE4B7E9_inline (ObjectPool_1_t28F249C599ED2EFD04F6B973FEFA2541B52062DF* __this, SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660* ___0_element, const RuntimeMethod* method)
{
	((  void (*) (ObjectPool_1_t28F249C599ED2EFD04F6B973FEFA2541B52062DF*, SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660*, const RuntimeMethod*))ObjectPool_1_Release_m3183E8F3194884123A027D720D37F341AF367E48_gshared_inline)(__this, ___0_element, method);
}
inline bool PropertyContainer_TryAccept_TisIl2CppFullySharedGenericAny_mDD1DF6BB0301B9008B88A30F25AC64BE2728F0D6 (RuntimeObject* ___0_visitor, Il2CppFullySharedGenericAny* ___1_container, int32_t* ___2_returnCode, VisitParameters_tF21AC3343F90A7925EEA69AA73269B7F2DCE66CE ___3_parameters, const RuntimeMethod* method)
{
	return ((  bool (*) (RuntimeObject*, Il2CppFullySharedGenericAny*, int32_t*, VisitParameters_tF21AC3343F90A7925EEA69AA73269B7F2DCE66CE, const RuntimeMethod*))PropertyContainer_TryAccept_TisIl2CppFullySharedGenericAny_mDD1DF6BB0301B9008B88A30F25AC64BE2728F0D6_fshared)(___0_visitor, ___1_container, ___2_returnCode, ___3_parameters, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t PathVisitor_get_ReturnCode_m900403F09B0392B4D8C2E2E0EBD06148FCFA2734_inline (PathVisitor_tCD0947C41CDBDB774D22D560E5FFA01EF61C4446* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ConversionRegistry_tBB92E13DAE967B31A341845C05EC88A2603E6426* ConverterGroup_get_registerRef_mF4AFC513DF255DDA084C020971EB187A5FFFD888 (ConverterGroup_t2D7DEDAC22662EF5C020DE54231DC23CD0B5268A* __this, const RuntimeMethod* method) ;
inline bool TypeConversion_TryConvert_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_mC18DF87EC56E149F05CE5A957357981A256F363B (ConversionRegistry_tBB92E13DAE967B31A341845C05EC88A2603E6426* ___0_registry, Il2CppFullySharedGenericAny* ___1_source, Il2CppFullySharedGenericAny* ___2_destination, const RuntimeMethod* method)
{
	return ((  bool (*) (ConversionRegistry_tBB92E13DAE967B31A341845C05EC88A2603E6426*, Il2CppFullySharedGenericAny*, Il2CppFullySharedGenericAny*, const RuntimeMethod*))TypeConversion_TryConvert_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_mC18DF87EC56E149F05CE5A957357981A256F363B_fshared)(___0_registry, ___1_source, ___2_destination, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeArray* Enum_GetValues_m803B9D68C367FAABC5AFB6B5B52775C8A573CEF9 (Type_t* ___0_enumType, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Array_GetValue_m007D247B8A6FE5BD60FD1CD510A714A416F2BA21 (RuntimeArray* __this, int32_t ___0_index, const RuntimeMethod* method) ;
inline NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 NativeArrayUnsafeUtility_ConvertExistingDataToNativeArray_TisIl2CppFullySharedGenericStruct_m6920C14D4E38FAB84BD2B5F148CE70DF7F224F52 (void* ___0_dataPointer, int32_t ___1_length, int32_t ___2_allocator, const RuntimeMethod* method)
{
	return ((  NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 (*) (void*, int32_t, int32_t, const RuntimeMethod*))NativeArrayUnsafeUtility_ConvertExistingDataToNativeArray_TisIl2CppFullySharedGenericStruct_m6920C14D4E38FAB84BD2B5F148CE70DF7F224F52_fshared)(___0_dataPointer, ___1_length, ___2_allocator, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA* CustomAttributeExtensions_GetCustomAttribute_m7241E02800DB70BF4B7FE0DD35A57C32196B4030 (MemberInfo_t* ___0_element, Type_t* ___1_attributeType, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA* CustomAttributeExtensions_GetCustomAttribute_m8451F6949BEA92A69AE673CD95D6AD767B66880A (MemberInfo_t* ___0_element, Type_t* ___1_attributeType, bool ___2_inherit, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* CustomAttributeExtensions_GetCustomAttributes_mA663A241432473A89C830284F82A775F47A76FDC (MemberInfo_t* ___0_element, Type_t* ___1_attributeType, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* CustomAttributeExtensions_GetCustomAttributes_m19F5D9C3B60FA00E4692C21EC0D59328FAF631AE (MemberInfo_t* ___0_element, Type_t* ___1_attributeType, bool ___2_inherit, const RuntimeMethod* method) ;
inline EventCallbackInternal_t74403797F0007ACB72019EE51D2C4DC33E5F63AF* EventCallbackPool_Get_TisIl2CppSharedGenericObject_mFF482DEA45910068B92C6850F72BFEA2DD7F5517 (EventCallbackPool_t337A1A0692B33BCB6B3D2C662666D78276F9CD3C* __this, Delegate_t* ___0_userCallback, int32_t ___1_argId, int32_t ___2_callbackOptions, const RuntimeMethod* method)
{
	return ((  EventCallbackInternal_t74403797F0007ACB72019EE51D2C4DC33E5F63AF* (*) (EventCallbackPool_t337A1A0692B33BCB6B3D2C662666D78276F9CD3C*, Delegate_t*, int32_t, int32_t, const RuntimeMethod*))EventCallbackPool_Get_TisIl2CppSharedGenericObject_mFF482DEA45910068B92C6850F72BFEA2DD7F5517_gshared)(__this, ___0_userCallback, ___1_argId, ___2_callbackOptions, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void EventCallbackRegistry__RegisterLocalCallback_m6A4C55AB7E0B0DF3E995EA6FFC1F2F19A91EA156 (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* __this, EventCallbackInternal_t74403797F0007ACB72019EE51D2C4DC33E5F63AF* ___0_callback, const RuntimeMethod* method) ;
inline int32_t ArgValueList_AcquireTemporary_TisIl2CppFullySharedGenericAny_m04BEDB27AA80C6FDF03E2790C8B975AAA5DD70FE (ArgValueList_tF9404610631615CB400FD008E21A0CE700A1814C* __this, Il2CppFullySharedGenericAny* ___0_value, const RuntimeMethod* method)
{
	return ((  int32_t (*) (ArgValueList_tF9404610631615CB400FD008E21A0CE700A1814C*, Il2CppFullySharedGenericAny*, const RuntimeMethod*))ArgValueList_AcquireTemporary_TisIl2CppFullySharedGenericAny_m04BEDB27AA80C6FDF03E2790C8B975AAA5DD70FE_fshared)(__this, ___0_value, method);
}
inline int64_t EventBase_1_TypeId_m5B74C35CC6F5F2EC559BDD7A71544FD6FDE37CDE_inline (const RuntimeMethod* method)
{
	return ((  int64_t (*) (const RuntimeMethod*))EventBase_1_TypeId_m5B74C35CC6F5F2EC559BDD7A71544FD6FDE37CDE_gshared_inline)(method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool EventCallbackRegistry__UnregisterLocalCallback_m377FCD52E974865BACD644081B7B0B19071D8C0A (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* __this, Delegate_t* ___0_userCallback, int64_t ___1_eventTypeId, int32_t ___2_callbackOptions, const RuntimeMethod* method) ;
inline int32_t UnsafeUtilityInternal_SizeOf_TisIl2CppFullySharedGenericStruct_m9FC95A845D303A9C72ED4044691DD435A70F994A_inline (const RuntimeMethod* method)
{
	return ((  int32_t (*) (const RuntimeMethod*))UnsafeUtilityInternal_SizeOf_TisIl2CppFullySharedGenericStruct_m9FC95A845D303A9C72ED4044691DD435A70F994A_fshared_inline)(method);
}
inline Il2CppFullySharedGenericAny* UnsafeUtilityInternal_As_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_mE1CA751887466B801BE69083C2B0EA3EDE41FF9B_inline (Il2CppFullySharedGenericAny* ___0_from, const RuntimeMethod* method)
{
	return ((  Il2CppFullySharedGenericAny* (*) (Il2CppFullySharedGenericAny*, const RuntimeMethod*))UnsafeUtilityInternal_As_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_mE1CA751887466B801BE69083C2B0EA3EDE41FF9B_fshared_inline)(___0_from, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5 (const RuntimeMethod* method) ;
inline void List_1_AddWithResize_mA6DFDBC2B22D6318212C6989A34784BD8303AF33 (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Il2CppFullySharedGenericAny ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, Il2CppFullySharedGenericAny, const RuntimeMethod*))List_1_AddWithResize_mA6DFDBC2B22D6318212C6989A34784BD8303AF33_fshared)((List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*)__this, ___0_item, method);
}
inline int32_t List_1_get_Count_m9832942FE310479E64D9D0D23F29C65305BE1622_inline (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, const RuntimeMethod*))List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared_inline)(__this, method);
}
inline Il2CppSharedGenericObject* Func_1_Invoke_m201DDBB72CE38673C89C22034EC8779981D83E6D (Func_1_t566D4A654882E282E54CDC3A001E05486F913903* __this, const RuntimeMethod* method)
{
	Il2CppSharedGenericObject* il2cppRetVal;
	((  void (*) (Func_1_tBB8824FA8746333BFFF3AB3CE4A41B58450AF431*, Il2CppFullySharedGenericAny*, const RuntimeMethod*))Func_1_Invoke_m35CBC166F464B8A1169583672025E6D64211856C_fshared)((Func_1_tBB8824FA8746333BFFF3AB3CE4A41B58450AF431*)__this, (Il2CppFullySharedGenericAny*)&il2cppRetVal, method);
	return il2cppRetVal;
}
inline int32_t ObjectPool_1_get_CountAll_m8372ACA3A976C3FCB45CD77A0AEF0BDA496EBB6D_inline (ObjectPool_1_tC6F0B8E0BAAEC07474AABA8496261838C79CE1F3* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (ObjectPool_1_tC6F0B8E0BAAEC07474AABA8496261838C79CE1F3*, const RuntimeMethod*))ObjectPool_1_get_CountAll_m8372ACA3A976C3FCB45CD77A0AEF0BDA496EBB6D_gshared_inline)(__this, method);
}
inline void ObjectPool_1_set_CountAll_m6E7C5E741DCB73BD743FC27BACBE1886F4414440_inline (ObjectPool_1_tC6F0B8E0BAAEC07474AABA8496261838C79CE1F3* __this, int32_t ___0_value, const RuntimeMethod* method)
{
	((  void (*) (ObjectPool_1_tC6F0B8E0BAAEC07474AABA8496261838C79CE1F3*, int32_t, const RuntimeMethod*))ObjectPool_1_set_CountAll_m6E7C5E741DCB73BD743FC27BACBE1886F4414440_gshared_inline)(__this, ___0_value, method);
}
inline Il2CppSharedGenericObject* List_1_get_Item_m02336594E8F76D7313E2BCB7A3ED5ABD13B4D515 (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, const RuntimeMethod* method)
{
	Il2CppSharedGenericObject* il2cppRetVal;
	((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, Il2CppFullySharedGenericAny*, const RuntimeMethod*))List_1_get_Item_m6E4BA37C1FB558E4A62AE4324212E45D09C5C937_fshared)((List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*)__this, ___0_index, (Il2CppFullySharedGenericAny*)&il2cppRetVal, method);
	return il2cppRetVal;
}
inline void List_1_RemoveAt_m36F68EA689DB109415094085A496D5EA21E2F23C (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, const RuntimeMethod* method)
{
	((  void (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, int32_t, const RuntimeMethod*))List_1_RemoveAt_mF6C23E9072D8CBE2872705E7E4D08BDB78770F5C_fshared)(__this, ___0_index, method);
}
inline void Action_1_Invoke_mDB8F7C7381C08B4DCE4E409D2FA4073BC17F90B5 (Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* __this, Il2CppSharedGenericObject* ___0_obj, const RuntimeMethod* method)
{
	((  void (*) (Action_1_t923A20D1D4F6B55B2ED5AE21B90F1A0CE0450D99*, Il2CppFullySharedGenericAny, const RuntimeMethod*))Action_1_Invoke_m5A038831CEB84A7E374FE59D43444412629F833F_fshared)((Action_1_t923A20D1D4F6B55B2ED5AE21B90F1A0CE0450D99*)__this, (Il2CppFullySharedGenericAny)___0_obj, method);
}
inline int32_t ObjectPool_1_get_CountInactive_m40351B6BE62E0944A0813C07869822EC0D1E8D26 (ObjectPool_1_tC6F0B8E0BAAEC07474AABA8496261838C79CE1F3* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (ObjectPool_1_tC6F0B8E0BAAEC07474AABA8496261838C79CE1F3*, const RuntimeMethod*))ObjectPool_1_get_CountInactive_m40351B6BE62E0944A0813C07869822EC0D1E8D26_gshared)(__this, method);
}
inline void List_1_Add_m0CE6E39234ED33F5195AABC8029D06D4756C75C1_inline (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, Il2CppFullySharedGenericAny, const RuntimeMethod*))List_1_Add_mD4F3498FBD3BDD3F03CBCFB38041CBAC9C28CAFC_fshared_inline)((List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*)__this, (Il2CppFullySharedGenericAny)___0_item, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisIl2CppFullySharedGenericAny_m7051201F0F2C986AB0084FB4A24BEE4D4733C5EB_fshared (Il2CppFullySharedGenericAny* ___0_destination, Il2CppFullySharedGenericAny* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(Type_t_StaticInit);
	uint8_t* V_0 = NULL;
	uint8_t* V_1 = NULL;
	uint8_t* V_2 = NULL;
	uint8_t* V_3 = NULL;
	RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B V_4;
	memset((&V_4), 0, sizeof(V_4));
	{
		bool L_0;
		L_0 = il2cpp_codegen_is_reference_or_contains_references(il2cpp_rgctx_method(method->rgctx_data, 0));
		if (L_0)
		{
			goto IL_002f;
		}
	}
	{
		Il2CppFullySharedGenericAny* L_1 = ___0_destination;
		uint8_t* L_2;
		L_2 = il2cpp_unsafe_as_ref<uint8_t>(L_1);
		V_1 = L_2;
		uint8_t* L_3 = V_1;
		uintptr_t L_4 = (il2cpp_codegen_conv<uintptr_t,uint8_t*,intptr_t,false,false>(L_3,NULL));
		Il2CppFullySharedGenericAny* L_5 = ___1_source;
		uint8_t* L_6;
		L_6 = il2cpp_unsafe_as_ref<uint8_t>(L_5);
		V_2 = L_6;
		uint8_t* L_7 = V_2;
		uintptr_t L_8 = (il2cpp_codegen_conv<uintptr_t,uint8_t*,intptr_t,false,false>(L_7,NULL));
		V_0 = (uint8_t*)L_8;
		uint8_t* L_9 = V_0;
		uint64_t L_10 = ___2_elementCount;
		uint32_t L_11 = (il2cpp_codegen_conv<uint32_t,uint64_t,int64_t,false,false>(L_10,NULL));
		int32_t L_12;
		L_12 = Unsafe_SizeOf_TisIl2CppFullySharedGenericAny_mD5748385042811E049C06FFD865AFD213C5FA9EB(il2cpp_rgctx_method(method->rgctx_data, 1));
		Buffer_Memmove_mAA31A45BF1B1FA6490F0913B7FB59537767E5EF3((uint8_t*)L_4, L_9, ((int32_t)il2cpp_codegen_multiply((int32_t)L_11, L_12)), NULL);
		uintptr_t L_13 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(0,NULL));
		V_1 = (uint8_t*)L_13;
		uintptr_t L_14 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(0,NULL));
		V_2 = (uint8_t*)L_14;
		return;
	}

IL_002f:
	{
		Il2CppFullySharedGenericAny* L_15 = ___0_destination;
		uint8_t* L_16;
		L_16 = il2cpp_unsafe_as_ref<uint8_t>(L_15);
		V_2 = L_16;
		uint8_t* L_17 = V_2;
		uintptr_t L_18 = (il2cpp_codegen_conv<uintptr_t,uint8_t*,intptr_t,false,false>(L_17,NULL));
		Il2CppFullySharedGenericAny* L_19 = ___1_source;
		uint8_t* L_20;
		L_20 = il2cpp_unsafe_as_ref<uint8_t>(L_19);
		V_1 = L_20;
		uint8_t* L_21 = V_1;
		uintptr_t L_22 = (il2cpp_codegen_conv<uintptr_t,uint8_t*,intptr_t,false,false>(L_21,NULL));
		V_3 = (uint8_t*)L_22;
		uint8_t* L_23 = V_3;
		uint64_t L_24 = ___2_elementCount;
		uint32_t L_25 = (il2cpp_codegen_conv<uint32_t,uint64_t,int64_t,false,false>(L_24,NULL));
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_26 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 2)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_27;
		L_27 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_26, NULL);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_28;
		L_28 = VirtualFuncInvoker0< RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B >::Invoke(98, L_27);
		V_4 = L_28;
		intptr_t L_29 = (&V_4)->___value;
		RuntimeImports_Memmove_wbarrier_m8487273EE6161DB248FB65BB931279FF04F3CA79((uint8_t*)L_18, L_23, L_25, L_29, NULL);
		uintptr_t L_30 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(0,NULL));
		V_2 = (uint8_t*)L_30;
		uintptr_t L_31 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(0,NULL));
		V_1 = (uint8_t*)L_31;
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FunctionPointer_1_tFA7B05CB8F034C67CD0B63063DD837ACC1C06AE9 BurstCompiler_CompileFunctionPointer_TisIl2CppSharedGenericObject_m56881AF9D74018BA2CDE51DE51F9700B6BD85134_gshared (Il2CppSharedGenericObject* ___0_delegateMethod, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BurstCompiler_t6972AFDB97C8649095A1B7EE54AAADB676BE2C37_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(BurstCompiler_t6972AFDB97C8649095A1B7EE54AAADB676BE2C37_StaticInit);
	{
		Il2CppSharedGenericObject* L_0 = ___0_delegateMethod;
		CHECKED_LOCAL_INIT(BurstCompiler_t6972AFDB97C8649095A1B7EE54AAADB676BE2C37_StaticInit,(BurstCompiler_t6972AFDB97C8649095A1B7EE54AAADB676BE2C37_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		void* L_1;
		L_1 = BurstCompiler_Compile_mF64F58440FAD34F65750AAF482F45E3F7C2D1B3C((RuntimeObject*)L_0, NULL);
		intptr_t L_2;
		memset((&L_2), 0, sizeof(L_2));
		IntPtr__ctor_m4F9A9B80F01996B610D5AE4797F20B98ECD0A3D9_inline((&L_2), L_1, NULL);
		FunctionPointer_1_tFA7B05CB8F034C67CD0B63063DD837ACC1C06AE9 L_3;
		memset((&L_3), 0, sizeof(L_3));
		FunctionPointer_1__ctor_m90A0E7C616EE3656333329253AA9678B5B69CF64_inline((&L_3), L_2, NULL);
		return L_3;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t BurstRuntime_GetHashCode64_TisIl2CppFullySharedGenericAny_m469AED5D9ACBE5277201D8FD554BBB838C1256CE_fshared (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(HashCode64_1_tEC6075B45444522F8859FD1C0FBECB8B1CB1DCEF_StaticInit);
	{
		CHECKED_LOCAL_INIT(HashCode64_1_tEC6075B45444522F8859FD1C0FBECB8B1CB1DCEF_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		int64_t L_0 = ((HashCode64_1_t25B99E9F7F998CC07CCA13124E03920779BFF803_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CallbackEventHandler_AddEventCategories_TisIl2CppSharedGenericObject_m48C6249122107B0206C2479BEA30DDCDEB81D426_gshared (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* __this, int32_t ___0_callbackOptions, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(EventBase_1_tB1D7BD1D1D5954DB9445A5C86B19893FD849D268_StaticInit);
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* V_0 = NULL;
	{
		V_0 = ((VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115*)IsInstClass((RuntimeObject*)__this, VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115_il2cpp_TypeInfo_var));
		VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* L_0 = V_0;
		if (!L_0)
		{
			goto IL_001b;
		}
	}
	{
		VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* L_1 = V_0;
		CHECKED_LOCAL_INIT(EventBase_1_tB1D7BD1D1D5954DB9445A5C86B19893FD849D268_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		int32_t L_2 = ((EventBase_1_t880C2458D345D7530A8295FA56BDDF14D326DE8F_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___EventCategory;
		int32_t L_3 = ___0_callbackOptions;
		VisualElement_AddEventCallbackCategories_m815891F5DC080DB4EA311240774DAF57DD74AB26(L_1, ((int32_t)(1<<((int32_t)((int32_t)L_2&((int32_t)31))))), L_3, NULL);
	}

IL_001b:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CallbackEventHandler_AddListenersAndCategories_TisIl2CppSharedGenericObject_m7ACB30B716F504416F9FB1910C2CAF60F02E627F_gshared (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* __this, Delegate_t* ___0_callback, int32_t ___1_callbackOptions, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		Delegate_t* L_0 = ___0_callback;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_1 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral7A0A3608A3C4012D35E5DE3B2BA646379CCF198F)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, method);
	}

IL_000e:
	{
		int32_t L_2 = ___1_callbackOptions;
		CallbackEventHandler_AddEventCategories_TisIl2CppSharedGenericObject_m48C6249122107B0206C2479BEA30DDCDEB81D426(__this, L_2, il2cpp_rgctx_method(method->rgctx_data, 0));
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CallbackEventHandler_RegisterCallback_TisIl2CppSharedGenericObject_m7DAA079F20D9D9FB60FB1D2717B7D20F7B10C032_gshared (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* __this, EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* ___0_callback, int32_t ___1_callbackOptions, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_StaticInit);
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* V_0 = NULL;
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* G_B2_0 = NULL;
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* G_B1_0 = NULL;
	{
		EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* L_0 = ___0_callback;
		int32_t L_1 = ___1_callbackOptions;
		CallbackEventHandler_AddListenersAndCategories_TisIl2CppSharedGenericObject_m7ACB30B716F504416F9FB1910C2CAF60F02E627F_inline(__this, (Delegate_t*)L_0, L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_2 = __this->___m_CallbackRegistry;
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_3 = L_2;
		if (L_3)
		{
			G_B2_0 = L_3;
			goto IL_0020;
		}
		G_B1_0 = L_3;
	}
	{
		CHECKED_LOCAL_INIT(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_StaticInit,(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_4;
		L_4 = EventCallbackRegistry_GetPooled_m2AA6591F6DE9F778B7B0F30D63123589242190E2(NULL);
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_5 = L_4;
		V_0 = L_5;
		__this->___m_CallbackRegistry = L_5;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_CallbackRegistry), (void*)L_5);
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_6 = V_0;
		G_B2_0 = L_6;
	}

IL_0020:
	{
		EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* L_7 = ___0_callback;
		int32_t L_8 = ___1_callbackOptions;
		EventCallbackRegistry_RegisterCallback_TisIl2CppSharedGenericObject_m88FAF5D5C2F885FF0B1926FB47DD948259F8029B_inline(G_B2_0, L_7, L_8, il2cpp_rgctx_method(method->rgctx_data, 1));
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CallbackEventHandler_RegisterCallback_TisIl2CppSharedGenericObject_mADA60DA6265A096768601EDE87108F04A585DF5C_gshared (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* __this, EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* ___0_callback, int32_t ___1_useTrickleDown, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_StaticInit);
	int32_t V_0 = 0;
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* V_1 = NULL;
	int32_t G_B3_0 = 0;
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* G_B5_0 = NULL;
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* G_B4_0 = NULL;
	{
		int32_t L_0 = ___1_useTrickleDown;
		if ((((int32_t)L_0) == ((int32_t)1)))
		{
			goto IL_0007;
		}
	}
	{
		G_B3_0 = 0;
		goto IL_0008;
	}

IL_0007:
	{
		G_B3_0 = 1;
	}

IL_0008:
	{
		V_0 = G_B3_0;
		EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* L_1 = ___0_callback;
		int32_t L_2 = V_0;
		CallbackEventHandler_AddListenersAndCategories_TisIl2CppSharedGenericObject_m7ACB30B716F504416F9FB1910C2CAF60F02E627F_inline(__this, (Delegate_t*)L_1, L_2, il2cpp_rgctx_method(method->rgctx_data, 0));
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_3 = __this->___m_CallbackRegistry;
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_4 = L_3;
		if (L_4)
		{
			G_B5_0 = L_4;
			goto IL_0029;
		}
		G_B4_0 = L_4;
	}
	{
		CHECKED_LOCAL_INIT(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_StaticInit,(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_5;
		L_5 = EventCallbackRegistry_GetPooled_m2AA6591F6DE9F778B7B0F30D63123589242190E2(NULL);
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_6 = L_5;
		V_1 = L_6;
		__this->___m_CallbackRegistry = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_CallbackRegistry), (void*)L_6);
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_7 = V_1;
		G_B5_0 = L_7;
	}

IL_0029:
	{
		EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* L_8 = ___0_callback;
		int32_t L_9 = V_0;
		EventCallbackRegistry_RegisterCallback_TisIl2CppSharedGenericObject_m88FAF5D5C2F885FF0B1926FB47DD948259F8029B_inline(G_B5_0, L_8, L_9, il2cpp_rgctx_method(method->rgctx_data, 1));
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CallbackEventHandler_RegisterCallback_TisIl2CppSharedGenericObject_TisIl2CppFullySharedGenericAny_m1B4F8A79DEE7821921AA2B21393FEA828346BE1E_fshared (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* __this, EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE* ___0_callback, Il2CppFullySharedGenericAny ___1_userArgs, int32_t ___2_useTrickleDown, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_StaticInit);
	const uint32_t SizeOf_TUserArgsType_t6FE3524CF9F229B1DE6FDD1C5C86E6CAAB721920 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_9 = alloca(SizeOf_TUserArgsType_t6FE3524CF9F229B1DE6FDD1C5C86E6CAAB721920);
	int32_t V_0 = 0;
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* V_1 = NULL;
	int32_t G_B3_0 = 0;
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* G_B5_0 = NULL;
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* G_B4_0 = NULL;
	{
		int32_t L_0 = ___2_useTrickleDown;
		if ((((int32_t)L_0) == ((int32_t)1)))
		{
			goto IL_0007;
		}
	}
	{
		G_B3_0 = 0;
		goto IL_0008;
	}

IL_0007:
	{
		G_B3_0 = 1;
	}

IL_0008:
	{
		V_0 = G_B3_0;
		EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE* L_1 = ___0_callback;
		int32_t L_2 = V_0;
		CallbackEventHandler_AddListenersAndCategories_TisIl2CppSharedGenericObject_m7ACB30B716F504416F9FB1910C2CAF60F02E627F_inline(__this, (Delegate_t*)L_1, L_2, il2cpp_rgctx_method(method->rgctx_data, 0));
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_3 = __this->___m_CallbackRegistry;
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_4 = L_3;
		if (L_4)
		{
			G_B5_0 = L_4;
			goto IL_0029;
		}
		G_B4_0 = L_4;
	}
	{
		CHECKED_LOCAL_INIT(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_StaticInit,(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_5;
		L_5 = EventCallbackRegistry_GetPooled_m2AA6591F6DE9F778B7B0F30D63123589242190E2(NULL);
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_6 = L_5;
		V_1 = L_6;
		__this->___m_CallbackRegistry = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_CallbackRegistry), (void*)L_6);
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_7 = V_1;
		G_B5_0 = L_7;
	}

IL_0029:
	{
		EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE* L_8 = ___0_callback;
		il2cpp_codegen_memcpy(L_9, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_userArgs : &___1_userArgs), SizeOf_TUserArgsType_t6FE3524CF9F229B1DE6FDD1C5C86E6CAAB721920);
		int32_t L_10 = V_0;
		EventCallbackRegistry_RegisterCallback_TisIl2CppSharedGenericObject_TisIl2CppFullySharedGenericAny_m4C03FF9793B46745C6FCB27E7C02C6816E3EFB1A_inline(G_B5_0, L_8, __this, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? L_9: *(void**)L_9), L_10, il2cpp_rgctx_method(method->rgctx_data, 2));
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CallbackEventHandler_RegisterCallbackOnce_TisIl2CppSharedGenericObject_mF88470AFD1D0397542F77E5E512CCB0F4FC3064D_gshared (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* __this, EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* ___0_callback, int32_t ___1_useTrickleDown, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_StaticInit);
	int32_t V_0 = 0;
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* V_1 = NULL;
	int32_t G_B2_0 = 0;
	int32_t G_B1_0 = 0;
	int32_t G_B3_0 = 0;
	int32_t G_B3_1 = 0;
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* G_B5_0 = NULL;
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* G_B4_0 = NULL;
	{
		int32_t L_0 = ___1_useTrickleDown;
		if ((((int32_t)L_0) == ((int32_t)1)))
		{
			G_B2_0 = 4;
			goto IL_0008;
		}
		G_B1_0 = 4;
	}
	{
		G_B3_0 = 0;
		G_B3_1 = G_B1_0;
		goto IL_0009;
	}

IL_0008:
	{
		G_B3_0 = 1;
		G_B3_1 = G_B2_0;
	}

IL_0009:
	{
		V_0 = ((int32_t)(G_B3_1|G_B3_0));
		EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* L_1 = ___0_callback;
		int32_t L_2 = V_0;
		CallbackEventHandler_AddListenersAndCategories_TisIl2CppSharedGenericObject_m7ACB30B716F504416F9FB1910C2CAF60F02E627F_inline(__this, (Delegate_t*)L_1, L_2, il2cpp_rgctx_method(method->rgctx_data, 0));
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_3 = __this->___m_CallbackRegistry;
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_4 = L_3;
		if (L_4)
		{
			G_B5_0 = L_4;
			goto IL_002b;
		}
		G_B4_0 = L_4;
	}
	{
		CHECKED_LOCAL_INIT(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_StaticInit,(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_5;
		L_5 = EventCallbackRegistry_GetPooled_m2AA6591F6DE9F778B7B0F30D63123589242190E2(NULL);
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_6 = L_5;
		V_1 = L_6;
		__this->___m_CallbackRegistry = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_CallbackRegistry), (void*)L_6);
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_7 = V_1;
		G_B5_0 = L_7;
	}

IL_002b:
	{
		EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* L_8 = ___0_callback;
		int32_t L_9 = V_0;
		EventCallbackRegistry_RegisterCallback_TisIl2CppSharedGenericObject_m88FAF5D5C2F885FF0B1926FB47DD948259F8029B_inline(G_B5_0, L_8, L_9, il2cpp_rgctx_method(method->rgctx_data, 1));
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CallbackEventHandler_RemoveListeners_TisIl2CppSharedGenericObject_m44964C63A8123B46E7D698F80D9205E7387A53B3_gshared (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* __this, Delegate_t* ___0_callback, const RuntimeMethod* method) 
{
	{
		Delegate_t* L_0 = ___0_callback;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_1 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral7A0A3608A3C4012D35E5DE3B2BA646379CCF198F)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, method);
	}

IL_000e:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CallbackEventHandler_UnregisterCallback_TisIl2CppSharedGenericObject_m489E8ED87FB88AFBC4FF1BA3EE5EE134B713505E_gshared (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* __this, EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* ___0_callback, int32_t ___1_useTrickleDown, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	int32_t V_0 = 0;
	int32_t G_B3_0 = 0;
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* G_B5_0 = NULL;
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* G_B4_0 = NULL;
	{
		int32_t L_0 = ___1_useTrickleDown;
		if ((((int32_t)L_0) == ((int32_t)1)))
		{
			goto IL_0007;
		}
	}
	{
		G_B3_0 = 0;
		goto IL_0008;
	}

IL_0007:
	{
		G_B3_0 = 1;
	}

IL_0008:
	{
		V_0 = G_B3_0;
		EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* L_1 = ___0_callback;
		CallbackEventHandler_RemoveListeners_TisIl2CppSharedGenericObject_m44964C63A8123B46E7D698F80D9205E7387A53B3_inline(__this, (Delegate_t*)L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_2 = __this->___m_CallbackRegistry;
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_3 = L_2;
		if (L_3)
		{
			G_B5_0 = L_3;
			goto IL_001b;
		}
		G_B4_0 = L_3;
	}
	{
		return;
	}

IL_001b:
	{
		EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* L_4 = ___0_callback;
		int32_t L_5 = V_0;
		bool L_6;
		L_6 = EventCallbackRegistry_UnregisterCallback_TisIl2CppSharedGenericObject_m83DD8A054C7A025857863C137916EC36CC066AA6_inline(G_B5_0, L_4, L_5, il2cpp_rgctx_method(method->rgctx_data, 1));
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CallbackEventHandler_UnregisterCallback_TisIl2CppSharedGenericObject_TisIl2CppFullySharedGenericAny_mFC49D4FE0F0D59A7AE693121AFE9857E9858129E_fshared (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* __this, EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE* ___0_callback, int32_t ___1_useTrickleDown, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	int32_t V_0 = 0;
	int32_t G_B3_0 = 0;
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* G_B5_0 = NULL;
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* G_B4_0 = NULL;
	{
		int32_t L_0 = ___1_useTrickleDown;
		if ((((int32_t)L_0) == ((int32_t)1)))
		{
			goto IL_0007;
		}
	}
	{
		G_B3_0 = 0;
		goto IL_0008;
	}

IL_0007:
	{
		G_B3_0 = 1;
	}

IL_0008:
	{
		V_0 = G_B3_0;
		EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE* L_1 = ___0_callback;
		CallbackEventHandler_RemoveListeners_TisIl2CppSharedGenericObject_m44964C63A8123B46E7D698F80D9205E7387A53B3_inline(__this, (Delegate_t*)L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_2 = __this->___m_CallbackRegistry;
		EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* L_3 = L_2;
		if (L_3)
		{
			G_B5_0 = L_3;
			goto IL_001b;
		}
		G_B4_0 = L_3;
	}
	{
		return;
	}

IL_001b:
	{
		EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE* L_4 = ___0_callback;
		int32_t L_5 = V_0;
		bool L_6;
		L_6 = EventCallbackRegistry_UnregisterCallback_TisIl2CppSharedGenericObject_TisIl2CppFullySharedGenericAny_mA1B34BFD9C5EE81778ED146A48FA639F9CE18948_inline(G_B5_0, L_4, L_5, il2cpp_rgctx_method(method->rgctx_data, 1));
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ClickDetector_ProcessEvent_TisIl2CppSharedGenericObject_m5765AE0E5F53D1B1B42447A4D46A147028B9A13D_gshared (ClickDetector_t6B5A82C99CFD12E051D8E84A7C8F7488355B8F31* __this, PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* ___0_evt, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EventBase_1_TypeId_m08396DED606ACD1093BEEA8D939E5DA37B797C12_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EventBase_1_TypeId_m1A2720F11AB0D624809678ADE4E6D8C250E43741_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EventBase_1_TypeId_m3FC722D85FBD1538CE6B96AD3364192799BF1CA6_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EventBase_1_TypeId_mA90FE9E21D00125CFC53652D23DB65FD2574D60D_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EventBase_1_t2637736704939772A86A2904BCFD1B314097DD1E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EventBase_1_t4941B5AE834AA028696C83756491CE8884F30DDE_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EventBase_1_t7D3D212E8AD9C53105AAC51DBD4FA5F700586F68_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EventBase_1_tB8AF17419B0E6B39E32F8925FCA535F5CF5C79D2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Item_m97E4330A4B3A01D99AD2ACC1BAE011F8FD18C7BB_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(EventBase_1_t7D3D212E8AD9C53105AAC51DBD4FA5F700586F68_StaticInit);
	CHECKED_LOCAL(EventBase_1_t4941B5AE834AA028696C83756491CE8884F30DDE_StaticInit);
	CHECKED_LOCAL(EventBase_1_tB8AF17419B0E6B39E32F8925FCA535F5CF5C79D2_StaticInit);
	CHECKED_LOCAL(EventBase_1_t2637736704939772A86A2904BCFD1B314097DD1E_StaticInit);
	ButtonClickStatus_tD4248512DB59E16501C4DDB6EDC2C6DE04290470* V_0 = NULL;
	{
		PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* L_0 = ___0_evt;
		int64_t L_1;
		L_1 = VirtualFuncInvoker0< int64_t >::Invoke(5, (EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C*)L_0);
		CHECKED_LOCAL_INIT(EventBase_1_t7D3D212E8AD9C53105AAC51DBD4FA5F700586F68_StaticInit,(EventBase_1_t7D3D212E8AD9C53105AAC51DBD4FA5F700586F68_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		int64_t L_2;
		L_2 = EventBase_1_TypeId_m08396DED606ACD1093BEEA8D939E5DA37B797C12_inline(EventBase_1_TypeId_m08396DED606ACD1093BEEA8D939E5DA37B797C12_RuntimeMethod_var);
		if ((!(((uint64_t)L_1) == ((uint64_t)L_2))))
		{
			goto IL_001d;
		}
	}
	{
		PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* L_3 = ___0_evt;
		int32_t L_4;
		L_4 = PointerEventBase_1_get_button_mE854A473F8446E8E956AB6229868A8642BE30D10_inline(L_3, NULL);
		if (L_4)
		{
			goto IL_001d;
		}
	}
	{
		PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* L_5 = ___0_evt;
		ClickDetector_StartClickTracking_mF87B35BA2F55B42F47709D89FA651EEFCE615CE7(__this, (EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C*)L_5, NULL);
		return;
	}

IL_001d:
	{
		PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* L_6 = ___0_evt;
		int64_t L_7;
		L_7 = VirtualFuncInvoker0< int64_t >::Invoke(5, (EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C*)L_6);
		CHECKED_LOCAL_INIT(EventBase_1_t4941B5AE834AA028696C83756491CE8884F30DDE_StaticInit,(EventBase_1_t4941B5AE834AA028696C83756491CE8884F30DDE_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		int64_t L_8;
		L_8 = EventBase_1_TypeId_m3FC722D85FBD1538CE6B96AD3364192799BF1CA6_inline(EventBase_1_TypeId_m3FC722D85FBD1538CE6B96AD3364192799BF1CA6_RuntimeMethod_var);
		if ((!(((uint64_t)L_7) == ((uint64_t)L_8))))
		{
			goto IL_0082;
		}
	}
	{
		PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* L_9 = ___0_evt;
		int32_t L_10;
		L_10 = PointerEventBase_1_get_button_mE854A473F8446E8E956AB6229868A8642BE30D10_inline(L_9, NULL);
		if (L_10)
		{
			goto IL_0045;
		}
	}
	{
		PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* L_11 = ___0_evt;
		int32_t L_12;
		L_12 = PointerEventBase_1_get_pressedButtons_m4A6DBB316294BB3899BA348BCD895B7C69F7D1D9_inline(L_11, NULL);
		if ((!(((uint32_t)((int32_t)(L_12&1))) == ((uint32_t)1))))
		{
			goto IL_0045;
		}
	}
	{
		PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* L_13 = ___0_evt;
		ClickDetector_StartClickTracking_mF87B35BA2F55B42F47709D89FA651EEFCE615CE7(__this, (EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C*)L_13, NULL);
		return;
	}

IL_0045:
	{
		PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* L_14 = ___0_evt;
		int32_t L_15;
		L_15 = PointerEventBase_1_get_button_mE854A473F8446E8E956AB6229868A8642BE30D10_inline(L_14, NULL);
		if (L_15)
		{
			goto IL_005f;
		}
	}
	{
		PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* L_16 = ___0_evt;
		int32_t L_17;
		L_17 = PointerEventBase_1_get_pressedButtons_m4A6DBB316294BB3899BA348BCD895B7C69F7D1D9_inline(L_16, NULL);
		if (((int32_t)(L_17&1)))
		{
			goto IL_005f;
		}
	}
	{
		PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* L_18 = ___0_evt;
		ClickDetector_SendClickEvent_m3AC03E996C28D79AAB47D49BEA01887AFF21B310(__this, (EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C*)L_18, NULL);
		return;
	}

IL_005f:
	{
		List_1_tBDD12EAD3C5C46706730C230F223EE020C6822D6* L_19 = __this->___m_ClickStatus;
		PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* L_20 = ___0_evt;
		int32_t L_21;
		L_21 = PointerEventBase_1_get_pointerId_m96350DDBEB7BE4407122043EEF6826D54520E58A_inline(L_20, NULL);
		ButtonClickStatus_tD4248512DB59E16501C4DDB6EDC2C6DE04290470* L_22;
		L_22 = List_1_get_Item_m97E4330A4B3A01D99AD2ACC1BAE011F8FD18C7BB(L_19, L_21, List_1_get_Item_m97E4330A4B3A01D99AD2ACC1BAE011F8FD18C7BB_RuntimeMethod_var);
		V_0 = L_22;
		ButtonClickStatus_tD4248512DB59E16501C4DDB6EDC2C6DE04290470* L_23 = V_0;
		VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* L_24 = L_23->___m_Target;
		if (!L_24)
		{
			goto IL_00b3;
		}
	}
	{
		ButtonClickStatus_tD4248512DB59E16501C4DDB6EDC2C6DE04290470* L_25 = V_0;
		int64_t L_26 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(0,NULL));
		L_25->___m_LastPointerDownTime = L_26;
		return;
	}

IL_0082:
	{
		PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* L_27 = ___0_evt;
		int64_t L_28;
		L_28 = VirtualFuncInvoker0< int64_t >::Invoke(5, (EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C*)L_27);
		CHECKED_LOCAL_INIT(EventBase_1_tB8AF17419B0E6B39E32F8925FCA535F5CF5C79D2_StaticInit,(EventBase_1_tB8AF17419B0E6B39E32F8925FCA535F5CF5C79D2_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		int64_t L_29;
		L_29 = EventBase_1_TypeId_m1A2720F11AB0D624809678ADE4E6D8C250E43741_inline(EventBase_1_TypeId_m1A2720F11AB0D624809678ADE4E6D8C250E43741_RuntimeMethod_var);
		if ((!(((uint64_t)L_28) == ((uint64_t)L_29))))
		{
			goto IL_0097;
		}
	}
	{
		PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* L_30 = ___0_evt;
		ClickDetector_CancelClickTracking_m93EDACD3A68DC09BD1DB229F9D390466E25E4339(__this, (EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C*)L_30, NULL);
		return;
	}

IL_0097:
	{
		PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* L_31 = ___0_evt;
		int64_t L_32;
		L_32 = VirtualFuncInvoker0< int64_t >::Invoke(5, (EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C*)L_31);
		CHECKED_LOCAL_INIT(EventBase_1_t2637736704939772A86A2904BCFD1B314097DD1E_StaticInit,(EventBase_1_t2637736704939772A86A2904BCFD1B314097DD1E_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		int64_t L_33;
		L_33 = EventBase_1_TypeId_mA90FE9E21D00125CFC53652D23DB65FD2574D60D_inline(EventBase_1_TypeId_mA90FE9E21D00125CFC53652D23DB65FD2574D60D_RuntimeMethod_var);
		if ((!(((uint64_t)L_32) == ((uint64_t)L_33))))
		{
			goto IL_00b3;
		}
	}
	{
		PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* L_34 = ___0_evt;
		int32_t L_35;
		L_35 = PointerEventBase_1_get_button_mE854A473F8446E8E956AB6229868A8642BE30D10_inline(L_34, NULL);
		if (L_35)
		{
			goto IL_00b3;
		}
	}
	{
		PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* L_36 = ___0_evt;
		ClickDetector_SendClickEvent_m3AC03E996C28D79AAB47D49BEA01887AFF21B310(__this, (EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C*)L_36, NULL);
	}

IL_00b3:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CollectionExtensions_GetValueOrDefault_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_m64F62F848FC640FEA0A7176567655B4C1E6783AF_fshared (RuntimeObject* ___0_dictionary, Il2CppFullySharedGenericAny ___1_key, Il2CppFullySharedGenericAny ___2_defaultValue, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_TValue_tEBA10F9D90EB194DB01999A57278A952FF03DA0F = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 3));
	const uint32_t SizeOf_TKey_tA3F6A7E1955A82041CECC138C619409DAB9C8BC3 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 0));
	const Il2CppFullySharedGenericAny L_3 = alloca(SizeOf_TKey_tA3F6A7E1955A82041CECC138C619409DAB9C8BC3);
	const Il2CppFullySharedGenericAny L_5 = alloca(SizeOf_TValue_tEBA10F9D90EB194DB01999A57278A952FF03DA0F);
	const Il2CppFullySharedGenericAny L_6 = L_5;
	Il2CppFullySharedGenericAny V_0 = alloca(SizeOf_TValue_tEBA10F9D90EB194DB01999A57278A952FF03DA0F);
	memset(V_0, 0, SizeOf_TValue_tEBA10F9D90EB194DB01999A57278A952FF03DA0F);
	{
		RuntimeObject* L_0 = ___0_dictionary;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_1 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralC0E02A0440A6BB4475B7E59901C37A6A25E773C8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, method);
	}

IL_000e:
	{
		RuntimeObject* L_2 = ___0_dictionary;
		il2cpp_codegen_memcpy(L_3, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 0)) ? ___1_key : &___1_key), SizeOf_TKey_tA3F6A7E1955A82041CECC138C619409DAB9C8BC3);
		bool L_4;
		L_4 = InterfaceFuncInvoker2Invoker< bool, Il2CppFullySharedGenericAny, Il2CppFullySharedGenericAny* >::Invoke(0, il2cpp_rgctx_data_init(method->rgctx_data, 1), L_2, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 0)) ? L_3: *(void**)L_3), (Il2CppFullySharedGenericAny*)V_0);
		if (L_4)
		{
			goto IL_001b;
		}
	}
	{
		il2cpp_codegen_memcpy(L_5, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 3)) ? ___2_defaultValue : &___2_defaultValue), SizeOf_TValue_tEBA10F9D90EB194DB01999A57278A952FF03DA0F);
		il2cpp_codegen_memcpy(il2cppRetVal, L_5, SizeOf_TValue_tEBA10F9D90EB194DB01999A57278A952FF03DA0F);
		return;
	}

IL_001b:
	{
		il2cpp_codegen_memcpy(L_6, V_0, SizeOf_TValue_tEBA10F9D90EB194DB01999A57278A952FF03DA0F);
		il2cpp_codegen_memcpy(il2cppRetVal, L_6, SizeOf_TValue_tEBA10F9D90EB194DB01999A57278A952FF03DA0F);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CollectionExtensions_AddToArray_TisIl2CppFullySharedGenericAny_mF12E142C387346A53DC32D69AE9C778B9E2C4081_fshared (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** ___0_array, Il2CppFullySharedGenericAny ___1_item, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_T_tD4E2EADC414204690F099BB51F5C155469AB24B4 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_8 = alloca(SizeOf_T_tD4E2EADC414204690F099BB51F5C155469AB24B4);
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** L_0 = ___0_array;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** L_1 = ___0_array;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_2 = *(L_1);
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_2)->max_length),NULL));
		Array_Resize_TisIl2CppFullySharedGenericAny_m263FC41C8DB989397C43C86556D63CEBE13F4712(L_0, ((int32_t)il2cpp_codegen_add(L_3, 1)), il2cpp_rgctx_method(method->rgctx_data, 0));
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** L_4 = ___0_array;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_5 = *(L_4);
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_6 = L_5;
		int32_t L_7 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_6)->max_length),NULL));
		il2cpp_codegen_memcpy(L_8, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_item : &___1_item), SizeOf_T_tD4E2EADC414204690F099BB51F5C155469AB24B4);
		il2cpp_codegen_memcpy((L_6)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(((int32_t)il2cpp_codegen_subtract(L_7, 1)))), L_8, SizeOf_T_tD4E2EADC414204690F099BB51F5C155469AB24B4);
		Il2CppCodeGenWriteBarrierForClass(il2cpp_rgctx_data(method->rgctx_data, 1), (void**)(L_6)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(((int32_t)il2cpp_codegen_subtract(L_7, 1)))), (void*)L_8);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CollectionExtensions_InsertIntoArray_TisIl2CppFullySharedGenericAny_m2561D707EF5FB2781D3884BC5277C6919EBF0E03_fshared (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** ___0_array, int32_t ___1_index, Il2CppFullySharedGenericAny ___2_item, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_T_tB6DB49F6B5E7886BCA7E9ACEB5DB521B7417633D = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_27 = alloca(SizeOf_T_tB6DB49F6B5E7886BCA7E9ACEB5DB521B7417633D);
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* V_0 = NULL;
	{
		int32_t L_0 = ___1_index;
		if ((((int32_t)L_0) < ((int32_t)0)))
		{
			goto IL_000b;
		}
	}
	{
		int32_t L_1 = ___1_index;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** L_2 = ___0_array;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = *(L_2);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((((int32_t)L_1) <= ((int32_t)L_4)))
		{
			goto IL_0016;
		}
	}

IL_000b:
	{
		IndexOutOfRangeException_t7ECB35264FB6CA8FAA516BD958F4B2ADC78E8A82* L_5 = (IndexOutOfRangeException_t7ECB35264FB6CA8FAA516BD958F4B2ADC78E8A82*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&IndexOutOfRangeException_t7ECB35264FB6CA8FAA516BD958F4B2ADC78E8A82_il2cpp_TypeInfo_var)));
		IndexOutOfRangeException__ctor_mFD06819F05B815BE2D6E826D4E04F4C449D0A425(L_5, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralFA7B055D415896E5E76FFEE1D010B524A9717B00)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_5, method);
	}

IL_0016:
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** L_6 = ___0_array;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_7 = *(L_6);
		V_0 = L_7;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** L_8 = ___0_array;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_9 = V_0;
		int32_t L_10 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_9)->max_length),NULL));
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_11 = (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*)(__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*)SZArrayNew(il2cpp_rgctx_data(method->rgctx_data, 0), (uint32_t)((int32_t)il2cpp_codegen_add(L_10, 1)));
		*(((RuntimeObject**)L_8)) = ((RuntimeObject*)L_11);
		Il2CppCodeGenWriteBarrier((void**)((RuntimeObject**)L_8), (void*)((RuntimeObject*)L_11));
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_12 = V_0;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** L_13 = ___0_array;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_14 = *(L_13);
		int32_t L_15 = ___1_index;
		Array_Copy_m4233828B4E6288B6D815F539AAA38575DE627900((RuntimeArray*)L_12, (RuntimeArray*)L_14, L_15, NULL);
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_16 = V_0;
		int32_t L_17 = ___1_index;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** L_18 = ___0_array;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_19 = *(L_18);
		int32_t L_20 = ___1_index;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_21 = V_0;
		int32_t L_22 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_21)->max_length),NULL));
		int32_t L_23 = ___1_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_16, L_17, (RuntimeArray*)L_19, ((int32_t)il2cpp_codegen_add(L_20, 1)), ((int32_t)il2cpp_codegen_subtract(L_22, L_23)), NULL);
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** L_24 = ___0_array;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_25 = *(L_24);
		int32_t L_26 = ___1_index;
		il2cpp_codegen_memcpy(L_27, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___2_item : &___2_item), SizeOf_T_tB6DB49F6B5E7886BCA7E9ACEB5DB521B7417633D);
		il2cpp_codegen_memcpy((L_25)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_26)), L_27, SizeOf_T_tB6DB49F6B5E7886BCA7E9ACEB5DB521B7417633D);
		Il2CppCodeGenWriteBarrierForClass(il2cpp_rgctx_data(method->rgctx_data, 1), (void**)(L_25)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_26)), (void*)L_27);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CollectionExtensions_RemoveFromArray_TisIl2CppFullySharedGenericAny_mCFA9A6539A3D96EBDE15348460A3C3BA4A1208FB_fshared (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** ___0_array, int32_t ___1_index, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_T_tE1363F2E699BA7EB69B7F8252BAD3C26131CDC17 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 0));
	const Il2CppFullySharedGenericAny L_16 = alloca(SizeOf_T_tE1363F2E699BA7EB69B7F8252BAD3C26131CDC17);
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		int32_t L_0 = ___1_index;
		if ((((int32_t)L_0) < ((int32_t)0)))
		{
			goto IL_000b;
		}
	}
	{
		int32_t L_1 = ___1_index;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** L_2 = ___0_array;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = *(L_2);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((((int32_t)L_1) < ((int32_t)L_4)))
		{
			goto IL_0016;
		}
	}

IL_000b:
	{
		ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F* L_5 = (ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F_il2cpp_TypeInfo_var)));
		ArgumentOutOfRangeException__ctor_mBC1D5DEEA1BA41DE77228CB27D6BAFEB6DCCBF4A(L_5, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral2B6D6F48C27C60C3B55391AB377D9DC8F5639AA1)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_5, method);
	}

IL_0016:
	{
		V_0 = 0;
		V_1 = 0;
		goto IL_0038;
	}

IL_001c:
	{
		int32_t L_6 = V_0;
		int32_t L_7 = ___1_index;
		if ((((int32_t)L_6) == ((int32_t)L_7)))
		{
			goto IL_0034;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** L_8 = ___0_array;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_9 = *(L_8);
		int32_t L_10 = V_1;
		int32_t L_11 = L_10;
		V_1 = ((int32_t)il2cpp_codegen_add(L_11, 1));
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** L_12 = ___0_array;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_13 = *(L_12);
		int32_t L_14 = V_0;
		int32_t L_15 = L_14;
		il2cpp_codegen_memcpy(L_16, (L_13)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_15)), SizeOf_T_tE1363F2E699BA7EB69B7F8252BAD3C26131CDC17);
		il2cpp_codegen_memcpy((L_9)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_11)), L_16, SizeOf_T_tE1363F2E699BA7EB69B7F8252BAD3C26131CDC17);
		Il2CppCodeGenWriteBarrierForClass(il2cpp_rgctx_data(method->rgctx_data, 0), (void**)(L_9)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_11)), (void*)L_16);
	}

IL_0034:
	{
		int32_t L_17 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_17, 1));
	}

IL_0038:
	{
		int32_t L_18 = V_0;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** L_19 = ___0_array;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_20 = *(L_19);
		int32_t L_21 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_20)->max_length),NULL));
		if ((((int32_t)L_18) < ((int32_t)L_21)))
		{
			goto IL_001c;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** L_22 = ___0_array;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC** L_23 = ___0_array;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_24 = *(L_23);
		int32_t L_25 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_24)->max_length),NULL));
		Array_Resize_TisIl2CppFullySharedGenericAny_m263FC41C8DB989397C43C86556D63CEBE13F4712(L_22, ((int32_t)il2cpp_codegen_subtract(L_25, 1)), il2cpp_rgctx_method(method->rgctx_data, 1));
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* CollectionExtensions_SerializedView_TisIl2CppFullySharedGenericAny_mCE17D8BD9D3423A2464201AAA28CF0A2216727ED_fshared (RuntimeObject* ___0_collection, Func_2_t3793651739D8B94095DFFF75C3BD348E5E2C455C* ___1_serializeElement, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5BEFD8CC60A79699B5BB00E37BAC5B62D371E174);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralD9691C4FD8A1F6B09DB1147CA32B442772FB46A1);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE166C9564FBDE461738077E3B1B506525EB6ACCC);
		il2cpp_rgctx_method_init(method);
	}
	const uint32_t SizeOf_T_tED2414E4D6A57FB111D6DDEAF1FABAC038A169C0 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 4));
	const Il2CppFullySharedGenericAny L_12 = alloca(SizeOf_T_tED2414E4D6A57FB111D6DDEAF1FABAC038A169C0);
	const Il2CppFullySharedGenericAny L_19 = L_12;
	const Il2CppFullySharedGenericAny L_13 = alloca(SizeOf_T_tED2414E4D6A57FB111D6DDEAF1FABAC038A169C0);
	StringBuilder_t* V_0 = NULL;
	RuntimeObject* V_1 = NULL;
	Il2CppFullySharedGenericAny V_2 = alloca(SizeOf_T_tED2414E4D6A57FB111D6DDEAF1FABAC038A169C0);
	memset(V_2, 0, SizeOf_T_tED2414E4D6A57FB111D6DDEAF1FABAC038A169C0);
	int32_t V_3 = 0;
	{
		RuntimeObject* L_0 = ___0_collection;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_1 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralDDFD5BA0EE5BD56B383E144892054DCB0A59D3ED)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, method);
	}

IL_000e:
	{
		Func_2_t3793651739D8B94095DFFF75C3BD348E5E2C455C* L_2 = ___1_serializeElement;
		if (L_2)
		{
			goto IL_001c;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_3 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_3, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECC4BC659FB1526108BFFD6AD5E7F03E47806740)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, method);
	}

IL_001c:
	{
		StringBuilder_t* L_4 = (StringBuilder_t*)il2cpp_codegen_object_new(StringBuilder_t_il2cpp_TypeInfo_var);
		StringBuilder__ctor_m1D99713357DE05DAFA296633639DB55F8C30587D(L_4, NULL);
		V_0 = L_4;
		StringBuilder_t* L_5 = V_0;
		StringBuilder_t* L_6;
		L_6 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_5, _stringLiteralD9691C4FD8A1F6B09DB1147CA32B442772FB46A1, NULL);
		RuntimeObject* L_7 = ___0_collection;
		RuntimeObject* L_8;
		L_8 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(method->rgctx_data, 0), L_7);
		V_1 = L_8;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0075:
			{
				{
					RuntimeObject* L_9 = V_1;
					if (!L_9)
					{
						goto IL_007e;
					}
				}
				{
					RuntimeObject* L_10 = V_1;
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_10);
				}

IL_007e:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_006b_1;
			}

IL_0037_1:
			{
				RuntimeObject* L_11 = V_1;
				InterfaceActionInvoker1Invoker< Il2CppFullySharedGenericAny* >::Invoke(0, il2cpp_rgctx_data_init(method->rgctx_data, 2), L_11, (Il2CppFullySharedGenericAny*)L_12);
				il2cpp_codegen_memcpy(V_2, L_12, SizeOf_T_tED2414E4D6A57FB111D6DDEAF1FABAC038A169C0);
				il2cpp_codegen_memcpy(L_13, V_2, SizeOf_T_tED2414E4D6A57FB111D6DDEAF1FABAC038A169C0);
				bool L_14 = il2cpp_codegen_would_box_to_non_null(il2cpp_rgctx_data(method->rgctx_data, 4), L_13);
				if (L_14)
				{
					goto IL_0054_1;
				}
			}
			{
				StringBuilder_t* L_15 = V_0;
				StringBuilder_t* L_16;
				L_16 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_15, _stringLiteral5BEFD8CC60A79699B5BB00E37BAC5B62D371E174, NULL);
				goto IL_0062_1;
			}

IL_0054_1:
			{
				StringBuilder_t* L_17 = V_0;
				Func_2_t3793651739D8B94095DFFF75C3BD348E5E2C455C* L_18 = ___1_serializeElement;
				il2cpp_codegen_memcpy(L_19, V_2, SizeOf_T_tED2414E4D6A57FB111D6DDEAF1FABAC038A169C0);
				String_t* L_20;
				L_20 = Func_2_Invoke_mDA85FF37C571381390F5F42887FD3EF66434548F(L_18, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 4)) ? L_19: *(void**)L_19), NULL);
				StringBuilder_t* L_21;
				L_21 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_17, L_20, NULL);
			}

IL_0062_1:
			{
				StringBuilder_t* L_22 = V_0;
				StringBuilder_t* L_23;
				L_23 = StringBuilder_Append_m71228B30F05724CD2CD96D9611DCD61BFB96A6E1(L_22, ((int32_t)44), NULL);
			}

IL_006b_1:
			{
				RuntimeObject* L_24 = V_1;
				bool L_25;
				L_25 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_24);
				if (L_25)
				{
					goto IL_0037_1;
				}
			}
			{
				goto IL_007f;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_007f:
	{
		StringBuilder_t* L_26 = V_0;
		int32_t L_27;
		L_27 = StringBuilder_get_Length_mDEA041E7357C68CC3B5885276BB403676DAAE0D8(L_26, NULL);
		if ((((int32_t)L_27) <= ((int32_t)1)))
		{
			goto IL_0098;
		}
	}
	{
		StringBuilder_t* L_28 = V_0;
		StringBuilder_t* L_29 = L_28;
		int32_t L_30;
		L_30 = StringBuilder_get_Length_mDEA041E7357C68CC3B5885276BB403676DAAE0D8(L_29, NULL);
		V_3 = L_30;
		int32_t L_31 = V_3;
		StringBuilder_set_Length_mE2427BDAEF91C4E4A6C80F3BDF1F6E01DBCC2414(L_29, ((int32_t)il2cpp_codegen_subtract(L_31, 1)), NULL);
	}

IL_0098:
	{
		StringBuilder_t* L_32 = V_0;
		StringBuilder_t* L_33;
		L_33 = StringBuilder_Append_m08904D74E0C78E5F36DCD9C9303BDD07886D9F7D(L_32, _stringLiteralE166C9564FBDE461738077E3B1B506525EB6ACCC, NULL);
		StringBuilder_t* L_34 = V_0;
		String_t* L_35;
		L_35 = VirtualFuncInvoker0< String_t* >::Invoke(3, L_34);
		return L_35;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 CollectionHelper_CreateNativeArray_TisIl2CppFullySharedGenericStruct_m4005EA9C29BA1075D9F591B6CBD82B5D22255763_fshared (int32_t ___0_length, AllocatorHandle_t3CA09720B1F89F91A8DDBA95E74C28A1EC3E3148 ___1_allocator, int32_t ___2_options, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AllocatorManager_tFB15A22029C8159A3DCD4C08935BE57D3E6B3C2C_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(AllocatorManager_tFB15A22029C8159A3DCD4C08935BE57D3E6B3C2C_StaticInit);
	NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		AllocatorHandle_t3CA09720B1F89F91A8DDBA95E74C28A1EC3E3148 L_0 = ___1_allocator;
		CHECKED_LOCAL_INIT(AllocatorManager_tFB15A22029C8159A3DCD4C08935BE57D3E6B3C2C_StaticInit,(AllocatorManager_tFB15A22029C8159A3DCD4C08935BE57D3E6B3C2C_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = AllocatorManager_IsCustomAllocator_m38BCD079BAB0D64962201CD05D671C2A42CE1909(L_0, NULL);
		if (L_1)
		{
			goto IL_001a;
		}
	}
	{
		int32_t L_2 = ___0_length;
		int32_t L_3;
		L_3 = AllocatorHandle_get_ToAllocator_m34C297958A940D9D35C11B3D28B8E5A08E170FDF((&___1_allocator), NULL);
		int32_t L_4 = ___2_options;
		NativeArray_1__ctor_m788DE0F85C4051DDF092DDF96484DE655ACFB6F1((&V_0), L_2, L_3, L_4, il2cpp_rgctx_method(method->rgctx_data, 0));
		goto IL_002c;
	}

IL_001a:
	{
		il2cpp_codegen_initobj((&V_0), sizeof(NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18));
		int32_t L_5 = ___0_length;
		AllocatorHandle_t3CA09720B1F89F91A8DDBA95E74C28A1EC3E3148 L_6 = ___1_allocator;
		int32_t L_7 = ___2_options;
		NativeArrayExtensions_Initialize_TisIl2CppFullySharedGenericStruct_m10ABF0E8955AE4504761E521587E2D89638A1972((&V_0), L_5, L_6, L_7, il2cpp_rgctx_method(method->rgctx_data, 1));
	}

IL_002c:
	{
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_8 = V_0;
		return L_8;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CollectionMarshallingAccessorsAsserts_AssertElementTypePunning_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_mF13D7CAF0665F0E73E26B2ADEDFE66120210FD52_fshared (const RuntimeMethod* method) 
{
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CommandBuffer_SetBufferData_TisIl2CppFullySharedGenericStruct_m46409F218741C0D24E21228312B5DA8446D174A8_fshared (CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* __this, ComputeBuffer_t51EADA9015EBCC1B982C5584E9AB2734415A8233* ___0_buffer, NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 ___1_data, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		ComputeBuffer_t51EADA9015EBCC1B982C5584E9AB2734415A8233* L_0 = ___0_buffer;
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_1 = ___1_data;
		void* L_2;
		L_2 = NativeArrayUnsafeUtility_GetUnsafeReadOnlyPtr_TisIl2CppFullySharedGenericStruct_m13C618FD69BBAB7D8C77632BF8A9116FCD17D234_inline(L_1, NULL);
		intptr_t L_3;
		L_3 = IntPtr_op_Explicit_mE2CEC14C61FD5E2159A03EA2AD97F5CDC5BB9F4D(L_2, NULL);
		int32_t L_4;
		L_4 = NativeArray_1_get_Length_mBE5CC8B844994CFC4AB434235F915881575E63C8_inline((&___1_data), NULL);
		int32_t L_5;
		L_5 = UnsafeUtility_SizeOf_TisIl2CppFullySharedGenericStruct_mB6FBF9DDC79FEC02EC8705E9E527E1CD495564BF_inline(il2cpp_rgctx_method(method->rgctx_data, 2));
		CommandBuffer_InternalSetComputeBufferNativeData_m8AD3F2597301A0E6A444F36909BEC29667A08B11(__this, L_0, L_3, 0, 0, L_4, L_5, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Component_GetComponent_TisIl2CppFullySharedGenericAny_m47CBDD147982125387F078ABBFDAAB92D397A6C2_fshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_ValueTuple_2_t84990CFAF672655D6B68E571698CDDC7C3BE608B = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 3));
	const uint32_t SizeOf_T_t1E55920ACC166F48B3917FD084904933E8AFE7F1 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	CHECKED_LOCAL(Type_t_StaticInit);
	const Il2CppFullySharedGenericAny L_4 = alloca(SizeOf_T_t1E55920ACC166F48B3917FD084904933E8AFE7F1);
	const Il2CppFullySharedGenericAny L_5 = alloca(SizeOf_T_t1E55920ACC166F48B3917FD084904933E8AFE7F1);
	const Il2CppFullySharedGenericAny L_7 = alloca(SizeOf_T_t1E55920ACC166F48B3917FD084904933E8AFE7F1);
	Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* V_0 = NULL;
	ValueTuple_2_t7064AD711EB2A320F9C65C116B07AF16D42C8CCB V_1 = alloca(SizeOf_ValueTuple_2_t84990CFAF672655D6B68E571698CDDC7C3BE608B);
	memset(V_1, 0, SizeOf_ValueTuple_2_t84990CFAF672655D6B68E571698CDDC7C3BE608B);
	Il2CppFullySharedGenericAny V_2 = alloca(SizeOf_T_t1E55920ACC166F48B3917FD084904933E8AFE7F1);
	memset(V_2, 0, SizeOf_T_t1E55920ACC166F48B3917FD084904933E8AFE7F1);
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* L_2;
		L_2 = Component_GetComponent_m4352437B839B9601EB007CAC1007C8E1FB8A427D(__this, L_1, NULL);
		V_0 = L_2;
		Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* L_3 = V_0;
		il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_2, SizeOf_T_t1E55920ACC166F48B3917FD084904933E8AFE7F1);
		il2cpp_codegen_memcpy(L_4, V_2, SizeOf_T_t1E55920ACC166F48B3917FD084904933E8AFE7F1);
		ValueTuple_2__ctor_m9C5289686C3B53268DC508F5480E6FB8006655F8((ValueTuple_2_t7064AD711EB2A320F9C65C116B07AF16D42C8CCB*)V_1, L_3, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? il2cpp_codegen_memcpy(L_5, L_4, SizeOf_T_t1E55920ACC166F48B3917FD084904933E8AFE7F1): *(void**)L_4), il2cpp_rgctx_method(method->rgctx_data, 2));
		Il2CppFullySharedGenericAny* L_6;
		L_6 = UnsafeUtility_As_TisComponent_t39FBE53E5EFCF4409111FB22C15FF73717632EC3_TisIl2CppFullySharedGenericAny_mEC3987CEFE5C29E2385827850E163034278AF68D_inline((((Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3**)il2cpp_codegen_get_field_data_pointer((ValueTuple_2_t7064AD711EB2A320F9C65C116B07AF16D42C8CCB*)V_1, il2cpp_rgctx_offset(method->rgctx_data, 4)))), NULL);
		il2cpp_codegen_memcpy(L_7, L_6, SizeOf_T_t1E55920ACC166F48B3917FD084904933E8AFE7F1);
		il2cpp_codegen_memcpy(il2cppRetVal, L_7, SizeOf_T_t1E55920ACC166F48B3917FD084904933E8AFE7F1);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Component_GetComponentInChildren_TisIl2CppFullySharedGenericAny_m6C912B287F81A629FB1D697E7CEB80D3B940295F_fshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(Type_t_StaticInit);
	const uint32_t SizeOf_T_t15C649661EFEF4630AEAB738C610F68EA352E1B2 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_3 = alloca(SizeOf_T_t15C649661EFEF4630AEAB738C610F68EA352E1B2);
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* L_2;
		L_2 = Component_GetComponentInChildren_m4050899166730E8F6629B895597CF4ECC894B597(__this, L_1, (bool)0, NULL);
		void* L_4 = UnBox_Any(L_2, il2cpp_rgctx_data(method->rgctx_data, 1), L_3);
		il2cpp_codegen_memcpy(il2cppRetVal, (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_4)), SizeOf_T_t15C649661EFEF4630AEAB738C610F68EA352E1B2);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Component_GetComponentInParent_TisIl2CppFullySharedGenericAny_mC623E57DF1C1113E52B35DF8F5130A698B7174A5_fshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(Type_t_StaticInit);
	const uint32_t SizeOf_T_t2A6C94BDAF110BD8403F6F2865F59F93528931C5 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_3 = alloca(SizeOf_T_t2A6C94BDAF110BD8403F6F2865F59F93528931C5);
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* L_2;
		L_2 = Component_GetComponentInParent_mA402B7F87D23E039BB045630E5EA3AC020D8A7C2(__this, L_1, (bool)0, NULL);
		void* L_4 = UnBox_Any(L_2, il2cpp_rgctx_data(method->rgctx_data, 1), L_3);
		il2cpp_codegen_memcpy(il2cppRetVal, (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_4)), SizeOf_T_t2A6C94BDAF110BD8403F6F2865F59F93528931C5);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Component_GetComponentInParent_TisIl2CppFullySharedGenericAny_m55153568FE86E91299227DEBCC2C6362600ACC5D_fshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, bool ___0_includeInactive, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(Type_t_StaticInit);
	const uint32_t SizeOf_T_t86F1C6CA88E5318FD608FD5FDDF3E736EBA5E516 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_4 = alloca(SizeOf_T_t86F1C6CA88E5318FD608FD5FDDF3E736EBA5E516);
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		bool L_2 = ___0_includeInactive;
		Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* L_3;
		L_3 = Component_GetComponentInParent_mA402B7F87D23E039BB045630E5EA3AC020D8A7C2(__this, L_1, L_2, NULL);
		void* L_5 = UnBox_Any(L_3, il2cpp_rgctx_data(method->rgctx_data, 1), L_4);
		il2cpp_codegen_memcpy(il2cppRetVal, (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_5)), SizeOf_T_t86F1C6CA88E5318FD608FD5FDDF3E736EBA5E516);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* Component_GetComponents_TisIl2CppFullySharedGenericAny_mD0F0A24D99D5E839646E6037D7E2DE05A9E1B67D_fshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_0;
		L_0 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_1;
		L_1 = GameObject_GetComponents_TisIl2CppFullySharedGenericAny_mE91E75D8E2C639EF49A5401C23EEFF029D54EDF0(L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_1;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Component_GetComponents_TisIl2CppFullySharedGenericAny_m301FE8944E539A9FD40BC5DE3EB37D66F6BB65A0_fshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* ___0_results, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(Type_t_StaticInit);
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_2 = ___0_results;
		List_1_t584CB490C8F4C21E0A0D5545409ED60BF71F3FE4* L_3;
		L_3 = UnsafeUtility_As_TisList_1_t584CB490C8F4C21E0A0D5545409ED60BF71F3FE4_mBBB120960A8F2691FFD08D375D14EBC37D7E8D38((RuntimeObject*)L_2, NULL);
		Component_GetComponentsForListInternal_m7B3A61AF88E5E6F845817EC28E22FD39944EFBC7(__this, L_1, L_3, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* Component_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_m173581815FF84722B49A5E4DE5A2820B5D2DB00E_fshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0;
		L_0 = Component_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mB27BBCE995DC86EC5D047AE9D0804238DA9B4E28(__this, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_0;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* Component_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mB27BBCE995DC86EC5D047AE9D0804238DA9B4E28_fshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, bool ___0_includeInactive, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_0;
		L_0 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
		bool L_1 = ___0_includeInactive;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_2;
		L_2 = GameObject_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mE619FF82D1099A3B6ADAD0FF12F3A05E06C32C24(L_0, L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Component_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mE4F8A731B0B2A6293CDAC8B19B0BA2A275089BF0_fshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* ___0_results, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_0 = ___0_results;
		Component_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mB3B314FA2B0FB49AB1B2E190A9FBC8337638078B(__this, (bool)0, L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Component_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mB3B314FA2B0FB49AB1B2E190A9FBC8337638078B_fshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, bool ___0_includeInactive, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* ___1_result, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_0;
		L_0 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
		bool L_1 = ___0_includeInactive;
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_2 = ___1_result;
		GameObject_GetComponentsInChildren_TisIl2CppFullySharedGenericAny_mD80CAA2E88B01D4173A65A4C4C20DCCF713ADF62(L_0, L_1, L_2, il2cpp_rgctx_method(method->rgctx_data, 0));
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* Component_GetComponentsInParent_TisIl2CppFullySharedGenericAny_m7DBB34BAB9422139D82BE02CA44558059A74136E_fshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0;
		L_0 = Component_GetComponentsInParent_TisIl2CppFullySharedGenericAny_m66D0CCEEF66E1B1598B57995FD87FC3CEEE1CA5C(__this, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_0;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* Component_GetComponentsInParent_TisIl2CppFullySharedGenericAny_m66D0CCEEF66E1B1598B57995FD87FC3CEEE1CA5C_fshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, bool ___0_includeInactive, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_0;
		L_0 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
		bool L_1 = ___0_includeInactive;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_2;
		L_2 = GameObject_GetComponentsInParent_TisIl2CppFullySharedGenericAny_m9BA598F4C76BF91ED59BD5DA257501EE1E9F6FBF(L_0, L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Component_GetComponentsInParent_TisIl2CppFullySharedGenericAny_mA29FCD472D98F35A351009B48ACA71BBE902C28A_fshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, bool ___0_includeInactive, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* ___1_results, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_0;
		L_0 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
		bool L_1 = ___0_includeInactive;
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_2 = ___1_results;
		GameObject_GetComponentsInParent_TisIl2CppFullySharedGenericAny_mD31B5889D11FC1B81FB10F6C35A2A6683ACB8921(L_0, L_1, L_2, il2cpp_rgctx_method(method->rgctx_data, 0));
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Component_TryGetComponent_TisIl2CppFullySharedGenericAny_m754E9486E0B3F9C50B4261F1F2088D02098E214B_fshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, Il2CppFullySharedGenericAny* ___0_component, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_0;
		L_0 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
		Il2CppFullySharedGenericAny* L_1 = ___0_component;
		bool L_2;
		L_2 = GameObject_TryGetComponent_TisIl2CppFullySharedGenericAny_m8FE39C82005602F4FEA27DC201600DABE2481BD1(L_0, L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ComputeBuffer_SetData_TisIl2CppFullySharedGenericStruct_m5985488AD855563F05D8D1B941067D3627A37C07_fshared (ComputeBuffer_t51EADA9015EBCC1B982C5584E9AB2734415A8233* __this, NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 ___0_data, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_0 = ___0_data;
		void* L_1;
		L_1 = NativeArrayUnsafeUtility_GetUnsafeReadOnlyPtr_TisIl2CppFullySharedGenericStruct_m13C618FD69BBAB7D8C77632BF8A9116FCD17D234_inline(L_0, NULL);
		intptr_t L_2;
		L_2 = IntPtr_op_Explicit_mE2CEC14C61FD5E2159A03EA2AD97F5CDC5BB9F4D(L_1, NULL);
		int32_t L_3;
		L_3 = NativeArray_1_get_Length_mBE5CC8B844994CFC4AB434235F915881575E63C8_inline((&___0_data), NULL);
		int32_t L_4;
		L_4 = UnsafeUtility_SizeOf_TisIl2CppFullySharedGenericStruct_mB6FBF9DDC79FEC02EC8705E9E527E1CD495564BF_inline(il2cpp_rgctx_method(method->rgctx_data, 2));
		ComputeBuffer_InternalSetNativeData_m77690E79461BD938F0BA7718FD51FF9B931893AC(__this, L_2, 0, 0, L_3, L_4, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ComputeBuffer_SetData_TisIl2CppFullySharedGenericStruct_m30ACAF6178277D4CA7E8E233A2A07F3CD93009AD_fshared (ComputeBuffer_t51EADA9015EBCC1B982C5584E9AB2734415A8233* __this, NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 ___0_data, int32_t ___1_nativeBufferStartIndex, int32_t ___2_computeBufferStartIndex, int32_t ___3_count, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		int32_t L_0 = ___1_nativeBufferStartIndex;
		if ((((int32_t)L_0) < ((int32_t)0)))
		{
			goto IL_001a;
		}
	}
	{
		int32_t L_1 = ___2_computeBufferStartIndex;
		if ((((int32_t)L_1) < ((int32_t)0)))
		{
			goto IL_001a;
		}
	}
	{
		int32_t L_2 = ___3_count;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_001a;
		}
	}
	{
		int32_t L_3 = ___1_nativeBufferStartIndex;
		int32_t L_4 = ___3_count;
		int32_t L_5;
		L_5 = NativeArray_1_get_Length_mBE5CC8B844994CFC4AB434235F915881575E63C8_inline((&___0_data), NULL);
		if ((((int32_t)((int32_t)il2cpp_codegen_add(L_3, L_4))) <= ((int32_t)L_5)))
		{
			goto IL_003d;
		}
	}

IL_001a:
	{
		int32_t L_6 = ___1_nativeBufferStartIndex;
		int32_t L_7 = L_6;
		RuntimeObject* L_8 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_7);
		int32_t L_9 = ___2_computeBufferStartIndex;
		int32_t L_10 = L_9;
		RuntimeObject* L_11 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_10);
		int32_t L_12 = ___3_count;
		int32_t L_13 = L_12;
		RuntimeObject* L_14 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_13);
		String_t* L_15;
		L_15 = String_Format_mA0534D6E2AE4D67A6BD8D45B3321323930EB930C(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral8C80868ED807D30CD87E7BACE1DFC57B2C47C706)), L_8, L_11, L_14, NULL);
		ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F* L_16 = (ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F_il2cpp_TypeInfo_var)));
		ArgumentOutOfRangeException__ctor_mBC1D5DEEA1BA41DE77228CB27D6BAFEB6DCCBF4A(L_16, L_15, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_16, method);
	}

IL_003d:
	{
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_17 = ___0_data;
		void* L_18;
		L_18 = NativeArrayUnsafeUtility_GetUnsafeReadOnlyPtr_TisIl2CppFullySharedGenericStruct_m13C618FD69BBAB7D8C77632BF8A9116FCD17D234_inline(L_17, NULL);
		intptr_t L_19;
		L_19 = IntPtr_op_Explicit_mE2CEC14C61FD5E2159A03EA2AD97F5CDC5BB9F4D(L_18, NULL);
		int32_t L_20 = ___1_nativeBufferStartIndex;
		int32_t L_21 = ___2_computeBufferStartIndex;
		int32_t L_22 = ___3_count;
		int32_t L_23;
		L_23 = UnsafeUtility_SizeOf_TisIl2CppFullySharedGenericStruct_mB6FBF9DDC79FEC02EC8705E9E527E1CD495564BF_inline(il2cpp_rgctx_method(method->rgctx_data, 2));
		ComputeBuffer_InternalSetNativeData_m77690E79461BD938F0BA7718FD51FF9B931893AC(__this, L_19, L_20, L_21, L_22, L_23, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ComputedTransitionUtils_GetWrappingTransitionData_TisIl2CppFullySharedGenericStruct_m3D59D97119E9A355C3860F8AF08D28A12CBDAD73_fshared (ReadOnlySpan_1_tE8C37D9A05FCAB953169AFFE8A0ABCA809781E25 ___0_list, int32_t ___1_i, Il2CppFullySharedGenericStruct ___2_defaultValue, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_T_t0F60DEF90665FA20B2F0FAE00D2C868E9904D3BC = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 2));
	const Il2CppFullySharedGenericStruct L_4 = alloca(SizeOf_T_t0F60DEF90665FA20B2F0FAE00D2C868E9904D3BC);
	const Il2CppFullySharedGenericStruct L_5 = L_4;
	{
		int32_t L_0;
		L_0 = ReadOnlySpan_1_get_Length_m3CC53FCCDE299F21DEF7AB63EF3D378DAB954005_inline((&___0_list), NULL);
		if (!L_0)
		{
			goto IL_001f;
		}
	}
	{
		int32_t L_1 = ___1_i;
		int32_t L_2;
		L_2 = ReadOnlySpan_1_get_Length_m3CC53FCCDE299F21DEF7AB63EF3D378DAB954005_inline((&___0_list), NULL);
		Il2CppFullySharedGenericStruct* L_3;
		L_3 = ReadOnlySpan_1_get_Item_mAFFA21964234394982172838F35555A4D5681233_inline((&___0_list), ((int32_t)(L_1%L_2)), il2cpp_rgctx_method(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(L_4, L_3, SizeOf_T_t0F60DEF90665FA20B2F0FAE00D2C868E9904D3BC);
		il2cpp_codegen_memcpy(il2cppRetVal, L_4, SizeOf_T_t0F60DEF90665FA20B2F0FAE00D2C868E9904D3BC);
		return;
	}

IL_001f:
	{
		il2cpp_codegen_memcpy(L_5, ___2_defaultValue, SizeOf_T_t0F60DEF90665FA20B2F0FAE00D2C868E9904D3BC);
		il2cpp_codegen_memcpy(il2cppRetVal, L_5, SizeOf_T_t0F60DEF90665FA20B2F0FAE00D2C868E9904D3BC);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConcreteTypeVisitor_Unity_Properties_IPropertyBagVisitor_Visit_TisIl2CppFullySharedGenericAny_m19F74EB5C5C7148082EE83A969B1A3D6D7757F78_fshared (ConcreteTypeVisitor_tC41932325C8C25F9B634923360AE061521744BB2* __this, RuntimeObject* ___0_properties, Il2CppFullySharedGenericAny* ___1_container, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		Il2CppFullySharedGenericAny* L_0 = ___1_container;
		GenericVirtualActionInvoker1< Il2CppFullySharedGenericAny* >::Invoke(il2cpp_rgctx_method(method->rgctx_data, 0), __this, L_0);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConstantBuffer_Push_TisIl2CppFullySharedGenericStruct_m410C1DECE5B01B801B0FE2106CA1C114C394CFB3_fshared (CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* ___0_cmd, Il2CppFullySharedGenericStruct* ___1_data, ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___2_cs, int32_t ___3_shaderId, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		ConstantBufferSingleton_1_t800A1489DAFDDB639BB1BC6406F901FBDE4F0F69* L_0;
		L_0 = ConstantBufferSingleton_1_get_instance_m9BCF8F5E44B966B85584D73034BF4E880D86DF2E(il2cpp_rgctx_method(method->rgctx_data, 0));
		ConstantBufferSingleton_1_t800A1489DAFDDB639BB1BC6406F901FBDE4F0F69* L_1 = L_0;
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_2 = ___0_cmd;
		Il2CppFullySharedGenericStruct* L_3 = ___1_data;
		ConstantBuffer_1_UpdateData_m2B6875F2248E344B4C53051FAA9355CA582A8830((ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D*)L_1, L_2, L_3, il2cpp_rgctx_method(method->rgctx_data, 1));
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_4 = ___0_cmd;
		ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* L_5 = ___2_cs;
		int32_t L_6 = ___3_shaderId;
		ConstantBuffer_1_Set_mE00DA8AF81B1D6E70D952A4A92D6F211DCE9FC52((ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D*)L_1, L_4, L_5, L_6, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConstantBuffer_PushGlobal_TisIl2CppFullySharedGenericStruct_m06D331CCE4A4DB4C0F4CF1F994F973CCB2E8D5A5_fshared (CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* ___0_cmd, Il2CppFullySharedGenericStruct* ___1_data, int32_t ___2_shaderId, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		ConstantBufferSingleton_1_t800A1489DAFDDB639BB1BC6406F901FBDE4F0F69* L_0;
		L_0 = ConstantBufferSingleton_1_get_instance_m9BCF8F5E44B966B85584D73034BF4E880D86DF2E(il2cpp_rgctx_method(method->rgctx_data, 0));
		ConstantBufferSingleton_1_t800A1489DAFDDB639BB1BC6406F901FBDE4F0F69* L_1 = L_0;
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_2 = ___0_cmd;
		Il2CppFullySharedGenericStruct* L_3 = ___1_data;
		ConstantBuffer_1_UpdateData_m2B6875F2248E344B4C53051FAA9355CA582A8830((ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D*)L_1, L_2, L_3, il2cpp_rgctx_method(method->rgctx_data, 1));
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_4 = ___0_cmd;
		int32_t L_5 = ___2_shaderId;
		ConstantBuffer_1_SetGlobal_mFD13E51675FB9005C153098E16D6B047A4070BA2((ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D*)L_1, L_4, L_5, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConstantBuffer_Set_TisIl2CppFullySharedGenericStruct_mDF954C7BABF56E09C2BB2B98E4FE93B4485309E8_fshared (ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___0_cs, int32_t ___1_shaderId, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		ConstantBufferSingleton_1_t800A1489DAFDDB639BB1BC6406F901FBDE4F0F69* L_0;
		L_0 = ConstantBufferSingleton_1_get_instance_m9BCF8F5E44B966B85584D73034BF4E880D86DF2E(il2cpp_rgctx_method(method->rgctx_data, 0));
		ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* L_1 = ___0_cs;
		int32_t L_2 = ___1_shaderId;
		ConstantBuffer_1_Set_m95B690077F35AFDA92F058EE5E32BE2911264A28((ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D*)L_0, L_1, L_2, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConstantBuffer_UpdateData_TisIl2CppFullySharedGenericStruct_mF289BD4A7472627DF5CBD72B968AF5E03158A50B_fshared (CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* ___0_cmd, Il2CppFullySharedGenericStruct* ___1_data, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		ConstantBufferSingleton_1_t800A1489DAFDDB639BB1BC6406F901FBDE4F0F69* L_0;
		L_0 = ConstantBufferSingleton_1_get_instance_m9BCF8F5E44B966B85584D73034BF4E880D86DF2E(il2cpp_rgctx_method(method->rgctx_data, 0));
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_1 = ___0_cmd;
		Il2CppFullySharedGenericStruct* L_2 = ___1_data;
		ConstantBuffer_1_UpdateData_m2B6875F2248E344B4C53051FAA9355CA582A8830((ConstantBuffer_1_tB96B3B060D1CA32A75A2FEDDCE02E9CA4BD3A56D*)L_0, L_1, L_2, il2cpp_rgctx_method(method->rgctx_data, 1));
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* ContextContainer_Create_TisIl2CppSharedGenericObject_m9AFC663D1EEB71C380F7CA8C06A42ED9D51D41A0_gshared (ContextContainer_t384E08F63FABEFF6A7F1A753F5EF02699DAD414C* __this, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(TypeId_1_tC53306B0453C6B251B85875C65702ADDCB35362A_StaticInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	uint32_t V_0 = 0;
	{
		CHECKED_LOCAL_INIT(TypeId_1_tC53306B0453C6B251B85875C65702ADDCB35362A_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		uint32_t L_0 = ((TypeId_1_tE871E3D998839394119381084FAB516D78C53198_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___value;
		V_0 = L_0;
		uint32_t L_1 = V_0;
		bool L_2;
		L_2 = ContextContainer_Contains_mD38FBF0FAC84169E395802CBF880BD0980490AF3_inline(__this, L_1, NULL);
		if (!L_2)
		{
			goto IL_0033;
		}
	}
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_3 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 1)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_4;
		L_4 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_3, NULL);
		String_t* L_5;
		L_5 = VirtualFuncInvoker0< String_t* >::Invoke(25, L_4);
		String_t* L_6;
		L_6 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral49AE794C00022ECA141068DEA9531BF6E0D342B7)), L_5, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral19185977762F1D370D03E76921A267F6A5FA9F81)), NULL);
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_7 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_7, L_6, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_7, method);
	}

IL_0033:
	{
		uint32_t L_8 = V_0;
		Il2CppSharedGenericObject* L_9;
		L_9 = ContextContainer_CreateAndGetData_TisIl2CppSharedGenericObject_m669524DBBE31F1D7ECBD4D5F583CE48EE0AD938C(__this, L_8, il2cpp_rgctx_method(method->rgctx_data, 3));
		return L_9;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* ContextContainer_CreateAndGetData_TisIl2CppSharedGenericObject_m669524DBBE31F1D7ECBD4D5F583CE48EE0AD938C_gshared (ContextContainer_t384E08F63FABEFF6A7F1A753F5EF02699DAD414C* __this, uint32_t ___0_typeId, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ContextContainer_t384E08F63FABEFF6A7F1A753F5EF02699DAD414C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Add_m084F52E7C436446A23080F15F803212491693F6B_RuntimeMethod_var);
		il2cpp_rgctx_method_init(method);
	}
	ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB* V_0 = NULL;
	int32_t V_1 = 0;
	ContextItem_tFC5ECB349F0410923AE6BC2A9CAACA3589B51086** V_2 = NULL;
	Item_tFA847F868B1ECD768A489638A61F7BC398DECDAC* G_B7_0 = NULL;
	Item_tFA847F868B1ECD768A489638A61F7BC398DECDAC* G_B6_0 = NULL;
	{
		ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB* L_0 = __this->___m_Items;
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		int64_t L_2 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_1,NULL));
		uint32_t L_3 = ___0_typeId;
		uint64_t L_4 = (il2cpp_codegen_conv<uint64_t,uint32_t,int32_t,false,false>(L_3,NULL));
		if ((((int64_t)L_2) > ((int64_t)L_4)))
		{
			goto IL_005c;
		}
	}
	{
		uint32_t L_5 = ((ContextContainer_t384E08F63FABEFF6A7F1A753F5EF02699DAD414C_StaticFields*)il2cpp_codegen_static_fields_for(ContextContainer_t384E08F63FABEFF6A7F1A753F5EF02699DAD414C_il2cpp_TypeInfo_var))->___s_TypeCount;
		uint32_t L_6;
		L_6 = math_ceilpow2_m55F757254B1339A2FAC288807A06BFF24898B214_inline(L_5, NULL);
		uint64_t L_7 = (il2cpp_codegen_conv<uint64_t,uint32_t,int32_t,false,false>(L_6,NULL));
		ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB* L_8 = __this->___m_Items;
		int32_t L_9 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_8)->max_length),NULL));
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(((int32_t)il2cpp_codegen_multiply(L_9, 2)),NULL));
		int64_t L_11;
		L_11 = math_max_mC58900240F2A88F08AD67CA1F3F4360987100521_inline((int64_t)L_7, L_10, NULL);
		intptr_t L_12 = (il2cpp_codegen_conv<intptr_t,int64_t,int64_t,true,false>(L_11,method));
		ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB* L_13 = (ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB*)(ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB*)SZArrayNew(ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB_il2cpp_TypeInfo_var, (uint32_t)L_12);
		V_0 = L_13;
		V_1 = 0;
		goto IL_004a;
	}

IL_0033:
	{
		ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB* L_14 = V_0;
		int32_t L_15 = V_1;
		ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB* L_16 = __this->___m_Items;
		int32_t L_17 = V_1;
		int32_t L_18 = L_17;
		Item_tFA847F868B1ECD768A489638A61F7BC398DECDAC L_19 = (L_16)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_18));
		(L_14)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(L_15), (Item_tFA847F868B1ECD768A489638A61F7BC398DECDAC)L_19);
		int32_t L_20 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_20, 1));
	}

IL_004a:
	{
		int32_t L_21 = V_1;
		ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB* L_22 = __this->___m_Items;
		int32_t L_23 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_22)->max_length),NULL));
		if ((((int32_t)L_21) < ((int32_t)L_23)))
		{
			goto IL_0033;
		}
	}
	{
		ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB* L_24 = V_0;
		__this->___m_Items = L_24;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_Items), (void*)L_24);
	}

IL_005c:
	{
		List_1_t9B68833848E4C4D7F623C05F6B77F0449396354A* L_25 = __this->___m_ActiveItemIndices;
		uint32_t L_26 = ___0_typeId;
		List_1_Add_m084F52E7C436446A23080F15F803212491693F6B_inline(L_25, L_26, List_1_Add_m084F52E7C436446A23080F15F803212491693F6B_RuntimeMethod_var);
		ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB* L_27 = __this->___m_Items;
		uint32_t L_28 = ___0_typeId;
		Item_tFA847F868B1ECD768A489638A61F7BC398DECDAC* L_29 = ((L_27)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_28)));
		ContextItem_tFC5ECB349F0410923AE6BC2A9CAACA3589B51086** L_30 = (ContextItem_tFC5ECB349F0410923AE6BC2A9CAACA3589B51086**)(&L_29->___storage);
		V_2 = L_30;
		ContextItem_tFC5ECB349F0410923AE6BC2A9CAACA3589B51086** L_31 = V_2;
		ContextItem_tFC5ECB349F0410923AE6BC2A9CAACA3589B51086* L_32 = *(L_31);
		if (L_32)
		{
			G_B7_0 = L_29;
			goto IL_008b;
		}
		G_B6_0 = L_29;
	}
	{
		ContextItem_tFC5ECB349F0410923AE6BC2A9CAACA3589B51086** L_33 = V_2;
		Il2CppSharedGenericObject* L_34;
		L_34 = Activator_CreateInstance_TisIl2CppSharedGenericObject_m4C74D3D65600820EB977E87AD08D24AF782C97C3(il2cpp_rgctx_method(method->rgctx_data, 0));
		*(((RuntimeObject**)L_33)) = ((RuntimeObject*)L_34);
		Il2CppCodeGenWriteBarrier((void**)((RuntimeObject**)L_33), (void*)((RuntimeObject*)L_34));
		G_B7_0 = G_B6_0;
	}

IL_008b:
	{
		Item_tFA847F868B1ECD768A489638A61F7BC398DECDAC* L_35 = G_B7_0;
		L_35->___isSet = (bool)1;
		ContextItem_tFC5ECB349F0410923AE6BC2A9CAACA3589B51086* L_36 = L_35->___storage;
		return ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_36, il2cpp_rgctx_data(method->rgctx_data, 1)));
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* ContextContainer_Get_TisIl2CppSharedGenericObject_m472009A0481035623C255E04E1A36993FC1A969F_gshared (ContextContainer_t384E08F63FABEFF6A7F1A753F5EF02699DAD414C* __this, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(TypeId_1_tE99A11CD2F4A1B1025AFA102960151D11345C907_StaticInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	uint32_t V_0 = 0;
	{
		CHECKED_LOCAL_INIT(TypeId_1_tE99A11CD2F4A1B1025AFA102960151D11345C907_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		uint32_t L_0 = ((TypeId_1_tE871E3D998839394119381084FAB516D78C53198_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___value;
		V_0 = L_0;
		uint32_t L_1 = V_0;
		bool L_2;
		L_2 = ContextContainer_Contains_mD38FBF0FAC84169E395802CBF880BD0980490AF3_inline(__this, L_1, NULL);
		if (L_2)
		{
			goto IL_0033;
		}
	}
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_3 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 1)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_4;
		L_4 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_3, NULL);
		String_t* L_5;
		L_5 = VirtualFuncInvoker0< String_t* >::Invoke(25, L_4);
		String_t* L_6;
		L_6 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral49AE794C00022ECA141068DEA9531BF6E0D342B7)), L_5, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral01A849374C26F36DBF4641EFCBB31ABDCEFF10D2)), NULL);
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_7 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_7, L_6, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_7, method);
	}

IL_0033:
	{
		ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB* L_8 = __this->___m_Items;
		uint32_t L_9 = V_0;
		ContextItem_tFC5ECB349F0410923AE6BC2A9CAACA3589B51086* L_10 = ((L_8)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_9)))->___storage;
		return ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_10, il2cpp_rgctx_data(method->rgctx_data, 2)));
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Contract_ForAll_TisIl2CppFullySharedGenericAny_mD4ED4CE84A1EDEA6818FA153D47F1E1B359208EC_fshared (RuntimeObject* ___0_collection, Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* ___1_predicate, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	const uint32_t SizeOf_T_tDF5CEEC973B77C280606399E2F2DE3DE92F1E18C = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 4));
	const Il2CppFullySharedGenericAny L_9 = alloca(SizeOf_T_tDF5CEEC973B77C280606399E2F2DE3DE92F1E18C);
	const Il2CppFullySharedGenericAny L_11 = alloca(SizeOf_T_tDF5CEEC973B77C280606399E2F2DE3DE92F1E18C);
	RuntimeObject* V_0 = NULL;
	Il2CppFullySharedGenericAny V_1 = alloca(SizeOf_T_tDF5CEEC973B77C280606399E2F2DE3DE92F1E18C);
	memset(V_1, 0, SizeOf_T_tDF5CEEC973B77C280606399E2F2DE3DE92F1E18C);
	bool V_2 = false;
	{
		RuntimeObject* L_0 = ___0_collection;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_1 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral469F05BE9BB4C7903C353D0EB9F6384C84A48B25)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, method);
	}

IL_000e:
	{
		Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* L_2 = ___1_predicate;
		if (L_2)
		{
			goto IL_001c;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_3 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_3, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral7EE837B2FC81E79F9F96BEFD9CD8B64870F5C628)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, method);
	}

IL_001c:
	{
		RuntimeObject* L_4 = ___0_collection;
		RuntimeObject* L_5;
		L_5 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(method->rgctx_data, 0), L_4);
		V_0 = L_5;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0043:
			{
				{
					RuntimeObject* L_6 = V_0;
					if (!L_6)
					{
						goto IL_004c;
					}
				}
				{
					RuntimeObject* L_7 = V_0;
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_7);
				}

IL_004c:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_0039_1;
			}

IL_0025_1:
			{
				RuntimeObject* L_8 = V_0;
				InterfaceActionInvoker1Invoker< Il2CppFullySharedGenericAny* >::Invoke(0, il2cpp_rgctx_data_init(method->rgctx_data, 2), L_8, (Il2CppFullySharedGenericAny*)L_9);
				il2cpp_codegen_memcpy(V_1, L_9, SizeOf_T_tDF5CEEC973B77C280606399E2F2DE3DE92F1E18C);
				Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* L_10 = ___1_predicate;
				il2cpp_codegen_memcpy(L_11, V_1, SizeOf_T_tDF5CEEC973B77C280606399E2F2DE3DE92F1E18C);
				bool L_12;
				L_12 = Predicate_1_Invoke_m6F76B5644E2D6BD882F23B65434545AD90EFE5A1(L_10, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 4)) ? L_11: *(void**)L_11), NULL);
				if (L_12)
				{
					goto IL_0039_1;
				}
			}
			{
				V_2 = (bool)0;
				goto IL_004f;
			}

IL_0039_1:
			{
				RuntimeObject* L_13 = V_0;
				bool L_14;
				L_14 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_13);
				if (L_14)
				{
					goto IL_0025_1;
				}
			}
			{
				goto IL_004d;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_004d:
	{
		return (bool)1;
	}

IL_004f:
	{
		bool L_15 = V_2;
		return L_15;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ConverterGroup_TryConvert_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_mAC70483BC70EBAD3C45D5C78D3A0D2626A9D4D8C_fshared (ConverterGroup_t2D7DEDAC22662EF5C020DE54231DC23CD0B5268A* __this, Il2CppFullySharedGenericAny* ___0_source, Il2CppFullySharedGenericAny* ___1_destination, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(Type_t_StaticInit);
	const uint32_t SizeOf_TDestination_tA083D70AA9C86C7FE3635A90495FD59C446D06F1 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 3));
	const Il2CppFullySharedGenericAny L_11 = alloca(SizeOf_TDestination_tA083D70AA9C86C7FE3635A90495FD59C446D06F1);
	Delegate_t* V_0 = NULL;
	TypeConverter_2_tA6276E0BDB8DB9E3B10AF0C31301ACC071589845* V_1 = NULL;
	{
		ConversionRegistry_tBB92E13DAE967B31A341845C05EC88A2603E6426* L_0 = (ConversionRegistry_tBB92E13DAE967B31A341845C05EC88A2603E6426*)(&__this->___m_Registry);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_1 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_2;
		L_2 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_1, NULL);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_3 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 2)) };
		Type_t* L_4;
		L_4 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_3, NULL);
		bool L_5;
		L_5 = ConversionRegistry_TryGetConverter_mA5C88244D7C991C10AE5940EA5829BC20B3781CD(L_0, L_2, L_4, (&V_0), NULL);
		if (!L_5)
		{
			goto IL_003c;
		}
	}
	{
		Delegate_t* L_6 = V_0;
		V_1 = ((TypeConverter_2_tA6276E0BDB8DB9E3B10AF0C31301ACC071589845*)IsInst((RuntimeObject*)L_6, il2cpp_rgctx_data(method->rgctx_data, 4)));
		TypeConverter_2_tA6276E0BDB8DB9E3B10AF0C31301ACC071589845* L_7 = V_1;
		if (!L_7)
		{
			goto IL_003c;
		}
	}
	{
		Il2CppFullySharedGenericAny* L_8 = ___1_destination;
		TypeConverter_2_tA6276E0BDB8DB9E3B10AF0C31301ACC071589845* L_9 = V_1;
		Il2CppFullySharedGenericAny* L_10 = ___0_source;
		TypeConverter_2_Invoke_m22213A88AF14CB4CFBD0CCA1547E1476B01580F9(L_9, L_10, (Il2CppFullySharedGenericAny*)L_11, NULL);
		il2cpp_codegen_memcpy((Il2CppFullySharedGenericAny*)L_8, L_11, SizeOf_TDestination_tA083D70AA9C86C7FE3635A90495FD59C446D06F1);
		Il2CppCodeGenWriteBarrierForClass(il2cpp_rgctx_data(method->rgctx_data, 3), (void**)(Il2CppFullySharedGenericAny*)L_8, (void*)L_11);
		return (bool)1;
	}

IL_003c:
	{
		Il2CppFullySharedGenericAny* L_12 = ___1_destination;
		il2cpp_codegen_initobj(L_12, SizeOf_TDestination_tA083D70AA9C86C7FE3635A90495FD59C446D06F1);
		return (bool)0;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ConverterGroup_TrySetValue_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_mEF4F57AFA15359AEB6E776211C80DA4E899BD20F_fshared (ConverterGroup_t2D7DEDAC22662EF5C020DE54231DC23CD0B5268A* __this, Il2CppFullySharedGenericAny* ___0_container, PropertyPath_tA523CA2740853534DF6C009C588464B45A6D0A79* ___1_path, Il2CppFullySharedGenericAny ___2_value, int32_t* ___3_returnCode, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(SetValueVisitor_1_t4DFE8E96EEAB4A42CD955D18010F8C45352DC20A_StaticInit);
	const uint32_t SizeOf_TValue_t907947127E1238CACA4529CAF8ECDC0CD6E4B7E5 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 3));
	const Il2CppFullySharedGenericAny L_10 = alloca(SizeOf_TValue_t907947127E1238CACA4529CAF8ECDC0CD6E4B7E5);
	SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660* V_0 = NULL;
	VisitParameters_tF21AC3343F90A7925EEA69AA73269B7F2DCE66CE V_1;
	memset((&V_1), 0, sizeof(V_1));
	bool V_2 = false;
	{
		PropertyPath_tA523CA2740853534DF6C009C588464B45A6D0A79* L_0 = ___1_path;
		bool L_1;
		L_1 = PropertyPath_get_IsEmpty_m5923CD28AB201F75C64F40AED27C7E42FC5DBF50(L_0, NULL);
		if (!L_1)
		{
			goto IL_000e;
		}
	}
	{
		int32_t* L_2 = ___3_returnCode;
		*(((int32_t*)L_2)) = 4;
		return (bool)0;
	}

IL_000e:
	{
		CHECKED_LOCAL_INIT(SetValueVisitor_1_t4DFE8E96EEAB4A42CD955D18010F8C45352DC20A_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		ObjectPool_1_t28F249C599ED2EFD04F6B973FEFA2541B52062DF* L_3 = ((SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Pool;
		SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660* L_4;
		L_4 = ObjectPool_1_Get_m3DAF7F2DB4FF067AB34CBB2573A626C6DE39AEA5_inline(L_3, il2cpp_rgctx_method(method->rgctx_data, 1));
		V_0 = L_4;
		SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660* L_5 = V_0;
		SetValueVisitor_1_set_group_m94980C75D99986FB92C00A15DA3C9C1E7DF4F175_inline(L_5, __this, il2cpp_rgctx_method(method->rgctx_data, 2));
		SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660* L_6 = V_0;
		PropertyPath_tA523CA2740853534DF6C009C588464B45A6D0A79* L_7 = ___1_path;
		PropertyPath_tA523CA2740853534DF6C009C588464B45A6D0A79 L_8 = (*(PropertyPath_tA523CA2740853534DF6C009C588464B45A6D0A79*)L_7);
		PathVisitor_set_Path_m8E3F004534B73D33C70C3BD20729F5987F11627B_inline((PathVisitor_tCD0947C41CDBDB774D22D560E5FFA01EF61C4446*)L_6, L_8, NULL);
		SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660* L_9 = V_0;
		il2cpp_codegen_memcpy(L_10, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 3)) ? ___2_value : &___2_value), SizeOf_TValue_t907947127E1238CACA4529CAF8ECDC0CD6E4B7E5);
		il2cpp_codegen_write_field_data<true>(L_9, il2cpp_rgctx_field(il2cpp_rgctx_data(method->rgctx_data, 0),1), il2cpp_rgctx_offset(method->rgctx_data, 4), L_10, SizeOf_TValue_t907947127E1238CACA4529CAF8ECDC0CD6E4B7E5);
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0056:
			{
				CHECKED_LOCAL_INIT(SetValueVisitor_1_t4DFE8E96EEAB4A42CD955D18010F8C45352DC20A_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
				ObjectPool_1_t28F249C599ED2EFD04F6B973FEFA2541B52062DF* L_11 = ((SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Pool;
				SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660* L_12 = V_0;
				ObjectPool_1_Release_m8A0B50EA8E475E234775D7C5979A56CD7AE4B7E9_inline(L_11, L_12, il2cpp_rgctx_method(method->rgctx_data, 6));
				return;
			}
		});
		try
		{
			{
				SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660* L_13 = V_0;
				Il2CppFullySharedGenericAny* L_14 = ___0_container;
				int32_t* L_15 = ___3_returnCode;
				il2cpp_codegen_initobj((&V_1), sizeof(VisitParameters_tF21AC3343F90A7925EEA69AA73269B7F2DCE66CE));
				VisitParameters_tF21AC3343F90A7925EEA69AA73269B7F2DCE66CE L_16 = V_1;
				bool L_17;
				L_17 = PropertyContainer_TryAccept_TisIl2CppFullySharedGenericAny_mDD1DF6BB0301B9008B88A30F25AC64BE2728F0D6((RuntimeObject*)L_13, L_14, L_15, L_16, il2cpp_rgctx_method(method->rgctx_data, 5));
				if (L_17)
				{
					goto IL_004b_1;
				}
			}
			{
				V_2 = (bool)0;
				goto IL_0069;
			}

IL_004b_1:
			{
				int32_t* L_18 = ___3_returnCode;
				SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660* L_19 = V_0;
				int32_t L_20;
				L_20 = PathVisitor_get_ReturnCode_m900403F09B0392B4D8C2E2E0EBD06148FCFA2734_inline((PathVisitor_tCD0947C41CDBDB774D22D560E5FFA01EF61C4446*)L_19, NULL);
				*(((int32_t*)L_18)) = ((int32_t)L_20);
				goto IL_0062;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0062:
	{
		int32_t* L_21 = ___3_returnCode;
		int32_t L_22 = *(((int32_t*)L_21));
		return (bool)((((int32_t)L_22) == ((int32_t)0))? 1 : 0);
	}

IL_0069:
	{
		bool L_23 = V_2;
		return L_23;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ConverterGroups_TryConvert_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_m29FA8B3E6CF2433C559347E366AE9A2607D8016D_fshared (Il2CppFullySharedGenericAny* ___0_source, Il2CppFullySharedGenericAny* ___1_destination, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ConverterGroups_tA746073C90C188444FFC6A55278138FEE2BBDF0E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TypeConversion_tC95A24AF93E22908A5717FBF24E5089C963863E9_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(ConverterGroups_tA746073C90C188444FFC6A55278138FEE2BBDF0E_StaticInit);
	CHECKED_LOCAL(TypeConversion_tC95A24AF93E22908A5717FBF24E5089C963863E9_StaticInit);
	{
		CHECKED_LOCAL_INIT(ConverterGroups_tA746073C90C188444FFC6A55278138FEE2BBDF0E_StaticInit,(ConverterGroups_tA746073C90C188444FFC6A55278138FEE2BBDF0E_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		ConverterGroup_t2D7DEDAC22662EF5C020DE54231DC23CD0B5268A* L_0 = ((ConverterGroups_tA746073C90C188444FFC6A55278138FEE2BBDF0E_StaticFields*)il2cpp_codegen_static_fields_for(ConverterGroups_tA746073C90C188444FFC6A55278138FEE2BBDF0E_il2cpp_TypeInfo_var))->___s_GlobalConverters;
		ConversionRegistry_tBB92E13DAE967B31A341845C05EC88A2603E6426* L_1;
		L_1 = ConverterGroup_get_registerRef_mF4AFC513DF255DDA084C020971EB187A5FFFD888(L_0, NULL);
		Il2CppFullySharedGenericAny* L_2 = ___0_source;
		Il2CppFullySharedGenericAny* L_3 = ___1_destination;
		CHECKED_LOCAL_INIT(TypeConversion_tC95A24AF93E22908A5717FBF24E5089C963863E9_StaticInit,(TypeConversion_tC95A24AF93E22908A5717FBF24E5089C963863E9_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_4;
		L_4 = TypeConversion_TryConvert_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_mC18DF87EC56E149F05CE5A957357981A256F363B(L_1, L_2, L_3, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_4;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CoreUtils_GetLastEnumValue_TisIl2CppFullySharedGenericStruct_m3490C90D457E7416799B447B84350B33C897702F_fshared (Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_StaticInit);
	const uint32_t SizeOf_T_t0E6189C577D62F8AC6CFF856A805C7B07F84E48D = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		CHECKED_LOCAL_INIT(Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_StaticInit,(Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		RuntimeArray* L_2;
		L_2 = Enum_GetValues_m803B9D68C367FAABC5AFB6B5B52775C8A573CEF9(L_1, NULL);
		RuntimeArray* L_3 = L_2;
		int32_t L_4;
		L_4 = il2cpp_codegen_array_get_length(L_3);
		RuntimeObject* L_5;
		L_5 = Array_GetValue_m007D247B8A6FE5BD60FD1CD510A714A416F2BA21(L_3, ((int32_t)il2cpp_codegen_subtract(L_4, 1)), NULL);
		il2cpp_codegen_memcpy(il2cppRetVal, (((Il2CppFullySharedGenericStruct)(Il2CppFullySharedGenericStruct*)UnBox(L_5, il2cpp_rgctx_data(method->rgctx_data, 1)))), SizeOf_T_t0E6189C577D62F8AC6CFF856A805C7B07F84E48D);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 CullingResults_GetNativeArray_TisIl2CppFullySharedGenericStruct_mAD95E79FFE908C81E8C8514A3F2E8E583998A7C0_fshared (CullingResults_tD6B7EF20B68D47DFF3A99EB2EA73F47F1D460267* __this, void* ___0_dataPointer, int32_t ___1_length, const RuntimeMethod* method) 
{
	{
		void* L_0 = ___0_dataPointer;
		int32_t L_1 = ___1_length;
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_2;
		L_2 = NativeArrayUnsafeUtility_ConvertExistingDataToNativeArray_TisIl2CppFullySharedGenericStruct_m6920C14D4E38FAB84BD2B5F148CE70DF7F224F52(L_0, L_1, 0, NULL);
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* CustomAttributeData_UnboxValues_TisIl2CppFullySharedGenericAny_m8A35F6AFA3152D955AC5F5DE44F805EA42773A8A_fshared (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___0_values, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_T_tFABA89096A3FC891574EE036D422A4D106829B20 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_9 = alloca(SizeOf_T_tFABA89096A3FC891574EE036D422A4D106829B20);
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* V_0 = NULL;
	int32_t V_1 = 0;
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___0_values;
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_2 = (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*)(__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*)SZArrayNew(il2cpp_rgctx_data(method->rgctx_data, 0), (uint32_t)L_1);
		V_0 = L_2;
		V_1 = 0;
		goto IL_0020;
	}

IL_000d:
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = V_0;
		int32_t L_4 = V_1;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_5 = ___0_values;
		int32_t L_6 = V_1;
		int32_t L_7 = L_6;
		RuntimeObject* L_8 = (L_5)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_7));
		void* L_10 = UnBox_Any(L_8, il2cpp_rgctx_data(method->rgctx_data, 1), L_9);
		il2cpp_codegen_memcpy((L_3)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_4)), (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_10)), SizeOf_T_tFABA89096A3FC891574EE036D422A4D106829B20);
		Il2CppCodeGenWriteBarrierForClass(il2cpp_rgctx_data(method->rgctx_data, 1), (void**)(L_3)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_4)), (void*)(((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_10)));
		int32_t L_11 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_11, 1));
	}

IL_0020:
	{
		int32_t L_12 = V_1;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_13 = ___0_values;
		int32_t L_14 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_13)->max_length),NULL));
		if ((((int32_t)L_12) < ((int32_t)L_14)))
		{
			goto IL_000d;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_15 = V_0;
		return L_15;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* CustomAttributeExtensions_GetCustomAttribute_TisIl2CppSharedGenericObject_m31BBABF66E9D94CB217F5E1D2E24A3E19261F4AC_gshared (MemberInfo_t* ___0_element, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(Type_t_StaticInit);
	{
		MemberInfo_t* L_0 = ___0_element;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_1 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_2;
		L_2 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_1, NULL);
		Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA* L_3;
		L_3 = CustomAttributeExtensions_GetCustomAttribute_m7241E02800DB70BF4B7FE0DD35A57C32196B4030(L_0, L_2, NULL);
		return ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_3, il2cpp_rgctx_data(method->rgctx_data, 1)));
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* CustomAttributeExtensions_GetCustomAttribute_TisIl2CppSharedGenericObject_mB9B01294EC960F55868C3509E659A07E683591C9_gshared (MemberInfo_t* ___0_element, bool ___1_inherit, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(Type_t_StaticInit);
	{
		MemberInfo_t* L_0 = ___0_element;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_1 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_2;
		L_2 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_1, NULL);
		bool L_3 = ___1_inherit;
		Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA* L_4;
		L_4 = CustomAttributeExtensions_GetCustomAttribute_m8451F6949BEA92A69AE673CD95D6AD767B66880A(L_0, L_2, L_3, NULL);
		return ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_4, il2cpp_rgctx_data(method->rgctx_data, 1)));
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* CustomAttributeExtensions_GetCustomAttributes_TisIl2CppSharedGenericObject_m625276DF58D5C427BB3FA21A965E2781AA254071_gshared (MemberInfo_t* ___0_element, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(Type_t_StaticInit);
	{
		MemberInfo_t* L_0 = ___0_element;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_1 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_2;
		L_2 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_1, NULL);
		RuntimeObject* L_3;
		L_3 = CustomAttributeExtensions_GetCustomAttributes_mA663A241432473A89C830284F82A775F47A76FDC(L_0, L_2, NULL);
		return ((RuntimeObject*)Castclass((RuntimeObject*)L_3, il2cpp_rgctx_data(method->rgctx_data, 2)));
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* CustomAttributeExtensions_GetCustomAttributes_TisIl2CppSharedGenericObject_m70B41F4B934119F4C60EF5835F720296B1404632_gshared (MemberInfo_t* ___0_element, bool ___1_inherit, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(Type_t_StaticInit);
	{
		MemberInfo_t* L_0 = ___0_element;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_1 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_2;
		L_2 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_1, NULL);
		bool L_3 = ___1_inherit;
		RuntimeObject* L_4;
		L_4 = CustomAttributeExtensions_GetCustomAttributes_m19F5D9C3B60FA00E4692C21EC0D59328FAF631AE(L_0, L_2, L_3, NULL);
		return ((RuntimeObject*)Castclass((RuntimeObject*)L_4, il2cpp_rgctx_data(method->rgctx_data, 2)));
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void IntPtr__ctor_m4F9A9B80F01996B610D5AE4797F20B98ECD0A3D9_inline (intptr_t* __this, void* ___0_value, const RuntimeMethod* method) 
{
	{
		void* L_0 = ___0_value;
		*__this = ((intptr_t)L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool ContextContainer_Contains_mD38FBF0FAC84169E395802CBF880BD0980490AF3_inline (ContextContainer_t384E08F63FABEFF6A7F1A753F5EF02699DAD414C* __this, uint32_t ___0_typeId, const RuntimeMethod* method) 
{
	{
		uint32_t L_0 = ___0_typeId;
		uint64_t L_1 = (il2cpp_codegen_conv<uint64_t,uint32_t,int32_t,false,false>(L_0,NULL));
		ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB* L_2 = __this->___m_Items;
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_2)->max_length),NULL));
		int64_t L_4 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_3,NULL));
		if ((((int64_t)L_1) >= ((int64_t)L_4)))
		{
			goto IL_001f;
		}
	}
	{
		ItemU5BU5D_t4AC8CDDFF9ED634B9ECE154F0C638C00B12B5CCB* L_5 = __this->___m_Items;
		uint32_t L_6 = ___0_typeId;
		bool L_7 = ((L_5)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_6)))->___isSet;
		return L_7;
	}

IL_001f:
	{
		return (bool)0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR uint32_t math_ceilpow2_m55F757254B1339A2FAC288807A06BFF24898B214_inline (uint32_t ___0_x, const RuntimeMethod* method) 
{
	{
		uint32_t L_0 = ___0_x;
		___0_x = ((int32_t)il2cpp_codegen_subtract((int32_t)L_0, 1));
		uint32_t L_1 = ___0_x;
		uint32_t L_2 = ___0_x;
		___0_x = ((int32_t)((int32_t)L_1|((int32_t)((uint32_t)L_2>>1))));
		uint32_t L_3 = ___0_x;
		uint32_t L_4 = ___0_x;
		___0_x = ((int32_t)((int32_t)L_3|((int32_t)((uint32_t)L_4>>2))));
		uint32_t L_5 = ___0_x;
		uint32_t L_6 = ___0_x;
		___0_x = ((int32_t)((int32_t)L_5|((int32_t)((uint32_t)L_6>>4))));
		uint32_t L_7 = ___0_x;
		uint32_t L_8 = ___0_x;
		___0_x = ((int32_t)((int32_t)L_7|((int32_t)((uint32_t)L_8>>8))));
		uint32_t L_9 = ___0_x;
		uint32_t L_10 = ___0_x;
		___0_x = ((int32_t)((int32_t)L_9|((int32_t)((uint32_t)L_10>>((int32_t)16)))));
		uint32_t L_11 = ___0_x;
		return ((int32_t)il2cpp_codegen_add((int32_t)L_11, 1));
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int64_t math_max_mC58900240F2A88F08AD67CA1F3F4360987100521_inline (int64_t ___0_x, int64_t ___1_y, const RuntimeMethod* method) 
{
	{
		int64_t L_0 = ___0_x;
		int64_t L_1 = ___1_y;
		if ((((int64_t)L_0) > ((int64_t)L_1)))
		{
			goto IL_0006;
		}
	}
	{
		int64_t L_2 = ___1_y;
		return L_2;
	}

IL_0006:
	{
		int64_t L_3 = ___0_x;
		return L_3;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PathVisitor_set_Path_m8E3F004534B73D33C70C3BD20729F5987F11627B_inline (PathVisitor_tCD0947C41CDBDB774D22D560E5FFA01EF61C4446* __this, PropertyPath_tA523CA2740853534DF6C009C588464B45A6D0A79 ___0_value, const RuntimeMethod* method) 
{
	{
		PropertyPath_tA523CA2740853534DF6C009C588464B45A6D0A79 L_0 = ___0_value;
		__this->___U3CPathU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)&((&(((&__this->___U3CPathU3Ek__BackingField))->___m_Part0))->___m_Name), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&(((&__this->___U3CPathU3Ek__BackingField))->___m_Part0))->___m_Key), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&(((&__this->___U3CPathU3Ek__BackingField))->___m_Part1))->___m_Name), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&(((&__this->___U3CPathU3Ek__BackingField))->___m_Part1))->___m_Key), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&(((&__this->___U3CPathU3Ek__BackingField))->___m_Part2))->___m_Name), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&(((&__this->___U3CPathU3Ek__BackingField))->___m_Part2))->___m_Key), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&(((&__this->___U3CPathU3Ek__BackingField))->___m_Part3))->___m_Name), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&(((&__this->___U3CPathU3Ek__BackingField))->___m_Part3))->___m_Key), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&(((&__this->___U3CPathU3Ek__BackingField))->___m_AdditionalParts), (void*)NULL);
		#endif
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t PathVisitor_get_ReturnCode_m900403F09B0392B4D8C2E2E0EBD06148FCFA2734_inline (PathVisitor_tCD0947C41CDBDB774D22D560E5FFA01EF61C4446* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___U3CReturnCodeU3Ek__BackingField;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void FunctionPointer_1__ctor_m539FE4A638EAB194C030CB25B52DBB1BADEA6E5A_fshared_inline (FunctionPointer_1_t8F9F55036C9757966AA570D1DB1FADC59D4C7547* __this, intptr_t ___0_ptr, const RuntimeMethod* method) 
{
	{
		intptr_t L_0 = ___0_ptr;
		__this->____ptr = L_0;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void CallbackEventHandler_AddListenersAndCategories_TisIl2CppSharedGenericObject_m7ACB30B716F504416F9FB1910C2CAF60F02E627F_gshared_inline (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* __this, Delegate_t* ___0_callback, int32_t ___1_callbackOptions, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		Delegate_t* L_0 = ___0_callback;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_1 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral7A0A3608A3C4012D35E5DE3B2BA646379CCF198F)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, method);
	}

IL_000e:
	{
		int32_t L_2 = ___1_callbackOptions;
		CallbackEventHandler_AddEventCategories_TisIl2CppSharedGenericObject_m48C6249122107B0206C2479BEA30DDCDEB81D426(__this, L_2, il2cpp_rgctx_method(method->rgctx_data, 0));
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void EventCallbackRegistry_RegisterCallback_TisIl2CppSharedGenericObject_m88FAF5D5C2F885FF0B1926FB47DD948259F8029B_gshared_inline (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* __this, EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* ___0_userCallback, int32_t ___1_callbackOptions, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_StaticInit);
	{
		CHECKED_LOCAL_INIT(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_StaticInit,(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		EventCallbackPool_t337A1A0692B33BCB6B3D2C662666D78276F9CD3C* L_0 = ((EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_StaticFields*)il2cpp_codegen_static_fields_for(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_il2cpp_TypeInfo_var))->___k_LocalCallbackPool;
		EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* L_1 = ___0_userCallback;
		int32_t L_2 = ___1_callbackOptions;
		EventCallbackInternal_t74403797F0007ACB72019EE51D2C4DC33E5F63AF* L_3;
		L_3 = EventCallbackPool_Get_TisIl2CppSharedGenericObject_mFF482DEA45910068B92C6850F72BFEA2DD7F5517(L_0, (Delegate_t*)L_1, 0, ((int32_t)((int32_t)L_2|((int32_t)16))), il2cpp_rgctx_method(method->rgctx_data, 0));
		EventCallbackRegistry__RegisterLocalCallback_m6A4C55AB7E0B0DF3E995EA6FFC1F2F19A91EA156(__this, L_3, NULL);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void EventCallbackRegistry_RegisterCallback_TisIl2CppSharedGenericObject_TisIl2CppFullySharedGenericAny_m4C03FF9793B46745C6FCB27E7C02C6816E3EFB1A_fshared_inline (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* __this, EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE* ___0_userCallback, CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* ___1_element, Il2CppFullySharedGenericAny ___2_userArgs, int32_t ___3_callbackOptions, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_StaticInit);
	{
		CHECKED_LOCAL_INIT(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_StaticInit,(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		EventCallbackPool_t337A1A0692B33BCB6B3D2C662666D78276F9CD3C* L_0 = ((EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_StaticFields*)il2cpp_codegen_static_fields_for(EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85_il2cpp_TypeInfo_var))->___k_LocalCallbackPool;
		EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE* L_1 = ___0_userCallback;
		ArgValueList_tF9404610631615CB400FD008E21A0CE700A1814C* L_2 = (ArgValueList_tF9404610631615CB400FD008E21A0CE700A1814C*)(&__this->___m_ArgValues);
		int32_t L_3;
		L_3 = ArgValueList_AcquireTemporary_TisIl2CppFullySharedGenericAny_m04BEDB27AA80C6FDF03E2790C8B975AAA5DD70FE(L_2, (Il2CppFullySharedGenericAny*)(il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 0)) ? ___2_userArgs : &___2_userArgs), il2cpp_rgctx_method(method->rgctx_data, 1));
		int32_t L_4 = ___3_callbackOptions;
		EventCallbackInternal_t74403797F0007ACB72019EE51D2C4DC33E5F63AF* L_5;
		L_5 = EventCallbackPool_Get_TisIl2CppSharedGenericObject_mFF482DEA45910068B92C6850F72BFEA2DD7F5517(L_0, (Delegate_t*)L_1, L_3, ((int32_t)((int32_t)L_4|((int32_t)16))), il2cpp_rgctx_method(method->rgctx_data, 2));
		EventCallbackRegistry__RegisterLocalCallback_m6A4C55AB7E0B0DF3E995EA6FFC1F2F19A91EA156(__this, L_5, NULL);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void CallbackEventHandler_RemoveListeners_TisIl2CppSharedGenericObject_m44964C63A8123B46E7D698F80D9205E7387A53B3_gshared_inline (CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4* __this, Delegate_t* ___0_callback, const RuntimeMethod* method) 
{
	{
		Delegate_t* L_0 = ___0_callback;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_1 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral7A0A3608A3C4012D35E5DE3B2BA646379CCF198F)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, method);
	}

IL_000e:
	{
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool EventCallbackRegistry_UnregisterCallback_TisIl2CppSharedGenericObject_m83DD8A054C7A025857863C137916EC36CC066AA6_gshared_inline (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* __this, EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* ___0_callback, int32_t ___1_callbackOptions, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EventBase_1_tB1C9B009295941ABC3A4EE55CAA627C7032300CB_StaticInit);
	{
		EventCallback_1_t5B1D52718EA71C8F33D11D62351463B137A59482* L_0 = ___0_callback;
		CHECKED_LOCAL_INIT(EventBase_1_tB1C9B009295941ABC3A4EE55CAA627C7032300CB_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 1)),il2cpp_codegen_runtime_class_init_inline);
		int64_t L_1;
		L_1 = EventBase_1_TypeId_m5B74C35CC6F5F2EC559BDD7A71544FD6FDE37CDE_inline(il2cpp_rgctx_method(method->rgctx_data, 0));
		int32_t L_2 = ___1_callbackOptions;
		bool L_3;
		L_3 = EventCallbackRegistry__UnregisterLocalCallback_m377FCD52E974865BACD644081B7B0B19071D8C0A(__this, (Delegate_t*)L_0, L_1, L_2, NULL);
		return L_3;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool EventCallbackRegistry_UnregisterCallback_TisIl2CppSharedGenericObject_TisIl2CppFullySharedGenericAny_mA1B34BFD9C5EE81778ED146A48FA639F9CE18948_fshared_inline (EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* __this, EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE* ___0_callback, int32_t ___1_callbackOptions, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EventBase_1_tF26CA202AE4B17297A16FC75515019ED4CD72147_StaticInit);
	{
		EventCallback_2_tC06EF2A4A70CBE1F08269EC0F2968422526F55BE* L_0 = ___0_callback;
		CHECKED_LOCAL_INIT(EventBase_1_tF26CA202AE4B17297A16FC75515019ED4CD72147_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 1)),il2cpp_codegen_runtime_class_init_inline);
		int64_t L_1;
		L_1 = EventBase_1_TypeId_m5B74C35CC6F5F2EC559BDD7A71544FD6FDE37CDE_inline(il2cpp_rgctx_method(method->rgctx_data, 0));
		int32_t L_2 = ___1_callbackOptions;
		bool L_3;
		L_3 = EventCallbackRegistry__UnregisterLocalCallback_m377FCD52E974865BACD644081B7B0B19071D8C0A(__this, (Delegate_t*)L_0, L_1, L_2, NULL);
		return L_3;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int64_t EventBase_1_TypeId_m5B74C35CC6F5F2EC559BDD7A71544FD6FDE37CDE_gshared_inline (const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(EventBase_1_t80C2B8C3086146FFDC6F4D5DC0BCD7E5BB211471_StaticInit);
	{
		CHECKED_LOCAL_INIT(EventBase_1_t80C2B8C3086146FFDC6F4D5DC0BCD7E5BB211471_StaticInit,(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)),il2cpp_codegen_runtime_class_init_inline);
		int64_t L_0 = ((EventBase_1_t880C2458D345D7530A8295FA56BDDF14D326DE8F_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_TypeId;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t PointerEventBase_1_get_button_mE854A473F8446E8E956AB6229868A8642BE30D10_gshared_inline (PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___U3CbuttonU3Ek__BackingField;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t PointerEventBase_1_get_pressedButtons_m4A6DBB316294BB3899BA348BCD895B7C69F7D1D9_gshared_inline (PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___U3CpressedButtonsU3Ek__BackingField;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t PointerEventBase_1_get_pointerId_m96350DDBEB7BE4407122043EEF6826D54520E58A_gshared_inline (PointerEventBase_1_tBEF58C01811AB35E9606A4E18E689AEAB4F17816* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___U3CpointerIdU3Ek__BackingField;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void* NativeArrayUnsafeUtility_GetUnsafeReadOnlyPtr_TisIl2CppFullySharedGenericStruct_m13C618FD69BBAB7D8C77632BF8A9116FCD17D234_fshared_inline (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 ___0_nativeArray, const RuntimeMethod* method) 
{
	{
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_0 = ___0_nativeArray;
		void* L_1 = L_0.___m_Buffer;
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t NativeArray_1_get_Length_mBE5CC8B844994CFC4AB434235F915881575E63C8_fshared_inline (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___m_Length;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t UnsafeUtility_SizeOf_TisIl2CppFullySharedGenericStruct_mB6FBF9DDC79FEC02EC8705E9E527E1CD495564BF_fshared_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		int32_t L_0;
		L_0 = UnsafeUtilityInternal_SizeOf_TisIl2CppFullySharedGenericStruct_m9FC95A845D303A9C72ED4044691DD435A70F994A_inline(il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Il2CppFullySharedGenericAny* UnsafeUtility_As_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_mD6BECC32C10552E82C8D23115CAF774983D95F0C_fshared_inline (Il2CppFullySharedGenericAny* ___0_from, const RuntimeMethod* method) 
{
	{
		Il2CppFullySharedGenericAny* L_0 = ___0_from;
		Il2CppFullySharedGenericAny* L_1;
		L_1 = UnsafeUtilityInternal_As_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_mE1CA751887466B801BE69083C2B0EA3EDE41FF9B_inline(L_0, NULL);
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_get_Length_m4430960CA0D0458B1A1106DD246CA9AB746B5DB2_fshared_inline (ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____length;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Il2CppFullySharedGenericAny* ReadOnlySpan_1_get_Item_m9143C9CF6493AF0AD667C5BDEEF1D22895283F77_fshared_inline (ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t120950580BDFD368223E024446019DB239080837 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
	ByReference_1_t607C1F3BC28B0E21B969461CDB0720FB01A82141 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t607C1F3BC28B0E21B969461CDB0720FB01A82141 L_2 = __this->____pointer;
		V_0 = L_2;
		Il2CppFullySharedGenericAny* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(Il2CppFullySharedGenericAny, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		Il2CppFullySharedGenericAny* L_5;
		L_5 = il2cpp_unsafe_add<Il2CppFullySharedGenericAny,int32_t>(L_3, L_4, SizeOf_T_t120950580BDFD368223E024446019DB239080837);
		return L_5;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mD4F3498FBD3BDD3F03CBCFB38041CBAC9C28CAFC_fshared_inline (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Il2CppFullySharedGenericAny ___0_item, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_9 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	const Il2CppFullySharedGenericAny L_10 = L_9;
	const Il2CppFullySharedGenericAny L_11 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_4 = V_0;
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_7 = V_0;
		int32_t L_8 = V_1;
		il2cpp_codegen_memcpy(L_9, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? ___0_item : &___0_item), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		il2cpp_codegen_memcpy((L_7)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_8)), L_9, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		Il2CppCodeGenWriteBarrierForClass(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), (void**)(L_7)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_8)), (void*)L_9);
		return;
	}

IL_0034:
	{
		il2cpp_codegen_memcpy(L_10, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? ___0_item : &___0_item), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		List_1_AddWithResize_mA6DFDBC2B22D6318212C6989A34784BD8303AF33(__this, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? il2cpp_codegen_memcpy(L_11, L_10, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E): *(void**)L_10), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 7));
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* ObjectPool_1_Get_m6033387D88EF9D1AD81A89F3045733BC4B98073D_gshared_inline (ObjectPool_1_tC6F0B8E0BAAEC07474AABA8496261838C79CE1F3* __this, const RuntimeMethod* method) 
{
	Il2CppSharedGenericObject* V_0 = NULL;
	int32_t V_1 = 0;
	int32_t V_2 = 0;
	Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* G_B7_0 = NULL;
	Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* G_B6_0 = NULL;
	{
		Il2CppSharedGenericObject* L_0 = __this->___m_FreshlyReleased;
		if (!L_0)
		{
			goto IL_0022;
		}
	}
	{
		Il2CppSharedGenericObject* L_1 = __this->___m_FreshlyReleased;
		V_0 = L_1;
		Il2CppSharedGenericObject** L_2 = (Il2CppSharedGenericObject**)(&__this->___m_FreshlyReleased);
		il2cpp_codegen_initobj(L_2, sizeof(Il2CppSharedGenericObject*));
		goto IL_0074;
	}

IL_0022:
	{
		List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* L_3 = __this->___m_List;
		int32_t L_4;
		L_4 = List_1_get_Count_m9832942FE310479E64D9D0D23F29C65305BE1622_inline(L_3, NULL);
		if (L_4)
		{
			goto IL_004d;
		}
	}
	{
		Func_1_t566D4A654882E282E54CDC3A001E05486F913903* L_5 = __this->___m_CreateFunc;
		Il2CppSharedGenericObject* L_6;
		L_6 = Func_1_Invoke_m201DDBB72CE38673C89C22034EC8779981D83E6D(L_5, NULL);
		V_0 = L_6;
		int32_t L_7;
		L_7 = ObjectPool_1_get_CountAll_m8372ACA3A976C3FCB45CD77A0AEF0BDA496EBB6D_inline(__this, NULL);
		V_1 = L_7;
		int32_t L_8 = V_1;
		ObjectPool_1_set_CountAll_m6E7C5E741DCB73BD743FC27BACBE1886F4414440_inline(__this, ((int32_t)il2cpp_codegen_add(L_8, 1)), NULL);
		goto IL_0074;
	}

IL_004d:
	{
		List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* L_9 = __this->___m_List;
		int32_t L_10;
		L_10 = List_1_get_Count_m9832942FE310479E64D9D0D23F29C65305BE1622_inline(L_9, NULL);
		V_2 = ((int32_t)il2cpp_codegen_subtract(L_10, 1));
		List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* L_11 = __this->___m_List;
		int32_t L_12 = V_2;
		Il2CppSharedGenericObject* L_13;
		L_13 = List_1_get_Item_m02336594E8F76D7313E2BCB7A3ED5ABD13B4D515(L_11, L_12, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 6));
		V_0 = L_13;
		List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* L_14 = __this->___m_List;
		int32_t L_15 = V_2;
		List_1_RemoveAt_m36F68EA689DB109415094085A496D5EA21E2F23C(L_14, L_15, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 7));
	}

IL_0074:
	{
		Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* L_16 = __this->___m_ActionOnGet;
		Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* L_17 = L_16;
		if (L_17)
		{
			G_B7_0 = L_17;
			goto IL_0080;
		}
		G_B6_0 = L_17;
	}
	{
		goto IL_0086;
	}

IL_0080:
	{
		Il2CppSharedGenericObject* L_18 = V_0;
		Action_1_Invoke_mDB8F7C7381C08B4DCE4E409D2FA4073BC17F90B5(G_B7_0, L_18, NULL);
	}

IL_0086:
	{
		Il2CppSharedGenericObject* L_19 = V_0;
		return L_19;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SetValueVisitor_1_set_group_m94980C75D99986FB92C00A15DA3C9C1E7DF4F175_fshared_inline (SetValueVisitor_1_t9F8375B4EA106BA863CE89AC21063AB486248660* __this, ConverterGroup_t2D7DEDAC22662EF5C020DE54231DC23CD0B5268A* ___0_value, const RuntimeMethod* method) 
{
	{
		ConverterGroup_t2D7DEDAC22662EF5C020DE54231DC23CD0B5268A* L_0 = ___0_value;
		il2cpp_codegen_write_field_data<ConverterGroup_t2D7DEDAC22662EF5C020DE54231DC23CD0B5268A*, true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0),2), il2cpp_rgctx_offset(il2cpp_codegen_method_rgctx(method), 1), L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ObjectPool_1_Release_m3183E8F3194884123A027D720D37F341AF367E48_gshared_inline (ObjectPool_1_tC6F0B8E0BAAEC07474AABA8496261838C79CE1F3* __this, Il2CppSharedGenericObject* ___0_element, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* G_B2_0 = NULL;
	Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* G_B1_0 = NULL;
	Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* G_B9_0 = NULL;
	Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* G_B8_0 = NULL;
	{
		Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* L_0 = __this->___m_ActionOnRelease;
		Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000c;
		}
		G_B1_0 = L_1;
	}
	{
		goto IL_0012;
	}

IL_000c:
	{
		Il2CppSharedGenericObject* L_2 = ___0_element;
		Action_1_Invoke_mDB8F7C7381C08B4DCE4E409D2FA4073BC17F90B5(G_B2_0, L_2, NULL);
	}

IL_0012:
	{
		Il2CppSharedGenericObject* L_3 = __this->___m_FreshlyReleased;
		if (L_3)
		{
			goto IL_0027;
		}
	}
	{
		Il2CppSharedGenericObject* L_4 = ___0_element;
		__this->___m_FreshlyReleased = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_FreshlyReleased), (void*)L_4);
		return;
	}

IL_0027:
	{
		int32_t L_5;
		L_5 = ObjectPool_1_get_CountInactive_m40351B6BE62E0944A0813C07869822EC0D1E8D26(__this, NULL);
		int32_t L_6 = __this->___m_MaxSize;
		if ((((int32_t)L_5) >= ((int32_t)L_6)))
		{
			goto IL_0042;
		}
	}
	{
		List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* L_7 = __this->___m_List;
		Il2CppSharedGenericObject* L_8 = ___0_element;
		List_1_Add_m0CE6E39234ED33F5195AABC8029D06D4756C75C1_inline(L_7, L_8, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		return;
	}

IL_0042:
	{
		int32_t L_9;
		L_9 = ObjectPool_1_get_CountAll_m8372ACA3A976C3FCB45CD77A0AEF0BDA496EBB6D_inline(__this, NULL);
		V_0 = L_9;
		int32_t L_10 = V_0;
		ObjectPool_1_set_CountAll_m6E7C5E741DCB73BD743FC27BACBE1886F4414440_inline(__this, ((int32_t)il2cpp_codegen_subtract(L_10, 1)), NULL);
		Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* L_11 = __this->___m_ActionOnDestroy;
		Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* L_12 = L_11;
		if (L_12)
		{
			G_B9_0 = L_12;
			goto IL_005d;
		}
		G_B8_0 = L_12;
	}
	{
		return;
	}

IL_005d:
	{
		Il2CppSharedGenericObject* L_13 = ___0_element;
		Action_1_Invoke_mDB8F7C7381C08B4DCE4E409D2FA4073BC17F90B5(G_B9_0, L_13, NULL);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t UnsafeUtilityInternal_SizeOf_TisIl2CppFullySharedGenericStruct_m9FC95A845D303A9C72ED4044691DD435A70F994A_fshared_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_T_t8EBE725B2740B3ECE9C2D5E9EBEB0B97172D5E18 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 0));
	{
		uint32_t L_0 = SizeOf_T_t8EBE725B2740B3ECE9C2D5E9EBEB0B97172D5E18;
		return (int32_t)L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Il2CppFullySharedGenericAny* UnsafeUtilityInternal_As_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_mE1CA751887466B801BE69083C2B0EA3EDE41FF9B_fshared_inline (Il2CppFullySharedGenericAny* ___0_from, const RuntimeMethod* method) 
{
	{
		Il2CppFullySharedGenericAny* L_0 = ___0_from;
		return (Il2CppFullySharedGenericAny*)(L_0);
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared_inline (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____size;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t ObjectPool_1_get_CountAll_m8372ACA3A976C3FCB45CD77A0AEF0BDA496EBB6D_gshared_inline (ObjectPool_1_tC6F0B8E0BAAEC07474AABA8496261838C79CE1F3* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___U3CCountAllU3Ek__BackingField;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ObjectPool_1_set_CountAll_m6E7C5E741DCB73BD743FC27BACBE1886F4414440_gshared_inline (ObjectPool_1_tC6F0B8E0BAAEC07474AABA8496261838C79CE1F3* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = ___0_value;
		__this->___U3CCountAllU3Ek__BackingField = L_0;
		return;
	}
}
