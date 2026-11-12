#include "pch-cpp.hpp"





template <typename T1, typename T2>
struct VirtualActionInvoker2
{
	typedef void (*Action)(void*,T1,T2,const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1, T2 p2)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj,p1,p2,invokeData.method);
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
template <typename R>
struct InterfaceFuncInvoker0Invoker
{
	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		R ret;
		invokeData.method->invoker_method(il2cpp_codegen_get_method_pointer(invokeData.method), invokeData.method, obj, NULL, &ret);
		return ret;
	}
};

struct ActionQueryMatcher_t0EF83980C6ABA9272F87A72C8B21FEF9403FA83E;
struct Action_1_t3DC3411926243F1DB9C330F8E105B904E38C1A0B;
typedef Il2CppFullySharedGenericStruct Data_t6F131ABB2DC3608FBC19EEE1D985484C670DE41A;
struct Dictionary_2_t6B4B40DA71BA0031F5736DF4459253299D8AF5F2;
struct Dictionary_2_tBCCCFBCAC02A3C03E3C84D75696D4860D7444A35;
struct Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588;
struct Dictionary_2_t6682243BDABA638FCBE4F1D9875B5B1455A7686E;
struct Dictionary_2_t4055F6540F36F21F9FEDAFB92D8E0089B38EBBC8;
struct HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87;
struct HashSet_1_t0F16759FF079A28DCA2DCF39BA6A61B5D502A475;
struct IEnumerator_1_t39504690A249AB0F407E265AD478DFF2C33744B5;
struct IEnumerator_1_tC2266E408683DCA4A8C20409F2A8BE21B265A725;
struct IEnumerator_1_t80E5984835398EC083520AB0DB51729D678DFF87;
struct IEnumerator_1_t45F3F7FD63BD81FEABDE4F646B59715C819D84FC;
struct IEqualityComparer_1_t47CC0B235E693652D181B679FF6D61A469ECC122;
struct IIndexedCollectionPropertyBagEnumerator_1_t26A0DC4708EC40881B4274736013DFC7E17C20CD;
struct IIndexedCollectionPropertyBagEnumerator_1_tBC1E85704D5DB6418F61BF7D35C0B5971256F255;
struct IIndexedCollectionPropertyBagEnumerator_1_t0C35254A79F9DD920D54665C0F0FCAB5C66429BB;
struct IIndexedCollectionPropertyBagEnumerator_1_t10A3FE79120150E0A39D2649E6366925E3E7977A;
struct IProperty_1_t6A61BCA264AF8DCD6B17CCD252C9C5BA66D03AE5;
struct IProperty_1_tB2F9BD0CE858D83F88C7377D49E55D3F01A6F8AC;
struct IProperty_1_t6C1DB536D5902BDA12B48EB9A2DC7D9902B80B6A;
struct IProperty_1_t47DF8E7CFAFA47596FCFDA6775C02B5B3C05AAE5;
struct LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9;
struct LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9;
struct ListQueryMatcher_1_t5888012D63B675F26A02C9BF208DC2C856BFBBC4;
struct ListQueryMatcher_1_t80B2C90F8E32F964B971ECBBAA0E811171012425;
struct List_1_tABCD0E063D584200D4058342D9635DCF321C9D4F;
struct List_1_t277B1C8B6F71A7A4C5A4459EBE72D3C71A10D5B9;
struct List_1_t38961482000AC5E53707ECBA054D29F4A45EFA99;
struct List_1_t8C03D59AE9CBDEDECDE563570171B47DCB063CF4;
struct List_1_tDA1565FB54D9C701D544C5C5DEB5F20D33BC07BF;
struct List_1_tCC0A1A2DFE580C45F2D3D390F3E50E3A3CF6FE3A;
struct List_1_t96E9133B70FB6765E6B138E810D33E18901715DA;
struct List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73;
struct List_1_tB1D3AE6638063900DFF2ED463335EF8912FBBF7B;
struct List_1_t7C8CC805CEADA09DFAC2AC1A5D731D5EE956F6DC;
struct List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD;
struct List_1_tEA16F82F7871418E28EB6F551D77A8AD9F2E337F;
struct List_1_t41896DF347A129D13205BBFADE03DFB21E05E459;
struct List_1_tA7CAD0AC45F81CB3151C43DB534729898937504A;
struct List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95;
struct List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB;
struct List_1_t7B0B015A57215073C45C813320AF5F3ECF8149F7;
struct List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E;
struct List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A;
struct List_1_t201EF66D89B48BDF1B448F483575106DF0B49F6A;
struct List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50;
struct List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A;
struct List_1_t3FD5514751EB43E4AB1C145E04BA259B6CD6C74E;
struct List_1_t9DBFF1202B9CF140B233388CFB97CA59663D27D2;
struct List_1_t83F6318ECE580FCC83D7D8AB9A8841147CEF4C65;
struct List_1_t8A3C0729B4B61738DFBD32119977F2991FD56447;
struct List_1_tF582953E1AEAD6ED84833148BAA8969A66E35905;
struct List_1_tE42EE58C643F21E17F2B0AA02402CB8FC1786F7B;
struct List_1_tCF065800FF47BB709E407B98E405036C6C82A151;
struct List_1_t08DDDF84D3AEA5E13828BE225CE2A4720A7CFB64;
struct List_1_t1E327CB749CA1F2F2DA41B2D4DFF57FD6BE0FF66;
struct List_1_t5C9CFABFCE231BB13B0FAEDE274BB6E94B922A00;
struct List_1_t3858E48BB083F1FE977819934ACB97F9BC44439A;
struct List_1_t66029D02EA129D03A082E470EB98C85812301A0C;
struct List_1_t268ADAE63E623B42094720668F8DB571CF6085E6;
struct List_1_tBF847DD77CE898FDD304012AB5DFBFC15986D8F7;
struct List_1_t4A80BCCFB0BC8742C8BB601365DB07226750573A;
struct List_1_t6590FC1C3D6405E4972389536FA11B52542105C5;
struct List_1_t0325EC485D0C783FC73CF8D1F8C84A6628937599;
struct List_1_tC96558A2A77DD595C724D43BFE409C769AB0FA1E;
struct List_1_tF43D0DA1B22D0A91FBF53BD9D3F2134C10703D4D;
struct List_1_tE8267DC50323E6835C1F1668CBED621535AD99C4;
struct List_1_t46384C53612EB5905F3A9C122B722D9C70F755A7;
struct List_1_t179992B095C528117EEF4782A1F45568BD2D51EA;
struct List_1_tF41D14F75BDD50A70D20F7CE0A3382E7D3667AB1;
struct List_1_tD554524C4D3CE5612DAE24A61267C5A8170B530A;
struct List_1_tE6CA98E04E3B94E85D2BCDBF673C6550F7865EF4;
struct List_1_tDB6FA8EDA7A26227B5CA100203EF7BAFA0FDBDDF;
struct List_1_t274EF879F062D96E9E5D05941FF15F5051FF20C2;
struct List_1_t70EE7982F45810D4B024CF720D910E67974A3094;
struct List_1_tB0B308D151FBF226396B20253C1FF11CF7D10D3A;
struct LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9;
struct NativePagedList_1_t9B43BA7543672C8F12763C42AC5C493E40A41AA8;
struct NativePagedList_1_tEBFD84343834CFE74C052BA60816E83FEE4F06DC;
struct Queue_1_t00794C5F46E3254E713B03F949E6AB3A1AB1FD41;
struct Queue_1_t488F4FFC87B785BACAAF18A6B2E9307E5451DF68;
struct Queue_1_t75840AD230E9D2498F813412770B56A17D0281F1;
struct Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A;
struct Stack_1_tB568ED1852EE70A3EECA2CD66F2AB41DDEC262FA;
struct Stack_1_t90AD4B4BDD857A86CDEC55130FE1BAB9871F0CFE;
struct Stack_1_tAE809352224B7904D166288D1476FD4C2ED0D631;
struct Stack_1_t19851BEF370D35BCE2A6C3848C5148B09113067C;
struct Stack_1_tBFBA4533224CC5B6CFEF7F656B39ED3995773D0B;
struct NativeArray_1U5BU5D_t4BC8FD0BBBD51EF2E3A56BF78296DF37173EFDBD;
struct SlotU5BU5D_t2B9148416634BCB8743E1F61254B305F1882FF1D;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C;
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct InternedStringU5BU5D_t0B851758733FC0B118D84BE83AED10A0404C18D5;
struct NameAndParametersU5BU5D_tA6C2AC34ACDB1967A7A2CEF4BE1D717ADA695CA2;
struct NamedValueU5BU5D_tADD8F1373B88C55F68499688D72C21A97F63303A;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct PropertyPathPartU5BU5D_t7994D542F14DDDDEABB1792C335C20149399AEBB;
struct RuleMatcherU5BU5D_t0135EA06151E72D04414F3EAF9420CB85EE2236C;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
struct VertexAttributeDescriptorU5BU5D_t5D10E60612F12777F59B7E33939F9075DB0E02B2;
struct VisualElementU5BU5D_tCAE8038767BF0FBEE26B3470C0FC4AE60E5229DF;
struct __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979;
struct __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC;
struct HIDPageUsageU5BU5D_t021CBAE17F584DBB0E9E003DA47E89ADE04211CD;
struct ControlItemU5BU5D_t7798E8B7C7F58B8F6D13B567539CD82E962C7104;
struct DeviceRequirementU5BU5D_t0496FAAB7554B7BFC270BA53BA6A5EFD5DE061CE;
struct StateChangeMonitorTimeoutU5BU5D_tC50E6F92A1C5CE6397F411E0967B3A428FFF1C77;
struct OngoingAccountSelectionU5BU5D_t0CC84AB78BA7B45022C3D6E08B0DADA7A61BCBD8;
struct OnScreenDeviceInfoU5BU5D_t0C70881971941DEFDD18CFFD3E4133B5CA0B7F30;
struct ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F;
struct Awaitable_t690337FEC1C411606E233EA36A41337B931C23CB;
struct BaseVisualElementPanel_tE3811F3D1474B72CB6CD5BCEECFF5B5CBEC1E303;
struct Binding_tA1358A155852138C1926082E4F3245B6F631CBED;
struct Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3;
struct EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C;
struct EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85;
struct EventConsumer_t6D08A21A3FF574758C289118775786B6D35754F3;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct IFormatterConverter_t726606DAC82C384B08C82471313C340968DDB609;
struct IInputStateChangeMonitor_t043FD8736A6F45C06A2FAFFC5BE70A565F999598;
struct InlineStyleAccess_t5CA7877999C9442491A220AE50D605C84D09A165;
struct InputControl_t74F06B623518F992BF8E38656A5E0857169E3E2E;
struct InputDevice_t8BCF67533E872A75779C24C93D1D7085B72D364B;
struct InputEvent_t10F727342D1A79DCFC06529C203BB61C194AEBC5;
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB;
struct LensFlareComponentSRP_tF068937A1EC9EB063AF855DB4994A1D2A5E3EFDC;
struct Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4;
struct MeshGenerationCallback_t9735BAFB971D361BCB27AB541F8205A7C0FFEF50;
struct MeshHandle_tC1E9A7ECCFDAEFDE064B8D58B35B9CEE5A70A22E;
struct OnScreenControl_t449BC1FA3DCA3F7787643FAB4F4B1906A7B32418;
struct Page_tB4EA8095DF85BAF22AB8FCA71400121E721B57C9;
struct PathRef_t76F7677792A14AF9D6A6EAB7F08C1A3DC2B27A55;
struct PlatformNotSupportedException_tD2BD7EB9278518AA5FE8AE75AD5D0D4298A4631A;
struct Regex_tE773142C2BE45C5D362B0F815AFF831707A51772;
struct RenderData_t1ABE116B2B5E0409AC699E195922516606531DC2;
struct ResolvedStyleAccess_t226CC840EBACEE31CE1139ED5F717532AFFAEB45;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37;
struct String_t;
struct StyleVariableContext_tF74F2787CE1F6BEBBFBFF0771CF493AC9E403527;
struct TextAsset_t2C64E93DA366D9DE5A8209E1802FA4884AC1BD69;
struct UInt64_t8F12534CC8FC4B5860F2A2CD1EE79D322E7A41AF;
struct UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7;
struct VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115;
struct VisualElementSelectorData_t4C4043092A19A653CE8637C7659E36E0D3CC23A0;
struct VisualElementTransformData_t3DD575B5990B68FF956673EFF036171C86A38DF3;
struct VisualTreeAsset_tFB5BF81F0780A412AE5A7C2C552B3EEA64EA2EEB;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
struct BindingData_t63DF4F2F4FA1BE1BD6130C2DDAE1888DCD76381A;
struct Record_tD4D528E1BFF948E92C590DCE7A010BA356D50049;
struct EqualityComparer_tF8FCE99C5DBB2F35D74728CB65346435381CFD5B;
struct Data_t6BD087CC0FA9794D342D260035A70E365224C66E;
struct GPUBuffer_t0260DDC48C911E3B86729ABA0B6E63C064853BC3;
struct TypeData_t01D670B4E71B5571B38C7412B1E652A47D6AF66A;

IL2CPP_EXTERN_C RuntimeClass* ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* PlatformNotSupportedException_tD2BD7EB9278518AA5FE8AE75AD5D0D4298A4631A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* VisualElementListPool_tB82E686FE29EA30D8CEDF6FF352FD1B7E7A30095_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral2390D6884F59E2E4EA04837AD7D6268548597633;
IL2CPP_EXTERN_C String_t* _stringLiteral27E800F6FC3576FA08A781DC2C4A4BD96BA82D76;
IL2CPP_EXTERN_C String_t* _stringLiteral38078454203FA992D959F0FD913FDD567D9EA9EB;
IL2CPP_EXTERN_C String_t* _stringLiteral63FC874122847D14784CB3ADBE59A08B9558FA97;
IL2CPP_EXTERN_C String_t* _stringLiteral670CBAB5C8A30A4C8DC68337B78F13CC30BC704D;
IL2CPP_EXTERN_C String_t* _stringLiteral834F4B6837B71847C4048C946DF8754B323D6BF9;
IL2CPP_EXTERN_C String_t* _stringLiteralF8D08FCF1537043BF0289FA98C51BF5A3AC7C618;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Item_mF58794633948FE8284FCDACC4456686548388092_RuntimeMethod_var;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;
struct PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_com;
struct PropertyPathPart_tFB308743948D2298957DC1898D90AF2ACFED9DFF_marshaled_pinvoke;

