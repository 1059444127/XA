// This is the main DLL file.

#include "McsfDataHeader/mcsf_data_header_common.h"
#include "SessionUtility.h"

using namespace std;
MCSF_DATAHEADER_BEGIN_NAMESPACE 

string CSessionUtility::SerializeDataHead(IDataHeaderElementMap* spDataHeaderElemMap)
 {
	 string sSerializedOutput("");
	 IDataHeaderElementMap::Serialize(spDataHeaderElemMap,&sSerializedOutput);
	 return sSerializedOutput;
 }

void CSessionUtility::DeserializeDataHead(string serializedDataHead, IDataHeaderElementMap* pHeader)
{
	IDataHeaderElementMap::Deserialize(serializedDataHead,pHeader);
}

CommandContext* CSessionUtility::CreateCommandContext(string receiver, int commandID, string serializeObject)
{
	Mcsf::CommandContext* context = new Mcsf::CommandContext();
	context->sReceiver = receiver;
	context->iCommandId = commandID;
	context->sSerializeObject = serializeObject;
	return context;
}

string CSessionUtility::RequestCommandProtoBuffer(int commandID, vector<string>&pVector )
{
	UIH::XR::Basis::Proto::SessionRequestCommand sRequestCommand;
	sRequestCommand.set_commandid(commandID);

	 for (vector<string>::iterator  iter =pVector.begin(); iter != pVector.end(); iter++)
    {
		std::string str = *iter;
		sRequestCommand.add_parameters(str);
    }
	string sRequestCommandString("");
	sRequestCommand.SerializeToString(&sRequestCommandString);
	return sRequestCommandString;
}

MCSF_DATAHEADER_END_NAMESPACE