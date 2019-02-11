#ifndef _CORE_H
#define _CORE_H

#ifndef WIN32_LEAN_AND_MEAN
// Prevents the Winsock.h from being included by the Windows.h header.
	#define WIN32_LEAN_AND_MEAN
#endif

#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <iphlpapi.h> // Required if an application is using the IP Helper APIs

#include <iostream>
#include <cstdio>

#pragma comment(lib, "Ws2_32.lib")
#pragma comment (lib, "Mswsock.lib")
#pragma comment (lib, "AdvApi32.lib")

class Core {
private:
	WSADATA wsaData; // WSADATA contains information about the Windows Sockets implementation.
	int iResult;

public:
	int InitWinsock();
	Core();
};

#endif // !_CORE_H
