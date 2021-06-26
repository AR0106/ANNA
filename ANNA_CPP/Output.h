#pragma once
#using "annaCore2.dll"

using namespace ANNA::Interaction;

ref class COutput
{
public:
	static bool CdirectInput = false;

	//static System::Collections::Generic::List<>

	static void CSendCommand(System::String ^command, array<System::String ^> ^args)
	{
		Output::SendCommand(command, args);
		return;
	}

	static int CSay(System::String ^sentence) 
	{
		return Output::Say(sentence);
	}
};

