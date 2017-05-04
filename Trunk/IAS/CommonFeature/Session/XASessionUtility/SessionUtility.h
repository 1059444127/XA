
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
#include<vector>
#pragma once
using namespace std;

MCSF_DATAHEADER_BEGIN_NAMESPACE 

class UIHDLLEXPORT CSessionUtility
{
public:
	 static string SerializeDataHead(IDataHeaderElementMap* spDataHeaderElemMap);
	 static void DeserializeDataHead(string serializedDataHead, IDataHeaderElementMap* pHeader);
	 static CommandContext* CreateCommandContext(string receiver, int commandID,string serializeObject);
	 static string RequestCommandProtoBuffer(int commandID, vector <string>&paras);
};

MCSF_DATAHEADER_END_NAMESPACE
#endif 