#include "Response.h"

time_t Interaction::Response::getTime()
{
    return creationTime;
}

std::string Interaction::Response::getExtensionID()
{
    return extensionID;
}

std::string Interaction::Response::getResponseID()
{
    return responseID;
}

Interaction::Response::Response(std::string aiResponse)
{
    response = aiResponse;
}

Interaction::Response::~Response()
{
    delete &creationTime;

    delete[] &extensionID;
    delete[] &responseID;

    delete[] &response;
}
