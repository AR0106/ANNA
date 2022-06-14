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

// Initialize ANNA Base Library Extension
void AnnaCore::Core::InitANNA()
{
	AnnaCore::Core::baseExtension = AnnaExtensions::Extension("ANNA", "Reforce Labs", "reforceAnna");
}

void AnnaCore::Core::RunANNA(std::string input, std::vector<std::string> *args)
{
#if DEBUG
	if (AnnaInteraction::Output::getDirectInput())
	{
		AnnaInteraction::Output::PushResponse(new AnnaInteraction::Response(baseExtension, "Direct Input Mode"));
	}
#endif

	AnnaInteraction::Output::PushResponse(new AnnaInteraction::Response(baseExtension, "ANNA Initialized"));

	// Checks if Input is a Base Command
	if (std::find(baseCommands.begin(), baseCommands.end(), input) != baseCommands.end())
	{
		switch (Helper::StringOperations::StrToUInt("hello"))
		{
		default:
			break;
		}
	}
}