struct SlotU5BU5D_t2B9148416634BCB8743E1F61254B305F1882FF1D;
struct __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87  : public RuntimeObject
{
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____buckets;
	SlotU5BU5D_t2B9148416634BCB8743E1F61254B305F1882FF1D* ____slots;
	int32_t ____count;
	int32_t ____lastIndex;
	int32_t ____freeList;
	RuntimeObject* ____comparer;
	int32_t ____version;
	SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37* ____siInfo;
};
struct LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9 : public RuntimeObject {};
struct LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9  : public RuntimeObject
{
	LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9* ___head;
	int32_t ___count;
	int32_t ___version;
	RuntimeObject* ____syncRoot;
	SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37* ____siInfo;
};
struct List_1_tDA1565FB54D9C701D544C5C5DEB5F20D33BC07BF  : public RuntimeObject
{
	NativeArray_1U5BU5D_t4BC8FD0BBBD51EF2E3A56BF78296DF37173EFDBD* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_t7C8CC805CEADA09DFAC2AC1A5D731D5EE956F6DC  : public RuntimeObject
{
	RuleMatcherU5BU5D_t0135EA06151E72D04414F3EAF9420CB85EE2236C* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95  : public RuntimeObject
{
	VisualElementU5BU5D_tCAE8038767BF0FBEE26B3470C0FC4AE60E5229DF* ____items;
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
struct LowLevelList_1_tFA10A51C575F28B96FF2E2E7FB3740E48546A1E0  : public RuntimeObject
{
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ____items;
	int32_t ____size;
	int32_t ____version;
};
struct Queue_1_t00794C5F46E3254E713B03F949E6AB3A1AB1FD41  : public RuntimeObject
{
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ____array;
	int32_t ____head;
	int32_t ____tail;
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
struct CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4  : public RuntimeObject
{
	bool ___isIMGUIContainer;
	EventCallbackRegistry_tE18297C3F7E535BD82EDA83EC6D6DAA386226B85* ___m_CallbackRegistry;
};
struct HierarchyTraversal_t89B1D4664C1C066FEED9B372488A3F751CB6FE7D  : public RuntimeObject
{
};
struct SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37  : public RuntimeObject
{
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ___m_members;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___m_data;
	TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* ___m_types;
	Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* ___m_nameToIndex;
	int32_t ___m_currMember;
	RuntimeObject* ___m_converter;
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
typedef Il2CppFullySharedGenericStruct Data_t6F131ABB2DC3608FBC19EEE1D985484C670DE41A;
struct Enumerator_t8925FB47C03812518E4A2FA72614F9DF6E7BC900 
{
	List_1_tABCD0E063D584200D4058342D9635DCF321C9D4F* ____list;
	int32_t ____index;
	int32_t ____version;
	RuntimeObject* ____current;
};
struct Enumerator_t3DB6B256496A78585890809B8DADD57AFDDEBD49 
{
	List_1_t277B1C8B6F71A7A4C5A4459EBE72D3C71A10D5B9* ____list;
	int32_t ____index;
	int32_t ____version;
	RuntimeObject* ____current;
};
struct Enumerator_tE6F11662226BE79E82B357013873FADA64A5444C 
{
	List_1_t38961482000AC5E53707ECBA054D29F4A45EFA99* ____list;
	int32_t ____index;
	int32_t ____version;
	RuntimeObject* ____current;
};
struct Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63 
{
	List_1_t8C03D59AE9CBDEDECDE563570171B47DCB063CF4* ____list;
	int32_t ____index;
	int32_t ____version;
	RuntimeObject* ____current;
};
struct Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69 
{
	uint64_t* ___m_Indices;
	int32_t ___m_Count;
	int32_t ___m_Current;
};
struct Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96 
{
	List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95* ___iterationList;
	int32_t ___currentIndex;
};
struct Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72 
{
	Data_t6F131ABB2DC3608FBC19EEE1D985484C670DE41A* ___m_Data;
	int32_t ___m_Index;
};
typedef Il2CppFullySharedGenericStruct Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB;
typedef Il2CppFullySharedGenericStruct Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457;
typedef Il2CppFullySharedGenericStruct Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF;
typedef Il2CppFullySharedGenericStruct Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0;
typedef Il2CppFullySharedGenericStruct Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C;
struct Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923 
{
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___m_Array;
	int32_t ___m_IndexStart;
	int32_t ___m_IndexEnd;
	int32_t ___m_Index;
};
typedef Il2CppFullySharedGenericStruct Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41;
struct Enumerator_t1858EB5DA113EEDAF7EDEE685C04492B2FBE9C6D 
{
	HIDPageUsageU5BU5D_t021CBAE17F584DBB0E9E003DA47E89ADE04211CD* ___m_Array;
	int32_t ___m_IndexStart;
	int32_t ___m_IndexEnd;
	int32_t ___m_Index;
};
struct Enumerator_tEDF92AFCC05D1B0B00370110CE3A4BE62C632C7C 
{
	ControlItemU5BU5D_t7798E8B7C7F58B8F6D13B567539CD82E962C7104* ___m_Array;
	int32_t ___m_IndexStart;
	int32_t ___m_IndexEnd;
	int32_t ___m_Index;
};
struct Enumerator_t924412C78294BC2600FCB913AF5CF4D5F45C239A 
{
	DeviceRequirementU5BU5D_t0496FAAB7554B7BFC270BA53BA6A5EFD5DE061CE* ___m_Array;
	int32_t ___m_IndexStart;
	int32_t ___m_IndexEnd;
	int32_t ___m_Index;
};
typedef Il2CppFullySharedGenericStruct InlinedArray_1_tCA1A9B410EBCE10320B39011CD2E337C395D7CA3;
struct LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9  : public LowLevelList_1_tFA10A51C575F28B96FF2E2E7FB3740E48546A1E0
{
};
struct NativeSlice_1_t135E2A23AA4EBDBCA846942F90F29A5004E8ACB5 
{
	uint8_t* ___m_Buffer;
	int32_t ___m_Stride;
	int32_t ___m_Length;
};
struct NativeSlice_1_t0D1A1AB7A9C4768B84EB7420D04A90920533C78A 
{
	uint8_t* ___m_Buffer;
	int32_t ___m_Stride;
	int32_t ___m_Length;
};
struct NativeSlice_1_t66375568C4FF313931F4D2F646D64FE6A406BAD2 
{
	uint8_t* ___m_Buffer;
	int32_t ___m_Stride;
	int32_t ___m_Length;
};
struct NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52 
{
	uint8_t* ___m_Buffer;
	int32_t ___m_Stride;
	int32_t ___m_Length;
};
struct NativeSlice_1_t26239564C98CCF89787A77F69BF0133259B076E4 
{
	uint8_t* ___m_Buffer;
	int32_t ___m_Stride;
	int32_t ___m_Length;
};
struct NativeSlice_1_tF501174E970F834D403C737FA11314C2674055EA 
{
	uint8_t* ___m_Buffer;
	int32_t ___m_Stride;
	int32_t ___m_Length;
};
struct NativeSlice_1_t10EE15BD47CA7D87727E99494E581ADA4021B6B2 
{
	uint8_t* ___m_Buffer;
	int32_t ___m_Stride;
	int32_t ___m_Length;
};
struct NativeSlice_1_t8E27FA1E73FC8A04E990C238360882D58A2DB2F9 
{
	uint8_t* ___m_Buffer;
	int32_t ___m_Stride;
	int32_t ___m_Length;
};
struct NativeSlice_1_t72AA15A00D1B7CE9B7265E487D15133C9F091FD0 
{
	uint8_t* ___m_Buffer;
	int32_t ___m_Stride;
	int32_t ___m_Length;
};
struct NativeSlice_1_t4207DB5E16E1EF9068326562E97E3AA675F0DAF9 
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
struct ReadOnly_tE952B10A1D4D5957366F8D5976512B509D8E7D57 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_tEC1B78AA9A46DB8AD1C5DBAE542E1B5A9A8DF8CE 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_tDAD0456355433E3A9DB77C242419A3C2B61FF7A1 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_t9EC554A4C51E0E58FA55ED6550A28C34131B53F8 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_tC6998C67EE7BA262710FFDCF6BE6728CB987FDE8 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_tFC602D25626C1ACA10B4F01C545A1D0D38840EC3 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_t3E2994BF23F5B0A8702C4C3F434E9A0E41458751 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_t9E27B0BA763C80368372F9471BC3CBC7760914A1 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_t3A49C41F0E00427FDA453EAA1AC36802B113FC83 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_t86B3AB6727FB0CF3474F5B7BEBDA30FC9F9951D2 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_t76BF0F8C193F52D463F8BB9AC0214BACB9D95660 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_t1572A3C97E859F8C123485E0238725356EE4CA1A 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_tA522D53D759ABB16A1EABA790D2AD0E19DACA601 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_t0FE5617AC8525F60705BB39B5A7AD67C85F26B8D 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_t490AE34C6A92FDCF1F34BA02E3FE19FE48B355BC 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_tD24F078322DD82D0B34548D94F195EEF89677EE0 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_t487675745B1D87BF6C08E2FA49C96616D0725408 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_t5F3121537F132F255E330E9C75EF3CE5321D6987 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_tEFEB81D67B96BE28BD71A1DEB8AE16EA6C902AC3 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_t641867D1516A48A7C44E7421FB5FB2267FAAEA18 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_tA25B512569688CCD186105D5826DF7B6D790737F 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_tCE5F8D70DB00F29CFA4C645ED1910D8EFEFD0CFB 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_tE2BB0F75963087A7149C3EA4E51899E26C046D87 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_t61D470FA128F3A1F6BD4A67BA697EE8D002A1F91 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_t9871B20A93A393B27B059E7A519336F6963606D8 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_t37CCA313F8D670925051D441B669A6C13037061F 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_t890D24C3B04F979FD5F2961BC38BD5F2B796D9B6 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
};
struct ReadOnly_tDCEED2CFBEB31EA295986393AC6277E98D2FF755 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
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
typedef Il2CppFullySharedGenericStruct Slot_tEC146EEEF7022C6542EBF082D658446682962BFD;
struct UQueryState_1_t62CA2841B4A1205A8554F7E9D2A3FBFB0F92B192 
{
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* ___m_Element;
	List_1_t7C8CC805CEADA09DFAC2AC1A5D731D5EE956F6DC* ___m_Matchers;
};
struct UnmanagedRefCountedList_1_t8B02F7EA0267E2F24A9C139864512B7B0BBE81BC 
{
	union
	{
		struct
		{
			Data_t6F131ABB2DC3608FBC19EEE1D985484C670DE41A* ___m_Data;
		};
		uint8_t UnmanagedRefCountedList_1_t422A4815A59948FB5060BB10CB6D6DC9F24D2948__padding[8];
	};
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
struct Focusable_t39F2BAF0AF6CA465BC2BEDAF9B5B2CF379B846D0  : public CallbackEventHandler_t99E35735225B4ACEAD1BA981632FD2D46E9CB2B4
{
	bool ___m_Focusable;
	int32_t ___m_TabIndex;
	bool ___m_DelegatesFocus;
	bool ___m_ExcludeFromFocusRing;
	bool ___U3CisEligibleToReceiveFocusFromDisabledChildU3Ek__BackingField;
};
struct FourCC_tA6CAA4015BC25A7F1053B6C512202D57A9C994ED 
{
	int32_t ___m_Code;
};
struct IndexedCollectionSharedPropertyState_t1E4ED168A72271A87F5FCFCBBF19BBB63F95FC79 
{
	int32_t ___Index;
	bool ___IsReadOnly;
};
struct IndexedCollectionSharedPropertyState_t1E4ED168A72271A87F5FCFCBBF19BBB63F95FC79_marshaled_pinvoke
{
	int32_t ___Index;
	int32_t ___IsReadOnly;
};
struct IndexedCollectionSharedPropertyState_t1E4ED168A72271A87F5FCFCBBF19BBB63F95FC79_marshaled_com
{
	int32_t ___Index;
	int32_t ___IsReadOnly;
};
struct InputEventPtr_tC2A58521C9AFB479CC88789D5E0797D817C721C0 
{
	alignas(IL2CPP_SIZEOF_VOID_P) InputEvent_t10F727342D1A79DCFC06529C203BB61C194AEBC5* ___m_EventPtr;
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
struct PhysicsHandle_tEC3DCC38ABB8395068171070539ABF8309A854C2 
{
	int32_t ___m_Index1;
	uint16_t ___m_World0;
	uint16_t ___m_Generation;
};
struct PropertyName_tE4B4AAA58AF3BF2C0CD95509EB7B786F096901C2 
{
	int32_t ___id;
};
struct Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 
{
	float ___x;
	float ___y;
	float ___z;
	float ___w;
};
struct RawSlice_t8512E18D23DE01C7FCF7249BCF43911D4138CE9A 
{
	uint8_t* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_Stride;
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
struct UInt64_t8F12534CC8FC4B5860F2A2CD1EE79D322E7A41AF 
{
	uint64_t ___m_value;
};
struct UIntPtr_t 
{
	void* ____pointer;
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
struct Vector3Int_t65CB06F557251D18A37BD71F3655BA836A357376 
{
	int32_t ___m_X;
	int32_t ___m_Y;
	int32_t ___m_Z;
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
struct double2_t2124FB15A5BDCC0BD48B80116D4B24DC9DCA9D83 
{
	double ___x;
	double ___y;
};
struct float2_t3398D7209CB6D09FEA5C49AC55070C5E7BE0B2A5 
{
	float ___x;
	float ___y;
};
struct float3_t7600B73F092B37F484B12910A5269F30C778D31E 
{
	float ___x;
	float ___y;
	float ___z;
};
struct float4_t545A994996126766890C1F28B43EA823F2410190 
{
	float ___x;
	float ___y;
	float ___z;
	float ___w;
};
struct int2_tB123E869131C6102341F8F9AA01726E0E7C2768D 
{
	int32_t ___x;
	int32_t ___y;
};
struct int3_t18873F121F1EF24ECE875EAB01170B69E1B498C5 
{
	int32_t ___x;
	int32_t ___y;
	int32_t ___z;
};
struct int4_t6DC3FEACC0DD1EAE4D5B897188E8D669BC4DAB90 
{
	int32_t ___x;
	int32_t ___y;
	int32_t ___z;
	int32_t ___w;
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
struct MatchContext_t04110FFA271D89A23BC1918BE657634A7DC06253 
{
	int32_t ___valueIndex;
	int32_t ___matchedVariableCount;
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
struct DispatchContext_tFA37790A5FF30508B0146B79E4FF1880EB82E455 
{
	uint32_t ___m_GateCount;
	Queue_1_t488F4FFC87B785BACAAF18A6B2E9307E5451DF68* ___m_Queue;
};
struct DispatchContext_tFA37790A5FF30508B0146B79E4FF1880EB82E455_marshaled_pinvoke
{
	uint32_t ___m_GateCount;
	Queue_1_t488F4FFC87B785BACAAF18A6B2E9307E5451DF68* ___m_Queue;
};
struct DispatchContext_tFA37790A5FF30508B0146B79E4FF1880EB82E455_marshaled_com
{
	uint32_t ___m_GateCount;
	Queue_1_t488F4FFC87B785BACAAF18A6B2E9307E5451DF68* ___m_Queue;
};
struct EventRecord_tEC2901C48A23F5AFE20A9E8D4F05F3799EA62BF2 
{
	EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C* ___m_Event;
	BaseVisualElementPanel_tE3811F3D1474B72CB6CD5BCEECFF5B5CBEC1E303* ___m_Panel;
};
struct EventRecord_tEC2901C48A23F5AFE20A9E8D4F05F3799EA62BF2_marshaled_pinvoke
{
	EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C* ___m_Event;
	BaseVisualElementPanel_tE3811F3D1474B72CB6CD5BCEECFF5B5CBEC1E303* ___m_Panel;
};
struct EventRecord_tEC2901C48A23F5AFE20A9E8D4F05F3799EA62BF2_marshaled_com
{
	EventBase_tD7F89B936EB8074AE31E7B15976C072277371F7C* ___m_Event;
	BaseVisualElementPanel_tE3811F3D1474B72CB6CD5BCEECFF5B5CBEC1E303* ___m_Panel;
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
struct CpuCopyRange_tCE4B2EBDC2B62D128407AE7B9484BB04BBC8FE93 
{
	uint8_t* ___srcPtr;
	uint8_t* ___dstPtr;
	int32_t ___byteSize;
};
struct StateChangeMonitorTimeout_t155BA6A9DDCDB6474629D31735C037134B0C3049 
{
	InputControl_t74F06B623518F992BF8E38656A5E0857169E3E2E* ___control;
	double ___time;
	RuntimeObject* ___monitor;
	int64_t ___monitorIndex;
	int32_t ___timerIndex;
};
struct StateChangeMonitorTimeout_t155BA6A9DDCDB6474629D31735C037134B0C3049_marshaled_pinvoke
{
	InputControl_t74F06B623518F992BF8E38656A5E0857169E3E2E* ___control;
	double ___time;
	RuntimeObject* ___monitor;
	int64_t ___monitorIndex;
	int32_t ___timerIndex;
};
struct StateChangeMonitorTimeout_t155BA6A9DDCDB6474629D31735C037134B0C3049_marshaled_com
{
	InputControl_t74F06B623518F992BF8E38656A5E0857169E3E2E* ___control;
	double ___time;
	RuntimeObject* ___monitor;
	int64_t ___monitorIndex;
	int32_t ___timerIndex;
};
struct OngoingAccountSelection_t8471F20105109B19CE04657B1C72EB8D23976956 
{
	InputDevice_t8BCF67533E872A75779C24C93D1D7085B72D364B* ___device;
	uint32_t ___userId;
};
struct OngoingAccountSelection_t8471F20105109B19CE04657B1C72EB8D23976956_marshaled_pinvoke
{
	InputDevice_t8BCF67533E872A75779C24C93D1D7085B72D364B* ___device;
	uint32_t ___userId;
};
struct OngoingAccountSelection_t8471F20105109B19CE04657B1C72EB8D23976956_marshaled_com
{
	InputDevice_t8BCF67533E872A75779C24C93D1D7085B72D364B* ___device;
	uint32_t ___userId;
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
struct CallbackInfo_tAAF5B235D20558849D45390E3FFA7A0FF2183A08 
{
	MeshGenerationCallback_t9735BAFB971D361BCB27AB541F8205A7C0FFEF50* ___callback;
	RuntimeObject* ___userData;
};
struct CallbackInfo_tAAF5B235D20558849D45390E3FFA7A0FF2183A08_marshaled_pinvoke
{
	Il2CppMethodPointer ___callback;
	Il2CppIUnknown* ___userData;
};
struct CallbackInfo_tAAF5B235D20558849D45390E3FFA7A0FF2183A08_marshaled_com
{
	Il2CppMethodPointer ___callback;
	Il2CppIUnknown* ___userData;
};
struct BrickChunkAlloc_t95EB283E186F5DA1E74A8DDE415EB8E7ABFDF51B 
{
	int32_t ___x;
	int32_t ___y;
	int32_t ___z;
};
struct CellStreamingScratchBufferLayout_t9C3A198B967DCECDB79C97C05BEFAEA74E70077E 
{
	int32_t ____SharedDestChunksOffset;
	int32_t ____L0L1rxOffset;
	int32_t ____L1GryOffset;
	int32_t ____L1BrzOffset;
	int32_t ____ValidityOffset;
	int32_t ____ProbeOcclusionOffset;
	int32_t ____SkyOcclusionOffset;
	int32_t ____SkyShadingDirectionOffset;
	int32_t ____L2_0Offset;
	int32_t ____L2_1Offset;
	int32_t ____L2_2Offset;
	int32_t ____L2_3Offset;
	int32_t ____L0Size;
	int32_t ____L0ProbeSize;
	int32_t ____L1Size;
	int32_t ____L1ProbeSize;
	int32_t ____ValiditySize;
	int32_t ____ValidityProbeSize;
	int32_t ____ProbeOcclusionSize;
	int32_t ____ProbeOcclusionProbeSize;
	int32_t ____SkyOcclusionSize;
	int32_t ____SkyOcclusionProbeSize;
	int32_t ____SkyShadingDirectionSize;
	int32_t ____SkyShadingDirectionProbeSize;
	int32_t ____L2Size;
	int32_t ____L2ProbeSize;
	int32_t ____ProbeCountInChunkLine;
	int32_t ____ProbeCountInChunkSlice;
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
struct UQueryMatcher_tF8783F54BA397641240DA7025126F6301EF53FF8  : public HierarchyTraversal_t89B1D4664C1C066FEED9B372488A3F751CB6FE7D
{
	List_1_t7C8CC805CEADA09DFAC2AC1A5D731D5EE956F6DC* ___m_Matchers;
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
struct U3C_StpSetupPerViewConstantsU3Ee__FixedBuffer_t9BB7998676CF28C673FFF3872BE1163DFEDAB3F6 
{
	union
	{
		struct
		{
			float ___FixedElementField;
		};
		uint8_t U3C_StpSetupPerViewConstantsU3Ee__FixedBuffer_t9BB7998676CF28C673FFF3872BE1163DFEDAB3F6__padding[256];
	};
};
struct Enumerator_tC1924C3C2DAC82DF39552A7D5AB2DCA096AFF4A5 
{
	ReadOnly_tE952B10A1D4D5957366F8D5976512B509D8E7D57 ___m_Array;
	int32_t ___m_Index;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___value;
};
struct Enumerator_tC72C450C6348CF3BBFD26CFBF57C18F3EE073DB9 
{
	NativeSlice_1_t66375568C4FF313931F4D2F646D64FE6A406BAD2 ___m_Array;
	int32_t ___m_Index;
};
struct Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817 
{
	NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52 ___m_Array;
	int32_t ___m_Index;
};
typedef Il2CppFullySharedGenericStruct Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C;
typedef Il2CppFullySharedGenericStruct Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092;
struct Enumerator_t78550044AD06493858F5BA5AB240550307F420E7 
{
	ReadOnly_tFC602D25626C1ACA10B4F01C545A1D0D38840EC3 ___m_Array;
	int32_t ___m_Index;
	double2_t2124FB15A5BDCC0BD48B80116D4B24DC9DCA9D83 ___value;
};
struct Enumerator_t388AFDC9E62EE1E1535D411A111F8A27EF74D33E 
{
	ReadOnly_t3E2994BF23F5B0A8702C4C3F434E9A0E41458751 ___m_Array;
	int32_t ___m_Index;
	float2_t3398D7209CB6D09FEA5C49AC55070C5E7BE0B2A5 ___value;
};
struct Enumerator_tFFC0BCF66DEEB6D6A13C6FE083BD247052D211FE 
{
	NativeSlice_1_t26239564C98CCF89787A77F69BF0133259B076E4 ___m_Array;
	int32_t ___m_Index;
};
struct Enumerator_t5245489831C71E2BAB60B43661AC3940DB2A5604 
{
	ReadOnly_t9E27B0BA763C80368372F9471BC3CBC7760914A1 ___m_Array;
	int32_t ___m_Index;
	float3_t7600B73F092B37F484B12910A5269F30C778D31E ___value;
};
struct Enumerator_t7C9805CF80D1529070ABC15180F4F22C3AC04B6D 
{
	NativeSlice_1_tF501174E970F834D403C737FA11314C2674055EA ___m_Array;
	int32_t ___m_Index;
};
struct Enumerator_t4354EE378CDAA2FCDEB356C8B42FCA37D8C8CE18 
{
	ReadOnly_t3A49C41F0E00427FDA453EAA1AC36802B113FC83 ___m_Array;
	int32_t ___m_Index;
	float4_t545A994996126766890C1F28B43EA823F2410190 ___value;
};
struct Enumerator_tF3D69EA9265DA5E51F00E9696E1691C76D9BE79F 
{
	ReadOnly_t76BF0F8C193F52D463F8BB9AC0214BACB9D95660 ___m_Array;
	int32_t ___m_Index;
	int2_tB123E869131C6102341F8F9AA01726E0E7C2768D ___value;
};
struct Enumerator_t5274CB61A812B8983822BADF40BB344FD05DE133 
{
	ReadOnly_t1572A3C97E859F8C123485E0238725356EE4CA1A ___m_Array;
	int32_t ___m_Index;
	int3_t18873F121F1EF24ECE875EAB01170B69E1B498C5 ___value;
};
struct Enumerator_t017A0AA1D5EB5F4DA649DEE4CA3B7A340BC2F735 
{
	ReadOnly_tA522D53D759ABB16A1EABA790D2AD0E19DACA601 ___m_Array;
	int32_t ___m_Index;
	int4_t6DC3FEACC0DD1EAE4D5B897188E8D669BC4DAB90 ___value;
};
struct Enumerator_t6630402C90E9E1872D41DA78B1CA17C07D89A9F5 
{
	List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* ____list;
	int32_t ____index;
	int32_t ____version;
	AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ____current;
};
struct Enumerator_t6B0EB72F34C4614A45F424103A9D11C74439E854 
{
	Stack_1_tB568ED1852EE70A3EECA2CD66F2AB41DDEC262FA* ____stack;
	int32_t ____version;
	int32_t ____index;
	MatchContext_t04110FFA271D89A23BC1918BE657634A7DC06253 ____currentElement;
};
struct Enumerator_t389EF10F90069ED924478E5694DFE1BB5CBCF251 
{
	List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* ____list;
	int32_t ____index;
	int32_t ____version;
	OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ____current;
};
struct Enumerator_t171CB118C2D1EDA6285019F361AFE0E164C5C353 
{
	List_1_t3FD5514751EB43E4AB1C145E04BA259B6CD6C74E* ____list;
	int32_t ____index;
	int32_t ____version;
	Page_t04FE552A388BF55B12C8868E19589136957E00A5 ____current;
};
struct Enumerator_t55A4B2784AB93C9EFCBE338B25EF791444550DA0 
{
	List_1_t9DBFF1202B9CF140B233388CFB97CA59663D27D2* ____list;
	int32_t ____index;
	int32_t ____version;
	AttributeOverrideRange_t47A9929769CAD72883980AE2C29FACC19B310D49 ____current;
};
struct Enumerator_t8415E89BC60CAC4A0A725233BEF28B6CC5A2D89A 
{
	List_1_t83F6318ECE580FCC83D7D8AB9A8841147CEF4C65* ____list;
	int32_t ____index;
	int32_t ____version;
	SerializedDataOverrideRange_t3A1FFF9457378F017DD8F445C7AF421B9AB65014 ____current;
};
struct Enumerator_t02FF38A6AF277B3D3A6E2312B96BDD7F6B746200 
{
	List_1_tF582953E1AEAD6ED84833148BAA8969A66E35905* ____list;
	int32_t ____index;
	int32_t ____version;
	ChangesFromUI_t56B0B09645F7D36924555D832D21882400771022 ____current;
};
struct Enumerator_tCF5704BBC09A79212D02EAD0ACC7E06C776B31DC 
{
	List_1_tE42EE58C643F21E17F2B0AA02402CB8FC1786F7B* ____list;
	int32_t ____index;
	int32_t ____version;
	Range_tD224E9E0AED0FDE1D3F2FC611A81313E46779045 ____current;
};
struct Enumerator_t011C20FE326DB23A697B81F1A63C0E138D1D366D 
{
	List_1_tCF065800FF47BB709E407B98E405036C6C82A151* ____list;
	int32_t ____index;
	int32_t ____version;
	AllocSize_t906784497648EDC3F45F4EA656C6858238BA2F7C ____current;
};
struct Enumerator_tD97C98C8AA749156CECC60E8ED112C7771E69B84 
{
	Stack_1_t90AD4B4BDD857A86CDEC55130FE1BAB9871F0CFE* ____stack;
	int32_t ____version;
	int32_t ____index;
	AllocSize_t906784497648EDC3F45F4EA656C6858238BA2F7C ____currentElement;
};
struct Enumerator_t8E71E3162A14D26A339D3561D43523590F594C86 
{
	Stack_1_t19851BEF370D35BCE2A6C3848C5148B09113067C* ____stack;
	int32_t ____version;
	int32_t ____index;
	DispatchContext_tFA37790A5FF30508B0146B79E4FF1880EB82E455 ____currentElement;
};
struct Enumerator_t3B7550C1130BBD2F1A4BAE88FB62305FC7A95602 
{
	Queue_1_t488F4FFC87B785BACAAF18A6B2E9307E5451DF68* ____q;
	int32_t ____version;
	int32_t ____index;
	EventRecord_tEC2901C48A23F5AFE20A9E8D4F05F3799EA62BF2 ____currentElement;
};
struct Enumerator_tC40C530F62540E93BDAAFF42E8BDE3B3B3328EA0 
{
	List_1_t1E327CB749CA1F2F2DA41B2D4DFF57FD6BE0FF66* ____list;
	int32_t ____index;
	int32_t ____version;
	FocusedElement_t1EE083A1C5276213C533A38C6B5DC02E9DE5CBEF ____current;
};
struct Enumerator_t70E14EB8E66A55CA7D752E23BC629F93213F3997 
{
	NativeSlice_1_t10EE15BD47CA7D87727E99494E581ADA4021B6B2 ___m_Array;
	int32_t ___m_Index;
};
struct Enumerator_t7ED9D0AD5E1E2926B245190AD8864C3EBCCDD22A 
{
	ReadOnly_t487675745B1D87BF6C08E2FA49C96616D0725408 ___m_Array;
	int32_t ___m_Index;
	CpuCopyRange_tCE4B2EBDC2B62D128407AE7B9484BB04BBC8FE93 ___value;
};
struct Enumerator_tB12CCD65F6BB7041CBB9B41F85A9B79DE9D58170 
{
	List_1_t6590FC1C3D6405E4972389536FA11B52542105C5* ____list;
	int32_t ____index;
	int32_t ____version;
	LensFlareCompInfo_tF75BC6BAED886C71425763DEA827F3794E718F37 ____current;
};
struct Enumerator_t388482DB0A98B84F0E9A2C3F7C6F6DD949077710 
{
	Queue_1_t75840AD230E9D2498F813412770B56A17D0281F1* ____q;
	int32_t ____version;
	int32_t ____index;
	CallbackInfo_tAAF5B235D20558849D45390E3FFA7A0FF2183A08 ____currentElement;
};
struct Enumerator_t6FFB1C365FAC345FA1F2532147624A50557CB5E3 
{
	NativeSlice_1_t8E27FA1E73FC8A04E990C238360882D58A2DB2F9 ___m_Array;
	int32_t ___m_Index;
};
struct Enumerator_tCF46E085892034EDBFF803E557E99F3BF8CB62D0 
{
	NativeSlice_1_t72AA15A00D1B7CE9B7265E487D15133C9F091FD0 ___m_Array;
	int32_t ___m_Index;
};
struct Enumerator_tF986B8853D1A715E2F0D1DCE156E5D67E7523E24 
{
	NativeSlice_1_t4207DB5E16E1EF9068326562E97E3AA675F0DAF9 ___m_Array;
	int32_t ___m_Index;
};
struct Enumerator_tB36F2E08EAEB0D3BA01C0FAFD87C7DCAF20D54CC 
{
	List_1_t179992B095C528117EEF4782A1F45568BD2D51EA* ____list;
	int32_t ____index;
	int32_t ____version;
	BrickChunkAlloc_t95EB283E186F5DA1E74A8DDE415EB8E7ABFDF51B ____current;
};
struct Enumerator_tFD671053B13AF7C8FBC30F672971625C0F600A3F 
{
	Stack_1_tBFBA4533224CC5B6CFEF7F656B39ED3995773D0B* ____stack;
	int32_t ____version;
	int32_t ____index;
	BrickChunkAlloc_t95EB283E186F5DA1E74A8DDE415EB8E7ABFDF51B ____currentElement;
};
struct Enumerator_t4A592C7851C80D9500C9618A4D11695ADF899357 
{
	ReadOnly_t9871B20A93A393B27B059E7A519336F6963606D8 ___m_Array;
	int32_t ___m_Index;
	CellStreamingScratchBufferLayout_t9C3A198B967DCECDB79C97C05BEFAEA74E70077E ___value;
};
struct Enumerator_tFF4CAB0BAFB87654B2D40AD99F8A109F5DA20279 
{
	List_1_tF41D14F75BDD50A70D20F7CE0A3382E7D3667AB1* ____list;
	int32_t ____index;
	int32_t ____version;
	SerializedPerSceneCellList_tDF6654463D2F5052472326DE949ABE002ED92591 ____current;
};
struct Enumerator_t521563A30F8D283F4E2C505DA24D7567FEF2A5FA 
{
	List_1_tE6CA98E04E3B94E85D2BCDBF673C6550F7865EF4* ____list;
	int32_t ____index;
	int32_t ____version;
	StreamableCellDesc_t30C9692E80F3E2FD12CF286548763F9F4DE5048A ____current;
};
struct Enumerator_t59E02176F2EE4E264A4750514AA127E2E21FF5D9 
{
	List_1_tDB6FA8EDA7A26227B5CA100203EF7BAFA0FDBDDF* ____list;
	int32_t ____index;
	int32_t ____version;
	SingleRange_tB50C1C2B62BDC445BDBA41FD3CDC77A45A211BBC ____current;
};
struct Enumerator_tD9A65EE19A797992BB565E9671EAD6D7C5EC0347 
{
	List_1_t274EF879F062D96E9E5D05941FF15F5051FF20C2* ____list;
	int32_t ____index;
	int32_t ____version;
	ElementInsertionData_tA691432209201F6E803ADDFD410186B673527959 ____current;
};
struct EnumeratorType_tBBA1299017EB1D186E06A710951234C8CAB75EB7 
{
	int32_t ___value__;
};
struct EnumeratorType_t088402F32227BCF199894AC9E5FDB96C9584C721 
{
	int32_t ___value__;
};
struct EnumeratorType_tC5AFB390070AA4297697E4E9025029C829FF218E 
{
	int32_t ___value__;
};
struct EnumeratorType_t213060DC702D8C5D779B12D7E6CE6AFC2D7BB6CA 
{
	int32_t ___value__;
};
typedef Il2CppFullySharedGenericStruct IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479;
struct InlinedArray_1_t1CE3E035944588D067779CB78450605FD753F5F0 
{
	int32_t ___length;
	StateChangeMonitorTimeout_t155BA6A9DDCDB6474629D31735C037134B0C3049 ___firstValue;
	StateChangeMonitorTimeoutU5BU5D_tC50E6F92A1C5CE6397F411E0967B3A428FFF1C77* ___additionalValues;
};
struct InlinedArray_1_tED5DB880E229C88EB2F96B60C2BFBEC2CA7EC28C 
{
	int32_t ___length;
	OngoingAccountSelection_t8471F20105109B19CE04657B1C72EB8D23976956 ___firstValue;
	OngoingAccountSelectionU5BU5D_t0CC84AB78BA7B45022C3D6E08B0DADA7A61BCBD8* ___additionalValues;
};
struct ListQueryMatcher_1_t5888012D63B675F26A02C9BF208DC2C856BFBBC4  : public UQueryMatcher_tF8783F54BA397641240DA7025126F6301EF53FF8
{
	List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95* ___U3CmatchesU3Ek__BackingField;
};
struct ListQueryMatcher_1_t80B2C90F8E32F964B971ECBBAA0E811171012425  : public UQueryMatcher_tF8783F54BA397641240DA7025126F6301EF53FF8
{
	List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* ___U3CmatchesU3Ek__BackingField;
};
struct Allocator_t996642592271AAD9EE688F142741D512C07B5824 
{
	int32_t ___value__;
};
struct Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Center;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Extents;
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
struct LanguageDirection_t30A3B6BBCEE6A6F57641E4E008E0DCC40603558C 
{
	int32_t ___value__;
};
struct LightType_t2D4D43054E7473EECEB54493C0055AE074780234 
{
	int32_t ___value__;
};
struct PhysicsBody_t4D9A16A318F8485217B0416E238278FA77455CD0 
{
	PhysicsHandle_tEC3DCC38ABB8395068171070539ABF8309A854C2 ___m_PhysicsHandle;
};
struct PhysicsRotate_t3AF8BA583108282C8B59872CB621EC366BC042B0 
{
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___direction;
};
struct ProfilerMarker_tA256E18DA86EDBC5528CE066FC91C96EE86501AD 
{
	intptr_t ___m_Ptr;
};
struct PropertyPathPartKind_t82152825D88A0E450DDCE8503272A10595047F87 
{
	int32_t ___value__;
};
struct RenderHints_t4032FC4AB3FD946FD2A484865B8861730D9035E7 
{
	int32_t ___value__;
};
struct ScaleMode_t16AD656758EE54C56B3DA34FE4F2033C9C2EE13D 
{
	int32_t ___value__;
};
struct StreamingContextStates_t5EE358E619B251608A9327618C7BFE8638FC33C1 
{
	int32_t ___value__;
};
struct TypeCode_tBEF9BE86C8BCF5A6B82F3381219738D27804EF79 
{
	int32_t ___value__;
};
struct VertexAttribute_tF34C1B76F20CA4AEC9D606BCD37A8A0C4A24C9A6 
{
	int32_t ___value__;
};
struct VertexAttributeFormat_tD714C51E671502B116ACE5E23F042BA80649D32F 
{
	int32_t ___value__;
};
struct VertexFlags_t4332E863C191870E63009EBE5F6E77D6D176C4FF 
{
	uint16_t ___value__;
};
struct Visibility_t1AEE2D871B126DD95D6F1D3B36EC92FE0D489F47 
{
	int32_t ___value__;
};
struct VisibleLightFlags_t337DB92EFB0014AD6A250E1E45338B1194657CD8 
{
	int32_t ___value__;
};
struct VisualElementFlags_t4D1066E11400967A1A2DA7331391ACDC4AA14409 
{
	int32_t ___value__;
};
struct WhiteSpace_t05265AA4B0C087FAE778422383EA36AF84EDCDD2 
{
	int32_t ___value__;
};
struct Wrap_tDDCA30F27EC4967717380ECCC67C0818EFA8BC53 
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
struct float4x4_t2F0C8ED1AD9B4E39295F0A2D7418771B5F05CECA 
{
	float4_t545A994996126766890C1F28B43EA823F2410190 ___c0;
	float4_t545A994996126766890C1F28B43EA823F2410190 ___c1;
	float4_t545A994996126766890C1F28B43EA823F2410190 ___c2;
	float4_t545A994996126766890C1F28B43EA823F2410190 ___c3;
};
struct PolyType_t61D62010DE1E30694B2F823BDFBA120B776ABAD7 
{
	int32_t ___value__;
};
struct EndType_t44002443DAAFEF649DA75F399F4A5CE38B2D9126 
{
	int32_t ___value__;
};
struct JoinType_tCC38C44CB0E5130C3761EFECF52EBDDD9EEC417D 
{
	int32_t ___value__;
};
struct MaskMesh_tBA4A29153655DB02C99D6BF624A64795A084806B 
{
	RawSlice_t8512E18D23DE01C7FCF7249BCF43911D4138CE9A ___vertices;
	NativeSlice_1_t0D1A1AB7A9C4768B84EB7420D04A90920533C78A ___indices;
	int32_t ___indexOffset;
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
struct LightMeshVertex_tC5C456CBCF889B89B395DCE82DBEBA91BEA3015A 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___position;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___color;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___uv;
};
struct BackgroundRepeatInstance_t2D7A8E1F6278188BE2026DF769C49A975D38B12D 
{
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___rect;
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___backgroundRepeatRect;
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___uv;
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
struct TransformWriteMode_tAC88F83F00B2B74873BDD3631AC07283279591D4 
{
	int32_t ___value__;
};
struct TransformChangeReason_tB8D979EC98187EBE40684E468216D798FBFF12F4 
{
	int32_t ___value__;
};
struct Brick_tE6E9230DFDF650A631C116E79FB28F41618C3CE0 
{
	Vector3Int_t65CB06F557251D18A37BD71F3655BA836A357376 ___position;
	int32_t ___subdivisionLevel;
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
struct StpConstantBufferData_t12C7727CEDC1B8B7ECA73862CD7BB3F5D32B2754 
{
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ____StpCommonConstant;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ____StpSetupConstants0;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ____StpSetupConstants1;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ____StpSetupConstants2;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ____StpSetupConstants3;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ____StpSetupConstants4;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ____StpSetupConstants5;
	U3C_StpSetupPerViewConstantsU3Ee__FixedBuffer_t9BB7998676CF28C673FFF3872BE1163DFEDAB3F6 ____StpSetupPerViewConstants;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ____StpDilConstants0;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ____StpTaaConstants0;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ____StpTaaConstants1;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ____StpTaaConstants2;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ____StpTaaConstants3;
};
struct LightData_tAC4023737E9903DE3F96B993AA323E062ABCB9ED 
{
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___position;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___color;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___attenuation;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___spotDirection;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___occlusionProbeChannels;
	uint32_t ___layerMask;
};
struct ShadowMeshVertex_t79CEE224683C48457A66B224C228E3A6F7529B79 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___position;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___tangent;
};
struct SettingsOptions_t935A71692F21EE9D959FCAC00574AA018F8BA2CB 
{
	uint16_t ___value__;
};
struct Flags_t36D5070200D1061BA68BF41808748FA000DEE57C 
{
	int32_t ___value__;
};
struct Enumerator_tA2DAF234D0BEBCCA6944A6E989C24F6A56AF9EE1 
{
	List_1_t7B0B015A57215073C45C813320AF5F3ECF8149F7* ____list;
	int32_t ____index;
	int32_t ____version;
	XRView_t70E23918C68E24DB22A8614AA8717B3BA213FAB5 ____current;
};
typedef Il2CppFullySharedGenericStruct Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE;
struct Enumerator_t68131B3CCF46CB6AE538F8487260FD636EA8416D 
{
	ReadOnly_t86B3AB6727FB0CF3474F5B7BEBDA30FC9F9951D2 ___m_Array;
	int32_t ___m_Index;
	float4x4_t2F0C8ED1AD9B4E39295F0A2D7418771B5F05CECA ___value;
};
struct Enumerator_t3B367DB4E88110F0F90313AF159B54F9F2189310 
{
	Stack_1_tAE809352224B7904D166288D1476FD4C2ED0D631* ____stack;
	int32_t ____version;
	int32_t ____index;
	MaskMesh_tBA4A29153655DB02C99D6BF624A64795A084806B ____currentElement;
};
struct Enumerator_tAEA8E19211BD601F53CB8150B6341FA05DA45454 
{
	HashSet_1_t0F16759FF079A28DCA2DCF39BA6A61B5D502A475* ____set;
	int32_t ____index;
	int32_t ____version;
	int32_t ____current;
};
struct Enumerator_t7C6C9B187BC566CC3B1B0C1686A9D10BC6044162 
{
	List_1_t08DDDF84D3AEA5E13828BE225CE2A4720A7CFB64* ____list;
	int32_t ____index;
	int32_t ____version;
	Registration_t4882E3A82099DD20B2E91917C08BD4D2216F21D7 ____current;
};
struct Enumerator_t8B6BE579C1A15C04149CE3D2A9DF092A98FAC1B5 
{
	List_1_t5C9CFABFCE231BB13B0FAEDE274BB6E94B922A00* ____list;
	int32_t ____index;
	int32_t ____version;
	CopyInfo_t633133D45AECBBD5429A958816DD143830F31B29 ____current;
};
struct Enumerator_t21468C09B8883BEC5302E6D499F204701ACC4DAD 
{
	InlinedArray_1_t1CE3E035944588D067779CB78450605FD753F5F0 ___array;
	int32_t ___index;
};
struct Enumerator_tBEDEF8A2AEFA29BDA7D7F473A7ED608B295FB066 
{
	InlinedArray_1_tED5DB880E229C88EB2F96B60C2BFBEC2CA7EC28C ___array;
	int32_t ___index;
};
struct Enumerator_t8232AA9389A7B7F6B771DFCE909748D371237BD4 
{
	ReadOnly_t5F3121537F132F255E330E9C75EF3CE5321D6987 ___m_Array;
	int32_t ___m_Index;
	LightMeshVertex_tC5C456CBCF889B89B395DCE82DBEBA91BEA3015A ___value;
};
struct Enumerator_t85BE84590B45EA87CA607680BCD144728A680AE1 
{
	ReadOnly_tEFEB81D67B96BE28BD71A1DEB8AE16EA6C902AC3 ___m_Array;
	int32_t ___m_Index;
	BackgroundRepeatInstance_t2D7A8E1F6278188BE2026DF769C49A975D38B12D ___value;
};
struct Enumerator_t19FD574FED1A42A647A84C93FD57BE2E72B5CC75 
{
	List_1_t0325EC485D0C783FC73CF8D1F8C84A6628937599* ____list;
	int32_t ____index;
	int32_t ____version;
	RepeatRectUV_tD14FA437CD80FC82B2ED78B34A201F3C91416A7C ____current;
};
struct Enumerator_t7EDB14F9243C2DF14DF7DC6C22009481005E807E 
{
	List_1_tF43D0DA1B22D0A91FBF53BD9D3F2134C10703D4D* ____list;
	int32_t ____index;
	int32_t ____version;
	AllocToFree_tE4DBFDF44B21D35677B13570BA217FDAE35476CB ____current;
};
struct Enumerator_t1C8FB1321AA452373F3AB56E9C1D7BE1DBA71DAB 
{
	List_1_tE8267DC50323E6835C1F1668CBED621535AD99C4* ____list;
	int32_t ____index;
	int32_t ____version;
	AllocToUpdate_t7E5493AF0DAEBF6C8D1E005C93179042069A46A3 ____current;
};
struct Enumerator_tEB79610A8CCBF4F4710B0F6E1829EA0139BCBAA2 
{
	ReadOnly_t61D470FA128F3A1F6BD4A67BA697EE8D002A1F91 ___m_Array;
	int32_t ___m_Index;
	Brick_tE6E9230DFDF650A631C116E79FB28F41618C3CE0 ___value;
};
struct Enumerator_tBC1A2915FA501745EF34972D12D59D8AC7ABD0D7 
{
	List_1_tD554524C4D3CE5612DAE24A61267C5A8170B530A* ____list;
	int32_t ____index;
	int32_t ____version;
	ObsoleteSerializablePerScenarioDataItem_t4DB1ADB8C2522C62B43309AF1874419D8572EEB7 ____current;
};
struct Enumerator_t6138A7577EFE976D77B2B547FB797E4866987E50 
{
	ReadOnly_t37CCA313F8D670925051D441B669A6C13037061F ___m_Array;
	int32_t ___m_Index;
	StpConstantBufferData_t12C7727CEDC1B8B7ECA73862CD7BB3F5D32B2754 ___value;
};
struct Enumerator_t10CBBC4ABAE537F4FB58D10E9A89F22F8B36FE88 
{
	ReadOnly_t890D24C3B04F979FD5F2961BC38BD5F2B796D9B6 ___m_Array;
	int32_t ___m_Index;
	LightData_tAC4023737E9903DE3F96B993AA323E062ABCB9ED ___value;
};
struct Enumerator_t3B9B063208074C5E566A7945D75943BF85B63EF9 
{
	ReadOnly_tDCEED2CFBEB31EA295986393AC6277E98D2FF755 ___m_Array;
	int32_t ___m_Index;
	ShadowMeshVertex_t79CEE224683C48457A66B224C228E3A6F7529B79 ___value;
};
struct IndexedCollectionPropertyBagEnumerator_1_t54E5EC43320B43386BAC9BD95919BC8AFF96CDC1 
{
	RuntimeObject* ___m_Impl;
	IndexedCollectionSharedPropertyState_t1E4ED168A72271A87F5FCFCBBF19BBB63F95FC79 ___m_Previous;
	int32_t ___m_Container;
	int32_t ___m_Position;
};
struct IndexedCollectionPropertyBagEnumerator_1_t525D86DA1644A0BE3CFE6B0578CECA57A52391BB 
{
	RuntimeObject* ___m_Impl;
	IndexedCollectionSharedPropertyState_t1E4ED168A72271A87F5FCFCBBF19BBB63F95FC79 ___m_Previous;
	int32_t ___m_Container;
	int32_t ___m_Position;
};
struct IndexedCollectionPropertyBagEnumerator_1_t9066C19946C6B9502B883FAF4B0AA9FB74B47690 
{
	RuntimeObject* ___m_Impl;
	IndexedCollectionSharedPropertyState_t1E4ED168A72271A87F5FCFCBBF19BBB63F95FC79 ___m_Previous;
	int32_t ___m_Container;
	int32_t ___m_Position;
};
struct NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_t07975297AD7F7512193094A7C0703BA872EF7A7B 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_tB60512C6E4578B7CC8EB79321680E495E69ABF81 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_t71485A1E60B31CCAD3E525C907CF172E8B804468 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_t3CC4179AFF43449D3539816D3EE4D2D69B6022E8 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_tCB972646B31CE26CF7D4A25C752E15BFC8E1ACD9 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_tB2C0A4BD42DDC767D3B622ACDDD0A5E3898E0F9C 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_t4CB8C2A6DBEDF6C671F933ADC9E21367D90886F3 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_t38B974FEBDE9C6BC682E4154FA808DE4F13EF788 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_tDCAD524EEC8B9DA68A8DF4A0B6C04058522C0E2F 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_tB7ABBF577BB1C93E1A5C402B4AB54D049F98738D 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_t6A78CEABEA568D17A3863F22D6B40CC1F747EBA7 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_t2E87C5C3A9E2CE7790C5229150C0DEDA038B4A62 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_t9086C71A5110879F9F76FDDC0009F7C7CF9D1B36 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_t2FEAC97D235FA3AC2CF64ECF72C860EAA0F44429 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_tD8A05F96A9BE1CAAA4C877FA4B9D07ABD8E909E1 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_t9F2DDC824E9AE9423E5F4D96E7EC4B5DC22B2E26 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_t660F0CEB8330CBEC88358E4FDD5795CBFB551416 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_tA250DFA6F2B09B543458A4231692763973E84C57 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_tEE0716F057E5AB90C1E3B19EA5CC04D624CFC9C7 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_tA322013FCE7ECBB07A30418AAB23BE082C4E46D6 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_t7D2E0867BA2C51E53A2DA86335020B175E7CDC86 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_tD433A29296E2B6926BA29A86B35FEEA6C63C8C68 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_tBA7C3E15B67AEC95EB7BED61838A132B300A973A 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_tCD7F13E096379BABA733E32276390D01F78F8D59 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_t3B6D299C308C4B6D24A1EE8D506A093CCB0EABDE 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_tCD560B25A8B4F8654C3DF3626A5F13D31353E25A 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_t6FF0646F3439F6B23FA364501169F60B17BCBD01 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
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
struct PhysicsTransform_tB8A6E5FBEECAEFA43E89B0975A2D617AC701E434 
{
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___position;
	PhysicsRotate_t3AF8BA583108282C8B59872CB621EC366BC042B0 ___rotation;
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
struct StreamingContext_t56760522A751890146EE45F82F866B55B7E33677 
{
	RuntimeObject* ___m_additionalContext;
	int32_t ___m_state;
};
struct StreamingContext_t56760522A751890146EE45F82F866B55B7E33677_marshaled_pinvoke
{
	Il2CppIUnknown* ___m_additionalContext;
	int32_t ___m_state;
};
struct StreamingContext_t56760522A751890146EE45F82F866B55B7E33677_marshaled_com
{
	Il2CppIUnknown* ___m_additionalContext;
	int32_t ___m_state;
};
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};
struct UnsafeMeshGenerationNode_t1FF7D38D9B39DC59A2F8E2DFC92BFAB2481E88A2 
{
	GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC ___m_Handle;
};
struct Vertex_t016AC68A2E6C62576E65412BEC71544AFC01AFC7 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___position;
	Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B ___tint;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___uv;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___layoutUV;
	uint16_t ___clipRectId;
	uint16_t ___elementId;
	uint16_t ___dynamicColorOrTextCoreId;
	uint16_t ____reserved2;
	uint16_t ___flags;
	uint16_t ___textureId;
	uint16_t ___svgGradientIndex;
	uint16_t ____reserved;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___circle;
};
struct VertexAttributeDescriptor_tD4231FBF57335465D16308D2A18E8E83D36BFA76 
{
	int32_t ___U3CattributeU3Ek__BackingField;
	int32_t ___U3CformatU3Ek__BackingField;
	int32_t ___U3CdimensionU3Ek__BackingField;
	int32_t ___U3CstreamU3Ek__BackingField;
};
struct VisibleLight_t0A4DF5B22865A00F618A0352B805277FA0132805 
{
	int32_t ___m_LightType;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___m_FinalColor;
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___m_ScreenRect;
	Matrix4x4_tDB70CF134A14BA38190C59AA700BCE10E2AED3E6 ___m_LocalToWorldMatrix;
	float ___m_Range;
	float ___m_SpotAngle;
	float ___m_InnerSpotAngle;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___m_AreaSize;
	float ___m_ShapeRadius;
	EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 ___m_EntityId;
	int32_t ___m_Flags;
};
struct VisibleReflectionProbe_t8AF1FAD09A36E33F5101B683DB8E99582815EF0B 
{
	Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3 ___m_Bounds;
	Matrix4x4_tDB70CF134A14BA38190C59AA700BCE10E2AED3E6 ___m_LocalToWorldMatrix;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___m_HdrData;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Center;
	float ___m_BlendDistance;
	int32_t ___m_Importance;
	int32_t ___m_BoxProjection;
	EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 ___m_EntityId;
	EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 ___m_TextureId;
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
struct PathArguments_t445220F54870AED8F8384306CF72A316887AD98C 
{
	int32_t ___polyType;
	bool ___closed;
};
struct PathArguments_t445220F54870AED8F8384306CF72A316887AD98C_marshaled_pinvoke
{
	int32_t ___polyType;
	int32_t ___closed;
};
struct PathArguments_t445220F54870AED8F8384306CF72A316887AD98C_marshaled_com
{
	int32_t ___polyType;
	int32_t ___closed;
};
struct PathArguments_t0C63AB02A9EC1ED9707C0320A8C39254200305F1 
{
	int32_t ___joinType;
	int32_t ___endType;
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
struct TransformChangeEvent_tAE5E62820ECA07C3DA656E95A0426D046DBF1A70 
{
	EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 ___m_TransformId;
	int32_t ___m_ChangeReason;
};
struct Enumerator_t1589EC4D2EC492C49806E46445B120B5062C57FF 
{
	NativeArray_1_tB60512C6E4578B7CC8EB79321680E495E69ABF81 ___m_Array;
	int32_t ___m_Index;
	Vertex_t016AC68A2E6C62576E65412BEC71544AFC01AFC7 ___value;
};
struct Enumerator_tE8F3A5178B689AF458B541610BA2119EF12379EE 
{
	ReadOnly_tEC1B78AA9A46DB8AD1C5DBAE542E1B5A9A8DF8CE ___m_Array;
	int32_t ___m_Index;
	Vertex_t016AC68A2E6C62576E65412BEC71544AFC01AFC7 ___value;
};
struct Enumerator_t63330B4094998948CC09317F806BD56E90FFF8EC 
{
	List_1_tA7CAD0AC45F81CB3151C43DB534729898937504A* ____list;
	int32_t ____index;
	int32_t ____version;
	VertexAttributeDescriptor_tD4231FBF57335465D16308D2A18E8E83D36BFA76 ____current;
};
struct Enumerator_t445F87EEA6E6AC92E1BA186B7AE8DDD5514AAD55 
{
	int32_t ___m_Type;
	RuntimeObject* ___m_Enumerator;
	Enumerator_t8925FB47C03812518E4A2FA72614F9DF6E7BC900 ___m_Properties;
	IndexedCollectionPropertyBagEnumerator_1_t54E5EC43320B43386BAC9BD95919BC8AFF96CDC1 ___m_IndexedCollectionPropertyBag;
	RuntimeObject* ___U3CCurrentU3Ek__BackingField;
};
struct Enumerator_t74DC91310B1C5EF28A7D5F1AEBE73D592C5CBFFE 
{
	NativeArray_1_t71485A1E60B31CCAD3E525C907CF172E8B804468 ___m_Array;
	int32_t ___m_Index;
	VisibleLight_t0A4DF5B22865A00F618A0352B805277FA0132805 ___value;
};
struct Enumerator_t6D1A6277C457F0F0D2DA0733F9EA3BBBA5638FA2 
{
	ReadOnly_tDAD0456355433E3A9DB77C242419A3C2B61FF7A1 ___m_Array;
	int32_t ___m_Index;
	VisibleLight_t0A4DF5B22865A00F618A0352B805277FA0132805 ___value;
};
struct Enumerator_tC191293166C9A01771C37368CC8027CCA916B2F2 
{
	NativeArray_1_t3CC4179AFF43449D3539816D3EE4D2D69B6022E8 ___m_Array;
	int32_t ___m_Index;
	VisibleReflectionProbe_t8AF1FAD09A36E33F5101B683DB8E99582815EF0B ___value;
};
struct Enumerator_tB4C80809F9AC66D159A59227A84147DCD5A80E65 
{
	ReadOnly_t9EC554A4C51E0E58FA55ED6550A28C34131B53F8 ___m_Array;
	int32_t ___m_Index;
	VisibleReflectionProbe_t8AF1FAD09A36E33F5101B683DB8E99582815EF0B ___value;
};
struct Enumerator_t3896F534A371E0D4D3C06F6A05FA1284B79DB1B0 
{
	int32_t ___m_Type;
	RuntimeObject* ___m_Enumerator;
	Enumerator_t3DB6B256496A78585890809B8DADD57AFDDEBD49 ___m_Properties;
	IndexedCollectionPropertyBagEnumerator_1_t525D86DA1644A0BE3CFE6B0578CECA57A52391BB ___m_IndexedCollectionPropertyBag;
	RuntimeObject* ___U3CCurrentU3Ek__BackingField;
};
struct Enumerator_tD8848D63FB8F3042E340D0B7701DABCFD33C3074 
{
	int32_t ___m_Type;
	RuntimeObject* ___m_Enumerator;
	Enumerator_tE6F11662226BE79E82B357013873FADA64A5444C ___m_Properties;
	IndexedCollectionPropertyBagEnumerator_1_t9066C19946C6B9502B883FAF4B0AA9FB74B47690 ___m_IndexedCollectionPropertyBag;
	RuntimeObject* ___U3CCurrentU3Ek__BackingField;
};
struct Enumerator_tB41744EFD7274B3F0B75074E72D0EB8C658D06A0 
{
	List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* ____list;
	int32_t ____index;
	int32_t ____version;
	XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ____current;
};
typedef Il2CppFullySharedGenericStruct Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405;
struct Enumerator_tE1470541970778078D070FF8C523A55C7301DA38 
{
	NativePagedList_1_t9B43BA7543672C8F12763C42AC5C493E40A41AA8* ___m_NativePagedList;
	NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 ___m_CurrentPage;
	int32_t ___m_IndexInCurrentPage;
	int32_t ___m_IndexOfCurrentPage;
	int32_t ___m_CountInCurrentPage;
};
struct Enumerator_t086F40832AA89EEACA905FF48964FD071FE85EDF 
{
	NativeArray_1_tCB972646B31CE26CF7D4A25C752E15BFC8E1ACD9 ___m_Array;
	int32_t ___m_Index;
	double2_t2124FB15A5BDCC0BD48B80116D4B24DC9DCA9D83 ___value;
};
struct Enumerator_tC5036FB16ECE7E07ADB3653C24C04104449783FA 
{
	NativeArray_1_tB2C0A4BD42DDC767D3B622ACDDD0A5E3898E0F9C ___m_Array;
	int32_t ___m_Index;
	float2_t3398D7209CB6D09FEA5C49AC55070C5E7BE0B2A5 ___value;
};
struct Enumerator_t2B31801724586ED01AF5D5736CA9B763A0C445FD 
{
	NativeArray_1_t4CB8C2A6DBEDF6C671F933ADC9E21367D90886F3 ___m_Array;
	int32_t ___m_Index;
	float3_t7600B73F092B37F484B12910A5269F30C778D31E ___value;
};
struct Enumerator_t43E2F6549F3C419F46925938F040B2F269217D30 
{
	NativeArray_1_t38B974FEBDE9C6BC682E4154FA808DE4F13EF788 ___m_Array;
	int32_t ___m_Index;
	float4_t545A994996126766890C1F28B43EA823F2410190 ___value;
};
struct Enumerator_t971AC6043461572DD98D6C4A4DEE5AD75327A648 
{
	NativeArray_1_tDCAD524EEC8B9DA68A8DF4A0B6C04058522C0E2F ___m_Array;
	int32_t ___m_Index;
	float4x4_t2F0C8ED1AD9B4E39295F0A2D7418771B5F05CECA ___value;
};
struct Enumerator_t090003D42EA14BAA8DE5177EAB88BDD1392AC0C4 
{
	NativeArray_1_tB7ABBF577BB1C93E1A5C402B4AB54D049F98738D ___m_Array;
	int32_t ___m_Index;
	int2_tB123E869131C6102341F8F9AA01726E0E7C2768D ___value;
};
struct Enumerator_tF8DE0BBE31E8D8484EC60B3602F9C7E01F0290F2 
{
	NativeArray_1_t6A78CEABEA568D17A3863F22D6B40CC1F747EBA7 ___m_Array;
	int32_t ___m_Index;
	int3_t18873F121F1EF24ECE875EAB01170B69E1B498C5 ___value;
};
struct Enumerator_tC3F4B7B9A0CD30697539D72194DF92D20F79A13A 
{
	NativeArray_1_t2E87C5C3A9E2CE7790C5229150C0DEDA038B4A62 ___m_Array;
	int32_t ___m_Index;
	int4_t6DC3FEACC0DD1EAE4D5B897188E8D669BC4DAB90 ___value;
};
struct Enumerator_tC5B56081C1DBEB30B3BB412C462F018E7DD0BF3E 
{
	List_1_t201EF66D89B48BDF1B448F483575106DF0B49F6A* ____list;
	int32_t ____index;
	int32_t ____version;
	ShadowResolutionRequest_tC1B869ADCA139D8D7AD8A355373F84C7F5E0FCBE ____current;
};
struct Enumerator_t33C92E0F6D3A7C1A0F01C3557B9D1BBEEDB9F871 
{
	NativeArray_1_t9086C71A5110879F9F76FDDC0009F7C7CF9D1B36 ___m_Array;
	int32_t ___m_Index;
	ShadowResolutionRequest_tC1B869ADCA139D8D7AD8A355373F84C7F5E0FCBE ___value;
};
struct Enumerator_t46AA23A6472FC599A8A00F6E2EFEABE48FDCEAFA 
{
	ReadOnly_t0FE5617AC8525F60705BB39B5A7AD67C85F26B8D ___m_Array;
	int32_t ___m_Index;
	ShadowResolutionRequest_tC1B869ADCA139D8D7AD8A355373F84C7F5E0FCBE ___value;
};
struct Enumerator_t3148939C1214EFF0F7118344B902FEA23F0F5BD2 
{
	NativeArray_1_t2FEAC97D235FA3AC2CF64ECF72C860EAA0F44429 ___m_Array;
	int32_t ___m_Index;
	PathArguments_t445220F54870AED8F8384306CF72A316887AD98C ___value;
};
struct Enumerator_tCD1FA3B480A174648321AAB7AC8537861D9768C4 
{
	ReadOnly_t490AE34C6A92FDCF1F34BA02E3FE19FE48B355BC ___m_Array;
	int32_t ___m_Index;
	PathArguments_t445220F54870AED8F8384306CF72A316887AD98C ___value;
};
struct Enumerator_t8C359D04270DDAE57CB000C049017C891E719625 
{
	NativeArray_1_tD8A05F96A9BE1CAAA4C877FA4B9D07ABD8E909E1 ___m_Array;
	int32_t ___m_Index;
	PathArguments_t0C63AB02A9EC1ED9707C0320A8C39254200305F1 ___value;
};
struct Enumerator_t3B1BFB6AD4B8AC74A45B2A5032FF15971DAFC1C2 
{
	ReadOnly_tD24F078322DD82D0B34548D94F195EEF89677EE0 ___m_Array;
	int32_t ___m_Index;
	PathArguments_t0C63AB02A9EC1ED9707C0320A8C39254200305F1 ___value;
};
struct Enumerator_tFB3B1F600FA34F8D319BB9BDCB184D6AA150C8B9 
{
	NativeArray_1_t9F2DDC824E9AE9423E5F4D96E7EC4B5DC22B2E26 ___m_Array;
	int32_t ___m_Index;
	CpuCopyRange_tCE4B2EBDC2B62D128407AE7B9484BB04BBC8FE93 ___value;
};
struct Enumerator_tD793BFBCC557EC7095B58A33418A2627510CBDF8 
{
	List_1_t3858E48BB083F1FE977819934ACB97F9BC44439A* ____list;
	int32_t ____index;
	int32_t ____version;
	HIDCollectionDescriptor_t9235094BD1880EA02E4E07F9C02AC98088EFB9F7 ____current;
};
struct Enumerator_t07EE6E0C44AB43A1D7E0C70F53CC5E680B1C7A60 
{
	List_1_t66029D02EA129D03A082E470EB98C85812301A0C* ____list;
	int32_t ____index;
	int32_t ____version;
	HIDElementDescriptor_t18CA9618F730F541BA1C873745A1AE71823D4CED ____current;
};
struct Enumerator_t5F87CC2FDCCD2D764C0136761C957B6C9F7D9A5B 
{
	List_1_t268ADAE63E623B42094720668F8DB571CF6085E6* ____list;
	int32_t ____index;
	int32_t ____version;
	HIDReportData_t2F0D5DFC9087B851FA3288EFBB6FF4EAA863BB4F ____current;
};
struct Enumerator_t06B71EF17663E35C7B0EA1A12263D9A5C5E116EB 
{
	List_1_t4A80BCCFB0BC8742C8BB601365DB07226750573A* ____list;
	int32_t ____index;
	int32_t ____version;
	JsonValue_t01DB320267C848E729A400EF2345979978F851D2 ____current;
};
struct Enumerator_tCFE1DE9DA7F9BF17D3449FCADEF8BA3526F18A03 
{
	NativeArray_1_t660F0CEB8330CBEC88358E4FDD5795CBFB551416 ___m_Array;
	int32_t ___m_Index;
	LightMeshVertex_tC5C456CBCF889B89B395DCE82DBEBA91BEA3015A ___value;
};
struct Enumerator_tE5C83163901E84EB8F5AD608580161BB0729C083 
{
	NativeArray_1_tA250DFA6F2B09B543458A4231692763973E84C57 ___m_Array;
	int32_t ___m_Index;
	BackgroundRepeatInstance_t2D7A8E1F6278188BE2026DF769C49A975D38B12D ___value;
};
struct Enumerator_tCBE8E11D5B4C1E8C02BA9D45CEDCE4F2E0219FAA 
{
	NativePagedList_1_tEBFD84343834CFE74C052BA60816E83FEE4F06DC* ___m_NativePagedList;
	NativeArray_1_tA250DFA6F2B09B543458A4231692763973E84C57 ___m_CurrentPage;
	int32_t ___m_IndexInCurrentPage;
	int32_t ___m_IndexOfCurrentPage;
	int32_t ___m_CountInCurrentPage;
};
struct Enumerator_t5809206050C0BE7F4A4228BC1153D175763C0D07 
{
	NativeArray_1_tD433A29296E2B6926BA29A86B35FEEA6C63C8C68 ___m_Array;
	int32_t ___m_Index;
	TransformChangeEvent_tAE5E62820ECA07C3DA656E95A0426D046DBF1A70 ___value;
};
struct Enumerator_tE20E0347D15FCE77AACC4CF08E0FB67F3CCB44A3 
{
	ReadOnly_tE2BB0F75963087A7149C3EA4E51899E26C046D87 ___m_Array;
	int32_t ___m_Index;
	TransformChangeEvent_tAE5E62820ECA07C3DA656E95A0426D046DBF1A70 ___value;
};
struct Enumerator_t0C8B9F05C0E0EF3B118254A800114418CE435281 
{
	NativeArray_1_tBA7C3E15B67AEC95EB7BED61838A132B300A973A ___m_Array;
	int32_t ___m_Index;
	Brick_tE6E9230DFDF650A631C116E79FB28F41618C3CE0 ___value;
};
struct Enumerator_t8BA6465E9B1D0097C698B6786A411F189058A62C 
{
	NativeArray_1_tCD7F13E096379BABA733E32276390D01F78F8D59 ___m_Array;
	int32_t ___m_Index;
	CellStreamingScratchBufferLayout_t9C3A198B967DCECDB79C97C05BEFAEA74E70077E ___value;
};
struct Enumerator_tE149D72F20A3FEB2D075973F8D99C6607C0C8BBB 
{
	NativeArray_1_t3B6D299C308C4B6D24A1EE8D506A093CCB0EABDE ___m_Array;
	int32_t ___m_Index;
	StpConstantBufferData_t12C7727CEDC1B8B7ECA73862CD7BB3F5D32B2754 ___value;
};
struct Enumerator_t46342BF1DCF08FD127D86B7A3F43B24E51F97AA2 
{
	NativeArray_1_tCD560B25A8B4F8654C3DF3626A5F13D31353E25A ___m_Array;
	int32_t ___m_Index;
	LightData_tAC4023737E9903DE3F96B993AA323E062ABCB9ED ___value;
};
struct Enumerator_t7B04F2FA5B9F11110FBE27F4E81ADEC116B0B266 
{
	NativeArray_1_t6FF0646F3439F6B23FA364501169F60B17BCBD01 ___m_Array;
	int32_t ___m_Index;
	ShadowMeshVertex_t79CEE224683C48457A66B224C228E3A6F7529B79 ___value;
};
struct InputControlList_1_t84F2DA44C9BD66500A2844A168208BD391B51BF4 
{
	int32_t ___m_Count;
	NativeArray_1_t07975297AD7F7512193094A7C0703BA872EF7A7B ___m_Indices;
	int32_t ___m_Allocator;
};
struct NativeArrayAllocator_t7D098BCF0B0C50F493B510D50B8986211C63F492 
{
	int32_t ___m_Allocator;
	MemoryLabel_t29CE7AB312D2ED888B444BBE5D452F7132EC9DB2 ___m_MemoryLabel;
};
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
	String_t* ____paramName;
};
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
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
struct OnScreenDeviceInfo_t2C7BB082C4486C5F8F0FE55F0BFA772B454AD0AC 
{
	InputEventPtr_tC2A58521C9AFB479CC88789D5E0797D817C721C0 ___eventPtr;
	NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF ___buffer;
	InputDevice_t8BCF67533E872A75779C24C93D1D7085B72D364B* ___device;
	OnScreenControl_t449BC1FA3DCA3F7787643FAB4F4B1906A7B32418* ___firstControl;
};
struct OnScreenDeviceInfo_t2C7BB082C4486C5F8F0FE55F0BFA772B454AD0AC_marshaled_pinvoke
{
	InputEventPtr_tC2A58521C9AFB479CC88789D5E0797D817C721C0 ___eventPtr;
	NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF ___buffer;
	InputDevice_t8BCF67533E872A75779C24C93D1D7085B72D364B* ___device;
	OnScreenControl_t449BC1FA3DCA3F7787643FAB4F4B1906A7B32418* ___firstControl;
};
struct OnScreenDeviceInfo_t2C7BB082C4486C5F8F0FE55F0BFA772B454AD0AC_marshaled_com
{
	InputEventPtr_tC2A58521C9AFB479CC88789D5E0797D817C721C0 ___eventPtr;
	NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF ___buffer;
	InputDevice_t8BCF67533E872A75779C24C93D1D7085B72D364B* ___device;
	OnScreenControl_t449BC1FA3DCA3F7787643FAB4F4B1906A7B32418* ___firstControl;
};
struct Painter2DJobData_t227572FEAE4A071ED0378501E752A72FF0ACC4EF 
{
	UnsafeMeshGenerationNode_t1FF7D38D9B39DC59A2F8E2DFC92BFAB2481E88A2 ___node;
	int32_t ___snapshotIndex;
	intptr_t ___vectorImagePtr;
	intptr_t ___texturePtr;
	int32_t ___userData;
};
struct TransformWriteTween_t86FB859350EF146AC5D9CB4CA8196377B714E2B7 
{
	PhysicsBody_t4D9A16A318F8485217B0416E238278FA77455CD0 ___m_Body;
	int32_t ___m_TransformWriteMode;
	PhysicsTransform_tB8A6E5FBEECAEFA43E89B0975A2D617AC701E434 ___m_PhysicsTransform;
	EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 ___m_TransformId;
	int32_t ___m_TransformDepth;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___m_LinearVelocity;
	float ___m_AngularVelocity;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_PositionFrom;
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___m_RotationFrom;
};
struct Enumerator_t70B197A0E6BB9C539CCD911D9CF29D8A4D917AF0 
{
	List_1_tBF847DD77CE898FDD304012AB5DFBFC15986D8F7* ____list;
	int32_t ____index;
	int32_t ____version;
	ControlItem_t25B2C46F52E78ADC5F54903F9E769364B02CD4AD ____current;
};
struct Enumerator_tFED847A03EEFF7E28B25AA2B377A552A8EBABE61 
{
	List_1_tC96558A2A77DD595C724D43BFE409C769AB0FA1E* ____list;
	int32_t ____index;
	int32_t ____version;
	TessellationJobParameters_tA2407D5C15761590BDD217DDE4861964544E8CE2 ____current;
};
struct Enumerator_tD420FB6306A5A190AEF85BCF79A6B30FED15DB15 
{
	NativeArray_1_tEE0716F057E5AB90C1E3B19EA5CC04D624CFC9C7 ___m_Array;
	int32_t ___m_Index;
	TessellationJobParameters_tA2407D5C15761590BDD217DDE4861964544E8CE2 ___value;
};
struct Enumerator_tB6D64401D53F5341B44E0546404E4C4A1A53E5AE 
{
	ReadOnly_t641867D1516A48A7C44E7421FB5FB2267FAAEA18 ___m_Array;
	int32_t ___m_Index;
	TessellationJobParameters_tA2407D5C15761590BDD217DDE4861964544E8CE2 ___value;
};
struct Enumerator_tAD8E8D1D38AFDA6F5EC89384F3705EABD7A30912 
{
	List_1_t46384C53612EB5905F3A9C122B722D9C70F755A7* ____list;
	int32_t ____index;
	int32_t ____version;
	Painter2DJobData_t227572FEAE4A071ED0378501E752A72FF0ACC4EF ____current;
};
struct Enumerator_tE7C72126E5AA72EDE3009D3A01F16D2457924FDC 
{
	NativeArray_1_tA322013FCE7ECBB07A30418AAB23BE082C4E46D6 ___m_Array;
	int32_t ___m_Index;
	Painter2DJobData_t227572FEAE4A071ED0378501E752A72FF0ACC4EF ___value;
};
struct Enumerator_t7F93152EA545C49005225C716A9E215EEDB60AB8 
{
	ReadOnly_tA25B512569688CCD186105D5826DF7B6D790737F ___m_Array;
	int32_t ___m_Index;
	Painter2DJobData_t227572FEAE4A071ED0378501E752A72FF0ACC4EF ___value;
};
struct Enumerator_tFCDE153118E71F9E3C3F2321A23EBF46516791FA 
{
	NativeArray_1_t7D2E0867BA2C51E53A2DA86335020B175E7CDC86 ___m_Array;
	int32_t ___m_Index;
	TransformWriteTween_t86FB859350EF146AC5D9CB4CA8196377B714E2B7 ___value;
};
struct Enumerator_tDD8D062448D584024D08E2F00DAA1019BC6B1898 
{
	ReadOnly_tCE5F8D70DB00F29CFA4C645ED1910D8EFEFD0CFB ___m_Array;
	int32_t ___m_Index;
	TransformWriteTween_t86FB859350EF146AC5D9CB4CA8196377B714E2B7 ___value;
};
struct InlinedArray_1_tC8A32AF03BC5EA969AD4315DC5E67BBAF2765992 
{
	int32_t ___length;
	OnScreenDeviceInfo_t2C7BB082C4486C5F8F0FE55F0BFA772B454AD0AC ___firstValue;
	OnScreenDeviceInfoU5BU5D_t0C70881971941DEFDD18CFFD3E4133B5CA0B7F30* ___additionalValues;
};
struct NativePagedList_1_t9B43BA7543672C8F12763C42AC5C493E40A41AA8  : public RuntimeObject
{
	int32_t ___k_PoolCapacity;
	List_1_tDA1565FB54D9C701D544C5C5DEB5F20D33BC07BF* ___m_Pages;
	NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 ___m_LastPage;
	int32_t ___m_CountInLastPage;
	NativeArrayAllocator_t7D098BCF0B0C50F493B510D50B8986211C63F492 ___m_FirstPageAllocator;
	NativeArrayAllocator_t7D098BCF0B0C50F493B510D50B8986211C63F492 ___m_OtherPagesAllocator;
	List_1_tCC0A1A2DFE580C45F2D3D390F3E50E3A3CF6FE3A* ___m_Enumerator;
	bool ___U3CdisposedU3Ek__BackingField;
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
struct LayoutDataAccess_t99AA56349D0BC76F3742B927F4F0DDB21511FBBA 
{
	int32_t ___m_Manager;
	UnmanagedDataStore_t66CFDF2DBB3C86F8A58F5B3EBEE5E9537BDF2759 ___m_Nodes;
	UnmanagedDataStore_t66CFDF2DBB3C86F8A58F5B3EBEE5E9537BDF2759 ___m_Configs;
};
struct PlatformNotSupportedException_tD2BD7EB9278518AA5FE8AE75AD5D0D4298A4631A  : public NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A
{
};
struct Enumerator_t731FDF0DE7AAD6A5EDBF34D30D4A269BFFA52C11 
{
	InlinedArray_1_tC8A32AF03BC5EA969AD4315DC5E67BBAF2765992 ___array;
	int32_t ___index;
};
struct LayoutNode_tADF081B0F16F76B66459DE38F3AD8EC098F22CBE 
{
	LayoutDataAccess_t99AA56349D0BC76F3742B927F4F0DDB21511FBBA ___m_Access;
	UnmanagedDataHandle_t5295F32E122AF2E09BF729381A22BD86B72C1DD1 ___m_Handle;
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
struct Enumerator_t8942583471D8F80E5517127E40570BC7B08A435E 
{
	List_1_t8A3C0729B4B61738DFBD32119977F2991FD56447* ____list;
	int32_t ____index;
	int32_t ____version;
	BindingRequest_tB47D4B85CD5F177F9D6891D44DE7958EE23A7172 ____current;
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
struct List_1_tDA1565FB54D9C701D544C5C5DEB5F20D33BC07BF_StaticFields
{
	NativeArray_1U5BU5D_t4BC8FD0BBBD51EF2E3A56BF78296DF37173EFDBD* ___s_emptyArray;
};
struct List_1_t7C8CC805CEADA09DFAC2AC1A5D731D5EE956F6DC_StaticFields
{
	RuleMatcherU5BU5D_t0135EA06151E72D04414F3EAF9420CB85EE2236C* ___s_emptyArray;
};
struct List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95_StaticFields
{
	VisualElementU5BU5D_tCAE8038767BF0FBEE26B3470C0FC4AE60E5229DF* ___s_emptyArray;
};
struct List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E_StaticFields
{
	__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ___s_emptyArray;
};
struct List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A_StaticFields
{
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___s_emptyArray;
};
struct LowLevelList_1_tFA10A51C575F28B96FF2E2E7FB3740E48546A1E0_StaticFields
{
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___s_emptyArray;
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct UQueryState_1_t62CA2841B4A1205A8554F7E9D2A3FBFB0F92B192_StaticFields
{
	ActionQueryMatcher_t0EF83980C6ABA9272F87A72C8B21FEF9403FA83E* ___s_Action;
	ListQueryMatcher_1_t80B2C90F8E32F964B971ECBBAA0E811171012425* ___s_List;
	ListQueryMatcher_1_t5888012D63B675F26A02C9BF208DC2C856BFBBC4* ___s_EnumerationList;
};
struct UnmanagedRefCountedList_1_t8B02F7EA0267E2F24A9C139864512B7B0BBE81BC_StaticFields
{
	MemoryLabel_t29CE7AB312D2ED888B444BBE5D452F7132EC9DB2 ___k_MemoryLabel;
	int32_t ___k_SizeOfData;
	int32_t ___k_SizeOfT;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
struct UIntPtr_t_StaticFields
{
	uintptr_t ___Zero;
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
struct SlotU5BU5D_t2B9148416634BCB8743E1F61254B305F1882FF1D  : public RuntimeArray
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


IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void* NativeArrayUnsafeUtility_GetUnsafeReadOnlyPtr_TisIl2CppFullySharedGenericStruct_m13C618FD69BBAB7D8C77632BF8A9116FCD17D234_fshared_inline (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 ___0_nativeArray, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mE21BD33899AFF1477136619A8C34639DCA029CC0_gshared (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_mAB3175D078843BDDC99FF3C3ED52257846066970_gshared (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* InputControlList_1_FromIndex_m442B00B502638B69F62E6669FB5DEDFA50ED17B6_gshared (uint64_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* Enumerator_get_Current_mB3247AE1E03448F01EBBF6941EC44FFCD31289FC_gshared (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mC4498C209E3B248A02CD1E4904D2E20274D81DB5_gshared (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mCD2379EFB931AEDA4F9C3D8290967A105E3878CB_gshared (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ListQueryMatcher_1_set_matches_m32D702F4DE02ABB90F1560B65718F096EDD98416_gshared_inline (ListQueryMatcher_1_t80B2C90F8E32F964B971ECBBAA0E811171012425* __this, List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ListQueryMatcher_1_Reset_mA87630F5478A77AC66CE91970A89BA506697A66D_gshared (ListQueryMatcher_1_t80B2C90F8E32F964B971ECBBAA0E811171012425* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_get_Item_m6E4BA37C1FB558E4A62AE4324212E45D09C5C937_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_index, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* Enumerator_get_Current_mD195DA373CA07B295F25635B42F3ED5E32221B5C_gshared (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m38DD50A147FD01D083464E220301DC3F99A84197_gshared (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared_inline (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mFFF471615D4AAD375CD440DAB84DE6979D99625F_gshared (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_mDD6869C382827C40E00CB14006C453CA19A2F6E6_gshared (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m92C8983EF703D0E6C2A069318E1F6728FB825575_gshared (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m97F29D6F80B478A97A6DA81F665E7DA693E17E64_fshared (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void UnsafeUtility_ReadArrayElement_TisIl2CppFullySharedGenericAny_m295186AA082411C57485F8BDB824E4D8AC1C6D93_fshared_inline (void* ___0_source, int32_t ___1_index, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mC9099E56E01AB33169AB4EE888BD9A2B752961DE_fshared_inline (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_mB15834348E10F7612876D2AE774493AE7AF604B2_fshared (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Enumerator_get_Current_mF9A198DACCE8ED0DC6FD097825D7A0F82D149DBA_fshared_inline (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m81767706F1E3B41EAB92F21B1669693FFE92ACCB_fshared_inline (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t NativeArray_1_get_Length_mBE5CC8B844994CFC4AB434235F915881575E63C8_fshared_inline (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_HasNext_m98065EB05573641926616E576CA797F054D69EF9_fshared (Enumerator_tE1470541970778078D070FF8C523A55C7301DA38* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void NativeArray_1_get_Item_mA8C8A69EB3A5D460C55DFCD27275CD5BA5E2B455_fshared_inline (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18* __this, int32_t ___0_index, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m0BBB67CF7366B5A989FBB12AA2293CD18853C98D_fshared (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t NativeSlice_1_get_Length_m0225CA0944599882AC9C2A06A99FDC685362AFBE_fshared_inline (NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m0DEF4AFE20672D3425DCFB0B4B7EBBAA7B69F595_fshared (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_m7DFE6628F82F51C868E30AF059855DEBF7ABE53A_fshared (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeSlice_1_get_Item_mB5E7BDE4EDF7E31480A9785BE103CC7E17E0E3A8_fshared (NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52* __this, int32_t ___0_index, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_mD5F9A8DA4A689D3BE78549843241C5C6D4936D84_fshared (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mEB8088957B367DEEE33935D4174B19579BF14B58_fshared (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m06F26A9D1616DCF8DE123B05DB3C01719237C3AF_fshared (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_mEC7989864CADAFF8FC0E8487223BF86743692433_fshared (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_m347C1E11612C5D8DBEFB997C4F82908223501003_fshared (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mA0F433CE2FCFD54A32DFD9BE11A2E16310CC4E07_fshared (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m62AB801313FAA9B181EBFE46DB8B2DB6D76B68B4_fshared (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mCB9B6D15D244D49204D2A903402C1D5011CC2830_fshared (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m9589C320A0B345A9190284C38FCB97F2015861DA_fshared_inline (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_m43C21832D8660DF1F98FA2733CE9D752ACB020D5_fshared (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Enumerator_get_Current_m44CEFC7935FF87861A589ACDB1024033E52A123E_fshared_inline (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m9E9FBC7AD882EDFB5FDCF6024ECA5DA4B2DD24DA_fshared (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m915D35B9BB2A77C5396F4FE1A1C53F1C93B8E337_fshared (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m1756F06B6BC460B8EEB4522B562E097F37D54C59_fshared (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Enumerator_get_Current_m51282E47C90E55BF2D96A0D329869ABC122A85A0_fshared_inline (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m7D04FEF0F3E60E6E691D979806634E034498134E_fshared (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_System_Collections_IEnumerator_Reset_m27D5470C040D50CC0200DDAB3C93DF3C4CA755B3_fshared (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mF3F5043F0C7EA67C19F3FA22DF2A5E66C4B82985_fshared (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_mC98285CB59B476CB33C08E3FDEC3AD026FD5D46B_fshared (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InlinedArray_1_get_Item_m559A0B1FCFEC59844BBCB769C3C743C5F46A2201_fshared (InlinedArray_1_tCA1A9B410EBCE10320B39011CD2E337C395D7CA3* __this, int32_t ___0_index, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_m29E9E90BB06DAEDA6F7D4721A6681EC28A0E54C6_fshared (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mC035FEFC4C4C462ECCBF7506FD23025A38501032_fshared (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m6CAB80EFB3B442E668D334714C1BA63697F23576_fshared (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Enumerator_get_Current_m5BA2E5A6E9016D327B7C3067D3D2B2016D37DFDA_fshared_inline (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m0F26F0C362DA633022A66F6C342673D39AD4622B_fshared (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m4FB57970CC4FBEE295E533DE12F0B21FF7E13AAA_fshared (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_System_Collections_IEnumerator_Reset_mEE0F6DC54C3238FF39CDFF5675B11CD97C544897_fshared (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m25395D42D3EE1E89A0B024E2FAF823E4A3FEF4D1_fshared (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mFE1EBE6F6425283FEAEAE7C79D02CDE4F9D367E8_fshared (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNextRare_mF1A6D7920FDC3C83A7DA1F838C2FC51D716637D9_fshared (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m8D8E5E878AF0A88A535AB1AB5BA4F23E151A678A_fshared (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Enumerator_get_Current_m8B42D4B2DE853B9D11B997120CD0228D4780E394_fshared_inline (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m3FCE22D15C040EE9F8614C711F786205BA6AAFB6_fshared (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_System_Collections_IEnumerator_Reset_mFA2701BBDA14ED6CC3113A32ED0E8B80DAD58A84_fshared (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mDE5B605B7794A192CC35F12013300AE26EB8FE5F_fshared (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNextRare_mA2A9C4EFAA8131F5D97D9B8626FDEDF343E93488_fshared (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m2F9CDD8406C6495182E05B55A00C3D0C9620CA67_fshared (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Enumerator_get_Current_m05879F7740A935DB74AE5672E2B537349369A3C1_fshared_inline (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mA8280DCF9D59E671AB204C71415939B6D7B4211E_fshared (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_System_Collections_IEnumerator_Reset_m15625F750730D127B128E2654A2B59982DEAB9C9_fshared (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_get_Current_mA7D722D310859E84FC0C9A2F8DBCAF8BA78F1D54_fshared_inline (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mBB836D4E68A5F554A9A7C377FFBD67DA4253EA7D_fshared (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Enumerator_set_Current_m1BB3A6D5580E7C6A864831EED00A4E1B8E81B1B5_fshared_inline (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool IndexedCollectionPropertyBagEnumerator_1_MoveNext_m6695B2065C6EB0E71B90C2EFF9429E4A76F4C0BA_fshared (IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* IndexedCollectionPropertyBagEnumerator_1_get_Current_m4E570DCDE463BEA50BA7B3DC03595ED13939EDBE_fshared (IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mB0957ADFA01BC52170FB018BD2AD59C2E163A22F_fshared (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void IndexedCollectionPropertyBagEnumerator_1_Reset_mEF20F04CD33523EE6B378C8F992E49A2214E1626_fshared (IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_mDD80A4C2F5EFC81C22B7F74CACFF9D0921BA0A3C_fshared (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void IndexedCollectionPropertyBagEnumerator_1_Dispose_mEBC1E3FF6876007C72F6F76ED237165B599B2505_fshared (IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m1938093C428E4EDE3B40A07184993FA35F74AB42_fshared (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mA25DFE205338DF12128791E990699B49B335B759_fshared (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mA6D24403558EA8BFEE5028D75AC049EC8677F637_fshared (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_ThrowEnumerationNotStartedOrEnded_mA8F87F65FFD80CCBB3A060C159463BB5B5603E93_fshared (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_m2DDD9E2B36A595D40708EBE84125C065CCF6F11D_fshared (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m01A32A0663137A48FD334AD3F7BF1A172FEBA204_fshared (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_System_Collections_IEnumerator_Reset_m5E20159EC017B0F62631E89E5784B2D532E2D314_fshared (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mBA9853A226A9CB16F8FB74D21519A91A38E063E0_fshared (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mA42BDA7B751B17DB4EC59F6995E3188780091966_fshared (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_m2845BC4731316BCC3E1AB5C3F5799107716B7810_fshared (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_mAEF87A53D24C4DB2F6069E75CF1A5D66D5F4D13F_fshared (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m6D0270EB2876CB03F839581CC803C1DAA52EF9D4_fshared (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m475AD9FE055280B2B1CF43E93F9A2340C3EAE75C_fshared (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m3347052D874561F1E1857019C4F418A27B125353_fshared (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_ThrowEnumerationNotStartedOrEnded_mDFA786E51878E61AE16B8C27D512FD3407BA6AFA_fshared (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_m748B0758792F434FAB29656C2F5CF6EEB4481D27_fshared (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mC2EE4A74701F665A795443A0814F1CF22BC12560_fshared (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_System_Collections_IEnumerator_Reset_m796D8D2C90C2436A06B08E30008143FCF74E8EC9_fshared (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void UnsafeUtilityInternal_ReadArrayElement_TisIl2CppFullySharedGenericAny_mC729F1975F0494EB9B61C929EAF0F798DCF68563_fshared_inline (void* ___0_source, int32_t ___1_index, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) ;

inline void* NativeArrayUnsafeUtility_GetUnsafeReadOnlyPtr_TisUInt64_t8F12534CC8FC4B5860F2A2CD1EE79D322E7A41AF_m44EAE568720002EAD1F4ECE1CBA62E756FB05AFA_inline (NativeArray_1_t07975297AD7F7512193094A7C0703BA872EF7A7B ___0_nativeArray, const RuntimeMethod* method)
{
	return ((  void* (*) (NativeArray_1_t07975297AD7F7512193094A7C0703BA872EF7A7B, const RuntimeMethod*))NativeArrayUnsafeUtility_GetUnsafeReadOnlyPtr_TisIl2CppFullySharedGenericStruct_m13C618FD69BBAB7D8C77632BF8A9116FCD17D234_fshared_inline)(___0_nativeArray, method);
}
inline bool Enumerator_MoveNext_mE21BD33899AFF1477136619A8C34639DCA029CC0 (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69*, const RuntimeMethod*))Enumerator_MoveNext_mE21BD33899AFF1477136619A8C34639DCA029CC0_gshared)(__this, method);
}
inline void Enumerator_Reset_mAB3175D078843BDDC99FF3C3ED52257846066970 (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69*, const RuntimeMethod*))Enumerator_Reset_mAB3175D078843BDDC99FF3C3ED52257846066970_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162 (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* __this, String_t* ___0_message, const RuntimeMethod* method) ;
inline Il2CppSharedGenericObject* InputControlList_1_FromIndex_m442B00B502638B69F62E6669FB5DEDFA50ED17B6 (uint64_t ___0_index, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (uint64_t, const RuntimeMethod*))InputControlList_1_FromIndex_m442B00B502638B69F62E6669FB5DEDFA50ED17B6_gshared)(___0_index, method);
}
inline Il2CppSharedGenericObject* Enumerator_get_Current_mB3247AE1E03448F01EBBF6941EC44FFCD31289FC (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* __this, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69*, const RuntimeMethod*))Enumerator_get_Current_mB3247AE1E03448F01EBBF6941EC44FFCD31289FC_gshared)(__this, method);
}
inline RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mC4498C209E3B248A02CD1E4904D2E20274D81DB5 (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_get_Current_mC4498C209E3B248A02CD1E4904D2E20274D81DB5_gshared)(__this, method);
}
inline void Enumerator_Dispose_mCD2379EFB931AEDA4F9C3D8290967A105E3878CB (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69*, const RuntimeMethod*))Enumerator_Dispose_mCD2379EFB931AEDA4F9C3D8290967A105E3878CB_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95* VisualElementListPool_Get_m99F3D55FC85A740A48A062146D40D59F50107CC2 (int32_t ___0_initialCapacity, const RuntimeMethod* method) ;
inline void ListQueryMatcher_1_set_matches_mA791583D020B06F7DB407BAA4AC0DC65BF61A2BC_inline (ListQueryMatcher_1_t5888012D63B675F26A02C9BF208DC2C856BFBBC4* __this, List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95* ___0_value, const RuntimeMethod* method)
{
	((  void (*) (ListQueryMatcher_1_t5888012D63B675F26A02C9BF208DC2C856BFBBC4*, List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95*, const RuntimeMethod*))ListQueryMatcher_1_set_matches_m32D702F4DE02ABB90F1560B65718F096EDD98416_gshared_inline)(__this, ___0_value, method);
}
inline void ListQueryMatcher_1_Reset_m1ED519C668E60E76E856B666DD5FC4B01121A181 (ListQueryMatcher_1_t5888012D63B675F26A02C9BF208DC2C856BFBBC4* __this, const RuntimeMethod* method)
{
	((  void (*) (ListQueryMatcher_1_t5888012D63B675F26A02C9BF208DC2C856BFBBC4*, const RuntimeMethod*))ListQueryMatcher_1_Reset_mA87630F5478A77AC66CE91970A89BA506697A66D_gshared)(__this, method);
}
inline VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* List_1_get_Item_mF58794633948FE8284FCDACC4456686548388092 (List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95* __this, int32_t ___0_index, const RuntimeMethod* method)
{
	VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* il2cppRetVal;
	((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, Il2CppFullySharedGenericAny*, const RuntimeMethod*))List_1_get_Item_m6E4BA37C1FB558E4A62AE4324212E45D09C5C937_fshared)((List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*)__this, ___0_index, (Il2CppFullySharedGenericAny*)&il2cppRetVal, method);
	return il2cppRetVal;
}
inline Il2CppSharedGenericObject* Enumerator_get_Current_mD195DA373CA07B295F25635B42F3ED5E32221B5C (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* __this, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96*, const RuntimeMethod*))Enumerator_get_Current_mD195DA373CA07B295F25635B42F3ED5E32221B5C_gshared)(__this, method);
}
inline RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m38DD50A147FD01D083464E220301DC3F99A84197 (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_get_Current_m38DD50A147FD01D083464E220301DC3F99A84197_gshared)(__this, method);
}
inline int32_t List_1_get_Count_m76A83B76330D385CC22ECE544729CDD0FCEAFECC_inline (List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95*, const RuntimeMethod*))List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared_inline)(__this, method);
}
inline bool Enumerator_MoveNext_mFFF471615D4AAD375CD440DAB84DE6979D99625F (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96*, const RuntimeMethod*))Enumerator_MoveNext_mFFF471615D4AAD375CD440DAB84DE6979D99625F_gshared)(__this, method);
}
inline void Enumerator_Reset_mDD6869C382827C40E00CB14006C453CA19A2F6E6 (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96*, const RuntimeMethod*))Enumerator_Reset_mDD6869C382827C40E00CB14006C453CA19A2F6E6_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void VisualElementListPool_Release_mAF4ED38CC4F9A6E58B06F4B0BD13611F7DECBCFA (List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95* ___0_elements, const RuntimeMethod* method) ;
inline void Enumerator_Dispose_m92C8983EF703D0E6C2A069318E1F6728FB825575 (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96*, const RuntimeMethod*))Enumerator_Dispose_m92C8983EF703D0E6C2A069318E1F6728FB825575_gshared)(__this, method);
}
inline void Enumerator_Dispose_m97F29D6F80B478A97A6DA81F665E7DA693E17E64 (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405*, const RuntimeMethod*))Enumerator_Dispose_m97F29D6F80B478A97A6DA81F665E7DA693E17E64_fshared)(__this, method);
}
inline void UnsafeUtility_ReadArrayElement_TisIl2CppFullySharedGenericStruct_m45AD40013DEE9AEF8031E0DA6F4D23BFD5AA8F42_inline (void* ___0_source, int32_t ___1_index, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (void*, int32_t, Il2CppFullySharedGenericAny*, const RuntimeMethod*))UnsafeUtility_ReadArrayElement_TisIl2CppFullySharedGenericAny_m295186AA082411C57485F8BDB824E4D8AC1C6D93_fshared_inline)(___0_source, ___1_index, il2cppRetVal, method);
}
inline bool Enumerator_MoveNext_mC9099E56E01AB33169AB4EE888BD9A2B752961DE_inline (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405*, const RuntimeMethod*))Enumerator_MoveNext_mC9099E56E01AB33169AB4EE888BD9A2B752961DE_fshared_inline)(__this, method);
}
inline void Enumerator_Reset_mB15834348E10F7612876D2AE774493AE7AF604B2 (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405*, const RuntimeMethod*))Enumerator_Reset_mB15834348E10F7612876D2AE774493AE7AF604B2_fshared)(__this, method);
}
inline void Enumerator_get_Current_mF9A198DACCE8ED0DC6FD097825D7A0F82D149DBA_inline (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405*, Il2CppFullySharedGenericStruct*, const RuntimeMethod*))Enumerator_get_Current_mF9A198DACCE8ED0DC6FD097825D7A0F82D149DBA_fshared_inline)((Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405*)__this, il2cppRetVal, method);
}
inline RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m81767706F1E3B41EAB92F21B1669693FFE92ACCB_inline (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_get_Current_m81767706F1E3B41EAB92F21B1669693FFE92ACCB_fshared_inline)(__this, method);
}
inline NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 List_1_get_Item_m944337BED8AAD9BC3015016C70A16A871E20901E (List_1_tDA1565FB54D9C701D544C5C5DEB5F20D33BC07BF* __this, int32_t ___0_index, const RuntimeMethod* method)
{
	NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 il2cppRetVal;
	((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, Il2CppFullySharedGenericAny*, const RuntimeMethod*))List_1_get_Item_m6E4BA37C1FB558E4A62AE4324212E45D09C5C937_fshared)((List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*)__this, ___0_index, (Il2CppFullySharedGenericAny*)&il2cppRetVal, method);
	return il2cppRetVal;
}
inline int32_t NativeArray_1_get_Length_mBE5CC8B844994CFC4AB434235F915881575E63C8_inline (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18*, const RuntimeMethod*))NativeArray_1_get_Length_mBE5CC8B844994CFC4AB434235F915881575E63C8_fshared_inline)(__this, method);
}
inline int32_t List_1_get_Count_m0F04F8BA763691AD4F57971309F219BA108367B3_inline (List_1_tDA1565FB54D9C701D544C5C5DEB5F20D33BC07BF* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_tDA1565FB54D9C701D544C5C5DEB5F20D33BC07BF*, const RuntimeMethod*))List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared_inline)(__this, method);
}
inline bool Enumerator_HasNext_m98065EB05573641926616E576CA797F054D69EF9 (Enumerator_tE1470541970778078D070FF8C523A55C7301DA38* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_tE1470541970778078D070FF8C523A55C7301DA38*, const RuntimeMethod*))Enumerator_HasNext_m98065EB05573641926616E576CA797F054D69EF9_fshared)(__this, method);
}
inline void NativeArray_1_get_Item_mA8C8A69EB3A5D460C55DFCD27275CD5BA5E2B455_inline (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18* __this, int32_t ___0_index, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18*, int32_t, Il2CppFullySharedGenericStruct*, const RuntimeMethod*))NativeArray_1_get_Item_mA8C8A69EB3A5D460C55DFCD27275CD5BA5E2B455_fshared_inline)((NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18*)__this, ___0_index, il2cppRetVal, method);
}
inline void Enumerator_Dispose_m0BBB67CF7366B5A989FBB12AA2293CD18853C98D (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817*, const RuntimeMethod*))Enumerator_Dispose_m0BBB67CF7366B5A989FBB12AA2293CD18853C98D_fshared)(__this, method);
}
inline int32_t NativeSlice_1_get_Length_m0225CA0944599882AC9C2A06A99FDC685362AFBE_inline (NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52*, const RuntimeMethod*))NativeSlice_1_get_Length_m0225CA0944599882AC9C2A06A99FDC685362AFBE_fshared_inline)(__this, method);
}
inline bool Enumerator_MoveNext_m0DEF4AFE20672D3425DCFB0B4B7EBBAA7B69F595 (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817*, const RuntimeMethod*))Enumerator_MoveNext_m0DEF4AFE20672D3425DCFB0B4B7EBBAA7B69F595_fshared)(__this, method);
}
inline void Enumerator_Reset_m7DFE6628F82F51C868E30AF059855DEBF7ABE53A (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817*, const RuntimeMethod*))Enumerator_Reset_m7DFE6628F82F51C868E30AF059855DEBF7ABE53A_fshared)(__this, method);
}
inline void NativeSlice_1_get_Item_mB5E7BDE4EDF7E31480A9785BE103CC7E17E0E3A8 (NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52* __this, int32_t ___0_index, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52*, int32_t, Il2CppFullySharedGenericStruct*, const RuntimeMethod*))NativeSlice_1_get_Item_mB5E7BDE4EDF7E31480A9785BE103CC7E17E0E3A8_fshared)((NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52*)__this, ___0_index, il2cppRetVal, method);
}
inline void Enumerator_get_Current_mD5F9A8DA4A689D3BE78549843241C5C6D4936D84 (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817*, Il2CppFullySharedGenericStruct*, const RuntimeMethod*))Enumerator_get_Current_mD5F9A8DA4A689D3BE78549843241C5C6D4936D84_fshared)((Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817*)__this, il2cppRetVal, method);
}
inline RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mEB8088957B367DEEE33935D4174B19579BF14B58 (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_get_Current_mEB8088957B367DEEE33935D4174B19579BF14B58_fshared)(__this, method);
}
inline bool Enumerator_MoveNext_m06F26A9D1616DCF8DE123B05DB3C01719237C3AF (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72*, const RuntimeMethod*))Enumerator_MoveNext_m06F26A9D1616DCF8DE123B05DB3C01719237C3AF_fshared)(__this, method);
}
inline void Enumerator_Reset_mEC7989864CADAFF8FC0E8487223BF86743692433 (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72*, const RuntimeMethod*))Enumerator_Reset_mEC7989864CADAFF8FC0E8487223BF86743692433_fshared)(__this, method);
}
inline void Enumerator_get_Current_m347C1E11612C5D8DBEFB997C4F82908223501003 (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72*, Il2CppFullySharedGenericStruct*, const RuntimeMethod*))Enumerator_get_Current_m347C1E11612C5D8DBEFB997C4F82908223501003_fshared)((Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72*)__this, il2cppRetVal, method);
}
inline RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mA0F433CE2FCFD54A32DFD9BE11A2E16310CC4E07 (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_get_Current_mA0F433CE2FCFD54A32DFD9BE11A2E16310CC4E07_fshared)(__this, method);
}
inline void Enumerator_Dispose_m62AB801313FAA9B181EBFE46DB8B2DB6D76B68B4 (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72*, const RuntimeMethod*))Enumerator_Dispose_m62AB801313FAA9B181EBFE46DB8B2DB6D76B68B4_fshared)(__this, method);
}
inline void Enumerator_Dispose_mCB9B6D15D244D49204D2A903402C1D5011CC2830 (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C*, const RuntimeMethod*))Enumerator_Dispose_mCB9B6D15D244D49204D2A903402C1D5011CC2830_fshared)(__this, method);
}
inline bool Enumerator_MoveNext_m9589C320A0B345A9190284C38FCB97F2015861DA_inline (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C*, const RuntimeMethod*))Enumerator_MoveNext_m9589C320A0B345A9190284C38FCB97F2015861DA_fshared_inline)(__this, method);
}
inline void Enumerator_Reset_m43C21832D8660DF1F98FA2733CE9D752ACB020D5 (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C*, const RuntimeMethod*))Enumerator_Reset_m43C21832D8660DF1F98FA2733CE9D752ACB020D5_fshared)(__this, method);
}
inline void Enumerator_get_Current_m44CEFC7935FF87861A589ACDB1024033E52A123E_inline (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C*, Il2CppFullySharedGenericStruct*, const RuntimeMethod*))Enumerator_get_Current_m44CEFC7935FF87861A589ACDB1024033E52A123E_fshared_inline)((Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C*)__this, il2cppRetVal, method);
}
inline RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m9E9FBC7AD882EDFB5FDCF6024ECA5DA4B2DD24DA (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_get_Current_m9E9FBC7AD882EDFB5FDCF6024ECA5DA4B2DD24DA_fshared)(__this, method);
}
inline void Enumerator_Dispose_m915D35B9BB2A77C5396F4FE1A1C53F1C93B8E337 (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB*, const RuntimeMethod*))Enumerator_Dispose_m915D35B9BB2A77C5396F4FE1A1C53F1C93B8E337_fshared)(__this, method);
}
inline bool Enumerator_MoveNext_m1756F06B6BC460B8EEB4522B562E097F37D54C59 (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB*, const RuntimeMethod*))Enumerator_MoveNext_m1756F06B6BC460B8EEB4522B562E097F37D54C59_fshared)(__this, method);
}
inline void Enumerator_get_Current_m51282E47C90E55BF2D96A0D329869ABC122A85A0_inline (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB*, Il2CppFullySharedGenericAny*, const RuntimeMethod*))Enumerator_get_Current_m51282E47C90E55BF2D96A0D329869ABC122A85A0_fshared_inline)((Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB*)__this, il2cppRetVal, method);
}
inline RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m7D04FEF0F3E60E6E691D979806634E034498134E (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_get_Current_m7D04FEF0F3E60E6E691D979806634E034498134E_fshared)(__this, method);
}
inline void Enumerator_System_Collections_IEnumerator_Reset_m27D5470C040D50CC0200DDAB3C93DF3C4CA755B3 (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_Reset_m27D5470C040D50CC0200DDAB3C93DF3C4CA755B3_fshared)(__this, method);
}
inline bool Enumerator_MoveNext_mF3F5043F0C7EA67C19F3FA22DF2A5E66C4B82985 (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092*, const RuntimeMethod*))Enumerator_MoveNext_mF3F5043F0C7EA67C19F3FA22DF2A5E66C4B82985_fshared)(__this, method);
}
inline void Enumerator_Reset_mC98285CB59B476CB33C08E3FDEC3AD026FD5D46B (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092*, const RuntimeMethod*))Enumerator_Reset_mC98285CB59B476CB33C08E3FDEC3AD026FD5D46B_fshared)(__this, method);
}
inline void InlinedArray_1_get_Item_m559A0B1FCFEC59844BBCB769C3C743C5F46A2201 (InlinedArray_1_tCA1A9B410EBCE10320B39011CD2E337C395D7CA3* __this, int32_t ___0_index, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (InlinedArray_1_tCA1A9B410EBCE10320B39011CD2E337C395D7CA3*, int32_t, Il2CppFullySharedGenericAny*, const RuntimeMethod*))InlinedArray_1_get_Item_m559A0B1FCFEC59844BBCB769C3C743C5F46A2201_fshared)((InlinedArray_1_tCA1A9B410EBCE10320B39011CD2E337C395D7CA3*)__this, ___0_index, il2cppRetVal, method);
}
inline void Enumerator_get_Current_m29E9E90BB06DAEDA6F7D4721A6681EC28A0E54C6 (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092*, Il2CppFullySharedGenericAny*, const RuntimeMethod*))Enumerator_get_Current_m29E9E90BB06DAEDA6F7D4721A6681EC28A0E54C6_fshared)((Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092*)__this, il2cppRetVal, method);
}
inline RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mC035FEFC4C4C462ECCBF7506FD23025A38501032 (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_get_Current_mC035FEFC4C4C462ECCBF7506FD23025A38501032_fshared)(__this, method);
}
inline void Enumerator_Dispose_m6CAB80EFB3B442E668D334714C1BA63697F23576 (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092*, const RuntimeMethod*))Enumerator_Dispose_m6CAB80EFB3B442E668D334714C1BA63697F23576_fshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlatformNotSupportedException__ctor_mD5DBE8E9A6FF4B75EF02671029C6D67A51EAFBD1 (PlatformNotSupportedException_tD2BD7EB9278518AA5FE8AE75AD5D0D4298A4631A* __this, const RuntimeMethod* method) ;
inline void Enumerator_get_Current_m5BA2E5A6E9016D327B7C3067D3D2B2016D37DFDA_inline (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457*, Il2CppFullySharedGenericAny*, const RuntimeMethod*))Enumerator_get_Current_m5BA2E5A6E9016D327B7C3067D3D2B2016D37DFDA_fshared_inline)((Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457*)__this, il2cppRetVal, method);
}
inline RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m0F26F0C362DA633022A66F6C342673D39AD4622B (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_get_Current_m0F26F0C362DA633022A66F6C342673D39AD4622B_fshared)(__this, method);
}
inline bool Enumerator_MoveNext_m4FB57970CC4FBEE295E533DE12F0B21FF7E13AAA (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457*, const RuntimeMethod*))Enumerator_MoveNext_m4FB57970CC4FBEE295E533DE12F0B21FF7E13AAA_fshared)(__this, method);
}
inline void Enumerator_System_Collections_IEnumerator_Reset_mEE0F6DC54C3238FF39CDFF5675B11CD97C544897 (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_Reset_mEE0F6DC54C3238FF39CDFF5675B11CD97C544897_fshared)(__this, method);
}
inline void Enumerator_Dispose_m25395D42D3EE1E89A0B024E2FAF823E4A3FEF4D1 (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457*, const RuntimeMethod*))Enumerator_Dispose_m25395D42D3EE1E89A0B024E2FAF823E4A3FEF4D1_fshared)(__this, method);
}
inline void Enumerator_Dispose_mFE1EBE6F6425283FEAEAE7C79D02CDE4F9D367E8 (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*, const RuntimeMethod*))Enumerator_Dispose_mFE1EBE6F6425283FEAEAE7C79D02CDE4F9D367E8_fshared)(__this, method);
}
inline bool Enumerator_MoveNextRare_mF1A6D7920FDC3C83A7DA1F838C2FC51D716637D9 (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*, const RuntimeMethod*))Enumerator_MoveNextRare_mF1A6D7920FDC3C83A7DA1F838C2FC51D716637D9_fshared)(__this, method);
}
inline bool Enumerator_MoveNext_m8D8E5E878AF0A88A535AB1AB5BA4F23E151A678A (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*, const RuntimeMethod*))Enumerator_MoveNext_m8D8E5E878AF0A88A535AB1AB5BA4F23E151A678A_fshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion_m5331E2E0EC0E36843D53F439C2529530595ACE9F (const RuntimeMethod* method) ;
inline void Enumerator_get_Current_m8B42D4B2DE853B9D11B997120CD0228D4780E394_inline (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*, Il2CppFullySharedGenericAny*, const RuntimeMethod*))Enumerator_get_Current_m8B42D4B2DE853B9D11B997120CD0228D4780E394_fshared_inline)((Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*)__this, il2cppRetVal, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen_m61D5A6F694C15854BCF089BFD945BD6DBDB47D4E (const RuntimeMethod* method) ;
inline RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m3FCE22D15C040EE9F8614C711F786205BA6AAFB6 (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_get_Current_m3FCE22D15C040EE9F8614C711F786205BA6AAFB6_fshared)(__this, method);
}
inline void Enumerator_System_Collections_IEnumerator_Reset_mFA2701BBDA14ED6CC3113A32ED0E8B80DAD58A84 (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_Reset_mFA2701BBDA14ED6CC3113A32ED0E8B80DAD58A84_fshared)(__this, method);
}
inline void Enumerator_Dispose_mDE5B605B7794A192CC35F12013300AE26EB8FE5F (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0*, const RuntimeMethod*))Enumerator_Dispose_mDE5B605B7794A192CC35F12013300AE26EB8FE5F_fshared)(__this, method);
}
inline bool Enumerator_MoveNextRare_mA2A9C4EFAA8131F5D97D9B8626FDEDF343E93488 (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0*, const RuntimeMethod*))Enumerator_MoveNextRare_mA2A9C4EFAA8131F5D97D9B8626FDEDF343E93488_fshared)(__this, method);
}
inline bool Enumerator_MoveNext_m2F9CDD8406C6495182E05B55A00C3D0C9620CA67 (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0*, const RuntimeMethod*))Enumerator_MoveNext_m2F9CDD8406C6495182E05B55A00C3D0C9620CA67_fshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InvalidOperationException__ctor_m1BE9BD198B904AA1D94F4B10DA88077DFD44B7A5 (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* __this, const RuntimeMethod* method) ;
inline void Enumerator_get_Current_m05879F7740A935DB74AE5672E2B537349369A3C1_inline (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0*, Il2CppFullySharedGenericAny*, const RuntimeMethod*))Enumerator_get_Current_m05879F7740A935DB74AE5672E2B537349369A3C1_fshared_inline)((Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0*)__this, il2cppRetVal, method);
}
inline RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mA8280DCF9D59E671AB204C71415939B6D7B4211E (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_get_Current_mA8280DCF9D59E671AB204C71415939B6D7B4211E_fshared)(__this, method);
}
inline void Enumerator_System_Collections_IEnumerator_Reset_m15625F750730D127B128E2654A2B59982DEAB9C9 (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_Reset_m15625F750730D127B128E2654A2B59982DEAB9C9_fshared)(__this, method);
}
inline RuntimeObject* Enumerator_get_Current_mA7D722D310859E84FC0C9A2F8DBCAF8BA78F1D54_inline (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE*, const RuntimeMethod*))Enumerator_get_Current_mA7D722D310859E84FC0C9A2F8DBCAF8BA78F1D54_fshared_inline)(__this, method);
}
inline RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mBB836D4E68A5F554A9A7C377FFBD67DA4253EA7D (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_get_Current_mBB836D4E68A5F554A9A7C377FFBD67DA4253EA7D_fshared)(__this, method);
}
inline void Enumerator_set_Current_m1BB3A6D5580E7C6A864831EED00A4E1B8E81B1B5_inline (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, RuntimeObject* ___0_value, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE*, RuntimeObject*, const RuntimeMethod*))Enumerator_set_Current_m1BB3A6D5580E7C6A864831EED00A4E1B8E81B1B5_fshared_inline)(__this, ___0_value, method);
}
inline bool Enumerator_MoveNext_m4E164BF10DEA42BD1FB9648DD5795D120F5B99F2 (Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63*, const RuntimeMethod*))Enumerator_MoveNext_m8D8E5E878AF0A88A535AB1AB5BA4F23E151A678A_fshared)(__this, method);
}
inline RuntimeObject* Enumerator_get_Current_m1141D239836770EF8C75957763553C76773A96A4_inline (Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63* __this, const RuntimeMethod* method)
{
	RuntimeObject* il2cppRetVal;
	((  void (*) (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*, Il2CppFullySharedGenericAny*, const RuntimeMethod*))Enumerator_get_Current_m8B42D4B2DE853B9D11B997120CD0228D4780E394_fshared_inline)((Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*)__this, (Il2CppFullySharedGenericAny*)&il2cppRetVal, method);
	return il2cppRetVal;
}
inline bool IndexedCollectionPropertyBagEnumerator_1_MoveNext_m6695B2065C6EB0E71B90C2EFF9429E4A76F4C0BA (IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479*, const RuntimeMethod*))IndexedCollectionPropertyBagEnumerator_1_MoveNext_m6695B2065C6EB0E71B90C2EFF9429E4A76F4C0BA_fshared)(__this, method);
}
inline RuntimeObject* IndexedCollectionPropertyBagEnumerator_1_get_Current_m4E570DCDE463BEA50BA7B3DC03595ED13939EDBE (IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479*, const RuntimeMethod*))IndexedCollectionPropertyBagEnumerator_1_get_Current_m4E570DCDE463BEA50BA7B3DC03595ED13939EDBE_fshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentOutOfRangeException__ctor_mB596C51BFA864B65C2CED275458FAE90F7CD29C9 (ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F* __this, const RuntimeMethod* method) ;
inline bool Enumerator_MoveNext_mB0957ADFA01BC52170FB018BD2AD59C2E163A22F (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE*, const RuntimeMethod*))Enumerator_MoveNext_mB0957ADFA01BC52170FB018BD2AD59C2E163A22F_fshared)(__this, method);
}
inline void Enumerator_System_Collections_IEnumerator_Reset_mE24EC919F3F9A0356303B687F207D479BB66B3AB (Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_Reset_mFA2701BBDA14ED6CC3113A32ED0E8B80DAD58A84_fshared)(__this, method);
}
inline void IndexedCollectionPropertyBagEnumerator_1_Reset_mEF20F04CD33523EE6B378C8F992E49A2214E1626 (IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479* __this, const RuntimeMethod* method)
{
	((  void (*) (IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479*, const RuntimeMethod*))IndexedCollectionPropertyBagEnumerator_1_Reset_mEF20F04CD33523EE6B378C8F992E49A2214E1626_fshared)(__this, method);
}
inline void Enumerator_Reset_mDD80A4C2F5EFC81C22B7F74CACFF9D0921BA0A3C (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE*, const RuntimeMethod*))Enumerator_Reset_mDD80A4C2F5EFC81C22B7F74CACFF9D0921BA0A3C_fshared)(__this, method);
}
inline void IndexedCollectionPropertyBagEnumerator_1_Dispose_mEBC1E3FF6876007C72F6F76ED237165B599B2505 (IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479* __this, const RuntimeMethod* method)
{
	((  void (*) (IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479*, const RuntimeMethod*))IndexedCollectionPropertyBagEnumerator_1_Dispose_mEBC1E3FF6876007C72F6F76ED237165B599B2505_fshared)(__this, method);
}
inline void Enumerator_Dispose_m1938093C428E4EDE3B40A07184993FA35F74AB42 (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE*, const RuntimeMethod*))Enumerator_Dispose_m1938093C428E4EDE3B40A07184993FA35F74AB42_fshared)(__this, method);
}
inline void Enumerator_Dispose_mA25DFE205338DF12128791E990699B49B335B759 (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C*, const RuntimeMethod*))Enumerator_Dispose_mA25DFE205338DF12128791E990699B49B335B759_fshared)(__this, method);
}
inline bool Enumerator_MoveNext_mA6D24403558EA8BFEE5028D75AC049EC8677F637 (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C*, const RuntimeMethod*))Enumerator_MoveNext_mA6D24403558EA8BFEE5028D75AC049EC8677F637_fshared)(__this, method);
}
inline void Enumerator_ThrowEnumerationNotStartedOrEnded_mA8F87F65FFD80CCBB3A060C159463BB5B5603E93 (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C*, const RuntimeMethod*))Enumerator_ThrowEnumerationNotStartedOrEnded_mA8F87F65FFD80CCBB3A060C159463BB5B5603E93_fshared)(__this, method);
}
inline void Enumerator_get_Current_m2DDD9E2B36A595D40708EBE84125C065CCF6F11D (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C*, Il2CppFullySharedGenericAny*, const RuntimeMethod*))Enumerator_get_Current_m2DDD9E2B36A595D40708EBE84125C065CCF6F11D_fshared)((Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C*)__this, il2cppRetVal, method);
}
inline RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m01A32A0663137A48FD334AD3F7BF1A172FEBA204 (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_get_Current_m01A32A0663137A48FD334AD3F7BF1A172FEBA204_fshared)(__this, method);
}
inline void Enumerator_System_Collections_IEnumerator_Reset_m5E20159EC017B0F62631E89E5784B2D532E2D314 (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_Reset_m5E20159EC017B0F62631E89E5784B2D532E2D314_fshared)(__this, method);
}
inline void Enumerator_Dispose_mBA9853A226A9CB16F8FB74D21519A91A38E063E0 (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923*, const RuntimeMethod*))Enumerator_Dispose_mBA9853A226A9CB16F8FB74D21519A91A38E063E0_fshared)(__this, method);
}
inline bool Enumerator_MoveNext_mA42BDA7B751B17DB4EC59F6995E3188780091966 (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923*, const RuntimeMethod*))Enumerator_MoveNext_mA42BDA7B751B17DB4EC59F6995E3188780091966_fshared)(__this, method);
}
inline void Enumerator_Reset_m2845BC4731316BCC3E1AB5C3F5799107716B7810 (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923*, const RuntimeMethod*))Enumerator_Reset_m2845BC4731316BCC3E1AB5C3F5799107716B7810_fshared)(__this, method);
}
inline void Enumerator_get_Current_mAEF87A53D24C4DB2F6069E75CF1A5D66D5F4D13F (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923*, Il2CppFullySharedGenericAny*, const RuntimeMethod*))Enumerator_get_Current_mAEF87A53D24C4DB2F6069E75CF1A5D66D5F4D13F_fshared)((Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923*)__this, il2cppRetVal, method);
}
inline RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m6D0270EB2876CB03F839581CC803C1DAA52EF9D4 (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_get_Current_m6D0270EB2876CB03F839581CC803C1DAA52EF9D4_fshared)(__this, method);
}
inline void Enumerator_Dispose_m475AD9FE055280B2B1CF43E93F9A2340C3EAE75C (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41*, const RuntimeMethod*))Enumerator_Dispose_m475AD9FE055280B2B1CF43E93F9A2340C3EAE75C_fshared)(__this, method);
}
inline bool Enumerator_MoveNext_m3347052D874561F1E1857019C4F418A27B125353 (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41*, const RuntimeMethod*))Enumerator_MoveNext_m3347052D874561F1E1857019C4F418A27B125353_fshared)(__this, method);
}
inline void Enumerator_ThrowEnumerationNotStartedOrEnded_mDFA786E51878E61AE16B8C27D512FD3407BA6AFA (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41*, const RuntimeMethod*))Enumerator_ThrowEnumerationNotStartedOrEnded_mDFA786E51878E61AE16B8C27D512FD3407BA6AFA_fshared)(__this, method);
}
inline void Enumerator_get_Current_m748B0758792F434FAB29656C2F5CF6EEB4481D27 (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41*, Il2CppFullySharedGenericAny*, const RuntimeMethod*))Enumerator_get_Current_m748B0758792F434FAB29656C2F5CF6EEB4481D27_fshared)((Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41*)__this, il2cppRetVal, method);
}
inline RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mC2EE4A74701F665A795443A0814F1CF22BC12560 (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_get_Current_mC2EE4A74701F665A795443A0814F1CF22BC12560_fshared)(__this, method);
}
inline void Enumerator_System_Collections_IEnumerator_Reset_m796D8D2C90C2436A06B08E30008143FCF74E8EC9 (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41*, const RuntimeMethod*))Enumerator_System_Collections_IEnumerator_Reset_m796D8D2C90C2436A06B08E30008143FCF74E8EC9_fshared)(__this, method);
}
inline void UnsafeUtilityInternal_ReadArrayElement_TisIl2CppFullySharedGenericAny_mC729F1975F0494EB9B61C929EAF0F798DCF68563_inline (void* ___0_source, int32_t ___1_index, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	((  void (*) (void*, int32_t, Il2CppFullySharedGenericAny*, const RuntimeMethod*))UnsafeUtilityInternal_ReadArrayElement_TisIl2CppFullySharedGenericAny_mC729F1975F0494EB9B61C929EAF0F798DCF68563_fshared_inline)(___0_source, ___1_index, il2cppRetVal, method);
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
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_mD8A207CB41B6E1C5B7AA4A220CB03058AF6CC858_gshared (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* __this, InputControlList_1_t84F2DA44C9BD66500A2844A168208BD391B51BF4 ___0_list, const RuntimeMethod* method) 
{
	Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* G_B2_0 = NULL;
	Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* G_B1_0 = NULL;
	uintptr_t G_B3_0;
	memset((&G_B3_0), 0, sizeof(G_B3_0));
	Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* G_B3_1 = NULL;
	{
		InputControlList_1_t84F2DA44C9BD66500A2844A168208BD391B51BF4 L_0 = ___0_list;
		int32_t L_1 = L_0.___m_Count;
		__this->___m_Count = L_1;
		__this->___m_Current = (-1);
		int32_t L_2 = __this->___m_Count;
		if ((((int32_t)L_2) > ((int32_t)0)))
		{
			G_B2_0 = __this;
			goto IL_0021;
		}
		G_B1_0 = __this;
	}
	{
		uintptr_t L_3 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(0,NULL));
		G_B3_0 = L_3;
		G_B3_1 = G_B1_0;
		goto IL_002c;
	}

IL_0021:
	{
		InputControlList_1_t84F2DA44C9BD66500A2844A168208BD391B51BF4 L_4 = ___0_list;
		NativeArray_1_t07975297AD7F7512193094A7C0703BA872EF7A7B L_5 = L_4.___m_Indices;
		void* L_6;
		L_6 = NativeArrayUnsafeUtility_GetUnsafeReadOnlyPtr_TisUInt64_t8F12534CC8FC4B5860F2A2CD1EE79D322E7A41AF_m44EAE568720002EAD1F4ECE1CBA62E756FB05AFA_inline(L_5, NULL);
		G_B3_0 = ((uintptr_t)(intptr_t)(L_6));
		G_B3_1 = G_B2_0;
	}

IL_002c:
	{
		G_B3_1->___m_Indices = (uint64_t*)G_B3_0;
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mE21BD33899AFF1477136619A8C34639DCA029CC0_gshared (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___m_Current;
		int32_t L_1 = __this->___m_Count;
		if ((((int32_t)L_0) < ((int32_t)L_1)))
		{
			goto IL_0010;
		}
	}
	{
		return (bool)0;
	}

IL_0010:
	{
		int32_t L_2 = __this->___m_Current;
		__this->___m_Current = ((int32_t)il2cpp_codegen_add(L_2, 1));
		int32_t L_3 = __this->___m_Current;
		int32_t L_4 = __this->___m_Count;
		return (bool)((((int32_t)((((int32_t)L_3) == ((int32_t)L_4))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}
}
IL2CPP_EXTERN_C  bool Enumerator_MoveNext_mE21BD33899AFF1477136619A8C34639DCA029CC0_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69>(__this);
	bool _returnValue;
	_returnValue = Enumerator_MoveNext_mE21BD33899AFF1477136619A8C34639DCA029CC0(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_mAB3175D078843BDDC99FF3C3ED52257846066970_gshared (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* __this, const RuntimeMethod* method) 
{
	{
		__this->___m_Current = (-1);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Reset_mAB3175D078843BDDC99FF3C3ED52257846066970_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69>(__this);
	Enumerator_Reset_mAB3175D078843BDDC99FF3C3ED52257846066970(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* Enumerator_get_Current_mB3247AE1E03448F01EBBF6941EC44FFCD31289FC_gshared (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	{
		uint64_t* L_0 = __this->___m_Indices;
		uintptr_t L_1 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(0,NULL));
		if ((!(((uintptr_t)L_0) == ((uintptr_t)L_1))))
		{
			goto IL_0015;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_2 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_2, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral670CBAB5C8A30A4C8DC68337B78F13CC30BC704D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_2, method);
	}

IL_0015:
	{
		uint64_t* L_3 = __this->___m_Indices;
		int32_t L_4 = __this->___m_Current;
		intptr_t L_5 = (il2cpp_codegen_conv<intptr_t,int32_t,int32_t,false,false>(L_4,NULL));
		int64_t L_6 = *(((int64_t*)((uint64_t*)il2cpp_codegen_add((intptr_t)L_3, ((intptr_t)il2cpp_codegen_multiply(L_5, 8))))));
		Il2CppSharedGenericObject* L_7;
		L_7 = InputControlList_1_FromIndex_m442B00B502638B69F62E6669FB5DEDFA50ED17B6(L_6, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0));
		return L_7;
	}
}
IL2CPP_EXTERN_C  Il2CppSharedGenericObject* Enumerator_get_Current_mB3247AE1E03448F01EBBF6941EC44FFCD31289FC_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69>(__this);
	Il2CppSharedGenericObject* _returnValue;
	_returnValue = Enumerator_get_Current_mB3247AE1E03448F01EBBF6941EC44FFCD31289FC(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mC4498C209E3B248A02CD1E4904D2E20274D81DB5_gshared (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	{
		Il2CppSharedGenericObject* L_0;
		L_0 = Enumerator_get_Current_mB3247AE1E03448F01EBBF6941EC44FFCD31289FC(__this, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		return (RuntimeObject*)L_0;
	}
}
IL2CPP_EXTERN_C  RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mC4498C209E3B248A02CD1E4904D2E20274D81DB5_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69>(__this);
	RuntimeObject* _returnValue;
	_returnValue = Enumerator_System_Collections_IEnumerator_get_Current_mC4498C209E3B248A02CD1E4904D2E20274D81DB5(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mCD2379EFB931AEDA4F9C3D8290967A105E3878CB_gshared (Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Dispose_mCD2379EFB931AEDA4F9C3D8290967A105E3878CB_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tBDA9B6FC1B6B46585AC3BB187EB3B6F058D22D69>(__this);
	Enumerator_Dispose_mCD2379EFB931AEDA4F9C3D8290967A105E3878CB(_thisAdjusted, method);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m9F29B7771DB9254FA527BBD2018BD4F2A8B941B0_gshared (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* __this, UQueryState_1_t62CA2841B4A1205A8554F7E9D2A3FBFB0F92B192 ___0_queryState, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&VisualElementListPool_tB82E686FE29EA30D8CEDF6FF352FD1B7E7A30095_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(VisualElementListPool_tB82E686FE29EA30D8CEDF6FF352FD1B7E7A30095_StaticInit);
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(UQueryState_1_t469E70AF7F7E359CC21E9875D08B1D48632DE915_StaticInit);
	{
		CHECKED_LOCAL_INIT(VisualElementListPool_tB82E686FE29EA30D8CEDF6FF352FD1B7E7A30095_StaticInit,(VisualElementListPool_tB82E686FE29EA30D8CEDF6FF352FD1B7E7A30095_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95* L_0;
		L_0 = VisualElementListPool_Get_m99F3D55FC85A740A48A062146D40D59F50107CC2(0, NULL);
		__this->___iterationList = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___iterationList), (void*)L_0);
		CHECKED_LOCAL_INIT(UQueryState_1_t469E70AF7F7E359CC21E9875D08B1D48632DE915_StaticInit,(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)),il2cpp_codegen_runtime_class_init_inline);
		ListQueryMatcher_1_t5888012D63B675F26A02C9BF208DC2C856BFBBC4* L_1 = ((UQueryState_1_t62CA2841B4A1205A8554F7E9D2A3FBFB0F92B192_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_EnumerationList;
		List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95* L_2 = __this->___iterationList;
		ListQueryMatcher_1_set_matches_mA791583D020B06F7DB407BAA4AC0DC65BF61A2BC_inline(L_1, L_2, NULL);
		ListQueryMatcher_1_t5888012D63B675F26A02C9BF208DC2C856BFBBC4* L_3 = ((UQueryState_1_t62CA2841B4A1205A8554F7E9D2A3FBFB0F92B192_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_EnumerationList;
		UQueryState_1_t62CA2841B4A1205A8554F7E9D2A3FBFB0F92B192 L_4 = ___0_queryState;
		VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* L_5 = L_4.___m_Element;
		UQueryState_1_t62CA2841B4A1205A8554F7E9D2A3FBFB0F92B192 L_6 = ___0_queryState;
		List_1_t7C8CC805CEADA09DFAC2AC1A5D731D5EE956F6DC* L_7 = L_6.___m_Matchers;
		VirtualActionInvoker2< VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115*, List_1_t7C8CC805CEADA09DFAC2AC1A5D731D5EE956F6DC* >::Invoke(7, (UQueryMatcher_tF8783F54BA397641240DA7025126F6301EF53FF8*)L_3, L_5, L_7);
		ListQueryMatcher_1_t5888012D63B675F26A02C9BF208DC2C856BFBBC4* L_8 = ((UQueryState_1_t62CA2841B4A1205A8554F7E9D2A3FBFB0F92B192_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_EnumerationList;
		ListQueryMatcher_1_Reset_m1ED519C668E60E76E856B666DD5FC4B01121A181(L_8, NULL);
		__this->___currentIndex = (-1);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* Enumerator_get_Current_mD195DA373CA07B295F25635B42F3ED5E32221B5C_gshared (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Item_mF58794633948FE8284FCDACC4456686548388092_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	{
		List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95* L_0 = __this->___iterationList;
		int32_t L_1 = __this->___currentIndex;
		VisualElement_t2667F9D19E62C7A315927506C06F223AB9234115* L_2;
		L_2 = List_1_get_Item_mF58794633948FE8284FCDACC4456686548388092(L_0, L_1, List_1_get_Item_mF58794633948FE8284FCDACC4456686548388092_RuntimeMethod_var);
		return ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_2, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)));
	}
}
IL2CPP_EXTERN_C  Il2CppSharedGenericObject* Enumerator_get_Current_mD195DA373CA07B295F25635B42F3ED5E32221B5C_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96>(__this);
	Il2CppSharedGenericObject* _returnValue;
	_returnValue = Enumerator_get_Current_mD195DA373CA07B295F25635B42F3ED5E32221B5C(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m38DD50A147FD01D083464E220301DC3F99A84197_gshared (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	{
		Il2CppSharedGenericObject* L_0;
		L_0 = Enumerator_get_Current_mD195DA373CA07B295F25635B42F3ED5E32221B5C(__this, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
		return (RuntimeObject*)L_0;
	}
}
IL2CPP_EXTERN_C  RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m38DD50A147FD01D083464E220301DC3F99A84197_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96>(__this);
	RuntimeObject* _returnValue;
	_returnValue = Enumerator_System_Collections_IEnumerator_get_Current_m38DD50A147FD01D083464E220301DC3F99A84197(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mFFF471615D4AAD375CD440DAB84DE6979D99625F_gshared (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->___currentIndex;
		V_0 = ((int32_t)il2cpp_codegen_add(L_0, 1));
		int32_t L_1 = V_0;
		__this->___currentIndex = L_1;
		int32_t L_2 = V_0;
		List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95* L_3 = __this->___iterationList;
		int32_t L_4;
		L_4 = List_1_get_Count_m76A83B76330D385CC22ECE544729CDD0FCEAFECC_inline(L_3, NULL);
		return (bool)((((int32_t)L_2) < ((int32_t)L_4))? 1 : 0);
	}
}
IL2CPP_EXTERN_C  bool Enumerator_MoveNext_mFFF471615D4AAD375CD440DAB84DE6979D99625F_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96>(__this);
	bool _returnValue;
	_returnValue = Enumerator_MoveNext_mFFF471615D4AAD375CD440DAB84DE6979D99625F(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_mDD6869C382827C40E00CB14006C453CA19A2F6E6_gshared (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* __this, const RuntimeMethod* method) 
{
	{
		__this->___currentIndex = (-1);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Reset_mDD6869C382827C40E00CB14006C453CA19A2F6E6_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96>(__this);
	Enumerator_Reset_mDD6869C382827C40E00CB14006C453CA19A2F6E6(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m92C8983EF703D0E6C2A069318E1F6728FB825575_gshared (Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&VisualElementListPool_tB82E686FE29EA30D8CEDF6FF352FD1B7E7A30095_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(VisualElementListPool_tB82E686FE29EA30D8CEDF6FF352FD1B7E7A30095_StaticInit);
	{
		List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95* L_0 = __this->___iterationList;
		CHECKED_LOCAL_INIT(VisualElementListPool_tB82E686FE29EA30D8CEDF6FF352FD1B7E7A30095_StaticInit,(VisualElementListPool_tB82E686FE29EA30D8CEDF6FF352FD1B7E7A30095_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		VisualElementListPool_Release_mAF4ED38CC4F9A6E58B06F4B0BD13611F7DECBCFA(L_0, NULL);
		__this->___iterationList = (List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___iterationList), (void*)(List_1_t6115BBE78FE9310B180A2027321DF46F2A06AC95*)NULL);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Dispose_m92C8983EF703D0E6C2A069318E1F6728FB825575_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tD0C3151607C8BBB42633CAA845F02C4B8E2AAA96>(__this);
	Enumerator_Dispose_m92C8983EF703D0E6C2A069318E1F6728FB825575(_thisAdjusted, method);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m54EAA0B597A1197DBC10AF73A3C423B5F8046A75_fshared (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18* ___0_array, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
	{
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18* L_0 = ___0_array;
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_1 = (*(NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18*)L_0);
		il2cpp_codegen_write_field_data<NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),0), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), L_1);
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), (-1));
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericStruct*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4)))), SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m97F29D6F80B478A97A6DA81F665E7DA693E17E64_fshared (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Dispose_m97F29D6F80B478A97A6DA81F665E7DA693E17E64_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405>(__this);
	Enumerator_Dispose_m97F29D6F80B478A97A6DA81F665E7DA693E17E64(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mC9099E56E01AB33169AB4EE888BD9A2B752961DE_fshared (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
	const Il2CppFullySharedGenericStruct L_5 = alloca(SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), ((int32_t)il2cpp_codegen_add(L_0, 1)));
		int32_t L_1 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		int32_t L_2 = (((NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1))))->___m_Length;
		if ((((int32_t)L_1) >= ((int32_t)L_2)))
		{
			goto IL_003f;
		}
	}
	{
		void* L_3 = (((NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1))))->___m_Buffer;
		int32_t L_4 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		UnsafeUtility_ReadArrayElement_TisIl2CppFullySharedGenericStruct_m45AD40013DEE9AEF8031E0DA6F4D23BFD5AA8F42_inline(L_3, L_4, (Il2CppFullySharedGenericStruct*)L_5, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5));
		il2cpp_codegen_write_field_data<true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4), L_5, SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790);
		return (bool)1;
	}

IL_003f:
	{
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericStruct*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4)))), SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790);
		return (bool)0;
	}
}
IL2CPP_EXTERN_C  bool Enumerator_MoveNext_mC9099E56E01AB33169AB4EE888BD9A2B752961DE_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405>(__this);
	bool _returnValue;
	_returnValue = Enumerator_MoveNext_mC9099E56E01AB33169AB4EE888BD9A2B752961DE_inline(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_mB15834348E10F7612876D2AE774493AE7AF604B2_fshared (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	{
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), (-1));
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Reset_mB15834348E10F7612876D2AE774493AE7AF604B2_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405>(__this);
	Enumerator_Reset_mB15834348E10F7612876D2AE774493AE7AF604B2(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_mF9A198DACCE8ED0DC6FD097825D7A0F82D149DBA_fshared (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
	const Il2CppFullySharedGenericStruct L_0 = alloca(SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790);
	{
		il2cpp_codegen_memcpy(L_0, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4)), SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790);
		il2cpp_codegen_memcpy(il2cppRetVal, L_0, SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_get_Current_mF9A198DACCE8ED0DC6FD097825D7A0F82D149DBA_AdjustorThunk (RuntimeObject* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method)
{
	Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405>(__this);
	Enumerator_get_Current_mF9A198DACCE8ED0DC6FD097825D7A0F82D149DBA_inline(_thisAdjusted, il2cppRetVal, method);
	return;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m81767706F1E3B41EAB92F21B1669693FFE92ACCB_fshared (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
	const Il2CppFullySharedGenericStruct L_0 = alloca(SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790);
	{
		Enumerator_get_Current_mF9A198DACCE8ED0DC6FD097825D7A0F82D149DBA_inline(__this, (Il2CppFullySharedGenericStruct*)L_0, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6));
		RuntimeObject* L_1 = Box(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), L_0);
		return L_1;
	}
}
IL2CPP_EXTERN_C  RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m81767706F1E3B41EAB92F21B1669693FFE92ACCB_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405>(__this);
	RuntimeObject* _returnValue;
	_returnValue = Enumerator_System_Collections_IEnumerator_get_Current_m81767706F1E3B41EAB92F21B1669693FFE92ACCB_inline(_thisAdjusted, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m604CC97AA88CA5CFDCC1B41045050F167F893993_fshared (Enumerator_tE1470541970778078D070FF8C523A55C7301DA38* __this, NativePagedList_1_t9B43BA7543672C8F12763C42AC5C493E40A41AA8* ___0_nativePagedList, int32_t ___1_offset, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	int32_t V_0 = 0;
	NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		__this->___m_IndexInCurrentPage = 0;
		__this->___m_IndexOfCurrentPage = 0;
		__this->___m_CountInCurrentPage = 0;
		NativePagedList_1_t9B43BA7543672C8F12763C42AC5C493E40A41AA8* L_0 = ___0_nativePagedList;
		__this->___m_NativePagedList = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_NativePagedList), (void*)L_0);
		V_0 = 0;
		goto IL_0083;
	}

IL_0020:
	{
		NativePagedList_1_t9B43BA7543672C8F12763C42AC5C493E40A41AA8* L_1 = __this->___m_NativePagedList;
		List_1_tDA1565FB54D9C701D544C5C5DEB5F20D33BC07BF* L_2 = L_1->___m_Pages;
		int32_t L_3 = V_0;
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_4;
		L_4 = List_1_get_Item_m944337BED8AAD9BC3015016C70A16A871E20901E(L_2, L_3, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0));
		V_1 = L_4;
		int32_t L_5;
		L_5 = NativeArray_1_get_Length_mBE5CC8B844994CFC4AB434235F915881575E63C8_inline((&V_1), NULL);
		__this->___m_CountInCurrentPage = L_5;
		int32_t L_6 = ___1_offset;
		int32_t L_7 = __this->___m_CountInCurrentPage;
		if ((((int32_t)L_6) < ((int32_t)L_7)))
		{
			goto IL_0054;
		}
	}
	{
		int32_t L_8 = ___1_offset;
		int32_t L_9 = __this->___m_CountInCurrentPage;
		___1_offset = ((int32_t)il2cpp_codegen_subtract(L_8, L_9));
		goto IL_007f;
	}

IL_0054:
	{
		int32_t L_10 = ___1_offset;
		__this->___m_IndexInCurrentPage = L_10;
		int32_t L_11 = V_0;
		__this->___m_IndexOfCurrentPage = L_11;
		NativePagedList_1_t9B43BA7543672C8F12763C42AC5C493E40A41AA8* L_12 = __this->___m_NativePagedList;
		List_1_tDA1565FB54D9C701D544C5C5DEB5F20D33BC07BF* L_13 = L_12->___m_Pages;
		int32_t L_14 = __this->___m_IndexOfCurrentPage;
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_15;
		L_15 = List_1_get_Item_m944337BED8AAD9BC3015016C70A16A871E20901E(L_13, L_14, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0));
		__this->___m_CurrentPage = L_15;
		return;
	}

IL_007f:
	{
		int32_t L_16 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_16, 1));
	}

IL_0083:
	{
		int32_t L_17 = V_0;
		NativePagedList_1_t9B43BA7543672C8F12763C42AC5C493E40A41AA8* L_18 = __this->___m_NativePagedList;
		List_1_tDA1565FB54D9C701D544C5C5DEB5F20D33BC07BF* L_19 = L_18->___m_Pages;
		int32_t L_20;
		L_20 = List_1_get_Count_m0F04F8BA763691AD4F57971309F219BA108367B3_inline(L_19, NULL);
		if ((((int32_t)L_17) < ((int32_t)((int32_t)il2cpp_codegen_subtract(L_20, 1)))))
		{
			goto IL_0020;
		}
	}
	{
		NativePagedList_1_t9B43BA7543672C8F12763C42AC5C493E40A41AA8* L_21 = __this->___m_NativePagedList;
		List_1_tDA1565FB54D9C701D544C5C5DEB5F20D33BC07BF* L_22 = L_21->___m_Pages;
		int32_t L_23;
		L_23 = List_1_get_Count_m0F04F8BA763691AD4F57971309F219BA108367B3_inline(L_22, NULL);
		__this->___m_IndexOfCurrentPage = ((int32_t)il2cpp_codegen_subtract(L_23, 1));
		NativePagedList_1_t9B43BA7543672C8F12763C42AC5C493E40A41AA8* L_24 = __this->___m_NativePagedList;
		int32_t L_25 = L_24->___m_CountInLastPage;
		__this->___m_CountInCurrentPage = L_25;
		int32_t L_26 = ___1_offset;
		__this->___m_IndexInCurrentPage = L_26;
		NativePagedList_1_t9B43BA7543672C8F12763C42AC5C493E40A41AA8* L_27 = __this->___m_NativePagedList;
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_28 = L_27->___m_LastPage;
		__this->___m_CurrentPage = L_28;
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_HasNext_m98065EB05573641926616E576CA797F054D69EF9_fshared (Enumerator_tE1470541970778078D070FF8C523A55C7301DA38* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___m_IndexInCurrentPage;
		int32_t L_1 = __this->___m_CountInCurrentPage;
		return (bool)((((int32_t)L_0) < ((int32_t)L_1))? 1 : 0);
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_GetNext_mFF56DC898EDC545D15C13A1F68E800AC47B09AD1_fshared (Enumerator_tE1470541970778078D070FF8C523A55C7301DA38* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5));
	const Il2CppFullySharedGenericStruct L_4 = alloca(SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	int32_t V_0 = 0;
	NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 V_1;
	memset((&V_1), 0, sizeof(V_1));
	Il2CppFullySharedGenericStruct G_B9_0 = alloca(SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	memset(G_B9_0, 0, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	Il2CppFullySharedGenericStruct G_B3_0 = alloca(SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	memset(G_B3_0, 0, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	Il2CppFullySharedGenericStruct G_B7_0 = alloca(SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	memset(G_B7_0, 0, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	Il2CppFullySharedGenericStruct G_B4_0 = alloca(SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	memset(G_B4_0, 0, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	Il2CppFullySharedGenericStruct G_B6_0 = alloca(SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	memset(G_B6_0, 0, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	Il2CppFullySharedGenericStruct G_B5_0 = alloca(SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	memset(G_B5_0, 0, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	Il2CppFullySharedGenericStruct G_B8_0 = alloca(SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	memset(G_B8_0, 0, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	{
		bool L_0;
		L_0 = Enumerator_HasNext_m98065EB05573641926616E576CA797F054D69EF9(__this, NULL);
		if (L_0)
		{
			goto IL_0013;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_1 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral27E800F6FC3576FA08A781DC2C4A4BD96BA82D76)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, method);
	}

IL_0013:
	{
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18* L_2 = (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18*)(&__this->___m_CurrentPage);
		int32_t L_3 = __this->___m_IndexInCurrentPage;
		NativeArray_1_get_Item_mA8C8A69EB3A5D460C55DFCD27275CD5BA5E2B455_inline(L_2, L_3, (Il2CppFullySharedGenericStruct*)L_4, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
		int32_t L_5 = __this->___m_IndexInCurrentPage;
		__this->___m_IndexInCurrentPage = ((int32_t)il2cpp_codegen_add(L_5, 1));
		int32_t L_6 = __this->___m_IndexInCurrentPage;
		int32_t L_7 = __this->___m_CountInCurrentPage;
		if ((!(((uint32_t)L_6) == ((uint32_t)L_7))))
		{
			il2cpp_codegen_memcpy(G_B9_0, L_4, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
			goto IL_00f8;
		}
		il2cpp_codegen_memcpy(G_B3_0, L_4, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	}
	{
		__this->___m_IndexInCurrentPage = 0;
		int32_t L_8 = __this->___m_IndexOfCurrentPage;
		__this->___m_IndexOfCurrentPage = ((int32_t)il2cpp_codegen_add(L_8, 1));
		NativePagedList_1_t9B43BA7543672C8F12763C42AC5C493E40A41AA8* L_9 = __this->___m_NativePagedList;
		List_1_tDA1565FB54D9C701D544C5C5DEB5F20D33BC07BF* L_10 = L_9->___m_Pages;
		int32_t L_11;
		L_11 = List_1_get_Count_m0F04F8BA763691AD4F57971309F219BA108367B3_inline(L_10, NULL);
		V_0 = L_11;
		int32_t L_12 = __this->___m_IndexOfCurrentPage;
		int32_t L_13 = V_0;
		if ((((int32_t)L_12) >= ((int32_t)L_13)))
		{
			il2cpp_codegen_memcpy(G_B7_0, G_B3_0, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
			goto IL_00b6;
		}
		il2cpp_codegen_memcpy(G_B4_0, G_B3_0, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	}
	{
		int32_t L_14 = __this->___m_IndexOfCurrentPage;
		int32_t L_15 = V_0;
		if ((((int32_t)L_14) >= ((int32_t)((int32_t)il2cpp_codegen_subtract(L_15, 1)))))
		{
			il2cpp_codegen_memcpy(G_B6_0, G_B4_0, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
			goto IL_00a3;
		}
		il2cpp_codegen_memcpy(G_B5_0, G_B4_0, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	}
	{
		NativePagedList_1_t9B43BA7543672C8F12763C42AC5C493E40A41AA8* L_16 = __this->___m_NativePagedList;
		List_1_tDA1565FB54D9C701D544C5C5DEB5F20D33BC07BF* L_17 = L_16->___m_Pages;
		int32_t L_18 = __this->___m_IndexOfCurrentPage;
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_19;
		L_19 = List_1_get_Item_m944337BED8AAD9BC3015016C70A16A871E20901E(L_17, L_18, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0));
		V_1 = L_19;
		int32_t L_20;
		L_20 = NativeArray_1_get_Length_mBE5CC8B844994CFC4AB434235F915881575E63C8_inline((&V_1), NULL);
		__this->___m_CountInCurrentPage = L_20;
		il2cpp_codegen_memcpy(G_B8_0, G_B5_0, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
		goto IL_00dc;
	}

IL_00a3:
	{
		NativePagedList_1_t9B43BA7543672C8F12763C42AC5C493E40A41AA8* L_21 = __this->___m_NativePagedList;
		int32_t L_22 = L_21->___m_CountInLastPage;
		__this->___m_CountInCurrentPage = L_22;
		il2cpp_codegen_memcpy(G_B8_0, G_B6_0, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
		goto IL_00dc;
	}

IL_00b6:
	{
		int32_t L_23 = V_0;
		__this->___m_IndexOfCurrentPage = ((int32_t)il2cpp_codegen_subtract(L_23, 1));
		NativePagedList_1_t9B43BA7543672C8F12763C42AC5C493E40A41AA8* L_24 = __this->___m_NativePagedList;
		int32_t L_25 = L_24->___m_CountInLastPage;
		__this->___m_CountInCurrentPage = L_25;
		int32_t L_26 = __this->___m_CountInCurrentPage;
		__this->___m_IndexInCurrentPage = L_26;
		il2cpp_codegen_memcpy(G_B8_0, G_B7_0, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	}

IL_00dc:
	{
		NativePagedList_1_t9B43BA7543672C8F12763C42AC5C493E40A41AA8* L_27 = __this->___m_NativePagedList;
		List_1_tDA1565FB54D9C701D544C5C5DEB5F20D33BC07BF* L_28 = L_27->___m_Pages;
		int32_t L_29 = __this->___m_IndexOfCurrentPage;
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_30;
		L_30 = List_1_get_Item_m944337BED8AAD9BC3015016C70A16A871E20901E(L_28, L_29, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0));
		__this->___m_CurrentPage = L_30;
		il2cpp_codegen_memcpy(G_B9_0, G_B8_0, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
	}

IL_00f8:
	{
		il2cpp_codegen_memcpy(il2cppRetVal, G_B9_0, SizeOf_T_t1AA07566E8267752B25D59F8EB23262840A40725);
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
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_mDE93458BF02169A52FC3A449C9F0B500A0B25958_fshared (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* __this, NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52* ___0_array, const RuntimeMethod* method) 
{
	{
		NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52* L_0 = ___0_array;
		NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52 L_1 = (*(NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52*)L_0);
		__this->___m_Array = L_1;
		__this->___m_Index = (-1);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m0BBB67CF7366B5A989FBB12AA2293CD18853C98D_fshared (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Dispose_m0BBB67CF7366B5A989FBB12AA2293CD18853C98D_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817>(__this);
	Enumerator_Dispose_m0BBB67CF7366B5A989FBB12AA2293CD18853C98D(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m0DEF4AFE20672D3425DCFB0B4B7EBBAA7B69F595_fshared (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___m_Index;
		__this->___m_Index = ((int32_t)il2cpp_codegen_add(L_0, 1));
		int32_t L_1 = __this->___m_Index;
		NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52* L_2 = (NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52*)(&__this->___m_Array);
		int32_t L_3;
		L_3 = NativeSlice_1_get_Length_m0225CA0944599882AC9C2A06A99FDC685362AFBE_inline(L_2, NULL);
		return (bool)((((int32_t)L_1) < ((int32_t)L_3))? 1 : 0);
	}
}
IL2CPP_EXTERN_C  bool Enumerator_MoveNext_m0DEF4AFE20672D3425DCFB0B4B7EBBAA7B69F595_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817>(__this);
	bool _returnValue;
	_returnValue = Enumerator_MoveNext_m0DEF4AFE20672D3425DCFB0B4B7EBBAA7B69F595(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_m7DFE6628F82F51C868E30AF059855DEBF7ABE53A_fshared (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* __this, const RuntimeMethod* method) 
{
	{
		__this->___m_Index = (-1);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Reset_m7DFE6628F82F51C868E30AF059855DEBF7ABE53A_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817>(__this);
	Enumerator_Reset_m7DFE6628F82F51C868E30AF059855DEBF7ABE53A(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_mD5F9A8DA4A689D3BE78549843241C5C6D4936D84_fshared (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t6DEC264D110932E9F21C7E232C370F10210B8165 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
	const Il2CppFullySharedGenericStruct L_2 = alloca(SizeOf_T_t6DEC264D110932E9F21C7E232C370F10210B8165);
	{
		NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52* L_0 = (NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52*)(&__this->___m_Array);
		int32_t L_1 = __this->___m_Index;
		NativeSlice_1_get_Item_mB5E7BDE4EDF7E31480A9785BE103CC7E17E0E3A8(L_0, L_1, (Il2CppFullySharedGenericStruct*)L_2, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_2, SizeOf_T_t6DEC264D110932E9F21C7E232C370F10210B8165);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_get_Current_mD5F9A8DA4A689D3BE78549843241C5C6D4936D84_AdjustorThunk (RuntimeObject* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method)
{
	Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817>(__this);
	Enumerator_get_Current_mD5F9A8DA4A689D3BE78549843241C5C6D4936D84(_thisAdjusted, il2cppRetVal, method);
	return;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mEB8088957B367DEEE33935D4174B19579BF14B58_fshared (Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t6DEC264D110932E9F21C7E232C370F10210B8165 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
	const Il2CppFullySharedGenericStruct L_0 = alloca(SizeOf_T_t6DEC264D110932E9F21C7E232C370F10210B8165);
	{
		Enumerator_get_Current_mD5F9A8DA4A689D3BE78549843241C5C6D4936D84(__this, (Il2CppFullySharedGenericStruct*)L_0, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		RuntimeObject* L_1 = Box(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), L_0);
		return L_1;
	}
}
IL2CPP_EXTERN_C  RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mEB8088957B367DEEE33935D4174B19579BF14B58_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tF7A3F0CE48AD9E97DE26F789092763ACB7D7F817>(__this);
	RuntimeObject* _returnValue;
	_returnValue = Enumerator_System_Collections_IEnumerator_get_Current_mEB8088957B367DEEE33935D4174B19579BF14B58(_thisAdjusted, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m6DCF4D06C211EBCD1DAF60721FC6976973BFC0F4_fshared (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* __this, UnmanagedRefCountedList_1_t8B02F7EA0267E2F24A9C139864512B7B0BBE81BC ___0_list, const RuntimeMethod* method) 
{
	{
		UnmanagedRefCountedList_1_t8B02F7EA0267E2F24A9C139864512B7B0BBE81BC L_0 = ___0_list;
		Data_t6F131ABB2DC3608FBC19EEE1D985484C670DE41A* L_1 = L_0.___m_Data;
		__this->___m_Data = L_1;
		__this->___m_Index = (-1);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m06F26A9D1616DCF8DE123B05DB3C01719237C3AF_fshared (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	{
		int32_t L_0 = __this->___m_Index;
		__this->___m_Index = ((int32_t)il2cpp_codegen_add(L_0, 1));
		Data_t6F131ABB2DC3608FBC19EEE1D985484C670DE41A* L_1 = __this->___m_Data;
		uintptr_t L_2 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(0,NULL));
		if ((((intptr_t)L_1) == ((intptr_t)L_2)))
		{
			goto IL_002c;
		}
	}
	{
		int32_t L_3 = __this->___m_Index;
		Data_t6F131ABB2DC3608FBC19EEE1D985484C670DE41A* L_4 = __this->___m_Data;
		int32_t L_5 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(L_4, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		return (bool)((((int32_t)L_3) < ((int32_t)L_5))? 1 : 0);
	}

IL_002c:
	{
		return (bool)0;
	}
}
IL2CPP_EXTERN_C  bool Enumerator_MoveNext_m06F26A9D1616DCF8DE123B05DB3C01719237C3AF_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72>(__this);
	bool _returnValue;
	_returnValue = Enumerator_MoveNext_m06F26A9D1616DCF8DE123B05DB3C01719237C3AF(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_mEC7989864CADAFF8FC0E8487223BF86743692433_fshared (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* __this, const RuntimeMethod* method) 
{
	{
		__this->___m_Index = (-1);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Reset_mEC7989864CADAFF8FC0E8487223BF86743692433_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72>(__this);
	Enumerator_Reset_mEC7989864CADAFF8FC0E8487223BF86743692433(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_m347C1E11612C5D8DBEFB997C4F82908223501003_fshared (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t6F18478E9D9AF4C2BEBFBEF94C4504CB6F03EDA2 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
	const Il2CppFullySharedGenericStruct L_5 = alloca(SizeOf_T_t6F18478E9D9AF4C2BEBFBEF94C4504CB6F03EDA2);
	{
		Data_t6F131ABB2DC3608FBC19EEE1D985484C670DE41A* L_0 = __this->___m_Data;
		uintptr_t L_1 = (il2cpp_codegen_conv<uintptr_t,Il2CppFullySharedGenericStruct*,intptr_t,false,false>((((Il2CppFullySharedGenericStruct*)il2cpp_codegen_get_field_data_pointer(L_0, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)))),NULL));
		int32_t L_2 = __this->___m_Index;
		intptr_t L_3 = (il2cpp_codegen_conv<intptr_t,int32_t,int32_t,false,false>(L_2,NULL));
		uint32_t L_4 = SizeOf_T_t6F18478E9D9AF4C2BEBFBEF94C4504CB6F03EDA2;
		il2cpp_codegen_memcpy(L_5, ((intptr_t)il2cpp_codegen_add((intptr_t)L_1, ((intptr_t)il2cpp_codegen_multiply(L_3, (int32_t)L_4)))), SizeOf_T_t6F18478E9D9AF4C2BEBFBEF94C4504CB6F03EDA2);
		il2cpp_codegen_memcpy(il2cppRetVal, L_5, SizeOf_T_t6F18478E9D9AF4C2BEBFBEF94C4504CB6F03EDA2);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_get_Current_m347C1E11612C5D8DBEFB997C4F82908223501003_AdjustorThunk (RuntimeObject* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method)
{
	Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72>(__this);
	Enumerator_get_Current_m347C1E11612C5D8DBEFB997C4F82908223501003(_thisAdjusted, il2cppRetVal, method);
	return;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mA0F433CE2FCFD54A32DFD9BE11A2E16310CC4E07_fshared (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t6F18478E9D9AF4C2BEBFBEF94C4504CB6F03EDA2 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
	const Il2CppFullySharedGenericStruct L_0 = alloca(SizeOf_T_t6F18478E9D9AF4C2BEBFBEF94C4504CB6F03EDA2);
	{
		Enumerator_get_Current_m347C1E11612C5D8DBEFB997C4F82908223501003(__this, (Il2CppFullySharedGenericStruct*)L_0, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
		RuntimeObject* L_1 = Box(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), L_0);
		return L_1;
	}
}
IL2CPP_EXTERN_C  RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mA0F433CE2FCFD54A32DFD9BE11A2E16310CC4E07_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72>(__this);
	RuntimeObject* _returnValue;
	_returnValue = Enumerator_System_Collections_IEnumerator_get_Current_mA0F433CE2FCFD54A32DFD9BE11A2E16310CC4E07(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m62AB801313FAA9B181EBFE46DB8B2DB6D76B68B4_fshared (Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Dispose_m62AB801313FAA9B181EBFE46DB8B2DB6D76B68B4_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t9B14991AA60E3D342BAA3D67463587A5B697BA72>(__this);
	Enumerator_Dispose_m62AB801313FAA9B181EBFE46DB8B2DB6D76B68B4(_thisAdjusted, method);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m7E124C7D7F367A3EC2AE25CA5F4D1284F5FA2E43_fshared (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, ReadOnly_tC6998C67EE7BA262710FFDCF6BE6728CB987FDE8* ___0_array, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
	{
		ReadOnly_tC6998C67EE7BA262710FFDCF6BE6728CB987FDE8* L_0 = ___0_array;
		ReadOnly_tC6998C67EE7BA262710FFDCF6BE6728CB987FDE8 L_1 = (*(ReadOnly_tC6998C67EE7BA262710FFDCF6BE6728CB987FDE8*)L_0);
		il2cpp_codegen_write_field_data<ReadOnly_tC6998C67EE7BA262710FFDCF6BE6728CB987FDE8, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),0), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), L_1);
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), (-1));
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericStruct*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4)))), SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mCB9B6D15D244D49204D2A903402C1D5011CC2830_fshared (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Dispose_mCB9B6D15D244D49204D2A903402C1D5011CC2830_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C>(__this);
	Enumerator_Dispose_mCB9B6D15D244D49204D2A903402C1D5011CC2830(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m9589C320A0B345A9190284C38FCB97F2015861DA_fshared (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
	const Il2CppFullySharedGenericStruct L_5 = alloca(SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), ((int32_t)il2cpp_codegen_add(L_0, 1)));
		int32_t L_1 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		int32_t L_2 = (((ReadOnly_tC6998C67EE7BA262710FFDCF6BE6728CB987FDE8*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1))))->___m_Length;
		if ((((int32_t)L_1) >= ((int32_t)L_2)))
		{
			goto IL_003f;
		}
	}
	{
		void* L_3 = (((ReadOnly_tC6998C67EE7BA262710FFDCF6BE6728CB987FDE8*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1))))->___m_Buffer;
		int32_t L_4 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		UnsafeUtility_ReadArrayElement_TisIl2CppFullySharedGenericStruct_m45AD40013DEE9AEF8031E0DA6F4D23BFD5AA8F42_inline(L_3, L_4, (Il2CppFullySharedGenericStruct*)L_5, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5));
		il2cpp_codegen_write_field_data<true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4), L_5, SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F);
		return (bool)1;
	}

IL_003f:
	{
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericStruct*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4)))), SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F);
		return (bool)0;
	}
}
IL2CPP_EXTERN_C  bool Enumerator_MoveNext_m9589C320A0B345A9190284C38FCB97F2015861DA_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C>(__this);
	bool _returnValue;
	_returnValue = Enumerator_MoveNext_m9589C320A0B345A9190284C38FCB97F2015861DA_inline(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_m43C21832D8660DF1F98FA2733CE9D752ACB020D5_fshared (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	{
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), (-1));
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Reset_m43C21832D8660DF1F98FA2733CE9D752ACB020D5_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C>(__this);
	Enumerator_Reset_m43C21832D8660DF1F98FA2733CE9D752ACB020D5(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_m44CEFC7935FF87861A589ACDB1024033E52A123E_fshared (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
	const Il2CppFullySharedGenericStruct L_0 = alloca(SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F);
	{
		il2cpp_codegen_memcpy(L_0, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4)), SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F);
		il2cpp_codegen_memcpy(il2cppRetVal, L_0, SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_get_Current_m44CEFC7935FF87861A589ACDB1024033E52A123E_AdjustorThunk (RuntimeObject* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method)
{
	Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C>(__this);
	Enumerator_get_Current_m44CEFC7935FF87861A589ACDB1024033E52A123E_inline(_thisAdjusted, il2cppRetVal, method);
	return;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m9E9FBC7AD882EDFB5FDCF6024ECA5DA4B2DD24DA_fshared (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
	const Il2CppFullySharedGenericStruct L_0 = alloca(SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F);
	{
		Enumerator_get_Current_m44CEFC7935FF87861A589ACDB1024033E52A123E_inline(__this, (Il2CppFullySharedGenericStruct*)L_0, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6));
		RuntimeObject* L_1 = Box(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), L_0);
		return L_1;
	}
}
IL2CPP_EXTERN_C  RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m9E9FBC7AD882EDFB5FDCF6024ECA5DA4B2DD24DA_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C>(__this);
	RuntimeObject* _returnValue;
	_returnValue = Enumerator_System_Collections_IEnumerator_get_Current_m9E9FBC7AD882EDFB5FDCF6024ECA5DA4B2DD24DA(_thisAdjusted, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m62B32E8D2F70569824269DDC82F384BDBC8662F5_fshared (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* __this, HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87* ___0_set, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	{
		HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87* L_0 = ___0_set;
		il2cpp_codegen_write_field_data<HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87*, true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),0), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), L_0);
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), 0);
		HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87* L_1 = ___0_set;
		int32_t L_2 = L_1->____version;
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), L_2);
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m915D35B9BB2A77C5396F4FE1A1C53F1C93B8E337_fshared (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Dispose_m915D35B9BB2A77C5396F4FE1A1C53F1C93B8E337_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB>(__this);
	Enumerator_Dispose_m915D35B9BB2A77C5396F4FE1A1C53F1C93B8E337(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m1756F06B6BC460B8EEB4522B562E097F37D54C59_fshared (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_11 = alloca(SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87* L_1 = *(HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_2 = L_1->____version;
		if ((((int32_t)L_0) == ((int32_t)L_2)))
		{
			goto IL_007b;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_3 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_3, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralF8D08FCF1537043BF0289FA98C51BF5A3AC7C618)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, method);
	}

IL_001e:
	{
		HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87* L_4 = *(HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		SlotU5BU5D_t2B9148416634BCB8743E1F61254B305F1882FF1D* L_5 = L_4->____slots;
		int32_t L_6 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		int32_t L_7 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(((Slot_tEC146EEEF7022C6542EBF082D658446682962BFD*)(L_5)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_6))), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		if ((((int32_t)L_7) < ((int32_t)0)))
		{
			goto IL_006d;
		}
	}
	{
		HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87* L_8 = *(HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		SlotU5BU5D_t2B9148416634BCB8743E1F61254B305F1882FF1D* L_9 = L_8->____slots;
		int32_t L_10 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		il2cpp_codegen_memcpy(L_11, il2cpp_codegen_get_field_data_pointer(((Slot_tEC146EEEF7022C6542EBF082D658446682962BFD*)(L_9)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_10))), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8)), SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8);
		il2cpp_codegen_write_field_data<true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),3), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5), L_11, SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8);
		int32_t L_12 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), ((int32_t)il2cpp_codegen_add(L_12, 1)));
		return (bool)1;
	}

IL_006d:
	{
		int32_t L_13 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), ((int32_t)il2cpp_codegen_add(L_13, 1)));
	}

IL_007b:
	{
		int32_t L_14 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87* L_15 = *(HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_16 = L_15->____lastIndex;
		if ((((int32_t)L_14) < ((int32_t)L_16)))
		{
			goto IL_001e;
		}
	}
	{
		HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87* L_17 = *(HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_18 = L_17->____lastIndex;
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), ((int32_t)il2cpp_codegen_add(L_18, 1)));
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8);
		return (bool)0;
	}
}
IL2CPP_EXTERN_C  bool Enumerator_MoveNext_m1756F06B6BC460B8EEB4522B562E097F37D54C59_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB>(__this);
	bool _returnValue;
	_returnValue = Enumerator_MoveNext_m1756F06B6BC460B8EEB4522B562E097F37D54C59(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_m51282E47C90E55BF2D96A0D329869ABC122A85A0_fshared (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_0 = alloca(SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8);
	{
		il2cpp_codegen_memcpy(L_0, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)), SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8);
		il2cpp_codegen_memcpy(il2cppRetVal, L_0, SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_get_Current_m51282E47C90E55BF2D96A0D329869ABC122A85A0_AdjustorThunk (RuntimeObject* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB>(__this);
	Enumerator_get_Current_m51282E47C90E55BF2D96A0D329869ABC122A85A0_inline(_thisAdjusted, il2cppRetVal, method);
	return;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m7D04FEF0F3E60E6E691D979806634E034498134E_fshared (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_5 = alloca(SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		if (!L_0)
		{
			goto IL_001d;
		}
	}
	{
		int32_t L_1 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87* L_2 = *(HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_3 = L_2->____lastIndex;
		if ((!(((uint32_t)L_1) == ((uint32_t)((int32_t)il2cpp_codegen_add(L_3, 1))))))
		{
			goto IL_0028;
		}
	}

IL_001d:
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_4 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_4, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral63FC874122847D14784CB3ADBE59A08B9558FA97)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_4, method);
	}

IL_0028:
	{
		il2cpp_codegen_memcpy(L_5, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)), SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8);
		RuntimeObject* L_6 = Box(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4), L_5);
		return L_6;
	}
}
IL2CPP_EXTERN_C  RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m7D04FEF0F3E60E6E691D979806634E034498134E_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB>(__this);
	RuntimeObject* _returnValue;
	_returnValue = Enumerator_System_Collections_IEnumerator_get_Current_m7D04FEF0F3E60E6E691D979806634E034498134E(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_System_Collections_IEnumerator_Reset_m27D5470C040D50CC0200DDAB3C93DF3C4CA755B3_fshared (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87* L_1 = *(HashSet_1_t2E820DA94CC6D5A61CA29EBD9BE297C43D691C87**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_2 = L_1->____version;
		if ((((int32_t)L_0) == ((int32_t)L_2)))
		{
			goto IL_001e;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_3 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_3, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralF8D08FCF1537043BF0289FA98C51BF5A3AC7C618)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, method);
	}

IL_001e:
	{
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), 0);
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_System_Collections_IEnumerator_Reset_m27D5470C040D50CC0200DDAB3C93DF3C4CA755B3_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB>(__this);
	Enumerator_System_Collections_IEnumerator_Reset_m27D5470C040D50CC0200DDAB3C93DF3C4CA755B3(_thisAdjusted, method);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mF3F5043F0C7EA67C19F3FA22DF2A5E66C4B82985_fshared (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_1 = *(int32_t*)il2cpp_codegen_get_field_data_pointer((((InlinedArray_1_tCA1A9B410EBCE10320B39011CD2E337C395D7CA3*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)))), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
		if ((((int32_t)L_0) < ((int32_t)L_1)))
		{
			goto IL_0015;
		}
	}
	{
		return (bool)0;
	}

IL_0015:
	{
		int32_t L_2 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), ((int32_t)il2cpp_codegen_add(L_2, 1)));
		int32_t L_3 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_4 = *(int32_t*)il2cpp_codegen_get_field_data_pointer((((InlinedArray_1_tCA1A9B410EBCE10320B39011CD2E337C395D7CA3*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)))), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
		return (bool)((((int32_t)L_3) < ((int32_t)L_4))? 1 : 0);
	}
}
IL2CPP_EXTERN_C  bool Enumerator_MoveNext_mF3F5043F0C7EA67C19F3FA22DF2A5E66C4B82985_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092>(__this);
	bool _returnValue;
	_returnValue = Enumerator_MoveNext_mF3F5043F0C7EA67C19F3FA22DF2A5E66C4B82985(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_mC98285CB59B476CB33C08E3FDEC3AD026FD5D46B_fshared (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	{
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), (-1));
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Reset_mC98285CB59B476CB33C08E3FDEC3AD026FD5D46B_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092>(__this);
	Enumerator_Reset_mC98285CB59B476CB33C08E3FDEC3AD026FD5D46B(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_m29E9E90BB06DAEDA6F7D4721A6681EC28A0E54C6_fshared (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_TValue_t358567379BF95FBB1E1F4C7C782060F238FA97AC = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_TValue_t358567379BF95FBB1E1F4C7C782060F238FA97AC);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		InlinedArray_1_get_Item_m559A0B1FCFEC59844BBCB769C3C743C5F46A2201((((InlinedArray_1_tCA1A9B410EBCE10320B39011CD2E337C395D7CA3*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)))), L_0, (Il2CppFullySharedGenericAny*)L_1, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5));
		il2cpp_codegen_memcpy(il2cppRetVal, L_1, SizeOf_TValue_t358567379BF95FBB1E1F4C7C782060F238FA97AC);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_get_Current_m29E9E90BB06DAEDA6F7D4721A6681EC28A0E54C6_AdjustorThunk (RuntimeObject* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092>(__this);
	Enumerator_get_Current_m29E9E90BB06DAEDA6F7D4721A6681EC28A0E54C6(_thisAdjusted, il2cppRetVal, method);
	return;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mC035FEFC4C4C462ECCBF7506FD23025A38501032_fshared (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_TValue_t358567379BF95FBB1E1F4C7C782060F238FA97AC = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6));
	const Il2CppFullySharedGenericAny L_0 = alloca(SizeOf_TValue_t358567379BF95FBB1E1F4C7C782060F238FA97AC);
	{
		Enumerator_get_Current_m29E9E90BB06DAEDA6F7D4721A6681EC28A0E54C6(__this, (Il2CppFullySharedGenericAny*)L_0, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		RuntimeObject* L_1 = Box(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6), L_0);
		return L_1;
	}
}
IL2CPP_EXTERN_C  RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mC035FEFC4C4C462ECCBF7506FD23025A38501032_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092>(__this);
	RuntimeObject* _returnValue;
	_returnValue = Enumerator_System_Collections_IEnumerator_get_Current_mC035FEFC4C4C462ECCBF7506FD23025A38501032(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m6CAB80EFB3B442E668D334714C1BA63697F23576_fshared (Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Dispose_m6CAB80EFB3B442E668D334714C1BA63697F23576_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t19CF5B142C374BA8F020A031DE19590D358EA092>(__this);
	Enumerator_Dispose_m6CAB80EFB3B442E668D334714C1BA63697F23576(_thisAdjusted, method);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m0F98614729A5017594BCB5D1D847C1D319913311_fshared (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9* ___0_list, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	{
		LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9* L_0 = ___0_list;
		il2cpp_codegen_write_field_data<LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9*, true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),0), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), L_0);
		LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9* L_1 = ___0_list;
		int32_t L_2 = L_1->___version;
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), L_2);
		LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9* L_3 = ___0_list;
		LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9* L_4 = L_3->___head;
		il2cpp_codegen_write_field_data<LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9*, true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), L_4);
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E);
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),4), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6), 0);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m37AB9FF23CB9A754FCD2CA40D0B482F61F9D6531_fshared (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37* ___0_info, StreamingContext_t56760522A751890146EE45F82F866B55B7E33677 ___1_context, const RuntimeMethod* method) 
{
	{
		PlatformNotSupportedException_tD2BD7EB9278518AA5FE8AE75AD5D0D4298A4631A* L_0 = (PlatformNotSupportedException_tD2BD7EB9278518AA5FE8AE75AD5D0D4298A4631A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&PlatformNotSupportedException_tD2BD7EB9278518AA5FE8AE75AD5D0D4298A4631A_il2cpp_TypeInfo_var)));
		PlatformNotSupportedException__ctor_mD5DBE8E9A6FF4B75EF02671029C6D67A51EAFBD1(L_0, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_m5BA2E5A6E9016D327B7C3067D3D2B2016D37DFDA_fshared (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_0 = alloca(SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E);
	{
		il2cpp_codegen_memcpy(L_0, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)), SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E);
		il2cpp_codegen_memcpy(il2cppRetVal, L_0, SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_get_Current_m5BA2E5A6E9016D327B7C3067D3D2B2016D37DFDA_AdjustorThunk (RuntimeObject* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457>(__this);
	Enumerator_get_Current_m5BA2E5A6E9016D327B7C3067D3D2B2016D37DFDA_inline(_thisAdjusted, il2cppRetVal, method);
	return;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m0F26F0C362DA633022A66F6C342673D39AD4622B_fshared (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_5 = alloca(SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6));
		if (!L_0)
		{
			goto IL_001d;
		}
	}
	{
		int32_t L_1 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6));
		LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9* L_2 = *(LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_3 = L_2->___count;
		if ((!(((uint32_t)L_1) == ((uint32_t)((int32_t)il2cpp_codegen_add(L_3, 1))))))
		{
			goto IL_0028;
		}
	}

IL_001d:
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_4 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_4, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral63FC874122847D14784CB3ADBE59A08B9558FA97)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_4, method);
	}

IL_0028:
	{
		il2cpp_codegen_memcpy(L_5, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)), SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E);
		RuntimeObject* L_6 = Box(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4), L_5);
		return L_6;
	}
}
IL2CPP_EXTERN_C  RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m0F26F0C362DA633022A66F6C342673D39AD4622B_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457>(__this);
	RuntimeObject* _returnValue;
	_returnValue = Enumerator_System_Collections_IEnumerator_get_Current_m0F26F0C362DA633022A66F6C342673D39AD4622B(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m4FB57970CC4FBEE295E533DE12F0B21FF7E13AAA_fshared (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_9 = alloca(SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9* L_1 = *(LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_2 = L_1->___version;
		if ((((int32_t)L_0) == ((int32_t)L_2)))
		{
			goto IL_001e;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_3 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_3, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralF8D08FCF1537043BF0289FA98C51BF5A3AC7C618)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, method);
	}

IL_001e:
	{
		LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9* L_4 = *(LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		if (L_4)
		{
			goto IL_003b;
		}
	}
	{
		LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9* L_5 = *(LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_6 = L_5->___count;
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),4), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6), ((int32_t)il2cpp_codegen_add(L_6, 1)));
		return (bool)0;
	}

