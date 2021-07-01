#pragma once
ref class CExtension
{
public:
	System::String Name;

	System::String Author;

	System::DateTime Time;

	System::String getANEID();

	System::Uri Link;

	System::String GetExtName();
};

