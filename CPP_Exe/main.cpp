// main.cpp: a totaly unmanaged application

#include "IUMWrapper.h"
#include <string>
#include <thread>
#include <iostream>




int main(int argc, char **argv)
{
	
	const char *LanguageCode = "de-DE";
	//std::string *LanguageCode;
	//LanguageCode = new std::string();
	//*LanguageCode = "de-DE";

	const char *Guid = "aca52b14-b579-4684-835a-c88ba09b6106";
	//std::string *Guid;
	//Guid = new std::string();
	//*Guid = "aca52b14-b579-4684-835a-c88ba09b6106";

	const char *InitDir = "C:\\temp";
	//std::string *InitDir;
	//InitDir = new std::string();
    //*InitDir = "C:\\temp";

	const char *Params = "-lang=en-GB, test=test2, ";
	//std::string *Params;	
	//Params = new std::string();
	//*Params = "-lang=en-GB, test=test2, ";


	// true = CheckForUpdates, false = OpenConfig
	bool openUpdateCheck = false;
	int retVal = -9999;

	if (openUpdateCheck) {
		IUMWrapper *wrapper = IUMWrapper::CreateInstance();
		retVal = wrapper->CheckForUpdates(LanguageCode, Guid, InitDir, Params);
		IUMWrapper::Destroy(wrapper);
	}
	else {
		IUMWrapper *wrapper = IUMWrapper::CreateInstance();
		try {
			retVal = wrapper->OpenConfig(LanguageCode, Guid, InitDir, Params);
		}
		catch (...) {
			//std::cerr << ex.what();
		}
		
		IUMWrapper::Destroy(wrapper);
	}
	

	delete(LanguageCode);
	delete(Guid);
	delete(InitDir);
	delete(Params);
	return (retVal);


}