IL_003b:
	{
		int32_t L_7 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6));
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),4), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6), ((int32_t)il2cpp_codegen_add(L_7, 1)));
		LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9* L_8 = *(LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		il2cpp_codegen_memcpy(L_9, il2cpp_codegen_get_field_data_pointer(L_8, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8)), SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E);
		il2cpp_codegen_write_field_data<true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),3), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5), L_9, SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E);
		LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9* L_10 = *(LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9* L_11 = *(LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9**)il2cpp_codegen_get_field_data_pointer(L_10, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 9));
		il2cpp_codegen_write_field_data<LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9*, true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), L_11);
		LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9* L_12 = *(LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9* L_13 = *(LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9* L_14 = L_13->___head;
		if ((!(((RuntimeObject*)(LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9*)L_12) == ((RuntimeObject*)(LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9*)L_14))))
		{
			goto IL_0085;
		}
	}
	{
		il2cpp_codegen_write_field_data<LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9*, true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), (LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9*)NULL);
	}

IL_0085:
	{
		return (bool)1;
	}
}
IL2CPP_EXTERN_C  bool Enumerator_MoveNext_m4FB57970CC4FBEE295E533DE12F0B21FF7E13AAA_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457>(__this);
	bool _returnValue;
	_returnValue = Enumerator_MoveNext_m4FB57970CC4FBEE295E533DE12F0B21FF7E13AAA(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_System_Collections_IEnumerator_Reset_mEE0F6DC54C3238FF39CDFF5675B11CD97C544897_fshared (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9* L_1 = *(LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_2 = L_1->___version;
		if ((((int32_t)L_0) == ((int32_t)L_2)))
		{
			goto IL_001e;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_3 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_3, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralF8D08FCF1537043BF0289FA98C51BF5A3AC7C618)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, method);
	}

IL_001e:
	{
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E);
		LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9* L_4 = *(LinkedList_1_tF459954AFF413E172A3D62178CA206041C9D1BF9**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9* L_5 = L_4->___head;
		il2cpp_codegen_write_field_data<LinkedListNode_1_tB885B6403BB775D7A06ED42A773BC5EB23FDE7E9*, true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), L_5);
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),4), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6), 0);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_System_Collections_IEnumerator_Reset_mEE0F6DC54C3238FF39CDFF5675B11CD97C544897_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457>(__this);
	Enumerator_System_Collections_IEnumerator_Reset_mEE0F6DC54C3238FF39CDFF5675B11CD97C544897(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m25395D42D3EE1E89A0B024E2FAF823E4A3FEF4D1_fshared (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Dispose_m25395D42D3EE1E89A0B024E2FAF823E4A3FEF4D1_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457>(__this);
	Enumerator_Dispose_m25395D42D3EE1E89A0B024E2FAF823E4A3FEF4D1(_thisAdjusted, method);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m5C66A91DC68887BE203985B56EF2F74222E9AF50_fshared (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* ___0_list, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	{
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_0 = ___0_list;
		il2cpp_codegen_write_field_data<List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),0), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), L_0);
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), 0);
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_1 = ___0_list;
		int32_t L_2 = L_1->____version;
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), L_2);
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mFE1EBE6F6425283FEAEAE7C79D02CDE4F9D367E8_fshared (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Dispose_mFE1EBE6F6425283FEAEAE7C79D02CDE4F9D367E8_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF>(__this);
	Enumerator_Dispose_mFE1EBE6F6425283FEAEAE7C79D02CDE4F9D367E8(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m8D8E5E878AF0A88A535AB1AB5BA4F23E151A678A_fshared (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_11 = alloca(SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1);
	List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* V_0 = NULL;
	{
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_0 = *(List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		V_0 = L_0;
		int32_t L_1 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_2 = V_0;
		int32_t L_3 = L_2->____version;
		if ((!(((uint32_t)L_1) == ((uint32_t)L_3))))
		{
			goto IL_004a;
		}
	}
	{
		int32_t L_4 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_5 = V_0;
		int32_t L_6 = L_5->____size;
		if ((!(((uint32_t)L_4) < ((uint32_t)L_6))))
		{
			goto IL_004a;
		}
	}
	{
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_7 = V_0;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_8 = L_7->____items;
		int32_t L_9 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		int32_t L_10 = L_9;
		il2cpp_codegen_memcpy(L_11, (L_8)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_10)), SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1);
		il2cpp_codegen_write_field_data<true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),3), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5), L_11, SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1);
		int32_t L_12 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), ((int32_t)il2cpp_codegen_add(L_12, 1)));
		return (bool)1;
	}

