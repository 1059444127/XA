// XRProcessStatusProxyUtility.h
#ifndef SESSION_UTILITY
#define SESSION_UTILITY

#ifdef UIHDLLDEFINE
#define UIHDLLEXPORT __declspec(dllexport)
#else
#define UIHDLLEXPORT __declspec(dllimport)
#endif 

#include "McsfDataHeader/mcsf_data_header_element_map_interface.h"
#include "McsfNetBase/mcsf_communication_proxy.h"
#include <algorithm>
#include "SessionRequestCommand.pb.h"
#pragma once
using namespace std;

	
#pragma comment(lib, "McsfNetBase.lib")
#pragma comment(lib, "McsfSystemEnvironmentConfig.lib")
#pragma comment(lib, "McsfLogger.lib")

MCSF_DATAHEADER_BEGIN_NAMESPACE 

class UIHDLLEXPORT CSessionUtility
{
public:
	 static string SerializeDataHead(IDataHeaderElementMap * m_pDataHeaderElemMap);
	 static shared_ptr<IDataHeaderElementMap> DeserializeDataHead(string serializedDataHead);
	 static CommandContext* CreateCommandContext(string receiver, int commandID,string serializeObject);
	 static string RequestCommandProtoBuffer(int commandID,string parArray[]);//参数用的对吗
};

MCSF_DATAHEADER_END_NAMESPACE
#endif 