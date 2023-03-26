#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif





// System.IntPtr[]
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
// System.Diagnostics.StackTrace[]
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
// System.Collections.IDictionary
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
// System.Runtime.Serialization.SafeSerializationManager
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
// System.String
struct String_t;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;

struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;


IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// <Module>
struct U3CModuleU3E_tDA285F13E9413BF3B79A99D6E310BE9AF3444EEB 
{
};

// System.Attribute
struct Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA  : public RuntimeObject
{
};

// System.Configuration.ConfigurationElement
struct ConfigurationElement_tAE3EE71C256825472831FFBB7F491275DFAF089E  : public RuntimeObject
{
};

// System.Configuration.ConfigurationPropertyCollection
struct ConfigurationPropertyCollection_t1DEB95D3283BB11A46B862E9D13710ED698B6C93  : public RuntimeObject
{
};

// System.Configuration.ConfigurationSectionGroup
struct ConfigurationSectionGroup_tE7948C2D31B193F4BA8828947ED3094B952C7863  : public RuntimeObject
{
};

// System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};

// System.Xml.XmlNode
struct XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF  : public RuntimeObject
{
};

// System.Xml.XmlReader
struct XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD  : public RuntimeObject
{
};

// System.__Il2CppComDelegate
struct __Il2CppComDelegate_tD0DD2BBA6AC8F151D32B6DFD02F6BDA339F8DC4D  : public Il2CppComObject
{
};

// System.Configuration.ConfigurationCollectionAttribute
struct ConfigurationCollectionAttribute_t1D7DBAAB4908B6B8F26EA1C66106A67BDE949558  : public Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA
{
};

// System.Configuration.ConfigurationElementCollection
struct ConfigurationElementCollection_t56E8398661A85A59616301BADF13026FB1492606  : public ConfigurationElement_tAE3EE71C256825472831FFBB7F491275DFAF089E
{
};

// System.Configuration.ConfigurationSection
struct ConfigurationSection_t0BC609F0151B160A4FAB8226679B62AF22539C3E  : public ConfigurationElement_tAE3EE71C256825472831FFBB7F491275DFAF089E
{
};

// System.IntPtr
struct IntPtr_t 
{
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;
};

// System.Exception
struct Exception_t  : public RuntimeObject
{
	// System.String System.Exception::_className
	String_t* ____className_1;
	// System.String System.Exception::_message
	String_t* ____message_2;
	// System.Collections.IDictionary System.Exception::_data
	RuntimeObject* ____data_3;
	// System.Exception System.Exception::_innerException
	Exception_t* ____innerException_4;
	// System.String System.Exception::_helpURL
	String_t* ____helpURL_5;
	// System.Object System.Exception::_stackTrace
	RuntimeObject* ____stackTrace_6;
	// System.String System.Exception::_stackTraceString
	String_t* ____stackTraceString_7;
	// System.String System.Exception::_remoteStackTraceString
	String_t* ____remoteStackTraceString_8;
	// System.Int32 System.Exception::_remoteStackIndex
	int32_t ____remoteStackIndex_9;
	// System.Object System.Exception::_dynamicMethods
	RuntimeObject* ____dynamicMethods_10;
	// System.Int32 System.Exception::_HResult
	int32_t ____HResult_11;
	// System.String System.Exception::_source
	String_t* ____source_12;
	// System.Runtime.Serialization.SafeSerializationManager System.Exception::_safeSerializationManager
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	// System.Diagnostics.StackTrace[] System.Exception::captured_traces
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	// System.IntPtr[] System.Exception::native_trace_ips
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips_15;
	// System.Int32 System.Exception::caught_in_unmanaged
	int32_t ___caught_in_unmanaged_16;
};
// Native definition for P/Invoke marshalling of System.Exception
struct Exception_t_marshaled_pinvoke
{
	char* ____className_1;
	char* ____message_2;
	RuntimeObject* ____data_3;
	Exception_t_marshaled_pinvoke* ____innerException_4;
	char* ____helpURL_5;
	Il2CppIUnknown* ____stackTrace_6;
	char* ____stackTraceString_7;
	char* ____remoteStackTraceString_8;
	int32_t ____remoteStackIndex_9;
	Il2CppIUnknown* ____dynamicMethods_10;
	int32_t ____HResult_11;
	char* ____source_12;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	Il2CppSafeArray/*NONE*/* ___native_trace_ips_15;
	int32_t ___caught_in_unmanaged_16;
};
// Native definition for COM marshalling of System.Exception
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className_1;
	Il2CppChar* ____message_2;
	RuntimeObject* ____data_3;
	Exception_t_marshaled_com* ____innerException_4;
	Il2CppChar* ____helpURL_5;
	Il2CppIUnknown* ____stackTrace_6;
	Il2CppChar* ____stackTraceString_7;
	Il2CppChar* ____remoteStackTraceString_8;
	int32_t ____remoteStackIndex_9;
	Il2CppIUnknown* ____dynamicMethods_10;
	int32_t ____HResult_11;
	Il2CppChar* ____source_12;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	Il2CppSafeArray/*NONE*/* ___native_trace_ips_15;
	int32_t ___caught_in_unmanaged_16;
};

