#include "Response.h"

time_t AnnaInteraction::Response::getTime()
{
    return creationTime;
}

std::string AnnaInteraction::Response::getExtensionID()
{
    return extensionID;
}

std::string AnnaInteraction::Response::getResponseID()
{
    return responseID;
}

std::string AnnaInteraction::Response::getResponse()
{
    return response;
}

AnnaInteraction::Response::Response(AnnaExtensions::Extension extension, std::string aiResponse)
{
    std::string aneid = extension.getANEID();

    response = aiResponse;
    extensionID = aneid;
    responseID = aneid.substr(0, 3) +
        std::to_string(std::chrono::system_clock::now().time_since_epoch().count()) +
        aneid.substr(4, 6) +
        extension.getExtensionName();

    delete &aneid;
}

AnnaInteraction::Response::~Response()
{
    delete &creationTime;

    delete[] &extensionID;
    delete[] &responseID;

    delete[] &response;
}
