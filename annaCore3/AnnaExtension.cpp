#include "AnnaExtension.h"

std::string Extensions::Extenion::getName()
{
    return name;
}

std::string Extensions::Extenion::getAuthor()
{
    return author;
}

std::string Extensions::Extenion::getANEID()
{
    return aneid;
}

std::string Extensions::Extenion::getUri()
{
    return uri;
}

std::string Extensions::Extenion::getExtensionName()
{
    return extensionName;
}

time_t Extensions::Extenion::getPublishedTime()
{
    return timePublished;
}
