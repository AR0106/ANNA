#pragma once
#include <string>
#include <vector>
#include <time.h>

namespace Extensions
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

	class Extenion
	{
	private:
		const std::string name;
		const std::string author;
		const std::string aneid;

		const std::string uri;

		const std::string extensionName;

		const time_t timePublished;

	public:
		std::vector<ICommand> commands;

		std::string getName();
		std::string getAuthor();
		std::string getANEID();
		
		std::string getUri();

		std::string getExtensionName();

		time_t getPublishedTime();
	};
}

