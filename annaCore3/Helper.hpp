#pragma once
#include <stdexcept>
#include <iostream>
#include <fstream>
#include <string>

#define FILE_SUCCESS 000
#define FILE_FAILURE 001

namespace Helper
{
	class NotImplemented : public std::logic_error
	{
	public:
		NotImplemented() : std::logic_error("Function Not Yet Implemented") { };
	};

	class FileOperations
	{
	public:

		// Creates and/or Appends to File
		static int WriteFile(std::vector<std::string> lines, const char* header, const char* fileName, bool append)
		{
			try
			{
				// Create and Open File
				std::ofstream file;

				// Checks Whether the File is Appendable
				if (append && FileExists(fileName))
				{
					file.open(fileName, std::ios_base::app);
				}
				else 
				{
					file.open(fileName);
				}

				// Adds Headers to File
				if (header != nullptr)
				{
					file << header << '\n';
				}

				// Iterates Through 'lines' Vector and Adds Each Line to the File
				for (auto& line : lines)
				{
					file << line << '\n';
				}

				file.close();
				delete &file;

				return FILE_SUCCESS;
			}
			catch (std::exception except)
			{
				throw except;
				return FILE_FAILURE;
			}
		}

		// Gets Line Count of Input File Stream
		static int GetLineCount(std::ifstream file)
		{
			try
			{
				// Stop Skipping of New Lines
				file.unsetf(std::ios_base::skipws);

				// Count Number of Lines in File
				unsigned lineCount = std::count(
					std::istream_iterator<char>(file),
					std::istream_iterator<char>(),
					('\n'));

				return lineCount;
			}
			catch (const std::exception* e)
			{
				return FILE_FAILURE;
			}
		}

		// Checks if a File Exists
		static bool FileExists(const char* fileName) 
		{
			struct stat buffer;
			return (stat(fileName, &buffer) == 0);
		}

		// Reads Input File Stream
		static std::vector<std::string> ReadFile(std::ifstream file)
		{
			std::vector<std::string> lines{};
			std::string line;

			try
			{
				// Reads File and Adds Read Lines to Lines
				if (file.is_open())
				{
					while (std::getline(file, line))
					{
						lines.push_back(line);
					}

					file.close();
				}

				delete &line;
				
				return lines;

				delete[] &lines;
			}
			catch (const std::exception& e)
			{
				delete &line;
				delete[] & lines;

				throw e;
			}
		}
	};

	class StringOperations {
	public:
		static constexpr unsigned int StrToUInt(const char* str, int h = 0)
		{
			return !str[h] ? 5381 : (StrToUInt(str, h + 1) * 33) ^ str[h];
		}
	};
};

