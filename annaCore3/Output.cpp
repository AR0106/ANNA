#include "Output.h"

void AnnaInteraction::Output::setDirectInput(bool directInput)
{
	isDirectInput = directInput;
}

bool AnnaInteraction::Output::getDirectInput()
{
	return false;
}

int AnnaInteraction::Output::getMostRecentReponseIndex()
{
	return responses.size() - 1;
}

AnnaInteraction::Response AnnaInteraction::Output::getMostRecentResponse()
{
	if (!responses.empty())
	{
		return responses.back();
	}
}

std::string AnnaInteraction::Output::ProcessInput(std::string input)
{
	try
	{
		if (isDirectInput)
		{
			return input;
		} 
		
		throw Helper::NotImplemented();

	}
	catch (std::exception* e)
	{
		std::string exception(e->what());

		// Log Error to Log File
		Helper::FileOperations::WriteFile(std::vector<std::string>{std::to_string(PROCESS_FAILED), exception, input}, "Push Response\n1/2/2022", "log.txt", true);

		// Cleanup
		delete& exception;
	}
}

// Sends Command to Main Function for Execution
int AnnaInteraction::Output::SendCommand(std::string *command, std::string args[])
{
	try {
		throw Helper::NotImplemented();
		return SEND_COMMAND_SUCCESSFUL;
	}
	catch (std::exception* except) {
		std::string exception(except->what());
		std::string command_str(*command);

		// Log Error to Log File
		Helper::FileOperations::WriteFile(std::vector<std::string>{exception, command_str}, "Send Command\n1/2/2022", "log.txt", true);

		// Cleanup
		delete &exception;
		delete &command_str;

		return SEND_COMMAND_FAILED;
	}
}

// Add Response to Vector of Responses
int AnnaInteraction::Output::PushResponse(AnnaInteraction::Response *response)
{
	try {
		// Add Response to Response List
		responses.push_back(*response);
		return PUSH_SUCCESSFUL;
	}
	catch (std::exception* except) {
		std::string exception(except->what());

		// Log Error to Log File
		Helper::FileOperations::WriteFile(std::vector<std::string>{std::to_string(PUSH_FAILED), exception, response->getResponse()}, "Push Response\n1/2/2022", "log.txt", true);
		
		// Cleanup
		delete& exception;

		return PUSH_FAILED;
	}
}
