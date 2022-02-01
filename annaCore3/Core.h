#pragma once
// Standard Library Includes
#include <string>
#include <array>
#include <ctime>

// Internal Includes
#include "Helper.hpp"
#include "Output.h"
#include "Response.h"
#include "AnnaExtension.h"

namespace AnnaCore
{
	class Core
	{
	public:
		static std::string GenerateANEID(std::string author, std::string extName, time_t compiledTime);
		
		void InitANNA();
		void RunANNA(std::string input, std::vector<std::string> *args);

		static AnnaExtensions::Extension baseExtension;

	protected:
		std::array<std::string, 7> baseCommands{ "hello", "from", "user", "ANEID", "search", "time", "meaning"};
	};
}