#pragma once
#using "annaCore2.dll"

using namespace ANNA;

ref class CResponse
{
public:
	System::String ExtensionID;

	System::String responseID;

	void* response;

	CResponse(ANNA::Extension extension, void* aiResponse);
	~CResponse();

private:
	System::DateTime Time();
};

