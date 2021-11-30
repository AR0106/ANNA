#pragma once
#include <stdio.h>
#include <time.h>
#include <string>

namespace Interaction 
{
	class Response
	{
	private:
		time_t creationTime;

		std::string extensionID;
		std::string responseID;

		std::string response;

	public:
		time_t getTime();

		std::string getExtensionID();
		std::string getResponseID();

		Response(std::string aiResponse);
		~Response();
	};
}