#pragma once
#include <string>
#include <vector>
#include <ctime>

#include "Helper.hpp"
#include "Core.h"

namespace AnnaExtensions
{
	class ICommand
	{
	public:
		// Command to Run
		virtual void OnRun() = 0;

		// Words That are Interpreted to Invoke the OnRun Method
		virtual std::vector<std::string> ActionWords() = 0;

		// Example Initialization Sentences Meant to Verify the Action Words
		virtual std::vector<std::string> ExampleSentences() = 0;
	};

	class Extension
	{
	private:
		std::string name;
		std::string author;
		std::string aneid;

		std::string uri;

		std::string extensionName;

		time_t timePublished;

	public:
		std::vector<ICommand> commands;

		std::string getName();
		std::string getAuthor();
		std::string getANEID();
		
		std::string getUri();

		std::string getExtensionName();

		time_t getPublishedTime();

		Extension(std::string _name, std::string _author, std::string _extensionName);
		~Extension();
	};

	class Extensions
	{
	public:
		int InstallExtension();
	};
}

