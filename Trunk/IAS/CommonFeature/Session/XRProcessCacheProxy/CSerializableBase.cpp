#include "StdAfx.h"
#include "McsfDataHeader/mcsf_data_header_element_interface.h"
#include "McsfDataHeader/mcsf_data_header_element_map_interface.h"
#include "CSerializableBase.h"
#include "SessiongManager_Logger.h"
using namespace std;
#pragma comment(lib, "McsfDataHeader.lib")

MCSF_DATAHEADER_BEGIN_NAMESPACE

void CSerializableBase::CacheStatus(){}

void CSerializableBase::QueryStatus(){}

 CSerializableBase::CSerializableBase()
{
	m_pDataHeaderElemMap=IDataHeaderElementMap::CreateDataHeaderElementMap();
}

void CSerializableBase::SetImageHead(IDataHeaderElementMap * pDataHeaderElemMap)
{
	m_pDataHeaderElemMap->Clone(&pDataHeaderElemMap);
}

void  CSerializableBase::CacheStatus(uint32_t tagCode,ConstString value)
{
	if(m_pDataHeaderElemMap->FindTag(tagCode))
	{
		m_pDataHeaderElemMap->RemoveElementByTag(tagCode);
	}
	m_pDataHeaderElemMap->InsertStringByTag(tagCode,value,sizeof(value));
}

string CSerializableBase::QueryStringStatus(uint32_t tagCode)//ашвЊжиди
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
			if(eType==TYPE_STRING)
			{
				return "";
			}
			ConstString sValue = nullptr;
            int iCount = 0;
			(void)m_pDataHeaderElemMap->GetStringByTag(tagCode,&sValue,&iCount);
			return sValue;
		}
        pElement = pElement->GetNextSibling();
    }
	return "";
}


MCSF_DATAHEADER_END_NAMESPACE  // end namespace Mcsf