IL_004a:
	{
		bool L_13;
		L_13 = Enumerator_MoveNextRare_mF1A6D7920FDC3C83A7DA1F838C2FC51D716637D9(__this, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6));
		return L_13;
	}
}
IL2CPP_EXTERN_C  bool Enumerator_MoveNext_m8D8E5E878AF0A88A535AB1AB5BA4F23E151A678A_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF>(__this);
	bool _returnValue;
	_returnValue = Enumerator_MoveNext_m8D8E5E878AF0A88A535AB1AB5BA4F23E151A678A(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNextRare_mF1A6D7920FDC3C83A7DA1F838C2FC51D716637D9_fshared (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_1 = *(List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_2 = L_1->____version;
		if ((((int32_t)L_0) == ((int32_t)L_2)))
		{
			goto IL_0018;
		}
	}
	{
		ThrowHelper_ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion_m5331E2E0EC0E36843D53F439C2529530595ACE9F(NULL);
	}

IL_0018:
	{
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_3 = *(List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_4 = L_3->____size;
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), ((int32_t)il2cpp_codegen_add(L_4, 1)));
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1);
		return (bool)0;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_m8B42D4B2DE853B9D11B997120CD0228D4780E394_fshared (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_0 = alloca(SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1);
	{
		il2cpp_codegen_memcpy(L_0, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)), SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1);
		il2cpp_codegen_memcpy(il2cppRetVal, L_0, SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_get_Current_m8B42D4B2DE853B9D11B997120CD0228D4780E394_AdjustorThunk (RuntimeObject* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF>(__this);
	Enumerator_get_Current_m8B42D4B2DE853B9D11B997120CD0228D4780E394_inline(_thisAdjusted, il2cppRetVal, method);
	return;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m3FCE22D15C040EE9F8614C711F786205BA6AAFB6_fshared (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_4 = alloca(SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		if (!L_0)
		{
			goto IL_001d;
		}
	}
	{
		int32_t L_1 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_2 = *(List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_3 = L_2->____size;
		if ((!(((uint32_t)L_1) == ((uint32_t)((int32_t)il2cpp_codegen_add(L_3, 1))))))
		{
			goto IL_0022;
		}
	}

IL_001d:
	{
		ThrowHelper_ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen_m61D5A6F694C15854BCF089BFD945BD6DBDB47D4E(NULL);
	}

IL_0022:
	{
		il2cpp_codegen_memcpy(L_4, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)), SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1);
		RuntimeObject* L_5 = Box(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4), L_4);
		return L_5;
	}
}
IL2CPP_EXTERN_C  RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m3FCE22D15C040EE9F8614C711F786205BA6AAFB6_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF>(__this);
	RuntimeObject* _returnValue;
	_returnValue = Enumerator_System_Collections_IEnumerator_get_Current_m3FCE22D15C040EE9F8614C711F786205BA6AAFB6(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_System_Collections_IEnumerator_Reset_mFA2701BBDA14ED6CC3113A32ED0E8B80DAD58A84_fshared (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_1 = *(List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_2 = L_1->____version;
		if ((((int32_t)L_0) == ((int32_t)L_2)))
		{
			goto IL_0018;
		}
	}
	{
		ThrowHelper_ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion_m5331E2E0EC0E36843D53F439C2529530595ACE9F(NULL);
	}

IL_0018:
	{
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), 0);
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_System_Collections_IEnumerator_Reset_mFA2701BBDA14ED6CC3113A32ED0E8B80DAD58A84_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF>(__this);
	Enumerator_System_Collections_IEnumerator_Reset_mFA2701BBDA14ED6CC3113A32ED0E8B80DAD58A84(_thisAdjusted, method);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m6C59AA282C548B36E982B84CACC3A8E4AA904775_fshared (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9* ___0_list, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	{
		LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9* L_0 = ___0_list;
		il2cpp_codegen_write_field_data<LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9*, true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),0), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), L_0);
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), 0);
		LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9* L_1 = ___0_list;
		int32_t L_2 = ((LowLevelList_1_tFA10A51C575F28B96FF2E2E7FB3740E48546A1E0*)L_1)->____version;
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), L_2);
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mDE5B605B7794A192CC35F12013300AE26EB8FE5F_fshared (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Dispose_mDE5B605B7794A192CC35F12013300AE26EB8FE5F_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0>(__this);
	Enumerator_Dispose_mDE5B605B7794A192CC35F12013300AE26EB8FE5F(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m2F9CDD8406C6495182E05B55A00C3D0C9620CA67_fshared (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_11 = alloca(SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274);
	LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9* V_0 = NULL;
	{
		LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9* L_0 = *(LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		V_0 = L_0;
		int32_t L_1 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9* L_2 = V_0;
		int32_t L_3 = ((LowLevelList_1_tFA10A51C575F28B96FF2E2E7FB3740E48546A1E0*)L_2)->____version;
		if ((!(((uint32_t)L_1) == ((uint32_t)L_3))))
		{
			goto IL_004a;
		}
	}
	{
		int32_t L_4 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9* L_5 = V_0;
		int32_t L_6 = ((LowLevelList_1_tFA10A51C575F28B96FF2E2E7FB3740E48546A1E0*)L_5)->____size;
		if ((!(((uint32_t)L_4) < ((uint32_t)L_6))))
		{
			goto IL_004a;
		}
	}
	{
		LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9* L_7 = V_0;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_8 = ((LowLevelList_1_tFA10A51C575F28B96FF2E2E7FB3740E48546A1E0*)L_7)->____items;
		int32_t L_9 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		int32_t L_10 = L_9;
		il2cpp_codegen_memcpy(L_11, (L_8)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_10)), SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274);
		il2cpp_codegen_write_field_data<true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),3), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5), L_11, SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274);
		int32_t L_12 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), ((int32_t)il2cpp_codegen_add(L_12, 1)));
		return (bool)1;
	}

