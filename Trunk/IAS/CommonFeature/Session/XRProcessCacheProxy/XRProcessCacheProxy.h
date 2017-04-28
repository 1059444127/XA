// XRProcessStatusProxy.h
#ifndef XR_PROCESS_CACHE_PROXY
#define XR_PROCESS_CACHE_PROXY

#pragma warning(push) 
#pragma warning(disable : 4251) 

#ifdef UIHDLLDEFINE
#define UIHDLLEXPORT __declspec(dllexport)
#else
#define UIHDLLEXPORT __declspec(dllimport)
#endif 

#include <iostream>  
#include <string>
#include <vector>
#include <hash_map>
#include "McsfDataHeader/mcsf_data_header_element_map_interface.h"
#include "McsfNetBase/mcsf_communication_proxy.h"
#include "McsfNetBase/SessionUtility.h"//路劲需要修改
#include "McsfLogger/mcsf_logger.h"
#include "CSerializableBase.h"
#include "CommunicationCommandID.h"
#include "SessionFuncID.h"
using namespace std;


MCSF_DATAHEADER_BEGIN_NAMESPACE // begin namespace
class UIHDLLEXPORT CProcessCacheProxy
{
public:
	static CProcessCacheProxy* GetInstance();
    void Commit(string proxyName);
	bool Initialize(Mcsf::ICommunicationProxy *proxy);
	void Add(CSerializableBase *serializedObj);
	bool Refresh(string proxyName);
private:
	CProcessCacheProxy();
	 ~CProcessCacheProxy();
	static CProcessCacheProxy* m_pInstance;
	IDataHeaderElementMap *m_pDataHeaderElemMap;
	ICommunicationProxy *m_pProxy;
	
	static const int UpdateID=UpdateProcessStatusID;
	static const int QueryID=QueryProcessStatusID;
	static const int CACHE_PROCESS_STATUS=IAS_SESSION_CACHE_PROCESS_STATUS;
};

MCSF_DATAHEADER_END_NAMESPACE // end namespace Mcsf
#endif 
