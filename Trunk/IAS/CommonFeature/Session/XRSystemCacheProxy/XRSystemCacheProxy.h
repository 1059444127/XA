// XRSystemSessionProxy.h
#ifndef XR_SYSTEM_CACHE_PROXY
#define XR_SYSTEM_CACHE_PROXY

#ifdef UIHDLLDEFINE
#define UIHDLLEXPORT __declspec(dllexport)
#else
#define UIHDLLEXPORT __declspec(dllimport)
#endif 

#include "McsfDataHeader/mcsf_data_header_element_map_interface.h"
#include "McsfNetBase/mcsf_communication_proxy.h"
#include <algorithm>
#include "CommunicationCommandID.h"
#include "SessionFuncID.h"
#include "McsfNetBase/SessionUtility.h"//路劲需要修改
#include "McsfLogger/mcsf_logger.h"
#pragma once
using namespace std;

	
#pragma comment(lib, "McsfNetBase.lib")

MCSF_DATAHEADER_BEGIN_NAMESPACE 

class UIHDLLEXPORT CSystemCacheProxy
{
public:
	 typedef unsigned int uint32_t;
	 bool Commit();
	 bool Refresh();
	 bool Initialize(ICommunicationProxy *pProxy);
	 void Reset();
	 bool SetImageHeadData(uint32_t tagCode, ConstString value);
	 string GetImageHeadData(uint32_t tagCode);
private:
	IDataHeaderElementMap *m_pDataHeaderElemMap;
	ICommunicationProxy *m_pProxy;

	static const int QuerySessionID=QuerySessionDataID;
	static const int StoreSessionID = StoreSessionDataID; 
	static const int CACHE_SYSTEM_DATA=IAS_SESSION_CACHE_SYSTEM_DATA;
};

MCSF_DATAHEADER_END_NAMESPACE
#endif 