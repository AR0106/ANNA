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
		
		void RunANNA(std::string input, std::array<std::string, sizeof(std::string)> args);

	protected:
		std::array<std::string, 7> colour{ "hello", "from", "user", "ANEID", "search", "time", "meaning"};


	};
}