IL_004a:
	{
		bool L_13;
		L_13 = Enumerator_MoveNextRare_mA2A9C4EFAA8131F5D97D9B8626FDEDF343E93488(__this, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6));
		return L_13;
	}
}
IL2CPP_EXTERN_C  bool Enumerator_MoveNext_m2F9CDD8406C6495182E05B55A00C3D0C9620CA67_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0>(__this);
	bool _returnValue;
	_returnValue = Enumerator_MoveNext_m2F9CDD8406C6495182E05B55A00C3D0C9620CA67(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNextRare_mA2A9C4EFAA8131F5D97D9B8626FDEDF343E93488_fshared (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9* L_1 = *(LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_2 = ((LowLevelList_1_tFA10A51C575F28B96FF2E2E7FB3740E48546A1E0*)L_1)->____version;
		if ((((int32_t)L_0) == ((int32_t)L_2)))
		{
			goto IL_0019;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_3 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_m1BE9BD198B904AA1D94F4B10DA88077DFD44B7A5(L_3, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, method);
	}

IL_0019:
	{
		LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9* L_4 = *(LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_5 = ((LowLevelList_1_tFA10A51C575F28B96FF2E2E7FB3740E48546A1E0*)L_4)->____size;
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), ((int32_t)il2cpp_codegen_add(L_5, 1)));
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274);
		return (bool)0;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_m05879F7740A935DB74AE5672E2B537349369A3C1_fshared (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_0 = alloca(SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274);
	{
		il2cpp_codegen_memcpy(L_0, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)), SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274);
		il2cpp_codegen_memcpy(il2cppRetVal, L_0, SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_get_Current_m05879F7740A935DB74AE5672E2B537349369A3C1_AdjustorThunk (RuntimeObject* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0>(__this);
	Enumerator_get_Current_m05879F7740A935DB74AE5672E2B537349369A3C1_inline(_thisAdjusted, il2cppRetVal, method);
	return;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mA8280DCF9D59E671AB204C71415939B6D7B4211E_fshared (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_5 = alloca(SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		if (!L_0)
		{
			goto IL_001d;
		}
	}
	{
		int32_t L_1 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9* L_2 = *(LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_3 = ((LowLevelList_1_tFA10A51C575F28B96FF2E2E7FB3740E48546A1E0*)L_2)->____size;
		if ((!(((uint32_t)L_1) == ((uint32_t)((int32_t)il2cpp_codegen_add(L_3, 1))))))
		{
			goto IL_0023;
		}
	}

IL_001d:
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_4 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_m1BE9BD198B904AA1D94F4B10DA88077DFD44B7A5(L_4, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_4, method);
	}

IL_0023:
	{
		il2cpp_codegen_memcpy(L_5, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)), SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274);
		RuntimeObject* L_6 = Box(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4), L_5);
		return L_6;
	}
}
IL2CPP_EXTERN_C  RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mA8280DCF9D59E671AB204C71415939B6D7B4211E_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0>(__this);
	RuntimeObject* _returnValue;
	_returnValue = Enumerator_System_Collections_IEnumerator_get_Current_mA8280DCF9D59E671AB204C71415939B6D7B4211E(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_System_Collections_IEnumerator_Reset_m15625F750730D127B128E2654A2B59982DEAB9C9_fshared (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9* L_1 = *(LowLevelListWithIList_1_t80E30B23146FD62D1FB6A639EA1EBAD199B029D9**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_2 = ((LowLevelList_1_tFA10A51C575F28B96FF2E2E7FB3740E48546A1E0*)L_1)->____version;
		if ((((int32_t)L_0) == ((int32_t)L_2)))
		{
			goto IL_0019;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_3 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_m1BE9BD198B904AA1D94F4B10DA88077DFD44B7A5(L_3, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, method);
	}

IL_0019:
	{
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), 0);
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_System_Collections_IEnumerator_Reset_m15625F750730D127B128E2654A2B59982DEAB9C9_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0>(__this);
	Enumerator_System_Collections_IEnumerator_Reset_m15625F750730D127B128E2654A2B59982DEAB9C9(_thisAdjusted, method);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_get_Current_mA7D722D310859E84FC0C9A2F8DBCAF8BA78F1D54_fshared (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	{
		RuntimeObject* L_0 = *(RuntimeObject**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		return L_0;
	}
}
IL2CPP_EXTERN_C  RuntimeObject* Enumerator_get_Current_mA7D722D310859E84FC0C9A2F8DBCAF8BA78F1D54_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE>(__this);
	RuntimeObject* _returnValue;
	_returnValue = Enumerator_get_Current_mA7D722D310859E84FC0C9A2F8DBCAF8BA78F1D54_inline(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_set_Current_m1BB3A6D5580E7C6A864831EED00A4E1B8E81B1B5_fshared (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	{
		RuntimeObject* L_0 = ___0_value;
		il2cpp_codegen_write_field_data<RuntimeObject*, true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),4), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), L_0);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mBB836D4E68A5F554A9A7C377FFBD67DA4253EA7D_fshared (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	{
		RuntimeObject* L_0;
		L_0 = Enumerator_get_Current_mA7D722D310859E84FC0C9A2F8DBCAF8BA78F1D54_inline(__this, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		return (RuntimeObject*)L_0;
	}
}
IL2CPP_EXTERN_C  RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mBB836D4E68A5F554A9A7C377FFBD67DA4253EA7D_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE>(__this);
	RuntimeObject* _returnValue;
	_returnValue = Enumerator_System_Collections_IEnumerator_get_Current_mBB836D4E68A5F554A9A7C377FFBD67DA4253EA7D(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_mEA52ADA4F9D63275758AC0D426BA0CBDDAD6474E_fshared (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, RuntimeObject* ___0_enumerator, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_IndexedCollectionPropertyBagEnumerator_1_tFD1F1453C2F94899EA80ADFEB02518E8B60D9706 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6));
	{
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),0), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), (int32_t)1);
		RuntimeObject* L_0 = ___0_enumerator;
		il2cpp_codegen_write_field_data<RuntimeObject*, true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4), L_0);
		il2cpp_codegen_initobj((((Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), sizeof(Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63));
		il2cpp_codegen_initobj((((IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7)))), SizeOf_IndexedCollectionPropertyBagEnumerator_1_tFD1F1453C2F94899EA80ADFEB02518E8B60D9706);
		Enumerator_set_Current_m1BB3A6D5580E7C6A864831EED00A4E1B8E81B1B5_inline(__this, (RuntimeObject*)NULL, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8));
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m9788E04B31AAEC644383BF9A7D0FD66F4D89F6F2_fshared (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63 ___0_properties, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_IndexedCollectionPropertyBagEnumerator_1_tFD1F1453C2F94899EA80ADFEB02518E8B60D9706 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6));
	{
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),0), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), (int32_t)2);
		il2cpp_codegen_write_field_data<RuntimeObject*, true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4), (RuntimeObject*)NULL);
		Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63 L_0 = ___0_properties;
		il2cpp_codegen_write_field_data<Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63, true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5), L_0);
		il2cpp_codegen_initobj((((IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7)))), SizeOf_IndexedCollectionPropertyBagEnumerator_1_tFD1F1453C2F94899EA80ADFEB02518E8B60D9706);
		Enumerator_set_Current_m1BB3A6D5580E7C6A864831EED00A4E1B8E81B1B5_inline(__this, (RuntimeObject*)NULL, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8));
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_mB5DAC8151C7ABCC2E039D679CC59C09170005D6A_fshared (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479 ___0_enumerator, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_IndexedCollectionPropertyBagEnumerator_1_tFD1F1453C2F94899EA80ADFEB02518E8B60D9706 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6));
	const IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479 L_0 = alloca(SizeOf_IndexedCollectionPropertyBagEnumerator_1_tFD1F1453C2F94899EA80ADFEB02518E8B60D9706);
	{
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),0), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), (int32_t)3);
		il2cpp_codegen_write_field_data<RuntimeObject*, true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4), (RuntimeObject*)NULL);
		il2cpp_codegen_initobj((((Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), sizeof(Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63));
		il2cpp_codegen_memcpy(L_0, ___0_enumerator, SizeOf_IndexedCollectionPropertyBagEnumerator_1_tFD1F1453C2F94899EA80ADFEB02518E8B60D9706);
		il2cpp_codegen_write_field_data<true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),3), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7), L_0, SizeOf_IndexedCollectionPropertyBagEnumerator_1_tFD1F1453C2F94899EA80ADFEB02518E8B60D9706);
		Enumerator_set_Current_m1BB3A6D5580E7C6A864831EED00A4E1B8E81B1B5_inline(__this, (RuntimeObject*)NULL, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8));
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mB0957ADFA01BC52170FB018BD2AD59C2E163A22F_fshared (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	bool V_0 = false;
	int32_t V_1 = 0;
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		V_1 = L_0;
		int32_t L_1 = V_1;
		switch (L_1)
		{
			case 0:
			{
				goto IL_001f;
			}
			case 1:
			{
				goto IL_0021;
			}
			case 2:
			{
				goto IL_0040;
			}
			case 3:
			{
				goto IL_005f;
			}
		}
	}
	{
		goto IL_007e;
	}

