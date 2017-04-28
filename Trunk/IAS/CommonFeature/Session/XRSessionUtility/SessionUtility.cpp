// This is the main DLL file.

#include "stdafx.h"
#include "McsfDataHeader/mcsf_data_header_common.h"
#include "SessionUtility.h"

using namespace std;
#pragma comment(lib, "McsfLogger.lib")
#pragma comment(lib, "McsfDataHeader.lib")
MCSF_DATAHEADER_BEGIN_NAMESPACE 

string CSessionUtility::SerializeDataHead(IDataHeaderElementMap *m_pDataHeaderElemMap)
 {
	 string sSerializedOutput("");
	 IDataHeaderElementMap::Serialize(m_pDataHeaderElemMap,&sSerializedOutput);
	 return sSerializedOutput;
 }

shared_ptr<IDataHeaderElementMap> CSessionUtility::DeserializeDataHead(string serializedDataHead)
{
	shared_ptr<IDataHeaderElementMap>
		pHeader(Mcsf::IDataHeaderElementMap::CreateDataHeader());
	IDataHeaderElementMap::Deserialize(serializedDataHead,pHeader.get());
	return pHeader;
}

CommandContext* CSessionUtility::CreateCommandContext(string receiver, int commandID, string serializeObject)
{
	Mcsf::CommandContext* context = new Mcsf::CommandContext();
	context->sReceiver = receiver;
	context->iCommandId = commandID;
	context->sSerializeObject = serializeObject;
	return context;
}

string CSessionUtility::RequestCommandProtoBuffer(int commandID,string parArray[])
{
	UIH::XR::Basis::Proto::SessionRequestCommand sRequestCommand;
	sRequestCommand.set_commandid(commandID);
	for(int i=0;i<sizeof(parArray)/sizeof(string);i++)
	{
		if(parArray[i]=="0")continue;
		sRequestCommand.set_parameters(i,parArray[i]);
	}
	string sRequestCommandString= "";
	sRequestCommand.SerializeToString(&sRequestCommandString);
	return sRequestCommandString;
}

MCSF_DATAHEADER_END_NAMESPACE