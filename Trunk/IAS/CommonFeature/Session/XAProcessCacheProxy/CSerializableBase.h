
#ifndef SERIALIZA_BASE
#define SERIALIZA_BASE

#ifdef UIHDLLDEFINE
#define UIHDLLEXPORT __declspec(dllexport)
#else
#define UIHDLLEXPORT __declspec(dllimport)
#endif 

#include "McsfDataHeader/mcsf_data_header_element_map_interface.h"
using namespace std;

MCSF_DATAHEADER_BEGIN_NAMESPACE // begin namespace

class UIHDLLEXPORT CSerializableBase
{
public:
	 typedef unsigned int uint32_t;
	 void SetImageHead( IDataHeaderElementMap *m_pDataHeaderElemMap);
	 virtual void CacheStatus();
	 virtual void QueryStatus();
	 IDataHeaderElementMap *m_pDataHeaderElemMap;
	  CSerializableBase();
protected:  
	 void CacheStatus(uint32_t tagCode,const string& value);
	 string QueryStringStatus(uint32_t tagCode);
};

MCSF_DATAHEADER_END_NAMESPACE // end namespace Mcsf
#endif 