IL_001f:
	{
		return (bool)0;
	}

IL_0021:
	{
		RuntimeObject* L_2 = *(RuntimeObject**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
		bool L_3;
		L_3 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_2);
		V_0 = L_3;
		RuntimeObject* L_4 = *(RuntimeObject**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
		RuntimeObject* L_5;
		L_5 = InterfaceFuncInvoker0Invoker< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 9), L_4);
		Enumerator_set_Current_m1BB3A6D5580E7C6A864831EED00A4E1B8E81B1B5_inline(__this, L_5, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8));
		goto IL_0084;
	}

IL_0040:
	{
		bool L_6;
		L_6 = Enumerator_MoveNext_m4E164BF10DEA42BD1FB9648DD5795D120F5B99F2((((Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 11));
		V_0 = L_6;
		RuntimeObject* L_7;
		L_7 = Enumerator_get_Current_m1141D239836770EF8C75957763553C76773A96A4_inline((((Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 12));
		Enumerator_set_Current_m1BB3A6D5580E7C6A864831EED00A4E1B8E81B1B5_inline(__this, L_7, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8));
		goto IL_0084;
	}

IL_005f:
	{
		bool L_8;
		L_8 = IndexedCollectionPropertyBagEnumerator_1_MoveNext_m6695B2065C6EB0E71B90C2EFF9429E4A76F4C0BA((((IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7)))), il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 13));
		V_0 = L_8;
		RuntimeObject* L_9;
		L_9 = IndexedCollectionPropertyBagEnumerator_1_get_Current_m4E570DCDE463BEA50BA7B3DC03595ED13939EDBE((((IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7)))), il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 14));
		Enumerator_set_Current_m1BB3A6D5580E7C6A864831EED00A4E1B8E81B1B5_inline(__this, L_9, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8));
		goto IL_0084;
	}

