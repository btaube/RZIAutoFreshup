// IMessageBoxWrapper.h: interface to hide the managed implementation from the dll client

#pragma once

#include <string>

#ifdef UMWRAPPER_EXPORTS
#define DLLAPI  __declspec(dllexport)
#else
#define DLLAPI  __declspec(dllimport)
#pragma comment (lib, "UMWrapper.lib") // if importing, link also
#endif


class DLLAPI    IUMWrapper
{
public:
	//virtual int CheckForUpdates(std::string *LanguageCode, std::string *Guid, std::string *InitDir, std::string *Params) = 0;
	//virtual int OpenConfig(std::string *LanguageCode, std::string *Guid, std::string *InitDir, std::string *Params) = 0;

	virtual int CheckForUpdates(const char *LanguageCode, const char *Guid, const char *InitDir, const char *Params) = 0;
	virtual int OpenConfig(const char *LanguageCode, const char *Guid, const char *InitDir, const char *Params) = 0;

	static IUMWrapper *CreateInstance();

	static void	Destroy(IUMWrapper *instance);
};
