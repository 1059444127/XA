// This is the main DLL file.
#include "XASystemCacheProxy.h"
#include "McsfDataHeader/mcsf_data_header_common.h"
#include "McsfDataHeader/mcsf_data_header_element_interface.h"
#include "McsfDataHeader/mcsf_data_header_element_map_interface.h"
#include "SessiongManager_Logger.h"

using namespace std;

MCSF_DATAHEADER_BEGIN_NAMESPACE

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

bool CSystemCacheProxy::SetStringByTag(uint32_t tagCode, const std::string &value)
{
	if(m_iDataHeaderElemMap->FindTag(tagCode))
	{
		m_iDataHeaderElemMap->RemoveElementByTag(tagCode);
	}
	return m_iDataHeaderElemMap->InsertStringByTag(tagCode,value.data(),sizeof(value));
}

bool CSystemCacheProxy::SetInt16ByTag(uint32_t tagCode, const short &value)
{
	if(m_iDataHeaderElemMap->FindTag(tagCode))
	{
		m_iDataHeaderElemMap->RemoveElementByTag(tagCode);
	}
	return m_iDataHeaderElemMap->InsertInt16ValueByTag(tagCode,value);
}

bool CSystemCacheProxy::SetInt16ArrayByTag(uint32_t tagCode,short *value,int size)
{
	if(m_iDataHeaderElemMap->FindTag(tagCode))
	{
		m_iDataHeaderElemMap->RemoveElementByTag(tagCode);
	}
	return m_iDataHeaderElemMap->InsertInt16ArrayByTag(tagCode,value,size);
}
 
bool CSystemCacheProxy::SetUInt16ByTag(uint32_t tagCode, const unsigned short &value)//111111111111111111111111111111111111111111
{
	if(m_iDataHeaderElemMap->FindTag(tagCode))
	{
		m_iDataHeaderElemMap->RemoveElementByTag(tagCode);
	}
	return m_iDataHeaderElemMap->InsertUInt16ValueByTag(tagCode,value);
}

bool CSystemCacheProxy::SetUInt16ArrayByTag(uint32_t tagCode,unsigned short *value,int size)
{
	if(m_iDataHeaderElemMap->FindTag(tagCode))
	{
		m_iDataHeaderElemMap->RemoveElementByTag(tagCode);
	}
	return m_iDataHeaderElemMap->InsertUInt16ArrayByTag(tagCode,value,size);
}

bool CSystemCacheProxy::SetInt32ByTag(uint32_t tagCode, const int &value)//111111111111111111111111111111111111111111
{
	if(m_iDataHeaderElemMap->FindTag(tagCode))
	{
		m_iDataHeaderElemMap->RemoveElementByTag(tagCode);
	}
	return m_iDataHeaderElemMap->InsertInt32ValueByTag(tagCode,value);
}

bool CSystemCacheProxy::SetInt32ArrayByTag(uint32_t tagCode, int *value,int size)//111111111111111111111111111111111111111111
{
	if(m_iDataHeaderElemMap->FindTag(tagCode))
	{
		m_iDataHeaderElemMap->RemoveElementByTag(tagCode);
	}
	return m_iDataHeaderElemMap->InsertInt32ArrayByTag(tagCode,value,size);
}

bool CSystemCacheProxy::SetUInt32ByTag(uint32_t tagCode, const unsigned int &value)//111111111111111111111111111111111111111111
{
	if(m_iDataHeaderElemMap->FindTag(tagCode))
	{
		m_iDataHeaderElemMap->RemoveElementByTag(tagCode);
	}
	return m_iDataHeaderElemMap->InsertUInt32ValueByTag(tagCode,value);
}

bool CSystemCacheProxy::SetUInt32ArrayByTag(uint32_t tagCode,unsigned int *value,int size)
{
	if(m_iDataHeaderElemMap->FindTag(tagCode))
	{
		m_iDataHeaderElemMap->RemoveElementByTag(tagCode);
	}
	return m_iDataHeaderElemMap->InsertUInt32ArrayByTag(tagCode,value,size);
}

bool CSystemCacheProxy::SetSingleFloatByTag(uint32_t tagCode, const float &value)
{
	if(m_iDataHeaderElemMap->FindTag(tagCode))
	{
		m_iDataHeaderElemMap->RemoveElementByTag(tagCode);
	}
	return m_iDataHeaderElemMap->InsertSingleFloatValueByTag(tagCode,value);
}

bool CSystemCacheProxy::SetSingleFloatArrayByTag(uint32_t tagCode, float *value,int size)
{
	if(m_iDataHeaderElemMap->FindTag(tagCode))
	{
		m_iDataHeaderElemMap->RemoveElementByTag(tagCode);
	}
	return m_iDataHeaderElemMap->InsertSingleFloatArrayByTag(tagCode,value,size);
}

bool CSystemCacheProxy::SetDoubleFloatByTag(uint32_t tagCode, const double &value)//111111111111111111111111111111111111111111
{
	if(m_iDataHeaderElemMap->FindTag(tagCode))
	{
		m_iDataHeaderElemMap->RemoveElementByTag(tagCode);
	}
	return m_iDataHeaderElemMap->InsertDoubleFloatValueByTag(tagCode,value);
}

