// This is the main DLL file.
#include "StdAfx.h"
#include "XRSystemCacheProxy.h"
#include "McsfDataHeader/mcsf_data_header_common.h"
#include "McsfDataHeader/mcsf_data_header_element_interface.h"
#include "McsfDataHeader/mcsf_data_header_element_map_interface.h"
#include "mcsf_systemenvironment_factory.h"
#include "mcsf_isystemenvironment_config.h"
#include "SessiongManager_Logger.h"
#include <io.h>
#include <fstream>
#include <algorithm>
#include "CommunicationNode.h"
using namespace std;
#pragma comment(lib, "McsfNetBase.lib")
#pragma comment(lib, "McsfSystemEnvironmentConfig.lib")
#pragma comment(lib, "McsfLogger.lib")
#pragma comment(lib, "XRSessionUtility.lib")
#pragma comment(lib, "McsfDataHeader.lib")

MCSF_DATAHEADER_BEGIN_NAMESPACE
static string SessionManager=SystemSessionManager;

bool CSystemCacheProxy::Commit()
{  
	string serializedDataHead=CSessionUtility::SerializeDataHead(m_pDataHeaderElemMap);
	string parArray[1]={serializedDataHead};
	string buf=CSessionUtility::RequestCommandProtoBuffer(StoreSessionDataID,parArray);
	Mcsf::CommandContext* context = CSessionUtility::CreateCommandContext(SystemSessionManager,IAS_SESSION_CACHE_SYSTEM_DATA,buf);
	int result=m_pProxy->AsyncSendCommand(context);
	return 0==result?true:false;
}  

bool CSystemCacheProxy::Refresh()
{
    string sResponse = "";
	string parArray[1]={"0"};
	string buf=CSessionUtility::RequestCommandProtoBuffer(QuerySessionDataID,parArray);
	Mcsf::CommandContext* context = CSessionUtility::CreateCommandContext(SystemSessionManager,IAS_SESSION_CACHE_SYSTEM_DATA,buf);
	int iRet = m_pProxy->SyncSendCommand(context, sResponse);
	if(iRet!=0)return false;
	shared_ptr<IDataHeaderElementMap> pElementMap=CSessionUtility::DeserializeDataHead(sResponse);//如何将shared_ptr<IDataHeaderElementMap>转为IDataHeaderElementMap类型呢
	m_pDataHeaderElemMap=pElementMap.get();
	return true;
}

 bool CSystemCacheProxy::Initialize(ICommunicationProxy *proxy)
{
    if (NULL == proxy)
    {
		LOG_ERROR("CSystemCacheProxy::Initialize false, proxy is NULL.");	
        return false;
    }
    m_pProxy = proxy;
    return true;
}

void CSystemCacheProxy::Reset()
{
	m_pDataHeaderElemMap=IDataHeaderElementMap::CreateDataHeaderElementMap();
}

bool CSystemCacheProxy::SetImageHeadData(uint32_t tagCode, ConstString value)//该方法需要重载
{
	if(m_pDataHeaderElemMap->FindTag(tagCode))
	{
		m_pDataHeaderElemMap->RemoveElementByTag(tagCode);
	}
	return m_pDataHeaderElemMap->InsertStringByTag(tagCode,value,sizeof(value));
}

string CSystemCacheProxy::GetImageHeadData(uint32_t tagCode)//该方法需要重载
{
	ConstString sValue = nullptr;
	if(m_pDataHeaderElemMap->FindTag(tagCode))
	{
		int iCount = 0;
		(void)m_pDataHeaderElemMap->GetStringByTag(tagCode,&sValue,&iCount);
	}
	return sValue;
}
MCSF_DATAHEADER_END_NAMESPACE  // end namespace Mcsf