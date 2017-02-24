// MessageBoxWrapper.h: actually wrapper to the managed class

#pragma once

#include <vcclr.h>
#include <vector>
#include <string>
#include "IUMWrapper.h"

using namespace UpdateModul;

class DLLAPI    UMWrapper : IUMWrapper
{
private:
    gcroot<CUMWrapper ^>  _managedObject;
public:
    UMWrapper() { }
    
	//int CheckForUpdates(std::string *LanguageCode, std::string *Guid, std::string *InitDir, std::string *Params);
	//int OpenConfig(std::string *LanguageCode, std::string *Guid, std::string *InitDir, std::string *Params);

	int CheckForUpdates(const char *LanguageCode, const char *Guid, const char *InitDir, const char *Params);
	int OpenConfig(const char *LanguageCode, const char *Guid, const char *InitDir, const char *Params);
};