IL_007e:
	{
		ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F* L_10 = (ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F_il2cpp_TypeInfo_var)));
		ArgumentOutOfRangeException__ctor_mB596C51BFA864B65C2CED275458FAE90F7CD29C9(L_10, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_10, method);
	}

IL_0084:
	{
		bool L_11 = V_0;
		return L_11;
	}
}
IL2CPP_EXTERN_C  bool Enumerator_MoveNext_mB0957ADFA01BC52170FB018BD2AD59C2E163A22F_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE>(__this);
	bool _returnValue;
	_returnValue = Enumerator_MoveNext_mB0957ADFA01BC52170FB018BD2AD59C2E163A22F(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_mDD80A4C2F5EFC81C22B7F74CACFF9D0921BA0A3C_fshared (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	int32_t V_0 = 0;
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		V_0 = L_0;
		int32_t L_1 = V_0;
		switch (L_1)
		{
			case 0:
			{
				goto IL_004e;
			}
			case 1:
			{
				goto IL_001f;
			}
			case 2:
			{
				goto IL_002b;
			}
			case 3:
			{
				goto IL_003c;
			}
		}
	}
	{
		goto IL_0048;
	}

IL_001f:
	{
		RuntimeObject* L_2 = *(RuntimeObject**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
		InterfaceActionInvoker0::Invoke(2, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_2);
		return;
	}

IL_002b:
	{
		Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63 L_3 = *(Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5));
		Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63 L_4 = L_3;
		RuntimeObject* L_5 = Box(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 15), &L_4);
		const MethodInfo* L_6 = GetInterfaceMethodInfo(il2cpp_rgctx_data_init(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 15), 2, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		Enumerator_System_Collections_IEnumerator_Reset_mE24EC919F3F9A0356303B687F207D479BB66B3AB((Enumerator_t0DD71F3BC0FD44A9CF49504A30E7449BA17D4E63*)UnBox(L_5), L_6);
		return;
	}

IL_003c:
	{
		IndexedCollectionPropertyBagEnumerator_1_Reset_mEF20F04CD33523EE6B378C8F992E49A2214E1626((((IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7)))), il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 16));
		return;
	}

IL_0048:
	{
		ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F* L_7 = (ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F_il2cpp_TypeInfo_var)));
		ArgumentOutOfRangeException__ctor_mB596C51BFA864B65C2CED275458FAE90F7CD29C9(L_7, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_7, method);
	}

IL_004e:
	{
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Reset_mDD80A4C2F5EFC81C22B7F74CACFF9D0921BA0A3C_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE>(__this);
	Enumerator_Reset_mDD80A4C2F5EFC81C22B7F74CACFF9D0921BA0A3C(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m1938093C428E4EDE3B40A07184993FA35F74AB42_fshared (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	int32_t V_0 = 0;
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		V_0 = L_0;
		int32_t L_1 = V_0;
		switch (L_1)
		{
			case 0:
			{
				goto IL_003d;
			}
			case 1:
			{
				goto IL_001f;
			}
			case 2:
			{
				goto IL_003d;
			}
			case 3:
			{
				goto IL_002b;
			}
		}
	}
	{
		goto IL_0037;
	}

IL_001f:
	{
		RuntimeObject* L_2 = *(RuntimeObject**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
		InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_2);
		return;
	}

IL_002b:
	{
		IndexedCollectionPropertyBagEnumerator_1_Dispose_mEBC1E3FF6876007C72F6F76ED237165B599B2505((((IndexedCollectionPropertyBagEnumerator_1_t4F5D8DBBF5DBE0AA3D7844FD8C5BB756D8B8D479*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7)))), NULL);
		return;
	}

IL_0037:
	{
		ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F* L_3 = (ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F_il2cpp_TypeInfo_var)));
		ArgumentOutOfRangeException__ctor_mB596C51BFA864B65C2CED275458FAE90F7CD29C9(L_3, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, method);
	}

