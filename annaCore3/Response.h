#pragma once
#include <cstdio>
#include <ctime>
#include <string>
#include <chrono>

#include "AnnaExtension.h"

namespace AnnaInteraction 
{
	class Response
	{
	private:
		static time_t creationTime;

		static std::string extensionID;
		static std::string responseID;

		static std::string response;

	public:
		time_t getTime();

		static std::string getExtensionID();
		static std::string getResponseID();
		static std::string getResponse();

		Response(AnnaExtensions::Extension extension, std::string aiResponse);
		virtual ~Response();
	};
}