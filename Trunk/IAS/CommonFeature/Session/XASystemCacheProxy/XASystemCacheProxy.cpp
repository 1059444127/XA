// This is the main DLL file.
#include "StdAfx.h"
#include "XASystemCacheProxy.h"
#include "McsfDataHeader/mcsf_data_header_common.h"
#include "McsfDataHeader/mcsf_data_header_element_interface.h"
#include "McsfDataHeader/mcsf_data_header_element_map_interface.h"
#include "mcsf_systemenvironment_factory.h"
#include "mcsf_isystemenvironment_config.h"
#include "SessiongManager_Logger.h"
#include <io.h>
#include <fstream>
#include <algorithm>

using namespace std;

MCSF_DATAHEADER_BEGIN_NAMESPACE
static string SessionManager=SystemSessionManager;

bool CSystemCacheProxy::Commit()
{  
	vector<string> paras;
	string serializedDataHead=CSessionUtility::SerializeDataHead(m_iDataHeaderElemMap);
	paras.push_back(serializedDataHead);
	string buf=CSessionUtility::RequestCommandProtoBuffer(StoreSessionDataID,paras);
	Mcsf::CommandContext* context = CSessionUtility::CreateCommandContext(SystemSessionManager,XA_SYSTEMSESSION_DATA_CACHE,buf);
	int result=m_proxy->AsyncSendCommand(context);
	return 0==result?true:false;
}  

bool CSystemCacheProxy::Refresh()
{
    string sResponse("");
	vector<string> paras;
	string buf=CSessionUtility::RequestCommandProtoBuffer(QuerySessionDataID,paras);
	Mcsf::CommandContext* context = CSessionUtility::CreateCommandContext(SystemSessionManager,XA_SYSTEMSESSION_DATA_CACHE,buf);
	int iRet = m_proxy->SyncSendCommand(context, sResponse);
	if(iRet!=0)return false;
    CSessionUtility::DeserializeDataHead(sResponse,m_iDataHeaderElemMap);
	return true;
}

 bool CSystemCacheProxy::Initialize(ICommunicationProxy *proxy)
{
    if (NULL == proxy)
    {
		LOG_ERROR("CSystemCacheProxy::Initialize false, proxy is NULL.");	
        return false;
    }
    m_proxy = proxy;
    return true;
}

 CSystemCacheProxy::CSystemCacheProxy()
{
	m_iDataHeaderElemMap=IDataHeaderElementMap::CreateDataHeaderElementMap();
}

void CSystemCacheProxy::Finalize()
{
	delete m_iDataHeaderElemMap;
}

bool CSystemCacheProxy::SetImageHeadData(uint32_t tagCode, const std::string &value)//该方法需要重载
{
	if(m_iDataHeaderElemMap->FindTag(tagCode))
	{
		m_iDataHeaderElemMap->RemoveElementByTag(tagCode);
	}
	return m_iDataHeaderElemMap->InsertStringByTag(tagCode,value.data(),sizeof(value));
}

string CSystemCacheProxy::GetImageHeadData(uint32_t tagCode)//该方法需要重载
{
	ConstString sValue = nullptr;
	if(m_iDataHeaderElemMap->FindTag(tagCode))
	{
		int iCount = 0;
		(void)m_iDataHeaderElemMap->GetStringByTag(tagCode,&sValue,&iCount);
	}
	return sValue;
}
MCSF_DATAHEADER_END_NAMESPACE  // end namespace Mcsf