// MessageBoxWrapper.cpp: implementation of MessageBoxWrapper and class factory

#include "IUMWrapper.h"
#include "UMWrapper.h"

using namespace System::Collections::Generic;

[System::STAThread]
int UMWrapper::CheckForUpdates(const char *LanguageCode, const char *Guid, const char *InitDir, const char *Params)
//int UMWrapper::CheckForUpdates(std::string *LanguageCode, std::string *Guid, std::string *InitDir, std::string *Params)
{

	_managedObject = gcnew CUMWrapper(gcnew System::String(LanguageCode), gcnew System::String(Guid), gcnew System::String(InitDir), gcnew System::String(Params));
	//_managedObject = gcnew CUMWrapper(gcnew System::String(LanguageCode->c_str()), gcnew System::String(Guid->c_str()), gcnew System::String(InitDir->c_str()), gcnew System::String(Params->c_str()));
	return _managedObject->CheckForUpdates();
}

[System::STAThread]
int UMWrapper::OpenConfig(const char *LanguageCode, const char *Guid, const char *InitDir, const char *Params)
//int UMWrapper::OpenConfig(std::string *LanguageCode, std::string *Guid, std::string *InitDir, std::string *Params)
{
	_managedObject = gcnew CUMWrapper(gcnew System::String(LanguageCode), gcnew System::String(Guid), gcnew System::String(InitDir), gcnew System::String(Params));
	//_managedObject = gcnew CUMWrapper(gcnew System::String(LanguageCode->c_str()), gcnew System::String(Guid->c_str()), gcnew System::String(InitDir->c_str()), gcnew System::String(Params->c_str()));
	return _managedObject->OpenConfig();
}

IUMWrapper	*IUMWrapper::CreateInstance()
{
	return ((IUMWrapper *)new UMWrapper());
}

void IUMWrapper::Destroy(IUMWrapper *instance)
{
	delete instance;
}