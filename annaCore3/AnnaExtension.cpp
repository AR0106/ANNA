#include "AnnaExtension.h"

std::string AnnaExtensions::Extension::getName()
{
    return name;
}

std::string AnnaExtensions::Extension::getAuthor()
{
    return author;
}

std::string AnnaExtensions::Extension::getANEID()
{
    return aneid;
}

std::string AnnaExtensions::Extension::getUri()
{
    return uri;
}

std::string AnnaExtensions::Extension::getExtensionName()
{
    return extensionName;
}

time_t AnnaExtensions::Extension::getPublishedTime()
{
    return timePublished;
}

AnnaExtensions::Extension::Extension(std::string _name, std::string _author, std::string _extensionName)
{
    name = _name;
    author = _author;
    aneid = AnnaCore::Core::GenerateANEID(_author, _extensionName, 0);
    uri = nullptr;
    extensionName = _extensionName;
    timePublished = 0;
}

AnnaExtensions::Extension::~Extension()
{
    delete &name;
    delete &author;
    delete &aneid;
    delete &uri;
    delete &extensionName;
    delete &timePublished;
}

int AnnaExtensions::Extensions::InstallExtension()
{
    throw Helper::NotImplemented();
}
