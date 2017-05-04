#include "StdAfx.h"
#include "XAProcessCacheProxy.h"
#include "SessiongManager_Logger.h"
#include "McsfDataHeader/mcsf_data_header_common.h"
#include "mcsf_systemenvironment_factory.h"
#include "mcsf_isystemenvironment_config.h"
#include <io.h>
#include <fstream>
#include <algorithm>

#include<vector>
using namespace std;

MCSF_DATAHEADER_BEGIN_NAMESPACE

static string SessionManager=SystemSessionManager;

CProcessCacheProxy::CProcessCacheProxy()
{  
	m_pDataHeaderElemMap=IDataHeaderElementMap::CreateDataHeaderElementMap();
}  

void CProcessCacheProxy::Finalize()
{
	delete m_pDataHeaderElemMap;
}

CProcessCacheProxy* CProcessCacheProxy::s_pInstance=new  CProcessCacheProxy();

CProcessCacheProxy* CProcessCacheProxy::GetInstance()  
{  
    return s_pInstance;  
}  

void CProcessCacheProxy::Commit()
{
	string serializedDataHead=CSessionUtility::SerializeDataHead(m_pDataHeaderElemMap);
	vector<string> paras;
	paras.push_back(m_proxyName);
	paras.push_back(serializedDataHead);
	string buf=CSessionUtility::RequestCommandProtoBuffer(UpdateProcessStatusID,paras);
	Mcsf::CommandContext* context = CSessionUtility::CreateCommandContext(SystemSessionManager,XA_SYSTEMSESSION_PROCESS_CACHE,buf);
	string sResponse("");
	int iRet = m_proxy->SyncSendCommand(context, sResponse);
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
	m_proxy=proxy;
	m_proxyName=proxy->GetName();
	return true;
}

void CProcessCacheProxy::Add(CSerializableBase *serializedObj)
{
	if(!serializedObj)
	{
		LOG_ERROR("CProcessCacheProxy::Add false, serializedObj is NULL.");	
		return;
	}
//	serializedObj->m_pDataHeaderElemMap->Clone(&m_pDataHeaderElemMap);//此处有问题？

    auto pDataHeaderElemMap = serializedObj->m_pDataHeaderElemMap;
    //Tag itag = 0x00080008;//这个tag是干什么用的
    //ConstString pStr = "UIH2";
    //int iStrSize = 4;
    //pDataHeaderElemMap->InsertStringByTag(itag, pStr, iStrSize);

    IDataHeaderElement* pElement = nullptr;
    if (!pDataHeaderElemMap->GetFirstElement(&pElement))
    {
        return;
    }
	//此处是否可以释放pImageHeader？
    while(nullptr != pElement)
    {
        InsertSrcElementToMap(pElement, m_pDataHeaderElemMap);
        pElement = pElement->GetNextSibling();
    }
}

bool CProcessCacheProxy::Refresh()
{
	vector<string> paras;
	paras.push_back(m_proxyName);
	string buf=CSessionUtility::RequestCommandProtoBuffer(QueryProcessStatusID,paras);
	Mcsf::CommandContext* context = CSessionUtility::CreateCommandContext(SystemSessionManager,XA_SYSTEMSESSION_PROCESS_CACHE,buf);
	string sResponse("");
	int iRet = m_proxy->SyncSendCommand(context,sResponse);
	if(0==iRet)
	{
		CSessionUtility::DeserializeDataHead(sResponse, m_pDataHeaderElemMap);
		return true;
	}
	return false;
}

void CProcessCacheProxy::InsertSrcElementToMap( IDataHeaderElement* pElement, IDataHeaderElementMap *pTargetElemMap)
{
    Tag tag = pElement->GetTag();
    EnumElementType eType = pElement->GetType();
	if(eType==TYPE_STRING)
	{
		 ConstString pStr = nullptr;
         int iStrSize= 0;
        (void)pElement->GetString(&pStr, &iStrSize);
         pTargetElemMap->InsertStringByTag(tag, pStr, iStrSize);
	}
}

MCSF_DATAHEADER_END_NAMESPACE