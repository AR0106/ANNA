#pragma once

// Libraries
#include <vector>
#include <string>
#include "Response.h"
#include "Helper.hpp"

// Definitions
#define PUSH_FAILED 010
#define PUSH_SUCCESSFUL 011
#define SEND_COMMAND_SUCCESSFUL 012
#define SEND_COMMAND_FAILED 013
#define PROCESS_SUCCESSFUL 014
#define PROCESS_FAILED 015

namespace AnnaInteraction
{
	class Output
	{
	private:
		bool isDirectInput = true;
		bool isGeneratedOutput = false;

		int mostRecentReponseIndex;

		AnnaInteraction::Response mostRecentResponse;

		std::vector<AnnaInteraction::Response> responses;

	public:
		// Toggles Direct Input for Debugging - DEVMODE ONLY
		void setDirectInput(bool directInput);
		// Gets Whether Direct Input is Enabled or not - DEVMODE ONLY
		bool getDirectInput();

		// Gets Index of Most Recent Response for 
		int getMostRecentReponseIndex();

		AnnaInteraction::Response getMostRecentResponse();

		std::string ProcessInput(std::string input);

		int SendCommand(std::string* command, std::string args[]);
		int PushResponse(AnnaInteraction::Response* response);
	};
}