// System.Configuration.IgnoreSection
struct IgnoreSection_t43A7C33C0083D18639AA3CC3D75DD93FCF1C5D97  : public ConfigurationSection_t0BC609F0151B160A4FAB8226679B62AF22539C3E
{
};

// System.SystemException
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};

// System.InvalidOperationException
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};

// System.ObjectDisposedException
struct ObjectDisposedException_tC5FB29E8E980E2010A2F6A5B9B791089419F89EB  : public InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB
{
	// System.String System.ObjectDisposedException::_objectName
	String_t* ____objectName_18;
};

// Unity.ThrowStub
struct ThrowStub_t9161280E38728A40D9B1A975AEE62E89C379E400  : public ObjectDisposedException_tC5FB29E8E980E2010A2F6A5B9B791089419F89EB
{
};

// <Module>

// <Module>

// System.Configuration.ConfigurationElement

// System.Configuration.ConfigurationElement

// System.Configuration.ConfigurationPropertyCollection

// System.Configuration.ConfigurationPropertyCollection

// System.Configuration.ConfigurationSectionGroup

// System.Configuration.ConfigurationSectionGroup

// System.Xml.XmlNode

// System.Xml.XmlNode

// System.Xml.XmlReader
struct XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD_StaticFields
{
	// System.UInt32 System.Xml.XmlReader::IsTextualNodeBitmap
	uint32_t ___IsTextualNodeBitmap_0;
	// System.UInt32 System.Xml.XmlReader::CanReadContentAsBitmap
	uint32_t ___CanReadContentAsBitmap_1;
	// System.UInt32 System.Xml.XmlReader::HasValueBitmap
	uint32_t ___HasValueBitmap_2;
};

// System.Xml.XmlReader

// System.__Il2CppComDelegate

// System.__Il2CppComDelegate

// System.Configuration.ConfigurationCollectionAttribute

// System.Configuration.ConfigurationCollectionAttribute

// System.Configuration.ConfigurationElementCollection

// System.Configuration.ConfigurationElementCollection

// System.Configuration.ConfigurationSection

// System.Configuration.ConfigurationSection

// System.Configuration.IgnoreSection

// System.Configuration.IgnoreSection

// Unity.ThrowStub

// Unity.ThrowStub
#ifdef __clang__
#pragma clang diagnostic pop
#endif



#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5400;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5400 = { sizeof(XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD), -1, sizeof(XmlReader_t4C709DEF5F01606ECB60B638F1BD6F6E0A9116FD_StaticFields), 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5401;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5401 = { sizeof(XmlNode_t3180B9B3D5C36CD58F5327D9F13458E3B3F030AF), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5402;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5402 = { sizeof(U3CModuleU3E_tDA285F13E9413BF3B79A99D6E310BE9AF3444EEB), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5403;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5403 = { sizeof(ConfigurationElement_tAE3EE71C256825472831FFBB7F491275DFAF089E), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5404;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5404 = { sizeof(ConfigurationSection_t0BC609F0151B160A4FAB8226679B62AF22539C3E), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5405;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5405 = { sizeof(int32_t)+ sizeof(RuntimeObject), sizeof(int32_t), 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5406;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5406 = { sizeof(ConfigurationPropertyCollection_t1DEB95D3283BB11A46B862E9D13710ED698B6C93), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5407;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5407 = { sizeof(ConfigurationElementCollection_t56E8398661A85A59616301BADF13026FB1492606), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5408;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5408 = { sizeof(ConfigurationCollectionAttribute_t1D7DBAAB4908B6B8F26EA1C66106A67BDE949558), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5409;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5409 = { sizeof(ConfigurationSectionGroup_tE7948C2D31B193F4BA8828947ED3094B952C7863), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5410;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5410 = { sizeof(IgnoreSection_t43A7C33C0083D18639AA3CC3D75DD93FCF1C5D97), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5411;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5411 = { sizeof(ThrowStub_t9161280E38728A40D9B1A975AEE62E89C379E400), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5412;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5412 = { 0, sizeof(Il2CppIActivationFactory*), 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5413;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5413 = { sizeof(Il2CppComObject), -1, 0, 0 };
extern const Il2CppTypeDefinitionSizes g_typeDefinitionSize5414;
const Il2CppTypeDefinitionSizes g_typeDefinitionSize5414 = { sizeof(__Il2CppComDelegate_tD0DD2BBA6AC8F151D32B6DFD02F6BDA339F8DC4D), -1, 0, 0 };
#ifdef __clang__
#pragma clang diagnostic pop
#endif
