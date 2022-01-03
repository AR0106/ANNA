#include "Core.h"

std::string AnnaCore::Core::GenerateANEID(std::string author, std::string extName, time_t compiledTime)
{
	// Temporary Algorithm
	///////////////////////////////////////////////////////
	///////////////////////////////////////////////////////
	//					  REWRITE LATER                  //
	///////////////////////////////////////////////////////
	///////////////////////////////////////////////////////
	return std::to_string(std::rand() % 99 + 10) + '\n' +
		author + '\n' +
		extName + '\n' +
		std::to_string(std::rand() % 9999999999999999 + 1111111111111111 / 12);
}

void AnnaCore::Core::RunANNA(std::string input, std::array<std::string, sizeof(std::string)> args)
{
	if (AnnaInteraction::Output::getDirectInput())
	{
		AnnaInteraction::Output::PushResponse(new AnnaInteraction::Response()
	}
}
