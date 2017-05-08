
#ifndef XR_PROCESS_CACHE_PROXY
#define XR_PROCESS_CACHE_PROXY

#ifdef UIHDLLDEFINE
#define UIHDLLEXPORT __declspec(dllexport)
#else
#define UIHDLLEXPORT __declspec(dllimport)
#endif 


#include <string>
#include <vector>
#include "McsfDataHeader/mcsf_data_header_element_map_interface.h"
#include "McsfDataHeader/mcsf_data_header_element_interface.h"
#include "McsfNetBase/mcsf_communication_proxy.h"
#include "XASession/SessionUtility.h"
#include "McsfLogger/mcsf_logger.h"
#include "CSerializableBase.h"
#include "XASession/CommunicationCommandID.h"
#include "XASession/SessionFuncID.h"
#include "XASession/CommunicationNode.h"
using namespace std;


MCSF_DATAHEADER_BEGIN_NAMESPACE 
class UIHDLLEXPORT CProcessCacheProxy
{
public:
	static CProcessCacheProxy* GetInstance();
    void Commit();
	bool Initialize(Mcsf::ICommunicationProxy *proxy);
	void Add(CSerializableBase *serializedObj);
	bool Refresh();
	void Finalize();
	IDataHeaderElementMap *m_pDataHeaderElemMap;
private:
	CProcessCacheProxy();
	void InsertSrcElementToMap(IDataHeaderElement* pElement, IDataHeaderElementMap *pTargetElemMap);
	static CProcessCacheProxy* s_pInstance;
	ICommunicationProxy *m_proxy;
	string m_proxyName;
};
MCSF_DATAHEADER_END_NAMESPACE 
#endif 
