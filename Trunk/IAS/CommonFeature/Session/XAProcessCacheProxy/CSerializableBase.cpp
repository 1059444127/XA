#include "StdAfx.h"
#include "McsfDataHeader/mcsf_data_header_element_interface.h"
#include "McsfDataHeader/mcsf_data_header_element_map_interface.h"
#include "CSerializableBase.h"
#include "SessiongManager_Logger.h"
using namespace std;

MCSF_DATAHEADER_BEGIN_NAMESPACE

void CSerializableBase::CacheStatus(){}

void CSerializableBase::QueryStatus(){}

 CSerializableBase::CSerializableBase()
{
	m_pDataHeaderElemMap=IDataHeaderElementMap::CreateDataHeaderElementMap();
}

void CSerializableBase::SetImageHead(IDataHeaderElementMap * pDataHeaderElemMap)
{
	 pDataHeaderElemMap->Clone(&m_pDataHeaderElemMap);
}

void  CSerializableBase::CacheStatus(uint32_t tagCode,const string &value)
{
	if(m_pDataHeaderElemMap->FindTag(tagCode))
	{
		m_pDataHeaderElemMap->RemoveElementByTag(tagCode);
	}
	bool re=m_pDataHeaderElemMap->InsertStringByTag(tagCode,value.data(),sizeof(value));
	if(re)
	{
		printf("CacheStatus re true\n");
	}
	else
	{
		printf("CacheStatus re false\n");
	}
}

string CSerializableBase::QueryStringStatus(uint32_t tagCode)//需要重载
{
	if(!m_pDataHeaderElemMap->FindTag(tagCode))
	{
		return "";
	}

	IDataHeaderElement* pElement = nullptr;
    if (!m_pDataHeaderElemMap->GetFirstElement(&pElement))
    {
       return "";
    }

    while(nullptr != pElement)
    {
        uint32_t elemetnTagCode = pElement->GetTag();
		if(elemetnTagCode==tagCode)
		{
		    EnumElementType eType = pElement->GetType();
			if(eType==TYPE_STRING)//如果是非string呢，如何保证该tag的etype是string呢
			{
				ConstString sValue = nullptr;
				int iCount = 0;
				(void)m_pDataHeaderElemMap->GetStringByTag(tagCode,&sValue,&iCount);
				printf("sValue%s\n",sValue);
				return sValue;
			}
			printf("type error");
		}
        pElement = pElement->GetNextSibling();
    }
	return "";
}

MCSF_DATAHEADER_END_NAMESPACE  // end namespace Mcsf