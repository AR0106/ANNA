#include "Response.h"

CResponse::CResponse(ANNA::Extension extension, void* aiResponse)
{
	ExtensionID = extension.ANEID;
}

System::DateTime CResponse::Time()
{
	return System::DateTime::Now;
}
