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
#include "McsfLogger/mcsf_logger.h"
#include <algorithm>
#include "XASession/CommunicationCommandID.h"
#include "XASession/SessionFuncID.h"
#include "XASession/SessionUtility.h"
#include "XASession/CommunicationNode.h"
#pragma once
using namespace std;

MCSF_DATAHEADER_BEGIN_NAMESPACE 

class UIHDLLEXPORT CSystemCacheProxy
{
public:
	 typedef unsigned int uint32_t;
	 bool Commit();
	 bool Refresh();
	 bool Initialize(ICommunicationProxy *pProxy);
	 void Finalize();
	 CSystemCacheProxy();
	 bool SetImageHeadData(uint32_t tagCode, const string &value);
	 string GetImageHeadData(uint32_t tagCode);
private:
	IDataHeaderElementMap *m_iDataHeaderElemMap;
	ICommunicationProxy *m_proxy;
};

MCSF_DATAHEADER_END_NAMESPACE
#endif 