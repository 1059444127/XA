// This is the main DLL file.
#include "StdAfx.h"
#include "XRProcessCacheProxy.h"
#include "SessiongManager_Logger.h"
#include "McsfDataHeader/mcsf_data_header_common.h"
#include "mcsf_systemenvironment_factory.h"
#include "mcsf_isystemenvironment_config.h"
#include <io.h>
#include <fstream>
#include <algorithm>
#include "CommunicationNode.h"

using namespace std;
#pragma comment(lib, "McsfNetBase.lib")
#pragma comment(lib, "McsfSystemEnvironmentConfig.lib")
#pragma comment(lib, "McsfLogger.lib")
#pragma comment(lib, "XRSessionUtility.lib")

MCSF_DATAHEADER_BEGIN_NAMESPACE

static string SessionManager=SystemSessionManager;


CProcessCacheProxy::CProcessCacheProxy()
{  
	m_pDataHeaderElemMap=IDataHeaderElementMap::CreateDataHeaderElementMap();
}  

CProcessCacheProxy::~CProcessCacheProxy()
{  
	delete m_pInstance;
}  

CProcessCacheProxy* CProcessCacheProxy::m_pInstance=new  CProcessCacheProxy();

CProcessCacheProxy* CProcessCacheProxy::GetInstance()  
{  
    return m_pInstance;  
}  

void CProcessCacheProxy::Commit(string proxyName)
{
	string serializedDataHead=CSessionUtility::SerializeDataHead(m_pDataHeaderElemMap);
	string parArray[2]={proxyName,serializedDataHead};
	string buf=CSessionUtility::RequestCommandProtoBuffer(UpdateProcessStatusID,parArray);
	Mcsf::CommandContext* context = CSessionUtility::CreateCommandContext(SystemSessionManager,IAS_SESSION_CACHE_PROCESS_STATUS,buf);
	string sResponse("");
	int iRet = m_pProxy->SyncSendCommand(context, sResponse);
	if(iRet!=0)
	{
		LOG_ERROR("CProcessCacheProxy::Commit failed, iRet = " <<iRet);	
	}
}

bool CProcessCacheProxy::Initialize(Mcsf::ICommunicationProxy *proxy)
{
	if(!proxy)
	{
		LOG_ERROR("CProcessCacheProxy::Initialize false, proxy is NULL.");	
		return false;
	}
	m_pProxy=proxy;
	return true;
}

void CProcessCacheProxy::Add(CSerializableBase *serializedObj)
{
	if(!serializedObj)
	{
		LOG_ERROR("CProcessCacheProxy::Add false, serializedObj is NULL.");	
		return;
	}
	serializedObj->SetImageHead(m_pDataHeaderElemMap);
}

bool CProcessCacheProxy::Refresh(string proxyName)
{
	if(proxyName=="")
	{
		LOG_ERROR("CProcessCacheProxy::Refresh false, proxyName is NULL.");	
		return false;
	}
	string parArray[1]={proxyName};
	string buf=CSessionUtility::RequestCommandProtoBuffer(QueryProcessStatusID,parArray);
	Mcsf::CommandContext* context = CSessionUtility::CreateCommandContext(SystemSessionManager,IAS_SESSION_CACHE_PROCESS_STATUS,buf);
	string sResponse("");
	int iRet = m_pProxy->SyncSendCommand(context, sResponse);
	if(0==iRet)
	{
		shared_ptr<IDataHeaderElementMap> pElementMap=CSessionUtility::DeserializeDataHead(sResponse);
		m_pDataHeaderElemMap=pElementMap.get();
		return true;
	}
	return false;
}

MCSF_DATAHEADER_END_NAMESPACE  // end namespace Mcsf