bool CSystemCacheProxy::SetDoubleFloatArrayByTag(uint32_t tagCode, double *value,int size)
{
	if(m_iDataHeaderElemMap->FindTag(tagCode))
	{
		m_iDataHeaderElemMap->RemoveElementByTag(tagCode);
	}
	return m_iDataHeaderElemMap->InsertDoubleFloatArrayByTag(tagCode,value,size);
}

//string CSystemCacheProxy::GetStringByTag(uint32_t tagCode)
//{
//	ConstString sValue = nullptr;
//	if(m_iDataHeaderElemMap->FindTag(tagCode))
//	{
//		int iCount = 0;
//		/*(void)m_iDataHeaderElemMap->GetStringByTag(tagCode,&sValue,&iCount);*/
//		bool bResult=m_iDataHeaderElemMap->GetStringByTag(tagCode,&sValue,&iCount);
//		if(!bResult)
//		{
//			return nullptr;
//		}
//	}
//	return sValue;
//}

bool CSystemCacheProxy::GetStringByTag(uint32_t tagCode,string &value)
{
	ConstString sValue = nullptr;
	int iCount = 0;
    bool bResult=m_iDataHeaderElemMap->GetStringByTag(tagCode,&sValue,&iCount);
	value=sValue;
	return bResult;
}

bool CSystemCacheProxy::GetInt16ByTag(uint32_t tagCode,short &value)
{
	return m_iDataHeaderElemMap->GetInt16ByTag(tagCode,&value,0);
}

bool CSystemCacheProxy::GetInt16ArrayByTag(uint32_t tagCode, short **valueArray,int *iSize)
{
	ConstInt16Array value = nullptr;
	bool bResult=m_iDataHeaderElemMap->GetInt16ArrayByTag(tagCode,&value,&(*iSize));
	 *valueArray= (short*)value;
	return bResult;
}

bool CSystemCacheProxy::GetUInt16ByTag(uint32_t tagCode,unsigned short &value)
{
	return m_iDataHeaderElemMap->GetUInt16ByTag(tagCode,&value,0);
}

bool CSystemCacheProxy::GetUInt16ArrayByTag(uint32_t tagCode,unsigned short **valueArray, int*iSize)
{
	ConstUInt16Array value = nullptr;
	bool bResult=m_iDataHeaderElemMap->GetUInt16ArrayByTag(tagCode,&value,&(*iSize));
	*valueArray= (unsigned short *)value;
	return bResult;
}

bool CSystemCacheProxy::GetInt32ByTag(uint32_t tagCode,int &value)
{
	return m_iDataHeaderElemMap->GetInt32ByTag(tagCode,&value,0);
}

bool CSystemCacheProxy::GetInt32ArrayByTag(uint32_t tagCode,int **valueArray, int *iSize)
{
	ConstInt32Array value = nullptr;
	bool bResult=m_iDataHeaderElemMap->GetInt32ArrayByTag(tagCode,&value,&(*iSize));
    *valueArray=(int *)value;
	return bResult;
}

bool CSystemCacheProxy::GetUInt32ByTag(uint32_t tagCode,unsigned int &value)
{
	return m_iDataHeaderElemMap->GetUInt32ByTag(tagCode,&value,0);
}


bool CSystemCacheProxy::GetUInt32ArrayByTag(uint32_t tagCode,unsigned int**valueArray, int *iSize)
{
	ConstUInt32Array value = nullptr;
	bool bResult=m_iDataHeaderElemMap->GetUInt32ArrayByTag(tagCode,&value,&(*iSize));
	*valueArray= (unsigned int *)value;
	return bResult;
}

bool CSystemCacheProxy::GetSingleFloatByTag(uint32_t tagCode,float&value)
{
	return m_iDataHeaderElemMap->GetSingleFloatByTag(tagCode,&value,0);
}

bool CSystemCacheProxy::GetSingleFloatArrayByTag(uint32_t tagCode,float **valueArray, int *iSize)
{
	ConstSingleFloatArray value = nullptr;
	bool bResult=m_iDataHeaderElemMap->GetSingleFloatArrayByTag(tagCode,&value,&(*iSize));
		*valueArray=(float *)value;
	return bResult;
}

bool CSystemCacheProxy::GetDoubleFloatByTag(uint32_t tagCode,double&value)
{
	//DoubleFloat value = 0;
	return m_iDataHeaderElemMap->GetDoubleFloatByTag(tagCode,&value,0);
}

bool CSystemCacheProxy::GetDoubleFloatArrayByTag(uint32_t tagCode,double **valueArray, int *iSize)
{
	ConstDoubleFloatArray value = nullptr;
	bool bResult=m_iDataHeaderElemMap->GetDoubleFloatArrayByTag(tagCode,&value,&(*iSize));
		 *valueArray=(double *)value;
	/*return (double *)value;*/
	return bResult;
}
MCSF_DATAHEADER_END_NAMESPACE