IL_003d:
	{
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Dispose_m1938093C428E4EDE3B40A07184993FA35F74AB42_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE>(__this);
	Enumerator_Dispose_m1938093C428E4EDE3B40A07184993FA35F74AB42(_thisAdjusted, method);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m8DA499B2030A9C843BE8FCAEFDFD4CD4EB5BA0BB_fshared (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, Queue_1_t00794C5F46E3254E713B03F949E6AB3A1AB1FD41* ___0_q, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t0E5DDAEBBF431B9439368009CC139FC0992B409B = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	{
		Queue_1_t00794C5F46E3254E713B03F949E6AB3A1AB1FD41* L_0 = ___0_q;
		il2cpp_codegen_write_field_data<Queue_1_t00794C5F46E3254E713B03F949E6AB3A1AB1FD41*, true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),0), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), L_0);
		Queue_1_t00794C5F46E3254E713B03F949E6AB3A1AB1FD41* L_1 = ___0_q;
		int32_t L_2 = L_1->____version;
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), L_2);
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), (-1));
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_t0E5DDAEBBF431B9439368009CC139FC0992B409B);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mA25DFE205338DF12128791E990699B49B335B759_fshared (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t0E5DDAEBBF431B9439368009CC139FC0992B409B = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	{
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), ((int32_t)-2));
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_t0E5DDAEBBF431B9439368009CC139FC0992B409B);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Dispose_mA25DFE205338DF12128791E990699B49B335B759_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C>(__this);
	Enumerator_Dispose_mA25DFE205338DF12128791E990699B49B335B759(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mA6D24403558EA8BFEE5028D75AC049EC8677F637_fshared (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t0E5DDAEBBF431B9439368009CC139FC0992B409B = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_23 = alloca(SizeOf_T_t0E5DDAEBBF431B9439368009CC139FC0992B409B);
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* V_0 = NULL;
	int32_t V_1 = 0;
	int32_t V_2 = 0;
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		Queue_1_t00794C5F46E3254E713B03F949E6AB3A1AB1FD41* L_1 = *(Queue_1_t00794C5F46E3254E713B03F949E6AB3A1AB1FD41**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_2 = L_1->____version;
		if ((((int32_t)L_0) == ((int32_t)L_2)))
		{
			goto IL_001e;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_3 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_3, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralF8D08FCF1537043BF0289FA98C51BF5A3AC7C618)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, method);
	}

IL_001e:
	{
		int32_t L_4 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		if ((!(((uint32_t)L_4) == ((uint32_t)((int32_t)-2)))))
		{
			goto IL_002a;
		}
	}
	{
		return (bool)0;
	}

IL_002a:
	{
		int32_t L_5 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), ((int32_t)il2cpp_codegen_add(L_5, 1)));
		int32_t L_6 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		Queue_1_t00794C5F46E3254E713B03F949E6AB3A1AB1FD41* L_7 = *(Queue_1_t00794C5F46E3254E713B03F949E6AB3A1AB1FD41**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_8 = L_7->____size;
		if ((!(((uint32_t)L_6) == ((uint32_t)L_8))))
		{
			goto IL_0061;
		}
	}
	{
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), ((int32_t)-2));
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_t0E5DDAEBBF431B9439368009CC139FC0992B409B);
		return (bool)0;
	}

IL_0061:
	{
		Queue_1_t00794C5F46E3254E713B03F949E6AB3A1AB1FD41* L_9 = *(Queue_1_t00794C5F46E3254E713B03F949E6AB3A1AB1FD41**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_10 = L_9->____array;
		V_0 = L_10;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_11 = V_0;
		int32_t L_12 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_11)->max_length),NULL));
		V_1 = L_12;
		Queue_1_t00794C5F46E3254E713B03F949E6AB3A1AB1FD41* L_13 = *(Queue_1_t00794C5F46E3254E713B03F949E6AB3A1AB1FD41**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_14 = L_13->____head;
		int32_t L_15 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		V_2 = ((int32_t)il2cpp_codegen_add(L_14, L_15));
		int32_t L_16 = V_2;
		int32_t L_17 = V_1;
		if ((((int32_t)L_16) < ((int32_t)L_17)))
		{
			goto IL_008c;
		}
	}
	{
		int32_t L_18 = V_2;
		int32_t L_19 = V_1;
		V_2 = ((int32_t)il2cpp_codegen_subtract(L_18, L_19));
	}

IL_008c:
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_20 = V_0;
		int32_t L_21 = V_2;
		int32_t L_22 = L_21;
		il2cpp_codegen_memcpy(L_23, (L_20)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_22)), SizeOf_T_t0E5DDAEBBF431B9439368009CC139FC0992B409B);
		il2cpp_codegen_write_field_data<true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),3), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5), L_23, SizeOf_T_t0E5DDAEBBF431B9439368009CC139FC0992B409B);
		return (bool)1;
	}
}
IL2CPP_EXTERN_C  bool Enumerator_MoveNext_mA6D24403558EA8BFEE5028D75AC049EC8677F637_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C>(__this);
	bool _returnValue;
	_returnValue = Enumerator_MoveNext_mA6D24403558EA8BFEE5028D75AC049EC8677F637(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_m2DDD9E2B36A595D40708EBE84125C065CCF6F11D_fshared (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t0E5DDAEBBF431B9439368009CC139FC0992B409B = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_T_t0E5DDAEBBF431B9439368009CC139FC0992B409B);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_000f;
		}
	}
	{
		Enumerator_ThrowEnumerationNotStartedOrEnded_mA8F87F65FFD80CCBB3A060C159463BB5B5603E93(__this, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6));
	}

IL_000f:
	{
		il2cpp_codegen_memcpy(L_1, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)), SizeOf_T_t0E5DDAEBBF431B9439368009CC139FC0992B409B);
		il2cpp_codegen_memcpy(il2cppRetVal, L_1, SizeOf_T_t0E5DDAEBBF431B9439368009CC139FC0992B409B);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_get_Current_m2DDD9E2B36A595D40708EBE84125C065CCF6F11D_AdjustorThunk (RuntimeObject* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C>(__this);
	Enumerator_get_Current_m2DDD9E2B36A595D40708EBE84125C065CCF6F11D(_thisAdjusted, il2cppRetVal, method);
	return;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_ThrowEnumerationNotStartedOrEnded_mA8F87F65FFD80CCBB3A060C159463BB5B5603E93_fshared (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	String_t* G_B3_0 = NULL;
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		if ((((int32_t)L_0) == ((int32_t)(-1))))
		{
			goto IL_0010;
		}
	}
	{
		G_B3_0 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral834F4B6837B71847C4048C946DF8754B323D6BF9));
		goto IL_0015;
	}

IL_0010:
	{
		G_B3_0 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral2390D6884F59E2E4EA04837AD7D6268548597633));
	}

IL_0015:
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_1 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_1, G_B3_0, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, method);
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m01A32A0663137A48FD334AD3F7BF1A172FEBA204_fshared (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t0E5DDAEBBF431B9439368009CC139FC0992B409B = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_0 = alloca(SizeOf_T_t0E5DDAEBBF431B9439368009CC139FC0992B409B);
	{
		Enumerator_get_Current_m2DDD9E2B36A595D40708EBE84125C065CCF6F11D(__this, (Il2CppFullySharedGenericAny*)L_0, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		RuntimeObject* L_1 = Box(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4), L_0);
		return L_1;
	}
}
IL2CPP_EXTERN_C  RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m01A32A0663137A48FD334AD3F7BF1A172FEBA204_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C>(__this);
	RuntimeObject* _returnValue;
	_returnValue = Enumerator_System_Collections_IEnumerator_get_Current_m01A32A0663137A48FD334AD3F7BF1A172FEBA204(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_System_Collections_IEnumerator_Reset_m5E20159EC017B0F62631E89E5784B2D532E2D314_fshared (Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t0E5DDAEBBF431B9439368009CC139FC0992B409B = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		Queue_1_t00794C5F46E3254E713B03F949E6AB3A1AB1FD41* L_1 = *(Queue_1_t00794C5F46E3254E713B03F949E6AB3A1AB1FD41**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_2 = L_1->____version;
		if ((((int32_t)L_0) == ((int32_t)L_2)))
		{
			goto IL_001e;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_3 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_3, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralF8D08FCF1537043BF0289FA98C51BF5A3AC7C618)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, method);
	}

IL_001e:
	{
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), (-1));
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_t0E5DDAEBBF431B9439368009CC139FC0992B409B);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_System_Collections_IEnumerator_Reset_m5E20159EC017B0F62631E89E5784B2D532E2D314_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_tFA6E7D5BA5EFB0C82F2AA1342FFD5C2B753D673C>(__this);
	Enumerator_System_Collections_IEnumerator_Reset_m5E20159EC017B0F62631E89E5784B2D532E2D314(_thisAdjusted, method);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_mF961799B5F1CC1D9AEAC1E22C8F93C2EAC4E885B_fshared (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* __this, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method) 
{
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0 = ___0_array;
		__this->___m_Array = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_Array), (void*)L_0);
		int32_t L_1 = ___1_index;
		__this->___m_IndexStart = ((int32_t)il2cpp_codegen_subtract(L_1, 1));
		int32_t L_2 = ___1_index;
		int32_t L_3 = ___2_length;
		__this->___m_IndexEnd = ((int32_t)il2cpp_codegen_add(L_2, L_3));
		int32_t L_4 = __this->___m_IndexStart;
		__this->___m_Index = L_4;
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mBA9853A226A9CB16F8FB74D21519A91A38E063E0_fshared (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Dispose_mBA9853A226A9CB16F8FB74D21519A91A38E063E0_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923>(__this);
	Enumerator_Dispose_mBA9853A226A9CB16F8FB74D21519A91A38E063E0(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mA42BDA7B751B17DB4EC59F6995E3188780091966_fshared (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___m_Index;
		int32_t L_1 = __this->___m_IndexEnd;
		if ((((int32_t)L_0) >= ((int32_t)L_1)))
		{
			goto IL_001c;
		}
	}
	{
		int32_t L_2 = __this->___m_Index;
		__this->___m_Index = ((int32_t)il2cpp_codegen_add(L_2, 1));
	}

IL_001c:
	{
		int32_t L_3 = __this->___m_Index;
		int32_t L_4 = __this->___m_IndexEnd;
		return (bool)((((int32_t)((((int32_t)L_3) == ((int32_t)L_4))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}
}
IL2CPP_EXTERN_C  bool Enumerator_MoveNext_mA42BDA7B751B17DB4EC59F6995E3188780091966_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923>(__this);
	bool _returnValue;
	_returnValue = Enumerator_MoveNext_mA42BDA7B751B17DB4EC59F6995E3188780091966(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Reset_m2845BC4731316BCC3E1AB5C3F5799107716B7810_fshared (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___m_IndexStart;
		__this->___m_Index = L_0;
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Reset_m2845BC4731316BCC3E1AB5C3F5799107716B7810_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923>(__this);
	Enumerator_Reset_m2845BC4731316BCC3E1AB5C3F5799107716B7810(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_mAEF87A53D24C4DB2F6069E75CF1A5D66D5F4D13F_fshared (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_TValue_t7989F1661D719E09D46EAE726E8A35AD8AB36DBA = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0));
	const Il2CppFullySharedGenericAny L_6 = alloca(SizeOf_TValue_t7989F1661D719E09D46EAE726E8A35AD8AB36DBA);
	{
		int32_t L_0 = __this->___m_Index;
		int32_t L_1 = __this->___m_IndexEnd;
		if ((!(((uint32_t)L_0) == ((uint32_t)L_1))))
		{
			goto IL_0019;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_2 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_2, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral38078454203FA992D959F0FD913FDD567D9EA9EB)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_2, method);
	}

IL_0019:
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = __this->___m_Array;
		int32_t L_4 = __this->___m_Index;
		int32_t L_5 = L_4;
		il2cpp_codegen_memcpy(L_6, (L_3)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_5)), SizeOf_TValue_t7989F1661D719E09D46EAE726E8A35AD8AB36DBA);
		il2cpp_codegen_memcpy(il2cppRetVal, L_6, SizeOf_TValue_t7989F1661D719E09D46EAE726E8A35AD8AB36DBA);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_get_Current_mAEF87A53D24C4DB2F6069E75CF1A5D66D5F4D13F_AdjustorThunk (RuntimeObject* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923>(__this);
	Enumerator_get_Current_mAEF87A53D24C4DB2F6069E75CF1A5D66D5F4D13F(_thisAdjusted, il2cppRetVal, method);
	return;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m6D0270EB2876CB03F839581CC803C1DAA52EF9D4_fshared (Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_TValue_t7989F1661D719E09D46EAE726E8A35AD8AB36DBA = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0));
	const Il2CppFullySharedGenericAny L_0 = alloca(SizeOf_TValue_t7989F1661D719E09D46EAE726E8A35AD8AB36DBA);
	{
		Enumerator_get_Current_mAEF87A53D24C4DB2F6069E75CF1A5D66D5F4D13F(__this, (Il2CppFullySharedGenericAny*)L_0, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		RuntimeObject* L_1 = Box(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0), L_0);
		return L_1;
	}
}
IL2CPP_EXTERN_C  RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m6D0270EB2876CB03F839581CC803C1DAA52EF9D4_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t9EBFDC5C792D6D306A9C02107ECFBC44DB94E923>(__this);
	RuntimeObject* _returnValue;
	_returnValue = Enumerator_System_Collections_IEnumerator_get_Current_m6D0270EB2876CB03F839581CC803C1DAA52EF9D4(_thisAdjusted, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_mD65EF492693E70B41695A031A49F72C7EFA82FCE_fshared (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A* ___0_stack, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t43B3E794C887F691C76D009756D4BAD94E1898AF = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	{
		Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A* L_0 = ___0_stack;
		il2cpp_codegen_write_field_data<Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A*, true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),0), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), L_0);
		Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A* L_1 = ___0_stack;
		int32_t L_2 = L_1->____version;
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), L_2);
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), ((int32_t)-2));
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_t43B3E794C887F691C76D009756D4BAD94E1898AF);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m475AD9FE055280B2B1CF43E93F9A2340C3EAE75C_fshared (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	{
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), (-1));
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_Dispose_m475AD9FE055280B2B1CF43E93F9A2340C3EAE75C_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41>(__this);
	Enumerator_Dispose_m475AD9FE055280B2B1CF43E93F9A2340C3EAE75C(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m3347052D874561F1E1857019C4F418A27B125353_fshared (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t43B3E794C887F691C76D009756D4BAD94E1898AF = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_13 = alloca(SizeOf_T_t43B3E794C887F691C76D009756D4BAD94E1898AF);
	const Il2CppFullySharedGenericAny L_23 = L_13;
	int32_t V_0 = 0;
	int32_t G_B5_0 = 0;
	int32_t G_B4_0 = 0;
	int32_t G_B10_0 = 0;
	int32_t G_B9_0 = 0;
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A* L_1 = *(Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_2 = L_1->____version;
		if ((((int32_t)L_0) == ((int32_t)L_2)))
		{
			goto IL_001e;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_3 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_3, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralF8D08FCF1537043BF0289FA98C51BF5A3AC7C618)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, method);
	}

IL_001e:
	{
		int32_t L_4 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		if ((!(((uint32_t)L_4) == ((uint32_t)((int32_t)-2)))))
		{
			goto IL_0067;
		}
	}
	{
		Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A* L_5 = *(Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_6 = L_5->____size;
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), ((int32_t)il2cpp_codegen_subtract(L_6, 1)));
		int32_t L_7 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		int32_t L_8 = ((((int32_t)((((int32_t)L_7) < ((int32_t)0))? 1 : 0)) == ((int32_t)0))? 1 : 0);
		if (!L_8)
		{
			G_B5_0 = L_8;
			goto IL_0066;
		}
		G_B4_0 = L_8;
	}
	{
		Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A* L_9 = *(Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_10 = L_9->____array;
		int32_t L_11 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		int32_t L_12 = L_11;
		il2cpp_codegen_memcpy(L_13, (L_10)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_12)), SizeOf_T_t43B3E794C887F691C76D009756D4BAD94E1898AF);
		il2cpp_codegen_write_field_data<true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),3), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5), L_13, SizeOf_T_t43B3E794C887F691C76D009756D4BAD94E1898AF);
		G_B5_0 = G_B4_0;
	}

IL_0066:
	{
		return (bool)G_B5_0;
	}

IL_0067:
	{
		int32_t L_14 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		if ((!(((uint32_t)L_14) == ((uint32_t)(-1)))))
		{
			goto IL_0072;
		}
	}
	{
		return (bool)0;
	}

IL_0072:
	{
		int32_t L_15 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		V_0 = ((int32_t)il2cpp_codegen_subtract(L_15, 1));
		int32_t L_16 = V_0;
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), L_16);
		int32_t L_17 = V_0;
		int32_t L_18 = ((((int32_t)((((int32_t)L_17) < ((int32_t)0))? 1 : 0)) == ((int32_t)0))? 1 : 0);
		if (!L_18)
		{
			G_B10_0 = L_18;
			goto IL_00a9;
		}
		G_B9_0 = L_18;
	}
	{
		Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A* L_19 = *(Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_20 = L_19->____array;
		int32_t L_21 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		int32_t L_22 = L_21;
		il2cpp_codegen_memcpy(L_23, (L_20)->GetAddressAtUnchecked(static_cast<il2cpp_array_size_t>(L_22)), SizeOf_T_t43B3E794C887F691C76D009756D4BAD94E1898AF);
		il2cpp_codegen_write_field_data<true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),3), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5), L_23, SizeOf_T_t43B3E794C887F691C76D009756D4BAD94E1898AF);
		return (bool)G_B9_0;
	}

IL_00a9:
	{
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_t43B3E794C887F691C76D009756D4BAD94E1898AF);
		return (bool)G_B10_0;
	}
}
IL2CPP_EXTERN_C  bool Enumerator_MoveNext_m3347052D874561F1E1857019C4F418A27B125353_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41>(__this);
	bool _returnValue;
	_returnValue = Enumerator_MoveNext_m3347052D874561F1E1857019C4F418A27B125353(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_get_Current_m748B0758792F434FAB29656C2F5CF6EEB4481D27_fshared (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t43B3E794C887F691C76D009756D4BAD94E1898AF = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_T_t43B3E794C887F691C76D009756D4BAD94E1898AF);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_000f;
		}
	}
	{
		Enumerator_ThrowEnumerationNotStartedOrEnded_mDFA786E51878E61AE16B8C27D512FD3407BA6AFA(__this, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6));
	}

IL_000f:
	{
		il2cpp_codegen_memcpy(L_1, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)), SizeOf_T_t43B3E794C887F691C76D009756D4BAD94E1898AF);
		il2cpp_codegen_memcpy(il2cppRetVal, L_1, SizeOf_T_t43B3E794C887F691C76D009756D4BAD94E1898AF);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_get_Current_m748B0758792F434FAB29656C2F5CF6EEB4481D27_AdjustorThunk (RuntimeObject* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method)
{
	Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41>(__this);
	Enumerator_get_Current_m748B0758792F434FAB29656C2F5CF6EEB4481D27(_thisAdjusted, il2cppRetVal, method);
	return;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_ThrowEnumerationNotStartedOrEnded_mDFA786E51878E61AE16B8C27D512FD3407BA6AFA_fshared (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	String_t* G_B3_0 = NULL;
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		if ((((int32_t)L_0) == ((int32_t)((int32_t)-2))))
		{
			goto IL_0011;
		}
	}
	{
		G_B3_0 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral834F4B6837B71847C4048C946DF8754B323D6BF9));
		goto IL_0016;
	}

IL_0011:
	{
		G_B3_0 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral2390D6884F59E2E4EA04837AD7D6268548597633));
	}

IL_0016:
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_1 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_1, G_B3_0, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, method);
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mC2EE4A74701F665A795443A0814F1CF22BC12560_fshared (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t43B3E794C887F691C76D009756D4BAD94E1898AF = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_0 = alloca(SizeOf_T_t43B3E794C887F691C76D009756D4BAD94E1898AF);
	{
		Enumerator_get_Current_m748B0758792F434FAB29656C2F5CF6EEB4481D27(__this, (Il2CppFullySharedGenericAny*)L_0, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		RuntimeObject* L_1 = Box(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4), L_0);
		return L_1;
	}
}
IL2CPP_EXTERN_C  RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_mC2EE4A74701F665A795443A0814F1CF22BC12560_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41>(__this);
	RuntimeObject* _returnValue;
	_returnValue = Enumerator_System_Collections_IEnumerator_get_Current_mC2EE4A74701F665A795443A0814F1CF22BC12560(_thisAdjusted, method);
	return _returnValue;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_System_Collections_IEnumerator_Reset_m796D8D2C90C2436A06B08E30008143FCF74E8EC9_fshared (Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t43B3E794C887F691C76D009756D4BAD94E1898AF = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A* L_1 = *(Stack_1_tF3E5E7101E929741300A1CF7C159A6ED9B61621A**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		int32_t L_2 = L_1->____version;
		if ((((int32_t)L_0) == ((int32_t)L_2)))
		{
			goto IL_001e;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_3 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_3, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralF8D08FCF1537043BF0289FA98C51BF5A3AC7C618)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, method);
	}

IL_001e:
	{
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), ((int32_t)-2));
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericAny*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))), SizeOf_T_t43B3E794C887F691C76D009756D4BAD94E1898AF);
		return;
	}
}
IL2CPP_EXTERN_C  void Enumerator_System_Collections_IEnumerator_Reset_m796D8D2C90C2436A06B08E30008143FCF74E8EC9_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<Enumerator_t9C40FA80DC7A4C63469E514386FAB9AE1039DF41>(__this);
	Enumerator_System_Collections_IEnumerator_Reset_m796D8D2C90C2436A06B08E30008143FCF74E8EC9(_thisAdjusted, method);
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
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void* NativeArrayUnsafeUtility_GetUnsafeReadOnlyPtr_TisIl2CppFullySharedGenericStruct_m13C618FD69BBAB7D8C77632BF8A9116FCD17D234_fshared_inline (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 ___0_nativeArray, const RuntimeMethod* method) 
{
	{
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_0 = ___0_nativeArray;
		void* L_1 = L_0.___m_Buffer;
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ListQueryMatcher_1_set_matches_m32D702F4DE02ABB90F1560B65718F096EDD98416_gshared_inline (ListQueryMatcher_1_t80B2C90F8E32F964B971ECBBAA0E811171012425* __this, List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* ___0_value, const RuntimeMethod* method) 
{
	{
		List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* L_0 = ___0_value;
		__this->___U3CmatchesU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CmatchesU3Ek__BackingField), (void*)L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared_inline (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____size;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void UnsafeUtility_ReadArrayElement_TisIl2CppFullySharedGenericAny_m295186AA082411C57485F8BDB824E4D8AC1C6D93_fshared_inline (void* ___0_source, int32_t ___1_index, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_T_t3818EBA71DF5EA591716A05E0BBD988D81931B5C = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_2 = alloca(SizeOf_T_t3818EBA71DF5EA591716A05E0BBD988D81931B5C);
	{
		void* L_0 = ___0_source;
		int32_t L_1 = ___1_index;
		UnsafeUtilityInternal_ReadArrayElement_TisIl2CppFullySharedGenericAny_mC729F1975F0494EB9B61C929EAF0F798DCF68563_inline(L_0, L_1, (Il2CppFullySharedGenericAny*)L_2, il2cpp_rgctx_method(method->rgctx_data, 0));
		il2cpp_codegen_memcpy(il2cppRetVal, L_2, SizeOf_T_t3818EBA71DF5EA591716A05E0BBD988D81931B5C);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mC9099E56E01AB33169AB4EE888BD9A2B752961DE_fshared_inline (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
	const Il2CppFullySharedGenericStruct L_5 = alloca(SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), ((int32_t)il2cpp_codegen_add(L_0, 1)));
		int32_t L_1 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		int32_t L_2 = (((NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1))))->___m_Length;
		if ((((int32_t)L_1) >= ((int32_t)L_2)))
		{
			goto IL_003f;
		}
	}
	{
		void* L_3 = (((NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1))))->___m_Buffer;
		int32_t L_4 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		UnsafeUtility_ReadArrayElement_TisIl2CppFullySharedGenericStruct_m45AD40013DEE9AEF8031E0DA6F4D23BFD5AA8F42_inline(L_3, L_4, (Il2CppFullySharedGenericStruct*)L_5, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5));
		il2cpp_codegen_write_field_data<true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4), L_5, SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790);
		return (bool)1;
	}

IL_003f:
	{
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericStruct*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4)))), SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790);
		return (bool)0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Enumerator_get_Current_mF9A198DACCE8ED0DC6FD097825D7A0F82D149DBA_fshared_inline (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
	const Il2CppFullySharedGenericStruct L_0 = alloca(SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790);
	{
		il2cpp_codegen_memcpy(L_0, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4)), SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790);
		il2cpp_codegen_memcpy(il2cppRetVal, L_0, SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_System_Collections_IEnumerator_get_Current_m81767706F1E3B41EAB92F21B1669693FFE92ACCB_fshared_inline (Enumerator_t82F77221EB09A3A14061DC0C6965526D856BC405* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
	const Il2CppFullySharedGenericStruct L_0 = alloca(SizeOf_T_tCB2B4AD90EC47251952F0D7CF84CE2365DFB7790);
	{
		Enumerator_get_Current_mF9A198DACCE8ED0DC6FD097825D7A0F82D149DBA_inline(__this, (Il2CppFullySharedGenericStruct*)L_0, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6));
		RuntimeObject* L_1 = Box(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3), L_0);
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
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void NativeArray_1_get_Item_mA8C8A69EB3A5D460C55DFCD27275CD5BA5E2B455_fshared_inline (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18* __this, int32_t ___0_index, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t05490BB68E93F7FF07D46087FEC0242A3FB9D022 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
	const Il2CppFullySharedGenericStruct L_2 = alloca(SizeOf_T_t05490BB68E93F7FF07D46087FEC0242A3FB9D022);
	{
		void* L_0 = __this->___m_Buffer;
		int32_t L_1 = ___0_index;
		UnsafeUtility_ReadArrayElement_TisIl2CppFullySharedGenericStruct_m45AD40013DEE9AEF8031E0DA6F4D23BFD5AA8F42_inline(L_0, L_1, (Il2CppFullySharedGenericStruct*)L_2, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6));
		il2cpp_codegen_memcpy(il2cppRetVal, L_2, SizeOf_T_t05490BB68E93F7FF07D46087FEC0242A3FB9D022);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t NativeSlice_1_get_Length_m0225CA0944599882AC9C2A06A99FDC685362AFBE_fshared_inline (NativeSlice_1_tA54E5D259EBCC7CD8512AA352C6F3709EB237B52* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___m_Length;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m9589C320A0B345A9190284C38FCB97F2015861DA_fshared_inline (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
	const Il2CppFullySharedGenericStruct L_5 = alloca(SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		il2cpp_codegen_write_field_data<int32_t, false>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),1), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2), ((int32_t)il2cpp_codegen_add(L_0, 1)));
		int32_t L_1 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		int32_t L_2 = (((ReadOnly_tC6998C67EE7BA262710FFDCF6BE6728CB987FDE8*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1))))->___m_Length;
		if ((((int32_t)L_1) >= ((int32_t)L_2)))
		{
			goto IL_003f;
		}
	}
	{
		void* L_3 = (((ReadOnly_tC6998C67EE7BA262710FFDCF6BE6728CB987FDE8*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1))))->___m_Buffer;
		int32_t L_4 = *(int32_t*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		UnsafeUtility_ReadArrayElement_TisIl2CppFullySharedGenericStruct_m45AD40013DEE9AEF8031E0DA6F4D23BFD5AA8F42_inline(L_3, L_4, (Il2CppFullySharedGenericStruct*)L_5, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5));
		il2cpp_codegen_write_field_data<true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),2), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4), L_5, SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F);
		return (bool)1;
	}

IL_003f:
	{
		il2cpp_codegen_initobj((((Il2CppFullySharedGenericStruct*)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4)))), SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F);
		return (bool)0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Enumerator_get_Current_m44CEFC7935FF87861A589ACDB1024033E52A123E_fshared_inline (Enumerator_tF9CAF4D90202E64A41350C2A4CF1CAF69A80A16C* __this, Il2CppFullySharedGenericStruct* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
	const Il2CppFullySharedGenericStruct L_0 = alloca(SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F);
	{
		il2cpp_codegen_memcpy(L_0, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4)), SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F);
		il2cpp_codegen_memcpy(il2cppRetVal, L_0, SizeOf_T_t4E82A4C927C92C52A5DF293884D44E2650C7B23F);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Enumerator_get_Current_m51282E47C90E55BF2D96A0D329869ABC122A85A0_fshared_inline (Enumerator_t0D36BD6D63D775CDEAF8501F5A252535FFFF52CB* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_0 = alloca(SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8);
	{
		il2cpp_codegen_memcpy(L_0, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)), SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8);
		il2cpp_codegen_memcpy(il2cppRetVal, L_0, SizeOf_T_t6D22409F37413B725563BF484486F55FB7202BB8);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Enumerator_get_Current_m5BA2E5A6E9016D327B7C3067D3D2B2016D37DFDA_fshared_inline (Enumerator_t21D50D6AE6FF834C37B6D62B4470E690B9130457* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_0 = alloca(SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E);
	{
		il2cpp_codegen_memcpy(L_0, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)), SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E);
		il2cpp_codegen_memcpy(il2cppRetVal, L_0, SizeOf_T_t40ED5F405E4C66916984D6B18DAD11FB5460B70E);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Enumerator_get_Current_m8B42D4B2DE853B9D11B997120CD0228D4780E394_fshared_inline (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_0 = alloca(SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1);
	{
		il2cpp_codegen_memcpy(L_0, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)), SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1);
		il2cpp_codegen_memcpy(il2cppRetVal, L_0, SizeOf_T_t010616E3077234188F9BB4FAF369F8571BC5F2E1);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Enumerator_get_Current_m05879F7740A935DB74AE5672E2B537349369A3C1_fshared_inline (Enumerator_t291DC5B00D016A9E1E061A4CCA7269A21EAF45C0* __this, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4));
	const Il2CppFullySharedGenericAny L_0 = alloca(SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274);
	{
		il2cpp_codegen_memcpy(L_0, il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)), SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274);
		il2cpp_codegen_memcpy(il2cppRetVal, L_0, SizeOf_T_tED0EB14CB913118FBDFEDD7D0C8F114A02B0A274);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_get_Current_mA7D722D310859E84FC0C9A2F8DBCAF8BA78F1D54_fshared_inline (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	{
		RuntimeObject* L_0 = *(RuntimeObject**)il2cpp_codegen_get_field_data_pointer(__this, il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Enumerator_set_Current_m1BB3A6D5580E7C6A864831EED00A4E1B8E81B1B5_fshared_inline (Enumerator_t4DEB327DC99C2F4A070D514E9186C87B10047DCE* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	{
		RuntimeObject* L_0 = ___0_value;
		il2cpp_codegen_write_field_data<RuntimeObject*, true>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0),4), il2cpp_rgctx_offset(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void UnsafeUtilityInternal_ReadArrayElement_TisIl2CppFullySharedGenericAny_mC729F1975F0494EB9B61C929EAF0F798DCF68563_fshared_inline (void* ___0_source, int32_t ___1_index, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_T_t0AA6189E765AD6394317DDCA688595C885DC47B5 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 0));
	const Il2CppFullySharedGenericAny L_6 = alloca(SizeOf_T_t0AA6189E765AD6394317DDCA688595C885DC47B5);
	{
		void* L_0 = ___0_source;
		int32_t L_1 = ___1_index;
		int64_t L_2 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_1,NULL));
		uint32_t L_3 = SizeOf_T_t0AA6189E765AD6394317DDCA688595C885DC47B5;
		int64_t L_4 = (il2cpp_codegen_conv<int64_t,uint32_t,int32_t,false,false>(L_3,NULL));
		intptr_t L_5 = (il2cpp_codegen_conv<intptr_t,int64_t,int64_t,false,false>(((int64_t)il2cpp_codegen_multiply(L_2, L_4)),NULL));
		il2cpp_codegen_memcpy(L_6, ((void*)il2cpp_codegen_add((intptr_t)L_0, L_5)), SizeOf_T_t0AA6189E765AD6394317DDCA688595C885DC47B5);
		il2cpp_codegen_memcpy(il2cppRetVal, L_6, SizeOf_T_t0AA6189E765AD6394317DDCA688595C885DC47B5);
		return;
	}
}
