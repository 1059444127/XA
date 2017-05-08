
#ifndef XR_SYSTEM_CACHE_PROXY
#define XR_SYSTEM_CACHE_PROXY

#ifdef UIHDLLDEFINE
#define UIHDLLEXPORT __declspec(dllexport)
#else
#define UIHDLLEXPORT __declspec(dllimport)
#endif 

#include "McsfDataHeader/mcsf_data_header_element_map_interface.h"
#include "McsfNetBase/mcsf_communication_proxy.h"
#include "McsfLogger/mcsf_logger.h"
#include "XASession/CommunicationCommandID.h"
#include "XASession/SessionFuncID.h"
#include "XASession/SessionUtility.h"
#include "XASession/CommunicationNode.h"
#pragma once
using namespace std;

MCSF_DATAHEADER_BEGIN_NAMESPACE 

class UIHDLLEXPORT CSystemCacheProxy
{
public:
	 typedef unsigned int uint32_t;
	 CSystemCacheProxy();
	 bool Commit();
	 bool Refresh();
	 bool Initialize(ICommunicationProxy *pProxy);
	 void Finalize();
	 bool SetStringByTag(uint32_t tagCode, const string &value);
	 bool SetInt16ByTag(uint32_t tagCode, const short &value);
	 bool SetInt16ArrayByTag(uint32_t tagCode,short *value,int size);
     bool SetUInt16ByTag(uint32_t tagCode, const unsigned short &value);
	 bool SetUInt16ArrayByTag(uint32_t tagCode,unsigned short *value,int size);
	 bool SetInt32ByTag(uint32_t tagCode, const int &value);
	 bool SetInt32ArrayByTag(uint32_t tagCode, int *value,int size);
	 bool SetUInt32ByTag(uint32_t tagCode, const unsigned int &value);
	 bool SetUInt32ArrayByTag(uint32_t tagCode,unsigned int *value,int size);
	 bool SetSingleFloatByTag(uint32_t tagCode, const float &value);
	 bool SetSingleFloatArrayByTag(uint32_t tagCode, float *value,int size);
	 bool SetDoubleFloatByTag(uint32_t tagCode, const double &value);
	 bool SetDoubleFloatArrayByTag(uint32_t tagCode, double *value,int size);
	 bool GetStringByTag(uint32_t tagCode,string &value);
	 bool GetInt16ByTag(uint32_t tagCode,short &value);
	 bool GetInt16ArrayByTag(uint32_t tagCode,short** valueArray,int *iSize);
	 bool GetUInt16ByTag(uint32_t tagCode,unsigned short&value);
	 bool GetUInt16ArrayByTag(uint32_t tagCode,unsigned short **valueArray, int *iSize);
	 bool GetInt32ByTag(uint32_t tagCode,int &value);
	 bool GetInt32ArrayByTag(uint32_t tagCode,int * *value, int *size);
	 bool GetUInt32ByTag(uint32_t tagCode,unsigned int&value);
	 bool GetUInt32ArrayByTag(uint32_t tagCode, unsigned int **valueArray, int *iSize);
	 bool GetSingleFloatByTag(uint32_t tagCode,float&value);
	 bool GetSingleFloatArrayByTag(uint32_t tagCode,float **valueArray, int *iSize);
	 bool  GetDoubleFloatByTag(uint32_t tagCode,double&value);
	 bool GetDoubleFloatArrayByTag(uint32_t tagCode,double**valueArray, int *iSize);
private:
	IDataHeaderElementMap *m_iDataHeaderElemMap;
	ICommunicationProxy *m_proxy;
};

MCSF_DATAHEADER_END_NAMESPACE
#endif 